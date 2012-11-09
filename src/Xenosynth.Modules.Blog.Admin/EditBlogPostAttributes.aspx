<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="EditBlogPostAttributes.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.EditBlogPostAttributes" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="EditFileAttributes" Src="~/Modules/Cms/Controls/EditFileAttributes.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>

<%@ Import namespace="Xenosynth.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Side" runat="server">
	<xs:BlogPostTasks ID="BlogPostTasks1" runat="server" CurrentFile="<%# CurrentFile %>" />
    <xs:BlogTasks ID="BlogTasks1" runat="server" CurrentBlog="<%# CurrentFile.ParentDirectory %>" />
</asp:Content>
			
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
    	
            <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
            <xs:BlogPostTabControl ID="BlogPostTabControl1" runat="server" FileID="<%# CurrentFile.ID %>"  Selected="Attributes" />
            <div class="formPanel">
            <fieldset>
                <xs:EditFileAttributes ID="EditFileAttributes1" runat="server" MessageBoxControl="MessageBox1" />
            </fieldset>
            </div>
        			
		</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>

