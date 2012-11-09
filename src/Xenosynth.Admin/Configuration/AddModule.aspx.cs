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

using Xenosynth.Modules;
using Fluent.DataBinding;
using Xenosynth.Admin.Controls;

namespace Xenosynth.Admin.Configuration {
	/// <summary>
	/// Summary description for AddModule.
	/// </summary>
	public partial class AddModule : System.Web.UI.Page {

		protected MessageBox MessageBox1;

		protected void Page_Load(object sender, System.EventArgs e) {
			// Put user code to initialize the page here
		}

		protected void ButtonAddModule_OnClick(object sender, EventArgs e){
            RegisteredModule rm = new RegisteredModule();
			DataBindingManagerModule.DataSource = rm;
			DataBindingManagerModule.PullData();

			rm.Insert();

            Response.Redirect("Modules.aspx");
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			DataBindingManagerModule.Reset();
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
