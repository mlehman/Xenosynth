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

namespace Xenosynth.Admin {
    public partial class MyProfile : System.Web.UI.Page {

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {


                MembershipUser m = Membership.GetUser();

                DataBindingManagerUser.DataSource = m;
                DataBindingManagerUser.PushData();

            }
        }

        public void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                MembershipUser m = Membership.GetUser();
                DataBindingManagerUser.DataSource = m;
                DataBindingManagerUser.PullData();
                Membership.UpdateUser(m);
            }
        }

        public void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerUser.DataSource = Membership.GetUser();
            DataBindingManagerUser.PushData();
        }
    }
}
