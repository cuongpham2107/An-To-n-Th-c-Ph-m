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
	//[NavigationItem(R.DataMenuItem)]
	[NavigationItem(R.V1)]
	[DefaultClassOptions]
	[XafDisplayName("Hoạt động sản phẩm truyền thông")]
	[DefaultProperty(nameof(TenHoatDongSanPhamTruyenThong))]
	//[Appearance("HideDefaultId", TargetItems = "KyBaoCao", Criteria = "not IsCurrentUserInRole('Administrators')", Visibility = ViewItemVisibility.Hide, Context = "Any")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[ImageName("BO_Contact")]
	public class HoatDongSanPhamTruyenThong : BaseObject {
		public HoatDongSanPhamTruyenThong(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
			if (SecuritySystem.CurrentUser != null) {
				var account = Session.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);
				CoQuanQuanLy = account.CoquanQuanly;
			}
		}

		string tenHoatDongSanPhamTruyenThong;
		[XafDisplayName("Tên hoạt động/sản phẩm truyền thông")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenHoatDongSanPhamTruyenThong {
			get => tenHoatDongSanPhamTruyenThong;
			set => SetPropertyValue(nameof(TenHoatDongSanPhamTruyenThong), ref tenHoatDongSanPhamTruyenThong, value);
		}

		CoQuanQuanLy donVi;
		[XafDisplayName("Đơn vị tổ chức"), ToolTip("")]
		public CoQuanQuanLy CoQuanQuanLy {
			get => donVi;
			set => SetPropertyValue(nameof(CoQuanQuanLy), ref donVi, value);
		}

		string soLuong;
		[XafDisplayName("Số lượng/Buổi")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string SoLuong {
			get => soLuong;
			set => SetPropertyValue(nameof(SoLuong), ref soLuong, value);
		}

		string soNguoiThamDu;
		[XafDisplayName("Số người tham dự")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string SoNguoiThamDu {
			get => soNguoiThamDu;
			set => SetPropertyValue(nameof(SoNguoiThamDu), ref soNguoiThamDu, value);
		}

		DateTime ngayBatdau;
		[XafDisplayName("Từ ngày"), ToolTip("")]
		public DateTime NgayBatdau {
			get => ngayBatdau;
			set {
				SetPropertyValue(nameof(NgayBatdau), ref ngayBatdau, value);
				if (!IsLoading && !IsSaving) {
					var nam = NgayBatdau.Year;
					var thang = NgayBatdau.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					this.KyBaoCao = ky;
				}
			}
		}

		DateTime ngayKetthuc;
		[XafDisplayName("Đến ngày"), ToolTip("")]
		public DateTime NgayKetthuc {
			get => ngayKetthuc;
			set {
				SetPropertyValue(nameof(NgayKetthuc), ref ngayKetthuc, value);
				if (!IsLoading && !IsSaving) {
					var nam = NgayKetthuc.Year;
					var thang = NgayKetthuc.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					this.KyBaoCao = ky;
				}
			}
		}

		KyBaoCao kyBaoCao;
		[XafDisplayName("Báo cáo tháng")]
		[VisibleInListView(false)]
		[Association("KyBaoCao-HoatDongSanPhamTruyenThongs")]
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
		[ModelDefault("AllowEdit", "False")]
		[VisibleInListView(false)]
		[Association("BaoCaoQuy-HoatDongSanPhamTruyenThongs")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		[XafDisplayName("Báo cáo 6 tháng"), ToolTip("")]
		[ModelDefault("AllowEdit", "False")]
		[VisibleInListView(false)]
		[Association("BaoCaoSauThang-HoatDongSanPhamTruyenThongs")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoNam baoCaoNam;
		[XafDisplayName("Báo cáo năm"), ToolTip("")]
		[ModelDefault("AllowEdit", "False")]
		[VisibleInListView(false)]
		[Association("BaoCaoNam-HoatDongSanPhamTruyenThongs")]
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

}