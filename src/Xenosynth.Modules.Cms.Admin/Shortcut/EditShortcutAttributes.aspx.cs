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
    public partial class EditShortcutAttributes : System.Web.UI.Page {

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsShortcut CurrentShortcut {
            get { return (CmsShortcut)CmsFile.FindByID(FileID); }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                EditFileAttributes1.FileToBind = CurrentShortcut;
                EditFileAttributes1.DataBind();
            }
        }
    }
}
