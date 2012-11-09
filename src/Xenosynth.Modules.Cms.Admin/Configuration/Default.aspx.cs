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
using Xenosynth.Web.UI;

namespace Xenosynth.Modules.Cms.Admin.Configuration {
    public partial class Default : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataGridFileTypes.DataSource = CmsFileType.FindAll();
                DataGridFileTypes.DataBind();
            }
        }
    }
}
