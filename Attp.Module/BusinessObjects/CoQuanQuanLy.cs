using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	[DefaultClassOptions]
	[XafDisplayName("Cơ quan quản lý chuyên nghành")]
	[DefaultProperty(nameof(TenCoQuan))]
	[ImageName("BO_Contact")]	
	//[Appearance("HideDefaultId", TargetItems = "Id", Criteria = "not IsCurrentUserInRole('Administrators')", Visibility = ViewItemVisibility.Hide, Context = "Any")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(R.V1)]
	public class CoQuanQuanLy : BaseObject {
		public CoQuanQuanLy(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		string tenCoQuan;
		[XafDisplayName("Tên cơ quan")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenCoQuan {
			get => tenCoQuan;
			set => SetPropertyValue(nameof(TenCoQuan), ref tenCoQuan, value);
		}

		string ghiChu;
		[XafDisplayName("Ghi chú")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string GhiChu {
			get => ghiChu;
			set => SetPropertyValue(nameof(GhiChu), ref ghiChu, value);
		}

		Capquanly capQuanLy;
		[XafDisplayName("Cấp quản lý")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public Capquanly CapQuanLy {
			get => capQuanLy;
			set => SetPropertyValue(nameof(CapQuanLy), ref capQuanLy, value);
		}

		[Association("CoQuanQuanLy-CoSoSanXuatKinhDoanhs")]
		[XafDisplayName("Cơ sở SQKD quản lý")]
		[VisibleInDetailView(true)]
		public XPCollection<CoSoSanXuatKinhDoanh> CoSoSanXuatKinhDoanhs {
			get
			{
				return GetCollection<CoSoSanXuatKinhDoanh>(nameof(CoSoSanXuatKinhDoanhs));
			}
		}		

		[XafDisplayName("Danh sách tài khoản"), ToolTip("")]
		[Association("CoQuanQuanLy-ApplicationUsers")]
		[VisibleInDetailView(false)]
		public XPCollection<ApplicationUser> Users => GetCollection<ApplicationUser>(nameof(Users));
	}

	public enum Capquanly {
		[XafDisplayName("Tỉnh")] Tinh = 0,
		[XafDisplayName("Huyện")] Huyen = 1,
	}

}