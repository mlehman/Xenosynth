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
using Xenosynth.Admin.Controls;
using Xenosynth.Web.UI;

namespace Xenosynth.Modules.Blog.Admin {
    public partial class EditBlog : System.Web.UI.Page {

        protected DropDownList DropDownListBlogTemplates;
        protected DropDownList DropDownListBlogPostTemplates;

        protected DataBindingManager DataBindingManagerBlog;
        protected MessageBox MessageBox1;


        private Blog cachedBlog;

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

        public Blog CurrentBlog {
            get {
                if (cachedBlog == null && FileID != Guid.Empty) {
                    cachedBlog = (Blog)CmsFile.FindByID(FileID);
                }
                return cachedBlog;
            }
        }

        protected void Page_Load(object sender, EventArgs e) {
            if (!IsPostBack) {

                DropDownListBlogTemplates.DataSource = CmsTemplate.FindAll();

                DropDownListBlogTemplates.DataTextField = "Title";
                DropDownListBlogTemplates.DataValueField = "ID";
                DropDownListBlogTemplates.DataBind();


                DropDownListBlogPostTemplates.DataSource = CmsTemplate.FindAll();

                DropDownListBlogPostTemplates.DataTextField = "Title";
                DropDownListBlogPostTemplates.DataValueField = "ID";
                DropDownListBlogPostTemplates.DataBind();

                DataBindingManagerBlog.DataSource = CurrentBlog;
                DataBindingManagerBlog.PushData();
            }
        }

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {

                Blog b = CurrentBlog;

                DataBindingManagerBlog.DataSource = b;
                DataBindingManagerBlog.PullData();

                b.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Blog has been updated.");
            }
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            DataBindingManagerBlog.DataSource = CurrentBlog;
            DataBindingManagerBlog.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
        }
    }
}
