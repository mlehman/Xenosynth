using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Inform;

namespace Xenosynth.Modules.Blog {
    public class BlogModule : IModule {

        public string DefaultUrl {
            get { return "~/Modules/Blog/Default.aspx"; }
        }

        public string ConfigurationUrl {
            get { return "~/Modules/Blog/Configuration/Default.aspx"; }
        }

        public void Init(HttpApplication application) {
            //application.BeginRequest += new EventHandler(OnPreRequestHandlerExecute);
        }

        public void Start() {


            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

            Inform.Common.DataStorageManager m = Inform.Common.DataStorageManager.GetDataStorageManager(ds);

            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Modules.Blog.Blog)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Modules.Blog.BlogPost)));
            m.AddTypeMapping(m.CreateTypeMappingFromAttributes(typeof(Xenosynth.Modules.Blog.BlogComment)));

        }
    }

}
