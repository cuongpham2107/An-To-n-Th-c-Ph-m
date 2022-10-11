using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Attp.Module.BusinessObjects.V2
{
	[DefaultClassOptions]
	[XafDisplayName("Cơ sở sản xuất kinh doanh")]
	[DefaultProperty(nameof(TenCoSo))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(Common.CategoryMenuItem)]
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

	public class DanhMucCoSoSanXuatKinhDoanh : BaseObject
    { 
        public DanhMucCoSoSanXuatKinhDoanh(Session session)
            : base(session)
        {
        }
		KetQuaThamDinhCSSXKD thamdinh;
		KetQuaThanhKiemTraCSSX thanhkiemtra;
        public override void AfterConstruction()
        {
            base.AfterConstruction();
			
			
		}
        protected override void OnLoaded()
        {
            base.OnLoaded();
			thamdinh = KetQuaThamDinhCSSXKDs.OrderByDescending(t => t.NgayThamDinh).FirstOrDefault();
			thanhkiemtra = KetQuaThanhKiemTraCSSXs.OrderByDescending(k => k.NgayThanhKiemTra).FirstOrDefault();
		}

        string maSo;
		[XafDisplayName("Mã số")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string MaSo
		{
			get => maSo;
			set => SetPropertyValue(nameof(MaSo), ref maSo, value);
		}

		string tenCoSo;
		[XafDisplayName("Tên cơ sở")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[VisibleInLookupListView(true)]
		public string TenCoSo
		{
			get => tenCoSo;
			set => SetPropertyValue(nameof(TenCoSo), ref tenCoSo, value);
		}

		string diaChi;
		[XafDisplayName("Địa Chỉ")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[VisibleInLookupListView(true)]
		public string DiaChi
		{
			get => diaChi;
			set => SetPropertyValue(nameof(DiaChi), ref diaChi, value);
		}

		string dienThoai;
		[XafDisplayName("Điện thoại")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string DienThoai
		{
			get => dienThoai;
			set => SetPropertyValue(nameof(DienThoai), ref dienThoai, value);
		}

		string fax;
		[XafDisplayName("Fax")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Fax
		{
			get => fax;
			set => SetPropertyValue(nameof(Fax), ref fax, value);
		}

		string email;
		[XafDisplayName("Email")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string Email
		{
			get => email;
			set => SetPropertyValue(nameof(Email), ref email, value);
		}

		string heThongQuanLyChatLuong;
		[XafDisplayName("Hệ thống quản lý chất lượng")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string HeThongQuanLyChatLuong
		{
			get => heThongQuanLyChatLuong;
			set => SetPropertyValue(nameof(HeThongQuanLyChatLuong), ref heThongQuanLyChatLuong, value);
		}

		DanhMucCoQuanQuanLy danhMucCoQuanQuanLy;
		[XafDisplayName("Cơ quan quản lý")]
		[VisibleInLookupListView(true)]
		[Association("DanhMucCoQuanQuanLy-DanhMucCoSoSanXuatKinhDoanhs")]
		public DanhMucCoQuanQuanLy DanhMucCoQuanQuanLy
		{
			get => danhMucCoQuanQuanLy;
			set => SetPropertyValue(nameof(DanhMucCoQuanQuanLy), ref danhMucCoQuanQuanLy, value);
		}
		LoaiHinhCoSoSanXuat loaiHinhCoSo;
		[XafDisplayName("Loại hình SXKD")]
		[Association("LoaiHinhCoSoSanXuat-DanhMucCoSoSanXuatKinhDoanhs")]
		public LoaiHinhCoSoSanXuat LoaiHinhCoSoSanXuat
		{
			get => loaiHinhCoSo;
			set => SetPropertyValue(nameof(LoaiHinhCoSoSanXuat), ref loaiHinhCoSo, value);
		}

		string tenSanPham;
		[XafDisplayName("Tên sản phẩm")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenSanPham
		{
			get => tenSanPham;
			set => SetPropertyValue(nameof(TenSanPham), ref tenSanPham, value);
		}

        [PersistentAlias("")]
        [XafDisplayName("Ngày thẩm định gần nhất")]
        [ModelDefault("AllowEdit","False")]		
        public DateTime? NgayThamDinhGanNhat
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
                    if (thamdinh != null) return thamdinh.NgayThamDinh;
                }
                return null;
            }
        }

        [PersistentAlias("")]
        [XafDisplayName("Kết quả thẩm định")]
		[ModelDefault("AllowEdit", "False")]
		public XLoai KetQuaThamDinh
        {
            get
            {
                if (!IsLoading && !IsSaving)
                {
					if (thamdinh != null) return (XLoai)thamdinh.XLoai;
                }
                return XLoai.chuaxeploai;
            }
        }


		[PersistentAlias("")]
		[XafDisplayName("Ngày thanh kiểm tra gần nhất")]
		[ModelDefault("AllowEdit", "False")]
		public DateTime? NgayThanhKiemTra
		{
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (thanhkiemtra != null) return thanhkiemtra.NgayThanhKiemTra;
				}
				return null;
			}
		}

		[PersistentAlias("")]
		[XafDisplayName("Kết quả thanh kiểm tra")]
		[ModelDefault("AllowEdit", "False")]
		public XLoai KetQuaThanhKiemTra
		{
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (thanhkiemtra != null) return (XLoai)thanhkiemtra.XLoai;
				}
				return XLoai.chuaxeploai;
			}
		}


		[PersistentAlias("")]
		[XafDisplayName("GCN có hiệu lực")]
		[CaptionsForBoolValues("Có", "Không")]
		public bool CoGiayChungNhanKhong
		{
			get
			{
				if (!IsLoading && !IsSaving)
				{
					var last = this.GiayChungNhanATTPs.OrderByDescending(i => i.CoHieuLucDen).FirstOrDefault();
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
		public DateTime? ThoihanGiaychungnhanHientai
		{
			get
			{
				//EvaluateAlias(nameof(ThoihanGiaychungnhanHientai));
				if (!IsLoading && !IsSaving)
				{
					var last = this.GiayChungNhanATTPs.OrderByDescending(i => i.CoHieuLucDen).FirstOrDefault();
					if (last != null)
						if (!last.BiThuHoi)
							return last.CoHieuLucDen;
				}
				return null;
			}
		}

		string ghiChu;
		[XafDisplayName("Ghi chú")]
		//[Size(128)]
		[Size(SizeAttribute.Unlimited)]
		//[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string GhiChu
		{
			get => ghiChu;
			set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
		}


		#region Associations
		[Association("DanhMucCoSoSanXuatKinhDoanh-SuPhatHanhChinhs")]
		[XafDisplayName("Sử phạt hành chính")]
		[ModelDefault("AllowEdit", "False")]
		public XPCollection<SuPhatHanhChinh> SuPhatHanhChinhs
		{
			get
			{
				return GetCollection<SuPhatHanhChinh>(nameof(SuPhatHanhChinhs));
			}
		}


		[Association("DanhMucCoSoSanXuatKinhDoanh-KetQuaThamDinhCSSXKDs")]
        [XafDisplayName("Kết quả thẩm định")]
        [ModelDefault("AllowEdit", "False")]
        public XPCollection<KetQuaThamDinhCSSXKD> KetQuaThamDinhCSSXKDs
		{
            get
            {
                return GetCollection<KetQuaThamDinhCSSXKD>(nameof(KetQuaThamDinhCSSXKDs));
            }
        }

        [Association("DanhMucCoSoSanXuatKinhDoanh-KetQuaThanhKiemTraCSSXs")]
        [XafDisplayName("Kết quả thanh kiểm tra")]
        [ModelDefault("AllowEdit", "False")]
        public XPCollection<KetQuaThanhKiemTraCSSX> KetQuaThanhKiemTraCSSXs
		{
            get
            {
                return GetCollection<KetQuaThanhKiemTraCSSX>(nameof(KetQuaThanhKiemTraCSSXs));
            }
        }


        [Association("DanhMucCoSoSanXuatKinhDoanh-GiayChungNhanATTPs")]
        [XafDisplayName("GCN đã được cấp")]
        [ModelDefault("AllowEdit", "False")]
        public XPCollection<GiayChungNhanATTP> GiayChungNhanATTPs
		{
            get
            {
                return GetCollection<GiayChungNhanATTP>(nameof(GiayChungNhanATTPs));
            }
        }

        #endregion


    }
}