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

namespace Xenosynth.Admin.Controls {
    public partial class PathBrowser : System.Web.UI.UserControl {

        public string RootPage;
        public string RootPageName;
        public string SubPage;
        public string SubPageName;

        protected void Page_Load(object sender, EventArgs e) {
            this.DataBind();
        }
    }
}