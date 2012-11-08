using System;
using System.Collections;
using System.Text;

using Inform;

namespace Xenosynth.Security {
	
	/// <summary>
	/// A Permission. <see 
	/// </summary>
    [TypeMapping(TableName="xs_Permissions")]
    public class Permission {
        
        [MemberMapping(PrimaryKey = true, ColumnName = "ID")]
        protected Guid id;

        [MemberMapping(ColumnName = "Key", Length = 100)]
        protected string key;

        [MemberMapping(ColumnName = "Category", Length = 100)]
        protected string category;

        [MemberMapping(ColumnName = "Name", Length = 100)]
        protected string name;

        [MemberMapping(ColumnName = "Description", Length = 250)]
        protected string description;

        private string[] roles;
		
		/// <summary>
		/// The unique identifier for the Permission. 
		/// </summary>
        public Guid ID {
            get { return id; }
            set { id = value; }
        }
		
		/// <summary>
		/// A unique indentifier to use for programmatically checking the Permission. 
		/// </summary>
        public string Key {
            get { return key; }
            set { key = value; }
        }
		
		/// <summary>
		/// A category to organize the Permission. 
		/// </summary>
        public string Category {
            get { return category; }
            set { category = value; }
        }
		
		/// <summary>
		/// A user-friendly name for the Permission. 
		/// </summary>
        public string Name {
            get { return name; }
            set { name = value; }
        }
		
		/// <summary>
		/// A description of the Permission. 
		/// </summary>
        public string Description {
            get { return description; }
            set { description = value; }
        }
		
		/// <summary>
		/// The list of roles that have this Permission. 
		/// </summary>
        public string[] Roles {
            get {
                if (roles == null) {
                    roles = Permissions.FindRolesForPermission(ID);
                }
                return roles;
            }
        }
		
		/// <summary>
		/// Updates this Permission in the database. 
		/// </summary>
        public void Update() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Update(this);
        }
		
		/// <summary>
		/// Deletes this Permission from the database. 
		/// </summary>
        public void Delete() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Delete(this);
        }

    }
}
