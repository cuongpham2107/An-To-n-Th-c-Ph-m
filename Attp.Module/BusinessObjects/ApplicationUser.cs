using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Attp.Module.BusinessObjects.V2;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Security;
using DevExpress.Persistent.BaseImpl.PermissionPolicy;
using DevExpress.Xpo;

namespace Attp.Module.BusinessObjects {
    [MapInheritance(MapInheritanceType.ParentTable)]
    [DefaultProperty(nameof(UserName))]
    public class ApplicationUser : PermissionPolicyUser, IObjectSpaceLink, ISecurityUserWithLoginInfo {
        public ApplicationUser(Session session) : base(session) { }

        [Browsable(false)]
        [Aggregated, Association("User-LoginInfo")]
        public XPCollection<ApplicationUserLoginInfo> LoginInfo {
            get { return GetCollection<ApplicationUserLoginInfo>(nameof(LoginInfo)); }
        }

        IEnumerable<ISecurityUserLoginInfo> IOAuthSecurityUser.UserLogins => LoginInfo.OfType<ISecurityUserLoginInfo>();

        IObjectSpace IObjectSpaceLink.ObjectSpace { get; set; }

        ISecurityUserLoginInfo ISecurityUserWithLoginInfo.CreateUserLoginInfo(string loginProviderName, string providerUserKey) {
            ApplicationUserLoginInfo result = ((IObjectSpaceLink)this).ObjectSpace.CreateObject<ApplicationUserLoginInfo>();
            result.LoginProviderName = loginProviderName;
            result.ProviderUserKey = providerUserKey;
            result.User = this;
            return result;
        }

        CoQuanQuanLy coquanQuanly;
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ quan quản lý")]
        [Association("CoQuanQuanLy-ApplicationUsers")]
        public CoQuanQuanLy CoquanQuanly
        {
            get => coquanQuanly;
            set => SetPropertyValue(nameof(CoquanQuanly), ref coquanQuanly, value);
        }

        DanhMucCoQuanQuanLy danhMucCoQuanQuanLy;
        [DevExpress.ExpressApp.DC.XafDisplayName("Cơ quan quản lý")]
        [Association("DanhMucCoQuanQuanLy-ApplicationUsers")]
        public DanhMucCoQuanQuanLy DanhMucCoQuanQuanLy
        {
            get => danhMucCoQuanQuanLy;
            set => SetPropertyValue(nameof(DanhMucCoQuanQuanLy), ref danhMucCoQuanQuanLy, value);
        }

	}
}
