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
	/// Summary description for MoveFile.
	/// </summary>
	public partial class MoveFile : System.Web.UI.Page {

		protected MessageBox MessageBox1;
		protected PageTasks PageTasks1;


		public Guid FileID {
			get { return new Guid(Request["FileID"]); }
		}

		public CmsFile CurrentFile {
			get { return CmsFile.FindByID(FileID); }
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

				CmsFile f = CurrentFile;

				if(f is CmsWebDirectory){
					PlaceHolderAction.Visible = false;
				} 

				//TODO: Better method to get, may need to restrict?
				CmsFileCollection files = CmsWebDirectory.FindAll();
				ArrayList allowedDirectories = new ArrayList();
				foreach(CmsFile file in files){
					if(!file.FullPath.StartsWith(f.FullPath)){
						allowedDirectories.Add(file);
					}
				}
				DropDownListDirectories.DataSource = allowedDirectories;
				DropDownListDirectories.DataTextField = "FullPath";
				DropDownListDirectories.DataValueField = "ID";
				DropDownListDirectories.DataBind();

                if (CurrentFile is CmsPage) {
                    RegularExpressionValidatorFileName.ValidationExpression = AllowedPageExtensionsRegex;
                    RegularExpressionValidatorFileName.ErrorMessage = AllowedPageExtensionsMessage;
                    RegularExpressionValidatorFileName.Enabled = true;
                } else {
                    RegularExpressionValidatorFileName.Enabled = false;
                }

				DataBindingManagerFile.DataSource = CurrentFile;
				DataBindingManagerFile.PushData();

			}
		}

		protected void ButtonUpdateFile_OnClick(object sender, EventArgs e){
			if(Page.IsValid){
				CmsFile f = CurrentFile;
				
                //TODO: Verify File can be move/copied

				if(RadioButtonListAction.SelectedValue == "Copy"){
					f.CopyToDirectory(TextBoxFileName.Text, new Guid(DropDownListDirectories.SelectedValue));
					MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "File has been copied.");
				} else {
					f.MoveToDirectory(TextBoxFileName.Text, new Guid(DropDownListDirectories.SelectedValue));
					MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "File has been moved.");
				}

				
			}
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			CmsFile f = CurrentFile;
			DataBindingManagerFile.DataSource = f;
			DataBindingManagerFile.PushData();
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
