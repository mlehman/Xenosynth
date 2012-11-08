using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using Inform;


namespace Xenosynth.Web {

	/// <summary>
	/// Summary description for CmsHostHeaderMapping.
	/// </summary>
    [TypeMapping(TableName="xs_HostHeaderMappings")]
	public class HostHeaderMapping {

        private static Dictionary<string, HostHeaderMapping> hostHeadersCache = new Dictionary<string, HostHeaderMapping>();

		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		private Guid id;

        [MemberMapping(ColumnName = "WebSiteID")]
        private Guid webSiteID;

		[MemberMapping(ColumnName="HostHeaderName", Length=250)]
		private string hostHeaderName;

        [MemberMapping(ColumnName = "IsDefault")]
        private bool isDefault;

        [RelationshipMapping(Name = "HostHeader_WebSite", ChildMember = "webSiteID", 
			 ParentType=typeof(WebSite), ParentMember="id")]
        private ObjectCache cachedSite = new ObjectCache();

		public HostHeaderMapping() {
			id = Guid.Empty;
		}

		public Guid ID {
			get { return id; }
		}

        public Guid WebSiteID {
            set { webSiteID = value; }
            get { return webSiteID; }
		}

		public WebSite Site {
			get {
                return (WebSite)cachedSite.CachedObject;
			}
		}

		public string HostHeaderName {
			set { hostHeaderName = value; }
			get { return hostHeaderName; }
		}

        public bool IsDefault {
            set { isDefault = value; }
            get { return isDefault; }
        }

        public static HostHeaderMapping Current {

			//TODO: Can cache, yet refresh ok?
			get {
                HostHeaderMapping mapping = null;
				if( (mapping = (HostHeaderMapping)HttpContext.Current.Items["Xenosynth.HostHeaderMapping"]) == null){

					string hostHeaderName = HttpContext.Current.Request.Url.Host;
                    
                    if (!hostHeadersCache.TryGetValue(hostHeaderName, out mapping)) {
                        mapping = HostHeaderMapping.FindHostHeaderMapping(hostHeaderName);
                        if (mapping == null) {
                            throw new XenosynthException(string.Format("No host header mapping is defined for '{0}'", hostHeaderName));
                        }
                        hostHeadersCache[hostHeaderName] = mapping;
                    }
                    HttpContext.Current.Items["Xenosynth.HostHeaderMapping"] = mapping;

				}

                return mapping;
			}
		}

		public void Insert(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			//TODO: Check ID is EmptyGuid?
			this.id = Guid.NewGuid();
			ds.Insert(this);
		}

		public void Update(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Update(this);
		}

		public void Delete(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Delete(this);
		}


		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(HostHeaderMapping), "ORDER BY HostHeaderName");
			return cmd.Execute();
		}

        public static HostHeaderMapping FindHostHeaderMapping(string hostHeaderName) {

            HostHeaderMapping h = null;
			
			//TODO: Push internal and add cache?
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(HostHeaderMapping), "WHERE HostHeaderName = @HostHeaderName");
			cmd.CreateInputParameter("@HostHeaderName", hostHeaderName);
            h = (HostHeaderMapping)cmd.Execute();

			if(h == null){
				//find wildcard
				cmd = ds.CreateFindObjectCommand(typeof(HostHeaderMapping), "WHERE HostHeaderName = @HostHeaderName");
				cmd.CreateInputParameter("@HostHeaderName", "*");
                h = (HostHeaderMapping)cmd.Execute();
			}

			return h;

		}

        public static HostHeaderMapping FindByID(Guid hostHeaderMappingID) {
			//TODO: Push internal and add cache?
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (HostHeaderMapping)ds.FindByPrimaryKey(typeof(HostHeaderMapping), hostHeaderMappingID);
		}

        public static IList FindBySite(Guid siteID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(HostHeaderMapping), "WHERE WebSiteID = @WebSiteID ORDER BY HostHeaderName");
            cmd.CreateInputParameter("@WebSiteID", siteID);
            return cmd.Execute();
        }


	}
}
