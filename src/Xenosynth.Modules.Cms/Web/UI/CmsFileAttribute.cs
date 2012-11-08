using System;
using System.Collections;
using Inform;


namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// An attribute (name value pair) for a CmsFile.
	/// </summary>
	[RelationshipMapping( Name="CmsFileAttribute_CmsFile",  ChildMember="fileID", 
		 ParentType=typeof(CmsFile), ParentMember="id")]
	public class CmsFileAttribute {

		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		private Guid id;

		[MemberMapping(ColumnName="FileID")]
		private Guid fileID;
		
		//TODO: Push out to template defintions?
		//[MemberMapping(ColumnName="TemplateAttributeID")]
		//private Guid templateAttributeID;

		[MemberMapping(ColumnName="Name", Length=50)]
		private string name;

		[MemberMapping(ColumnName="Value", Length=250)]
		private string val;

		[MemberMapping(ColumnName="SortOrder")]
		private int sortOrder;

		public CmsFileAttribute() {
			id = Guid.Empty;
		}

		/// <summary>
		/// The unique identifier for this CmsFileAttribute. 
		/// </summary>
		public Guid ID {
			get { return id; }
		}
		
		/// <summary>
		/// The unique identifier for the CmsFile for this CmsFileAttribute. 
		/// </summary>
		public Guid FileID {
			set { fileID = value; }
			get { return fileID; }
		}
		
		/// <summary>
		/// The name of this CmsFileAttribute. 
		/// </summary>
		public string Name {
			set { name = value; }
			get { return name; }
		}
		
		/// <summary>
		/// The value of this CmsFileAttribute. 
		/// </summary>
		public string Value {
			set { val = value; }
			get { return val; }
		}
		
		/// <summary>
		/// The sort position of this CmsFileAttribute for the CmsFile.. 
		/// </summary>
		public int SortOrder {
			set { sortOrder = value; }
			get { return sortOrder; }
		}

		/// <summary>
		/// Finds the CmsFileAttribute by ID. 
		/// </summary>
		/// <param name="fileAttributeID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsFileAttribute"/>
		/// </returns>
		public static CmsFileAttribute FindByID(Guid fileAttributeID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return  (CmsFileAttribute)ds.FindByPrimaryKey(typeof(CmsFileAttribute),fileAttributeID); 
		}
		
		/// <summary>
		/// Finds a list of CmsFileAttributes for a CmsFile. 
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindByFileID(Guid fileID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFileAttribute), "WHERE FileID=@FileID ORDER BY SortOrder");
			cmd.CreateInputParameter("@FileID",fileID);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds all CmsFileAttributes for a CmsFile with a specified name.
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindByFileID(Guid fileID, string name){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsFileAttribute), "WHERE FileID=@FileID AND Name=@Name");
			cmd.CreateInputParameter("@FileID",fileID);
			cmd.CreateInputParameter("@Name",name);
			return cmd.Execute();
		}
		
		
		/// <summary>
		/// Finds a CmsFileAttribute for a CmsFile with the specified name.
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsFileAttribute"/>
		/// </returns>
		public static CmsFileAttribute FindSingleAttributeByFileID(Guid fileID, string name){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(CmsFileAttribute), "WHERE FileID=@FileID AND Name=@Name");
			cmd.CreateInputParameter("@FileID",fileID);
			cmd.CreateInputParameter("@Name",name);
			return (CmsFileAttribute)cmd.Execute();
		}
		
		/// <summary>
		/// Updates the sort order of the CmsFileAttributes for a CmsFile.  Used after adding or removing CmsFileAttributes. 
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		public static void UpdateSortOrder(Guid fileID){
			IList fileAttributes = FindByFileID(fileID);
			int i = 0;
			foreach(CmsFileAttribute f in fileAttributes){
				f.SortOrder = i++;
				f.Update();
			}
		}

		/// <summary>
		/// Move a CmsFileAttribute up or down in the sort order. 
		/// </summary>
		/// <param name="up">
		/// A <see cref="System.Boolean"/>
		/// </param>
		public void MoveSortOrder(bool up){
			CmsFileAttribute f = FindByNextInOrder(up);
			if(f != null){
				//swap
				int s = this.SortOrder;
				this.SortOrder = f.SortOrder;
				f.SortOrder = s;

				f.Update();
				this.Update();
			}
		}

		public  CmsFileAttribute FindByNextInOrder(bool up){

			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindObjectCommand cmd;
			if(up){
				cmd = ds.CreateFindObjectCommand(typeof(CmsFileAttribute), "WHERE FileID=@FileID AND SortOrder < @SortOrder ORDER BY SortOrder DESC", true);
			} else {
				cmd = ds.CreateFindObjectCommand(typeof(CmsFileAttribute), "WHERE FileID=@FileID AND SortOrder > @SortOrder ORDER BY SortOrder ASC", true);
			}
			cmd.CreateInputParameter("@FileID", this.FileID);
			cmd.CreateInputParameter("@SortOrder", this.SortOrder);

			return (CmsFileAttribute)cmd.Execute();
		}	
		
		/// <summary>
		/// Updates this CmsFileAttribute in the database. 
		/// </summary>
		public void Update(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Update(this);
		}
		
		/// <summary>
		/// Save a new CmsFileAttribute to the database. 
		/// </summary>
		public void Save(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			//TODO: Check ID is EmptyGuid?
			this.id = Guid.NewGuid();
			ds.Insert(this);
		}

		/// <summary>
		/// Delete this CmsFileAttribute from the database. 
		/// </summary>
		public void Delete(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Delete(this);
		}

		/// <summary>
		/// Delete all CmsFileAttributes for a CmsFile matching the specified name and value. 
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.String"/>
		/// </param>
		public static void Delete(Guid fileID, string name, string value){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IDataAccessCommand cmd = ds.CreateDataAccessCommand("DELETE FROM CmsFileAttributes WHERE FileID=@FileID AND Name=@Name AND Value=@Value");
			cmd.CreateInputParameter("@FileID", fileID);
			cmd.CreateInputParameter("@Name", name);
			cmd.CreateInputParameter("@Value", value);
			cmd.ExecuteNonQuery();
		}
		
		/// <summary>
		/// Copy the CmsFileAttribute to another CmsFile.  
		/// </summary>
		/// <param name="fileID">
		/// A <see cref="Guid"/>
		/// </param>
		public void CopyTo(Guid fileID){
			CmsFileAttribute a = new CmsFileAttribute();
			a.FileID = fileID;
			a.Name = this.Name;
			a.Value = this.Value;
			a.SortOrder = this.SortOrder;
			a.Save();
		}
	}
}
