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
    [XafDisplayName("Loại hình SXKD")]
    [DefaultProperty(nameof(TenLoaiHinh))]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
    [ListViewFindPanel(true)]
    [LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
    [NavigationItem(Common.CategoryMenuItem)]
    public class LoaiHinhCoSoSanXuat : BaseObject
    { 
        public LoaiHinhCoSoSanXuat(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        string tenLoaiHinh;
        [XafDisplayName("Tên loại hình")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string TenLoaiHinh
        {
            get => tenLoaiHinh;
            set => SetPropertyValue(nameof(TenLoaiHinh), ref tenLoaiHinh, value);
        }

        string kyHieuMaHoa;
        [XafDisplayName("Ký hiệu mã hóa")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string KyHieuMaHoa
        {
            get => kyHieuMaHoa;
            set => SetPropertyValue(nameof(KyHieuMaHoa), ref kyHieuMaHoa, value);
        }

        #region Associations
        [Association("LoaiHinhCoSoSanXuat-DanhMucCoSoSanXuatKinhDoanhs")]
        [XafDisplayName("Danh sách cơ sở sản xuất kinh doanh")]
        public XPCollection<DanhMucCoSoSanXuatKinhDoanh> DanhMucCoSoSanXuatKinhDoanhs
        {
            get
            {
                return GetCollection<DanhMucCoSoSanXuatKinhDoanh>(nameof(DanhMucCoSoSanXuatKinhDoanhs));
            }
        }
        #endregion
    }
}