using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;

namespace Xenosynth.Web {
	
	/// <summary>
	/// MimeMapping provides the mime-types for common file extensions. 
	/// </summary>
    public class MimeMapping {

        private static Dictionary<string, string> mimeTypes = new Dictionary<string, string>();

        static MimeMapping() {

            mimeTypes.Add(".*", "application/octet-stream");
            mimeTypes.Add(".323", "text/h323");
            mimeTypes.Add(".acx", "application/internet-property-stream");
            mimeTypes.Add(".ai", "application/postscript");
            mimeTypes.Add(".aif", "audio/x-aiff");
            mimeTypes.Add(".aifc", "audio/aiff");
            mimeTypes.Add(".aiff", "audio/aiff");
            mimeTypes.Add(".application", "application/x-ms-application");
            mimeTypes.Add(".asf", "video/x-ms-asf");
            mimeTypes.Add(".asr", "video/x-ms-asf");
            mimeTypes.Add(".asx", "video/x-ms-asf");
            mimeTypes.Add(".au", "audio/basic");
            mimeTypes.Add(".avi", "video/x-msvideo");
            mimeTypes.Add(".axs", "application/olescript");
            mimeTypes.Add(".bas", "text/plain");
            mimeTypes.Add(".bcpio", "application/x-bcpio");
            mimeTypes.Add(".bin", "application/octet-stream");
            mimeTypes.Add(".bmp", "image/bmp");
            mimeTypes.Add(".c", "text/plain");
            mimeTypes.Add(".cat", "application/vndms-pkiseccat");
            mimeTypes.Add(".cdf", "application/x-cdf");
            mimeTypes.Add(".cer", "application/x-x509-ca-cert");
            mimeTypes.Add(".clp", "application/x-msclip");
            mimeTypes.Add(".cmx", "image/x-cmx");
            mimeTypes.Add(".cod", "image/cis-cod");
            mimeTypes.Add(".cpio", "application/x-cpio");
            mimeTypes.Add(".crd", "application/x-mscardfile");
            mimeTypes.Add(".crl", "application/pkix-crl");
            mimeTypes.Add(".crt", "application/x-x509-ca-cert");
            mimeTypes.Add(".csh", "application/x-csh");
            mimeTypes.Add(".css", "text/css");
            mimeTypes.Add(".dcr", "application/x-director");
            mimeTypes.Add(".deploy", "application/octet-stream");
            mimeTypes.Add(".der", "application/x-x509-ca-cert");
            mimeTypes.Add(".dib", "image/bmp");
            mimeTypes.Add(".dir", "application/x-director");
            mimeTypes.Add(".disco", "text/xml");
            mimeTypes.Add(".dll", "application/x-msdownload");
            mimeTypes.Add(".doc", "application/msword");
            mimeTypes.Add(".dot", "application/msword");
            mimeTypes.Add(".dvi", "application/x-dvi");
            mimeTypes.Add(".dxr", "application/x-director");
            mimeTypes.Add(".eml", "message/rfc822");
            mimeTypes.Add(".eps", "application/postscript");
            mimeTypes.Add(".etx", "text/x-setext");
            mimeTypes.Add(".evy", "application/envoy");
            mimeTypes.Add(".exe", "application/octet-stream");
            mimeTypes.Add(".fif", "application/fractals");
            mimeTypes.Add(".flr", "x-world/x-vrml");
            mimeTypes.Add(".gif", "image/gif");
            mimeTypes.Add(".gtar", "application/x-gtar");
            mimeTypes.Add(".gz", "application/x-gzip");
            mimeTypes.Add(".h", "text/plain");
            mimeTypes.Add(".hdf", "application/x-hdf");
            mimeTypes.Add(".hlp", "application/winhlp");
            mimeTypes.Add(".hqx", "application/mac-binhex40");
            mimeTypes.Add(".hta", "application/hta");
            mimeTypes.Add(".htc", "text/x-component");
            mimeTypes.Add(".htm", "text/html");
            mimeTypes.Add(".html", "text/html");
            mimeTypes.Add(".htt", "text/webviewhtml");
            mimeTypes.Add(".ico", "image/x-icon");
            mimeTypes.Add(".ief", "image/ief");
            mimeTypes.Add(".iii", "application/x-iphone");
            mimeTypes.Add(".ins", "application/x-internet-signup");
            mimeTypes.Add(".isp", "application/x-internet-signup");
            mimeTypes.Add(".IVF", "video/x-ivf");
            mimeTypes.Add(".jfif", "image/pjpeg");
            mimeTypes.Add(".jpe", "image/jpeg");
            mimeTypes.Add(".jpeg", "image/jpeg");
            mimeTypes.Add(".jpg", "image/jpeg");
            mimeTypes.Add(".js", "application/x-javascript");
            mimeTypes.Add(".latex", "application/x-latex");
            mimeTypes.Add(".lsf", "video/x-la-asf");
            mimeTypes.Add(".lsx", "video/x-la-asf");
            mimeTypes.Add(".m13", "application/x-msmediaview");
            mimeTypes.Add(".m14", "application/x-msmediaview");
            mimeTypes.Add(".m1v", "video/mpeg");
            mimeTypes.Add(".m3u", "audio/x-mpegurl");
            mimeTypes.Add(".man", "application/x-troff-man");
            mimeTypes.Add(".manifest", "application/x-ms-manifest");
            mimeTypes.Add(".mdb", "application/x-msaccess");
            mimeTypes.Add(".me", "application/x-troff-me");
            mimeTypes.Add(".mht", "message/rfc822");
            mimeTypes.Add(".mhtml", "message/rfc822");
            mimeTypes.Add(".mid", "audio/mid");
            mimeTypes.Add(".mmf", "application/x-smaf");
            mimeTypes.Add(".mny", "application/x-msmoney");
            mimeTypes.Add(".mov", "video/quicktime");
            mimeTypes.Add(".movie", "video/x-sgi-movie");
            mimeTypes.Add(".mp2", "video/mpeg");
            mimeTypes.Add(".mp3", "audio/mpeg");
            mimeTypes.Add(".mpa", "video/mpeg");
            mimeTypes.Add(".mpe", "video/mpeg");
            mimeTypes.Add(".mpeg", "video/mpeg");
            mimeTypes.Add(".mpg", "video/mpeg");
            mimeTypes.Add(".mpp", "application/vnd.ms-project");
            mimeTypes.Add(".mpv2", "video/mpeg");
            mimeTypes.Add(".ms", "application/x-troff-ms");
            mimeTypes.Add(".mvb", "application/x-msmediaview");
            mimeTypes.Add(".nc", "application/x-netcdf");
            mimeTypes.Add(".nws", "message/rfc822");
            mimeTypes.Add(".oda", "application/oda");
            mimeTypes.Add(".ods", "application/oleobject");
            mimeTypes.Add(".p10", "application/pkcs10");
            mimeTypes.Add(".p12", "application/x-pkcs12");
            mimeTypes.Add(".p7b", "application/x-pkcs7-certificates");
            mimeTypes.Add(".p7c", "application/pkcs7-mime");
            mimeTypes.Add(".p7m", "application/pkcs7-mime");
            mimeTypes.Add(".p7r", "application/x-pkcs7-certreqresp");
            mimeTypes.Add(".p7s", "application/pkcs7-signature");
            mimeTypes.Add(".pbm", "image/x-portable-bitmap");
            mimeTypes.Add(".pdf", "application/pdf");
            mimeTypes.Add(".pfx", "application/x-pkcs12");
            mimeTypes.Add(".pgm", "image/x-portable-graymap");
            mimeTypes.Add(".pko", "application/vndms-pkipko");
            mimeTypes.Add(".pma", "application/x-perfmon");
            mimeTypes.Add(".pmc", "application/x-perfmon");
            mimeTypes.Add(".pml", "application/x-perfmon");
            mimeTypes.Add(".pmr", "application/x-perfmon");
            mimeTypes.Add(".pmw", "application/x-perfmon");
            mimeTypes.Add(".png", "image/png");
            mimeTypes.Add(".pnm", "image/x-portable-anymap");
            mimeTypes.Add(".pnz", "image/png");
            mimeTypes.Add(".pot", "application/vnd.ms-powerpoint");
            mimeTypes.Add(".ppm", "image/x-portable-pixmapv");
            mimeTypes.Add(".pps", "application/vnd.ms-powerpoint");
            mimeTypes.Add(".ppt", "application/vnd.ms-powerpoint");
            mimeTypes.Add(".prf", "application/pics-rules");
            mimeTypes.Add(".ps", "application/postscript");
            mimeTypes.Add(".pub", "application/x-mspublisher");
            mimeTypes.Add(".qt", "video/quicktime");
            mimeTypes.Add(".ra", "audio/x-pn-realaudio");
            mimeTypes.Add(".ram", "audio/x-pn-realaudio");
            mimeTypes.Add(".ras", "image/x-cmu-raster");
            mimeTypes.Add(".rgb", "image/x-rgb");
            mimeTypes.Add(".rmi", "audio/mid");
            mimeTypes.Add(".roff", "application/x-troff");
            mimeTypes.Add(".rtf", "application/rtf");
            mimeTypes.Add(".rtx", "text/richtext");
            mimeTypes.Add(".scd", "application/x-msschedule");
            mimeTypes.Add(".sct", "text/scriptlet");
            mimeTypes.Add(".setpay", "application/set-payment-initiation");
            mimeTypes.Add(".setreg", "application/set-registration-initiation");
            mimeTypes.Add(".sh", "application/x-sh");
            mimeTypes.Add(".shar", "application/x-shar");
            mimeTypes.Add(".sit", "application/x-stuffit");
            mimeTypes.Add(".smd", "audio/x-smd");
            mimeTypes.Add(".smx", "audio/x-smd");
            mimeTypes.Add(".smz", "audio/x-smd");
            mimeTypes.Add(".snd", "audio/basic");
            mimeTypes.Add(".spc", "application/x-pkcs7-certificates");
            mimeTypes.Add(".spl", "application/futuresplash");
            mimeTypes.Add(".src", "application/x-wais-source");
            mimeTypes.Add(".sst", "application/vndms-pkicertstore");
            mimeTypes.Add(".stl", "application/vndms-pkistl");
            mimeTypes.Add(".stm", "text/html");
            mimeTypes.Add(".sv4cpio", "application/x-sv4cpio");
            mimeTypes.Add(".sv4crc", "application/x-sv4crc");
            mimeTypes.Add(".swf", "application/x-shockwave-flash");
            mimeTypes.Add(".t", "application/x-troff");
            mimeTypes.Add(".tar", "application/x-tar");
            mimeTypes.Add(".tcl", "application/x-tcl");
            mimeTypes.Add(".tex", "application/x-tex");
            mimeTypes.Add(".texi", "application/x-texinfo");
            mimeTypes.Add(".texinfo", "application/x-texinfo");
            mimeTypes.Add(".tgz", "application/x-compressed");
            mimeTypes.Add(".tif", "image/tiff");
            mimeTypes.Add(".tiff", "image/tiff");
            mimeTypes.Add(".tr", "application/x-troff");
            mimeTypes.Add(".trm", "application/x-msterminal");
            mimeTypes.Add(".tsv", "text/tab-separated-values");
            mimeTypes.Add(".txt", "text/plain");
            mimeTypes.Add(".uls", "text/iuls");
            mimeTypes.Add(".ustar", "application/x-ustar");
            mimeTypes.Add(".vcf", "text/x-vcard");
            mimeTypes.Add(".wav", "audio/wav");
            mimeTypes.Add(".wbmp", "image/vnd.wap.wbmp");
            mimeTypes.Add(".wcm", "application/vnd.ms-works");
            mimeTypes.Add(".wdb", "application/vnd.ms-works");
            mimeTypes.Add(".wks", "application/vnd.ms-works");
            mimeTypes.Add(".wmf", "application/x-msmetafile");
            mimeTypes.Add(".wps", "application/vnd.ms-works");
            mimeTypes.Add(".wri", "application/x-mswrite");
            mimeTypes.Add(".wrl", "x-world/x-vrml");
            mimeTypes.Add(".wrz", "x-world/x-vrml");
            mimeTypes.Add(".wsdl", "text/xml");
            mimeTypes.Add(".xaf", "x-world/x-vrml");
            mimeTypes.Add(".xbm", "image/x-xbitmap");
            mimeTypes.Add(".xla", "application/vnd.ms-excel");
            mimeTypes.Add(".xlc", "application/vnd.ms-excel");
            mimeTypes.Add(".xlm", "application/vnd.ms-excel");
            mimeTypes.Add(".xls", "application/vnd.ms-excel");
            mimeTypes.Add(".xlt", "application/vnd.ms-excel");
            mimeTypes.Add(".xlw", "application/vnd.ms-excel");
            mimeTypes.Add(".xml", "text/xml");
            mimeTypes.Add(".xof", "x-world/x-vrml");
            mimeTypes.Add(".xpm", "image/x-xpixmap");
            mimeTypes.Add(".xsd", "text/xml");
            mimeTypes.Add(".xsl", "text/xml");
            mimeTypes.Add(".xwd", "image/x-xwindowdump");
            mimeTypes.Add(".z", "application/x-compress");
            mimeTypes.Add(".zip", "application/x-zip-compressed");

        }
		
		/// <summary>
		/// Gets the mime-type based on the extension of a filename. 
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String"/>
		/// </returns>
        public static string GetMimeType(string filename) {

            String extension = Path.GetExtension(filename);
            if (extension == null) {
                return mimeTypes[".*"];
            }

            string mimeType = null;
            if (!mimeTypes.TryGetValue(extension.ToLower(), out mimeType)) {
                return mimeTypes[".*"];
            }

            return mimeType;

        }
    }
}
