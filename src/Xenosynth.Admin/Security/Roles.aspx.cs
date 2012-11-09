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

using Fluent.Logging;

namespace Xenosynth.Admin.Security {
    public partial class ViewRoles : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridRoles.DataSource = Roles.GetAllRoles();
                DataGridRoles.DataBind();
            }
        }

        protected void DataGridRoles_OnDelete(object sender, DataGridCommandEventArgs e) {


            string role = (string)e.CommandArgument;

            if (Roles.GetUsersInRole(role).Length != 0) {
                MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Error, "This role cannot be deleted because there are users present in it.");
                return;
            }

            Roles.DeleteRole(role);

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "The role {0} has been deleted.", role );
                
            DataGridRoles.DataSource = Roles.GetAllRoles();
            DataGridRoles.DataBind();
   
        }
    }
}
