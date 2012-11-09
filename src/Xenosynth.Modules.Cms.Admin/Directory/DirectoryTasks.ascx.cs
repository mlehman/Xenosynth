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

using Xenosynth.Web.UI;

namespace Xenosynth.Admin.Content {
    public partial class DirectoryTasks : System.Web.UI.UserControl {

        private CmsWebDirectory directoryCache;

        public CmsWebDirectory CurrentDirectory {
            get {
                if (directoryCache == null) {
                    if (ViewState["ID"] != null) {
                        directoryCache = CmsWebDirectory.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return directoryCache;
            }
            set {
                directoryCache = value;
                ViewState["ID"] = directoryCache.ID;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                if (this.Visible) {
                    DataBind(); 
                }
            }
        }
    }
}