using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Xenosynth.Admin.Controls;
using Xenosynth.Web.UI;
using Xenosynth.Web;
using Xenosynth.Data;

namespace Xenosynth.Modules.Cms.Admin.Page {

    public partial class EditContentType : System.Web.UI.Page {

        private CmsPage pageCache;

        public Guid PageID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsPage CurrentPage {
            get {
                if (pageCache == null) {
                    pageCache = CmsPage.FindByID(PageID);
                }
                return pageCache;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                CmsWebDirectory d = (CmsWebDirectory)CurrentPage.ParentDirectory;
                if (d.HasTemplateGallery) {
                    CmsTemplateGallery tg = CmsTemplateGallery.FindByID(d.TemplateGalleryID);
                    DropDownListTemplates.DataSource = tg.Templates;
                } else {
                    DropDownListTemplates.DataSource = CmsTemplate.FindAll();
                }

                DropDownListTemplates.DataTextField = "Title";
                DropDownListTemplates.DataValueField = "ID";
                DropDownListTemplates.DataBind();

                ListItem i = DropDownListTemplates.Items.FindByValue(CurrentPage.TemplateID.ToString());
                if (i != null) {
                    i.Selected = true;
                }

                if (CmsContext.Current.IsInAdminRole(Page.User)) {
                    DropDownListTemplates.Items.Insert(0, new ListItem("Static Page", Guid.Empty.ToString()));
                }
                BindContent();

                DropDownListContentType.DataSource = CmsConfiguration.Current.RegisteredContentTypes;
                DropDownListContentType.DataTextField = "FullName";
                DropDownListContentType.DataValueField = "AssemblyQualifiedName";
                DropDownListContentType.DataBind();
            }
        }

        protected void BindContent() {
            DataGridContent.DataSource = CurrentPage.ContentBlocks;
            DataGridContent.DataBind();
        }

        protected void ButtonUpdatePage_OnClick(object sender, EventArgs e) {
            CmsPage p = CurrentPage;
            p.TemplateID = new Guid(DropDownListTemplates.SelectedValue);
            p.Update();

            BindContent();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Page template has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            CmsPage p = CurrentPage;
            DropDownListTemplates.ClearSelection();
            ListItem i = DropDownListTemplates.Items.FindByValue(CurrentPage.TemplateID.ToString());
            if (i != null) {
                i.Selected = true;
            }
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

        protected void ButtonAddContent_OnClick(object sender, EventArgs e) {
            string typeName = DropDownListContentType.SelectedValue;
            Type t = Type.GetType(typeName, true);
            IContentPersister ic = (IContentPersister)t.GetConstructor(Type.EmptyTypes).Invoke(null);
            ic.CreateInitialContent(TextBoxControlID.Text, CurrentPage.ID);
            BindContent();
        }

        //		protected void CreateContent_OnClick(object sender, EventArgs e){
        //			CmsPage p = CurrentPage;
        //			DropDownListTemplates.ClearSelection();
        //			ListItem i = DropDownListTemplates.Items.FindByValue(CurrentPage.TemplateID.ToString());
        //			if(i != null){
        //				i.Selected = true;
        //			}
        //			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        //		}

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
