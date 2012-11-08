using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using Inform;

namespace Xenosynth.Web.UI {

	/// <summary>
	/// A template for a CmsPage.
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsFile))]
    public class CmsTemplate : CmsFile {

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description;

		[RelationshipMapping( Name="CmsTemplate_CmsRegisteredContent",  ChildMember="templateID", 
			 ChildType=typeof(CmsRegisteredContent), ParentMember="id")]
		private CollectionCache registeredContent = new CollectionCache();

        private static Guid typeID = new Guid("45919578-6524-4307-bb43-4ab93c6d0c9e");

		
		/// <summary>
		/// A description of the CmsTemplate. 
		/// </summary>
		public string Description {
			set { description = value; }
			get { return description; }
		}


		/// <summary>
		/// List of CmsRegisteredContent for this CmsTemplate. 
		/// </summary>
		public IList RegisteredContent {
			get { return registeredContent.CachedCollection; }
		}

        public CmsTemplate()
            : base(typeID) {
		}


		/// <summary>
		/// A list of all CmsTemplates. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsTemplate), "ORDER BY Title", true);
			return cmd.Execute();
		}
		
		/// <summary>
		/// The number of pages using this CmsTemplate. 
		/// </summary>
		/// <returns>
		/// A <see cref="System.Int32"/>
		/// </returns>
		private int CountPagesUsing(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			IDataAccessCommand cmd = ds.CreateDataAccessCommand("SELECT COUNT(*) FROM CmsPages WHERE TemplateID = @TemplateID");
			cmd.CreateInputParameter("@TemplateID", this.ID);

			return (int)cmd.ExecuteScalar();
		}
		
		/// <summary>
		/// Finds a CmsTemplate by ID. 
		/// </summary>
		/// <param name="id">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsTemplate"/>
		/// </returns>
		public static CmsTemplate FindByID(Guid id){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			return (CmsTemplate)ds.FindByPrimaryKey(typeof(CmsTemplate),id);
		}
		
		/// <summary>
		/// Delete this CmsTemplate from the database.  
		/// </summary>
        public override void PermanentlyDelete() {

			if(CountPagesUsing() > 0 ){
				throw new ApplicationException("Can not delete template that is being used.");
			}

			foreach(CmsRegisteredContent rc in RegisteredContent){
				rc.Unregister();
			}

            base.PermanentlyDelete();
		}
		
		/// <summary>
		/// Marks this CmsTemplate as deleted in the database. 
		/// </summary>
        public override void Delete() {

            if (CountPagesUsing() > 0) {
                throw new ApplicationException("Can not delete template that is being used.");
            }

            base.Delete();
        }

	}
}
