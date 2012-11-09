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
    public partial class ImageGalleryTasks : System.Web.UI.UserControl {

        private CmsImageGallery galleryCache;

        public CmsImageGallery CurrentGallery {
            get {
                if (galleryCache == null) {
                    if (ViewState["ID"] != null) {
                        galleryCache = CmsImageGallery.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return galleryCache;
            }
            set {
                galleryCache = value;
                ViewState["ID"] = galleryCache.ID;
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