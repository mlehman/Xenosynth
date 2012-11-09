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
	/// Summary description for EditModule.
	/// </summary>
	public partial class EditModule : System.Web.UI.Page {

		protected MessageBox MessageBox1;

		public Guid ModuleID {
			get { return new Guid(Request["ModuleID"]); }
		}

        public RegisteredModule CurrentModule {
            get { return RegisteredModule.FindByID(ModuleID); }
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){

				DataBindingManagerModule.DataSource = CurrentModule;
				DataBindingManagerModule.PushData();
			}
		}

		protected void ButtonUpdateModule_OnClick(object sender, EventArgs e){
            RegisteredModule rm = CurrentModule;
			DataBindingManagerModule.DataSource = rm;
			DataBindingManagerModule.PullData();

			rm.Update();

			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Module has been updated.");
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
