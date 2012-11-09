<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewBlogPostRevisions.aspx.cs" Inherits="Xenosynth.Modules.Blog.Admin.ViewBlogPostRevisions" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="~/Modules/Cms//PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogTasks" Src="BlogTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTasks" Src="BlogPostTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="BlogPostTabControl" Src="BlogPostTabControl.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="~/Modules/Cms/SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="~/Modules/Cms//FileExplorer.ascx" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentFile %>"   ID="PathBrowser1" />
    
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentFile %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:BlogPostTabControl ID="BlogPostTabControl1" runat="server" FileID="<%# CurrentFile.ID %>" Selected="Revisions" />
	<div class="formPanel">
    <asp:DataGrid ID="DataGridRevisionHistory" Runat="Server"
		AutoGenerateColumns="False"
		GridLines="None"
		DataKeyField="ID"
		CssClass="grid"
		Width="100%"
		HeaderStyle-CssClass="gridHeader"
		AlternatingItemStyle-CssClass="altRow"
		OnDeleteCommand="DataGridRevisionHistory_OnDeleteCommand"
		>
		<Columns>
			<asp:TemplateColumn HeaderText="File Name" HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn">
				<ItemTemplate>
					<asp:HyperLink Runat="server" Title='<%# DataBinder.Eval(Container.DataItem, "Title", "View Page - {0}") %>' CssClass="page action" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "VersionUrl") %>' ID="Hyperlink1" NAME="Hyperlink1"><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false) %></asp:HyperLink>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:BoundColumn DataField="Version" HeaderText="Version" />
			<asp:BoundColumn DataField="State" HeaderText="State" />
			<asp:BoundColumn DataField="DateModified" HeaderText="Date Modified" DataFormatString="{0:d}" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn"/>
			<asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				<ItemTemplate>
					<a class="pageMove action" title="Copy/Move Revision" href="MoveFile.aspx?FileID=<%# DataBinder.Eval(Container.DataItem, "ID") %>"></a>
					<asp:LinkButton Title="Delete Old Archives" CssClass="action delete" ID="DeleteTemplateButton" Runat="server" CommandName="Delete" ></asp:LinkButton>
					<ab:AlertButton runat="server" Control="DeleteTemplateButton" Message='Permanently delete old archives from this point?' DialogMode="Confirm" ID="Alertbutton1"/>
				</ItemTemplate>
			</asp:TemplateColumn>
		</Columns>
	</asp:DataGrid>
	<erp:EmptyRepeaterPanel runat="server" control="DataGridRevisionHistory" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		<span class="warning">This file has no revisions.</span>
	</erp:EmptyRepeaterPanel>
	
        
    </div>

	</MainPanelTemplate>
    </xs:SlidePanel>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:BlogPostTasks ID="BlogPostTasks1" runat="server" CurrentFile="<%# CurrentFile %>" />
    <xs:BlogTasks ID="BlogTasks1" runat="server" CurrentBlog="<%# CurrentFile.ParentDirectory %>" />
</asp:Content>
