<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddShortcut.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Shortcut.AddShortcut" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ShortcutTabControl" Src="ShortcutTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# ParentDirectory %>" />
</asp:Content>
			
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

 <xs:PathBrowser  runat="server" CurrentFile="<%# ParentDirectory %>"   ID="PathBrowser1" />
 
 <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# ParentDirectory %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	
<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:ShortcutTabControl ID="ShortcutTabControl1" runat="server"  Selected="Properties" />
    <div class="formPanel">
	<fieldset>
		<legend>Shortcut Properties</legend>
		
		<label for="TextBoxTitle" class="required"> Title:</label>  <asp:TextBox CssClass="input" style="width: 250px;" Runat="server" ID="TextBoxTitle"  MaxLength="100"/>
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />
		<br />
		<span class="toolTip">The name that will be displayed.</span>
		
		<label for="TextBoxFileName" class="required"> File Name:</label> <asp:TextBox CssClass="input" Runat="server" style="width: 250px;" ID="TextBoxFileName" MaxLength="100" />
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		<asp:RegularExpressionValidator ID="RegularExpressionValidatorFileName" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" />
		<br />
		<span class="toolTip">The name as it will appear in the URL ( http://www.yoursite.com/filename.aspx ).</span>
		
		<label for="TextBoxExternalUrl" class="required"> External Url:</label>  <asp:TextBox CssClass="input" style="width: 500px;" Runat="server" ID="TextBoxExternalUrl"  MaxLength="500"/>
		<asp:RequiredFieldValidator CssClass="error" Runat="server" ControlToValidate="TextBoxExternalUrl" Display="Dynamic" ErrorMessage="Required." />
		<br />
		<span class="toolTip">The external url ( http://www.externalsite.com/filename.html ).</span>
		
		<asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAddShortcut_OnClick" />
		<asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False" />
	</fieldset>
	</div>
	
	
	<DataBinding:DataBindingManager ID="DataBindingManagerShortcut" runat="server">
		<DataBinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		<DataBinding:TextBoxBinding ControlToBind="TextBoxExternalUrl" DataMember="ExternalUrl" />
	</DataBinding:DataBindingManager>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
			
</asp:Content>

