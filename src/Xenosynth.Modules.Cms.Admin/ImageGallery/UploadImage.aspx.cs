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
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content.ImageGallery {
    public partial class UploadImage : System.Web.UI.Page {

        private CmsImage cachedImage;

        public Guid FileID {
            get {
                string id = Request["FileID"];
                if (id == null) {
                    return Guid.Empty;
                } else {
                    return new Guid(id);
                }
            }
        }

        public CmsImage CurrentImage {
            get {
                if (cachedImage == null && FileID != Guid.Empty) {
                    cachedImage = CmsImage.FindByID(FileID);
                }
                return cachedImage;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                CurrentImage.UploadImage(HtmlInputFileAttach.PostedFile);
                Response.Redirect("ViewImageSizes.aspx?FileID=" + CurrentImage.ID);
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
