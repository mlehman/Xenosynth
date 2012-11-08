using System;
using Inform;
using Xenosynth.Web.UI;

namespace Xenosynth.Web {
	
	/// <summary>
	/// A SearchResult for CmsFile in a CmsDirectory search.
	/// </summary>
	public class SearchResult {

		[MemberMapping(ColumnName="Title")]
		private string displayName;

		[MemberMapping(ColumnName="FullPath")]
		private string fullPath;

        [MemberMapping(ColumnName = "VersionID")]
        private Guid versionID;

		[MemberMapping(ColumnName="Rank")]
		private int rank;

        private CmsFile fileCache;

		private SearchResult() {
		}

		/// <summary>
		/// The title of the CmsFile. 
		/// </summary>
		public string DisplayName {
			get{ return displayName; }
		}
		
		/// <summary>
		/// The full path of the CmsFile. 
		/// </summary>
		public string FullPath {
			get{ return fullPath; }
		}
		
		/// <summary>
		/// The Url for the CmsFile. 
		/// </summary>
		public string Url {
			get { return CmsContext.Current.ResolveUrl(fullPath); }
		}
		
		/// <summary>
		/// Gets an instance of the actual CmsFile. 
		/// </summary>
        public CmsFile File {
            get {
                if (fileCache == null) {
                    fileCache = CmsFile.FindByID(versionID);
                }
                return fileCache;
            }
        }
		
		/// <summary>
		/// The search rank. 
		/// </summary>
		public int Rank {
			get{ return rank; }
		}

	}
}
