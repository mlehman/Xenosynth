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
    public partial class RoleTabControl : System.Web.UI.UserControl {
        public string Selected;
        public string role;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Users";
                t.Enabled = role != null;
                t.NavigateUrl = "EditRole.aspx?Role=" + role;
                TabControlRole.Tabs.Add(t);

                t = new Tab();
                t.Text = "Permissions";
                t.Enabled = role != null;
                t.NavigateUrl = "EditRolePermissions.aspx?Role=" + role;
                TabControlRole.Tabs.Add(t);


                TabControlRole.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}