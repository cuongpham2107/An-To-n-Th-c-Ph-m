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
	[DefaultClassOptions]
	[XafDisplayName("Báo cáo tháng")]
	[DefaultProperty(nameof(TenKyBaoCao))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	//[Appearance("HideDefaultId", TargetItems = "BaoCaoNam, baoCaoSauThang, BaoCaoQuy", Criteria = "not IsCurrentUserInRole('Administrators')", Visibility = ViewItemVisibility.Hide, Context = "Any")]
	public class KyBaoCao : BaseObject {
		public KyBaoCao(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
			Nam = DateTime.Today.Year;
			Thang = DateTime.Today.Month;
		}

		int nam;
		[XafDisplayName("Năm")]
		public int Nam {
			get => nam;
			set {
				SetPropertyValue(nameof(Nam), ref nam, value);
				if (!IsLoading)
					TenKyBaoCao = $"Báo cáo tháng {Thang} năm {Nam}";
			}
		}

		int thang;
		[XafDisplayName("Tháng")]
		public int Thang {
			get => thang;
			set {
				SetPropertyValue(nameof(Thang), ref thang, value);
				if (!IsLoading)
					TenKyBaoCao = $"Báo cáo tháng {Thang} năm {Nam}";
			}
		}

		string tenKyBaoCao;
		[XafDisplayName("Tên báo cáo")]
		public string TenKyBaoCao {
			get => tenKyBaoCao;
			set => SetPropertyValue(nameof(TenKyBaoCao), ref tenKyBaoCao, value);
		}

		BaoCaoNam baoCaoNam;
		//[Association("BaoCaoNam-KyBaoCaos")]
		[XafDisplayName("Báo cáo năm")]
		public BaoCaoNam BaoCaoNam {
			get => baoCaoNam;
			set => SetPropertyValue(nameof(BaoCaoNam), ref baoCaoNam, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		//[Association("BaoCaoSauThang-KyBaoCaos")]
		[XafDisplayName("Báo cáo 6 tháng")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoQuy baoCaoQuy;
		//[Association("BaoCaoQuy-KyBaoCaos")]
		[XafDisplayName("Báo cáo quý")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		//bool pheDuyet;
		//[XafDisplayName("Phê duyệt"), ToolTip("")]
		//[ModelDefault("AllowEdit", "False")]
		//public bool PheDuyet {
		//	get => pheDuyet;
		//	set => SetPropertyValue(nameof(PheDuyet), ref pheDuyet, value);
		//}

		//[Action(Caption = "Phê duyệt", ConfirmationMessage = "Phê duyệt báo cáo này? Sau khi phê duyệt, dữ liệu của kỳ báo cáo tháng này sẽ không thể sửa được nữa.", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=False", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
		//public void PheDuyetAction() {
		//	PheDuyet = true;
		//}

		#region Associations
		[Association("KyBaoCao-KeHoachThamDinhs")]
		[XafDisplayName("Kế hoạch thẩm định")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThamDinh> KeHoachThamDinhs {
			get => GetCollection<KeHoachThamDinh>(nameof(KeHoachThamDinhs));
		}

		[Association("KyBaoCao-KeHoachThanhKiemTras")]
		[XafDisplayName("Kế hoạch thanh kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThanhKiemTra> KeHoachThanhKiemTras {
			get => GetCollection<KeHoachThanhKiemTra>(nameof(KeHoachThanhKiemTras));
		}

		[Association("KyBaoCao-GiayChungNhans")]
		[XafDisplayName("Giấy chứng nhận đã cấp")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<GiayChungNhan> GiayChungNhans {
			get => GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
		}

		[Association("KyBaoCao-DuLieuKiemTraCSSXKDs")]
		[XafDisplayName("Kết quả kiểm tra cơ sở SXKD")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<DuLieuKiemTraCSSXKD> DuLieuKiemTraCSSXKDs => GetCollection<DuLieuKiemTraCSSXKD>(nameof(DuLieuKiemTraCSSXKDs));

		[Association("KyBaoCao-VanBanChiDaos")]
		[XafDisplayName("Văn bản chỉ đạo đã ban hành")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<VanBanChiDao> VanBanChiDaos {
			get => GetCollection<VanBanChiDao>(nameof(VanBanChiDaos));
		}

		[Association("KyBaoCao-HoatDongSanPhamTruyenThongs")]
		[XafDisplayName("Hoạt động sản phẩm truyền thông đã thực hiện")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<HoatDongSanPhamTruyenThong> HoatDongSanPhamTruyenThongs {
			get => GetCollection<HoatDongSanPhamTruyenThong>(nameof(HoatDongSanPhamTruyenThongs));
		}

		#endregion

		private XPCollection<AuditDataItemPersistent> auditTrail;
		[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
		public XPCollection<AuditDataItemPersistent> AuditTrail {
			get {
				if (auditTrail == null) {
					auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
				}
				return auditTrail;
			}
		}
	}
}