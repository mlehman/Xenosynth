using System;
using System.Collections;
using System.IO;
using System.Web;
using Xenosynth.Configuration;

namespace Xenosynth.Web {

	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code.
	/// </summary>
	internal class FileRequestCache {

		string fileCachePath;
		Hashtable files = new Hashtable();

        public FileRequestCache() {
		}

		public string FileCachePath {
			get {
				if(fileCachePath == null){
                    fileCachePath = (string)XenosynthContext.Current.Configuration.GetValue("xenosynth.installation.fileCache", true);
				}
				return fileCachePath;
			}
		}

		public void ResolveRequestCache(){

			//TODO: When to enable?
			return;

			HttpContext context = HttpContext.Current;
			HttpResponse response = context.Response;
			HttpRequest request = context.Request;

			if(request.RequestType == "POST"){
				return;
			}

			string path = null;
			if((path = (string)files[request.Path]) != null){

				FileInfo fi = new FileInfo (path);
				DateTime lastWT = fi.LastWriteTime.ToUniversalTime ();
				response.AddHeader ("Last-Modified", lastWT.ToString ("r"));
				response.WriteFile (path);
				context.ApplicationInstance.CompleteRequest();
				//response.ContentType = MimeTypes.GetMimeType (path);

			} else {
			
				path = request.Path.Substring(1);
				path = Path.Combine(FileCachePath, path);
				
				EnsureDirectoryExists(path);

				FileCacheStream fcs = new FileCacheStream(response.Filter, path);
				response.Filter = fcs;
				context.Items.Add("FileCacheStream", fcs);
			}
		}

		private static void EnsureDirectoryExists(string path){
			path = Path.GetDirectoryName(path);
			if(!Directory.Exists(path)){
				Directory.CreateDirectory(path);
			}
		}

		public void UpdateRequestCache(){

			HttpContext context = HttpContext.Current;
			HttpRequest request = context.Request;
			FileCacheStream fcs = (FileCacheStream)context.Items["FileCacheStream"];
			if(fcs != null){
				fcs.MoveFile();
				files[request.Path] = fcs.Path;
			}
		}
	}
}
