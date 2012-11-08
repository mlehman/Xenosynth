using System;
using System.Collections;
using System.Text;
using Inform;
using Xenosynth.Modules;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// A CmsFile type. 
	/// </summary>
    public class CmsFileType {

        [MemberMapping(PrimaryKey=true, ColumnName="ID")]
        private Guid _id;

        [MemberMapping(ColumnName = "ModuleID")]
        private Guid _moduleID;

        [MemberMapping(ColumnName="Name", Length=50)]
        private string _name;

        [MemberMapping(ColumnName="Description", Length=250)]
        private string _description;

        [MemberMapping(ColumnName = "IsDirectory")]
        private bool _isDirectory;

        [MemberMapping(ColumnName = "IsVersioned")]
        private bool _isVersioned;

        [MemberMapping(ColumnName = "IsContentType")]
        private bool _isContentType;

        [MemberMapping(ColumnName="EditUrl", Length=250)]
        private string _editUrl;

        [MemberMapping(ColumnName = "CreateUrl", Length = 250)]
        private string _createUrl;

        [MemberMapping(ColumnName = "BrowseUrl", Length = 250)]
        private string _browseUrl;

        [MemberMapping(ColumnName = "DefaultAction")]
        private Action _defaultAction;
		
		/// <summary>
		/// CmsFile admin actions. 
		/// </summary>
        public enum Action {
            Create = 1,
            Edit = 2,
            Browse = 3,
            View = 4
        }
		
		/// <summary>
		/// The unique identifier for the CmsFileType. 
		/// </summary>
        public Guid ID {
            get { return _id; }
        }
		
		/// <summary>
		/// The module ID that creates this CmsFileType. 
		/// </summary>
        public Guid ModuleID {
            get { return _moduleID; }
        }
		
		/// <summary>
		/// The registered module creates this CmsFileType belongs. 
		/// </summary>
        public RegisteredModule Module {
            get { return XenosynthContext.Current.Modules[ModuleID]; }
        }
		
		/// <summary>
		/// Whether this CmsFileType is versioned. 
		/// </summary>
        public bool IsVersioned {
            get { return _isVersioned; }
        }
		
		/// <summary>
		/// Whether this CmsFileType is a directory. 
		/// </summary>
        public bool IsDirectory {
            get { return _isDirectory; }
        }
		
		/// <summary>
		/// The name of this CmsFileType. 
		/// </summary>
        public string Name {
            get { return _name; }
        }
		
		/// <summary>
		/// A description of this CmsFileType. 
		/// </summary>
        public string Description {
            get { return _description; }
        }
		
		/// <summary>
		/// The URL for the admin page to create this CmsFileType. 
		/// </summary>
        public string CreateUrl {
            get { return _createUrl; }
        }
		
		/// <summary>
		/// The URL for the admin page to edit this CmsFileType. 
		/// </summary>
        public string EditUrl {
            get { return _editUrl; }
        }
		
		/// <summary>
		/// The URL for the admin page to browse this CmsFileType. 
		/// </summary>
        public string BrowseUrl {
            get { return _browseUrl; }
        }
		
		/// <summary>
		/// The URL for the admin page of the default action for this CmsFileType. 
		/// </summary>
        public Action DefaultAction {
            get { return _defaultAction; }
        }
		
		/// <summary>
		/// The css class for this file in the admin. 
		/// </summary>
        public string CssClass {
            get {
                if (_name != null) {
                    return _name.ToLower().Replace(" ", "");
                } else {
                    return string.Empty;
                }
            }
        }
		
		/// <summary>
		/// If this is a directory, the CmsFileTypes that are allowed.
		/// </summary>
        public IList AllowedFileTypes {
            get { return FindAllowedTypes(this.ID); }
        }

        public CmsFileType() {
        }

        public CmsFileType(Guid id, Guid moduleID, String name, String description, bool isDirectory, String createUrl, String editUrl, String browseUrl) {
            _id = id;
            _moduleID = moduleID;
            _name = name;
            _description = description;
            _isDirectory = isDirectory;
            _createUrl = createUrl;
            _editUrl = editUrl;
            _browseUrl = browseUrl;
        }
		
		/// <summary>
		/// Insert this CmsFileType in the database. 
		/// </summary>
        public void Insert() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Insert(this);
        }
		
		/// <summary>
		/// Update this CmsFileType in the database. 
		/// </summary>
        public void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Update(this);
        }
		
		/// <summary>
		/// Delete this CmsFileType in the database. 
		/// </summary>
        public void Delete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Delete(this);
        }

		/// <summary>
		/// Find this CmsFileType by ID.
		/// </summary>
        public static CmsFileType FindByID(Guid id) {
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return (CmsFileType)ds.FindByPrimaryKey(typeof(CmsFileType), id);
		}
		
		/// <summary>
		/// Gets the list of CmsFileTypes that are allowed if this is a directory. 
		/// </summary>
		/// <param name="fileTypeID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindAllowedTypes(Guid fileTypeID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFileType), "INNER JOIN CmsFileTypeAllowedTypes ON CmsFileTypes.ID = CmsFileTypeAllowedTypes.FileTypeID WHERE DirectoryTypeID = @DirectoryTypeID ORDER BY CmsFileTypeAllowedTypes.SortOrder");
            cmd.CreateInputParameter("@DirectoryTypeID", fileTypeID);
            return cmd.Execute();
        }
		
		/// <summary>
		/// Finds all CmsFileTypes. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindAll() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFileType), "ORDER BY Name");
            return cmd.Execute();
        }

    }
}
