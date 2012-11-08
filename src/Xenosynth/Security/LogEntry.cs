using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlTypes;

using Inform;
using System.Web;
using System.Web.Security;

namespace Xenosynth.Security {
	
	/// <summary>
	/// An entry in the sytem logs. 
	/// </summary>
    [TypeMapping(TableName="xs_LogEntries")]
    public class LogEntry {
		
		/// <summary>
		/// The type of the event.
		/// </summary>
        public enum LogEventType {
            Debug = 0,
            Audit = 1,
            System = 2
        }
		
		/// <summary>
		/// The source of the event. 
		/// </summary>
        public enum LogSource {
            None = 0,
            File = 1,
            User = 2,
            Configuration = 3,
            System = 4
        }
        
		[MemberMapping(ColumnName="ID", PrimaryKey=true, Identity=true)]
		private int id;

		[MemberMapping(ColumnName="Type")]
        private LogEventType eventType;

		[MemberMapping(ColumnName="EventDate")]
		private DateTime eventDate;

		[MemberMapping(ColumnName="Source")]
        private LogSource source;

		[MemberMapping(ColumnName="SourceID")]
		private Guid sourceID;

		[MemberMapping(ColumnName="Event", Length=50)]
		private string eventName;

        [MemberMapping(ColumnName="UserID")]
		private Guid userID;

		[MemberMapping(ColumnName="Username", Length=50)]
		private string username;

		[MemberMapping(ColumnName="IP", Length=50)]
		private string ip;

		[MemberMapping(ColumnName="Detail", Length=500)]
		private string detail;

        [MemberMapping(ColumnName="Data", Length=500)]
		private string data;


        private LogEntry() {
		}
		
		/// <summary>
		/// A unique identifier for the LogEntry. 
		/// </summary>
		public int ID {
			get { return id; }
		}
		
		/// <summary>
		/// The type of the event. 
		/// </summary>
        public LogEventType EventType {
			get { return eventType; }
		}
		
		/// <summary>
		/// The date and time the even occured. 
		/// </summary>
		public DateTime EventDate {
			get { return eventDate; }
		}
		
		/// <summary>
		/// The source of the event. Can be used with the SourceID to determine the exact source of the event. 
		/// </summary>
        public LogSource Source {
			get { return source; }
		}
		
		/// <summary>
		/// The unique identifier of the event source. 
		/// </summary>
		public Guid SourceID {
			get { return sourceID; }
		}
		
		/// <summary>
		/// The name of this event. 
		/// </summary>
		public string EventName {
			get { return eventName; }
		}
		
		/// <summary>
		/// If event occured under the scope of an authenticated user, this is the user's username. 
		/// </summary>
		public string Username {
			get { return username; }
		}
		
		/// <summary>
		/// If the event occured under the scope of an authenticated user, this is the user's unique identifier. 
		/// </summary>
        public Guid UserID {
            get { return userID; }
        }
		
		/// <summary>
		/// If the event occured in under an HTTP request, this is the IP of the request. 
		/// </summary>
		public string IP {
			get { return ip; }
		}
		
		/// <summary>
		/// A detailed description of the event. 
		/// </summary>
		public string Detail {
			get { return detail; }
		}
		
		
		/// <summary>
		/// Any additional data that the event type may include. 
		/// </summary>
        public string Data {
			get { return data; }
		}

		
		/// <summary>
		//// Logs an event to the system logs. 
		/// </summary>
		/// <param name="eventType">
		/// A <see cref="LogEventType"/>
		/// </param>
		/// <param name="source">
		/// A <see cref="LogSource"/>
		/// </param>
		/// <param name="sourceID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="eventName">
		/// A <see cref="System.String"/>
		/// </param>
		/// <param name="detail">
		/// A <see cref="System.String"/>
		/// </param>
        public static void LogEvent(LogEventType eventType, LogSource source, Guid sourceID, string eventName, string detail) {
			
			HttpContext context = HttpContext.Current;

            LogEntry e = new LogEntry();
			e.eventDate = DateTime.Now;
			e.eventType = eventType;
			e.eventName = eventName;
			e.source = source;
			e.sourceID = sourceID;
			if(context != null){
				if(context.User.Identity.IsAuthenticated){
					e.username = context.User.Identity.Name;
                    e.userID = (Guid)Membership.GetUser().ProviderUserKey;
				}
				e.ip = context.Request.UserHostAddress;
			}
			e.detail = detail;

            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            ds.Insert(e);
		}
		
		/// <summary>
		/// Finds all events of a specified type for an event source. 
		/// </summary>
		/// <param name="eventType">
		/// A <see cref="LogEventType"/>
		/// </param>
		/// <param name="source">
		/// A <see cref="LogSource"/>
		/// </param>
		/// <param name="sourceID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
		public static IList FindBy(LogEventType eventType, LogSource source, Guid sourceID){ //TODO: Rename BySource
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(LogEntry), "WHERE Type=@Type AND Source=@Source AND SourceID=@SourceID ORDER BY EventDate DESC");
			cmd.CreateInputParameter("@Type", eventType);
			cmd.CreateInputParameter("@Source", source);
			cmd.CreateInputParameter("@SourceID", sourceID);
			return cmd.Execute();
		}
		
		/// <summary>
		/// Finds all events in the past amount of days for a user.
		/// </summary>
		/// <param name="userID">
		/// A <see cref="Guid"/>
		/// </param>
		/// <param name="days">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="IList"/>
		/// </returns>
        public static IList FindByUser(Guid userID, int days) {
            DataStore ds = DataStoreServices.GetDataStore("Xenosynth");
            IFindCollectionCommand cmd = ds.CreateFindCollectionCommand(typeof(LogEntry), "WHERE UserID=@UserID AND EventDate > @Date ORDER BY EventDate DESC");
            cmd.CreateInputParameter("@UserID", userID);
            cmd.CreateInputParameter("@Date", DateTime.Now.AddDays(-days));
            return cmd.Execute();
        }
    }
} 
