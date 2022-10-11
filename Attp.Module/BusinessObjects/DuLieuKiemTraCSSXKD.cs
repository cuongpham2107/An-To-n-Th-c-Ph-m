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
	[ImageName("BO_Contact")]
	[XafDisplayName("Kết quả kiểm tra")]
	[DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class DuLieuKiemTraCSSXKD : BaseObject {
		public DuLieuKiemTraCSSXKD(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("CoSoSanXuatKinhDoanh-DuLieuKiemTraCSSXKDs")]
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

		DateTime ngayKiemTra;
		[XafDisplayName("Ngày kiểm tra")]
		[ModelDefault("ImmediatePostData", "True")]
		public DateTime NgayKiemTra {
			get => ngayKiemTra;
			set {
				SetPropertyValue(nameof(NgayKiemTra), ref ngayKiemTra, value);

				if (!IsLoading && !IsSaving) {
					var nam = NgayKiemTra.Year;
					var thang = NgayKiemTra.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					this.KyBaoCao = ky;

					//if (CoSoSanXuatKinhDoanh != null)
					//	CoSoSanXuatKinhDoanh.NgayKiemTraGanNhat = NgayKiemTra;
				}
			}
		}

		LoaiThamDinh hinhThucKiemTra;
		[XafDisplayName("Hình thức kiểm tra")]
		public LoaiThamDinh HinhThucKiemTra {
			get => hinhThucKiemTra;
			set => SetPropertyValue(nameof(HinhThucKiemTra), ref hinhThucKiemTra, value);
		}

		XepLoai ketQua;
		[XafDisplayName("Kết quả")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public XepLoai KetQua {
			get => ketQua;
			set {
				SetPropertyValue(nameof(KetQua), ref ketQua, value);
			}
		}

		string xuLyViPham;
		[XafDisplayName("Xử lý vi phạm")]
		public string XuLyViPham {
			get => xuLyViPham;
			set => SetPropertyValue(nameof(XuLyViPham), ref xuLyViPham, value);
		}

		string hanhViViPham;
		[XafDisplayName("Hành vi vi phạm")]
		public string HanhViViPham {
			get => hanhViViPham;
			set => SetPropertyValue(nameof(HanhViViPham), ref hanhViViPham, value);
		}

		int soTienPhat;
		[XafDisplayName("Số tiền phạt")]
		public int SoTienPhat {
			get => soTienPhat;
			set => SetPropertyValue(nameof(SoTienPhat), ref soTienPhat, value);
		}

		int tongSoMauLay;
		[XafDisplayName("Tổng số mẫu lấy")]
		public int TongSoMauLay {
			get => tongSoMauLay;
			set => SetPropertyValue(nameof(TongSoMauLay), ref tongSoMauLay, value);
		}

		int soMauViPham;
		[XafDisplayName("Số mẫu vi phạm")]
		public int SoMauViPham {
			get => soMauViPham;
			set => SetPropertyValue(nameof(SoMauViPham), ref soMauViPham, value);
		}

		int chiTieuViPham;
		[XafDisplayName("Chỉ tiêu vi phạm")]
		public int ChiTieuViPham {
			get => chiTieuViPham;
			set => SetPropertyValue(nameof(ChiTieuViPham), ref chiTieuViPham, value);
		}

		string ghiChu;
		[XafDisplayName("Ghi chú")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string GhiChu {
			get => ghiChu;
			set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
		}

		KyBaoCao kyBaoCao;
		[XafDisplayName("Báo cáo tháng")]
		[VisibleInListView(false)]
		[Association("KyBaoCao-DuLieuKiemTraCSSXKDs")]
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
		[Association("BaoCaoQuy-DuLieuKiemTraCSSXKDs")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		[XafDisplayName("Báo cáo 6 tháng"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoSauThang-DuLieuKiemTraCSSXKDs")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoNam baoCaoNam;
		[XafDisplayName("Báo cáo năm"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoNam-DuLieuKiemTraCSSXKDs")]
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

	public enum XepLoai {
		[XafDisplayName("Chưa xếp loại")] chuaxeploai = 0,
		[XafDisplayName("A")] A = 1,
		[XafDisplayName("B")] B = 2,
		[XafDisplayName("C")] C = 3,
	}

}