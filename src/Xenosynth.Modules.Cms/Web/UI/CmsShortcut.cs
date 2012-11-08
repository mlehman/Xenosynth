using System;
using System.Collections;
using System.Data.SqlTypes;
using Inform;
using System.Web;

using Xenosynth.Web;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// Provides a link to another url. 
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsFile))]
    public class CmsShortcut : CmsFile, IHttpHandler {

        [MemberMapping(ColumnName = "IsExternal", AllowNulls = true)]
        private bool isExternal;

        [MemberMapping(ColumnName = "InternalFileID", AllowNulls = true)]
        private SqlGuid internalFileID;

        [MemberMapping(ColumnName = "ExternalUrl", Length = 500, AllowNulls = true)]
        private string externalUrl;

        private static Guid typeID = new Guid("4b5df903-7a51-4f41-982b-2ddf3bb18671");

        public CmsShortcut()
            : base(typeID) {
        }
		
		/// <summary>
		/// Whether this is a link to an internal or external to the CMS. 
		/// </summary>
        public bool IsExternal {
            get { return isExternal; }
            set { isExternal = value; }
        }
		
		/// <summary>
		/// The CmsFileID for an internal link. 
		/// </summary>
        public SqlGuid InternalFileID {
            get { return internalFileID; }
            set { internalFileID = value; }
        }
		
		/// <summary>
		/// The url for an external link. 
		/// </summary>
        public string ExternalUrl {
            get { return externalUrl; }
            set { externalUrl = value; }
        }

        //public override string Url {
        //    get {
        //        if (externalUrl != null && externalUrl.StartsWith("~")) {
        //            return VirtualPathUtility.ToAbsolute(externalUrl);
        //        } else {
        //            return externalUrl;
        //        }
        //    }
        //}
		
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
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
        public void ProcessRequest(HttpContext context) {
            context.Response.Redirect(this.ExternalUrl);
        }

        #endregion

    }
}
