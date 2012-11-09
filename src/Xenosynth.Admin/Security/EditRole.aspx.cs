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

using Fluent.DataBinding;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Security {
    public partial class EditRole : System.Web.UI.Page {

        protected string CurrentRole {
            get { return Request["role"]; }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridUsers.DataSource = Roles.GetUsersInRole(CurrentRole);
                DataGridUsers.DataBind();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {

            foreach (DataGridItem di in DataGridUsers.Items) {

                CheckBox cb = (CheckBox)di.FindControl("CheckBoxUserInRole");
                HiddenField hf = (HiddenField)di.FindControl("HiddenFieldUser");

                string username = hf.Value;
                string role = Request["role"];

                if (!cb.Checked) {
                    Roles.RemoveUserFromRole(username, role);
                }

            }

            DataGridUsers.DataSource = Roles.GetUsersInRole(CurrentRole);
            DataGridUsers.DataBind();

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Users in role have been updated.");
        }
    }
}
