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

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class EditLibrary : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerDocumentLibrary;
        protected MessageBox MessageBox1;

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsDocumentLibrary CurrentDirectory {
            get { return (CmsDocumentLibrary)CmsFile.FindByID(DirectoryID); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerDocumentLibrary.DataSource = CurrentDirectory;
                DataBindingManagerDocumentLibrary.PushData();
            }
        }

        protected void ButtonUpdateDirectory_OnClick(object sender, EventArgs e) {
            CmsDocumentLibrary d = CurrentDirectory;
            DataBindingManagerDocumentLibrary.DataSource = d;
            DataBindingManagerDocumentLibrary.PullData();

            d.Update();

            d.RefreshDescendentFullPaths();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Document Library has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsDocumentLibrary d = CurrentDirectory;
            DataBindingManagerDocumentLibrary.DataSource = d;
            DataBindingManagerDocumentLibrary.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
