<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="True" CodeBehind="ViewImageHistory.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.ViewImageHistory" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageTasks" Src="ImageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="ImageTabControl" Src="ImageTabControl.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="ViewFileHistory" Src="../Controls/ViewFileHistory.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
            <xs:ImageTabControl ID="ImageTabControl1" runat="server" FileID="<%# CurrentFile.ID %>" Selected="History" />
            <div class="formPanel">
                <xs:ViewFileHistory ID="ViewFileHistory1" runat="server" MessageBoxControl="MessageBox1" />
            </div>

	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:ImageTasks ID="ImageTasks1" runat="server" CurrentFile="<%# CurrentFile %>" />
    <xs:ImageGalleryTasks ID="ImageGalleryTasks1" runat="server" CurrentGallery="<%# CurrentFile.ParentDirectory %>" />
</asp:Content>
