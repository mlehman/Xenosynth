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

namespace Xenosynth.Admin.Security {
    public partial class EditRolePermissions : System.Web.UI.Page {

        protected string CurrentRole {
            get { return Request["role"]; }
        }


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                BindPermissions();
            }
        }

        private void BindPermissions() {
            DataGridPermissions.DataSource = Permissions.FindAllPermissions();
            DataGridPermissions.DataBind();
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {

            foreach (DataGridItem di in DataGridPermissions.Items) {

                CheckBox cb = (CheckBox)di.FindControl("CheckBoxRoleHasPermission");
            //    HiddenField hf = (HiddenField)di.FindControl("HiddenFieldUser");

            //    string username = hf.Value;
            //    string role = Request["role"];

                Guid permissionID = (Guid)DataGridPermissions.DataKeys[di.ItemIndex];
                bool roleHasPermission = Permissions.RoleHasPermission(CurrentRole, permissionID);

                if (cb.Checked && !roleHasPermission) {
                    Permissions.AddPermissionToRole(CurrentRole, permissionID);
                } else if (!cb.Checked && roleHasPermission){
                    Permissions.RemovePermissionFromRole(CurrentRole, permissionID);
                }

            }

            DataGridPermissions.DataSource = Permissions.FindAllPermissions();
            DataGridPermissions.DataBind();

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Permissions in role have been updated.");
        }
    }
}
