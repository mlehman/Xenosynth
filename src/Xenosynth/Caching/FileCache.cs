using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Xenosynth.Caching {
	
	/// <summary>
	/// The FileCache provides a simple service for caching binary data to temporary files. 
	/// <remarks>
	/// The FileCache is a utility class to assist with storing run-time generated files (such as image thumbnails) or caching resources from external sources (such as blobs from the database).
	/// </remarks>
	/// </summary>
    public class FileCache {

        private string fileCachePath;

        internal FileCache(string fileCachePath) {
            this.fileCachePath = fileCachePath;
        }
		
		/// <summary>
		/// The directory for file caching. 
		/// </summary>
        public string FileCachePath {
            get {
                return fileCachePath;
            }
        }

        private static void EnsureDirectoryExists(string path) {
            path = Path.GetDirectoryName(path);
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
        }

		/// <summary>
		/// Writes the binary data to a file in the file cache using the filename.
		/// </summary>
		/// <param name="bytes">
		/// A <see cref="System.Byte[]"/> to be written to the file.
		/// </param>
		/// <param name="filename">
		/// A <see cref="System.String"/> the filename.  The file name can include a relative path.
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/> The absolute path of the file saved in the cache.
		/// </returns>
        public string Add(byte[] bytes, string filename) {

            string fullPath = Path.Combine(fileCachePath, filename);

            EnsureDirectoryExists(fullPath);

            FileStream fs = new FileStream(fullPath, FileMode.Create);
            try {
                fs.Write(bytes, 0, bytes.Length);
            } finally {
                fs.Close();
            }

            return fullPath;

        }

		/// <summary>
		/// Get the absolute path of a file saved in the cache, using the same filename it was added to the cache. 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>.
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/> for the full path of the file.
		/// </returns>
        public string Get(string filename) {

            string fullPath = Path.Combine(fileCachePath, filename);

            if (File.Exists(fullPath)) {
                return fullPath;
            } else {
                return null;
            }

        }

    }
}
