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
    public partial class BlogTabControl : System.Web.UI.UserControl {

        protected TabControl TabControlBlog;

        public string Selected;
        public Guid FileID = Guid.Empty;

        private void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {

                this.DataBind();

                Tab t = new Tab();
                t.Text = "Properties";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditBlog.aspx?FileID=" + FileID;
                TabControlBlog.Tabs.Add(t);

                t = new Tab();
                t.Text = "Attributes";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "EditBlogAttributes.aspx?FileID=" + FileID;
                TabControlBlog.Tabs.Add(t);

                t = new Tab();
                t.Text = "History";
                t.Enabled = FileID != Guid.Empty;
                t.NavigateUrl = "ViewBlogHistory.aspx?FileID=" + FileID;
                TabControlBlog.Tabs.Add(t);

                TabControlBlog.Tabs.FindByText(Selected).Selected = true;

            }
        }

    }
}