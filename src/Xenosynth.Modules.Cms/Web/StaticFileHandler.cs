using System;
using System.IO;
using System.Web; 

namespace Xenosynth.Web
{
	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
	/// </summary>
	class StaticFileHandler : IHttpHandler {

        public StaticFileHandler() {
        }

		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
		public void ProcessRequest (HttpContext context) {

			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			string fileName = request.PhysicalPath;

			FileInfo fi = new FileInfo (fileName);
			if (!fi.Exists)
				throw new HttpException (404, "File '" + request.FilePath + "' not found.");

			if ((fi.Attributes & FileAttributes.Directory) != 0) {
				response.Redirect (request.Path + '/');
				return;
			}

			string strHeader = request.Headers ["If-Modified-Since"];
			try {
				if (strHeader != null) {
					DateTime dtIfModifiedSince = DateTime.ParseExact (strHeader, "r", null);
					DateTime ftime = fi.LastWriteTime.ToUniversalTime();
					if (ftime <= dtIfModifiedSince) {
						response.StatusCode = 304;
						return;
					}
				}
			} catch { } 

			try {
				response.Cache.SetLastModified(fi.LastWriteTime.ToUniversalTime ());

                string mimeType = MimeMapping.GetMimeType(fileName);
                context.Response.ContentType = mimeType;

                if (mimeType.StartsWith("image")) {
                    response.Cache.AppendCacheExtension("post-check=900,pre-check=3600");
                }
                
				response.TransmitFile(fileName);

			} catch (Exception) {
				throw new HttpException (403, "Forbidden.");
			}
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code.  
		/// </summary>
		public bool IsReusable {
			get {
				return true;
			}
		}
	}

}
