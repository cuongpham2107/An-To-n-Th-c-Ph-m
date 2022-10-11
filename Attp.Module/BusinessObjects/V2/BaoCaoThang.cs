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
	[NavigationItem(Common.ReportMenuItem)]
	[DefaultClassOptions]
	[XafDisplayName("Báo cáo tháng")]
	[DefaultProperty(nameof(TenKyBaoCao))]
	[ImageName("BO_Contact")]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	public class KyBaoCaoThang : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        // Use CodeRush to create XPO classes and properties with a few keystrokes.
        // https://docs.devexpress.com/CodeRushForRoslyn/118557
        public KyBaoCaoThang(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
		int nam;
		[XafDisplayName("Năm")]
		public int Nam
		{
			get => nam;
			set
			{
				SetPropertyValue(nameof(Nam), ref nam, value);
				if (!IsLoading)
					TenKyBaoCao = $"Báo cáo tháng {Thang} năm {Nam}";
			}
		}

		int thang;
		[XafDisplayName("Tháng")]
		public int Thang
		{
			get => thang;
			set
			{
				SetPropertyValue(nameof(Thang), ref thang, value);
				if (!IsLoading)
					TenKyBaoCao = $"Báo cáo tháng {Thang} năm {Nam}";
			}
		}

		string tenKyBaoCao;
		[XafDisplayName("Tên báo cáo")]
		public string TenKyBaoCao
		{
			get => tenKyBaoCao;
			set => SetPropertyValue(nameof(TenKyBaoCao), ref tenKyBaoCao, value);
		}

		KyBaoCaoNam kyBaoCaoNam;
		//[Association("BaoCaoNam-KyBaoCaos")]
		[XafDisplayName("Báo cáo năm")]
		public KyBaoCaoNam KyBaoCaoNam
		{
			get => kyBaoCaoNam;
			set => SetPropertyValue(nameof(KyBaoCaoNam), ref kyBaoCaoNam, value);
		}

		KyBaoCaoSauThang kyBaoCaoSauThang;
		//[Association("BaoCaoSauThang-KyBaoCaos")]
		[XafDisplayName("Báo cáo 6 tháng")]
		public KyBaoCaoSauThang KyBaoCaoSauThang
		{
			get => kyBaoCaoSauThang;
			set => SetPropertyValue(nameof(KyBaoCaoSauThang), ref kyBaoCaoSauThang, value);
		}

		KyBaoCaoQuy kyBaoCaoQuy;
		//[Association("BaoCaoQuy-KyBaoCaos")]
		[XafDisplayName("Báo cáo quý")]
		public KyBaoCaoQuy KyBaoCaoQuy
		{
			get => kyBaoCaoQuy;
			set => SetPropertyValue(nameof(BaoCaoQuy), ref kyBaoCaoQuy, value);
		}


		#region Associations
		//[Association("KyBaoCaoThang-KetQuaThamDinhCSSXs")]
		//[XafDisplayName("Kết quả thẩm định")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<KetQuaThamDinhCSSX> KetQuaThamDinhCSSXs
		//{
  //          get => GetCollection<KetQuaThamDinhCSSX>(nameof(KetQuaThamDinhCSSXs));
  //      }

  //      [Association("KyBaoCaoThang-KetQuaThanhKiemTraCSSXs")]
  //      [XafDisplayName("Kết quả thanh kiểm tra")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<KetQuaThanhKiemTraCSSX> KetQuaThanhKiemTraCSSXs
		//{
  //          get => GetCollection<KetQuaThanhKiemTraCSSX>(nameof(KetQuaThanhKiemTraCSSXs));
  //      }

  //      [Association("KyBaoCaoThang-GiayChungNhanATTPs")]
  //      [XafDisplayName("Giấy chứng nhận đã cấp")]
  //      [ModelDefault("AllowEdit", "False")]
  //      public XPCollection<GiayChungNhanATTP> GiayChungNhanATTPs
		//{
  //          get => GetCollection<GiayChungNhanATTP>(nameof(GiayChungNhanATTPs));
  //      }

  //      [Association("KyBaoCaoThang-DuLieuKiemTraCSSXs")]
		//[XafDisplayName("Kết quả kiểm tra cơ sở SXKD")]
		//[ModelDefault("AllowEdit", "False")]
		//public XPCollection<DuLieuKiemTraCSSX> DuLieuKiemTraCSSXs => GetCollection<DuLieuKiemTraCSSX>(nameof(DuLieuKiemTraCSSXs));

		//[Association("KyBaoCao-VanBanChiDaos")]
		//[XafDisplayName("Văn bản chỉ đạo đã ban hành")]
		//[ModelDefault("AllowEdit", "False")]
		//public XPCollection<VanBanChiDao> VanBanChiDaos
		//{
		//	get => GetCollection<VanBanChiDao>(nameof(VanBanChiDaos));
		//}

		//[Association("KyBaoCao-HoatDongSanPhamTruyenThongs")]
		//[XafDisplayName("Hoạt động sản phẩm truyền thông đã thực hiện")]
		//[ModelDefault("AllowEdit", "False")]
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