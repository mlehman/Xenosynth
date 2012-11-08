using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Inform;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;


namespace Xenosynth.Web.UI {
	
	/// <summary>
    /// A CmsDocumentLibrary is the directory for holding CmsDocuments.
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsDirectory))]
    public class CmsDocumentLibrary : CmsDirectory, IHttpHandler {

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description; 

        private static Guid typeID = new Guid("8D4C2788-8F19-4d4f-8ED4-A972E5E29438");

        public string Description {
			set { description = value; }
			get { return description; }
		}


        public CmsDocumentLibrary()
            : base(typeID) {
        }

		/// <summary>
		/// Finds all CmsDocumentLibraries. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsDocumentLibrary), "ORDER BY FullPath", true);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds all roots CmsDocumentLibraries. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindAllRoot() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsDocumentLibrary), "LEFT OUTER JOIN CmsFiles AS Parents ON Parents.ID = CmsFiles.ParentID WHERE NOT CmsFiles.FileTypeID = Parents.FileTypeID ORDER BY CmsFiles.FullPath", true);
            return cmd.Execute();
        }
		
		/// <summary>
		/// Find a CmsDocumentLibrary by ID. 
		/// </summary>
		/// <param name="libraryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsDocumentLibrary"/>
		/// </returns>
        public static CmsDocumentLibrary FindByID(Guid libraryID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (CmsDocumentLibrary)ds.FindByPrimaryKey(typeof(CmsDocumentLibrary), libraryID);
        }
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="virtualPath">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Stream"/>
		/// </returns>
        public override Stream Open(string virtualPath) {
            throw new NotImplementedException();
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
            return this;
        }
      
        #region IHttpHandler Members
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
        public bool IsReusable {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context) {


        }

        #endregion

        
    }

}