using Attp.Module.BusinessObjects;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Linq;

namespace Attp.Module.Controllers {
	public class CoSoViewController : ViewController {
		public CoSoViewController() {
			TargetObjectType = typeof(CoSoSanXuatKinhDoanh);
			//TargetViewType = ViewType.ListView;
			SetCoQuanQuanLy();
			SetLoaiHinhSXKD();
		}

		private void SetCoQuanQuanLy() {
			PopupWindowShowAction action = new(this, "Chọn cơ quan quản lý", "Edit") {
				Caption = "Chọn cơ quan quản lý",
				SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
			};
			
			action.CustomizePopupWindowParams += (s, e) => {
				//IObjectSpace objectSpace = Application.CreateObjectSpace();
				//string listViewId = Application.FindLookupListViewId(typeof(CoQuanQuanLy));
				//CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(CoQuanQuanLy), listViewId);
				//e.View = Application.CreateListView(listViewId, collectionSource, true);

				IObjectSpace objectSpace = Application.CreateObjectSpace();
				var keHoach = new Parameter();
				//string listViewId = Application.FindLookupListViewId(typeof(CoQuanQuanLy));
				//CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(CoQuanQuanLy), listViewId);
				e.View = Application.CreateDetailView(objectSpace, keHoach, true);
			};
			action.Execute += (s, e) => {
				//var popupCoquan = e.PopupWindowViewCurrentObject as CoQuanQuanLy;
				//var coquan = ObjectSpace.GetObject(popupCoquan);
				//foreach (object item in View.SelectedObjects) {
				//	(item as CoSoSanXuatKinhDoanh).CoQuanQuanLy = coquan;
				//}
				//ObjectSpace.CommitChanges();
				var popupCoquan = e.PopupWindowViewCurrentObject as Parameter;
				var keHoach = ObjectSpace.GetObject(popupCoquan);

				IObjectSpace objectSpace = Application.CreateObjectSpace();
				string listViewId = Application.FindListViewId(typeof(CoSoSanXuatKinhDoanh));

				//var criteria = view.CollectionSource.Criteria;
				//if (View.ObjectTypeInfo.Type == typeof(CoSoSanXuatKinhDoanh))
				//	criteria.Add("crit1", new BinaryOperator("CoQuanQuanLy.Oid", account.CoquanQuanly.Oid, BinaryOperatorType.Equal));

				CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(CoSoSanXuatKinhDoanh), listViewId);
				collectionSource.Criteria.Add("crit1", new BinaryOperator("CoSoQuanLyKinhDoanh.MaSo", "20J8.001.438", BinaryOperatorType.Equal));
				e.ShowViewParameters.CreatedView=Application.CreateListView(listViewId, collectionSource, true);
				
			};
		}

		private void SetLoaiHinhSXKD() {
			PopupWindowShowAction action = new(this, "Chọn loại hình SXKD", "Edit") {
				Caption = "Chọn loại hình SXKD",
				SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects
			};
			action.CustomizePopupWindowParams += (s, e) => {
				IObjectSpace objectSpace = Application.CreateObjectSpace();
				string listViewId = Application.FindLookupListViewId(typeof(LoaiHinhCoSo));
				CollectionSourceBase collectionSource = Application.CreateCollectionSource(objectSpace, typeof(LoaiHinhCoSo), listViewId);
				e.View = Application.CreateListView(listViewId, collectionSource, true);
			};
			action.Execute += (s, e) => {
				var popupLoaihinh = e.PopupWindowViewCurrentObject as LoaiHinhCoSo;
				var loaihinh = ObjectSpace.GetObject(popupLoaihinh);
				foreach (object item in View.SelectedObjects) {
					(item as CoSoSanXuatKinhDoanh).LoaiHinhCoSo = loaihinh;
				}
				ObjectSpace.CommitChanges();
			};
		}
	}

	public class CosoViewController<T> : ViewController {
		public CosoViewController() {
			TargetObjectType = typeof(T);
			SwitchToCoso();
		}

		private void SwitchToCoso() {
			SimpleAction action = new(this, $"SwitchToCosoFrom_{typeof(T)}", "Edit") {
				SelectionDependencyType = SelectionDependencyType.RequireSingleObject,
				Caption = "Xem cơ sở"
			};
			action.Execute += (s, e) => {
				var os = Application.CreateObjectSpace(typeof(T));
				var currentObj = View.CurrentObject;

				object cssxkd = currentObj switch {
					GiayChungNhan => (currentObj as GiayChungNhan)?.CoSoSanXuatKinhDoanh,
					KeHoachThamDinh => (currentObj as KeHoachThamDinh)?.CoSoSanXuatKinhDoanh,
					KeHoachThanhKiemTra => (currentObj as KeHoachThanhKiemTra)?.CoSoSanXuatKinhDoanh,
					DuLieuKiemTraCSSXKD => (currentObj as DuLieuKiemTraCSSXKD)?.CoSoSanXuatKinhDoanh,
					_ => null
				};

				if (cssxkd != null) {
					var obj = os.GetObject(cssxkd);
					e.ShowViewParameters.CreatedView = Application.CreateDetailView(os, obj);
				}
			};
		}
	}

	public class GiayChungNhanViewController : CosoViewController<GiayChungNhan> { }

	public class KeHoachThamDinhViewController : CosoViewController<KeHoachThamDinh> { }

	public class KeHoachThanhKiemTraViewController : CosoViewController<KeHoachThanhKiemTra> { }

	public class DuLieuKiemTraCSSXKDViewController : CosoViewController<DuLieuKiemTraCSSXKD> { }
}
