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

	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Kế hoạch thanh kiểm tra")]
	[DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(R.V1)]
	//[NavigationItem(R.DataMenuItem)]

	public class KeHoachThanhKiemTra : BaseObject {
		public KeHoachThanhKiemTra(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
			LoaiThamDinh = LoaiThamDinh.KiemTraDinhKy;
		}

		LoaiThamDinh loaiThamDinh;
		[XafDisplayName("Hình thức kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public LoaiThamDinh LoaiThamDinh {
			get => loaiThamDinh;
			set => SetPropertyValue(nameof(LoaiThamDinh), ref loaiThamDinh, value);
		}

		bool taiKiemTra;
		[XafDisplayName("Tái kiểm tra"), ToolTip("")]
		public bool TaiKiemTra {
			get => taiKiemTra;
			set => SetPropertyValue(nameof(TaiKiemTra), ref taiKiemTra, value);
		}

		DateTime ngayThamDinh;
		[XafDisplayName("Ngày kiểm tra dự kiến")]
		public DateTime NgayThamDinh {
			get => ngayThamDinh;
			set {
				SetPropertyValue(nameof(NgayThamDinh), ref ngayThamDinh, value);
				if (!IsLoading && !IsSaving) {
					var nam = value.Year;
					var thang = value.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					KyBaoCao = ky;
				}
			}
		}

		CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("CoSoSanXuatKinhDoanh-KeHoachThanhKiemTras")]
		[ModelDefault("ImmediatePostData", "True")]
		public CoSoSanXuatKinhDoanh CoSoSanXuatKinhDoanh {
			get => coSoSanXuatKinhDoanh;
			set => SetPropertyValue(nameof(CoSoSanXuatKinhDoanh), ref coSoSanXuatKinhDoanh, value);
		}

		[PersistentAlias("[CoSoSanXuatKinhDoanh.CoQuanQuanLy]")]
		[XafDisplayName("Cơ quan quản lý")]
		public CoQuanQuanLy CoQuanQuanLy {
			get {
				var tmp = EvaluateAlias(nameof(CoQuanQuanLy));
				if (tmp != null) {
					return tmp as CoQuanQuanLy;
				}
				else
					return null;
			}
		}

		string ghichu;
		[XafDisplayName("Ghi chú")]
		public string Ghichu {
			get => ghichu;
			set => SetPropertyValue(nameof(Ghichu), ref ghichu, value);
		}

		bool daThucHien;
		[XafDisplayName("Đã thực hiện")]
		[ModelDefault("AllowEdit", "False")]
		public bool DaThucHien {
			get => daThucHien;
			set => SetPropertyValue(nameof(DaThucHien), ref daThucHien, value);
		}

		string noiDung;
		[XafDisplayName("Nội dung")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string NoiDung {
			get => noiDung;
			set => SetPropertyValue(nameof(NoiDung), ref noiDung, value);
		}

		KyBaoCao kyBaoCao;
		[XafDisplayName("Báo cáo tháng")]
		[Association("KyBaoCao-KeHoachThanhKiemTras")]
		[VisibleInListView(false)]
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
		[Association("BaoCaoQuy-KeHoachThanhKiemTras")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		[XafDisplayName("Báo cáo 6 tháng"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoSauThang-KeHoachThanhKiemTras")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoNam baoCaoNam;
		[XafDisplayName("Báo cáo năm"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoNam-KeHoachThanhKiemTras")]
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

		#region Actions
		[Action(Caption = "Đánh dấu hoàn thành", ConfirmationMessage = "Đánh dấu hoàn thành lịch kiểm tra này?", AutoCommit = true, ImageName = "CheckBox")]
		public void DanhdauHoanthanh() {
			DaThucHien = true;
			OnChanged(nameof(DaThucHien));
		}
		#endregion
	}

	public enum LoaiThamDinh {
		[XafDisplayName("Kiểm tra đánh giá phân loại")] KiemTraDanhGiaXepLoai = 0,
		[XafDisplayName("Kiểm tra định kỳ")] KiemTraDinhKy = 1,
		//[XafDisplayName("Tái kiểm tra")] TaiKiemTra = 2,
	}
}