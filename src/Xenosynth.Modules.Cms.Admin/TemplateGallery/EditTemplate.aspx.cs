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
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Content.TemplateGallery {
    public partial class EditTemplate : System.Web.UI.Page {

        private CmsTemplate cachedTemplate;

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

        public CmsTemplate CurrentTemplate {
            get {
                if (cachedTemplate == null && FileID != Guid.Empty) {
                    cachedTemplate = CmsTemplate.FindByID(FileID);
                }
                return cachedTemplate;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataBindingManagerTemplate.DataSource = CurrentTemplate;
                DataBindingManagerTemplate.PushData();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {

                CmsTemplate t = CurrentTemplate;

                DataBindingManagerTemplate.DataSource = t;
                DataBindingManagerTemplate.PullData();

                t.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Template has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerTemplate.DataSource = CurrentTemplate;
            DataBindingManagerTemplate.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
