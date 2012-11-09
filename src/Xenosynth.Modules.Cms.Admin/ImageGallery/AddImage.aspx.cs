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
    public partial class AddImage : System.Web.UI.Page {

        protected MessageBox MessageBox1;

        private CmsImageGallery cachedGallery;

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

        public CmsImageGallery CurrentGallery {
            get {
                if (cachedGallery == null && GalleryID != Guid.Empty) {
                    cachedGallery = CmsImageGallery.FindByID(GalleryID);
                }
                return cachedGallery;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                try {
                    CmsImage i = new CmsImage(HtmlInputFileAttach.PostedFile);
                    DataBindingManagerImage.DataSource = i;
                    DataBindingManagerImage.PullData();

                    i.ParentID = CurrentGallery.ID;
                    i.SortOrder = CurrentGallery.Files.Count;
                    i.Insert();

                    Response.Redirect(i.FileType.EditUrl + "?FileID=" + i.ID);
                } catch (Exception ex) {
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ex.Message);
                }
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {

        }
    }
}
