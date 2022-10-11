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
	[NavigationItem(Common.ReportMenuItem)]
	[XafDisplayName("Báo cáo quý")]
	[DefaultProperty(nameof(TenBaoCao))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class KyBaoCaoQuy : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public KyBaoCaoQuy(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
		string tenBaoCao;
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		[XafDisplayName("Tên báo cáo")]
		public string TenBaoCao
		{
			get => tenBaoCao;
			set => SetPropertyValue(nameof(TenBaoCao), ref tenBaoCao, value);
		}

		int quy;
		[XafDisplayName("Quý")]
		public int Quy
		{
			get => quy;
			set
			{
				SetPropertyValue(nameof(Quy), ref quy, value);
				if (!IsLoading)
					TenBaoCao = $"Báo cáo quý {Quy} năm {Nam}";
			}
		}

		int nam;
		[XafDisplayName("Năm"), ModelDefault("DisplayFormat", "{0:G}")]
		public int Nam
		{
			get => nam;
			set
			{
				SetPropertyValue(nameof(Nam), ref nam, value);
				if (!IsLoading)
					TenBaoCao = $"Báo cáo quý {Quy} năm {Nam}";
			}
		}

        //bool pheDuyet;
        //[XafDisplayName("Phê duyệt"), ToolTip(""), ModelDefault("AllowEdit", "False")]
        //public bool PheDuyet {
        //	get => pheDuyet;
        //	set => SetPropertyValue(nameof(PheDuyet), ref pheDuyet, value);
        //}

        //[Action(Caption = "Phê duyệt", ConfirmationMessage = "Phê duyệt báo cáo này? Sau khi phê duyệt, dữ liệu của tất cả các kỳ báo cáo tháng thuộc quý này sẽ không thể sửa được nữa.", AutoCommit = true, TargetObjectsCriteria = "[PheDuyet]=False", SelectionDependencyType = MethodActionSelectionDependencyType.RequireMultipleObjects)]
        //public void PheDuyetAction() {
        //	PheDuyet = true;
        //	var baocaothang = Session.Query<KyBaoCao>().Where(i => i.BaoCaoQuy.Equals(this)).ToList();
        //	foreach (var thang in baocaothang) {
        //		thang.PheDuyet = true;
        //	}
        //}

        #region Associations
  //      [Association("KyBaoCaoQuy-KetQuaThamDinhCSSXs")]
  //      [XafDisplayName("Kết quả thẩm định")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<KetQuaThamDinhCSSX> KetQuaThamDinhCSSXs
		//{
  //          get => GetCollection<KetQuaThamDinhCSSX>(nameof(KetQuaThamDinhCSSXs));
  //      }

  //      [Association("KyBaoCaoQuy-KetQuaThanhKiemTraCSSXs")]
  //      [XafDisplayName("Kết quả thanh kiểm tra")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<KetQuaThanhKiemTraCSSX> KetQuaThanhKiemTraCSSXs
		//{
  //          get => GetCollection<KetQuaThanhKiemTraCSSX>(nameof(KetQuaThanhKiemTraCSSXs));
  //      }

  //      [Association("KyBaoCaoQuy-GiayChungNhanATTPs")]
  //      [XafDisplayName("Giấy chứng nhận đã cấp")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<GiayChungNhanATTP> GiayChungNhanATTPs
		//{
  //          get => GetCollection<GiayChungNhanATTP>(nameof(GiayChungNhanATTPs));
  //      }

  //      [Association("KyBaoCaoQuy-DuLieuKiemTraCSSXs")]
		//[XafDisplayName("Kết quả kiểm tra cơ sở SXKD")]
		//[ModelDefault("AllowEdit", "False")]
		//public XPCollection<DuLieuKiemTraCSSX> DuLieuKiemTraCSSXs => GetCollection<DuLieuKiemTraCSSX>(nameof(DuLieuKiemTraCSSXs));

		//[Association("BaoCaoQuy-VanBanChiDaos")]
		//[XafDisplayName("Văn bản chỉ đạo đã ban hành")]
		//[ModelDefault("AllowEdit", "False")]
		//public XPCollection<VanBanChiDao> VanBanChiDaos
		//{
		//	get => GetCollection<VanBanChiDao>(nameof(VanBanChiDaos));
		//}

		//[Association("BaoCaoQuy-HoatDongSanPhamTruyenThongs")]
		//[ModelDefault("AllowEdit", "False")]
		//[XafDisplayName("Hoạt động sản phẩm truyền thông đã thực hiện")]
		//public XPCollection<HoatDongSanPhamTruyenThong> HoatDongSanPhamTruyenThongs
		//{
		//	get => GetCollection<HoatDongSanPhamTruyenThong>(nameof(HoatDongSanPhamTruyenThongs));
		//}
		#endregion

		private XPCollection<AuditDataItemPersistent> auditTrail;
		[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
		public XPCollection<AuditDataItemPersistent> AuditTrail
		{
			get
			{
				if (auditTrail == null)
				{
					auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
				}
				return auditTrail;
			}
		}
	}
}