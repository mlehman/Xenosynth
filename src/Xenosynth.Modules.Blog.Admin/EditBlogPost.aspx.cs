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
using Fluent.DataBinding;
using Xenosynth.Web.UI;
using Telerik.WebControls;

namespace Xenosynth.Modules.Blog.Admin {

    public partial class EditBlogPost : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerBlogPost;
        protected MessageBox MessageBox1;
        protected RadEditor TextBoxText;

        private BlogPost cachedBlogPost;

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

        public BlogPost CurrentBlogPost {
            get {
                if (cachedBlogPost == null && FileID != Guid.Empty) {
                    cachedBlogPost = (BlogPost)CmsFile.FindByID(FileID);
                }
                return cachedBlogPost;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                DataBindingManagerBlogPost.DataSource = CurrentBlogPost;
                DataBindingManagerBlogPost.PushData();

                IList galleries = CmsImageGallery.FindAllRoot();
                ArrayList paths = new ArrayList();
                foreach (CmsImageGallery g in galleries) {
                    paths.Add(g.FullPath);
                }
                TextBoxText.ImagesPaths = (string[])paths.ToArray(typeof(string));
                TextBoxText.UploadImagesPaths = (string[])paths.ToArray(typeof(string));
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {

                BlogPost bp = CurrentBlogPost;

                DataBindingManagerBlogPost.DataSource = bp;
                DataBindingManagerBlogPost.PullData();

                bp.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Blog Post has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerBlogPost.DataSource = CurrentBlogPost;
            DataBindingManagerBlogPost.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
