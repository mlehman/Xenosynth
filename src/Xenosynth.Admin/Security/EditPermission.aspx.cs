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
    public partial class EditPermission : System.Web.UI.Page {

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
                DataBindingManagerPermission.DataSource = CurrentPermission;
                DataBindingManagerPermission.PushData();
            }
        }

        public void ValidatorDuplicate_OnServerValidate(object sender, ServerValidateEventArgs e) {
            Permission permission = Permissions.FindPermissionByKey(e.Value);
            if (permission != null && permission.ID != PermissionID) {
                e.IsValid = false;
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                Permission p = CurrentPermission;
                DataBindingManagerPermission.DataSource = p;
                DataBindingManagerPermission.PullData();
                p.Update();
                Response.Redirect("Permissions.aspx");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect("Permissions.aspx");
        }
    }
}
