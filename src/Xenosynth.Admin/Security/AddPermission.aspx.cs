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
    public partial class AddPermission : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {

        }

        public void ValidatorDuplicate_OnServerValidate(object sender, ServerValidateEventArgs e) {
            e.IsValid = !Permissions.PermissionExists(e.Value);
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                Permission p = new Permission();
                DataBindingManagerPermission.DataSource = p;
                DataBindingManagerPermission.PullData();
                Permissions.Insert(p);
                Response.Redirect("Permissions.aspx");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect("Permissions.aspx"); 
        }
    }
}
