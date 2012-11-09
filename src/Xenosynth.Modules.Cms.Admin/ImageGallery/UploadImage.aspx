<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.UploadImage" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="ImageTabControl" Src="ImageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageTasks" Src="ImageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:ImageTasks ID="ImageTasks1" runat="server" CurrentFile="<%# CurrentImage %>" />
    <xs:ImageGalleryTasks ID="ImageGalleryTasks1" runat="server" CurrentGallery="<%# CurrentImage.ParentDirectory %>" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentImage %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentImage %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:ImageTabControl ID="ImageTabControl1" runat="server" FileID="<%# CurrentImage.ID %>" Selected="Upload" />
	<div class="formPanel">
	    <fieldset>
		    <legend>Image Properties</legend>
		    
		    <label for="TextBoxID">Select File:</label><input runat="server" type="file" id="HtmlInputFileAttach" class="file"  /><br />
		    <span class="toolTip">Image file (*.gif, *.jpg, *.png)</span>
		  
		    <asp:Button Runat="server" CssClass="submit" Text="Upload &raquo;" OnClick="ButtonAdd_OnClick" ID="Button1"/>
		    <asp:Button ID="Button2" Runat="server" Text="Cancel" OnClick="ButtonCancel_OnClick" CausesValidation="False"/>
	    </fieldset>
	</div>
	
	
	<databinding:DataBindingManager ID="DataBindingManagerImage" runat="server">
		<databinding:TextBoxBinding ControlToBind="TextBoxFileName"  DataMember="FileName" />
		<databinding:TextBoxBinding ControlToBind="TextBoxTitle" DataMember="Title" />
	</databinding:DataBindingManager>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    

</asp:Content>
