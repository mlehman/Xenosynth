using System;
using System.Web;
using System.Configuration;
using System.Security.Principal;

using Xenosynth.Web.UI;
using Xenosynth.Modules.Cms;

namespace Xenosynth.Web {

	/// <summary>
	/// Summary description for CmsContext.
	/// </summary>
	public class CmsContext {


		private CmsContext() {
		}


		public static CmsContext Current {
			get {
                CmsContext c = CachedInstance;
				if(c == null){
					c = new CmsContext();
                    CachedInstance = c;
				}
				return c;  
			}
		}

        //TODO: Should this be in application cache?
		private static CmsContext CachedInstance {
			get { return (CmsContext)HttpContext.Current.Cache["Xenosynth.Web.CmsContext"];}
			set { HttpContext.Current.Cache["Xenosynth.Web.CmsContext"] = value; }
		}

        //public CmsWebDirectory RootDirectory {
        //    //TODO: Cache for request, yet refresh ok?
        //    get {
        //        if( HttpContext.Current.Items["Xenosynth.RootDirectory"] == null){
        //            string hostHeaderName = HttpContext.Current.Request.Url.Host;
        //            CmsWebDirectory root = CmsHostHeaderMapping.FindCmsDirectory(hostHeaderName);
        //            if(root == null){
        //                throw new ApplicationException("Could not resolve host header.");
        //            }
        //            HttpContext.Current.Items["Xenosynth.RootDirectory"] = root;
        //        }
        //        return (CmsWebDirectory)HttpContext.Current.Items["Xenosynth.RootDirectory"] ;
        //    }
        //}

        public WebSite Site {
            get {
                return HostHeaderMapping.Current.Site;
            }
        }

        public CmsWebDirectory RootDirectory {
            get { //TODO: Best method?
                CmsWebDirectory rootDirectory = (CmsWebDirectory)HttpContext.Current.Items["Xenosynth.RootDirectory"];
                if (rootDirectory == null) {
                    rootDirectory = CmsWebDirectory.FindRootForSite(CmsContext.Current.Site.ID);
                    HttpContext.Current.Items["Xenosynth.RootDirectory"] = rootDirectory;
                }
                return rootDirectory;
            }
        }

		private static void CreateCmsContext(CmsContext cmsContext){
			CachedInstance = cmsContext;
		}

		public string MapPath(string url){

			string applicationPath = HttpContext.Current.Request.ApplicationPath;
			
			if(applicationPath != "/"){
				url = url.Substring(applicationPath.Length);
			}

			return "/" + RootDirectory.FileName + url;
		}

		public string ResolveUrl(string url){

			string resolvedUrl;
            string rootPath = RootDirectory.FullPath;

            if (url.ToLower().StartsWith(rootPath.ToLower())) {
                url = url.Substring(rootPath.Length + 1);
			}

			if(url.ToLower().StartsWith("~")){
				url = url.Substring(2);
			}

			//HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host + "/"

			string applicationPath = HttpContext.Current.Request.ApplicationPath;
			string cmsPath = CmsConfiguration.Current.AdminPath.Replace("/","").Replace("~","");
			
			if(applicationPath.ToLower().IndexOf(cmsPath.ToLower()) > -1){ // in cms virtual dir?
				if(!url.StartsWith("/")){
					url = "/" + url; 
				}
				resolvedUrl = applicationPath + "/.." + url;
			} else { // in site
				if(applicationPath.Length > 1){
					applicationPath += "/";
				}
				resolvedUrl = applicationPath + url;
			}
	
			return resolvedUrl;
		}

		public string ResolveVersionUrl(CmsFile page){
			string url = "/XSViewPage.aspx?PageID=" + page.ID; //TODO: for all files?
			return ResolveUrl(url);
		}

		public bool IsInAuthoringRole(IPrincipal user){
			string[] roles = CmsConfiguration.Current.AuthoringRoles;
			foreach(string role in roles){
				if(user.IsInRole(role)){
					return true;
				}
			}
			return false;						  
		}

		public bool IsInAdminRole(IPrincipal user){
			string[] roles = CmsConfiguration.Current.AdminRoles;
			foreach(string role in roles){
				if(user.IsInRole(role)){
					return true;
				}
			}
			return false;						  
		}
	

	}
}
