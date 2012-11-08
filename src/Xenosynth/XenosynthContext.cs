using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

using Xenosynth.Web;
using Xenosynth.Caching;
using Xenosynth.Configuration;
using Xenosynth.Modules;

namespace Xenosynth {
	
	/// <summary>
	/// Encapsulates all information about the current Xenosyth application. 
	/// </summary>
    public class XenosynthContext {

        private WebSiteCollection webSiteCollection;
        private RegisteredModuleCollection registeredModuleCollection;
        private FileCache fileCache;
		
		/// <summary>
		/// Gets the XenosynthContext object for the current application. 
		/// </summary>
        public static XenosynthContext Current {
            get {
                XenosynthContext c = (XenosynthContext)HttpContext.Current.Cache["Xenosynth.XenosynthContext"]; ;
                if (c == null) {
                    c = new XenosynthContext();
                    HttpContext.Current.Cache["Xenosynth.XenosynthContext"] = c;
                }
                return c;
            }
        }
		
		/// <summary>
		/// Gets the FileCache for the current application. 
		/// </summary>
        public FileCache FileCache {
            get {
                if (fileCache == null) {
                    string configuredFileCache = (string)Configuration["xenosynth.installation.fileCache"].Value;
                    fileCache = new FileCache(configuredFileCache);
                }
                return fileCache;
            }
        }
		
		/// <summary>
		/// Gets the WebSites for the current application. 
		/// </summary>
        public WebSiteCollection WebSites {
            get {
                if (webSiteCollection == null) {
                    webSiteCollection = new WebSiteCollection();
                    foreach (WebSite site in WebSite.FindAll()) {
                        webSiteCollection.Add(site);
                    }
                }
                return webSiteCollection;
            }
            
        }
		
		/// <summary>
		/// Gets the RegisteredModules for the current application. 
		/// </summary>
        public RegisteredModuleCollection Modules {
            get {
                if (registeredModuleCollection == null) {
                    registeredModuleCollection = new RegisteredModuleCollection();
                    foreach (RegisteredModule registeredModule in RegisteredModule.FindAll()) {
                        registeredModuleCollection.Add(registeredModule);
                    }
                }
                return registeredModuleCollection;
            }

        }
		
		/// <summary>
		/// Gets the SystemConfiguration for the current application.
		/// </summary>
        public SystemConfiguration Configuration {
            get { return SystemConfiguration.Current; }
        }
		
		/// <summary>
		/// This method supports the Xenosynth Framework and is not intended to be used directly from your code.  
		/// </summary>
        public void InitializeModules() {
            foreach (RegisteredModule module in Modules) {
                if (module.IsEnabled) {
                    module.Init(HttpContext.Current.ApplicationInstance);
                }
            }
        }
		
		/// <summary>
		/// This method supports the Xenosynth Framework and is not intended to be used directly from your code.  
		/// </summary>
        public void StartModules() {
            foreach (RegisteredModule module in Modules) {
                if (module.IsEnabled) {
                    module.Start();
                }
            }
        }
    }
}
