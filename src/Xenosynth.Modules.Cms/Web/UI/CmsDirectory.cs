using System;
using System.Collections;
using System.Text;

using Inform;
using System.Data;
using System.Web;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// The base class for directories in the Xenosynth CMS. 
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsFile))]
    public abstract class CmsDirectory : CmsFile {

        [MemberMapping(ColumnName = "IsRestricted")]
        protected bool isRestricted;


        private CmsFileCollection files;
        private CmsDirectoryCollection subdirectories;

        protected CmsDirectory(Guid fileTypeID)
            : base(fileTypeID) {
        }


        /// <summary>
        /// Returns both a collection of both pages and subdirectories.
        /// </summary>
        public CmsFileCollection Files {
            get {
                if (files == null) {
                    files = CmsFile.FindByDirectoryID(this.FileID);
                }
                return files;
            }
        }

        /// <summary>
        /// Returns both a collection of pages and subdirectories that are not hidden.
        /// </summary>
        public CmsFileCollection DisplayedFiles {
            get { return new CmsFileCollection(FindDisplayed(Files)); }
        }

        public bool HasFiles {
            get { return (Files.Count > 0); }
        }

        /// <summary>
        /// Returns a collection of directories under this directory.
        /// </summary>
        public CmsDirectoryCollection Subdirectories {
            get {
                if (subdirectories == null) {
                    subdirectories = CmsDirectory.FindDirectoryByDirectoryID(this.FileID);
                }
                return subdirectories;
            }
        }

        /// <summary>
        /// Returns a collection of directories under this directory that are not hidden.
        /// </summary>
        public CmsDirectoryCollection DisplayedSubdirectories {
            get { return new CmsDirectoryCollection(FindDisplayed(Subdirectories)); }
        }
		
		/// <summary>
		/// Finds all directories for a directory. 
		/// </summary>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsDirectoryCollection"/>
		/// </returns>
        public static CmsDirectoryCollection FindDirectoryByDirectoryID(Guid directoryID) {
            return FindDirectoryByDirectoryID(directoryID, CmsHttpContext.Current.Mode != CmsMode.Published);
        }
		
		/// <summary>
		/// Finds a the subdirectories for a directory. 
		/// </summary>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="showUnpublished">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsDirectoryCollection"/>
		/// </returns>
        public static CmsDirectoryCollection FindDirectoryByDirectoryID(Guid directoryID, bool showUnpublished) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = null;
            if (!showUnpublished) {
                if (HttpContext.Current.User != null
                        && HttpContext.Current.User.Identity.IsAuthenticated
                        && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)) {
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsDirectory), "WHERE ParentID = @ParentID AND State = @State ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                } else { //enforce dates
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsDirectory), "WHERE ParentID = @ParentID AND State = @State AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                    cmd.CreateInputParameter("@Date", DateTime.Now);
                }

            } else { //TODO: switch to fileID?
                cmd = ds.CreateFindCollectionCommand(typeof(CmsDirectory), "WHERE CmsFiles.ParentID = @ParentID AND State < @State AND CmsFiles.ID NOT IN (SELECT CmsFiles.RevisionSourceID FROM CmsFiles WHERE NOT CmsFiles.RevisionSourceID IS NULL) ORDER BY SortOrder", true);
                cmd.CreateInputParameter("@State", CmsState.Deleted);
            }

            cmd.CreateInputParameter("@ParentID", directoryID);
            return new CmsDirectoryCollection(cmd.Execute());
        }
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="files">
		/// A <see cref="CmsFileCollection"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        protected IList FindDisplayed(CmsFileCollection files) {
            ArrayList displayedFiles = new ArrayList();
            foreach (CmsFile f in files) {
                if (!f.IsHidden) {
                    displayedFiles.Add(f);
                }
            }

            return displayedFiles;
        }
		
		/// <summary>
		/// Reorders and incrementally updates the sort order of the pages after adding or removing pages. 
		/// </summary>
        public void UpdateSortOrder() {
            CmsFileCollection files = this.Files;
            int i = 0;
            foreach (CmsFile f in files) {
                f.SortOrder = i++;
                f.Update(); //TODO: Fire full update with Audit Logging?
            }
        }

        public void RefreshDescendentFullPaths() {
            foreach (CmsFile f in Files) {
                f.RefreshFullPath();
                f.Update();
                if (f.FileType.IsDirectory) {
                    CmsDirectory dir = (CmsDirectory)f;
                    dir.RefreshDescendentFullPaths();
                }
            }

        }

		/// <summary>
		/// Marks this CmsDirectory as deleted. 
		/// </summary>
        public override void Delete() {

            if (HasFiles) {
                throw new ApplicationException("Can not delete CmsDirectory that has dependencies.");
            }

            base.Delete();
        }

		/// <summary>
		/// Deletes this CmsDirectory from the database. 
		/// </summary>
        public override void PermanentlyDelete() {

            if (HasFiles) {
                throw new ApplicationException("Can not permanently delete CmsDirectory that has dependencies.");
            }

            base.PermanentlyDelete();
        }
		
		/// <summary>
		/// Finds all CmsFiles in this directory that have all the specified attributes. 
		/// </summary>
		/// <param name="recursive">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <param name="attributes">
		/// A <see cref="CmsAttribute[]"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/> of CmsFiles.
		/// </returns>
        public IList FindFilesByAttributes(bool recursive, params CmsAttribute[] attributes) {
            //TODO: Faster search! Recursive?

            CmsFileCollection files = this.Files;
            ArrayList selectedFiles = new ArrayList();

            foreach (CmsFile f in files) {

                bool hasAll = true;

                foreach (CmsAttribute attr in attributes) {
                    bool found = false;
                    string[] values = f.Attributes.GetValues(attr.Name);
                    if (values != null) {
                        foreach (string v in values) {
                            if (v == attr.Value) {
                                found = true;
                                break;
                            }
                        }
                    }
                    if (!found) {
                        hasAll = false;
                        break;
                    }
                }
                if (hasAll) {
                    selectedFiles.Add(f);
                }

                if(recursive && f.FileType.IsDirectory){
                    CmsDirectory d = (CmsDirectory)f;
                    selectedFiles.AddRange(d.FindFilesByAttributes(recursive, attributes));
                }
            }
            return selectedFiles;
        }
		
		/// <summary>
		/// Finds all the CmsFiles in the directory that has any of the specified attributes.
		/// </summary>
		/// <param name="recursive">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <param name="attributes">
		/// A <see cref="CmsAttribute[]"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public IList FindFilesByAnyAttributes(bool recursive, params CmsAttribute[] attributes) {
            //TODO: Faster search! Recursive?

            CmsFileCollection files = this.Files;
            ArrayList selectedFiles = new ArrayList();

            foreach (CmsFile f in files) {

                bool hasAll = true;

                foreach (CmsAttribute attr in attributes) {
                    bool found = false;
                    string[] values = f.Attributes.GetValues(attr.Name);
                    if (values != null) {
                        foreach (string v in values) {
                            if (v == attr.Value) {
                                selectedFiles.Add(f);
                                break;
                            }
                        }
                    }
                   
                }

                if (recursive && f.FileType.IsDirectory) {
                    CmsDirectory d = (CmsDirectory)f;
                    selectedFiles.AddRange(d.FindFilesByAttributes(recursive, attributes));
                }
            }
            return selectedFiles;
        }
		
		
		/// <summary>
		/// Gets a list of all the unique <see cref="String"> values for the attribute in this directory. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="attributes">
		/// A <see cref="CmsAttribute[]"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public IList FindUniqueAttributeValues(string name, params CmsAttribute[] attributes) {

            IList files = this.FindFilesByAttributes(false, attributes);
            ArrayList list = new ArrayList();

            foreach (CmsFile f in files) {
                string[] values = f.Attributes.GetValues(name);
                if (values != null) {
                    foreach (string v in values) {
                        if (!list.Contains(v)) {
                            list.Add(v);
                        }
                    }
                }
            }

            list.Sort();

            return list;

        }

		
		/// <summary>
		/// Gets a list of all the unique <see cref="String"> values for the attribute in this directory. 
		/// </summary>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="recursive">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>
		/// </returns>
        public string[] FindUniqueAttributeValues(string name, bool recursive) {

            string sql = null;

            if (recursive) {
                sql = @"
SELECT DISTINCT Value FROM CmsFileAttributes
WHERE Name = @Name
AND FileID IN (
 SELECT ID FROM CmsFiles
 WHERE State >= @State1 AND State < @State2
    AND FullPath LIKE @FullPath
)
ORDER BY Value
";
            } else {
                sql = @"
SELECT DISTINCT Value FROM CmsFileAttributes
WHERE Name = @Name
AND FileID IN (
 SELECT ID FROM CmsFiles
 WHERE State >= @State1 AND State < @State2
    AND ParentID = @ParentID
)
ORDER BY Value
";
            }


            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand(sql);
            cmd.CreateInputParameter("@Name", name);
            if (recursive) {
                cmd.CreateInputParameter("@FullPath", this.FullPath + "%");
            } else {
                cmd.CreateInputParameter("@ParentID", this.ID);
            }

           
            cmd.CreateInputParameter("@State1", CmsHttpContext.Current.SearchScopeLowerBound);
            cmd.CreateInputParameter("@State2", CmsHttpContext.Current.SearchScopeUpperBound);
             

            IDataReader r = cmd.ExecuteReader();

            ArrayList values = new ArrayList();
            while (r.Read()) {
                values.Add(r["Value"].ToString());
            }

            return (string[])values.ToArray(typeof(string));

        }


        public virtual CmsFile DefaultFile {
            //TODO: Expand this logic!
            get {
                return this.Files[0];
            }

        }

        public virtual string DefaultFileName {
            get {
                if (DefaultFile == null) {
                    return null;
                } else {
                    return DefaultFile.FileName;
                }
            }
        }

    }
}
