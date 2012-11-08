using System;
using System.Collections;
using Inform;

namespace Xenosynth.Web {
	
	/// <summary>
	/// CmsResource.
	/// </summary>
	public class CmsResource {

		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		private Guid id;

		[MemberMapping(ColumnName="Name", Length=250)]
		private string name;

		[MemberMapping(ColumnName="Category", Length=250)]
		private string category;

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description;

		[MemberMapping(ColumnName="Value", Length=4000)]
		private string val;

		public Guid ID {
			get { return id; }
		}
		
		/// <summary>
		/// The user-friendly name of the CmsResource. 
		/// </summary>
		public string Name {
			set { name = value; }
			get { return name; }
		}
		
		/// <summary>
		/// The category to organize the CmsResource 
		/// </summary>
		public string Category {
			set { category = value; }
			get { return category; }
		}
		
		/// <summary>
		/// A description of the CmsResource. 
		/// </summary>
		public string Description {
			set { description = value; }
			get { return description; }
		}
		
		/// <summary>
		/// A value for the CmsResource. 
		/// </summary>
		public string Value {
			set { val = value; }
			get { return val; }
		}

		/// <summary>
		/// Saves the CmsResource to the database. 
		/// </summary>
		public void Save(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			//TODO: Check ID is EmptyGuid?
			this.id = Guid.NewGuid();
			ds.Insert(this);
		}
		
		/// <summary>
		/// Updates the CmsResource in the database. 
		/// </summary>
		public void Update(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Update(this);
		}
		
		/// <summary>
		/// Deletes the CmsResource in the database. 
		/// </summary>
		public void Delete(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Delete(this);
		}
		
		/// <summary>
		/// Finds all CmsResources in the database. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsResource), "ORDER BY Category, Name");
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds a CmsResource by ID. 
		/// </summary>
		/// <param name="resourceID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsResource"/>
		/// </returns>
		public static CmsResource FindByID(Guid resourceID){
			//TODO: Push internal and add cache?
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return (CmsResource)ds.FindByPrimaryKey(typeof(CmsResource), resourceID);
		}
	}
}
