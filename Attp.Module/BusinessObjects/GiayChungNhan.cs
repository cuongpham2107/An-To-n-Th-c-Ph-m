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
	//[Appearance("HideDefaultId", TargetItems = "GiayChungNhanes", Visibility = ViewItemVisibility.Hide, Context = "Any")]
	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Giấy chứng nhận")]
	[DefaultProperty(nameof(CoSoSanXuatKinhDoanh))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class GiayChungNhan : BaseObject {
		public GiayChungNhan(Session session)
			: base(session) {
		}

		#region Overrides
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		protected override void OnSaving() {

			OnChanged(nameof(KyBaoCao));
		}
		#endregion

		string soCap;
		[XafDisplayName("Số cấp")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string SoCap {
			get => soCap;
			set => SetPropertyValue(nameof(SoCap), ref soCap, value);
		}

		CoSoSanXuatKinhDoanh coSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("CoSoSanXuatKinhDoanh-GiayChungNhans")]
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

		DateTime ngayCap;
		[XafDisplayName("Ngày cấp")]
		[ModelDefault("ImmediatePostData", "True")]
		public DateTime NgayCap {
			get => ngayCap;
			set {
				SetPropertyValue(nameof(NgayCap), ref ngayCap, value);

				if (!IsLoading && !IsSaving) {
					var nam = NgayCap.Year;
					var thang = NgayCap.Month;
					var ky = Session.Query<KyBaoCao>().Where(i => i.Nam == nam && i.Thang == thang).FirstOrDefault();
					this.KyBaoCao = ky;
				}
			}
		}

		DateTime coHieuLucDen;
		[XafDisplayName("Có hiệu lực đến")]
		public DateTime CoHieuLucDen {
			get => coHieuLucDen;
			set => SetPropertyValue(nameof(CoHieuLucDen), ref coHieuLucDen, value);
		}

		FileData fileData;
		[XafDisplayName("File scan")]
		[VisibleInListView(false)]
		public FileData FileData {
			get => fileData;
			set => SetPropertyValue(nameof(FileData), ref fileData, value);
		}

		KyBaoCao kyBaoCao;
		[XafDisplayName("Báo cáo tháng")]
		[Association("KyBaoCao-GiayChungNhans")]
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
		[Association("BaoCaoQuy-GiayChungNhans")]
		public BaoCaoQuy BaoCaoQuy {
			get => baoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref baoCaoQuy, value);
		}

		BaoCaoSauThang baoCaoSauThang;
		[XafDisplayName("Báo cáo 6 tháng"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoSauThang-GiayChungNhans")]
		public BaoCaoSauThang BaoCaoSauThang {
			get => baoCaoSauThang;
			set => SetPropertyValue(nameof(BaoCaoSauThang), ref baoCaoSauThang, value);
		}

		BaoCaoNam baoCaoNam;
		[XafDisplayName("Báo cáo năm"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("BaoCaoNam-GiayChungNhans")]
		public BaoCaoNam BaoCaoNam {
			get => baoCaoNam;
			set => SetPropertyValue(nameof(BaoCaoNam), ref baoCaoNam, value);
		}

		#region Thu_hồi
		const string _thuhoi = "Bị thu hồi";
		bool biThuHoi;
		[XafDisplayName("Bị thu hồi"), ToolTip("")]
		[DetailViewLayout(_thuhoi)]
		public bool BiThuHoi {
			get => biThuHoi;
			set => SetPropertyValue(nameof(BiThuHoi), ref biThuHoi, value);
		}

		DateTime ngayThuHoi;
		[XafDisplayName("Ngày thu hồi"), ToolTip("")]
		[DetailViewLayout(_thuhoi)]
		public DateTime NgayThuHoi {
			get => ngayThuHoi;
			set => SetPropertyValue(nameof(NgayThuHoi), ref ngayThuHoi, value);
		}

		string lyDoThuHoi;
		[XafDisplayName("Lý do thu hồi"), ToolTip("")]
		[DetailViewLayout(_thuhoi)]
		public string LyDoThuHoi {
			get => lyDoThuHoi;
			set => SetPropertyValue(nameof(LyDoThuHoi), ref lyDoThuHoi, value);
		}

		[Action(Caption = "Thu hồi", ConfirmationMessage = "Sau khi thu hồi bạn sẽ không thể thay đổi lại được nữa. Bạn thực sự muốn thu hồi giấy chứng nhận này?", AutoCommit = true, TargetObjectsCriteria = "[BiThuHoi]=False")]
		public void ThuHoiAction(ThuHoiParameter parameter) {
			BiThuHoi = true;
			NgayThuHoi = parameter.NgayThuHoi;
			LyDoThuHoi = parameter.LyDoThuHoi;
		}

		[Action(Caption = "Hủy thu hồi", AutoCommit = true, TargetObjectsCriteria = "[BiThuHoi]=True", ConfirmationMessage = "Hủy quyết định thu hồi?")]
		public void ThuHoiActionUndo() {
			BiThuHoi = false;
			NgayThuHoi = DateTime.Now;
			LyDoThuHoi = "Đã hủy thu hồi";
		}

		#endregion

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

	[DomainComponent]
	public class ThuHoiParameter {
		[XafDisplayName("Ngày thu hồi")]
		public DateTime NgayThuHoi { get; set; } = DateTime.Now;

		[XafDisplayName("Lý do thu hồi")]
		public string LyDoThuHoi { get; set; }

	}
}