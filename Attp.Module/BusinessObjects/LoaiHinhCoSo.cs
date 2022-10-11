using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Linq;

namespace Attp.Module.BusinessObjects {
	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Loại hình SXKD")]
	[DefaultProperty(nameof(TenLoaiHinh))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	[NavigationItem(R.V1)]
	public class LoaiHinhCoSo : BaseObject {
		public LoaiHinhCoSo(Session session)
			: base(session) {
		}
		public override void AfterConstruction() {
			base.AfterConstruction();
		}

		string tenLoaiHinh;
		[XafDisplayName("Tên loại hình")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string TenLoaiHinh {
			get => tenLoaiHinh;
			set => SetPropertyValue(nameof(TenLoaiHinh), ref tenLoaiHinh, value);
		}

		string kyHieuMaHoa;
		[XafDisplayName("Ký hiệu mã hóa")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string KyHieuMaHoa {
			get => kyHieuMaHoa;
			set => SetPropertyValue(nameof(KyHieuMaHoa), ref kyHieuMaHoa, value);
		}

		#region Associations
		[Association("LoaiHinhCoSo-CoSoSanXuatKinhDoanhs")]
		[XafDisplayName("Danh sách cơ sở sản xuất kinh doanh")]
		public XPCollection<CoSoSanXuatKinhDoanh> CoSoSanXuatKinhDoanhs {
			get
			{
				return GetCollection<CoSoSanXuatKinhDoanh>(nameof(CoSoSanXuatKinhDoanhs));
			}
		}
		#endregion
	}
}