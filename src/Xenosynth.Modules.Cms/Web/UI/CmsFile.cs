using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlTypes;
using System.Web;

using Inform;
using Xenosynth.Security;
using System.Web.Security;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// Summary description for CmsFileObject.
	/// </summary>
	public class CmsFile {

        public class CmsFileInfo {

            private string fullPath;
            private Guid fileID;
            private Type fileType;

            public CmsFileInfo(string fullPath, Guid fileID, Type fileType) {
                this.fullPath = fullPath;
                this.fileID = fileID;
                this.fileType = fileType;
            }

            public string FullPath { get { return fullPath;  } }
            public Guid FileID { get { return fileID; } }
            public Type FileType { get { return fileType; } }

        }

        //TODO: move to application cache?
        private static Dictionary<string, CmsFileInfo> fileInfoCache = new Dictionary<string, CmsFileInfo>();


		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		protected Guid id; //TODO: Rename VersionID

        [MemberMapping(ColumnName = "FileID")]
        protected Guid fileID;

        [MemberMapping(ColumnName = "Version")]
        protected int version;

		[MemberMapping(ColumnName="ParentID")]
		protected SqlGuid parentID;

        [MemberMapping(ColumnName = "State", AllowNulls = false)]
        protected CmsState state;

		[MemberMapping(ColumnName="FileName", Length=100)]
		protected string fileName;

        [MemberMapping(ColumnName = "FileTypeID")]
        protected Guid fileTypeID;

        [MemberMapping(ColumnName = "OwnerID")]
        protected Guid ownerID;

		[MemberMapping(ColumnName="Title", Length=100)]
		protected string title;

		[MemberMapping(ColumnName="FullPath", Length=250)]
		protected string fullPath;

		[MemberMapping(ColumnName="DateCreated")]
		protected DateTime dateCreated;

		[MemberMapping(ColumnName="DateModified")]
		protected DateTime dateModified;

		[MemberMapping(ColumnName="SortOrder")]
		protected int sortOrder;

		[MemberMapping(ColumnName="IsHidden")]
		protected bool isHidden;

        [MemberMapping(ColumnName = "RevisionSourceID", AllowNulls = true)]
        protected SqlGuid revisionSourceID;

		[MemberMapping(ColumnName="PublishStart")]
		protected SqlDateTime publishStart;

		[MemberMapping(ColumnName="PublishEnd")]
		protected SqlDateTime publishEnd;

		[RelationshipMapping( Name="CmsFiles_CmsDirectory",  ChildMember="parentID", 
			 ParentType=typeof(CmsDirectory), ParentMember="id", Type=Relationship.OneToOne)]
		private ObjectCache parentDirectoryCache = new ObjectCache();

        protected CmsFileType fileType;

        private CmsFileAttributeCollection attributes;

		/// <summary>
		/// The identifier for this version of the CmsFileItem.
		/// </summary>
		public Guid ID {
			get { return id; }
		}

        /// <summary>
        /// The identifier for all versions of this CmsFileItem.
        /// </summary>
        public Guid FileID {
            get { return fileID; }
        }

        public int Version {
            get { return version; }
        }

        public virtual CmsFileType FileType {
            get {
                if (fileType == null) {
                    fileType = CmsFileType.FindByID(this.FileTypeID);
                }
                return fileType;
            }
        }

        public Guid FileTypeID {
            get { return fileTypeID; }
        }

        public Guid OwnerID {
            get { return ownerID; }
        }

        public MembershipUser Owner {
            get { return Membership.GetUser(ownerID); }
        }

		public Guid ParentID {
			set { parentID = new SqlGuid(value); }
			get { 
				if(parentID.IsNull){
					  return Guid.Empty;
				  } else {
					  return parentID.Value; 
				  }
			}
		}

        public CmsDirectory ParentDirectory {
            get { return (CmsDirectory)parentDirectoryCache.CachedObject; }
        }

        public CmsState State {
            get { return state; }
        }

		public string FileName {
			set { fileName = value; }
			get { return fileName; }
		}

		public string Title {
			set { title = value; }
			get { return title; }
		}

        public CmsFileAttributeCollection Attributes {
            get {
                if (attributes == null || attributes.fileID != this.ID) {
                    attributes = new CmsFileAttributeCollection(this.ID, CmsFileAttribute.FindByFileID(this.ID));
                }
                return attributes;
            }
        }

		public string FullPath {
			set { fullPath = value; }
			get { return fullPath; }
		}

		public virtual string Url {
			get {
                string url = null;
                if (fullPath != null) {
                    url = CmsContext.Current.ResolveUrl(fullPath);
                    if (!Path.HasExtension(url)) {
                        url += "/";
                    }
                }
                return url;

			}
		}

        public string VersionUrl {
            get {
                return CmsContext.Current.ResolveVersionUrl(this);
            }
        }

		public DateTime DateCreated {
			get { return dateCreated; }
		}

		public DateTime DateModified {
			get { return dateModified; }
			set { dateModified = value; }
		}

       

		public int SortOrder {
			set { sortOrder = value; }
			get { return sortOrder; }
		}

		public bool IsHidden {
			set { isHidden = value; }
			get { return isHidden; }
		}

		public SqlDateTime PublishStart {
			set { publishStart = value; }
			get { return publishStart; }
		}

        public DateTime PublishStartSortable {
            get {
                if (PublishStart.IsNull) {
                    return dateCreated.AddYears(1000);
                } else {
                    return PublishStart.Value;
                }
            }
        }

		public SqlDateTime PublishEnd {
			set { publishEnd = value; }
			get { return publishEnd; }
		}
        
        public bool IsRoot {
            get { return this.ParentID.Equals(Guid.Empty); }
        }

        public virtual bool IsVirtual {
            get { return false; }
        }

        public static CmsFile Current {
            get { return CmsHttpContext.Current.CmsFile; }
        }

        public IList AuditLog { //TODO: Typed or Generic?
            //TODO: Cache?
            get {
                return LogEntry.FindBy(LogEntry.LogEventType.Audit,
                       LogEntry.LogSource.File,
                       this.FileID);
            }
        }

        public string DefaultActionUrl {
            get { 
                switch (this.FileType.DefaultAction) {
                    case CmsFileType.Action.Browse:
                        return this.FileType.BrowseUrl + "?FileID=" + this.ID;

                    case CmsFileType.Action.Create:
                        return this.FileType.CreateUrl + "?FileID=" + this.ID;

                    case CmsFileType.Action.Edit:
                        return this.FileType.EditUrl + "?FileID=" + this.ID;

                    default:
                        return this.Url;
                }
            }
        }
 
		protected CmsFile(Guid fileTypeID) {
            this.fileTypeID = fileTypeID;
            this.state = CmsState.Unsaved;
            this.version = 1;
            this.id = Guid.NewGuid();
            this.fileID = id;
			dateCreated = DateTime.Now; 
			dateModified = dateCreated;
            this.revisionSourceID = SqlGuid.Null;
        }

        public virtual Stream Open() {
            throw new NotImplementedException();
        }

        public virtual Stream Open(string virtualPath) {
            throw new NotImplementedException();
        }


        public bool IsDescendantOf(CmsFile file) {
            return this.FullPath.StartsWith(file.FullPath);
        }

        public CmsFileCollection RetrieveRevisionHistory() { //TODO: Refator and Rename, Speed up by FileID

            ArrayList revisions = new ArrayList();

            CmsFile revisionSource = this;

            while (revisionSource != null && !revisionSource.revisionSourceID.IsNull) {
                revisionSource = CmsFile.FindByID(revisionSource.revisionSourceID.Value);
                if (revisionSource != null) {
                    revisions.Add(revisionSource);
                }
            }

            return new CmsFileCollection(revisions);
        }

		internal void RefreshFullPath() {
			if(!Guid.Empty.Equals(ParentID)){
				this.fullPath = ParentDirectory.FullPath + "/" + this.FileName;
			} else {
				this.fullPath = "/" + this.FileName;
			}
		}

        public virtual void UpdateOrVersion() {
            if (this.State == CmsState.Published) { 	 //TODO: Archived?
                InsertVersion();
            } else {
                this.Update();
            }
        }

        public virtual void InsertVersion() {

            //TODO: sweep archive old versions?
            //this.Update();


            //Create new revision
            this.revisionSourceID = this.ID;
            this.id = Guid.NewGuid();
         
            this.version += 1;
            this.state = CmsState.Unpublished;
            this.dateCreated = DateTime.Now;
            this.dateModified = DateTime.Now;

            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            ds.Insert(this);

            LogEntry.LogEvent(LogEntry.LogEventType.Audit, LogEntry.LogSource.File, this.FileID, "Versioned",
                string.Format("Versioned {0} '{1}' to {2}.", this.FileType.Name, this.FileName, this.Version));

            //copy attributes
            IList attrs = CmsFileAttribute.FindByFileID(this.revisionSourceID.Value);
            foreach (CmsFileAttribute a in attrs) {
                a.CopyTo(id);
            }

        }

        public virtual void Insert() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            ds.SetContext(this);
            this.RefreshFullPath();

            this.state = CmsState.Unpublished;
            this.dateModified = DateTime.Now;

            MembershipUser user = Membership.GetUser();
            this.ownerID = (Guid)user.ProviderUserKey;

            ds.Insert(this);

            LogAuditEvent(this, "Created", "Created");
        }

        public virtual void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            this.RefreshFullPath();
            this.dateModified = DateTime.Now;
            ds.Update(this);

        }

        public virtual void PermanentlyDelete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            
            //TODO: Put on thread ds.BeginTransaction();

            //disconnect from revision history
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("UPDATE CmsFiles SET RevisionSourceID = NULL WHERE RevisionSourceID = @VersionID");
            cmd.CreateInputParameter("@VersionID", this.ID);
            cmd.ExecuteNonQuery();

            try {
                IList attrs = CmsFileAttribute.FindByFileID(this.ID);
                foreach (CmsFileAttribute a in attrs) {
                    a.Delete();
                }

                ds.BeginTransaction();
                ds.Delete(this);

                LogAuditEvent(this, "Permanently Deleted", "Permanently Deleted");

                ds.CommitTransaction();

                //TODO: Move to base?
                CmsFile source = null;

                if (!revisionSourceID.IsNull) {
                    source = CmsFile.FindByID(revisionSourceID.Value);
                }

                if (source != null) {
                    source.PermanentlyDelete();
                }
            } catch (Exception e) {
                ds.RollbackTransaction();
                throw new Exception("Could not delete file.", e);
            }
            //ds.CommitTransaction();
        }

        public virtual void Delete() {

            this.state = CmsState.Deleted;
            this.dateModified = DateTime.Now;
            this.Update();

            LogAuditEvent(this, "Deleted", "Deleted");

        }

        public void Restore() {

            //TODO: reconnect revision history?

            this.state = CmsState.Unpublished;
            this.dateModified = DateTime.Now;
            this.Update();

            LogAuditEvent(this, "Restored", "Restored");
        }

        public void Archive() {

            if (this.state == CmsState.Published) {
                this.publishEnd = DateTime.Now;
            }

            this.state = CmsState.Archived;
            this.dateModified = DateTime.Now;
            this.Update();

            LogAuditEvent(this, "Archived", "Archived");

        }

        public void Publish() {

            if (this.State == CmsState.Unsaved) {
                throw new ApplicationException("Can not publish Unsaved file.");
            } else {

                this.state = CmsState.Published;
                this.dateModified = DateTime.Now;

                if (this.publishStart.IsNull) {
                    this.publishStart = DateTime.Now;
                }

                UpdatePublishingSchedule();
                
                this.Update();

                LogAuditEvent(this, "Published", "Published");
            }

        }

        public void UpdatePublishingSchedule() {
            CmsFileCollection revisions = this.RetrieveRevisionHistory();

                foreach(CmsFile revision in revisions){

                    //prevent overlap
                    if (revision.PublishEnd.IsNull || revision.PublishEnd > this.PublishStart) {
                        revision.PublishEnd = this.PublishStart;
                        revision.Update();
                    }

                    //archive if expired
                    if (!revision.PublishEnd.IsNull && revision.PublishEnd < DateTime.Now) {
                        revision.Archive();
                    }


                }

        }

        public void Unpublish() {

            if (this.State == CmsState.Unsaved) {
                throw new ApplicationException("Can not unpublish Unsaved file.");
            } else {
                this.state = CmsState.Unpublished;
                this.dateModified = DateTime.Now;

                this.Update();

                LogAuditEvent(this, "Unpublished", "Unpublished");

                if (!this.revisionSourceID.IsNull) {
                    CmsFile revisionSource = CmsFile.FindByID(this.revisionSourceID.Value);
                    if (revisionSource != null) {
                        revisionSource.Publish();
                    }
                }
            }

        }

		public void MoveToDirectory(string fileName, Guid directoryID){

            CmsDirectory oldDirectory = (CmsDirectory)CmsDirectory.FindByID(this.ParentID);
            CmsDirectory newDirectory = (CmsDirectory)CmsDirectory.FindByID(directoryID);

            this.parentID = newDirectory.FileID;
			this.fileName = fileName;
			this.Update();

			oldDirectory.UpdateSortOrder();
			newDirectory.UpdateSortOrder();
		}

        public virtual void CopyToDirectory(string filename, Guid directoryID) {

            CmsDirectory oldDirectory = (CmsDirectory)CmsFile.FindByID(this.ParentID);
            CmsDirectory newDirectory = (CmsDirectory)CmsFile.FindByID(directoryID);

            //Create copy
            this.fileName = filename;
            this.revisionSourceID = SqlGuid.Null;
            this.id = Guid.NewGuid();
            this.fileID = Guid.NewGuid();
            this.dateCreated = DateTime.Now;
            this.parentID = newDirectory.FileID;

            this.Insert();

            oldDirectory.UpdateSortOrder();
            if (newDirectory.ID != oldDirectory.ID) {
                newDirectory.UpdateSortOrder();
            }
        }

		public void MoveSortOrder(bool up){
			CmsFile f = FindByNextInOrder(up);
			if(f != null){
				//swap
				int s = this.SortOrder;
				this.SortOrder = f.SortOrder;
				f.SortOrder = s;

				f.Update();
				this.Update();
			}
		}


		public void MoveSortOrder(int index){
			CmsFileCollection files = this.ParentDirectory.Files;
			int i = 0;
			foreach(CmsFile f in files){
				if(i == index){
					i++;
				}
				if(f.ID != this.ID){
					f.SortOrder = i++;
					f.Update();
				}
			}
			this.SortOrder = index;
			this.Update();
		}

		public  CmsFile FindByNextInOrder(bool up){

			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd;
			if(up){
				cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE ParentID=@ParentID AND SortOrder < @SortOrder ORDER BY SortOrder DESC", true);
			} else {
				cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE ParentID=@ParentID AND SortOrder > @SortOrder ORDER BY SortOrder ASC", true);
			}
			cmd.CreateInputParameter("@ParentID", this.ParentID);
			cmd.CreateInputParameter("@SortOrder", this.SortOrder);

			return (CmsFile)cmd.Execute();
		}	

		public static CmsFile FindByID(Guid fileID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE CmsFiles.ID = @ID", true);
			cmd.CreateInputParameter("@ID", fileID);
			return (CmsFile)cmd.Execute();
		}

        public static CmsFile FindByFullPath(string path) {


            if (path == null) {
                throw new ArgumentNullException("path");
            } else {
                path = path.TrimEnd('/');
            }


            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindObjectCommand cmd = null;
            CmsFileInfo fileInfo = null;
            bool cacheFileInfo = false;

            switch (CmsHttpContext.Current.Mode) {
                case CmsMode.Edit:
                    cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC, Version DESC ", true);
                    cmd.CreateInputParameter("@FullPath", path);
                    cmd.CreateInputParameter("@State1", CmsState.Unpublished);
                    cmd.CreateInputParameter("@State2", CmsState.Archived);
                    break;
                case CmsMode.Unpublished:
                    cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC, Version DESC", true);
                    cmd.CreateInputParameter("@FullPath", path);
                    cmd.CreateInputParameter("@State1", CmsState.Unpublished);
                    cmd.CreateInputParameter("@State2", CmsState.Archived);
                    break;
                case CmsMode.Published:
                    if (HttpContext.Current.User != null
                        && HttpContext.Current.User.Identity.IsAuthenticated
                        && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)) {
                        cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC, Version DESC", true);
                        cmd.CreateInputParameter("@FullPath", path);
                        cmd.CreateInputParameter("@State1", CmsState.Published);
                        cmd.CreateInputParameter("@State2", CmsState.Archived);
                    } else { //enforce dates

                        if (fileInfoCache.TryGetValue(path, out fileInfo)) {
                            cmd = ds.CreateFindObjectCommand(fileInfo.FileType, "WHERE FileID = @FileID AND State >= @State1 AND State < @State2 AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY State ASC, Version DESC", true);
                            cmd.CreateInputParameter("@FileID", fileInfo.FileID);                     
                        } else {
                            cacheFileInfo = true; //cache miss
                            cmd = ds.CreateFindObjectCommand(typeof(CmsFile), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY State ASC, Version DESC", true);
                            cmd.CreateInputParameter("@FullPath", path);
                        }

                        cmd.CreateInputParameter("@State1", CmsState.Published);
                        cmd.CreateInputParameter("@State2", CmsState.Archived);
                        cmd.CreateInputParameter("@Date", DateTime.Now);

                    }
                    break;
            }

            CmsFile file = (CmsFile)cmd.Execute();
            if (cacheFileInfo && file != null) {
                fileInfoCache[path] = new CmsFileInfo(path, file.FileID, file.GetType());
            }
            return file;
        }

        public static CmsFile FindByFileID(Guid fileID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindObjectCommand cmd = null;
            Type fileType = typeof(CmsFile);

            CmsFileInfo fileInfo = null;
            //if (fileInfoCache.TryGetValue(path, out fileInfo)) {
            //    fileType = fileInfo.FileType;
            //}

            switch (CmsHttpContext.Current.Mode) {
                case CmsMode.Edit:
                    cmd = ds.CreateFindObjectCommand(fileType, "WHERE FileID = @FileID AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
                    cmd.CreateInputParameter("@FileID", fileID);
                    cmd.CreateInputParameter("@State1", CmsState.Unpublished);
                    cmd.CreateInputParameter("@State2", CmsState.Archived);
                    break;
                case CmsMode.Unpublished:
                    cmd = ds.CreateFindObjectCommand(fileType, "WHERE FileID = @FileID AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
                    cmd.CreateInputParameter("@FileID", fileID);
                    cmd.CreateInputParameter("@State1", CmsState.Unpublished);
                    cmd.CreateInputParameter("@State2", CmsState.Archived);
                    break;
                case CmsMode.Published:
                    if (HttpContext.Current.User != null
                        && HttpContext.Current.User.Identity.IsAuthenticated
                        && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)) {
                        cmd = ds.CreateFindObjectCommand(fileType, "WHERE FileID = @FileID AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
                        cmd.CreateInputParameter("@FileID", fileID);
                        cmd.CreateInputParameter("@State1", CmsState.Published);
                        cmd.CreateInputParameter("@State2", CmsState.Archived);
                    } else { //enforce dates
                        cmd = ds.CreateFindObjectCommand(fileType, "WHERE FileID = @FileID AND State >= @State1 AND State < @State2 AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY State ASC", true);
                        cmd.CreateInputParameter("@FileID", fileID);
                        cmd.CreateInputParameter("@State1", CmsState.Published);
                        cmd.CreateInputParameter("@State2", CmsState.Archived);
                        cmd.CreateInputParameter("@Date", DateTime.Now);
                    }
                    break;
            }
            return (CmsFile)cmd.Execute();
        }

		public static CmsFileCollection FindByDirectoryID(Guid directoryID){
			return FindByDirectoryID(directoryID, CmsHttpContext.Current.Mode != CmsMode.Published);
		}

		public static CmsFileCollection FindByDirectoryID(Guid directoryID, bool showUnpublished){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

			IFindCollectionCommand cmd = null;
			if(!showUnpublished){
                if (HttpContext.Current.User != null
                        && HttpContext.Current.User.Identity.IsAuthenticated
                        && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)) {
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsFile), "WHERE ParentID = @ParentID AND State = @State ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                } else { //enforce dates
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsFile), "WHERE ParentID = @ParentID AND State = @State AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                    cmd.CreateInputParameter("@Date", DateTime.Now);
                }
				
			} else { //TODO: switch to fileID?
                cmd = ds.CreateFindCollectionCommand(typeof(CmsFile), "WHERE CmsFiles.ParentID = @ParentID AND State < @State AND CmsFiles.ID NOT IN (SELECT CmsFiles.RevisionSourceID FROM CmsFiles WHERE NOT CmsFiles.RevisionSourceID IS NULL) ORDER BY SortOrder", true);
                cmd.CreateInputParameter("@State", CmsState.Deleted);
            }

			cmd.CreateInputParameter("@ParentID", directoryID);
			return new CmsFileCollection(cmd.Execute());
		}



        public static void LogAuditEvent(CmsFile file, string eventName, string message) {
            LogEntry.LogEvent(LogEntry.LogEventType.Audit, LogEntry.LogSource.File, file.FileID, eventName,
               message + string.Format(" {0} '{1}', Version {2}.", file.FileType.Name, file.FileName, file.Version));
        }

        public virtual IHttpHandler GetHandler(string url) {
            throw new NotImplementedException();
        }
    }
}
