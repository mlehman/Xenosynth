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
using Xenosynth.Web;
using Xenosynth.Modules.Cms;

namespace Xenosynth.Admin.Content.TemplateGallery {
    public partial class EditTemplateContentBlocks : System.Web.UI.Page {

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

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                BindContent();

                DropDownListContentType.DataSource = CmsConfiguration.Current.RegisteredContentTypes;
                DropDownListContentType.DataTextField = "FullName";
                DropDownListContentType.DataValueField = "AssemblyQualifiedName";
                DropDownListContentType.DataBind();
            }
        }


        private void BindContent() {
            CmsTemplate t = CurrentTemplate;
            DataGridRegisteredContent.DataSource = t.RegisteredContent;
            DataGridRegisteredContent.DataBind();
        }

        protected void ButtonRegisterContent_OnClick(object sender, EventArgs e) {
            CmsRegisteredContent rc = new CmsRegisteredContent();
            DataBindingRegisteredContent.DataSource = rc;
            DataBindingRegisteredContent.PullData();
            rc.TemplateID = FileID;
            rc.Register();

            BindContent();
            DataBindingRegisteredContent.Reset();
        }

        protected void ButtonCancelContent_OnClick(object sender, EventArgs e) {
            DataBindingRegisteredContent.Reset();
        }

        protected void DataGridRegisteredContent_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            CmsRegisteredContent rc = CmsRegisteredContent.FindByID((Guid)DataGridRegisteredContent.DataKeys[e.Item.ItemIndex]);
            rc.Unregister();
            BindContent();
        }
    }
}
