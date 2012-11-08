using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inform;

//using Xenosynth.Web.UI;

namespace Xenosynth.Web {
	
	/// <summary>
	/// A CMS WebSite which holds the mapping to a Root Directory in the CMS Module. 
	/// </summary>
    [TypeMapping(TableName="xs_WebSites")]
    public class WebSite {

        [MemberMapping(PrimaryKey = true, ColumnName = "ID")]
        private Guid id;

        [MemberMapping(ColumnName = "Name", Length = 50)]
        private string name;

        [MemberMapping(ColumnName = "Description", Length = 250)]
        private string description;

        [MemberMapping(ColumnName = "RootWebDirectoryID")]
        private Guid rootWebDirectoryID;

        [MemberMapping(ColumnName = "ApplicationPath", Length = 250)]
        private string applicationPath;

        [MemberMapping(ColumnName = "Directory", Length = 500)]
        private string directory;

        //[RelationshipMapping(Name = "CmsSite_CmsDirectory", ChildMember = "rootWebDirectoryID",
        //     ParentType = typeof(CmsWebDirectory), ParentMember = "id")]
        //private ObjectCache cachedRootDirectory = new ObjectCache();

		
		/// <summary>
		/// The ID of the WebSite. 
		/// </summary>
        public Guid ID {
            get { return id; }
        }
		
		/// <summary>
		/// The name of the WebSite. 
		/// </summary>
        public string Name {
            set { name = value; }
            get { return name; }
        }
		
		/// <summary>
		/// A description for the WebSite. 
		/// </summary>
        public string Description {
            set { description = value; }
            get { return description; }
        }
		
		/// <summary>
		/// The ID of the CmsWebDirectory that is the root for the WebSite. 
		/// </summary>
        public Guid RootWebDirectoryID {
            set { rootWebDirectoryID = value; }
            get { return rootWebDirectoryID; }
        }

        public string ApplicationPath {
            set {
                applicationPath = NormalizeApplicationPath(value);
            }
            get { return applicationPath; }
        }
		
		/// <summary>
		/// Resolves the ApplicationPath for this WebSite. 
		/// </summary>
		/// <param name="applicationPath">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        private string NormalizeApplicationPath(string applicationPath) {

            if (applicationPath == null) {
                applicationPath = "/";
            } else {
                applicationPath = applicationPath.Replace("\\", "/");
                if (!applicationPath.StartsWith("/")) {
                    applicationPath = "/" + applicationPath;
                }
                if (!applicationPath.EndsWith("/")) {
                    applicationPath = applicationPath + "/";
                }
            }

            return applicationPath;
        }
		
		/// <summary>
		/// The directory for the website. 
		/// </summary>
        public string Directory {
            set { directory = value; }
            get { return directory; }
        }

        
		/// <summary>
		/// Returns the Absolute URL for the WebSite. 
		/// </summary>
        public string Url {
            get {
                //TODO: protocol settings?
                return "http://" + HostHeaderMapping.Current.HostHeaderName + ApplicationPath;
            }
        }

		/// <summary>
		/// Finds all WebSites for this application. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        //TODO: Typed?
        public static IList FindAll() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(WebSite), "ORDER BY Name");
            return cmd.Execute();
        }
		
		/// <summary>
		/// Finds the WebSite by ID. 
		/// </summary>
		/// <param name="siteID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="WebSite"/>
		/// </returns>
        public static WebSite FindByID(Guid siteID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (WebSite)ds.FindByPrimaryKey(typeof(WebSite), siteID);
        }
		
		/// <summary>
		/// Saves a newly constructed WebSite instance to the database. 
		/// </summary>
        public void Save() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            //TODO: Check ID is EmptyGuid?
            this.id = Guid.NewGuid();
            ds.Insert(this);
        }
		
		/// <summary>
		/// Updates the WebSite in the database. 
		/// </summary>
        public void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Update(this);
        }
		
		/// <summary>
		/// Deletes the WebSite from the database. 
		/// </summary>
        public void Delete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Delete(this);
        }
    }
}
