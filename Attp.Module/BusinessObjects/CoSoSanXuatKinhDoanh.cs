using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.XtraPrinting.Native;
using System;
using System.ComponentModel;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	[DefaultClassOptions]
	[XafDisplayName("Cơ sở sản xất kinh doanh")]
	[DefaultProperty(nameof(TenCoSo))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(R.V1)]
	//[ListViewFilter("Tất cả cơ sở", "", Index = 0)]
	//[ListViewFilter("Cơ sở có giấy chứng nhận", "[CoGiayChungNhanKhong]=true", Index = 1)]
	//[ListViewFilter("Cơ sở có giấy chứng nhận dưới 6 tháng", "DateDiffMonth(Today(),[ThoihanGiaychungnhanHientai])<6", Index = 2)]
	//[ListViewFilter("Cơ sở chưa có giấy chứng nhận", "[CoGiayChungNhanKhong]=false", Index = 3)]
	[Appearance("KhongGCN", AppearanceItemType = "ViewItem", TargetItems = "CoGiayChungNhanKhong,TenCoSo",
	Criteria = "CoGiayChungNhanKhong=False", Context = "ListView", FontColor = "Red", Priority = 3)]
	[Appearance("CoGCN", AppearanceItemType = "ViewItem", TargetItems = "CoGiayChungNhanKhong, TenCoSo",
	Criteria = "CoGiayChungNhanKhong=True", Context = "ListView", FontColor = "Green", Priority = 1)]
	[Appearance("SaphethanGCN", AppearanceItemType = "ViewItem", TargetItems = "CoGiayChungNhanKhong, TenCoSo",
	Criteria = "DateDiffMonth(Today(),[ThoihanGiaychungnhanHientai])<6", Context = "ListView", FontColor = "Orange", Priority = 2)]
	public class CoSoSanXuatKinhDoanh : BaseObject {
		public CoSoSanXuatKinhDoanh(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		DuLieuKiemTraCSSXKD kiemtra;
		protected override void OnLoaded() {
			base.OnLoaded();

			kiemtra = DuLieuKiemTraCSSXKDs.OrderByDescending(i => i.NgayKiemTra).FirstOrDefault();
		}

		string maSo;
		[XafDisplayName("Mã số")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string MaSo {
			get => maSo;
			set => SetPropertyValue(nameof(MaSo), ref maSo, value);
		}

		string tenCoSo;
		[XafDisplayName("Tên cơ sở")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[VisibleInLookupListView(true)]
		public string TenCoSo {
			get => tenCoSo;
			set => SetPropertyValue(nameof(TenCoSo), ref tenCoSo, value);
		}

		string diaChi;
		[XafDisplayName("Địa Chỉ")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[VisibleInLookupListView(true)]
		public string DiaChi {
			get => diaChi;
			set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
		}

		string dienThoai;
		[XafDisplayName("Điện thoại")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string DienThoai {
			get => dienThoai;
			set => SetPropertyValue(nameof(DienThoai), ref dienThoai, value);
		}

		string fax;
		[XafDisplayName("Fax")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Fax {
			get => fax;
			set => SetPropertyValue(nameof(Fax), ref fax, value);
		}

		string email;
		[XafDisplayName("Email")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Email {
			get => email;
			set => SetPropertyValue(nameof(Email), ref email, value);
		}

		string heThongQuanLyChatLuong;
		[XafDisplayName("Hệ thống quản lý chất lượng")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string HeThongQuanLyChatLuong {
			get => heThongQuanLyChatLuong;
			set => SetPropertyValue(nameof(HeThongQuanLyChatLuong), ref heThongQuanLyChatLuong, value);
		}

		CoQuanQuanLy coQuanQuanLy;
		[XafDisplayName("Cơ quan quản lý")]
		[VisibleInLookupListView(true)]
		[Association("CoQuanQuanLy-CoSoSanXuatKinhDoanhs")]
		public CoQuanQuanLy CoQuanQuanLy {
			get => coQuanQuanLy;
			set => SetPropertyValue(nameof(CoQuanQuanLy), ref coQuanQuanLy, value);
		}

		LoaiHinhCoSo loaiHinhCoSo;
		[XafDisplayName("Loại hình SXKD")]
		[Association("LoaiHinhCoSo-CoSoSanXuatKinhDoanhs")]
		public LoaiHinhCoSo LoaiHinhCoSo {
			get => loaiHinhCoSo;
			set => SetPropertyValue(nameof(LoaiHinhCoSo), ref loaiHinhCoSo, value);
		}

		string tenSanPham;
		[XafDisplayName("Tên sản phẩm")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenSanPham {
			get => tenSanPham;
			set => SetPropertyValue(nameof(TenSanPham), ref tenSanPham, value);
		}

		[PersistentAlias("")]
		[XafDisplayName("Ngày kiểm tra gần nhất")]
		public DateTime? NgayKiemTraGanNhat {
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (kiemtra != null) return kiemtra.NgayKiemTra;
				}
				return null;
			}

		}

		[PersistentAlias("")]
		[XafDisplayName("Kết quả kiểm tra")]
		public XepLoai KetQuaKiemTra {
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (kiemtra != null) return kiemtra.KetQua;
				}
				return XepLoai.chuaxeploai;
			}
		}

		[PersistentAlias("")]
		[XafDisplayName("Lần kiểm tra tiếp theo")]
		public DateTime? LanKiemTraTiepTheo {
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (kiemtra != null)
					{
						int months = 0;
						switch (KetQuaKiemTra)
						{
							case XepLoai.A: months = 18; break;
							case XepLoai.B: months = 12; break;
							case XepLoai.C: months = 3; break;
							case XepLoai.chuaxeploai:
								break;
						}
						return NgayKiemTraGanNhat.Value.AddMonths(months);
					}
					return null;
				}
				return null;
			}
		}

		[PersistentAlias("")]
		[XafDisplayName("GCN có hiệu lực")]
		[CaptionsForBoolValues("Có", "Không")]
		public bool CoGiayChungNhanKhong {
			get
			{
				if (!IsLoading && !IsSaving)
				{
					var last = this.GiayChungNhans.OrderByDescending(i => i.CoHieuLucDen).FirstOrDefault();
					if (last == null) return false;
					if (last.BiThuHoi) return false;
					if (last.CoHieuLucDen > DateTime.Today)
						return true;
				}
				return false;
			}
		}

		[PersistentAlias("")]
		[XafDisplayName("Thời hạn GCN hiện tại")]
		public DateTime? ThoihanGiaychungnhanHientai {
			get
			{
				//EvaluateAlias(nameof(ThoihanGiaychungnhanHientai));
				if (!IsLoading && !IsSaving)
				{
					var last = this.GiayChungNhans.OrderByDescending(i => i.CoHieuLucDen).FirstOrDefault();
					if (last != null)
						if (!last.BiThuHoi)
							return last.CoHieuLucDen;
				}
				return null;
			}
		}

		string ghiChu;
		[XafDisplayName("Ghi chú")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string GhiChu {
			get => ghiChu;
			set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
		}

		#region Associations
		[Association("CoSoSanXuatKinhDoanh-KeHoachThamDinhs")]
		[XafDisplayName("Kế hoạch thẩm định")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThamDinh> KeHoachThamDinhs {
			get
			{
				return GetCollection<KeHoachThamDinh>(nameof(KeHoachThamDinhs));
			}
		}

		[Association("CoSoSanXuatKinhDoanh-KeHoachThanhKiemTras")]
		[XafDisplayName("Kế hoạch kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<KeHoachThanhKiemTra> KeHoachThanhKiemTras {
			get
			{
				return GetCollection<KeHoachThanhKiemTra>(nameof(KeHoachThanhKiemTras));
			}
		}

		[Association("CoSoSanXuatKinhDoanh-DuLieuKiemTraCSSXKDs")]
		[XafDisplayName("Kết quả kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<DuLieuKiemTraCSSXKD> DuLieuKiemTraCSSXKDs {
			get
			{
				return GetCollection<DuLieuKiemTraCSSXKD>(nameof(DuLieuKiemTraCSSXKDs));
			}
		}

		[Association("CoSoSanXuatKinhDoanh-GiayChungNhans")]
		[XafDisplayName("GCN đã được cấp")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<GiayChungNhan> GiayChungNhans {
			get
			{
				return GetCollection<GiayChungNhan>(nameof(GiayChungNhans));
			}
		}

		#endregion

		[Action(Caption = "Lập kế hoạch kiểm tra", ConfirmationMessage = "Lập kế hoạch kiểm tra mới cho cơ sở này?", AutoCommit = true)]
		public void TaoKehoachThamdinh(Parameter parameter) {
			var kehoach = new KeHoachThanhKiemTra(Session) {
				CoSoSanXuatKinhDoanh = this,
				NgayThamDinh = parameter.NgayThang,
				LoaiThamDinh = parameter.LoaiThamDinh,
			};
			KeHoachThanhKiemTras.Add(kehoach);
			OnChanged(nameof(KeHoachThanhKiemTras));
		}

		#region Audit
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
		#endregion
	}

	[DomainComponent]
	public class Parameter {
		public DateTime NgayThang { get; set; }
		public LoaiThamDinh LoaiThamDinh { get; set; }
	}
}