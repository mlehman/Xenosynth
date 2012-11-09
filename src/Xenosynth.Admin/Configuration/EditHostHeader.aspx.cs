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

namespace Xenosynth.Admin.Configuration {
	/// <summary>
	/// Summary description for EditHostHeader.
	/// </summary>
	public partial class EditHostHeader : System.Web.UI.Page {

		protected MessageBox MessageBox1;

		public Guid HostHeaderID {
			get { return new Guid(Request["HostHeaderID"]); }
		}

		public CmsHostHeaderMapping CurrentHostHeaderMapping {
			get { return CmsHostHeaderMapping.FindByID(HostHeaderID); }
		}

		protected void Page_Load(object sender, System.EventArgs e) {
			if(!IsPostBack){
				DropDownListDirectories.DataSource = CmsWebDirectory.FindAll();
				DropDownListDirectories.DataTextField = "FullPath";
				DropDownListDirectories.DataValueField = "ID";
				DropDownListDirectories.DataBind();

				DataBindingManagerHostHeader.DataSource = CurrentHostHeaderMapping;
				DataBindingManagerHostHeader.PushData();
			}
		}

		protected void ButtonUpdateHostHeader_OnClick(object sender, EventArgs e){
			CmsHostHeaderMapping h = CurrentHostHeaderMapping;
			DataBindingManagerHostHeader.DataSource = h;
			DataBindingManagerHostHeader.PullData();

			h.Update();

			MessageBox1.ShowMessage(MessageBox.MessageBoxMode.Info, "Host Header Mapping has been updated.");
		}

		protected void ButtonCancel_OnClick(object sender, EventArgs e){
			CmsHostHeaderMapping h = CurrentHostHeaderMapping;
			DataBindingManagerHostHeader.DataSource = h;
			DataBindingManagerHostHeader.PushData();
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
