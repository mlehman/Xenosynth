using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Configuration;

namespace Xenosynth.Configuration {
	
    /// <summary>
    /// Provides access to Xenosynth system configuration.
    /// <remarks>
    /// The system configuration provides an easy way to have a configuration that can be managed using the end user from the Xenosynth Admin.  Settings that should not be managed by the end user should reside in the web/user config.
    /// </remarks>
    /// </summary>
    public class SystemConfiguration : IEnumerable {

        private static SystemConfiguration current = new SystemConfiguration();

        private Hashtable settingsCache;
        private string connectionString;

        private const string CONNECTION_STRING_SETTINGS_NAME = "Xenosynth";
 
		/// <summary>
		/// This property supports the Xenosynth Framework and is not intended to be used directly from your code. 
		/// </summary>
        public string ConnectionString {
            set { connectionString = value; }
            get {
                if (connectionString == null) {
                    ConnectionStringSettings connectionStringSetting = ConfigurationManager.ConnectionStrings[CONNECTION_STRING_SETTINGS_NAME];
                    if (connectionStringSetting == null || string.IsNullOrEmpty(connectionStringSetting.ConnectionString)) {
                        throw new ConfigurationErrorsException(string.Format(Properties.Resources.ConnectionStringSettingsNullOrEmpty, CONNECTION_STRING_SETTINGS_NAME));
                    } else {
                        connectionString = connectionStringSetting.ConnectionString;
                    }
                }
                return connectionString;
            }
        }
		
		/// <summary>
		/// This property supports the Xenosynth Framework and is not intended to be used directly from your code. 
		/// </summary>
        public string TableName {
            get { return "ConfigSettings"; }
        }

        private SystemConfiguration() {
        }
		
		/// <summary>
		/// Gets the <see cref="SystemConfiguration"/> object. 
		/// </summary>
        public static SystemConfiguration Current {
            get { return current; }
        }
		
		/// <summary>
		/// A list categories in the current system configuration.  
		/// </summary>
        public string[] Categories {
            get {
                ArrayList categories = new ArrayList();
                foreach (ConfigurationSetting c in Settings.Values) {
                    if (!categories.Contains(c.Category)) {
                        categories.Add(c.Category);
                    }
                }
                categories.Sort();
                return (string[])categories.ToArray(typeof(string));
            }
        }
		
		/// <summary>
		/// Gets a list of <see cref="ConfigurationSetting"/> for the given category.
		/// </summary>
		/// <param name="category">
		/// A <see cref="System.String"/> for the category.
		/// </param>
		/// <returns>
		/// A <see cref="IList"/> of <see cref="ConfigurationSetting"/>.
		/// </returns>
        public IList GetConfigurationSettingByCategory(string category) {
            ArrayList settings = new ArrayList();
            foreach (ConfigurationSetting c in Settings.Values) {
                if (c.Category == category) {
                    settings.Add(c);
                }
            }
            return settings;
        }
		
		/// <summary>
		/// Returns the value for a setting.
		/// </summary>
		/// <param name="key">
		/// The key for the setting.
		/// </param>
		/// <param name="throwErrorRequired">
		/// Whether to throw an error if no setting exists for the key. If false and no key exists, returns null.
		/// </param>
		/// <returns>
		/// The value for the configuration settings.
		/// </returns>
        public object GetValue(string key, bool throwErrorRequired) {
            
            ConfigurationSetting setting = this[key];
            
            if (setting == null) {
                if (throwErrorRequired) {
                    throw new ConfigurationErrorsException(string.Format(Properties.Resources.ConfigurationSettingNullOrEmpty, key));
                } else {
                    return null;
                }
            } 

            return setting.Value;
        }
		
		/// <summary>
		/// A Hashtable of <see cref="ConfigurationSetting"/> indexed by the setting's key.
		/// </summary>
        private Hashtable Settings {
            get {
                if (settingsCache == null) {
                    settingsCache = new Hashtable();

                    SqlConnection conn = null;

                    try {
                        conn = new SqlConnection(ConnectionString);
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT * FROM " + TableName, conn);
                        SqlDataReader r = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                        while (r.Read()) {

                            Type type = Type.GetType(r["Type"].ToString(), true);

                            ConfigurationSetting setting = new ConfigurationSetting(
                                r["Key"].ToString(),
                                r["Category"].ToString(),
                                r["Name"].ToString(),
                                r["Description"].ToString(),
                                type,
                                ConvertFromString(r["Value"].ToString(), type)
                                );

                            AddToCache(setting);
                        }

                    } catch (Exception e) {
                        settingsCache = null;
                        throw e;
                    } finally {
                        if (conn != null && conn.State == ConnectionState.Open) {
                            conn.Close();
                        }
                    }
                }

                return settingsCache;
            }
        }

        private void AddToCache(ConfigurationSetting setting) {
            settingsCache.Add(setting.Key, setting);
            setting.Change += new EventHandler(OnSettingChange);
        }

        private object ConvertFromString(string value, Type type) {
            TypeConverter converter = TypeDescriptor.GetConverter(type);
            if (converter != null) {
                return converter.ConvertFromString(value);
            } else {
                return Convert.ChangeType(value, type);
            }
        }

        private string ConvertToString(object value) {
            TypeConverter converter = TypeDescriptor.GetConverter(value);
            if (converter != null) {
                return converter.ConvertToString(value);
            } else {
                return value.ToString();
            }
        }
		
		/// <summary>
		/// Whether the system configuration can be changed.
		/// </summary>
        public bool IsReadOnly {
            get {
                return false;
            }
        }
		
		/// <summary>
		/// Returns the <see cref="ConfigurationSetting"/> for the supplied key. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
        public ConfigurationSetting this[string key] {
            get {
                return (ConfigurationSetting)Settings[key];
            }
        }
		
		/// <summary>
		/// Removes the <see cref="ConfigurationSetting"/> for the supplied key.
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
        public void Remove(string key) {
            if (!this.Contains(key)) {
                throw new ApplicationException("A setting with key '" + key + "' does not exists.");
            }

            string sql = "DELETE " + TableName + "  WHERE [Key] = @Key";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Key", key);


            try {
                conn.Open();
                cmd.ExecuteNonQuery();
            } finally {
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }

            RemoveFromCache(this[key]);
        }

        private void RemoveFromCache(ConfigurationSetting setting) {
            settingsCache.Remove(setting.Key);
            setting.Change -= new EventHandler(OnSettingChange);
        }
		
		/// <summary>
		/// Whether the configuration contains a <see cref="ConfigurationSetting"/> for the supplied key. 
		/// </summary>
		/// <param name="key">
		/// A <see cref="System.String"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// </returns>
        public bool Contains(string key) {
            return Settings.Contains(key);
        }
		
		/// <summary>
		/// This method supports the Xenosynth Framework and is not intended to be used directly from your code. 
		/// </summary>
        public void Clear() {
            throw new NotImplementedException();
        }
		
		/// <summary>
		/// Returns a collection of all the <see cref="ConfigurationSetting"/>.
		/// </summary>
        public ICollection Values {
            get {
                return Settings.Values;
            }
        }
		
		/// <summary>
		/// Adds a <see cref="ConfigurationSetting"/> to the system configuration. 
		/// </summary>
		/// <param name="setting">
		/// A <see cref="ConfigurationSetting"/>
		/// </param>
        public void Add(ConfigurationSetting setting) {
            if (this.Contains(setting.Key)) {
                throw new ApplicationException("A setting with key '" + setting.Key + "' already exists.");
            }

            string sql = "INSERT " + TableName + " ([Key], Category, Name, Description, Type, Value) Values (@Key, @Category, @Name, @Description, @Type, @Value)";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Key", setting.Key);
            cmd.Parameters.Add("@Name", setting.Value);
            cmd.Parameters.Add("@Category", setting.Category);
            cmd.Parameters.Add("@Description", setting.Description);
            cmd.Parameters.Add("@Type", setting.ValueType.AssemblyQualifiedName);
            cmd.Parameters.Add("@Value", ConvertToString(setting.Value));

            try {
                conn.Open();
                cmd.ExecuteNonQuery();
            } finally {
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }

            AddToCache(setting);
        }
		
		/// <summary>
		/// The collection of configuration <see cref="System.String"/> keys. 
		/// </summary>
        public ICollection Keys {
            get {
                return Settings.Keys;
            }
        }
		
		/// <summary>
		/// The count of configurations settings. 
		/// </summary>
        public int Count {
            get {
                return Settings.Count;
            }
        }


        #region IEnumerable Members
		
		/// <summary>
		/// An enumerator for the configuration settings. 
		/// </summary>
		/// <returns>
		/// A <see cref="IEnumerator"/>
		/// </returns>
        IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return settingsCache.GetEnumerator();
        }

        #endregion

        private void OnSettingChange(object sender, EventArgs e) {
            ConfigurationSetting setting = (ConfigurationSetting)sender;
            string sql = "UPDATE " + TableName + " SET Value = @Value WHERE [Key] = @Key";

            SqlConnection conn = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("@Key", setting.Key);
            cmd.Parameters.Add("@Value", ConvertToString(setting.Value));

            try {
                conn.Open();
                cmd.ExecuteNonQuery();
            } finally {
                if (conn.State == ConnectionState.Open) {
                    conn.Close();
                }
            }

        }
    }
}
