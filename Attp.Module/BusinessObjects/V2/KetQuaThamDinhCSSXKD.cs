
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace Attp.Module.BusinessObjects.V2
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Kết quả thẩm định")]
    [DefaultProperty(nameof(DanhMucCoSoSanXuatKinhDoanh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Common.DataMenuItem)]
    public class KetQuaThamDinhCSSXKD : BaseObject
    { 
        public KetQuaThamDinhCSSXKD(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
			LThamDinh = LThamDinh.KiemTraDanhGiaXepLoai;
		}
        
        private string title;
        [XafDisplayName("Tiêu đề")]		
        public string Title
        {
            get { return title; }
            set { SetPropertyValue(nameof(Title), ref title, value); }
        }


        LThamDinh lThamDinh;
		[XafDisplayName("Hình thức kiểm tra")]
		//[ModelDefault("AllowEdit", "False")]
		public LThamDinh LThamDinh
		{
			get => lThamDinh;
			set => SetPropertyValue(nameof(LThamDinh), ref lThamDinh, value);
		}


		DateTime ngayThamDinh;
		[XafDisplayName("Ngày thẩm định")]
		public DateTime NgayThamDinh
		{
			get => ngayThamDinh;
			set
			{
				SetPropertyValue(nameof(NgayThamDinh), ref ngayThamDinh, value);
			}
		}

		DanhMucCoSoSanXuatKinhDoanh danhMucCoSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("DanhMucCoSoSanXuatKinhDoanh-KetQuaThamDinhCSSXKDs")]
		[ModelDefault("ImmediatePostData", "True")]
		public DanhMucCoSoSanXuatKinhDoanh DanhMucCoSoSanXuatKinhDoanh
		{
			get => danhMucCoSoSanXuatKinhDoanh;
			set => SetPropertyValue(nameof(DanhMucCoSoSanXuatKinhDoanh), ref danhMucCoSoSanXuatKinhDoanh, value);
		}

		[PersistentAlias("[DanhMucCoSoSanXuatKinhDoanh.DanhMucCoQuanQuanLy]")]
		[XafDisplayName("Cơ quan quản lý")]
		public DanhMucCoQuanQuanLy DanhMucCoQuanQuanLy
		{
			get
			{
				var tmp = EvaluateAlias(nameof(DanhMucCoQuanQuanLy));
				if (tmp != null)
				{
					return tmp as DanhMucCoQuanQuanLy;
				}
                else
                {
					return null;
				}
				
			}
		}
		XLoai xLoai;
        [XafDisplayName("Đánh giá xếp loại")]		
		public XLoai XLoai
		{
            get { return xLoai; }
            set { SetPropertyValue(nameof(XLoai), ref xLoai, value); }
        }
		GiayChungNhanATTP giayChungNhanATTP;
        [XafDisplayName("Giấy chứng nhận"),ToolTip("")]
		public GiayChungNhanATTP GiayChungNhanATTP
        {
            get { return giayChungNhanATTP; }
            set { SetPropertyValue(nameof(GiayChungNhanATTP), ref giayChungNhanATTP, value); }
        }
		bool biThuHoi;
        [XafDisplayName("Giấy chứng nhận bị thu hồi")]
        [ModelDefault("AllowEdit","False")]
		public bool BiThuHoi
        {
            get
            {
				if(GiayChungNhanATTP != null)
                {
					return GiayChungNhanATTP.BiThuHoi;
                }
				return biThuHoi;
               
            }
			set { SetPropertyValue(nameof(BiThuHoi), ref biThuHoi, value); }
		}
			

		string ghichu;
		[XafDisplayName("Ghi chú")]
		[Size(SizeAttribute.Unlimited)]
		public string Ghichu
		{
			get => ghichu;
			set => SetPropertyValue(nameof(Ghichu), ref ghichu, value);
		}


		#region Association
		[Association("KetQuaThamDinhCSSXKD-FileDLs")]
		[XafDisplayName("File kết quả thẩm định")]
		public XPCollection<FileDL> FileDLs
		{
			get
			{
				return GetCollection<FileDL>(nameof(FileDLs));
			}
		}


		[Association("KetQuaThamDinhCSSXKD-SuPhatHanhChinhs")]
		[XafDisplayName("Kết quả xử lý vi phạm hành chính")]
		public XPCollection<SuPhatHanhChinh> SuPhatHanhChinhs
		{
			get
			{
				return GetCollection<SuPhatHanhChinh>(nameof(SuPhatHanhChinhs));
			}
		}
        #endregion

    }
	public enum XLoai
	{
		[XafDisplayName("Chưa xếp loại")] chuaxeploai = 0,
		[XafDisplayName("A")] A = 1,
		[XafDisplayName("B")] B = 2,
		[XafDisplayName("C")] C = 3,
	}
}