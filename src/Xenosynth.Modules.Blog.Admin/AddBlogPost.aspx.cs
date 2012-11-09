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
using Telerik.WebControls;

namespace Xenosynth.Modules.Blog.Admin {
    public partial class AddBlogPost : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerBlogPost;
        protected RadEditor TextBoxText;

        private Blog blogCache;

        public Blog CurrentBlog {
            get {
                if (blogCache == null) {
                    blogCache = (Blog)CmsFile.FindByID(BlogID);
                }
                return blogCache;
            }
        }

        public Guid BlogID {
            get { return new Guid(Request["FileID"]); }
        }

        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                IList galleries = CmsImageGallery.FindAllRoot();
                ArrayList paths = new ArrayList();
                foreach (CmsImageGallery g in galleries) {
                    paths.Add(g.FullPath);
                }
                TextBoxText.ImagesPaths = (string[])paths.ToArray(typeof(string));
                TextBoxText.UploadImagesPaths = (string[])paths.ToArray(typeof(string));
            }
        }

        protected void ButtonAdd_OnClick(object sender, EventArgs e) {
            BlogPost bp = new BlogPost();
            DataBindingManagerBlogPost.DataSource = bp;
            DataBindingManagerBlogPost.PullData();

            bp.ParentID = CurrentBlog.FileID;
            bp.SortOrder = CurrentBlog.Files.Count;

            bp.Insert();

            Response.Redirect(bp.FileType.EditUrl + "?FileID=" + bp.ID);
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            Response.Redirect(CurrentBlog.FileType.BrowseUrl + "?FileID=" + CurrentBlog.ID);
        }
    }
}
