using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	//[XafDefaultProperty(nameof(PropertyName))]
	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("File dữ liệu")]
	//[NavigationItem(R.DataMenuItem)]
	[NavigationItem(R.V1)]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[FileAttachment(nameof(FileData))]
	public class FileDuLieu : BaseObject {
		public FileDuLieu(Session session) : base(session) { }


		FileData fileData;
		[XafDisplayName("File dữ liệu"), ToolTip("")]
		public FileData FileData {
			get => fileData;
			set => SetPropertyValue(nameof(FileData), ref fileData, value);
		}

		string moTa;
		[XafDisplayName("Mô tả"), ToolTip("")]
		public string MoTa {
			get => moTa;
			set => SetPropertyValue(nameof(MoTa), ref moTa, value);
		}

		DateTime thoiGian = DateTime.Now;
		[XafDisplayName("Thời gian tạo"), ToolTip("")]
		public DateTime ThoiGian {
			get => thoiGian;
			set => SetPropertyValue(nameof(ThoiGian), ref thoiGian, value);
		}
	}
}