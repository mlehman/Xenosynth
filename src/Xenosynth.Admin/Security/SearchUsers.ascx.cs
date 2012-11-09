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
    public partial class SearchUsers : System.Web.UI.UserControl {

        protected void Page_Load(object sender, EventArgs e) {

        }

        public void ButtonSearch_OnClick(object sender, System.EventArgs e) {
            string url = string.Format("~/Content/Search.aspx?Email={0}&UserName={1}",
                TextBoxEmail.Text.Trim(),
                TextBoxUserName.Text.Trim());
            Response.Redirect(url);
        }
    }
}