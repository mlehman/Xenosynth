using System;
using System.IO;

namespace Xenosynth.Web {
	
	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
	/// </summary>
	internal class FileCacheStream : System.IO.Stream, IDisposable {

		private FileStream  fileStream;
		private Stream outputStream;
		private string path;

		public FileCacheStream(System.IO.Stream stream, string path) {
			this.path = path;
			outputStream = stream;
			fileStream = new FileStream(path + ".tmp", FileMode.OpenOrCreate, FileAccess.ReadWrite);
		}

		public string Path {
			get { return path; }
		}

		public override bool CanRead {
			get { return outputStream.CanRead; }
		}

		public override bool CanWrite {
			get { return outputStream.CanWrite; }
		}

		public override bool CanSeek {
			get { return outputStream.CanSeek; }
		}

		public override long Length {
			get { return outputStream.Length; }
		}

		public override long Position {
			get { return outputStream.Position; }
			set { outputStream.Position = value; }
		}

		public override int Read(byte[] buffer, int offset, int count) {
			return outputStream.Read(buffer, offset, count);
		}

		public override long Seek(long offset, System.IO.SeekOrigin direction) {
			return outputStream.Seek(offset, direction);
		}

		public override void SetLength(long length) {
			outputStream.SetLength(length);
		}

		public override void Close() {
			outputStream.Close();
			fileStream.Close();
		}

		public void MoveFile(){
			File.Delete(path);
			File.Move(path + ".tmp", path);
		}

		public override void Flush() {
			outputStream.Flush();
		}

		public override void Write(byte[] buffer, int offset, int count) {
			outputStream.Write(buffer, offset, count);

			try{
				fileStream.Write(buffer, offset, count);
			} catch {}
		}
		#region IDisposable Members

		public void Dispose() {
			fileStream.Close();
		}

		#endregion
	}
}

