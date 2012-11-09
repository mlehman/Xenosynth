<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="AddTemplateGallery.aspx.cs" Inherits="Xenosynth.Admin.Content.TemplateGallery.AddTemplateGallery" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="TemplateGalleryTabControl" Src="TemplateGalleryTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# ParentDirectory %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# ParentDirectory %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

	<xs:TemplateGalleryTabControl ID="TemplateGalleryTabControl1" runat="server" Selected="Properties" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Template Gallery Properties</legend>
    		
		    <label for="TextBoxDisplayName" class="required">Title:</label>  <asp:TextBox CssClass="input" Runat="server" ID="TextBoxTitle" /> <br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="error" Runat="server" ControlToValidate="TextBoxTitle" Display="Dynamic" ErrorMessage="Required." />				
		    <span class="toolTip">This is the name that will be displayed.</span>
    		
    		<label for="TextBoxFileName" class="required">File Name:</label> <asp:TextBox CssClass="input" Runat="server" ID="TextBoxFileName" /><br />
		    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="error" Runat="server" ControlToValidate="TextBoxFileName" Display="Dynamic" ErrorMessage="Required." />
		    <span class="toolTip">This is the name as it will appear in the URL ( http://www.yoursite.com/filename ).</span>
		    
		    <asp:Button Runat="server" CssClass="submit" Text="Create &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerGallery" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
	</databinding:DataBindingManager>
	
            </MainPanelTemplate>
        </xs:SlidePanel>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:SearchFiles ID="SearchFiles1" runat="server" />
</asp:Content>
