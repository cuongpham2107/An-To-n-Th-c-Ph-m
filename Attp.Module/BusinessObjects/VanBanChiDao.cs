using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	[NavigationItem(R.V1)]
	//[NavigationItem(R.DataMenuItem)]
	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Văn bản chỉ đạo")]
	[DefaultProperty(nameof(TenVanBan))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	//[Appearance("HideDefaultId", TargetItems = "KyBaoCao", Criteria = "not IsCurrentUserInRole('Administrators')", Visibility = ViewItemVisibility.Hide, Context = "Any")]	
	public class VanBanChiDao : BaseObject {
		public VanBanChiDao(Session session)
			: base(session) {
		}

		#region Overrides
		public override void AfterConstruction() {
			base.AfterConstruction();
			if (SecuritySystem.CurrentUser != null) {
				var account = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
				CoQuanQuanLy = account.CoquanQuanly;
			}
		}
		#endregion


		string tenVanBan;
		[XafDisplayName("Tên văn bản")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenVanBan {
			get => tenVanBan;
			set => SetPropertyValue(nameof(TenVanBan), ref tenVanBan, value);
		}

		string soVanBan;
		[XafDisplayName("Số văn bản")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string SoVanBan {
			get => soVanBan;
			set => SetPropertyValue(nameof(SoVanBan), ref soVanBan, value);
		}

		CoQuanQuanLy coQuanQuanLy;
		[XafDisplayName("Cơ quan ban hành")]
		public CoQuanQuanLy CoQuanQuanLy {
			get => coQuanQuanLy;
			set => SetPropertyValue(nameof(CoQuanQuanLy), ref coQuanQuanLy, value);
		}

		DateTime ngayBanHanh;
		[XafDisplayName("Ngày ban hành")]
		public DateTime NgayBanHanh {
			get => ngayBanHanh;
			set {
				SetPropertyValue(nameof(NgayBanHanh), ref ngayBanHanh, value);
				if (!IsLoading && !IsSaving) {
					var nam = NgayBanHanh.Year;
					var thang = NgayBanHanh.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					this.KyBaoCao = ky;
				}
			}
		}

		FileData fileData;
		[XafDisplayName("File văn bản")]
		public FileData FileData {
			get => fileData;
			set => SetPropertyValue(nameof(FileData), ref fileData, value);
		}

		// nhóm thuộc tính báo cáo
		KyBaoCao kyBaoCao;
		[XafDisplayName("Báo cáo tháng")]
		[VisibleInListView(false)]
		[Association("KyBaoCao-VanBanChiDaos")]
		public KyBaoCao KyBaoCao {
			get => kyBaoCao;
			set {
				SetPropertyValue(nameof(KyBaoCao), ref kyBaoCao, value);
				if (!IsLoading && !IsSaving) {
					BaoCaoQuy = KyBaoCao?.BaoCaoQuy;
					BaoCaoSauThang = KyBaoCao?.BaoCaoSauThang;
					BaoCaoNam = KyBaoCao?.BaoCaoNam;
				}
			}
		}

		BaoCaoQuy baoCaoQuy;
		[XafDisplayName("Báo cáo quý"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoQuy-VanBanChiDaos")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		[XafDisplayName("Báo cáo 6 tháng"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoSauThang-VanBanChiDaos")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoNam baoCaoNam;
		[XafDisplayName("Báo cáo năm"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoNam-VanBanChiDaos")]
		public BaoCaoNam BaoCaoNam {
			get => baoCaoNam;
			set => SetPropertyValue(nameof(BaoCaoNam), ref baoCaoNam, value);
		}

		bool pheDuyet;
		[XafDisplayName("Phê duyệt"), ToolTip(""), ModelDefault("AllowEdit", "False")]
		public bool PheDuyet {
			get => pheDuyet;
			set => SetPropertyValue(nameof(PheDuyet), ref pheDuyet, value);
		}
		[Action(Caption = "Phê duyệt", ConfirmationMessage = "Phê duyệt dữ liệu này? Sau khi phê duyệt sẽ KHÔNG thể sửa chữa thông tin được nữa.", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=False", TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueAtLeastForOne, SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
		public void PheDuyetAction() {
			PheDuyet = true;
		}

		[Action(Caption = "Hủy phê duyệt", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=True")]
		public void PheDuyetActionUndo() {
			PheDuyet = false;
		}
	}
	//[XafDefaultProperty(nameof(PropertyName))]
}