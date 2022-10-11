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
	[XafDisplayName("File dữ liệu")]
    [NavigationItem(Common.DataMenuItem)]
    [DefaultListViewOptions(MasterDetailMode.ListViewOnly, true, NewItemRowPosition.Top)]
	[FileAttachment(nameof(FileData))]

	public class FileDL : BaseObject
    { 
        public FileDL(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            
        }

		FileData fileData;
		[XafDisplayName("File dữ liệu"), ToolTip("")]
		public FileData FileData
		{
			get => fileData;
			set => SetPropertyValue(nameof(FileData), ref fileData, value);
		}

		string moTa;
		[XafDisplayName("Mô tả"), ToolTip("")]
		public string MoTa
		{
			get => moTa;
			set => SetPropertyValue(nameof(MoTa), ref moTa, value);
		}

		DateTime thoiGian = DateTime.Now;
		[XafDisplayName("Thời gian tạo"), ToolTip("")]
		public DateTime ThoiGian
		{
			get => thoiGian;
			set => SetPropertyValue(nameof(ThoiGian), ref thoiGian, value);
		}

        KetQuaThamDinhCSSXKD ketQuaThamDinhCSSXKD;
        [XafDisplayName("Kết quả thẩm định"), ToolTip("")]
        [VisibleInListView(false)]
        [ModelDefault("AllowEdit", "False")]
        [Association("KetQuaThamDinhCSSXKD-FileDLs")]
        public KetQuaThamDinhCSSXKD KetQuaThamDinhCSSXKD
        {
            get => ketQuaThamDinhCSSXKD;
            set => SetPropertyValue(nameof(KetQuaThamDinhCSSXKD), ref ketQuaThamDinhCSSXKD, value);
        }

		KetQuaThanhKiemTraCSSX ketQuaThanhKiemTraCSSX;
        [XafDisplayName("Kết quả thanh kiểm tra"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("KetQuaThanhKiemTraCSSX-FileDLs")]
		public KetQuaThanhKiemTraCSSX KetQuaThanhKiemTraCSSX
        {
            get { return ketQuaThanhKiemTraCSSX; }
            set { SetPropertyValue(nameof(KetQuaThanhKiemTraCSSX), ref ketQuaThanhKiemTraCSSX, value); }
        }

		SuPhatHanhChinh suPhatHanhChinh; 
		[XafDisplayName("Sử phạt hành chính"), ToolTip("")]
		[VisibleInListView(false)]
		[ModelDefault("AllowEdit", "False")]
		[Association("SuPhatHanhChinh-FileDLs")]
		public SuPhatHanhChinh SuPhatHanhChinh
		{
			get { return suPhatHanhChinh; }
			set { SetPropertyValue(nameof(SuPhatHanhChinh), ref suPhatHanhChinh, value); }
		}

	}
}