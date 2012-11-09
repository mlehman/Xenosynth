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
    public partial class EditUserRoles : System.Web.UI.Page {

        MembershipUser cachedUser;
        string[] cachedRoles;

        public Guid UserID {
            get { return new Guid(Request["UserID"]); }
        }

        public MembershipUser CurrentUser {
            get {
                if (cachedUser == null) {
                    cachedUser = Membership.GetUser(UserID);
                }
                return cachedUser;
            }
        }

        public string[] CurrentUserRoles {
             get {
                if (cachedRoles == null) {
                    cachedRoles = Roles.GetRolesForUser(CurrentUser.UserName);
                }
                return cachedRoles;
            }
        }

        public bool UserIsInRole(string role) {
            return Array.Exists(CurrentUserRoles, delegate(string s) { return s == role; });
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                UserTabControl1.UserID = UserID;

                DataGridUserRoles.DataSource = Roles.GetAllRoles();
                DataGridUserRoles.DataBind();

            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {

            foreach (DataGridItem di in DataGridUserRoles.Items) {
                
                CheckBox cb = (CheckBox)di.FindControl("CheckBoxUserInRole");
                HiddenField hf = (HiddenField)di.FindControl("HiddenFieldRole");

                string role = hf.Value;
                bool userInRole = UserIsInRole(role);

                if (cb.Checked && !userInRole) {
                    Roles.AddUserToRole(CurrentUser.UserName,role);
                } else if (!cb.Checked && userInRole) {
                    Roles.RemoveUserFromRole(CurrentUser.UserName,role);
                }
                
            }

            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "User roles have been updated.");
        }
    }
}
