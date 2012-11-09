
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
using System.Configuration;

using Fluent.DataBinding;
using Xenosynth.Web;
using Xenosynth.Web.UI;
using Xenosynth.Modules.Cms;

namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for AddPage.
	/// </summary>
	public partial class AddPage : System.Web.UI.Page {

        private CmsWebDirectory parentCache;

        public CmsWebDirectory ParentDirectory {
            get {
                if (parentCache == null) {
                    parentCache = CmsWebDirectory.FindByID(DirectoryID);
                }
                return parentCache;
            }
        }

		public Guid DirectoryID {
			get { return new Guid(Request["FileID"]); }
		}

		protected string AllowedPageExtensionsMessage {
			get{
				string extensions = string.Join(",",CmsConfiguration.Current.AllowedPageExtensions);
				return string.Format("Check file name. Be sure to use a valid page extension ( {0} )", extensions);
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

				CmsWebDirectory d = CmsWebDirectory.FindByID(DirectoryID);
				if(d.HasTemplateGallery){
					CmsTemplateGallery tg = CmsTemplateGallery.FindByID(d.TemplateGalleryID);
					DropDownListTemplates.DataSource = tg.Templates;
				} else {
					DropDownListTemplates.DataSource = CmsTemplate.FindAll();
				}
				
				DropDownListTemplates.DataTextField = "Title";
				DropDownListTemplates.DataValueField = "ID";
				DropDownListTemplates.DataBind();

				//if(CmsContext.Current.IsInAdminRole(Page.User)) {
				//	DropDownListTemplates.Items.Insert(0, new ListItem("Static Page",Guid.Empty.ToString()));
				//}
			}
		}

		protected void ButtonAddPage_OnClick(object sender, EventArgs e){
			if(Page.IsValid){
				CmsPage p = new CmsPage();
				DataBindingManagerPage.DataSource = p;
				DataBindingManagerPage.PullData();

				p.ParentID = DirectoryID;

				if(p.TemplateID == Guid.Empty){
					p.IsStatic = true;
				}

				CmsWebDirectory d = CmsWebDirectory.FindByID(DirectoryID);
				p.SortOrder = d.Files.Count;  //TODO: Pull this out into page!
				p.Insert();

                Response.Redirect(p.FileType.EditUrl + "?FileID=" + p.ID);
			}
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
            Response.Redirect(ParentDirectory.FileType.BrowseUrl + "?FileID=" + ParentDirectory.ID);
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
