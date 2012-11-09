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
using Fluent.DataBinding;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content.ImageGallery {
    public partial class EditGallery : System.Web.UI.Page {

        protected MessageBox MessageBox1;

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsImageGallery CurrentDirectory {
            get { return (CmsImageGallery)CmsFile.FindByID(DirectoryID); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerImageGallery.DataSource = CurrentDirectory;
                DataBindingManagerImageGallery.PushData();
            }
        }


        protected void ButtonUpdateDirectory_OnClick(object sender, EventArgs e) {
            CmsImageGallery d = CurrentDirectory;
            DataBindingManagerImageGallery.DataSource = d;
            DataBindingManagerImageGallery.PullData();

            d.Update();

            d.RefreshDescendentFullPaths();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Directory has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsImageGallery d = CurrentDirectory;
            DataBindingManagerImageGallery.DataSource = d;
            DataBindingManagerImageGallery.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
