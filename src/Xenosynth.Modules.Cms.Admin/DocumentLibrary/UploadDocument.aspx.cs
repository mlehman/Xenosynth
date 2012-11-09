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
using Fluent.DataBinding;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class UploadDocument : System.Web.UI.Page {

        protected MessageBox MessageBox1;
        protected DataBindingManager DataBindingManagerDocument;
        protected HtmlInputFile HtmlInputFileAttach;

        private CmsDocument cachedDocument;

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

        public CmsDocument CurrentDocument {
            get {
                if (cachedDocument == null && FileID != Guid.Empty) {
                    cachedDocument = CmsDocument.FindByID(FileID);
                }
                return cachedDocument;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {
                CurrentDocument.UploadDocument(HtmlInputFileAttach.PostedFile);
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "The new file has been uploaded.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
