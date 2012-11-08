using System;
using System.Collections;
using System.Data.SqlTypes;
using System.Reflection;
using System.Web;

using Inform;

namespace Xenosynth.Modules {
	
	/// <summary>
	/// Registration information for a Module that extends the Xenosynth Framework. This class supports the Xenosynth Framework and is not intended to be used directly from your code. 
	/// </summary>
    [TypeMapping(TableName="xs_RegisteredModules")]
	public class RegisteredModule {
		
		[MemberMapping(PrimaryKey=true, ColumnName="ID")]
		private Guid id;

		[MemberMapping(ColumnName="Name", Length=250)]
		private string name;

		[MemberMapping(ColumnName="Description", Length=500)]
		private string description;

		[MemberMapping(ColumnName="ResourceFolder", Length=50)]
		private string resourceFolder;

		[MemberMapping(ColumnName="ClassName", Length=100)]
		private string className;

        [MemberMapping(ColumnName = "InitOrder")]
        private int initOrder;

        [MemberMapping(ColumnName = "Enabled")]
        private bool enabled;

        private static Hashtable modules = new Hashtable();
		
		/// <summary>
		/// Constructs a new RegisteredModule.
		/// </summary> 
        public RegisteredModule() {
			id = Guid.Empty;
		}
		
		/// <summary>
		/// A unique indentifier for the registered module. 
		/// </summary>
		public Guid ID {
			get { return id; }
		}
		
		/// <summary>
		/// The user-friendly name of the module. 
		/// </summary>
		public string Name {
			set { name = value; }
			get { return name; }
		}
		
		/// <summary>
		/// A description of the module. 
		/// </summary>
		public string Description {
			set { description = value; }
			get { return description; }
		}
		
		/// <summary>
		/// The location of the resource folder for the module. 
		/// </summary>
		public string ResourceFolder {
			set { resourceFolder = value; }
			get { return resourceFolder; }
		}
		
		/// <summary>
		/// The name of the class that implements IModule for this registered module. Modules that do not implement an <see cref="IModule"/> can leave this null.
		/// </summary>
		public string ClassName {
			set { className = value; }
			get { return className; }
		}
		
		/// <summary>
		/// The initiliation order for the module. Lower numbers are initialized first. 
		/// </summary>
        public int InitOrder {
            set { initOrder = value; }
            get { return initOrder; }
        }
		
		/// <summary>
		/// Whether the module should be initialized.  If false, the module will not get loaded or have its init and start methods called. 
		/// </summary>
        public bool IsEnabled {
            set { enabled = value; }
            get { return enabled; }
        }
		
		/// <summary>
		/// If the module has been loaded and successfully initialized. 
		/// </summary>
        public bool IsInitialized {
            get {
                return modules.ContainsKey(ID);
            }
        }
		
		/// <summary>
		/// Returns the actual instance of the IModule if initialized. 
		/// </summary>
        public IModule Instance {
            get { 
                IModule module = (IModule)modules[ID]; 
                if(module == null){
                     throw new XenosynthException("Module '" + ID + "' not initialized.");
                }
                return module;
            }
        }
		
		/// <summary>
		/// Returns the url for the module's entry adminstration page. 
		/// </summary>
        public string DefaultUrl {
            get {
                if (HasRegisteredModuleClass && IsInitialized) {
                    return Instance.DefaultUrl;
                } else {
                    return "~/Modules/" + ResourceFolder + "/default.aspx";
                }
            }
        }
		
		/// <summary>
		/// Returns the url for the module's congifuration page. 
		/// </summary>
        public string ConfigurationUrl {
            get {
                if (HasRegisteredModuleClass && IsInitialized) {
                    return Instance.ConfigurationUrl;
                } else {
                    return "~/Modules/" + ResourceFolder + "/default.aspx";
                }
            }
        }
		
		/// <summary>
		/// The assembly name for the module. 
		/// </summary>
		public string AssemblyName {
			get { return className.Substring(className.IndexOf(",")); }
		}
		
		/// <summary>
		/// Whether the registered module has an IModule. 
		/// </summary>
		public bool HasRegisteredModuleClass {
			get { return ClassName != null && ClassName.Trim().Length > 0; }
		}

        private IModule CreateModuleInstance() {
            Type t = Type.GetType(ClassName);
            if (t == null) {
                throw new XenosynthException("Could not locate module class: " + ClassName);
            }
            return (IModule)Activator.CreateInstance(t);    
        }
		
		/// <summary>
		/// Initializes the registered module. 
		/// </summary>
		/// <param name="application">
		/// A <see cref="HttpApplication"/>
		/// </param>
		public void Init(HttpApplication application){
			if(HasRegisteredModuleClass){
                IModule module = CreateModuleInstance();
                modules[this.ID] = module;
                module.Init(application);
			}
		}	
	
		/// <summary>
		/// Starts the registered module. 
		/// </summary>
		public void Start(){
			if(HasRegisteredModuleClass){
                IModule module = CreateModuleInstance();
				module.Start();
			}
		}
		
		/// <summary>
		/// Inserts this registered module in to the database. 
		/// </summary>
		public void Insert(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			//TODO: Check ID is EmptyGuid?
			this.id = Guid.NewGuid();
			ds.Insert(this);
		}
		
		/// <summary>
		/// Updates this registered module in the database. 
		/// </summary>
		public void Update(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Update(this);
		}
		
		/// <summary>
		/// Deletes this registered module from the database. 
		/// </summary>
		public void Delete(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
			ds.Delete(this);
		}

		/// <summary>
		/// Finds the registered module from the database by ID. 
		/// </summary>
		/// <param name="registeredModuleID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="RegisteredModule"/>
		/// </returns>
        public static RegisteredModule FindByID(Guid registeredModuleID) {
			//TODO: Push internal and add cache?
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (RegisteredModule)ds.FindByPrimaryKey(typeof(RegisteredModule), registeredModuleID);
		}
		
		/// <summary>
		/// Finds all registered modules in the database. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/> of <see cref="RegisteredModule"/> of
		/// </returns>
		public static IList FindAll(){
			DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(RegisteredModule), "ORDER BY InitOrder");
			return cmd.Execute();
		}
	}
}
