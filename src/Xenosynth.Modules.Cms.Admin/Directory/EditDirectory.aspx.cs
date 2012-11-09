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

namespace Xenosynth.Admin.Content {
	/// <summary>
	/// Summary description for EditDirectory.
	/// </summary>
	public partial class EditDirectory : System.Web.UI.Page {

		protected MessageBox MessageBox1;

		public Guid DirectoryID {
			get { return new Guid(Request["FileID"]); }
		}

		public CmsWebDirectory CurrentDirectory {
			get { return CmsWebDirectory.FindByID(DirectoryID); }
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){

				DropDownListTemplateGalleries.DataSource = CmsTemplateGallery.FindAll();
				DropDownListTemplateGalleries.DataTextField = "Title";
				DropDownListTemplateGalleries.DataValueField = "ID";
				DropDownListTemplateGalleries.DataBind();
				DropDownListTemplateGalleries.Items.Insert(0, new ListItem("-- all --",""));

                DropDownListMediaGalleries.DataSource = CmsImageGallery.FindAll();
				DropDownListMediaGalleries.DataTextField = "FullPath";
				DropDownListMediaGalleries.DataValueField = "ID";
				DropDownListMediaGalleries.DataBind();
				DropDownListMediaGalleries.Items.Insert(0, new ListItem("-- all --",""));

				DataBindingManagerDirectory.DataSource = CurrentDirectory;
				DataBindingManagerDirectory.PushData();
			}
		}

		
		protected void ButtonUpdateDirectory_OnClick(object sender, EventArgs e){
			CmsWebDirectory d = CurrentDirectory;
			DataBindingManagerDirectory.DataSource = d;
			DataBindingManagerDirectory.PullData();

			d.Update();

            d.RefreshDescendentFullPaths();
			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Directory has been updated.");
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			CmsWebDirectory d = CurrentDirectory;
            DataBindingManagerDirectory.DataSource = d;
            DataBindingManagerDirectory.PushData();
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
