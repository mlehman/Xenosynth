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
using Xenosynth.Admin.Controls;
using Xenosynth.Web;

namespace Xenosynth.Admin.Resources {
	/// <summary>
	/// Summary description for EditResource.
	/// </summary>
	public partial class EditResource : System.Web.UI.Page {
		protected MessageBox MessageBox1;

		public Guid ResourceID {
			get { return new Guid(Request["ResourceID"]); }
		}

		public CmsResource CurrentResource {
			get { return CmsResource.FindByID(ResourceID); }
		}

	
		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
				DataBindingManagerResource.DataSource = CurrentResource;
				DataBindingManagerResource.PushData();
			}
		}

		protected void ButtonUpdateResource_OnClick(object sender, EventArgs e){
			CmsResource r = CurrentResource;
			DataBindingManagerResource.DataSource = r;
			DataBindingManagerResource.PullData();
			r.Update();
			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Resource has been updated.");
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			CmsResource r = CurrentResource;
			DataBindingManagerResource.DataSource = r;
			DataBindingManagerResource.PushData();
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
