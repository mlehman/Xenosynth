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

namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for AddDirectory.
	/// </summary>
	public partial class AddDirectory : System.Web.UI.Page {

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

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){

				DropDownListTemplateGalleries.DataSource = CmsTemplateGallery.FindAll();
				DropDownListTemplateGalleries.DataTextField = "FullPath";
				DropDownListTemplateGalleries.DataValueField = "ID";
				DropDownListTemplateGalleries.DataBind();
				DropDownListTemplateGalleries.Items.Insert(0, new ListItem("-- all --",""));

                DropDownListImageGalleries.DataSource = CmsImageGallery.FindAll();
                DropDownListImageGalleries.DataTextField = "FullPath";
                DropDownListImageGalleries.DataValueField = "ID";
                DropDownListImageGalleries.DataBind();
                DropDownListImageGalleries.Items.Insert(0, new ListItem("-- all --", ""));
			}
		}

		protected void ButtonAddDirectory_OnClick(object sender, EventArgs e){
			CmsWebDirectory d = new CmsWebDirectory();
			DataBindingManagerDirectory.DataSource = d;
			DataBindingManagerDirectory.PullData();

			d.ParentID = DirectoryID;

			CmsWebDirectory pd = CmsWebDirectory.FindByID(DirectoryID);
			d.SortOrder = pd.Files.Count;

			d.Insert();

            Response.Redirect(d.FileType.EditUrl + "?FileID=" + d.ID);
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
