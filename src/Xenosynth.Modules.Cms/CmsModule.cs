using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;

using Inform;
using Xenosynth.Modules;

namespace Xenosynth.Modules.Cms {
	
	/// <summary>
	/// This class supports the Xenosynth Framework and is not intended to be used directly from your code. 
	/// </summary>
    public class CmsModule : IModule {
		
		/// <summary>
		/// The default page in the Xenosynth Admin. 
		/// </summary>
        public string DefaultUrl {
            get { return "~/Modules/Cms/Directory/BrowseDirectory.aspx"; }
        }
		
		/// <summary>
		/// The default configuration page in the Xenosynth Admin.
		/// </summary>
        public string ConfigurationUrl {
            get { return "~/Modules/Cms/Configuration/Default.aspx"; }
        }
		
		/// <summary>
		/// This method supports the Xenosynth Framework and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="application">
		/// A <see cref="HttpApplication"/>
		/// </param>
        public void Init(HttpApplication application) {
            //application.BeginRequest += new EventHandler(OnPreRequestHandlerExecute);
        }
		
		/// <summary>
		/// This method supports the Xenosynth Framework and is not intended to be used directly from your code.  
		/// </summary>
        public void Start() {


            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            //ds.Name = "Xenosynth";
            //ds.Connection.ConnectionString = ConfigurationManager.ConnectionStrings["Xenosynth"].ConnectionString;
            //ds.Settings.CreateOnInitialize = false;
            //ds.Settings.AutoGenerate = false;
            //ds.Settings.UseStoredProcedures = false;
            //ds.Settings.FindObjectReturnsNull = true;

            Inform.Common.DataStorageManager m = Inform.Common.DataStorageManager.GetDataStorageManager(ds);

            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.CmsRegisteredContent)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsFileType)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsFile)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsFileAttribute)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsDirectory)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsTemplate)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsTemplateGallery)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.CmsGalleryTemplate)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsImageGallery)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsDocumentLibrary)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsWebDirectory)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsPage)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsImage)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsDocument)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.UI.CmsShortcut)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.CmsResource)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Data.LiteralDataItem)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Web.SearchResult)));
           
           

            //DataStoreServices.RegisterDataStore(ds);
        }
    }

}
