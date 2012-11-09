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

using Xenosynth.Admin.Controls.WebParts;

namespace Xenosynth.Admin.Configuration {
    public partial class WebParts : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridWebParts.DataSource = RegisteredWebPart.FindAll();
                DataGridWebParts.DataBind();
            }
        }
    }
}
