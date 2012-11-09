<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="ViewPageRevisions.aspx.cs" Inherits="Xenosynth.Admin.Content.Page.ViewPageRevisions" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PageTasks" Src="PageTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="PageTabControl" Src="PageTabControl.ascx" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="../Directory/DirectoryTasks.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentPage %>"   ID="PathBrowser1" />
    
    <xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentPage %>"  />
	    </SidePanelTemplate>
	    <MainPanelTemplate>
	    
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<xs:PageTabControl ID="PageTabControl1" runat="server" FileID="<%# CurrentPage.ID %>" Selected="Revisions" />
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
			<asp:TemplateColumn  HeaderText="Publish Start" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn">
			    <ItemTemplate>
					<%# (bool)Eval("PublishStart.IsNull") ? "" : Eval("PublishStart.Value", "{0:d}")%>
				</ItemTemplate>
			</asp:TemplateColumn>
			<asp:TemplateColumn  HeaderText="Publish End" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn">
			    <ItemTemplate>
					<%# (bool)Eval("PublishEnd.IsNull") ? "" : Eval("PublishEnd.Value", "{0:d}")%>
				</ItemTemplate>
			</asp:TemplateColumn><asp:BoundColumn DataField="DateModified" HeaderText="Date Modified" DataFormatString="{0:d}" HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn"/>
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
		<span class="warning">This page has no revisions.</span>
	</erp:EmptyRepeaterPanel>
	
        
    </div>
    
     </MainPanelTemplate>
    </xs:SlidePanel>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
		<xs:PageTasks Runat="server" CurrentPage="<%# CurrentPage %>" ID="Pagetasks1"/>
		<xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentPage.ParentDirectory %>" />
</asp:Content>
