using System;
using System.Collections;
using System.Text;
using Xenosynth.Web.UI;
using System.Web;

namespace Xenosynth.Modules.Cms.User {
	
	
	/// <summary>
	/// Tracks the recent files used by an authenticated user. 
	/// </summary>
    public class RecentFiles {

        private static int Limit = 5;
		
		/// <summary>
		/// A list of recent CmsFiles accessed by the current user. 
		/// </summary>
        public static ArrayList Files {
            get {
                ArrayList cachedRecentFiles = (ArrayList)HttpContext.Current.Session["RecentFiles"];
                if (cachedRecentFiles == null) {
                    cachedRecentFiles = new ArrayList();
                    HttpContext.Current.Session["RecentFiles"] = cachedRecentFiles;
                }
                return cachedRecentFiles;
            }
        }
		
		/// <summary>
		/// Adds a CmsFile into the recent files.  Moves the file up the list if already in the list. 
		/// </summary>
		/// <param name="file">
		/// A <see cref="CmsFile"/>
		/// </param>
        public static void AddFile(CmsFile file) {

            if (file == null) {
                return;
            }

            ArrayList files = Files;

            CmsFileInfo r = null;
            foreach (CmsFileInfo rf in files) {
                if (rf.ID == file.ID) {
                    r = rf;
                }
            }
            if (r != null) {
                files.Remove(r);
            }

            files.Insert(0, new CmsFileInfo(file));

            if (files.Count > Limit) {
                files.RemoveRange(Limit, files.Count - Limit);
            }
        }


    }
}
