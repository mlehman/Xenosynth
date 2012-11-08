using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace Xenosynth.Web.UI.WebControls {
	/// <summary>
	/// A control for creating a text field for a FileAttribute.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:LiteralAttributePlaceHolder runat=server></{0}:LiteralAttributePlaceHolder>")]
	public class LiteralAttributePlaceHolder : AttributePlaceHolder {
	
		private TextBoxMode textMode = TextBoxMode.SingleLine;
		private int width;
		private int rows;
			
		private TextBox textBox;
		private Literal literal;
	
		public int Width {
			get { return width; }
			set { width = value; }
		}

		public int Rows {
			get { return rows; }
			set { rows = value; }
		}

		public TextBoxMode TextMode {
			get {return this.textMode; }
			set { this.textMode = value;}
		}

		protected override void CreateAuthoringChildControls() {

			literal = new Literal();
			literal.ID = this.ID + "Literal1";
			this.Controls.Add(literal);
			literal.Visible = false;

			textBox = new TextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);

			textBox.TextMode = TextMode;
			if(Width > 0){
				textBox.Width = Width;
			}
			if(Rows > 0){
				textBox.Rows = Rows;
			}

			ChildControlsCreated = true;
		}

		protected override void CreatePresentationChildControls() {

			literal = new Literal();
			literal.ID = this.ID + "Literal1";
			this.Controls.Add(literal);

			textBox = new TextBox();
			textBox.ID = this.ID + "TextBox1";
			this.Controls.Add(textBox);
			textBox.Visible = false;

			ChildControlsCreated = true;
		}


		protected override void LoadAuthoringPlaceHolderContent() {
			EnsureChildControls();
			if(Attribute != null){
				this.textBox.Text = Attribute.Value;	
			} 
		}

		protected override void LoadPresentationPlaceHolderContent() {
			EnsureChildControls();
			if(Attribute != null){
				this.literal.Text = Attribute.Value;	
			} 
		}

		protected override void SavePlaceHolderContent() {
			EnsureChildControls();
			Attribute.Value = this.textBox.Text;
		}

	}
	
}
