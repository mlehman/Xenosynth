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
using System.Reflection;

using Xenosynth.Modules;

namespace Xenosynth.Admin.Configuration {
	/// <summary>
	/// Summary description for About.
	/// </summary>
	public partial class About : System.Web.UI.Page {
		protected void Page_Load(object sender, System.EventArgs e) {
            if (!IsPostBack) {
                RegisteredModuleVersions.DataSource = RegisteredModule.FindAll();
                RegisteredModuleVersions.DataBind();
            }
		}

		public string VersionInfo(string typeName){
			try {
				Type type = Type.GetType(typeName);
				AssemblyName name = Assembly.GetAssembly(type).GetName();
				return name.Version.ToString();
			} catch {
				return " unavailable";
			}
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
