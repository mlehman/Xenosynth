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

using Fluent.DataBinding;
using Xenosynth.Web;
using Xenosynth.Web.UI;
using Xenosynth.Admin.Controls;
using Xenosynth.Modules.Cms;


namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for EditPage.
	/// </summary>
	public partial class EditPage : System.Web.UI.Page {

		protected MessageBox MessageBox1;

        CmsPage pageCache;

        public Guid PageID {
            get { return new Guid(Request["FileID"]); }
        }

        public CmsPage CurrentPage {
            get {
                if (pageCache == null) {
                    pageCache = CmsPage.FindByID(PageID);
                }
                return pageCache;
            }
        }

		protected string AllowedPageExtensionsMessage {
			get{
				string extensions = string.Join(",",CmsConfiguration.Current.AllowedPageExtensions);
				return string.Format("Please use a page extension ( {0} )", extensions);
			}
		}

		protected string AllowedPageExtensionsRegex {
			get {
				return CmsConfiguration.Current.AllowedPageNameRegex;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){

				RegularExpressionValidatorFileName.ValidationExpression = AllowedPageExtensionsRegex;
				RegularExpressionValidatorFileName.ErrorMessage = AllowedPageExtensionsMessage;

				DataBindingManagerPage.DataSource = CurrentPage;
				DataBindingManagerPage.PushData();
			}
		}

		

		protected void ButtonUpdatePage_OnClick(object sender, EventArgs e){
            if (Page.IsValid) {
                CmsPage p = CurrentPage;
                DataBindingManagerPage.DataSource = p;
                DataBindingManagerPage.PullData();

                p.Update();

                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Page has been updated.");
            }
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			CmsPage p = CurrentPage;
			DataBindingManagerPage.DataSource = CurrentPage;
			DataBindingManagerPage.PushData();
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
