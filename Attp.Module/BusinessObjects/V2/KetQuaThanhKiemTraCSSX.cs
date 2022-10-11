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
	[XafDisplayName("Kết quả thanh kiểm tra")]
	[DefaultProperty(nameof(Title))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]
	//[NavigationItem(R.V1)]
	[NavigationItem(Common.DataMenuItem)]

	public class KetQuaThanhKiemTraCSSX : BaseObject
	{
		public KetQuaThanhKiemTraCSSX(Session session)
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


		DateTime ngayThanhKiemTra;
		[XafDisplayName("Ngày thanh kiểm tra")]
		public DateTime NgayThanhKiemTra
		{
			get => ngayThanhKiemTra;
			set
			{
				SetPropertyValue(nameof(NgayThanhKiemTra), ref ngayThanhKiemTra, value);
			}
		}

		DanhMucCoSoSanXuatKinhDoanh danhMucCoSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("DanhMucCoSoSanXuatKinhDoanh-KetQuaThanhKiemTraCSSXs")]
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
		XLoai xLoai;
		[XafDisplayName("Đánh giá xếp loại")]
		public XLoai XLoai
		{
			get { return xLoai; }
			set { SetPropertyValue(nameof(XLoai), ref xLoai, value); }
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
		[Association("KetQuaThanhKiemTraCSSX-FileDLs")]
		[XafDisplayName("File kết quả thẩm định")]
		public XPCollection<FileDL> FileDLs
		{
			get
			{
				return GetCollection<FileDL>(nameof(FileDLs));
			}
		}


		[Association("KetQuaThanhKiemTraCSSX-SuPhatHanhChinhs")]
		[XafDisplayName("Kết quả sử phạt hành chính")]
		public XPCollection<SuPhatHanhChinh> SuPhatHanhChinhs
		{
			get
			{
				return GetCollection<SuPhatHanhChinh>(nameof(SuPhatHanhChinhs));
			}
		}
        #endregion

    }

    public enum LThamDinh
	{
		[XafDisplayName("Kiểm tra đánh giá phân loại")] KiemTraDanhGiaXepLoai = 0,
		[XafDisplayName("Kiểm tra định kỳ")] KiemTraDinhKy = 1,
		//[XafDisplayName("Tái kiểm tra")] TaiKiemTra = 2,
	}
}