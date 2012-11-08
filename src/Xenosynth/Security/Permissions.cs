using System;
using System.Collections;
using System.Text;
using System.Data;

using Inform;

namespace Xenosynth.Security {
	
	/// <summary>
	/// Permissions provide a more granular way to control access to resources and features than roles.  Permissions can be assigned to roles in the Xenosynth Admin.
	/// </summary>
    public class Permissions {
		
		/// <summary>
		/// Inserts a new Permission into the database. 
		/// </summary>
		/// <param name="p">
		/// A <see cref="Permission"/>
		/// </param>
        public static void Insert(Permission p) {
            
            p.ID = Guid.NewGuid();

            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Insert(p);
        }

		/// <summary>
		/// Finds all Permissions. 
		/// </summary>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindAllPermissions() {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(Permission), "ORDER BY Category, Name");
            return cmd.Execute();
        }
		
		/// <summary>
		/// Finds a Permission by the unique identifier. 
		/// </summary>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="Permission"/>
		/// </returns>
        public static Permission FindPermissionByID(Guid permissionID) {
            //TODO: Push internal and add cache?
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return (Permission)ds.FindByPrimaryKey(typeof(Permission), permissionID);
        }

		/// <summary>
		/// Finds a Permission by the key. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="Permission"/>
		/// </returns>
        public static Permission FindPermissionByKey(string key) {
            //TODO: Push internal and add cache?
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindObjectCommand cmd = ds.CreateFindObjectCommand(typeof(Permission), "WHERE [Key] = @Key");
            cmd.CreateInputParameter("@Key", key);
            return (Permission)cmd.Execute();
        }
		
		/// <summary>
		/// Finds all roles for the Permission. 
		/// </summary>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.String[]"/>
		/// </returns>
        public static string[] FindRolesForPermission(Guid permissionID) {
            //TODO: Push internal and add cache?
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("SELECT RoleName FROM xs_RolePermissions WHERE PermissionID = @PermissionID");
            cmd.CreateInputParameter("@PermissionID", permissionID);
            IDataReader r = cmd.ExecuteReader();
            ArrayList roles = new ArrayList();
            try {
                while (r.Read()) {
                    roles.Add(r.GetString(0));
                }
            } finally {
                r.Close();
            }
            return (string[])roles.ToArray(typeof(string));
        }
		
		/// <summary>
		/// Whether the permission indentified by the key exists. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool PermissionExists(string key) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            return FindPermissionByKey(key) != null; //HACK: Speed up.
        }
		
		/// <summary>
		/// Whether the role has the Permission. 
		/// </summary>
		/// <param name="role">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool RoleHasPermission(string role, Guid permissionID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("SELECT COUNT(*) FROM xs_RolePermissions WHERE PermissionID = @PermissionID AND RoleName = @Role ");
            cmd.CreateInputParameter("@PermissionID", permissionID);
            cmd.CreateInputParameter("@Role", role);
            return (int)cmd.ExecuteScalar() > 0;
        }

        //public static bool UserHasPermission(string key) {
        //    DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
        //    return FindPermissionByKey(key) != null; //HACK: Speed up.
        //}
		
		/// <summary>
		/// Adds the Permission to the role. 
		/// </summary>
		/// <param name="role">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
        public static void AddPermissionToRole(string role, Guid permissionID) {
            if (!RoleHasPermission(role, permissionID)) {
                DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
                IDataAccessCommand cmd = ds.CreateDataAccessCommand("INSERT INTO xs_RolePermissions VALUES (@PermissionID, @Role)");
                cmd.CreateInputParameter("@PermissionID", permissionID);
                cmd.CreateInputParameter("@Role", role);
                cmd.ExecuteScalar();
            }
        }
		
		/// <summary>
		/// Removes the Permission from the role. 
		/// </summary>
		/// <param name="role">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
        public static void RemovePermissionFromRole(string role, Guid permissionID) {
                DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
                IDataAccessCommand cmd = ds.CreateDataAccessCommand("DELETE FROM xs_RolePermissions WHERE PermissionID = @PermissionID AND RoleName = @Role");
                cmd.CreateInputParameter("@PermissionID", permissionID);
                cmd.CreateInputParameter("@Role", role);
                cmd.ExecuteScalar();
        }

		/// <summary>
		/// Whether the current User has the Permission. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public static bool UserHasPermission(string key) {
            Permission p = FindPermissionByKey(key);
            string[] roles = p.Roles;
            foreach (string role in roles) {
                if (System.Web.HttpContext.Current.User.IsInRole(role)) {
                    return true;
                }
            }
            return false; 
        }
		
		/// <summary>
		/// Deletes the Permission from the database. 
		/// </summary>
		/// <param name="permissionID">
		/// A <see cref="Guid"/>
		/// </param>
        public static void DeletePermission(Guid permissionID) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IDataAccessCommand cmd = ds.CreateDataAccessCommand("DELETE FROM xs_RolePermissions WHERE PermissionID = @PermissionID");
            cmd.CreateInputParameter("@PermissionID", permissionID);
            cmd.ExecuteScalar();

            ds.Delete(typeof(Permission), permissionID);

        }
    }
}
