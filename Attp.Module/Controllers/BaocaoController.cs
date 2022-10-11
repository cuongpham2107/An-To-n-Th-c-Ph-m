using Attp.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System;
using System.Linq;

namespace Attp.Module.Controllers {
	public partial class PhanquyenController : ViewController {
		public PhanquyenController() {
			InitializeComponent();
			// Target required Views (via the TargetXXX properties) and create their Actions.
			//TargetObjectType = typeof(CoSoSanXuatKinhDoanh);
			TargetViewNesting = Nesting.Any;
			TargetViewType = ViewType.Any;
			Activated += PhanquyenController_Activated;
		}

		private void PhanquyenController_Activated(object sender, EventArgs e) {
			var os = Application.CreateObjectSpace(typeof(CoSoSanXuatKinhDoanh));
			var account = os.GetObjectByKey<ApplicationUser>(SecuritySystem.CurrentUserId);

			if (account.Roles.Any(r => r.Name == "Administrators" || r.Name == "Managers")) return;

			if (View is ListView view) {
				var criteria = view.CollectionSource.Criteria;
				if (View.ObjectTypeInfo.Type == typeof(CoSoSanXuatKinhDoanh))
					criteria.Add("crit1", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(DuLieuKiemTraCSSXKD))
					criteria.Add("crit2", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(GiayChungNhan))
					criteria.Add("crit3", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(KeHoachThanhKiemTra))
					criteria.Add("crit4", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(KeHoachThamDinh))
					criteria.Add("crit4-1", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(VanBanChiDao))
					criteria.Add("crit5", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				if (View.ObjectTypeInfo.Type == typeof(HoatDongSanPhamTruyenThong))
					criteria.Add("crit6", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));
			}
		}

		protected override void OnActivated() {
			base.OnActivated();
			// Perform various tasks depending on the target View.

		}
		protected override void OnViewControlsCreated() {
			base.OnViewControlsCreated();
			// Access and customize the target View control.
		}
		protected override void OnDeactivated() {
			// Unsubscribe from previously subscribed events and release other references and resources.
			base.OnDeactivated();
		}
	}

	public class DisableValidatedKeHoachThamDinh : ObjectViewController<DetailView, KeHoachThamDinh> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}

	public class DisableValidatedKeHoachThanhKiemTra : ObjectViewController<DetailView, KeHoachThanhKiemTra> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}

	public class DisableValidatedDuLieuKiemTra : ObjectViewController<DetailView, DuLieuKiemTraCSSXKD> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}

	public class DisableValidatedGiayChungNhan : ObjectViewController<DetailView, GiayChungNhan> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}

	public class DisableValidatedVanBanChiDao : ObjectViewController<DetailView, VanBanChiDao> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}

	public class DisableValidatedHoatDongSanPhamTruyenThong : ObjectViewController<DetailView, HoatDongSanPhamTruyenThong> {
		protected override void OnActivated() {
			base.OnActivated();

			if (ViewCurrentObject?.PheDuyet == true) {
				var editors = View.GetItems<PropertyEditor>();
				foreach (var editor in editors) {
					editor.AllowEdit["hihi"] = false;
				}
			}
		}
	}
}
