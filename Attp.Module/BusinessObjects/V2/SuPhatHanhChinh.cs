using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Attp.Module.BusinessObjects.V2
{
    [DefaultClassOptions]
    [ImageName("BO_Contact")]
    [XafDisplayName("Sử phạt hành chính")]
    [DefaultProperty(nameof(DanhMucCoSoSanXuatKinhDoanh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    //[NavigationItem(R.V1)]
    [NavigationItem(Common.DataMenuItem)]

    public class SuPhatHanhChinh : BaseObject
    {
        public SuPhatHanhChinh(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }
        DanhMucCoSoSanXuatKinhDoanh danhMucCoSoSanXuatKinhDoanh;
        [XafDisplayName("Cơ sở sản xuất kinh doanh")]
        [Association("DanhMucCoSoSanXuatKinhDoanh-SuPhatHanhChinhs")]
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
                    return null;
            }
        }

        string xuLyViPham;
        [XafDisplayName("Xử lý vi phạm")]
        public string XuLyViPham
        {
            get => xuLyViPham;
            set => SetPropertyValue(nameof(XuLyViPham), ref xuLyViPham, value);
        }

        string hanhViViPham;
        [XafDisplayName("Hành vi vi phạm")]
        public string HanhViViPham
        {
            get => hanhViViPham;
            set => SetPropertyValue(nameof(HanhViViPham), ref hanhViViPham, value);
        }
        int soTienPhat;
        [XafDisplayName("Số tiền phạt")]
        public int SoTienPhat
        {
            get => soTienPhat;
            set => SetPropertyValue(nameof(SoTienPhat), ref soTienPhat, value);
        }

        int tongSoMauLay;
        [XafDisplayName("Tổng số mẫu lấy")]
        public int TongSoMauLay
        {
            get => tongSoMauLay;
            set => SetPropertyValue(nameof(TongSoMauLay), ref tongSoMauLay, value);
        }
        int soMauViPham;
        [XafDisplayName("Số mẫu vi phạm")]
        public int SoMauViPham
        {
            get => soMauViPham;
            set => SetPropertyValue(nameof(SoMauViPham), ref soMauViPham, value);
        }
        int chiTieuViPham;
        [XafDisplayName("Chỉ tiêu vi phạm")]
        public int ChiTieuViPham
        {
            get => chiTieuViPham;
            set => SetPropertyValue(nameof(ChiTieuViPham), ref chiTieuViPham, value);
        }

        private string lyDo;
        [XafDisplayName("Lý do xử phạt"), ToolTip("")]
        [Size(SizeAttribute.Unlimited)]
        public string LyDo
        {
            get { return lyDo; }
            set { SetPropertyValue(nameof(LyDo), ref lyDo, value); }
        }

        KetQuaThamDinhCSSXKD ketQuaThamDinhCSSXKD;
        [Association("KetQuaThamDinhCSSXKD-SuPhatHanhChinhs")]
        [XafDisplayName("Kết quả thẩm định")]
        public KetQuaThamDinhCSSXKD KetQuaThamDinhCSSXKD
        {
            get { return ketQuaThamDinhCSSXKD; }
            set { SetPropertyValue(nameof(KetQuaThamDinhCSSXKD), ref ketQuaThamDinhCSSXKD, value); }
        }
        KetQuaThanhKiemTraCSSX ketQuaThanhKiemTraCSSX;
        [Association("KetQuaThanhKiemTraCSSX-SuPhatHanhChinhs")]
        [XafDisplayName("Kết quả thanh kiểm tra")]
        public KetQuaThanhKiemTraCSSX KetQuaThanhKiemTraCSSX
        {
            get { return ketQuaThanhKiemTraCSSX; }
            set { SetPropertyValue(nameof(KetQuaThanhKiemTraCSSX), ref ketQuaThanhKiemTraCSSX, value); }
        }




        private DateTime ngaySuPhat;
        [XafDisplayName("Ngày sử phạt")]
        public DateTime NgaySuPhat
        {
            get { return ngaySuPhat; }
            set { SetPropertyValue(nameof(NgaySuPhat), ref ngaySuPhat, value); }
        }

        #region
        [Association("SuPhatHanhChinh-FileDLs")]
        [XafDisplayName("File sử phạt hành chính")]
        public XPCollection<FileDL> FileDLs
        {
            get
            {
                return GetCollection<FileDL>(nameof(FileDLs));
            }
        }
        #endregion

    }
}