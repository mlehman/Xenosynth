using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Xenosynth.Security;
using Fluent;

namespace Xenosynth.Admin.Security {
    public partial class ViewUserHistory : System.Web.UI.Page {

        MembershipUser userCached;

        public Guid UserID {
            get { return (Guid)CurrentUser.ProviderUserKey; }
        }

        public MembershipUser CurrentUser {
            get {
                if (userCached == null) {
                    if (Request["UserID"] != null) {
                        userCached = Membership.GetUser(new Guid(Request["UserID"]));
                    } else if (Request["UserName"] != null) {
                        userCached = Membership.GetUser(Request["UserName"]);
                    }
                }
                return userCached;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridAdapterAuditLog.BindDataGrid();
            }
        }

        public void DataGridAdapterAuditLog_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e) {
            DataGridAdapterAuditLog.DataSource = LogEntry.FindByUser(UserID, 30);
            DataGridAdapterAuditLog.DataBind();
        }

    }
}
