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

namespace Xenosynth.Admin.Content.TemplateGallery {
    public partial class AddTemplate : System.Web.UI.Page {

        private CmsTemplateGallery cachedGallery;

        public Guid GalleryID {
            get {
                string id = Request["FileID"];
                if (id == null) {
                    return Guid.Empty;
                } else {
                    return new Guid(id);
                }
            }
        }

        public CmsTemplateGallery CurrentGallery {
            get {
                if (cachedGallery == null && GalleryID != Guid.Empty) {
                    cachedGallery = CmsTemplateGallery.FindByID(GalleryID);
                }
                return cachedGallery;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            CmsTemplate t = new CmsTemplate();
            DataBindingManagerTemplate.DataSource = t;
            DataBindingManagerTemplate.PullData();

            t.ParentID = GalleryID;

            t.SortOrder = CurrentGallery.Files.Count;

            t.Insert();

            Response.Redirect(t.FileType.EditUrl + "?FileID=" + t.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(CurrentGallery.FileType.BrowseUrl + "?FileID=" + CurrentGallery.ID);
        }
    }
}
