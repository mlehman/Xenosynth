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

namespace Xenosynth.Modules.Blog.Admin {
    public partial class EditBlogPostPublishing : System.Web.UI.Page {

        protected DataBindingManager DataBindingManagerBlogPost;
        protected MessageBox MessageBox1;

        BlogPost blogPostCache;

        public Guid BlogPostID {
            get { return new Guid(Request["FileID"]); }
        }

        public BlogPost CurrentBlogPost {
            get {
                if (blogPostCache == null) {
                    blogPostCache = (BlogPost)CmsFile.FindByID(BlogPostID);
                }
                return blogPostCache;
            }
        }


        protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                DataBindingManagerBlogPost.DataSource = CurrentBlogPost;
                DataBindingManagerBlogPost.PushData();
            }
        }



        protected void ButtonUpdatePage_OnClick(object sender, EventArgs e) {
            BlogPost p = CurrentBlogPost;
            DataBindingManagerBlogPost.DataSource = p;
            DataBindingManagerBlogPost.PullData();

            p.DateModified = DateTime.Now;
            p.Update();

            p.UpdatePublishingSchedule();

            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Blog Post has been updated.");
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerBlogPost.DataSource = CurrentBlogPost;
            DataBindingManagerBlogPost.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }

    }
}
