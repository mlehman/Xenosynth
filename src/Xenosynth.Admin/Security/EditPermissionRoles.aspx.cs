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
    public partial class EditPermissionRoles : System.Web.UI.Page {

        Permission permissionCache;

        public Guid PermissionID {
            get { return new Guid(Request["PermissionID"]); }
        }

        public Permission CurrentPermission {
            get {
                if (permissionCache == null) {
                    permissionCache = Permissions.FindPermissionByID(PermissionID);
                }
                return permissionCache;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridRoles.DataSource = Roles.GetAllRoles();
                DataGridRoles.DataBind();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {

            foreach (DataGridItem di in DataGridRoles.Items) {

                CheckBox cb = (CheckBox)di.FindControl("CheckBoxRoleHasPermission");
                HiddenField hf = (HiddenField)di.FindControl("HiddenFieldRole");

                //    string username = hf.Value;
                //    string role = Request["role"];

                string role = hf.Value;
                bool roleHasPermission = Permissions.RoleHasPermission(role, PermissionID);

                if (cb.Checked && !roleHasPermission) {
                    Permissions.AddPermissionToRole(role, PermissionID);
                } else if (!cb.Checked && roleHasPermission) {
                    Permissions.RemovePermissionFromRole(role, PermissionID);
                }

            }

            DataGridRoles.DataSource = Roles.GetAllRoles();
            DataGridRoles.DataBind();

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Roles with permission have been updated.");
        }
    }
}
