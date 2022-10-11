using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attp.Module.BusinessObjects {
	//[XafDefaultProperty(nameof(PropertyName))]
	[DefaultClassOptions]
	[ImageName("BO_Contact")]
	[XafDisplayName("Lưu vết dữ liệu")]
	public class AuditLog : BaseObject {
		public AuditLog(Session session) : base(session) { }

        private XPCollection<AuditDataItemPersistent> auditTrail;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AuditDataItemPersistent> AuditTrail {
            get {
                if (auditTrail == null) {
                    auditTrail = AuditedObjectWeakReference.GetAuditTrail(Session, this);
                }
                return auditTrail;
            }
        }

    }
}
