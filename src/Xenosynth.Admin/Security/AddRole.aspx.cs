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

namespace Xenosynth.Admin.Security {
    public partial class AddRole : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void ValidatorDuplicate_OnServerValidate(object sender, ServerValidateEventArgs e) {
            e.IsValid = !Roles.RoleExists(e.Value);
        }

        public void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                Roles.CreateRole(TextBoxRole.Text);
                Response.Redirect("Roles.aspx");
            }
        }

        public void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect("Roles.aspx");
        }
    }
}
