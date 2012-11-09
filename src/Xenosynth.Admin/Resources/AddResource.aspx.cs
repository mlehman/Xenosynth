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

namespace Xenosynth.Admin.Resources {
	/// <summary>
	/// Summary description for AddResouce.
	/// </summary>
	public partial class AddResource : System.Web.UI.Page {

		protected void Page_Load(object sender, System.EventArgs e) {
			// Put user code to initialize the page here
		}

		protected void ButtonAddResource_OnClick(object sender, EventArgs e){
			CmsResource r = new CmsResource();
			DataBindingManagerResource.DataSource = r;
			DataBindingManagerResource.PullData();
			r.Save();
			Response.Redirect("Default.aspx");
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			Response.Redirect("Default.aspx");
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
