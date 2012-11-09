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
    public partial class EditUser : System.Web.UI.Page {

        MembershipUser userCached;

        public Guid UserID {
            get { return (Guid)CurrentUser.ProviderUserKey; }
        }

        public MembershipUser CurrentUser {
            get {
                if (userCached == null) {
                    if (Request["UserID"] != null) {
                        userCached = Membership.GetUser(new Guid(Request["UserID"]));
                    } else if (Request["UserName"] != null) {
                        userCached = Membership.GetUser(Request["UserName"]);
                    }
                }
                return userCached;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                UserTabControl1.UserID = UserID;

                MembershipUser m = CurrentUser;

                DataBindingManagerUser.DataSource = m;
                DataBindingManagerUser.PushData();

            }
        }

        public void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (IsValid) {
                MembershipUser m = CurrentUser;
               
                DataBindingManagerUser.DataSource = m;
                DataBindingManagerUser.PullData();
                Membership.UpdateUser(m);

            }
        }

        public void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerUser.DataSource = CurrentUser;
            DataBindingManagerUser.PushData();
        }
    }
}
