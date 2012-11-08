using System;
using System.IO;
using System.Web;

using Xenosynth.Web.UI;

namespace Xenosynth.Web {
	
	/// <summary>
	/// The CmsHttpContext provides quick access to resources and methods for the context of a current http request.
	/// </summary>
	public class CmsHttpContext {

		private CmsFile cmsFile;

		private CmsHttpContext() {
		}
		
		/// <summary>
		/// Returns the CmsFile that matches the URL of the current request.  If no CmsFile matches the URL, it returns null. 
		/// </summary>
		public CmsFile CmsFile {
			get {
                if (cmsFile == null) {

					string cmsPath = CmsContext.Current.MapPath(FilePath);

					if(cmsPath.IndexOf("XSViewPage") > -1){ //TODO: extend for all file types
						if(HttpContext.Current.User.Identity.IsAuthenticated && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)){
                            cmsFile = CmsFile.FindByID(new Guid(HttpContext.Current.Request["PageID"]));
						}
					} else {
                        cmsFile = CmsFile.FindByFullPath(cmsPath);
					}
				}
                return cmsFile;
			}
		}

        internal void TransferRequest(CmsFile cmsFile) {
            this.cmsFile = cmsFile;
            this.cmsFile.GetHandler(HttpContext.Current.Request.CurrentExecutionFilePath + cmsFile.FileName).ProcessRequest(HttpContext.Current);
        }
		
		/// <summary>
		/// Whether the current request contains a dynamic path.  A dynamic path is passed as a parameter to a request handler.
		/// </summary>
        public bool IsDynamicPath {
            get {
                string filePath = HttpContext.Current.Request.FilePath;
                return filePath.Contains("/_");
            }
        }
		
		/// <summary>
		/// If the current request path is dynamic, this is the parameterized portion of the path. 
		/// </summary>
        public string DynamicPath {
            get {
                string filePath = HttpContext.Current.Request.FilePath;
                return filePath.Substring(filePath.IndexOf("/_"));
            }
        }
		
		/// <summary>
		/// The file path. If the request is dynamic, this is the file portion of the path. 
		/// </summary>
        public string FilePath{
            get {
                if (IsDynamicPath) {
                    string filePath = HttpContext.Current.Request.FilePath;
                    return filePath.Substring(0, filePath.IndexOf("/_"));
                } else {
                    return HttpContext.Current.Request.FilePath;
                }
            }    
        }

        /// <summary>
        /// This method will be deprecated.  Returns the current CmsFile casted to a CmsPage.
        /// </summary>
        public CmsPage CmsPage {
            get { return this.CmsFile as CmsPage; }
        }
		
		/// <summary>
		/// The CmsMode of the current http request.
		/// </summary>
		public CmsMode Mode {
			get { return _CurrentMode; }
			set { _CurrentMode = value;	}
		}
		
		
		/// <summary>
		/// The CmsHttpContext for the current http request. 
		/// </summary>
		public static CmsHttpContext Current {
			get {
				if( _CurrentContext == null){
					_CurrentContext = new CmsHttpContext();
				}
				return _CurrentContext;
			}
		}

		private static CmsMode _CurrentMode {
			get {
                if(HttpContext.Current.Items["Xenosynth.Web.CmsMode"] != null){
                    return (CmsMode)HttpContext.Current.Items["Xenosynth.Web.CmsMode"];
                }

                if (HttpContext.Current.Request.Cookies["Xenosynth.Web.CmsMode"] == null
                    || HttpContext.Current.User == null 
                    || !HttpContext.Current.User.Identity.IsAuthenticated) {
					return CmsMode.Published;
				}

                return (CmsMode)Enum.Parse(typeof(CmsMode), HttpContext.Current.Request.Cookies["Xenosynth.Web.CmsMode"].Value);
			}
			set {
                HttpContext.Current.Items["Xenosynth.Web.CmsMode"] = value;
				HttpContext.Current.Response.Cookies["Xenosynth.Web.CmsMode"].Value = value.ToString(); 
			}
		}



		private static CmsHttpContext _CurrentContext {
			get { return (CmsHttpContext)HttpContext.Current.Items["Xenosynth.Web.CmsHttpContext"];}
			set { HttpContext.Current.Items["Xenosynth.Web.CmsHttpContext"] = value; }
		}

		/// <summary>
		/// The lowest state of a CmsFile allowed based on the current request's mode. 
		/// </summary>
        public CmsState SearchScopeLowerBound {
            get {
                switch (Mode) {
                    case CmsMode.Edit:
                        return  CmsState.Unpublished;
                    case CmsMode.Unpublished:
                        return CmsState.Unpublished;
                    case CmsMode.Published:
                        return CmsState.Published;
                    default:
                        throw new ApplicationException("Unknown CmsMode");
                }
            }
        }
		
		/// <summary>
		/// The highest state of a CmsFile allowed based on the current request's mode.  
		/// </summary>
        public CmsState SearchScopeUpperBound {
            get {
                switch (Mode) {
                    case CmsMode.Edit:
                        return CmsState.Archived;
                    case CmsMode.Unpublished:
                        return CmsState.Archived;
                    case CmsMode.Published:
                        return  CmsState.Archived;
                    default:
                        throw new ApplicationException("Unknown CmsMode");
                }
            }
        }

       
	}
}
