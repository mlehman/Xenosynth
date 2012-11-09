namespace Xenosynth.Admin.Content {
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    using Xenosynth.Web.UI;
    using Xenosynth.Modules.Cms.Admin;

    /// <summary>
    ///		Summary description for PageTasks.
    /// </summary>
    public partial class PageTasks : System.Web.UI.UserControl {

        private CmsPage pageCache;

        public CmsPage CurrentPage {
            get {
                if (pageCache == null) {
                    if (ViewState["ID"] != null) {
                        pageCache = CmsPage.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return pageCache;
            }
            set {
                pageCache = value;
                ViewState["ID"] = pageCache.ID;
            }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                if (this.Visible) {
                    DataBind();
                }
            }
        }

        protected void Publish_OnClick(object sender, System.EventArgs e) {
            CurrentPage.Publish();
            Response.Redirect(CurrentPage.FileType.EditUrl + "?FileID=" + CurrentPage.ID);
        }

        protected void Unpublish_OnClick(object sender, System.EventArgs e) {
            CurrentPage.Unpublish();
            Response.Redirect(CurrentPage.FileType.EditUrl + "?FileID=" + CurrentPage.ID);
        }

        protected void Archive_OnClick(object sender, System.EventArgs e) {
            CurrentPage.Archive();
            Response.Redirect(CurrentPage.FileType.EditUrl + "?FileID=" + CurrentPage.ID);
        }

        protected void Delete_OnClick(object sender, System.EventArgs e) {
            CurrentPage.Delete();
            Response.Redirect(CurrentPage.ParentDirectory.FileType.BrowseUrl + "?FileID=" + CurrentPage.ParentDirectory.ID);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
        }
        #endregion
    }
}
