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
    public partial class EditImage : System.Web.UI.Page {

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
            if (!IsPostBack) {
                DataBindingManagerImage.DataSource = CurrentImage;
                DataBindingManagerImage.PushData();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                
                CmsImage i = CurrentImage;

                DataBindingManagerImage.DataSource = i;
                DataBindingManagerImage.PullData();

                i.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Image has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerImage.DataSource = CurrentImage;
            DataBindingManagerImage.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
