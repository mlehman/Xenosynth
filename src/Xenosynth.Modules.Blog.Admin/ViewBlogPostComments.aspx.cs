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

namespace Xenosynth.Modules.Blog.Admin {
    public partial class ViewBlogPostComments : System.Web.UI.Page {
        protected DataGrid DataGridComments;

        BlogPost fileCache;

        public Guid FileID {
            get { return new Guid(Request["FileID"]); }
        }

        public BlogPost CurrentFile {
            get {
                if (fileCache == null) {
                    fileCache = (BlogPost)CmsFile.FindByID(FileID);
                }
                return fileCache;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            BindComments();
        }

        protected void BindComments() {
            DataGridComments.DataSource = BlogComment.FindAllByPost(FileID);
            DataGridComments.DataBind();
        }

        protected void DataGridComments_OnDeleteCommand(object sender, DataGridCommandEventArgs e) {
            BlogComment c = BlogComment.FindByID((Guid)DataGridComments.DataKeys[e.Item.ItemIndex]);
            c.Delete();
            BindComments();
        }
    }
}
