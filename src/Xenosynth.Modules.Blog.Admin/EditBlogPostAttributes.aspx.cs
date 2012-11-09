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
using Xenosynth.Modules.Cms.Admin.Controls;
using Xenosynth.Web.UI;

namespace Xenosynth.Modules.Blog.Admin {
    public partial class EditBlogPostAttributes : System.Web.UI.Page {
        protected EditFileAttributes EditFileAttributes1;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsFile CurrentFile {
            get { return CmsFile.FindByID(FileID); }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                EditFileAttributes1.FileToBind = CurrentFile;
                EditFileAttributes1.DataBind();
            }
        }
    }
}
