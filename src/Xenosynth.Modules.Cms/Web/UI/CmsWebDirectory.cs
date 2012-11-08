using System;
using System.Collections;
using Inform;
using System.Data.SqlTypes;
using Xenosynth.Web;
using System.Data;
using Xenosynth.Modules.Cms;
using System.Web;
using System.Web.SessionState;

namespace Xenosynth.Web.UI {

    /// <summary>
    /// A CmsWebDirectory is a general directory type for cms content.
    /// </summary>
    [TypeMapping(BaseType = typeof(CmsDirectory))]
    public class CmsWebDirectory : CmsDirectory, IHttpHandler, IRequiresSessionState {

        [MemberMapping(ColumnName = "TemplateGalleryID", AllowNulls = true)]
        private SqlGuid templateGalleryID;

        [MemberMapping(ColumnName = "ImageGalleryID", AllowNulls = true)]
        private SqlGuid imageGalleryID;

        private static Guid typeID = new Guid("f18a7ba6-99e7-44ce-bee5-aa26888b0a12");

        public CmsWebDirectory()
            : base(typeID) {
        }
		
		/// <summary>
		/// Whether this directory has a restricted set of templates. 
		/// </summary>
        public bool HasTemplateGallery {
            get {
                return !this.templateGalleryID.IsNull;
            }
        }
		
		/// <summary>
		/// The ID of the restricted template gallery. 
		/// </summary>
        public Guid TemplateGalleryID {
            get {
                if (templateGalleryID.IsNull) {
                    return Guid.Empty;
                } else {
                    return templateGalleryID.Value;
                }
            }
            set {
                if (value.Equals(Guid.Empty)) {
                    templateGalleryID = SqlGuid.Null;
                } else {
                    templateGalleryID = new SqlGuid(value);
                }
            }
        }
		
		/// <summary>
		/// Whether this directory has a restricted image gallery. 
		/// </summary>
        public bool HasImageGallery {
            get {
                return !this.imageGalleryID.IsNull;
            }
        }
		
		/// <summary>
		/// The ID of the restricted image gallery. 
		/// </summary>
        public Guid ImageGalleryID {
            get {
                if (imageGalleryID.IsNull) {
                    return Guid.Empty;
                } else {
                    return imageGalleryID.Value;
                }
            }
            set {
                if (value.Equals(Guid.Empty)) {
                    imageGalleryID = SqlGuid.Null;
                } else {
                    imageGalleryID = new SqlGuid(value);
                }
            }
        }


		/// <summary>
		/// Whether this directory has any CmsPages. 
		/// </summary>
        internal bool HasAnyPages {
            get {
                CmsPageCollection pages = CmsPage.FindByDirectoryID(this.ID);
                return pages.Count > 0;
            }
        }

		/// <summary>
		/// Whether this directory has any subdirectories. 
		/// </summary>
        internal bool HasSubdirectories {
            get {
                return Subdirectories.Count > 0;
            }
        }


        /// <summary>
        /// Returns a collection of pages under this directory.
        /// </summary>
        public CmsPageCollection Pages {
            //TODO: Cached?
            get { return CmsPage.FindByDirectoryID(this.ID); }
        }

        /// <summary>
        /// Returns a collection of directories under this directory that are not hidden.
        /// </summary>
        public CmsPageCollection DisplayedPages {
            get { return new CmsPageCollection(FindDisplayed(Pages)); }
        }

		
		/// <summary>
		/// The default file for this directory. 
		/// </summary>
        public override CmsFile DefaultFile {
            get {

                CmsFile f = CmsFile.FindByFullPath(this.fullPath + "/" + CmsConfiguration.Current.DefaultPageName);
                if (f == null && this.HasAnyPages) {
                    f = this.Pages[0];
                }
                return f;
            }
        }
		
		/// <summary>
		/// The default page for this directory. This method may be deprecated. 
		/// </summary>
        public CmsPage DefaultPage {
            //TODO: Expand this logic!
            get {

                CmsPage p = (CmsPage)CmsPage.FindByFullPath(this.fullPath + "/" + CmsConfiguration.Current.DefaultPageName);
                if (p == null && this.HasAnyPages) {
                    p = this.Pages[0];
                }
                return p;
            }

        }

        //public override bool IsVirtual {
        //    get {
        //        return true;
        //    }
        //}
		
		/// <summary>
		/// Finds a CmsWebDirectory by ID. 
		/// </summary>
		/// <param name="id">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsWebDirectory"/>
		/// </returns>
        public new static CmsWebDirectory FindByID(Guid id) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (CmsWebDirectory)ds.FindByPrimaryKey(typeof(CmsWebDirectory), id);
        }
		
		/// <summary>
		/// Find a CmsWebDirectory by ID.
		/// </summary>
		/// <param name="id">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsWebDirectory"/>
		/// </returns>
        public static CmsWebDirectory FindByID(string id) {
            return FindByID(new Guid(id));
        }
		
		/// <summary>
		/// Finds a CmsWebDirectory by the full path from its root.
		/// </summary>
		/// <param name="path">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsWebDirectory"/>
		/// </returns>
        public static CmsWebDirectory FindByFullPath(string path) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsWebDirectory), "WHERE FullPath = @FullPath", true);
            cmd.CreateInputParameter("@FullPath", path.TrimEnd('/'));
            return (CmsWebDirectory)cmd.Execute();
        }


        internal static void RemoveGallery(Guid templateGalleryID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("UPDATE CmsDirectories SET TemplateGalleryID = NULL WHERE TemplateGalleryID = @TemplateGalleryID");
            cmd.CreateInputParameter("@TemplateGalleryID", templateGalleryID);
            cmd.ExecuteNonQuery();
        }

		/// <summary>
		/// Finds all pages in this directory using a specified template. 
		/// </summary>
		/// <param name="template">
		/// A <see cref="CmsTemplate"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public IList FindPagesByTemplate(CmsTemplate template) {
            return FindPagesByTemplate(template, false);
        }
		
		/// <summary>
		/// Finds all pages in this directory, or recursively, using the specified template. 
		/// </summary>
		/// <param name="template">
		/// A <see cref="CmsTemplate"/>
		/// </param>
		/// <param name="recursive">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public IList FindPagesByTemplate(CmsTemplate template, bool recursive) {
            //TODO: Faster search! Recursive?
            ArrayList selectedPages = new ArrayList();

            if (template != null) {
                CmsFileCollection files;
                if (recursive) {
                    files = this.Files;
                } else {
                    files = this.Pages;
                }


                foreach (CmsFile f in files) {
                    if (f is CmsPage) {
                        CmsPage p = (CmsPage)f;
                        if (p.TemplateID == template.ID) {
                            selectedPages.Add(p);
                        }
                    } else if (f is CmsWebDirectory) {
                        CmsWebDirectory d = (CmsWebDirectory)f;
                        selectedPages.AddRange(d.FindPagesByTemplate(template, true));
                    }
                }
            }
            return selectedPages;
        }

		/// <summary>
		/// Finds all CmsWebDirectories. 
		/// </summary>
		/// <returns>
		/// A <see cref="CmsFileCollection"/>
		/// </returns>
        public static CmsFileCollection FindAll() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsWebDirectory), "ORDER BY FullPath", true);
            return new CmsFileCollection(cmd.Execute());
        }
		
		/// <summary>
		/// Finds all deleted files from this directory. 
		/// </summary>
		/// <returns>
		/// A <see cref="CmsFileCollection"/>
		/// </returns>
        public CmsFileCollection FindDeletedFiles() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFile), "WHERE State = @State AND CmsFiles.ID NOT IN (SELECT CmsFiles.RevisionSourceID FROM CmsFiles WHERE NOT CmsFiles.RevisionSourceID IS NULL) ORDER BY FullPath", true);
            cmd.CreateInputParameter("@State", CmsState.Deleted);
            return new CmsFileCollection(cmd.Execute());
        }

        public CmsFileCollection SearchFiles(string text) {
            //TODO: Restrict to Directory
            string filter = @"
 WHERE 
	(
        Title Like @Text 
	     OR CmsFiles.ID IN (
		    SELECT PageID
		    FROM LiteralDataItems
		    WHERE Text Like @Text
	    )
    ) AND CmsFiles.ID NOT IN (
		SELECT RevisionSourceID 
		FROM CmsPages 
		WHERE NOT RevisionSourceID IS NULL
	) ORDER BY FullPath";

            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFile), filter, true);
            cmd.CreateInputParameter("@Text", FormatSearchString(text));
            return new CmsFileCollection(cmd.Execute());
        }

        private string FormatSearchString(string s) {
            if (s == null) {
                return "%";
            } else {
                return "%" + s.Replace("*", "%") + "%";
            }
        }
		
		/// <summary>
		/// Searches this directory for CmsPages. Requires enabling full-text search. 
		/// </summary>
		/// <param name="searchTerm">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="SearchResultCollection"/>
		/// </returns>
        public SearchResultCollection SearchPages(string searchTerm) {
            //TODO: Restrict to Directory
            string query = @"
 SELECT TOP 1000 
	CmsFiles.Title, 
	CmsFiles.FullPath, 
    CmsFiles.ID As VersionID,
   	ISNULL(LiteralDataItemsSearchResults.Rank, 0) 
	+ ISNULL(CmsPagesSearchResults.Rank, 0) * 3 +
	+ ISNULL(CmsFilesSearchResults.Rank,0) * 2 As Rank
FROM CmsFiles
INNER JOIN CmsPages
	ON CmsFiles.ID = CmsPages.ID
LEFT OUTER JOIN
	( 
		SELECT PageID, SUM(Rank) As Rank
		FROM FREETEXTTABLE(LiteralDataItems, *, @SearchTerm) AS GroupedSearchResults
		INNER JOIN LiteralDataItems
   		ON LiteralDataItems.LiteralItemID = GroupedSearchResults.[Key]
		GROUP BY PageID
	) AS LiteralDataItemsSearchResults 
	ON LiteralDataItemsSearchResults.PageID = CmsFiles.ID
LEFT OUTER JOIN
	FREETEXTTABLE(CmsPages, *, @SearchTerm) AS CmsPagesSearchResults
   	ON CmsPages.ID = CmsPagesSearchResults.[Key]
LEFT OUTER JOIN
	FREETEXTTABLE(CmsFiles, *, @SearchTerm) AS CmsFilesSearchResults
   	ON CmsFiles.ID = CmsFilesSearchResults.[Key]
WHERE CmsFiles.State = 200 AND
	ISNULL(LiteralDataItemsSearchResults.Rank, 0) 
	+ ISNULL(CmsPagesSearchResults.Rank,0)
	+ ISNULL(CmsFilesSearchResults.Rank,0) > 0
ORDER BY 
	ISNULL(LiteralDataItemsSearchResults.Rank, 0) 
	+ ISNULL(CmsPagesSearchResults.Rank,0) * 3
	+ ISNULL(CmsFilesSearchResults.Rank,0) * 2 DESC";

            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(SearchResult), query);
            cmd.CreateInputParameter("@SearchTerm", FormatSearchString(searchTerm));
            return new SearchResultCollection(cmd.Execute());
        }

        public DateTime FindLastFileUpdated(bool includeSubdirectories) {

            string sql;
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            if (includeSubdirectories) {
                sql = @"
SELECT MAX(DateModified) 
FROM CmsFiles
WHERE FullPath LIKE @FullPath
";
                IDataAccessCommand cmd = ds.CreateDataAccessCommand(sql);
                cmd.CreateInputParameter("@FullPath", this.FullPath + '%');
                return (DateTime)cmd.ExecuteScalar();

            } else {
                sql = @"
SELECT MAX(DateModified) 
FROM CmsFiles
WHERE ParentID LIKE @ParentID
";
                IDataAccessCommand cmd = ds.CreateDataAccessCommand(sql);
                cmd.CreateInputParameter("@ParentID", this.ID);
                return (DateTime)cmd.ExecuteScalar();
            }
        }

		/// <summary>
		/// Finds the root directory for a WebSite. 
		/// </summary>
		/// <param name="siteID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsWebDirectory"/>
		/// </returns>
        public static CmsWebDirectory FindRootForSite(Guid siteID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsWebDirectory), "WHERE CmsFiles.ID = (SELECT RootWebDirectoryID FROM xs_WebSites WHERE xs_WebSites.ID = @SiteID) ", true);
            cmd.CreateInputParameter("@SiteID", siteID);
            return (CmsWebDirectory)cmd.Execute();
        }
		
		/// <summary>
		/// Finds the root directory for a host header. 
		/// </summary>
		/// <param name="hostHeaderName">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsWebDirectory"/>
		/// </returns>
        public static CmsWebDirectory FindCmsDirectory(string hostHeaderName) {
            HostHeaderMapping hhm = HostHeaderMapping.FindHostHeaderMapping(hostHeaderName);
            if (hhm != null) {
                WebSite site = WebSite.FindByID(hhm.WebSiteID);
                return CmsWebDirectory.FindByID(site.RootWebDirectoryID);
            } else {
                return null;
            }
        }
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="url">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="IHttpHandler"/>
		/// </returns>
        public override IHttpHandler GetHandler(string url) {
            return this;
        }

        #region IHttpHandler Members
		
		/// <summary>
		/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
        public bool IsReusable {
            get { return false; }
        }
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
        public void ProcessRequest(HttpContext context) {

            if (this.DefaultFileName != null) {
                if (!context.Request.FilePath.EndsWith("/")) {
                    context.Response.Redirect(context.Request.FilePath + "/");
                }

                CmsHttpContext.Current.TransferRequest(this.DefaultFile);

            } else {
                throw new HttpException(404, "File '" + context.Request.FilePath + "' not found.");
            }
        }
        #endregion
    }
}

    
