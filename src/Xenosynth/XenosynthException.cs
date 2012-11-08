using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth {
	
	/// <summary>
	/// The exception that is thrown by Xenosynth Framework for Framework exceptions. 
	/// </summary>
    public class XenosynthException : ApplicationException {
		
		/// <summary>
		/// Constructs a new instance of the XenosynthException class with a specified error message. 
		/// </summary>
		/// <param name="message">
		/// A <see cref="System.String"/>
		/// </param>
        public XenosynthException(string message)
            : base(message) {
        }
		
		/// <summary>
		/// Initializes a new instance of the XenosynthException class with a specified error message and a reference to the inner exception that is the cause of this exception.
		/// </summary>
		/// <param name="message">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="innerException">
		/// A <see cref="Exception"/>
		/// </param>
        public XenosynthException(string message, Exception innerException)
            : base(message, innerException) {
        }

    }
}
