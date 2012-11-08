using System;
using System.Configuration;
using Fluent.Text;
using Xenosynth.Configuration;

namespace Xenosynth.Modules.Cms {
	/// <summary>
	/// The CmsConfiguration provides access to common configuration settings.
	/// </summary>
	public class CmsConfiguration {

		private static CmsConfiguration instance;

		private string adminPath;
		private string[] authoringRoles;
		private string[] adminRoles;

		private string[] allowedPageExtensions;
		private string defaultPageName;
		private string customCssUrl;

		private Type[] registeredContentTypes;
		
		/// <summary>
		/// The CmsConfiguration for the current application. 
		/// </summary>
		public static CmsConfiguration Current {
			get {
				if(instance == null){
					instance = new CmsConfiguration();
				}
				return instance;
			}
		}
		
		/// <summary>
		/// The path to the Xenosynth Admin. 
		/// </summary>
		public string AdminPath {
			get { return adminPath; }
		}
		
		/// <summary>
		/// A list of roles that have authoring rights. 
		/// </summary>
		public string[] AuthoringRoles {
			get { return authoringRoles; }
		}
		
		/// <summary>
		/// A list of roles that have admin rights. 
		/// </summary>
		public string[] AdminRoles {
			get { return adminRoles; }
		}
		
		/// <summary>
		/// The list of file extensions that are allowed for CmsPages. 
		/// </summary>
		public string[] AllowedPageExtensions {
			get { return allowedPageExtensions; }
		}
		
		/// <summary>
		/// Returns a regex string that will validate the file names for CmsPages. 
		/// </summary>
		public string AllowedPageNameRegex {
			get { 
				string[] extensions = CmsConfiguration.Current.AllowedPageExtensions;
				StringConcatenator regex = new StringConcatenator("|");
				foreach(string extension in extensions){
					regex.AppendFormat("^[\\-0-9A-Za-z_ ]+(.{0})$", extension.Trim());
				}
				return regex.ToString();
			}
		}
		
		/// <summary>
		/// Returns the filename that will be treated as the Default page for a directory. 
		/// </summary>
		public string DefaultPageName {
			get { return defaultPageName; }
		}
		
		/// <summary>
		/// Returns the url for the css file for this applications customizations. 
		/// </summary>
		public string CustomCssUrl {
			get { return customCssUrl; }
		}
		
		/// <summary>
		/// The types registered with <see cref="CmsRegisteredContent"> for content block persistence.  
		/// </summary>
		public Type[] RegisteredContentTypes {
			get { return registeredContentTypes; }
		}
		
		/// <summary>
		/// Constructs a CmsConfiguration for this application. 
		/// </summary>
		public CmsConfiguration() {

            SystemConfiguration config = XenosynthContext.Current.Configuration;

            adminPath = (string)config.GetValue("xenosynth.installation.appPath", true);
			if(!adminPath.EndsWith("/")){
				adminPath += "/";
			}

            authoringRoles = ((string)config.GetValue("xenosynth.security.authoringRoles", true)).Split(',');
            adminRoles = ((string)config.GetValue("xenosynth.security.adminRoles", true)).Split(',');
            allowedPageExtensions = ((string)config.GetValue("xenosynth.installation.allowedPageExtensions", true)).Split(',');
            defaultPageName = (string)config.GetValue("xenosynth.installation.defaultPage", true);
            customCssUrl = (string)config.GetValue("xenosynth.installation.customCss", false);

            string[] registeredContentTypeNames = ((string)config.GetValue("xenosynth.installation.registeredContentTypes", true)).Split(';');
			registeredContentTypes = new Type[registeredContentTypeNames.Length];
			for(int i = 0; i < registeredContentTypeNames.Length; i++){
				Type t = Type.GetType(registeredContentTypeNames[i]); 
				if(t == null){
					throw new ConfigurationErrorsException(string.Format(Properties.Resources.RegisteredContentTypeMissing, registeredContentTypeNames[i]));
				}
				registeredContentTypes[i] = t;

			}
		}

		
	}
}
