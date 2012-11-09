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

using Xenosynth.Web;

namespace Xenosynth.Admin.Configuration {
    public partial class Sites : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridSites.DataSource = WebSite.FindAll();
                DataGridSites.DataBind();
            }
        }
    }
}
