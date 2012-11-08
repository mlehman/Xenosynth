using System;
using System.Collections.Generic;
using System.Text;

namespace Xenosynth.Configuration {
	
	/// <summary>
	/// A ConfigurationSetting is a key/value pairs for a typed value. 
	/// </summary>
    public class ConfigurationSetting {
   
        public event EventHandler Change;

		private string key;
		private string category;
		private string name;
		private string description;
		private Type type;
		private object val;
		
		/// <summary>
		/// Contsructs a new Configuration Setting. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="category">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="name">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="description">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="type">
		/// A <see cref="Type"/>
		/// </param>
		/// <param name="value">
		/// A <see cref="System.Object"/>
		/// </param>
        public ConfigurationSetting(string key, string category, string name, string description, Type type, object value) {
			this.key = key;
			this.category = category;
			this.name = name;
			this.description = description;
			this.type = type;
			this.val = value;
		}
		
		/// <summary>
		/// The key to use for looking up the setting. 
		/// </summary>
		public string Key {
			get { return key; }
		}
		
		/// <summary>
		/// A category used to organize the settings. 
		/// </summary>
		public string Category {
			get { return category; }
		}
		
		/// <summary>
		/// A user-friendly name for the setting. 
		/// </summary>
		public string Name {
			get { return name; }
		}
		
		/// <summary>
		/// A more detailed description of the setting. 
		/// </summary>
		public string Description {
			get { return description; }
		}
		
		/// <summary>
		/// The type for the value. 
		/// </summary>
		public Type ValueType {
			get { return type; }
		}
		
		/// <summary>
		/// The current value for the setting. 
		/// </summary>
		public object Value {
			get {
				return val; 
			}
			set { 
				if(val != value){
					if(ValueType.IsInstanceOfType(value)){
						val = value; 
						if(Change != null ){
							Change(this, EventArgs.Empty);
						}
					} else {
						throw new ArgumentException("Value is not an instance of " + ValueType.FullName);
					}
				}
			}
		}

	

	}
}