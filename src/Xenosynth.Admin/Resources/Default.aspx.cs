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
using Fluent;

namespace Xenosynth.Admin.Resources {
	/// <summary>
	/// Summary description for _Default.
	/// </summary>
	public partial class _Default : System.Web.UI.Page {


		protected void Page_Load(object sender, System.EventArgs e) {
			BindResources();
		}

		public void BindResources(){
			DataGridAdapterResources.BindDataGrid();
		}

		public void DataGridAdapterResources_DataGridBinding(DataGridAdapter sender, DataGridBindingEventArgs e){
			DataGridAdapterResources.DataSource = CmsResource.FindAll();
		}

		public void DataGridResources_OnDeleteCommand(object sender, DataGridCommandEventArgs e){
			
			CmsResource r = CmsResource.FindByID((Guid)DataGridResources.DataKeys[e.Item.ItemIndex]);
			if(r != null){
				r.Delete();
			}
			
			BindResources();

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
