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
using Xenosynth.Admin.Controls;
using AjaxControlToolkit;
using Fluent;

namespace Xenosynth.Admin.Content {

	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page {

        protected FileExplorer FileExplorer1;

		private CmsWebDirectory cmsDirectoryCache;
       

		public CmsWebDirectory CurrentDirectory {
			get {
				if(cmsDirectoryCache == null){
					Guid id = (Guid)ViewState["cmsDirectoryID"];
					cmsDirectoryCache = CmsWebDirectory.FindByID(id);
				}
				return cmsDirectoryCache;
			}
			set {
				cmsDirectoryCache = value;
				ViewState["cmsDirectoryID"] = value.ID; 
			}
		}
	
		protected void Page_Load(object sender, System.EventArgs e) {

			if(!IsPostBack){
                if (Request["FileID"] != null && !Guid.Empty.Equals(Request["FileID"])) {
                    CurrentDirectory = CmsWebDirectory.FindByID(Request["FileID"]);
				} else {
					CurrentDirectory = CmsContext.Current.RootDirectory;
				}

                string sortExpression = (string)XenosynthContext.Current.Configuration.GetValue("Xenosynth.Modules.Cms.Directory.SortExpression", false);
                if (sortExpression == null) {
                    sortExpression = "SortOrder";
                }

                DataGridAdapterFiles.SortExpression = sortExpression;
                BindDataGridFiles();
			}
			//Rebind rather than use viewstate
			//BindDataGridFiles();
		}


		private void BindDataGridFiles(){
			DataGridAdapterFiles.BindDataGrid();
		}

		public void DataGridAdapterFiles_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e){
            CmsHttpContext.Current.Mode = CmsMode.Unpublished;
			DataGridAdapterFiles.DataSource = CmsFile.FindByDirectoryID(CurrentDirectory.ID, true);
		}


		protected void DataGridFiles_OnDeleteCommand(object sender, DataGridCommandEventArgs e){
			//TODO: Better handle this error
			try {
				CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[e.Item.ItemIndex]);
				f.Delete();
			} catch (ApplicationException ae) {
				MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
			}
			BindDataGridFiles();
            FileExplorer1.BuildTree();
		}

        protected void ButtonPublishSelected_OnClick(object sender, EventArgs e) {
            //TODO: Better handle this error
            try {
                foreach (DataGridItem di in DataGridFiles.Items) {
                    CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
                    if (CheckBoxSelect.Checked) {
                        CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
                        if (f.State == CmsState.Unpublished) {
                            f.Publish();
                        }
                    }
                }
            } catch (ApplicationException ae) {
                MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
            }
            BindDataGridFiles();
        }

		protected void ButtonDeleteSelected_OnClick(object sender, EventArgs e){
			//TODO: Better handle this error
			try {
				foreach(DataGridItem di in DataGridFiles.Items){
					CheckBox CheckBoxSelect = (CheckBox)di.FindControl("CheckBoxSelect");
					if(CheckBoxSelect.Checked){
						CmsFile f = CmsFile.FindByID((Guid)DataGridFiles.DataKeys[di.ItemIndex]);
						f.Delete();
					}
				}
			} catch (ApplicationException ae) {
				MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Error, ae.Message);
			}
			BindDataGridFiles();
            FileExplorer1.BuildTree();
		}

		protected void DataGridFiles_OnItemCommand(Object sender, DataGridCommandEventArgs e){
			
			switch(e.CommandName){
				case "SortOrderUpdated":
					BindDataGridFiles();
					break;
			}
			
		}

        protected string GetStateDescription(object o) {
            string state = "";

            if (o is CmsPage) {
                CmsPage p = (CmsPage)o;
                state += p.State;
            }

            return state;
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
