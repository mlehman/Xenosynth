using System;
using Inform;

namespace Xenosynth.Data {
	
	/// <summary>
	/// This class supports the Xenosynth CMS Module and is not intended to be used directly from your code.
	/// </summary>
	public class LiteralDataItem {

		[MemberMapping(PrimaryKey=true, ColumnName="LiteralItemID")]
		Guid literalItemID;

		[MemberMapping(ColumnName="ControlID", AllowNulls=false)]
		private string controlID;

		[MemberMapping(ColumnName="PageID", AllowNulls=false)]
		private Guid pageID;

		[MemberMapping(ColumnName="Text", DbType="TEXT", AllowNulls=false)]
		private string text;

		internal Guid LiteralItemID {
			get { return literalItemID; }
			set { literalItemID = value; }
		}

		internal string ControlID {
			get { return controlID; }
			set { controlID = value; }
		}

		internal Guid PageID {
			get { return pageID; }
			set { pageID = value; }
		}

		public string Text {
			get { return text; }
			set { text = value; }
		}
		
	}
}
