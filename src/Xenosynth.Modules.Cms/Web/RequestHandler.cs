/*
 * Copyright 2002 Fluent Consulting, Inc. All rights reserved.
 * PROPRIETARY/CONFIDENTIAL. Use is subject to license terms.
 */

using System;
using System.IO;
using System.Web; 
using System.Web.UI;
using System.Web.SessionState;

using Xenosynth.Web.UI;

namespace Xenosynth.Web {

	/// <summary>
	/// The RequestHandler handles mapping all requests that match a filepath in the cms to their request handlers.
	/// <remarks>
	/// All extensions that are used in the cms files need to mapped to this handler. If a static file matches the path, it will be server in preference to the file in the cms.
	/// </remarks>
	/// </summary>
	public class RequestHandler : IHttpHandlerFactory, IReadOnlySessionState {

		public RequestHandler() {
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
		/// <param name="requestType">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="url">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="pathTranslated">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="IHttpHandler"/>
		/// </returns>
		public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated) {

            PreventCachingInAuthoringMode(context);
            
            if (!File.Exists(pathTranslated)) {

                CmsFile f = CmsHttpContext.Current.CmsFile;
                if (f != null) {
                    return f.GetHandler(url);
                } else {
                   throw new HttpException(404, "Page not found."); 
                }
            } else {

                string extension = Path.GetExtension(pathTranslated);

                if (extension == ".aspx") {
                    return PageParser.GetCompiledPageInstance(url, pathTranslated, HttpContext.Current);
                } else {
                    return new StaticFileHandler(); 
                }
            }
		}
       
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
		public void PreventCachingInAuthoringMode(HttpContext context){
			if(context.User.Identity.IsAuthenticated){
				context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
			}
		}

		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="url">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
		public string ResolveTemplate(string url){
			if(url.StartsWith("/")){
                url = url.Replace(CmsContext.Current.RootDirectory.FullPath, "~");
			}
			return url;
		}

		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="handler">
		/// A <see cref="System.Web.IHttpHandler"/>
		/// </param>
		public void ReleaseHandler(System.Web.IHttpHandler handler) {
		
		}


	}
}
