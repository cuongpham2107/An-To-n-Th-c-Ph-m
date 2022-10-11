using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;
using System.Net.Mail;
using System.Net;
using DevExpress.ExpressApp;
using System.Text;
using System.Collections.Generic;

namespace Attp.Module.BusinessObjects {
	[XafDisplayName("Cấu hình"), DefaultClassOptions()]
	public class Configuration : BaseObject {
		public Configuration(Session session) : base(session) { }

		string key;
		[XafDisplayName("Khóa")]
		public string Key {
			get => key;
			set => SetPropertyValue(nameof(Key), ref key, value);
		}

		string data;
		[XafDisplayName("Giá trị")]
		public string Value {
			get => data;
			set => SetPropertyValue(nameof(Value), ref data, value);
		}

		string description;
		[XafDisplayName("Ghi chú")]
		public string Description {
			get => description;
			set => SetPropertyValue(nameof(Description), ref description, value);
		}
	}

	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Mẫu email")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	public class EmailTemplate : BaseObject {
		public EmailTemplate(Session session) : base(session) { }

		string name;
		[XafDisplayName("Tên mẫu"), ToolTip("")]
		public string Name {
			get => name;
			set => SetPropertyValue(nameof(Name), ref name, value);
		}

		string header;
		[XafDisplayName("Tiêu đề"), ToolTip("")]
		public string Header {
			get => header;
			set => SetPropertyValue(nameof(Header), ref header, value);
		}

		string body;
		[XafDisplayName("Nội dung"), ToolTip(""), Size(SizeAttribute.Unlimited)]
		public string Body {
			get => body;
			set => SetPropertyValue(nameof(Body), ref body, value);
		}
	}

	public class Helper {
		public class EmailParameter {
			public string Host { get; set; }
			public int Port { get; set; }
			public string Account { get; set; }
			public string Password { get; set; }
			public string From { get; set; }
		}

		public static EmailParameter GetEmailParams(IObjectSpace objectSpace) {
			return new() {
				Host = objectSpace.GetObjectsQuery<Configuration>().FirstOrDefault(c => c.Key == "EmailHost").Value,
				Port = int.Parse(objectSpace.GetObjectsQuery<Configuration>().FirstOrDefault(c => c.Key == "EmailPort").Value),
				Account = objectSpace.GetObjectsQuery<Configuration>().FirstOrDefault(c => c.Key == "EmailAccount").Value,
				Password = objectSpace.GetObjectsQuery<Configuration>().FirstOrDefault(c => c.Key == "EmailPassword").Value,
				From = objectSpace.GetObjectsQuery<Configuration>().FirstOrDefault(c => c.Key == "EmailFrom").Value
			};
		}

		public static (string Header, string Body) GetEmailTemplate(string name, IObjectSpace objectSpace) {
			var template = objectSpace.GetObjectsQuery<EmailTemplate>().FirstOrDefault(c => c.Name == name);
			return (template.Header, template.Body);
		}

		public static void SendEmail(string from, string to, string subject, string body, EmailParameter parameter) {
			try {
				MailMessage message = new();
				SmtpClient smtp = new();
				message.From = new MailAddress(from);
				message.To.Add(new MailAddress(to));
				message.Subject = subject;
				message.IsBodyHtml = true; //to make message body as html  
				message.Body = body;
				smtp.Port = parameter.Port;
				smtp.Host = parameter.Host; //for gmail host  
				smtp.EnableSsl = true;
				smtp.UseDefaultCredentials = false;
				smtp.Credentials = new NetworkCredential(parameter.Account, parameter.Password);
				smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
				smtp.Send(message);
			}
			catch (Exception) {
				throw new UserFriendlyException("Có lỗi trong quá trình gửi email. Hãy liên hệ với quản trị viên để xử lý.");
			}
		}

		public static string CreateBody(Dictionary<string, string> pairs, string template) {
			StringBuilder sb = new(template);
			foreach (var pair in pairs) {
				sb.Replace(pair.Key, pair.Value);
			}
			return sb.ToString();
		}
	}
}
