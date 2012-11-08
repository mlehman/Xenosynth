using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Inform;
using System.Collections.ObjectModel;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web;


namespace Xenosynth.Web.UI {
	
	/// <summary>
    /// A directory for CmsImages.
	/// </summary>
    [TypeMapping(BaseType = typeof(CmsDirectory))]
    public class CmsImageGallery : CmsDirectory, IHttpHandler {

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description; 

        private ReadOnlyCollection<ImageSize> imageSizes;

        private static Guid typeID = new Guid("17062b5b-e5e0-4c5c-aaa0-a10c324b5c7d");

        public string Description {
			set { description = value; }
			get { return description; }
		}
		
		/// <summary>
		/// Returns the list of defined image sizes fore re-sizing. 
		/// </summary>
        public ReadOnlyCollection<ImageSize> ImageSizes {
            get {
                if(imageSizes == null){
                    List<ImageSize> l = new List<ImageSize>();
                    l.Add(new ImageSize("Square","q",75, false));
                    l.Add(new ImageSize("Thumbnail", "t", 100, true));
                    l.Add(new ImageSize("Small", "s", 240, true));
                    l.Add(new ImageSize("Medium", "m", 752, true));
                    l.Add(new ImageSize("Large", "l", 1024, true));
                    imageSizes = l.AsReadOnly();
                }
                return imageSizes;
            }
        }

       /// <summary>
       /// Gets an ImageSize by name. 
       /// </summary>
       /// <param name="key">
       /// A <see cref="System.String"/>
       /// </param>
       /// <returns>
       /// A <see cref="ImageSize"/>
       /// </returns>
		public ImageSize GetImageSize(string key){ //TODO: Move into collection
           foreach(ImageSize size in ImageSizes){
                if(size.Key == key){
                return size;
                }
            }

           throw new KeyNotFoundException();
       }
       

        public CmsImageGallery() : base(typeID) {
        }

		/// <summary>
		/// Finds all CmsImageGalleries. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsImageGallery), "ORDER BY FullPath", true);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds all root CmsImageGalleries. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindAllRoot() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(CmsImageGallery), "LEFT OUTER JOIN CmsFiles AS Parents ON Parents.ID = CmsFiles.ParentID WHERE NOT CmsFiles.FileTypeID = Parents.FileTypeID ORDER BY CmsFiles.FullPath", true);
            return cmd.Execute();
        }
		
		/// <summary>
		/// Finds the CmsImageGallery by ID. 
		/// </summary>
		/// <param name="galleryID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="CmsImageGallery"/>
		/// </returns>
        public static CmsImageGallery FindByID(Guid galleryID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (CmsImageGallery)ds.FindByPrimaryKey(typeof(CmsImageGallery), galleryID);
        }
		
		/// <summary>
		/// This method supports the Xenosynth CMS Module and is not intended to be used directly from your code. 
		/// </summary>
		/// <param name="virtualPath">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Stream"/>
		/// </returns>
        public override Stream Open(string virtualPath) {

            string key = virtualPath.Substring(2,virtualPath.IndexOf("/", 1)-2); //strip out /_key/
            string path = virtualPath.Substring(virtualPath.IndexOf("/",1));
            CmsFile file = CmsFile.FindByFullPath(this.FullPath + path);

            MemoryStream s = new MemoryStream(((CmsImage)file).Bytes);
            System.Drawing.Image i = System.Drawing.Image.FromStream(s);

            ImageSize size = GetImageSize(key);

            s = new MemoryStream();
            size.Resize(i,s);
            s.Position = 0;
            return s;
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

            if (CmsHttpContext.Current.IsDynamicPath) {

                string extension = Path.GetExtension(FileName);
                if (extension == ".gif") {
                    context.Response.ContentType = "image/gif";
                } else if (extension == ".png") {
                    context.Response.ContentType = "image/png";
                } else {
                    context.Response.ContentType = "image/jpeg";
                }


                string dynamicPath = CmsHttpContext.Current.DynamicPath;
                string key = dynamicPath.Substring(2, dynamicPath.IndexOf("/", 1) - 2); //strip out /_key/
                
                string path = dynamicPath.Substring(dynamicPath.IndexOf("/", 1));
                CmsFile file = CmsFile.FindByFullPath(this.FullPath + path);

                try {
                    string strHeader = context.Request.Headers["If-Modified-Since"];
                    if (strHeader != null) {
                        DateTime dtIfModifiedSince = DateTime.ParseExact(strHeader, "r", null);
                        DateTime ftime = file.DateModified.ToUniversalTime();
                        if (ftime <= dtIfModifiedSince) {
                            context.Response.StatusCode = 304;
                            return;
                        }
                    }
                } catch { } 

                MemoryStream s = new MemoryStream(((CmsImage)file).Bytes);
                System.Drawing.Image i = System.Drawing.Image.FromStream(s);

                ImageSize size = GetImageSize(key);

                context.Response.Cache.SetLastModified(file.DateModified);
                context.Response.Cache.AppendCacheExtension("post-check=900,pre-check=3600");
                size.Resize(i, context.Response.OutputStream);
                return;

            }
        }

        #endregion

        
    }
	
	/// <summary>
	/// A defined image size. 
	/// </summary>
    public class ImageSize {

        private string key;
        private string name;
        private int constraint;
        private bool keepAspectRatio;
		
		/// <summary>
		/// A key for the size. 
		/// </summary>
        public String Key {
            get { return key; }
        }
		
		/// <summary>
		/// A name for the size. 
		/// </summary>
        public String Name {
            get { return name; }
        }
		
		/// <summary>
		/// The max witdh or height. 
		/// </summary>
        public int Constraint {
            get { return constraint; }
        }
		
		/// <summary>
		/// Whether the aspect ration should be kept when resizing to this size. 
		/// </summary>
        public bool KeepAspectRatio {
            get { return keepAspectRatio; }
        }

        internal ImageSize(string name, string key, int constraint, bool keepAspectRatio) {
            this.name = name;
            this.key = key;
            this.constraint = constraint;
            this.keepAspectRatio = keepAspectRatio;
        }


        internal void Resize(Image i, Stream stream) {

            Size size = GetSize(i.Size);

            System.Drawing.Bitmap b = new System.Drawing.Bitmap(size.Width, size.Height, PixelFormat.Format32bppArgb);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(b);

            g.FillRectangle(Brushes.White, 0, 0, size.Width, size.Height);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, size.Width, size.Height);
            g.DrawImage(i, rect, 0, 0, i.Width, i.Height, System.Drawing.GraphicsUnit.Pixel);

            b.Save(stream, i.RawFormat);
        }

        public Size GetSize(Size orginalSize) {
            Size newSize = new Size();
            if (keepAspectRatio) {
                float scalar = Math.Min(((float)constraint / orginalSize.Width), ((float)constraint / orginalSize.Height));
                newSize.Width = (int)(scalar * orginalSize.Width);
                newSize.Height = (int)(scalar * orginalSize.Height);
            } else {
                newSize.Width = constraint;
                newSize.Height = constraint; 
            }
            return newSize;
        }
    }
}