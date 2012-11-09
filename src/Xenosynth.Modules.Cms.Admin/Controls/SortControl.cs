using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using Xenosynth.Web.UI;

namespace Xenosynth.Admin.Controls {

	public enum SortControlMode {
		Buttons,
		DropDown,
        TextBox
	}

	/// <summary>
	/// Summary description for SortControl.
	/// </summary>
	[ToolboxData("<{0}:SortControl runat=server></{0}:SortControl>")]
	public class SortControl : System.Web.UI.Control {

		DropDownList DropDownSort;
        TextBox TextBoxSort;

		private CmsFile fileCache;

		public CmsFile File {
			get { 
				if(fileCache == null){
					fileCache = CmsFile.FindByID(FileID);
				}
				return fileCache;
			}
		}

		public Guid FileID {
			get {
				if(ViewState["FileID"] == null){
					return Guid.Empty;
				} else {
					return (Guid)ViewState["FileID"];
				}
			}
			set { ViewState["FileID"] = value; }
		}

		public int SortOrder {
			get { return (int)ViewState["SortOrder"];}
			set { ViewState["SortOrder"] = value; }
		}

		public int Count {
			get { return (int)ViewState["Count"];}
			set { ViewState["Count"] = value; }
		}

		public SortControlMode SortMode {
			get {
                return (SortControlMode)XenosynthContext.Current.Configuration.GetValue("xenosynth.preferences.paging.sortControlMode", true);
			}
		}

		public SortControl(){
		}

		protected override void OnLoad(EventArgs e) {
			base.OnLoad (e);
			EnsureChildControls();
		}


		protected override void CreateChildControls() {
			
			//TODO: Push to config class?
			
			if(SortMode == SortControlMode.DropDown){
                DropDownSort = new DropDownList();
				this.Controls.Add(DropDownSort);
				DropDownSort.SelectedIndexChanged += new EventHandler(DropDownSort_SelectedIndexChanged);
				DropDownSort.AutoPostBack = true;
			} else if(SortMode == SortControlMode.TextBox){
                TextBoxSort = new TextBox();
                this.Controls.Add(TextBoxSort);
                TextBoxSort.Width = 40;
                TextBoxSort.ToolTip = "Update Sort Order";
                LinkButton LinkButtonUpdate = new LinkButton();
                this.Controls.Add(LinkButtonUpdate);
                LinkButtonUpdate.CssClass = "action move";
                LinkButtonUpdate.Click += new EventHandler(LinkButtonUpdate_Click);
				
			} else {
				LinkButton LinkButtonMoveUp = new LinkButton();
				this.Controls.Add(LinkButtonMoveUp);
				LinkButtonMoveUp.CssClass = "action moveUp";
				LinkButtonMoveUp.Click += new EventHandler(LinkButtonMoveUp_Click);

				LinkButton LinkButtonMoveDown = new LinkButton();
				this.Controls.Add(LinkButtonMoveDown);
				LinkButtonMoveDown.CssClass = "action moveDown";
				LinkButtonMoveDown.Click += new EventHandler(LinkButtonMoveDown_Click);
			}
		}

		protected override void OnDataBinding(EventArgs e) {
			EnsureChildControls();
			base.OnDataBinding (e);
							
			if(SortMode == SortControlMode.DropDown){
				
				for(int i = 0; i < Count; i++){
					ListItem item = new ListItem((i + 1).ToString(), i.ToString());
					DropDownSort.Items.Add(item);
				}
				
				//check range
				SortOrder = Math.Max(0,SortOrder);
				SortOrder = Math.Min(DropDownSort.Items.Count - 1, SortOrder);

				DropDownSort.SelectedIndex = SortOrder;
            } else if (SortMode == SortControlMode.TextBox) {
                TextBoxSort.Text = (SortOrder + 1).ToString();
            }
		}


		private void LinkButtonMoveUp_Click(object sender, EventArgs e) {
			File.MoveSortOrder(true);
			File.ParentDirectory.UpdateSortOrder();
			BubbleSortOrderUpdatedCommand();
		}

		private void LinkButtonMoveDown_Click(object sender, EventArgs e) {
			File.MoveSortOrder(false);
			File.ParentDirectory.UpdateSortOrder();
			BubbleSortOrderUpdatedCommand();
		}

		public void BubbleSortOrderUpdatedCommand(){
			this.RaiseBubbleEvent(this, new CommandEventArgs("SortOrderUpdated", FileID));
		}

		private void DropDownSort_SelectedIndexChanged(object sender, EventArgs e) {
			int selectedIndex = int.Parse(DropDownSort.SelectedValue);
			File.MoveSortOrder(selectedIndex);
			BubbleSortOrderUpdatedCommand();
		}

        private void LinkButtonUpdate_Click(object sender, EventArgs e) {
            try {
                int selectedIndex = int.Parse(TextBoxSort.Text);
                selectedIndex = Math.Max(1, selectedIndex);
                File.MoveSortOrder(selectedIndex-1);
            } catch { }
           
            BubbleSortOrderUpdatedCommand();
        }
	}
}
