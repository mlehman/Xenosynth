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
using Fluent.Navigation;
using System.Drawing;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content.ImageGallery {
    public partial class ViewImageSizes : System.Web.UI.Page {

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

        public Size SelectSize {
            get {
                string key = TabControlImageSizes.SelectedTab.CommandName;

                if (key == "o") {
                    return CurrentImage.Size;
                } else {
                    CmsImageGallery g = (CmsImageGallery)CurrentImage.ParentDirectory;
                    return g.GetImageSize(key).GetSize(CurrentImage.Size);
                }
            }
        }

        public string PreviewImageUrl {
            get {
                string key = TabControlImageSizes.SelectedTab.CommandName;

                if (key == "o") {
                    return CurrentImage.Url;
                } else {
                    CmsImageGallery g = (CmsImageGallery)CurrentImage.ParentDirectory;
                    return g.Url + "_" + key + "/" + CurrentImage.FileName;
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
        

                CmsImageGallery g = (CmsImageGallery)CurrentImage.ParentDirectory;

                Tab t = new Tab();
                t.CommandName = "o";
                t.Text = "Original";
                t.Selected = true;
                TabControlImageSizes.Tabs.Add(t);

                foreach (ImageSize s in g.ImageSizes) {
                    t = new Tab();
                    t.CommandName = s.Key;
                    t.Text = s.Name;
                    TabControlImageSizes.Tabs.Add(t);
                }
            }
        }


    }
}
