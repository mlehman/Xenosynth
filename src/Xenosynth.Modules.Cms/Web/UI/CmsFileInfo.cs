using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// Summary information for a CmsFile. 
	/// </summary>
    class CmsFileInfo {

        private Guid id;
        private string title;
        private CmsFileType fileType;
        private string url;

        public CmsFileInfo(CmsFile file) {
            this.id = file.ID;
            this.title = file.Title;
            this.fileType = file.FileType;
            this.url = file.DefaultActionUrl;
        }
		
		/// <summary>
		/// The unique identifier for the CmsFile. 
		/// </summary>
        public Guid ID {
            get { return id; }
        }
		
		/// <summary>
		/// The title of the CmsFile. 
		/// </summary>
        public string Title {
            get { return title; }
        }
		
		/// <summary>
		/// Gets an instance of the CmsFile. 
		/// </summary>
        public CmsFile File {
            get { return CmsFile.FindByID(id); }
        }
		
		
		/// <summary>
		/// The file type for this CmsFile. 
		/// </summary>
        public CmsFileType FileType {
            get { return fileType; }
        }
		
		/// <summary>
		/// The Url for the CmsFile. 
		/// </summary>
        public string Url {
            get { return url; }
        }
    }
}

