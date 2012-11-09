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

namespace Xenosynth.Modules.Blog.Admin {
    public partial class BlogTasks : System.Web.UI.UserControl {

        private Blog blogCache;

        public Blog CurrentBlog {
            get {
                if (blogCache == null) {
                    if (ViewState["ID"] != null) {
                        blogCache = Blog.FindByID((Guid)ViewState["ID"]);
                    }
                }
                return blogCache;
            }
            set {
                blogCache = value;
                ViewState["ID"] = blogCache.ID;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {
                if (this.Visible) {
                    DataBind();
                }
            }
        }
    }
}