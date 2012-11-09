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

using Fluent.Navigation;

namespace Xenosynth.Admin.Security {
    public partial class PermissionTabControl : System.Web.UI.UserControl {
        public string Selected;
        public Guid permissionID;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "General";
                t.Enabled = permissionID != Guid.Empty;
                t.NavigateUrl = "EditPermission.aspx?PermissionID=" + permissionID;
                TabControlPermission.Tabs.Add(t);

                t = new Tab();
                t.Text = "Roles";
                t.Enabled = permissionID != Guid.Empty;
                t.NavigateUrl = "EditPermissionRoles.aspx?PermissionID=" + permissionID;
                TabControlPermission.Tabs.Add(t);


                TabControlPermission.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}