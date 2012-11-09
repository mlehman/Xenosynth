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

namespace Xenosynth.Modules.Cms.Admin.Shortcut {
    public partial class ShortcutTasks : System.Web.UI.UserControl {

        private CmsShortcut shortcutCache;

        public CmsShortcut CurrentFile {
            get {
                if (shortcutCache == null) {
                    if (ViewState["ID"] != null) {
                        shortcutCache = (CmsShortcut)CmsFile.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return shortcutCache;
            }
            set {
                shortcutCache = value;
                ViewState["ID"] = shortcutCache.ID;
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