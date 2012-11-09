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
    public partial class UserTabControl : System.Web.UI.UserControl {

        public string Selected;
        public Guid UserID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = UserID != Guid.Empty;
                t.NavigateUrl = "EditUser.aspx?UserID=" + UserID;
                TabControlUser.Tabs.Add(t);

                t = new Tab();
                t.Text = "Roles";
                t.Enabled = UserID != Guid.Empty;
                t.NavigateUrl = "EditUserRoles.aspx?UserID=" + UserID;
                TabControlUser.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = UserID != Guid.Empty;
                t.NavigateUrl = "ViewUserHistory.aspx?UserID=" + UserID;
                TabControlUser.Tabs.Add(t);

                TabControlUser.Tabs.FindByText(Selected).Selected = true;

            }
        }
    }
}