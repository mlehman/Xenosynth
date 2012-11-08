using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Security.Principal;
using System.Web.Security;
using System.Web.Hosting;

using Inform;
using Xenosynth.Data;
using Xenosynth.Web.UI;
using Xenosynth.Modules;

namespace Xenosynth.Web {

	/// <summary>
	/// The CmsHttpApplication provides a subclass of the HttpApplication need to support the functionality of the Xenosynth CMS Module. 
	/// <remarks> It is required for the front-end website to use this class in place of the standard HttpApplication to support the CMS initialization and caching framework. </remarks> 
	/// </summary>
	public class CmsHttpApplication : HttpApplication {

        FileRequestCache fileCache = new FileRequestCache();

		public CmsHttpApplication() {
			//TODO: Remove and use wire up?
			this.BeginRequest += new EventHandler(CmsHttpApplication_BeginRequest);
			this.ResolveRequestCache += new EventHandler(CmsHttpApplication_ResolveRequestCache);
			this.UpdateRequestCache += new EventHandler(CmsHttpApplication_UpdateRequestCache);
		}
		
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		public override void Init() {
			base.Init ();

            //HostingEnvironment.RegisterVirtualPathProvider(new CmsVirtualPathProvider());

            InitModules();
           
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
        public void InitModules() {
            XenosynthContext.Current.InitializeModules();
        }


		protected void Application_Start(Object sender, EventArgs e) {
			Initialize();
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e) {
				//Authentication.AuthenticateRequest();
		}

		
		/// <summary>
		/// Provides a set of custom strings for page caching. 
		/// <remarks>
		/// page - time stamp of last update of this page
		/// subdirectories - time stamp of last update of any file recursively
		/// site - time stamp of last update of any file on site
		/// </remarks>
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
		/// <param name="custom">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public override string GetVaryByCustomString(HttpContext context, string custom) {
			if(custom.ToLower() == "page"){
				return CmsPage.Current.FullPath + CmsPage.Current.DateModified.Ticks.ToString();
			} else if(custom.ToLower() == "directory"){
				CmsWebDirectory d = CmsWebDirectory.FindByID(CmsPage.Current.ParentID);
				return d.FullPath + d.FindLastFileUpdated(false).Ticks.ToString();
			} else if(custom.ToLower() == "subdirectories"){
				CmsWebDirectory d = CmsWebDirectory.FindByID(CmsPage.Current.ParentID);
				return d.FullPath + d.FindLastFileUpdated(true).Ticks.ToString();
            } else if (custom.ToLower() == "site") {
                CmsWebDirectory d = CmsContext.Current.RootDirectory;
                return d.FullPath + d.FindLastFileUpdated(true).Ticks.ToString();
            } else {
				return base.GetVaryByCustomString (context, custom);
			}
		}


		private void CmsHttpApplication_BeginRequest(object sender, EventArgs e) {
			Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(Validate), null);	
		}

        /*

        public void ReWritePath(HttpContext context, string filepath) {

            if (!File.Exists(filepath)) {

                CmsFile f = CmsHttpContext.Current.CmsFile;
                if (f != null) {
                    if (f is CmsPage) { //TODO: Extend for all file types
                        CmsPage p = (CmsPage)f;
                        context.RewritePath(ResolveTemplate(p.Template.Url), false);
                    }

                }
            }

        }

        public void PreventCachingInAuthoringMode(HttpContext context) {
            if (context.User.Identity.IsAuthenticated) {
                context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            }
        }


        public string ResolveTemplate(string url) {
            if (url.StartsWith("/")) {
                url = url.Replace(CmsContext.Current.Site.RootDirectory.FullPath, "~");
            }
            return url;
        }

        */
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
		/// <param name="data">
		/// A <see cref="Object"/>
		/// </param>
		/// <param name="status">
		/// A <see cref="HttpValidationStatus"/>
		/// </param>
		public void Validate(HttpContext context, Object data, ref HttpValidationStatus status) {
            if (context.User != null && context.User.Identity.IsAuthenticated) {
				status = HttpValidationStatus.IgnoreThisRequest;
			} else {
				status = HttpValidationStatus.Valid;
			}
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		internal void Initialize(){

            DataStoreServices.Initialize(false);

            DataStore ds = new Inform.Sql.SqlDataStore();

            ds.Name = "Xenosynth";
            ds.Connection.ConnectionString = XenosynthContext.Current.Configuration.ConnectionString;
            ds.Settings.CreateOnInitialize = false;
            ds.Settings.AutoGenerate = false;
            ds.Settings.UseStoredProcedures = false;
            ds.Settings.FindObjectReturnsNull = true;

            Inform.Common.DataStorageManager m = Inform.Common.DataStorageManager.GetDataStorageManager(ds);

            //move into base?
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Security.LogEntry)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Modules.RegisteredModule)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Security.Permission)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.WebSite)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.HostHeaderMapping)));

            DataStoreServices.RegisterDataStore(ds);

            XenosynthContext.Current.StartModules();
		}

		private void CmsHttpApplication_ResolveRequestCache(object sender, EventArgs e) {
			fileCache.ResolveRequestCache();
		}

		private void CmsHttpApplication_UpdateRequestCache(object sender, EventArgs e) {
			fileCache.UpdateRequestCache();
		}
	}

}
