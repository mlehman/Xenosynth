using System;
using System.Collections.ObjectModel;
using System.Text;

namespace Xenosynth.Modules {
	
	/// <summary>
	/// A collection of <see cref="RegisteredModule"/>.
	/// </summary>
    public class RegisteredModuleCollection : Collection<RegisteredModule> {
		
		/// <summary>
		/// Find the <see cref="RegisteredModule"/> with this ID. 
		/// </summary>
		/// <param name="moduleID">
		/// A <see cref="Guid"/>
		/// </param>
        public RegisteredModule this[Guid moduleID] {
            get {
                foreach (RegisteredModule rm in this.Items) {
                    if (rm.ID == moduleID) {
                        return rm;
                    }
                }
                return null;
            }
        }
    }
}
