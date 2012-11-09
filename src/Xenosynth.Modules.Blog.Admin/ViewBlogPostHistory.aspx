<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewBlogPostHistory.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.ViewBlogPostHistory" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms//PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms//FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="ViewFileHistory" Src="~/Modules/Cms/Controls/ViewFileHistory.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	        <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	        <xs:BlogPostTabControl ID="BlogPostTabControl1" runat="server" FileID="<%# CurrentFile.ID %>" Selected="History" />
            <div class="formPanel">
                <xs:ViewFileHistory ID="ViewFileHistory1" runat="server" MessageBoxControl="MessageBox1" />
	        </div>
    	   
	    </MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
     <xs:BlogPostTasks ID="BlogPostTasks1" runat="server" CurrentFile="<%# CurrentFile %>" />
    <xs:BlogTasks ID="BlogTasks1" runat="server" CurrentBlog="<%# CurrentFile.ParentDirectory %>" />
</asp:Content>
