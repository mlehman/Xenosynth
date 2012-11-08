using System;
using System.Collections;
using Inform;

using Xenosynth.Web.UI;

namespace Xenosynth.Web.UI {
	/// <summary>
	/// A directory for CmsTemplates.
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsDirectory))]
	public class CmsTemplateGallery : CmsDirectory {

        private static Guid typeID = new Guid("{794CDE4D-601C-4201-8ED0-1D4F8A28CDDB}");

        public IList Templates {
            get {
                return FindTemplates();
            }
        }



        public CmsTemplateGallery()
            : base(typeID) {
            this.IsHidden = true;
		}


		/// <summary>
		/// A list of all CmsTemplateGalleries. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsTemplateGallery), null, true);
			return cmd.Execute();
		}
		
		/// <summary>
		/// A list of CmsTemplates in the CmsTemplateGallery.
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		private IList FindTemplates(){
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsTemplate), "WHERE CmsFiles.ParentID = @GalleryID ORDER BY Title", true); //TODO: expand recursive?
            cmd.CreateInputParameter("@GalleryID", this.ID);
            return cmd.Execute();
		}
		
		/// <summary>
		/// Find a CmsTemplateGallery by ID. 
		/// </summary>
		/// <param name="galleryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsTemplateGallery"/>
		/// </returns>
		public static CmsTemplateGallery FindByID(Guid galleryID){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return (CmsTemplateGallery)ds.FindByPrimaryKey(typeof(CmsTemplateGallery), galleryID);  
		}

        //public void Save(){
        //    DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
        //    //TODO: Check ID is EmptyGuid?
        //    this.id = Guid.NewGuid();
        //    ds.Insert(this);
        //}

        //public void Update(){
        //    DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
        //    ds.Update(this);
        //}
		
		/// <summary>
		/// Marks the CmsTemplateGallery as deleted in the database. 
		/// </summary>
		public override void Delete(){
			CmsWebDirectory.RemoveGallery(this.ID);
            base.Delete();
		}

        //public void RemoveTemplate(Guid templateID){
        //    DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
        //    IDataAccessCommand cmd = ds.CreateDataAccessCommand("DELETE FROM CmsGalleryTemplates WHERE TemplateID=@TemplateID AND GalleryID=@GalleryID");
        //    cmd.CreateInputParameter("@TemplateID", templateID);
        //    cmd.CreateInputParameter("@GalleryID", this.ID);
        //    cmd.ExecuteNonQuery();
        //}

	}
}
