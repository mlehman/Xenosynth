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
using Xenosynth.Admin.Controls;
using Fluent.DataBinding;

namespace Xenosynth.Modules.Cms.Admin.DocumentLibrary {
    public partial class EditDocument : System.Web.UI.Page {

        protected MessageBox MessageBox1;
        protected DataBindingManager DataBindingManagerDocument;

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
            if (!IsPostBack) {
                DataBindingManagerDocument.DataSource = CurrentDocument;
                DataBindingManagerDocument.PushData();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {

                CmsDocument d = CurrentDocument;

                DataBindingManagerDocument.DataSource = d;
                DataBindingManagerDocument.PullData();

                d.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Document has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerDocument.DataSource = CurrentDocument;
            DataBindingManagerDocument.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
