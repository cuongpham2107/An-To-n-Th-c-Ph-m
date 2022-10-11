using Attp.Module.BusinessObjects;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Attp.Module.Controllers {
	public class DuLieuKiemTraEmailViewController : ViewController {
		public DuLieuKiemTraEmailViewController() {
			TargetObjectType = typeof(DuLieuKiemTraCSSXKD);
			SendEmailAction();
		}
		void SendEmailAction() {
			SimpleAction action = new(this, "SendEmailDuLieuKiemTra", DevExpress.Persistent.Base.PredefinedCategory.Edit) {
				Caption = "Email thông báo",
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject
			};
			action.Execute += Action_Execute;
		}

		private void Action_Execute(object sender, SimpleActionExecuteEventArgs e) {
			Helper.EmailParameter parameter = Helper.GetEmailParams(ObjectSpace);
			var obj = e.CurrentObject as DuLieuKiemTraCSSXKD;
			var coso = obj?.CoSoSanXuatKinhDoanh;
			if (coso == null) throw new UserFriendlyException("Dữ liệu chưa được gán cho cơ sở");
			if (string.IsNullOrEmpty(coso.Email)) throw new UserFriendlyException("Không có địa chỉ email của doanh nghiệp này.");

			var (header, body) = Helper.GetEmailTemplate("DuLieuKiemTra", ObjectSpace);
			if (string.IsNullOrEmpty(body))
				body = "Kính gửi chủ cơ sở {{ten}}.<br/>Trân trọng thông báo kết quả kiểm tra như sau:<br/>Ngày kiểm tra: {{ngay}}<br/>Kết quả: {{ketqua}}<br/>Cơ quan thực hiện: {{coquan}}";

			Dictionary<string, string> pairs = new() {
				{ "{{ten}}", coso.TenCoSo },
				{ "{{ngay}}", obj.NgayKiemTra.ToShortDateString() },
				{ "{{ketqua}}", obj.KetQua.ToString() },
				{ "{{coquan}}", obj.CoQuanQuanLy?.TenCoQuan },
			};
			var message = Helper.CreateBody(pairs, body);
			Helper.SendEmail(parameter.From, coso.Email, header, message, parameter);
		}
	}

	public class KeHoachThanhKiemTraEmailViewController : ViewController {
		public KeHoachThanhKiemTraEmailViewController() {
			TargetObjectType = typeof(KeHoachThanhKiemTra);
			SendEmailAction();
		}
		void SendEmailAction() {
			SimpleAction action = new(this, "SendEmailKeHoachThanhKiemTra", DevExpress.Persistent.Base.PredefinedCategory.Edit) {
				Caption = "Email thông báo",
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject
			};
			action.Execute += Action_Execute;
		}

		private void Action_Execute(object sender, SimpleActionExecuteEventArgs e) {
			Helper.EmailParameter parameter = Helper.GetEmailParams(ObjectSpace);
			var obj = e.CurrentObject as KeHoachThanhKiemTra;
			var coso = obj?.CoSoSanXuatKinhDoanh;

			if (coso == null) throw new UserFriendlyException("Kế hoạch chưa được gán cho cơ sở");
			if (string.IsNullOrEmpty(coso.Email)) throw new UserFriendlyException("Không có địa chỉ email của doanh nghiệp này.");

			var (header, body) = Helper.GetEmailTemplate("KeHoachThanhKiemTra", ObjectSpace);
			if (string.IsNullOrEmpty(body))
				body = "Kính gửi chủ cơ sở {{ten}}.<br/>Trân trọng thông báo kế hoạch thanh tra như sau:<br/>Ngày thanh tra: {{ngay}}<br/>Nội dung: {{noidung}}<br/>Cơ quan thực hiện: {{coquan}}";

			Dictionary<string, string> pairs = new() {
				{ "{{ten}}", coso.TenCoSo },
				{ "{{ngay}}", obj.NgayThamDinh.ToShortDateString() },
				{ "{{noidung}}", obj.NoiDung },
				{ "{{coquan}}", obj.CoQuanQuanLy?.TenCoQuan },
			};
			var message = Helper.CreateBody(pairs, body);

			Helper.SendEmail(parameter.From, coso.Email, header, message, parameter);
		}
	}

	public class KeHoachThamDinhEmailViewController : ViewController {
		public KeHoachThamDinhEmailViewController() {
			TargetObjectType = typeof(KeHoachThamDinh);
			SendEmailAction();
		}
		void SendEmailAction() {
			SimpleAction action = new(this, "SendEmailKeHoachThamDinh", DevExpress.Persistent.Base.PredefinedCategory.Edit) {
				Caption = "Email thông báo",
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject
			};
			action.Execute += Action_Execute;
		}

		private void Action_Execute(object sender, SimpleActionExecuteEventArgs e) {
			Helper.EmailParameter parameter = Helper.GetEmailParams(ObjectSpace);
			var obj = e.CurrentObject as KeHoachThamDinh;
			var coso = obj?.CoSoSanXuatKinhDoanh;

			if (coso == null) throw new UserFriendlyException("Kế hoạch chưa được gán cho cơ sở");
			if (string.IsNullOrEmpty(coso.Email)) throw new UserFriendlyException("Không có địa chỉ email của doanh nghiệp này.");

			var ten = coso.TenCoSo; // {{ten}}
			var ngay = obj.NgayThamDinh.ToShortDateString(); // {{ngay}}
			var noidung = obj.NoiDung; // {{noidung}}
			var coquan = obj.CoQuanQuanLy?.TenCoQuan; // {{coquan}}		

			var (header, body) = Helper.GetEmailTemplate("KeHoachThamDinh", ObjectSpace);

			if (string.IsNullOrEmpty(body))
				body = "Kính gửi chủ cơ sở {{ten}}.<br/>Trân trọng thông báo kế hoạch thẩm định như sau:<br/>Ngày thẩm định: {{ngay}}<br/>Nội dung: {{noidung}}<br/>Cơ quan thực hiện: {{coquan}}";

			StringBuilder sb = new(body);
			var message = sb
			.Replace("{{ten}}", ten)
			.Replace("{{ngay}}", ngay)
			.Replace("{{noidung}}", noidung)
			.Replace("{{coquan}}", coquan)
			.ToString();

			Helper.SendEmail(parameter.From, coso.Email, header, message, parameter);
		}
	}

	public class GiayChungNhanEmailViewController : ViewController {
		public GiayChungNhanEmailViewController() {

			// Target required Views (via the TargetXXX properties) and create their Actions.
			TargetObjectType = typeof(GiayChungNhan);

			SendEmailAction();
			SendEmailThuHoiAction();
		}

		private void SendEmailThuHoiAction() {
			SimpleAction action = new(this, "SendEmailThuHoiFromGiayChungNhan", "Edit") {
				Caption = "Email Thu hồi Giấy chứng nhận",
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
				TargetObjectsCriteria = "[BiThuHoi]=True"
			};

			action.Execute += (s, e) => {
				Helper.EmailParameter parameter = Helper.GetEmailParams(ObjectSpace);
				var gcn = (e.CurrentObject as GiayChungNhan);
				var coso = gcn?.CoSoSanXuatKinhDoanh;

				if (coso == null) throw new UserFriendlyException("Giấy chứng nhận chưa được gán cho cơ sở");
				if (string.IsNullOrEmpty(coso.Email)) throw new UserFriendlyException("Không có địa chỉ email của doanh nghiệp này.");

				var ten = coso.TenCoSo; // {{ten}}				
				var so = gcn.SoCap; // {{so}}
				var ngaycap = gcn.NgayCap.ToShortDateString(); // {{ngaycap}}				
				var thoihan = gcn.CoHieuLucDen.ToShortDateString(); // {{thoihan}}
				var ngaythuhoi = gcn.NgayThuHoi.ToShortDateString(); // {{ngaythuhoi}}
				var lydo = gcn.LyDoThuHoi; // {{lydo}}

				var (header, body) = Helper.GetEmailTemplate("ThuHoiGiayChungNhan", ObjectSpace);

				if (string.IsNullOrEmpty(body))
					body = "Kính gửi chủ cơ sở {{ten}}.<br/>Trân trọng thông báo: giấy chứng nhận an toàn thực phẩm số {{so}}, ngày cấp {{ngaycap}}, thời hạn tới {{thoihan}} đã bị thu hồi.<br/>Ngày thu hồi: {{ngaythuhoi}}.<br/>Lý do: {{lydo}}";

				StringBuilder sb = new(body);
				var message = sb
				.Replace("{{ten}}", ten)
				.Replace("{{so}}", so)
				.Replace("{{ngaycap}}", ngaycap)
				.Replace("{{thoihan}}", thoihan)
				.Replace("{{ngaythuhoi}}", ngaythuhoi)
				.Replace("{{lydo}}", lydo)
				.ToString();

				Helper.SendEmail(parameter.From, coso.Email, header, message, parameter);
			};
		}

		private void SendEmailAction() {
			SimpleAction action = new(this, "SendEmailFromGiayChungNhan", "Edit") {
				Caption = "Email Giấy chứng nhận",
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
				TargetObjectsCriteria = "[BiThuHoi]=False"
			};

			action.Execute += (s, e) => {
				Helper.EmailParameter parameter = Helper.GetEmailParams(ObjectSpace);
				var gcn = (e.CurrentObject as GiayChungNhan);
				var coso = gcn?.CoSoSanXuatKinhDoanh;

				if (coso == null) throw new UserFriendlyException("Giấy chứng nhận chưa được gán cho cơ sở");
				if (string.IsNullOrEmpty(coso.Email)) throw new UserFriendlyException("Không có địa chỉ email của doanh nghiệp này.");

				var ten = coso.TenCoSo; // {{ten}}
				var so = gcn.SoCap; // {{so}}
				var ngaycap = gcn.NgayCap.ToShortDateString(); // {{ngaycap}}
				var thoihan = gcn.CoHieuLucDen.ToShortDateString(); // {{thoihan}}

				var (header, body) = Helper.GetEmailTemplate("GiayChungNhan", ObjectSpace);

				if (string.IsNullOrEmpty(body))
					body = "Kính gửi chủ cơ sở {{ten}}.<br/>Trân trọng thông báo đơn vị đã được cấp giấy chứng nhận an toàn thực phẩm số {{so}}.<br/>Ngày cấp {{ngaycap}}.<br/>Thời hạn tới {{thoihan}}.";

				StringBuilder sb = new(body);
				var message = sb
				.Replace("{{ten}}", ten)
				.Replace("{{so}}", so)
				.Replace("{{ngaycap}}", ngaycap)
				.Replace("{{thoihan}}", thoihan)
				.ToString();

				Helper.SendEmail(parameter.From, coso.Email, header, message, parameter);
			};
		}
	}
}
