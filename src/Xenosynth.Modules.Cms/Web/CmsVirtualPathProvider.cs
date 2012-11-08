using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web.Hosting;

using Xenosynth.Web.UI;
using System.Web.Caching;
using System.Web;

namespace Xenosynth.Web {
	
	/// <summary>
	/// A virtual path provider for CmsFiles. This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
	/// </summary>
    public class CmsVirtualPathProvider : VirtualPathProvider {

        Hashtable virtualFiles = null;

        private bool IsPathVirtual(string virtualPath) {

            if (IsDynamicPath(virtualPath)) {
                return true;
            } else {
                //TODO: Faster method?
                if (CmsContext.Current != null) {
                    CmsFile file = CmsFile.FindByFullPath(CmsContext.Current.MapPath(virtualPath));
                    if (file != null) {
                        return file.IsVirtual;
                    }
                }
            }

            return false;

        }

        private static bool IsDynamicPath(string virtualPath) {
            return virtualPath.Contains("/_");
        }

        private static string GetDynamicPath(string virtualPath) {
            return virtualPath.Substring(virtualPath.IndexOf("/_"));
        }


        public override bool FileExists(string virtualPath) {
            if (IsPathVirtual(virtualPath)) {
                if (IsDynamicPath(virtualPath)) {
                    return true;
                } else {
                    CmsFile file = CmsFile.FindByFullPath(CmsContext.Current.MapPath(virtualPath));
                    if (file != null) {
                        return true;
                    } else {
                        return Previous.FileExists(virtualPath);
                    }
                }

            } else
                return Previous.FileExists(virtualPath);
        }

        public override bool DirectoryExists(string virtualDir) {
            if (IsPathVirtual(virtualDir)) {
                return true;
            } else
                return Previous.DirectoryExists(virtualDir);
        }


        public override VirtualFile GetFile(string virtualPath) {
            if (IsPathVirtual(virtualPath))
                return new CmsVirtualFile(virtualPath, this);
            else
                return Previous.GetFile(virtualPath);
        }

        public override VirtualDirectory GetDirectory(string virtualDir) {
            if (IsPathVirtual(virtualDir))
                return new CmsVirtualDirectory(virtualDir, this);
            else
                return Previous.GetDirectory(virtualDir);
        }

        public Stream Open(string virtualPath) {

            

            if (IsDynamicPath(virtualPath)) {

                //TODO: Pull out into Image

                string filePath = virtualPath.Substring(0, virtualPath.IndexOf("/_"));
                CmsFile file = (CmsFile)CmsFile.FindByFullPath(CmsContext.Current.MapPath(filePath));
                return file.Open(GetDynamicPath(virtualPath));

            } else {
                CmsFile file = (CmsFile)CmsFile.FindByFullPath(CmsContext.Current.MapPath(virtualPath));

                HttpContext.Current.Response.AppendHeader("Last-Modified", file.DateModified.ToString("r"));

                return file.Open();
            }
        }

        public Hashtable GetVirtualData {
            get { return this.virtualFiles; }
            set { this.virtualFiles = value; }
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart) {
            return null;
        }

        //public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart) {
           
        //    if (IsPathVirtual(virtualPath)) {
        //        //System.Collections.Specialized.StringCollection fullPathDependencies = null;

        //        //// Get the full path to all dependencies.
        //        //foreach (string virtualDependency in virtualPathDependencies) {
        //        //    if (fullPathDependencies == null)
        //        //        fullPathDependencies = new System.Collections.Specialized.StringCollection();

        //        //    fullPathDependencies.Add(virtualDependency);
        //        //}
        //        //if (fullPathDependencies == null)
        //        //    return null;

        //        //// Copy the list of full-path dependencies into an array.
        //        //string[] fullPathDependenciesArray = new string[fullPathDependencies.Count];
        //        //fullPathDependencies.CopyTo(fullPathDependenciesArray, 0);
        //        //// Copy the virtual path into an array.
        //        //string[] virtualPathArray = new string[1];
        //        //virtualPathArray[0] = virtualPath;

        //        //return new CacheDependency(virtualPathArray, fullPathDependenciesArray, utcStart);
        //        return new CacheDependency(HttpContext.Current.Server.MapPath("~/Web.config"));
        //    } else
        //        return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        //}
    }



    public class CmsVirtualDirectory : VirtualDirectory {

        CmsVirtualPathProvider vp;
        private ArrayList children = new ArrayList();
        private ArrayList directories = new ArrayList();
        private ArrayList files = new ArrayList();

        public CmsVirtualDirectory(string virtualDir, CmsVirtualPathProvider provider) : base(virtualDir) { vp = provider; }

        public override IEnumerable Children { get { return children; } }
        public override IEnumerable Directories { get { return directories; } }
        public override IEnumerable Files { get { return files; } }


    }




    public class CmsVirtualFile : VirtualFile {

        private CmsVirtualPathProvider vp;
        private string virPath;


        public CmsVirtualFile(string virtualPath, CmsVirtualPathProvider provider)
            : base(virtualPath) {
            this.vp = provider;
            this.virPath = virtualPath;
        }

        public override Stream Open() {
            return vp.Open(virPath);
        }

    }
}
