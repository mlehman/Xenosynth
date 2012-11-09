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
using Xenosynth.Modules.Cms.Admin.Controls;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class EditDocumentAttributes : System.Web.UI.Page {

        protected EditFileAttributes EditFileAttributes1;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsDocument CurrentDocument {
            get { return (CmsDocument)CmsFile.FindByID(FileID); }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                EditFileAttributes1.FileToBind = CurrentDocument;
                EditFileAttributes1.DataBind();
            }
        }
    }
}
