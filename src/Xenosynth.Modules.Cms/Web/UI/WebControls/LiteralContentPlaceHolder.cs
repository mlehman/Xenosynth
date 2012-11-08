using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

using Xenosynth.Data;

namespace Xenosynth.Web.UI.WebControls {


	/// <summary>
	/// A control for creating an editable text field for a content block.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:LiteralContentPlaceHolder runat=server></{0}:LiteralContentPlaceHolder>"),
	ContentType(typeof(LiteralContent))]
	public class LiteralContentPlaceHolder : ContentPlaceHolder {

		private TextBoxMode textMode = TextBoxMode.SingleLine;
		private int width;
			
		private TextBox textBox;
		private Literal literal;
	
		public int Width {
			get { return width; }
			set { width = value; }
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
			if(Content.IsAvailable){
				this.textBox.Text = ((LiteralContent)Content).Text;	
			} else {
				this.textBox.Visible = false;
				Literal l = new Literal();
				this.Controls.Add(l);
				l.Text = string.Format("[Content '{0}' is not registered.]", this.ID);
			}
		}

		protected override void LoadPresentationPlaceHolderContent() {
			EnsureChildControls();
			if(Content.IsAvailable){
				this.literal.Text = ((LiteralContent)Content).Text;
			}
		}

		protected override void SavePlaceHolderContent() {
			EnsureChildControls();
			((LiteralContent)Content).Text = this.textBox.Text;
		}

	}
}
