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
	[XafDisplayName("Báo cáo năm")]
	[DefaultProperty(nameof(TenBaoCao))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class BaoCaoNam : BaseObject {
		public BaoCaoNam(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
			Nam = DateTime.Today.Year;
		}

		//protected override void OnLoaded() {
		//	base.OnLoaded();

		//	if (!IsLoading) {
		//		var baocaothang = Session.Query<KyBaoCao>().Where(i => i.BaoCaoNam.Equals(this)).ToList();
		//		bool pheduyet = true;
		//		foreach (var thang in baocaothang) {
		//			pheduyet = pheduyet && thang.PheDuyet;
		//		}
		//		PheDuyet = pheduyet;
		//	}
		//}

		protected override void OnSaving() {
			var item = Session.Query<BaoCaoNam>().Count(i => i.Nam == Nam && i.Oid != Oid);
			if (item >= 1) {
				throw new UserFriendlyException($"Báo cáo của năn {Nam} đã có sẵn. Bạn không cần thêm mới nữa.");
			}
			else {
				base.OnSaving();
			}
		}

		int nam;
		[XafDisplayName("Năm"), ModelDefault("DisplayFormat", "{0:G}")]
		public int Nam {
			get => nam;
			set {
				SetPropertyValue(nameof(Nam), ref nam, value);
				if (!IsLoading)
					TenBaoCao = $"Báo cáo năm {Nam}";
			}
		}

		string tenBaoCao;
		[XafDisplayName("Tên báo cáo")]
		public string TenBaoCao {
			get => tenBaoCao;
			set => SetPropertyValue(nameof(TenBaoCao), ref tenBaoCao, value);
		}

		#region Associations
		[Association("BaoCaoNam-KeHoachThamDinhs")]
		[XafDisplayName("Kế hoạch thẩm định")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThamDinh> KeHoachThamDinhs {
			get => GetCollection<KeHoachThamDinh>(nameof(KeHoachThamDinhs));
		}

		[Association("BaoCaoNam-KeHoachThanhKiemTras")]
		[XafDisplayName("Kế hoạch thanh kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThanhKiemTra> KeHoachThanhKiemTras {
			get => GetCollection<KeHoachThanhKiemTra>(nameof(KeHoachThanhKiemTras));
		}

		[Association("BaoCaoNam-GiayChungNhans")]
		[XafDisplayName("Giấy chứng nhận đã cấp")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<GiayChungNhan> GiayChungNhans {
			get => GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
		}

		[Association("BaoCaoNam-DuLieuKiemTraCSSXKDs")]
		[ModelDefault("AllowEdit", "False")]
		[XafDisplayName("Kết quả kiểm tra cơ sở SXKD")]
		public XPCollection<DuLieuKiemTraCSSXKD> DuLieuKiemTraCSSXKDs => GetCollection<DuLieuKiemTraCSSXKD>(nameof(DuLieuKiemTraCSSXKDs));

		[Association("BaoCaoNam-VanBanChiDaos")]
		[ModelDefault("AllowEdit", "False")]
		[XafDisplayName("Văn bản chỉ đạo đã ban hành")]
		public XPCollection<VanBanChiDao> VanBanChiDaos {
			get => GetCollection<VanBanChiDao>(nameof(VanBanChiDaos));
		}

		[Association("BaoCaoNam-HoatDongSanPhamTruyenThongs")]
		[ModelDefault("AllowEdit", "False")]
		[XafDisplayName("Hoạt động sản phẩm truyền thông đã thực hiện")]
		public XPCollection<HoatDongSanPhamTruyenThong> HoatDongSanPhamTruyenThongs {
			get => GetCollection<HoatDongSanPhamTruyenThong>(nameof(HoatDongSanPhamTruyenThongs));
		}
		#endregion

		[Action(Caption = "Tạo danh mục báo cáo", ConfirmationMessage = "Tạo danh mục báo cáo tháng/quý/6 tháng của năm?", ImageName = "BO_Report")]
		public void TaoBaocao() {

			var bc6t1 = Session.Query<BaoCaoSauThang>().FirstOrDefault(i => i.Nam == Nam && i.DotBaoCao == 1) ?? new BaoCaoSauThang(Session) { Nam = Nam, DotBaoCao = 1 };
			var bc6t2 = Session.Query<BaoCaoSauThang>().FirstOrDefault(i => i.Nam == Nam && i.DotBaoCao == 2) ?? new BaoCaoSauThang(Session) { Nam = Nam, DotBaoCao = 2 };

			var bcq1 = Session.Query<BaoCaoQuy>().FirstOrDefault(i => i.Nam == Nam && i.Quy == 1) ?? new BaoCaoQuy(Session) { Nam = Nam, Quy = 1 };
			var bcq2 = Session.Query<BaoCaoQuy>().FirstOrDefault(i => i.Nam == Nam && i.Quy == 2) ?? new BaoCaoQuy(Session) { Nam = Nam, Quy = 2 };
			var bcq3 = Session.Query<BaoCaoQuy>().FirstOrDefault(i => i.Nam == Nam && i.Quy == 3) ?? new BaoCaoQuy(Session) { Nam = Nam, Quy = 3 };
			var bcq4 = Session.Query<BaoCaoQuy>().FirstOrDefault(i => i.Nam == Nam && i.Quy == 4) ?? new BaoCaoQuy(Session) { Nam = Nam, Quy = 4 };

			var bct1 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 1) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 1, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq1 };
			var bct2 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 2) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 2, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq1 };
			var bct3 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 3) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 3, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq1 };

			var bct4 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 4) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 4, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq2 };
			var bct5 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 5) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 5, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq2 };
			var bct6 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 6) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 6, BaoCaoNam = this, BaoCaoSauThang = bc6t1, BaoCaoQuy = bcq2 };

			var bct7 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 7) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 7, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq3 };
			var bct8 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 8) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 8, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq3 };
			var bct9 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 9) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 9, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq3 };

			var bct10 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 10) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 10, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq4 };
			var bct11 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 11) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 11, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq4 };
			var bct12 = Session.Query<KyBaoCao>().FirstOrDefault(i => i.Nam == Nam && i.Thang == 12) ?? new KyBaoCao(Session) { Nam = Nam, Thang = 12, BaoCaoNam = this, BaoCaoSauThang = bc6t2, BaoCaoQuy = bcq4 };

			Session.CommitTransaction();
		}

		//bool pheDuyet;
		//[XafDisplayName("Phê duyệt"), ToolTip(""), ModelDefault("AllowEdit", "False")]
		//public bool PheDuyet {
		//	get => pheDuyet;
		//	set => SetPropertyValue(nameof(PheDuyet), ref pheDuyet, value);
		//}

		//[Action(Caption = "Phê duyệt", ConfirmationMessage = "Phê duyệt báo cáo này? Sau khi phê duyệt, dữ liệu của tất cả các kỳ báo cáo tháng thuộc năm này sẽ không thể sửa được nữa.", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=False", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
		//public void PheDuyetAction() {
		//	PheDuyet = true;
		//	var baocaothang = Session.Query<KyBaoCao>().Where(i => i.BaoCaoNam.Equals(this)).ToList();
		//	foreach (var thang in baocaothang) {
		//		thang.PheDuyet = true;
		//	}
		//}

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