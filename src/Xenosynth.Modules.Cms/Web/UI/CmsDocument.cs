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
	/// A CmsDocument for a binary file. 
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsFile))]
    public class CmsDocument : CmsFile, IHttpHandler {

        [MemberMapping(ColumnName="Description", Length=250)]
		private string description;

        [MemberMapping(ColumnName="Keywords", Length=250)]
		private string keywords;

		[MemberMapping(ColumnName="Length")]
		private int length;

		private HttpPostedFile postedFile;
		private string filePath;


        private static Guid typeID = new Guid("F7D6C7A7-0AB2-475c-8903-EE541011A079"); 

		
		/// <summary>
		/// A description of the CmsDocument.
		/// </summary>
        public string Description {
            get { return description; }
            set { description = value; }
        }
		
		/// <summary>
		/// Keywords for the CmsDocument. 
		/// </summary>
        public string Keywords {
            get { return keywords; }
            set { keywords = value; }
        }

		
		/// <summary>
		/// The Bytes for the binary document. 
		/// </summary>
        public byte[] Bytes {
            get {
                //TODO: Best method? Calling before saved?
                DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
                SqlDataAccessCommand cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("SELECT [Document] FROM CmsDocuments WHERE ID = @ID");
                cmd.CreateInputParameter("@ID", id);
                return (byte[])cmd.ExecuteScalar();
            }
        }

		/// <summary>
		/// The length of the binary file. 
		/// </summary>
		public int Length {
			get { return length; }
		}
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
        public override bool IsVirtual {
            get {
                return true;
            }
        }

		public CmsDocument() : base(typeID) {
		}
		
		/// <summary>
		/// Constructs a CmsDocument from a file posted in the http request. 
		/// </summary>
		/// <param name="postedFile">
		/// A <see cref="HttpPostedFile"/>
		/// </param>
		public CmsDocument(HttpPostedFile postedFile) : this() {
            this.postedFile = postedFile;
			this.FileName = Path.GetFileName(postedFile.FileName);
			this.length = postedFile.ContentLength; 
		}
		
		/// <summary>
		/// Constructs a CmsDocument from a file on disk. 
		/// </summary>
		/// <param name="fileName">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="filePath">
		/// A <see cref="System.String"/>
		/// </param>
        public CmsDocument(string fileName, string filePath)
            : this() {
            this.filePath = filePath;
            this.FileName = fileName;
			this.length = (int)new FileInfo(filePath).Length;
		}
		
		/// <summary>
		/// Inserts this CmsDocument into the database. 
		/// </summary>
		public override void Insert(){

            base.Insert();

			try {
                
                if (this.postedFile != null) {
                    this.StoreBytes(this.postedFile.InputStream);
                } else {
                    this.StoreBytes(new FileStream(this.filePath, FileMode.Open));
                }
                

			} catch (Exception e) {
				PermanentlyDelete();
				throw new ApplicationException("Upload failed" , e);
			}

		}
		
		/// <summary>
		/// Finds a CmsDocument by ID. 
		/// </summary>
		/// <param name="documentID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsDocument"/>
		/// </returns>
        public static CmsDocument FindByID(Guid documentID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (CmsDocument)ds.FindByPrimaryKey(typeof(CmsDocument), documentID);
		}
		
		/// <summary>
		/// Uploads a file posted in the http request to this CmsDocument. 
		/// </summary>
		/// <param name="postedFile">
		/// A <see cref="HttpPostedFile"/>
		/// </param>
        public void UploadDocument(HttpPostedFile postedFile) {
			this.length = postedFile.ContentLength;
            this.UpdateOrVersion();
            this.StoreBytes(postedFile.InputStream);
		}

		private void StoreBytes(Stream stream) {
			//http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpconconservingresourceswhenwritingblobvaluestosqlserver.asp
            
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");

			int bufferLen = 128;  // The size of the "chunks" of the image.

            SqlDataAccessCommand cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("UPDATE CmsDocuments SET [Document] = 0x0 WHERE ID = @ID; SELECT TEXTPTR([Document]) FROM CmsDocuments WHERE ID = @ID");
			cmd.CreateInputParameter("@ID", id);
			cmd.CreateOutputParameter("@Pointer", SqlDbType.Binary, 16);
			byte[] pointer = (byte[])cmd.ExecuteScalar();


            cmd = (SqlDataAccessCommand)ds.CreateDataAccessCommand("UPDATETEXT CmsDocuments.[Document] @Pointer @Offset 0 @Bytes");
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
		/// Opens a Stream for the binary data of this CmsDocument. 
		/// </summary>
		/// <returns>
		/// A <see cref="Stream"/>
		/// </returns>
        public override Stream Open() {
            return new MemoryStream(Bytes); //TODO: Fix this to direct stream?
        }

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


            string cachedFile = XenosynthContext.Current.FileCache.Get("Cms/Documents/" + this.ID);

            if (cachedFile != null && File.GetLastWriteTime(cachedFile) < this.DateModified) {
                cachedFile = null;
            }

            if (cachedFile == null) {
                cachedFile = XenosynthContext.Current.FileCache.Add(Bytes, "Cms/Documents/" + this.ID);
            }


            context.Response.ContentType = MimeMapping.GetMimeType(fileName);
            context.Response.Cache.SetLastModified(DateModified);
            context.Response.TransmitFile(cachedFile);
        }

        #endregion
    }

}
