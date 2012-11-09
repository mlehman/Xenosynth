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

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class DocumentLibraryTasks : System.Web.UI.UserControl {

        private CmsDocumentLibrary libraryCache;

        public CmsDocumentLibrary CurrentLibrary {
            get {
                if (libraryCache == null) {
                    if (ViewState["ID"] != null) {
                        libraryCache = CmsDocumentLibrary.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return libraryCache;
            }
            set {
                libraryCache = value;
                ViewState["ID"] = libraryCache.ID;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (this.Visible) {
                    DataBind();
                }
            }
        }
    }
}