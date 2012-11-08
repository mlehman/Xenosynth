using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Inform;
using Inform.Sql;
using System.Drawing;

namespace Xenosynth.Web.UI {
	
	/// <summary>
	/// Used for storing images in the CMS. 
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsFile))]
    public class CmsImage : CmsFile, IHttpHandler {

        [MemberMapping(ColumnName="Description", Length=250)]
		private string description;

        [MemberMapping(ColumnName="Keywords", Length=250)]
		private string keywords;

        [MemberMapping(ColumnName = "AltText", Length = 50)]
        private string altText;

        [MemberMapping(ColumnName = "Height")]
        private int height;

        [MemberMapping(ColumnName = "Width")]
        private int width;

		[MemberMapping(ColumnName="Length")]
		private int length;

		private HttpPostedFile postedFile;
		private string filePath;


        private static Guid typeID = new Guid("b2db2ed4-df75-4870-85f3-e9a5700fb6ab");

		/// <summary>
		/// The description of the CmsImage. 
		/// </summary>
        public string Description {
            get { return description; }
            set { description = value; }
        }
		
		/// <summary>
		/// The alt text for the CmsImage. 
		/// </summary>
        public string AltText {
            get { return altText; }
            set { altText = value; }
        }
		
		/// <summary>
		/// The keywords for the CmsImage.
		/// </summary>
        public string Keywords {
            get { return keywords; }
            set { keywords = value; }
        }
		
		/// <summary>
		/// The width and height of the CmsImage. 
		/// </summary>
        public Size Size {
            get { return new Size(width,height); }
        }
		
		/// <summary>
		/// The binary data for the image. 
		/// </summary>
        public byte[] Bytes {
            get {
                //TODO: Best method? Calling before saved?
                DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
                SqlDataAccessCommand cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("SELECT [Image] FROM CmsImages WHERE ID = @ID");
                cmd.CreateInputParameter("@ID", id);
                return (byte[])cmd.ExecuteScalar();
            }
        }

		/// <summary>
		/// The length of the image in bytes. 
		/// </summary>
		public int Length {
			get { return length; }
		}
		
		/// <summary>
		/// The height of the image. 
		/// </summary>
        public int Height {
            get { return height; }
        }
		
		/// <summary>
		/// The width of the image. 
		/// </summary>
        public int Width {
            get { return width; }
        }

        public override bool IsVirtual {
            get {
                return true;
            }
        }

		public CmsImage() : base(typeID) {
		}
		
		/// <summary>
		/// Constructs an new CmsImage from a file posted in the http request. 
		/// </summary>
		/// <param name="postedFile">
		/// A <see cref="HttpPostedFile"/>
		/// </param>
		public CmsImage(HttpPostedFile postedFile) : this() {
            CheckFile(postedFile.FileName);

            this.postedFile = postedFile;
			this.FileName = Path.GetFileName(postedFile.FileName);
			this.length = postedFile.ContentLength; 
		}
		
		/// <summary>
		/// Constucts a new CmsImage from a file on disk/ 
		/// </summary>
		/// <param name="fileName">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="filePath">
		/// A <see cref="System.String"/>
		/// </param>
        public CmsImage(string fileName, string filePath) : this() {
            CheckFile(fileName);

            this.filePath = filePath;
            this.FileName = fileName;
			this.length = (int)new FileInfo(filePath).Length;
		}
		
		/// <summary>
		/// Checks that the filename is a valid image. 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
        private void CheckFile(string filename) {
            string mimeType = MimeMapping.GetMimeType(filename);
            if (!mimeType.StartsWith("image")) {
                throw new InvalidDataException("The file is not a recognized image format.");
            }
        }

		/// <summary>
		/// Inserts a new CmsImage into the database. 
		/// </summary>
		public override void Insert(){

            base.Insert();

			try {
                

                if (this.postedFile != null) {
                    this.StoreBytes(this.postedFile.InputStream);
                } else {
                    this.StoreBytes(new FileStream(this.filePath, FileMode.Open));
                }


                //TODO: save to temp file first?
                UpdateImageData();
                

			} catch (Exception e) {
				PermanentlyDelete();
				throw new ApplicationException("Upload failed" , e);
			}

		}

        private void UpdateImageData() {
            Image img = Image.FromStream(new MemoryStream(Bytes));

            this.width = img.Width;
            this.height = img.Height;

            this.Update();
        }
		
		/// <summary>
		/// Finds a CmsImage by ID. 
		/// </summary>
		/// <param name="imageID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsImage"/>
		/// </returns>
        public static CmsImage FindByID(Guid imageID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (CmsImage)ds.FindByPrimaryKey(typeof(CmsImage), imageID);
		}
		
		/// <summary>
		/// Updloads a new image to the CmsImage from a file posted in the http request. 
		/// </summary>
		/// <param name="postedFile">
		/// A <see cref="HttpPostedFile"/>
		/// </param>
        public void UploadImage(HttpPostedFile postedFile) {
			this.length = postedFile.ContentLength;
            this.UpdateOrVersion();
            this.StoreBytes(postedFile.InputStream);
            UpdateImageData();
		}

		private void StoreBytes(Stream stream) {
			//http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconconservingresourceswhenwritingblobvaluestosqlserver.asp
            
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

			int bufferLen = 128;  // The size of the "chunks" of the image.

            SqlDataAccessCommand cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("UPDATE CmsImages SET [Image] = 0x0 WHERE ID = @ID; SELECT TEXTPTR([Image]) FROM CmsImages WHERE ID = @ID");
			cmd.CreateInputParameter("@ID", id);
			cmd.CreateOutputParameter("@Pointer", SqlDbType.Binary, 16);
			byte[] pointer = (byte[])cmd.ExecuteScalar();


            cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("UPDATETEXT CmsImages.[Image] @Pointer @Offset 0 @Bytes");
			cmd.CreateInputParameter("@Pointer", SqlDbType.Binary, 16, pointer);
			IDbDataParameter paramBytes = cmd.CreateInputParameter("@Bytes", SqlDbType.Image, bufferLen, null);
			IDbDataParameter paramOffset = cmd.CreateInputParameter("@Offset", SqlDbType.Int, 0);

			//''''''''''''''''''''''''''''''''''
			// Read the image in and write it to the database 128 (bufferLen) bytes at a time.
			// Tune bufferLen for best performance. Larger values write faster, but
			// use more system resources.

            BinaryReader br = new BinaryReader(stream);
			

			byte[] buffer = br.ReadBytes(bufferLen);
			int offset_ctr = 0;

			while (buffer.Length > 0) {
				paramBytes.Value = buffer;
				cmd.ExecuteNonQuery();
				offset_ctr += bufferLen;
				paramOffset.Value = offset_ctr;
				buffer = br.ReadBytes(bufferLen);
			}

			br.Close();
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <returns>
		/// A <see cref="Stream"/>
		/// </returns>
        public override Stream Open() {
            return new MemoryStream(Bytes); //TODO: Fix this to direct stream?
        }
		
		/// <summary>
		/// Send the image as a download to the current http request. Clears the reqeust and ends it when competed. 
		/// </summary>
		public void BeginDownload(){

			HttpResponse Response = HttpContext.Current.Response;

			Response.Clear();
			Response.ContentType = "application/octet-stream";
			Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);

            Response.BinaryWrite(Bytes);
			Response.End();
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
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="context">
		/// A <see cref="HttpContext"/>
		/// </param>
        public void ProcessRequest(HttpContext context) {

            try {
                string strHeader = context.Request.Headers["If-Modified-Since"];
                if (strHeader != null) {
                    DateTime dtIfModifiedSince = DateTime.ParseExact(strHeader, "r", null);
                    DateTime ftime = this.DateModified.ToUniversalTime();
                    if (ftime <= dtIfModifiedSince) {
                        context.Response.StatusCode = 304;
                        return;
                    }
                }
            } catch { }

            string cachedFile = XenosynthContext.Current.FileCache.Get("Cms/Images/" + this.ID);

            if (cachedFile != null && File.GetLastWriteTime(cachedFile) < this.DateModified) {
                cachedFile = null;
            }

            if (cachedFile == null) {
                cachedFile = XenosynthContext.Current.FileCache.Add(Bytes, "Cms/Images/" + this.ID);
            }

            context.Response.ContentType = MimeMapping.GetMimeType(fileName);
            context.Response.Cache.SetLastModified(DateModified);
            context.Response.Cache.AppendCacheExtension("post-check=900,pre-check=3600");
            context.Response.TransmitFile(cachedFile);
        }

        #endregion
    }

}
