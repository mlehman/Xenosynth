<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewBlogPostComments.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.ViewBlogPostComments" Title="Untitled Page" %>
<%@ Register TagPrefix="mtv" Namespace="Fluent" Assembly="Fluent.MultiLineTextBoxValidator" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms/PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms/FileExplorer.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>

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
            <xs:BlogPostTabControl ID="BlogPostTabControl1" runat="server" FileID="<%# CurrentFile.ID %>"  Selected="Comments" />
            <div class="formPanel">
                <asp:DataGrid ID="DataGridComments" Runat="Server"
		            AutoGenerateColumns="False"
		            GridLines="None"
		            DataKeyField="ID"
		            CssClass="grid"
		            Width="100%"
		            HeaderStyle-CssClass="gridHeader"
		            AlternatingItemStyle-CssClass="altRow"
		            OnDeleteCommand="DataGridComments_OnDeleteCommand"
		            >
		            <Columns>
			            <asp:BoundColumn DataField="Name" HeaderText="Name" />
			            <asp:BoundColumn DataField="IP" HeaderText="IP" />
			            <asp:BoundColumn DataField="DateCreated" HeaderText="Date Created" DataFormatString="{0:d}" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn"/>
			            <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				            <ItemTemplate>
					            <asp:LinkButton Title="Delete Comment" CssClass="action delete" ID="DeleteCommentButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					            <ab:AlertButton runat="server" Control="DeleteCommentButton" Message='Permanently delete comment?' DialogMode="Confirm" ID="Alertbutton1"/>
				            </ItemTemplate>
			            </asp:TemplateColumn>
		            </Columns>
	            </asp:DataGrid>
	            <erp:EmptyRepeaterPanel runat="server" control="DataGridComments" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		            <span class="warning">This post has no comments.</span>
	            </erp:EmptyRepeaterPanel>
            </div>
        			
		</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
