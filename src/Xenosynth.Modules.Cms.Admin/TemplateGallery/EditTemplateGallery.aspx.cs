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

using Xenosynth.Admin.Controls;
using Xenosynth.Web.UI;

namespace Xenosynth.Admin.Content.TemplateGallery {
    public partial class EditTemplateGallery : System.Web.UI.Page {

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsTemplateGallery CurrentDirectory {
            get { return CmsTemplateGallery.FindByID(DirectoryID); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerGallery.DataSource = CurrentDirectory;
                DataBindingManagerGallery.PushData();
            }
        }


        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            CmsTemplateGallery d = CurrentDirectory;
            DataBindingManagerGallery.DataSource = d;
            DataBindingManagerGallery.PullData();

            d.Update();

            d.RefreshDescendentFullPaths();  //TODO: not right spot
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Template Gallery has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsTemplateGallery d = CurrentDirectory;
            DataBindingManagerGallery.DataSource = d;
            DataBindingManagerGallery.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
