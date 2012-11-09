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
    public partial class AddUser : System.Web.UI.Page {
  


        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

            }
        }

        public void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                try { 
                    MembershipUser m = Membership.CreateUser(TextBoxUserName.Text, TextBoxPassword.Text, TextBoxEmail.Text);
                    DataBindingManagerUser.DataSource = m;
                    DataBindingManagerUser.PullData();
                    Membership.UpdateUser(m);
                    Response.Redirect("EditUser.aspx?UserID=" + m.ProviderUserKey);
                } catch (Exception ex) {
                    MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Error, ex.Message);
                }
            }
        }

        public void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerUser.Reset();
            MessageBox1.ShowMessage(Xenosynth.Admin.Controls.MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
