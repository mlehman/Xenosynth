using System;
using Inform;

namespace Xenosynth.Web {
	
	/// <summary>
	/// A CmsGalleryTemplate organizes a sub set of CmsTemplates. This class may be deprecated.  This class supports the Xenosynth CMS Module and is not intended to be used directly from your code.
	/// </summary>
	public class CmsGalleryTemplate {
		
		[MemberMapping(PrimaryKey=true, Identity=true, ColumnName="ID")]
		private int id;

		[MemberMapping(ColumnName="GalleryID", AllowNulls=true)]
		private Guid galleryID;

		[MemberMapping(ColumnName="TemplateID", AllowNulls=true)]
		private Guid templateID;
		
		/// <summary>
		/// The unique identifier for the CmsTemplateGallery. 
		/// </summary>
		public int ID {
			get { return id; }
		}
		
		/// <summary>
		/// The unique identifier for the CmsTemplateGallery. 
		/// </summary>
		public Guid GalleryID {
			set { galleryID = value; }
			get { return galleryID; }
		}
		
		/// <summary>
		/// The unique identifier of the CmsTemplate. 
		/// </summary>
		public Guid TemplateID {
			set { templateID = value; }
			get { return templateID; }
		}
		
		/// <summary>
		/// Saves the a new CmsGalleryTemplate to the database. 
		/// </summary>
		public void Save(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Insert(this);
		}
		
		/// <summary>
		/// Deletes the CmsGalleryTemplate from the database.
		/// </summary>
		/// <param name="galleryID">
		/// A <see cref="Guid"/>
		/// </param>
		public static void DeleteByGalleryID(Guid galleryID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IDataAccessCommand cmd = ds.CreateDataAccessCommand("DELETE FROM CmsGalleryTemplates WHERE GalleryID=@GalleryID");
			cmd.CreateInputParameter("@GalleryID", galleryID);
			cmd.ExecuteNonQuery();
		}

		

		
	}
}
