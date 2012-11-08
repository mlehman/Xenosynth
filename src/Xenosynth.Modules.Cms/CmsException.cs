using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Modules.Cms {
	
	/// <summary>
	/// The exception that is thrown by Xenosynth CMS Module for exceptions. 
	/// </summary>
    public class CmsException : ApplicationException {
		
		/// <summary>
		/// Constructs a new instance of the CmsException class with a specified error message. 
		/// </summary>
		/// <param name="message">
		/// A <see cref="System.String"/>
		/// </param>
        public CmsException(string message)
            : base(message) {
        }
		
		/// <summary>
		/// Initializes a new instance of the CmsException class with a specified error message and a reference to the inner exception that is the cause of this exception. 
		/// </summary>
		/// <param name="message">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="innerException">
		/// A <see cref="Exception"/>
		/// </param>
        public CmsException(string message, Exception innerException)
            : base(message, innerException) {
        }

    }
}
