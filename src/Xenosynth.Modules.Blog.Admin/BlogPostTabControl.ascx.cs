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
using Fluent.Navigation;

namespace Xenosynth.Modules.Blog.Admin {
    public partial class BlogPostTabControl : System.Web.UI.UserControl {

        protected TabControl TabControlBlogPost;

        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditBlogPost.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);

                t = new Tab();
                t.Text = "Publishing";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditBlogPostPublishing.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);
                
                t = new Tab();
                t.Text = "Comments";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewBlogPostComments.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditBlogPostAttributes.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);

                t = new Tab();
                t.Text = "Revisions";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewBlogPostRevisions.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewBlogPostHistory.aspx?FileID=" + FileID;
                TabControlBlogPost.Tabs.Add(t);

                TabControlBlogPost.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}