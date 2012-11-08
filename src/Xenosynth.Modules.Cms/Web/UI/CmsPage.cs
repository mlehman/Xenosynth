using System;
using System.Collections;
using System.Data.SqlTypes;
using Inform;
using System.Web;

using Xenosynth.Web;
using System.Web.UI;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// A CmsPage is an HTML page in the CMS.
	/// </summary>
	[TypeMapping(BaseType=typeof(CmsFile))]
	public class CmsPage : CmsFile {

		//seperate context?
		public event PageEventHandler SaveCmsPageEvent;

		[MemberMapping(ColumnName="Description", Length=250)]
		protected string description;

		[MemberMapping(ColumnName="Keywords", Length=250)]
		protected string keywords;

		[MemberMapping(ColumnName="TemplateID", AllowNulls=true)]
		private SqlGuid templateID;

		[MemberMapping(ColumnName="IsStatic", AllowNulls=false)]
		private bool isstatic;

		[RelationshipMapping( Name="CmsPages_CmsTemplates",  ChildMember="templateID", 
			 ParentType=typeof(CmsTemplate), ParentMember="id")]
		private ObjectCache cachedTemplate = new ObjectCache();

        private CmsContentBlockCollection content;

        private static Guid typeID = new Guid("7a362020-609b-4ee7-9d56-393fb8f7cc56");

		public CmsPage() : base(typeID) {
		}
		
		/// <summary>
		/// The CmsFileType for this CmsPage. 
		/// </summary>
        public override CmsFileType FileType {
            get {
                if (fileType == null) {

                    fileType = CmsFileType.FindByID(this.TemplateID);

                    if (fileType == null) {
                        fileType = CmsFileType.FindByID(this.FileTypeID);
                    }
                }
                return fileType;
            }
        }
		
		/// <summary>
		/// Gets the description for this CmsPage. 
		/// </summary>
		public string Description {
			get { return description; }
			set { description = value; }
		}
		
		/// <summary>
		/// Gets the keywords for this CmsPage. 
		/// </summary>
		public string Keywords {
			get { return keywords; }
			set { keywords = value; }
		}
		
		/// <summary>
		/// Gets the collection of content blocks for this CmsPage. 
		/// </summary>
        public CmsContentBlockCollection ContentBlocks {
			get {
				if (content == null){
                    content = new CmsContentBlockCollection(this);
				}
				return content;
			}
		}

		/// <summary>
		/// Gets the ID for this CmsPage's template. 
		/// </summary>
		public Guid TemplateID {
			get { 
				if(templateID.IsNull){
					return Guid.Empty;
				} else {
					return templateID.Value; 
				}
			}
			set { 
				if(value.Equals(Guid.Empty)){
					templateID = SqlGuid.Null;
				} else {
					templateID = new SqlGuid(value);
				}
			}
		}
		
		/// <summary>
		/// Gets the template for this CmsPage. 
		/// </summary>
		public CmsTemplate Template {
			get { return (CmsTemplate)cachedTemplate.CachedObject; }
		}
		
		/// <summary>
		/// Returns the CmsPage that matches the current http request path. This method may be deprecated. 
		/// </summary>
		public static CmsPage Current {
			get { return CmsHttpContext.Current.CmsPage; }
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		public bool IsStatic {
			get { return isstatic; }
			set { isstatic = value; }
		}


        //public static CmsPage FindByFullPath(string path){
        //    DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

        //    IFindObjectCommand cmd = null;
			
        //    switch(CmsHttpContext.Current.Mode) {
        //        case CmsMode.Edit:
        //            cmd = ds.CreateFindObjectCommand(typeof(CmsPage), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
        //            cmd.CreateInputParameter("@FullPath", path);
        //            cmd.CreateInputParameter("@State1", CmsState.Unpublished );
        //            cmd.CreateInputParameter("@State2", CmsState.Archived );
        //            break;
        //        case CmsMode.Unpublished:
        //            cmd = ds.CreateFindObjectCommand(typeof(CmsPage), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
        //            cmd.CreateInputParameter("@FullPath", path);
        //            cmd.CreateInputParameter("@State1", CmsState.Unpublished );
        //            cmd.CreateInputParameter("@State2", CmsState.Archived );
        //            break;
        //        case CmsMode.Published:
        //            if( HttpContext.Current.User != null
        //                && HttpContext.Current.User.Identity.IsAuthenticated 
        //                && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User) ){
        //                cmd = ds.CreateFindObjectCommand(typeof(CmsPage), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 ORDER BY State ASC", true);
        //                cmd.CreateInputParameter("@FullPath", path);
        //                cmd.CreateInputParameter("@State1", CmsState.Published );
        //                cmd.CreateInputParameter("@State2", CmsState.Archived );
        //            } else { //enforce dates
        //                cmd = ds.CreateFindObjectCommand(typeof(CmsPage), "WHERE FullPath = @FullPath AND State >= @State1 AND State < @State2 AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY State ASC", true);
        //                cmd.CreateInputParameter("@FullPath", path);
        //                cmd.CreateInputParameter("@State1", CmsState.Published );
        //                cmd.CreateInputParameter("@State2", CmsState.Archived );
        //                cmd.CreateInputParameter("@Date", DateTime.Now );
        //            }
        //            break;
        //        }
        //    return (CmsPage)cmd.Execute();
        //}

		
		/// <summary>
		///  This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsPageCollection"/>
		/// </returns>
		internal static CmsPageCollection FindAllByDirectoryID(Guid directoryID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsPage), "WHERE ParentID = @ParentID ORDER BY SortOrder", true);
			cmd.CreateInputParameter("@ParentID", directoryID);
			return new CmsPageCollection(cmd.Execute());
		}
		
		/// <summary>
		/// Gets a collection of CmsPages in a directory. 
		/// </summary>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsPageCollection"/>
		/// </returns>
		public static new CmsPageCollection FindByDirectoryID(Guid directoryID){
			return FindByDirectoryID(directoryID, CmsHttpContext.Current.Mode != CmsMode.Published);
		}
		
		/// <summary>
		/// Gets a collection of CmsPages in a directory. 
		/// </summary>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="showUnpublished">
		/// A <see cref="System.Boolean"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsPageCollection"/>
		/// </returns>
		public static new CmsPageCollection FindByDirectoryID(Guid directoryID, bool showUnpublished){

            //TODO: Push more generic!!
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            IFindCollectionCommand cmd = null;
            if (!showUnpublished) {
                if (HttpContext.Current.User != null
                        && HttpContext.Current.User.Identity.IsAuthenticated
                        && CmsContext.Current.IsInAuthoringRole(HttpContext.Current.User)) {
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsPage), "WHERE ParentID = @ParentID AND State = @State ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                } else { //enforce dates
                    cmd = ds.CreateFindCollectionCommand(typeof(CmsPage), "WHERE ParentID = @ParentID AND State = @State AND (CmsFiles.PublishStart IS NULL OR CmsFiles.PublishStart < @Date) AND (CmsFiles.PublishEnd IS NULL OR CmsFiles.PublishEnd > @Date) ORDER BY SortOrder", true);
                    cmd.CreateInputParameter("@State", CmsState.Published);
                    cmd.CreateInputParameter("@Date", DateTime.Now);
                }
            } else { //TODO: switch to fileID?
                cmd = ds.CreateFindCollectionCommand(typeof(CmsPage), "WHERE CmsFiles.ParentID = @ParentID AND State < @State AND CmsFiles.ID NOT IN (SELECT CmsFiles.RevisionSourceID FROM CmsFiles WHERE NOT CmsFiles.RevisionSourceID IS NULL) ORDER BY SortOrder", true);
                cmd.CreateInputParameter("@State", CmsState.Deleted);
            }

            cmd.CreateInputParameter("@ParentID", directoryID);
			return new CmsPageCollection(cmd.Execute());
		}

		/// <summary>
		/// Finds a list of CmsPages by template ID. 
		/// </summary>
		/// <param name="templateID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindByTemplateID(Guid templateID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsPage), "WHERE TemplateID = @TemplateID", true);
			cmd.CreateInputParameter("@TemplateID", templateID);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds a CmsPage by ID. 
		/// </summary>
		/// <param name="pageID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsPage"/>
		/// </returns>
		public static new CmsPage FindByID(Guid pageID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsPage), "WHERE CmsPages.ID = @ID", true);
			cmd.CreateInputParameter("@ID", pageID);
			return (CmsPage)cmd.Execute();
		}

		
		/// <summary>
		/// Copies to the CmsPage to a new directory with the specified name. 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="directoryID">
		/// A <see cref="Guid"/>
		/// </param>
		public override void CopyToDirectory(string filename, Guid directoryID){

            Guid sourceID = this.ID;

            base.CopyToDirectory(filename, directoryID);
			
			foreach(CmsRegisteredContent rc in Template.RegisteredContent){
				rc.CopyContent(sourceID, this.ID);
			}

			//copy attributes
			IList attrs = CmsFileAttribute.FindByFileID(sourceID);
			foreach(CmsFileAttribute a in attrs){
				a.CopyTo(id);
			}
			
		}
		
		/// <summary>
		/// Updates, or inserts a new version if already published, of the CmsPage into the database. 
		/// </summary>
        public override void UpdateOrVersion() {

            base.UpdateOrVersion();

            if (SaveCmsPageEvent != null) {
                SaveCmsPageEvent(this, new PageEventArgs(this.ID));
            }
        }
		
		/// <summary>
		/// Inserts the CmsPage into the database. 
		/// </summary>
        public override void Insert() {

			if(IsStatic){
                ////Create template
                //CmsTemplate t = new CmsTemplate();
                //t.FileName = this.FileName;
                //t.Title = this.Title;
                //t.TemplatePath = this.FullPath;
                //t.Description = "Generated template for static page.";
                //t.Insert();
                //this.TemplateID = t.ID;

                throw new ApplicationException("Static not supported.");
			}

            base.Insert();

			foreach(CmsRegisteredContent rc in Template.RegisteredContent){
				rc.CreateInitialContent(this.ID);
			}
		}
		
		/// <summary>
		/// Inserts this page, or a new version if its already published, into the database. 
		/// </summary>
        public override void InsertVersion() {

            base.InsertVersion();

            //saved over by SaveCmsPageEvent, so no need to copy
            foreach (CmsRegisteredContent rc in Template.RegisteredContent) {
                rc.CreateInitialContent(this.ID);
            }

        }
		
		/// <summary>
		/// Deletes this CmsPage from the database. 
		/// </summary>
		public override void PermanentlyDelete(){

			foreach(CmsRegisteredContent rc in Template.RegisteredContent){
				rc.DeleteContent(this.ID);
			}


            base.PermanentlyDelete();

            //TODO: Clean up static template?
            if(IsStatic){
                CleanUpStaticTemplate();
            }

			this.ParentDirectory.UpdateSortOrder();
			
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		private void CleanUpStaticTemplate(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IDataAccessCommand cmd = ds.CreateDataAccessCommand("SELECT COUNT(*) FROM CmsPages WHERE TemplateID = @TemplateID");
			cmd.CreateInputParameter("@TemplateID", this.TemplateID);
			int count = (int)cmd.ExecuteScalar();
			if(count == 0){
                this.Template.PermanentlyDelete();
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

            string pathTranslated = HttpContext.Current.Server.MapPath(ResolveTemplate(this.Template.Url));
            //HttpContext.Current.RewritePath(this.Template.Url, false);         
            return PageParser.GetCompiledPageInstance(url, pathTranslated, HttpContext.Current);
        }
		
		/// <summary>
		/// Resolves the path to the template for this CmsPage. 
		/// </summary>
		/// <param name="url">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public string ResolveTemplate(string url) {
            if (url.StartsWith("/")) {
                url = url.Replace(CmsContext.Current.RootDirectory.FullPath, "~");
            }
            return url;
        }

	}
	
	/// <summary>
	/// A handler for the CmsPage editing events. 
	/// </summary>
	public delegate void PageEventHandler(object sender, PageEventArgs args);
	
	/// <summary>
	/// Events fired during the editing of a CmsPage. 
	/// </summary>
	public class PageEventArgs : EventArgs {
		
		private Guid pageID;

		internal PageEventArgs(Guid pageID){
			this.pageID = pageID;
		}

		public Guid PageID {
			get{ return pageID; }
		}
	}
}
