using System;
using System.Web;

namespace Xenosynth.Modules {
	/// <summary>
	/// IModule is the interface that is required to be implemented by any extension that needs initialization controlled by the Xenosynth Framework.
	/// </summary>
	public interface IModule {
        
		/// <summary>
		/// The url for the admininstration module's web application. 
		/// </summary>
        string DefaultUrl { get; }
		
		/// <summary> 
		/// The url for the modules configuration page. 
		/// </summary>
        string ConfigurationUrl { get; }
		
		/// <summary>
		/// A method hook called once on application start up to allow the module to perform any initialization tasks. 
		/// </summary>
		/// <param name="application">
		/// The current <see cref="HttpApplication"/>.
		/// </param>
		void Init(HttpApplication application);
		
		/// <summary>
		/// A method hook called once on application start up after all modules have finished initialization.
		/// </summary>
		void Start();
        
	}
}
