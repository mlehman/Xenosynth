using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using Xenosynth.Web;
using Xenosynth.Web.UI;
using Fluent.DataBinding;
using Xenosynth.Admin.Controls;
using Xenosynth.Data;
using Xenosynth.Modules.Cms;

namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for EditPageContent.
	/// </summary>
	public partial class EditPageContent : System.Web.UI.Page {

		protected MessageBox MessageBox1;

		private CmsPage pageCache;

		public Guid PageID {
			get { return new Guid(Request["FileID"]); }
		}

		public CmsPage CurrentPage {
			get { 
				if(pageCache == null){
					pageCache = CmsPage.FindByID(PageID);
				}
				return pageCache;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
                BindContent();

                if (Request["message"] == "new.version") {
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "A new unpublished version has been created.");
                }
			}
		}

		protected void BindContent(){


            DataListContentBlocks.DataSource = CurrentPage.ContentBlocks;
            DataListContentBlocks.DataBind();

		}

        protected void ButtonUpdate_OnClick(object sender, EventArgs e) {
            if (Page.IsValid) {

                string startingID = CurrentPage.ID.ToString();

                CurrentPage.UpdateOrVersion();

                foreach (DataListItem dli in DataListContentBlocks.Items) {
                    string key = (string)DataListContentBlocks.DataKeys[dli.ItemIndex];
                    LiteralContent lc = (LiteralContent)CurrentPage.ContentBlocks[key];

                    Control c = dli.FindControl("ContentBlock");
                    lc.Text = CleanMSFormatting((string)DataBinder.Eval(c, "HTML"));

                    lc.SaveContent(key, CurrentPage.ID);

                }

                CmsFile.LogAuditEvent(CurrentPage, "Updated", "Content Updated");

                if (startingID.Equals(CurrentPage.ID.ToString())) {
                    MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Page has been updated.");
                } else {
                    Response.Redirect("EditPageContent.aspx?FileID=" + CurrentPage.ID + "&message=new.version");
                }
            }
        }

        private string CleanMSFormatting(string s) { 
            if (!string.IsNullOrEmpty(s))
                return s.Replace('\u2018', '\'').Replace('\u2019', '\'').Replace('\u201c', '\"').Replace('\u201d', '\"');
            else
                return s;
        }

        protected void ButtonCancel_OnClick(object sender, EventArgs e) {
            //DataBindingManagerBlogPost.DataSource = CurrentBlogPost;
            //DataBindingManagerBlogPost.PushData();
            MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Your changes have been canceled.");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {    
		}
		#endregion
	}
}
