<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewImageSizes.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.ViewImageSizes" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageTabControl" Src="ImageTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageTasks" Src="ImageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
     <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentImage %>"   ID="PathBrowser1" />
     
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentImage %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:ImageTabControl ID="ImageTabControl1" runat="server" FileID="<%# CurrentImage.ID %>" Selected="Image Sizes" />
	<div class="formPanel">
	    
	    
	        <navigation:TabControl ID="TabControlImageSizes" SelectedEnabled="true" runat="Server" CssClass="formTabs">
            </navigation:TabControl>
	        
		    <div class="formPanel" style="text-align: center;">
		        ( <%= SelectSize.Width %> x <%= SelectSize.Height %> ) <br />
		        <div style="overflow: auto; width: 100%;">
		        <img src="<%= PreviewImageUrl %>" />
		        </div>
		    </div>
		
	</div>
	
	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ImageTasks ID="ImageTasks1" runat="server" CurrentFile="<%# CurrentImage %>" />
    <xs:ImageGalleryTasks ID="ImageGalleryTasks1" runat="server" CurrentGallery="<%# CurrentImage.ParentDirectory %>" />
</asp:Content>
