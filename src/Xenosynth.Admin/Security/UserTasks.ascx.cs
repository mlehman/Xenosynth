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

namespace Xenosynth.Admin.Users {
    public partial class UserTasks : System.Web.UI.UserControl {

        public MembershipUser CurrentUser;

        protected void Page_Load(object sender, EventArgs e) {
            DataBind();
        }

        public void ButtonUnlock_OnClick(object sender, EventArgs e) {
            CurrentUser.UnlockUser();
        }

    }
}