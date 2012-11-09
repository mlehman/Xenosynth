<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="AddDirectory.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content.AddDirectory" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTabControl" Src="DirectoryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="Side">
	<xs:DirectoryTasks CurrentDirectory="<%# ParentDirectory %>"   runat="server" />
	<xs:SearchFiles runat="server" />
</asp:Content>

<asp:Content ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFIle="<%# ParentDirectory %>"   ID="PathBrowser1" /> 
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# ParentDirectory %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	<xs:DirectoryTabControl ID="DirectoryTabControl1" runat="server" Selected="Properties" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Directory Properties</legend>
		    
		    <label for="TextBoxTitle" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> <br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />				
		    <span class="toolTip">This is the name that will be displayed.</span>
		    
		    <label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" /><br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		    <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>

		    <label for="DropDownListTemplateGalleries">Template Gallery:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListTemplateGalleries" /> <br />
		    <span class="toolTip">The gallery of templates for the look and feel of the pages.</span>
    		
		    <label for="DropDownListImageGalleries">Image Gallery:</label>  <asp:DropDownList CssClass="input" Runat="server" ID="DropDownListImageGalleries" /> <br />
		    <span class="toolTip">The media gallery for the pages.</span>
    		
		    <asp:CheckBox CssClass="checkbox" Runat="server" ID="CheckBoxIsHidden" Text="Is hidden?" />
		    <span class="toolTip">Whether the directory is hidden in the navigation.</span>
    		
		    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAddDirectory_OnClick" ID="Button1"/>
		    <asp:Button Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerDirectory" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
		<databinding:ListControlBinding ControlToBind="DropDownListTemplateGalleries" DataMember="TemplateGalleryID" />
		<databinding:ListControlBinding ControlToBind="DropDownListImageGalleries" DataMember="ImageGalleryID" />
		<databinding:CheckBoxBinding ControlToBind="CheckBoxIsHidden" DataMember="IsHidden" />
	</databinding:DataBindingManager>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
