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
	[XafDisplayName("Cơ quan quản lý chuyên nghành")]
	[DefaultProperty(nameof(TenCoQuan))]
	[ImageName("BO_Contact")]
	//[Appearance("HideDefaultId", TargetItems = "Id", Criteria = "not IsCurrentUserInRole('Administrators')", Visibility = ViewItemVisibility.Hide, Context = "Any")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(Common.CategoryMenuItem)]

	public class DanhMucCoQuanQuanLy : BaseObject
    {
        public DanhMucCoQuanQuanLy(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

		string tenCoQuan;
		[XafDisplayName("Tên cơ quan")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenCoQuan
		{
			get => tenCoQuan;
			set => SetPropertyValue(nameof(TenCoQuan), ref tenCoQuan, value);
		}

		string ghiChu;
		[XafDisplayName("Ghi chú")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string GhiChu
		{
			get => ghiChu;
			set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
		}

		CapQL capQL;
		[XafDisplayName("Cấp quản lý")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public CapQL CapQL
		{
			get => capQL;
			set => SetPropertyValue(nameof(CapQL), ref capQL, value);
		}

		[Association("DanhMucCoQuanQuanLy-DanhMucCoSoSanXuatKinhDoanhs")]
		[XafDisplayName("Cơ sở SQKD quản lý")]
		[VisibleInDetailView(true)]
		public XPCollection<DanhMucCoSoSanXuatKinhDoanh> DanhMucCoSoSanXuatKinhDoanhs
		{
			get
			{
				return GetCollection<DanhMucCoSoSanXuatKinhDoanh>(nameof(DanhMucCoSoSanXuatKinhDoanhs));
			}
		}


        [XafDisplayName("Danh sách tài khoản"), ToolTip("")]
        [Association("DanhMucCoQuanQuanLy-ApplicationUsers")]
        [VisibleInDetailView(false)]
        public XPCollection<ApplicationUser> Users => GetCollection<ApplicationUser>(nameof(Users));

    }

	public enum CapQL
	{
		[XafDisplayName("Tỉnh")] Tinh = 0,
		[XafDisplayName("Huyện")] Huyen = 1,
	}
}