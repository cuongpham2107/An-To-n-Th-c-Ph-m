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
    [NavigationItem(Common.DataMenuItem)]
    [DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Giấy chứng nhận")]
	[DefaultProperty(nameof(SoCap))]
	[DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[ListViewFindPanel(true)]
	[LookupEditorMode(LookupEditorMode.AllItemsWithSearch)]

	public class GiayChungNhanATTP : BaseObject
    { 
        public GiayChungNhanATTP(Session session)
            : base(session)
        {
        }
		public override void AfterConstruction()
        {
            base.AfterConstruction();
			
        }
		string soCap;
		[XafDisplayName("Số giấy chứng nhận")]
		[Size(SizeAttribute.DefaultStringMappingFieldSize)]
		public string SoCap
		{
			get => soCap;
			set => SetPropertyValue(nameof(SoCap), ref soCap, value);
		}

		DanhMucCoSoSanXuatKinhDoanh danhMucCoSoSanXuatKinhDoanh;
		[XafDisplayName("Cơ sở sản xuất kinh doanh")]
		[Association("DanhMucCoSoSanXuatKinhDoanh-GiayChungNhanATTPs")]
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

		DateTime ngayCap;
		[XafDisplayName("Ngày cấp giấy chứng nhận")]
		[ModelDefault("ImmediatePostData", "True")]
		public DateTime NgayCap
		{
			get => ngayCap;
			set
			{
				SetPropertyValue(nameof(NgayCap), ref ngayCap, value);

			}
		}

        [XafDisplayName("Kết quả thẩm định")]
        [ModelDefault("AllowEdit","False")]		
        public XLoai XepLoaiThamDinh
        {
            get
            {
				if (!IsLoading && !IsSaving)
                {
					if (DanhMucCoSoSanXuatKinhDoanh != null)
					{
						return (XLoai)DanhMucCoSoSanXuatKinhDoanh.KetQuaThamDinh;
					}
				}		
                return XLoai.chuaxeploai;

            }
        }

		[XafDisplayName("Kết quả thanh kiểm tra")]
		[ModelDefault("AllowEdit", "False")]

		public XLoai XepLoaiThanhKiemTra
		{
			get
			{
				if (!IsLoading && !IsSaving)
				{
					if (DanhMucCoSoSanXuatKinhDoanh != null)
					{
						return (XLoai)DanhMucCoSoSanXuatKinhDoanh.KetQuaThanhKiemTra;
					}
				}
				return XLoai.chuaxeploai;

			}
		}


		DateTime coHieuLucDen;
		[XafDisplayName("Có hiệu lực đến")]
		public DateTime CoHieuLucDen
		{
			get => coHieuLucDen;
			set => SetPropertyValue(nameof(CoHieuLucDen), ref coHieuLucDen, value);
		}

		FileData fileData;
		[XafDisplayName("File scan")]
		[VisibleInListView(false)]
		public FileData FileData
		{
			get => fileData;
			set => SetPropertyValue(nameof(FileData), ref fileData, value);
		}


		#region Thu_hồi
		bool bThuHoi;
		[XafDisplayName("Bị thu hồi"), ToolTip("")]
		[ModelDefault("AllowEdit", "False")]
		public bool BiThuHoi
		{
			get
            {
				if(!IsLoading && !IsSaving)
                {
					if (XepLoaiThanhKiemTra == XLoai.C)
					{
						return true;
					}
				}		
				return false;
            }
			set => SetPropertyValue(nameof(BiThuHoi), ref bThuHoi, value);
		}

		DateTime ngayThuHoi;
		[XafDisplayName("Ngày thu hồi"), ToolTip("")]
		public DateTime NgayThuHoi
		{
			get
            {
				if (!IsLoading && !IsSaving)
				{
					if (XepLoaiThanhKiemTra == XLoai.C)
					{
						return (DateTime)DanhMucCoSoSanXuatKinhDoanh.NgayThanhKiemTra;
					}
				}
				return ngayThuHoi;
			}
			set => SetPropertyValue(nameof(NgayThuHoi), ref ngayThuHoi, value);
		}

		string lyDoThuHoi;
		[XafDisplayName("Lý do thu hồi"), ToolTip("")]
		public string LyDoThuHoi
		{
            get
            {
				if (!IsLoading && !IsSaving)
				{
					if (XepLoaiThanhKiemTra == XLoai.C)
					{
						return $"Kết quả thanh kiểm tra của Cơ sở sản xuất kinh doanh {DanhMucCoSoSanXuatKinhDoanh.TenCoSo} Đạt Xếp loại {XLoai.C}";
					}
				}
				return lyDoThuHoi;
			}
			set => SetPropertyValue(nameof(LyDoThuHoi), ref lyDoThuHoi, value);
		}

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