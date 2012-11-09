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

namespace Xenosynth.Admin.Content.ImageGallery {
    public partial class ViewGalleryHistory : System.Web.UI.Page {

        CmsFile fileCache;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsFile CurrentFile {
            get {
                if (fileCache == null) {
                    fileCache = CmsFile.FindByID(FileID);
                }
                return fileCache;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                ViewFileHistory1.FileToBind = CurrentFile;
                ViewFileHistory1.DataBind();
            }
        }

    }
}
