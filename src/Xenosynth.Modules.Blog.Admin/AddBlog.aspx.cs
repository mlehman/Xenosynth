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
using Fluent.DataBinding;
using Xenosynth.Web.UI;

namespace Xenosynth.Modules.Blog.Admin {
    public partial class AddBlog : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerBlog;
        protected DropDownList DropDownListBlogTemplates;
        protected DropDownList DropDownListBlogPostTemplates;

        private CmsDirectory parentCache;

        public CmsDirectory ParentDirectory {
            get {
                if (parentCache == null) {
                    parentCache = (CmsDirectory)CmsFile.FindByID(DirectoryID);
                }
                return parentCache;
            }
        }

        public Guid DirectoryID {
            get { return new Guid(Request["FileID"]); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                DropDownListBlogTemplates.DataSource = CmsTemplate.FindAll();

                DropDownListBlogTemplates.DataTextField = "Title";
                DropDownListBlogTemplates.DataValueField = "ID";
                DropDownListBlogTemplates.DataBind();


                DropDownListBlogPostTemplates.DataSource = CmsTemplate.FindAll();

                DropDownListBlogPostTemplates.DataTextField = "Title";
                DropDownListBlogPostTemplates.DataValueField = "ID";
                DropDownListBlogPostTemplates.DataBind();
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            Blog b = new Blog();
            DataBindingManagerBlog.DataSource = b;
            DataBindingManagerBlog.PullData();

            b.ParentID = ParentDirectory.FileID;
            b.SortOrder = ParentDirectory.Files.Count;

            b.Insert();

            Response.Redirect(b.FileType.BrowseUrl + "?FileID=" + b.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
        }
    }
}
