<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="MoveFile.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.MoveFile" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="FileExplorer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
			
		<xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    		
		<p class="block">Select a directory and click 'Move' to move this file to another location.</p>
		
		<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
		<fieldset>
			<legend>File Location</legend>
			
			<asp:PlaceHolder Runat="server" ID="PlaceHolderAction">
				
				<asp:RadioButtonList Runat="server" CssClass="checkbox" RepeatDirection="Horizontal" ID="RadioButtonListAction">
					<asp:ListItem Selected="True" Value="Move">Move</asp:ListItem>
					<asp:ListItem Value="Copy">Copy</asp:ListItem>
				</asp:RadioButtonList>
				<br />
			</asp:PlaceHolder>
		
			<label for="TextBoxFileName">File Name:</label>  <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxFileName"  MaxLength="100"/> <br />
			<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." ID="Requiredfieldvalidator1" NAME="Requiredfieldvalidator1"/>
		<asp:RegularExpressionValidator ID="RegularExpressionValidatorFileName" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" />
			<span class="toolTip">The name as it will appear in the URL ( http://www.yoursite.com/filename.aspx ).</span>
			
			<label for="TextBoxFullPath">Directory:</label>  <asp:DropDownList CssClass="input" ID="DropDownListDirectories" Runat="server" /> <br />
			
			<asp:Button Runat="server" CssClass="submit" Text="Move &raquo;" OnClick="ButtonUpdateFile_OnClick" ID="Button1"/>
			<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" ID="Button2" NAME="Button2"/>
		</fieldset>
		
		<DataBinding:DataBindingManager ID="DataBindingManagerFile" runat="server">
			<DataBinding:TextBoxBinding ControlToBind="TextBoxFileName" DataMember="FileName" />
			<DataBinding:ListControlBinding ControlToBind="DropDownListDirectories" DataMember="ParentID" />
		</DataBinding:DataBindingManager>
			
			</MainPanelTemplate>
    </xs:SlidePanel>	
				
</asp:Content>


