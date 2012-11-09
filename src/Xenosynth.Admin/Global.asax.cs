using System;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Security.Principal;
using System.Web.Security;

using Inform;

//using Xenosynth.Web;
using Xenosynth.Modules;
using Xenosynth.Configuration;

namespace Xenosynth.Admin {
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication {
		public Global() {
			InitializeComponent();
		}	

		public override void Init() {
			base.Init ();

			//AppDomain.CurrentDomain.AssemblyResolve +=new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            
			//initialize modules
            XenosynthContext.Current.InitializeModules();
		}

		
		protected void Application_Start(Object sender, EventArgs e) {
            
            DataStoreServices.Initialize(false);

            DataStore ds = new Inform.Sql.SqlDataStore();

            ds.Name = "Xenosynth";
            ds.Connection.ConnectionString = XenosynthContext.Current.Configuration.ConnectionString;
            ds.Settings.CreateOnInitialize = false;
            ds.Settings.AutoGenerate = false;
            ds.Settings.UseStoredProcedures = false;
            ds.Settings.FindObjectReturnsNull = true;

            Inform.Common.DataStorageManager m = Inform.Common.DataStorageManager.GetDataStorageManager(ds);

            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Security.LogEntry)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Modules.RegisteredModule)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Security.Permission)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.WebSite)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.HostHeaderMapping)));

            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Admin.Controls.WebParts.RegisteredWebPart)));

            DataStoreServices.RegisterDataStore(ds);

            XenosynthContext.Current.StartModules();
    
		}
 
		protected void Session_Start(Object sender, EventArgs e) {

		}

		protected void Application_BeginRequest(Object sender, EventArgs e) {

		}

		protected void Application_EndRequest(Object sender, EventArgs e) {

		}


		protected void Application_AuthenticateRequest(Object sender, EventArgs e) {

		}

		protected void Application_AuthorizeRequest(Object sender, EventArgs e) {
 
		}

		protected void Application_Error(Object sender, EventArgs e) {

		}

		protected void Session_End(Object sender, EventArgs e) {

		}

		protected void Application_End(Object sender, EventArgs e) {

		}

        private void Application_PreRequestHandlerExecute(object sender, EventArgs e) {
            if (HttpContext.Current.Handler is System.Web.UI.Page) {
                System.Web.UI.Page page = (System.Web.UI.Page)HttpContext.Current.Handler;
                page.PreInit += new EventHandler(Page_PreInit);
            }
        }



        private void Page_PreInit(object sender, EventArgs e) {

            System.Web.UI.Page page = (System.Web.UI.Page)HttpContext.Current.Handler;
            String theme = (string)XenosynthContext.Current.Configuration.GetValue("xenosynth.preferences.theme", false);
            if (theme != null) {
                page.Theme = theme;
            }
        }
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {    
		}
		#endregion

//		private System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args) {
//			throw new ApplicationException("Looking for:" + args.Name);
//		}
	}
}

