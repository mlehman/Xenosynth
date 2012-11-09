<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="BrowseLibrary.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.DocumentLibrary.BrowseLibrary" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="DocumentLibraryTasks" Src="DocumentLibraryTasks.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Modules.Cms.Admin" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
    <xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDirectory %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	        <xs:FileExplorer runat="server" CurrentFile="<%# CurrentDirectory %>" />
	    </SidePanelTemplate>
	    <MainPanelTemplate>

	        
	            <asp:UpdatePanel ID="UpdatePanelFiles" runat="server" UpdateMode="Always">
	                <Triggers>
	                    <asp:AsyncPostbackTrigger ControlID="DataGridFiles" EventName="SelectedIndexChanged" />
	                </Triggers>
	                <ContentTemplate>
            	    
                <div style="overflow-x: auto;" >
	            <asp:DataGrid ID="DataGridFiles" 
		            Runat="Server"
		            EnableViewState="False"
		            DataKeyField="ID"
		            OnDeleteCommand="DataGridFiles_OnDeleteCommand"
		            OnItemCommand="DataGridFiles_OnItemCommand"
		            PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
		            >
		            <Columns>
			            <asp:TemplateColumn HeaderStyle-Wrap="false">
			                <HeaderTemplate><asp:LinkButton ID="LinkButton1" Runat="Server" CommandName="Sort" CommandArgument="Title">Title
						            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Title") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate>
				            <div class="td_text"><div class="td_action">
					        <asp:HyperLink ID="HyperLink1" Runat="server" 
					            Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "Edit - {0}") %>'
					            CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  "Edit action") %>' 
					            NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "DefaultActionUrl") %>' 
					            ><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false)%></asp:HyperLink>
				            </div></div>
				        </ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn>
			                <HeaderTemplate><asp:LinkButton ID="LinkButton2" Runat="Server" CommandName="Sort" CommandArgument="FileName">File Name
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileName") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileName") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn  HeaderStyle-Width="100">
			                <HeaderTemplate><asp:LinkButton ID="LinkButton3"  Runat="Server" CommandName="Sort" CommandArgument="FileTypeID">Type
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileTypeID") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileType.Name") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn HeaderStyle-Width="60">
			                <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="Version">Version
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Version") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "Version") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn  HeaderStyle-Width="60">
			                <HeaderTemplate><asp:LinkButton ID="LinkButton5"  Runat="Server" CommandName="Sort" CommandArgument="State">State
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("State") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "State") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn  HeaderStyle-Wrap="false"  HeaderStyle-Width="75">
			                <HeaderTemplate><asp:LinkButton ID="LinkButton6"  Runat="Server" CommandName="Sort" CommandArgument="DateModified">Modified
						            <asp:Image ID="Image5" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("DateModified") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "DateModified", "{0:d}") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn SortExpression="SortOrder" HeaderStyle-Wrap="false">
			                <HeaderTemplate><asp:LinkButton ID="LinkButton7" Runat="Server" CommandName="Sort" CommandArgument="SortOrder">Order
						            <asp:Image ID="Image6" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("SortOrder") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate>
				                <nobr>
					            <xs:SortControl Runat="server"
						            FileID='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
						            SortOrder='<%# DataBinder.Eval(Container.DataItem,"SortOrder") %>'
						            Count='<%# CurrentDirectory.Files.Count %>'
						             />
						        </nobr>
				            </ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" HeaderStyle-Width="100"  ItemStyle-Width="100" ItemStyle-Wrap="false">
				            <ItemTemplate>
				            <nobr>
				                <asp:HyperLink  Runat="server" 
					                    Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					                    CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					                    NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "Url") %>' 
					                    ></asp:HyperLink>
				                <asp:HyperLink ID="HyperLink2" Runat="server" Title="Edit" 
				                    CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
				                <asp:LinkButton ID="DeletePageButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					            <ab:AlertButton runat="server" Control="DeletePageButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Permanently delete {0}?") %>' DialogMode="Confirm" />
				            
				            </ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn HeaderText="Select" HeaderStyle-CssClass="selectColumn" ItemStyle-CssClass="selectColumn">
			                <HeaderTemplate>
			                    <input type="checkbox" onclick="javascript:toggleCheckBoxes(this);" />
			                </HeaderTemplate>
				            <ItemTemplate> 
					            <asp:CheckBox ID="CheckBoxSelect" Runat="server" name="selected" />
				            </ItemTemplate>
			            </asp:TemplateColumn>
		            </Columns>
	            </asp:DataGrid>
            	</div>
		                </ContentTemplate>
	            </asp:UpdatePanel>
            	
	            <% if(DataGridFiles.Visible) { %>
	            <div class="selectedActions" >
	                <fieldset style="display: inline;">
	                    <legend>Selected Files</legend>
	                    <asp:LinkButton Runat="server"  Title="Publish" CssClass="action publish" ID="ButtonPublishSelected" OnClick="ButtonPublishSelected_OnClick"> Publish </asp:LinkButton>
		                <ab:AlertButton runat="server" Control="ButtonPublishSelected" Message="Publish selected files?" DialogMode="Confirm" ID="Alertbutton2"/>
		                <asp:LinkButton Runat="server"  Title="Delete" CssClass="action delete" ID="ButtonDeleteSelected" OnClick="ButtonDeleteSelected_OnClick"> Delete </asp:LinkButton>
		                <ab:AlertButton runat="server" Control="ButtonDeleteSelected" Message="Permanently delete selected files?" DialogMode="Confirm" ID="Alertbutton1"/>
	                </fieldset>
	            </div>
	            <% } %>
            	
	            <erp:EmptyRepeaterPanel runat="server" control="DataGridFiles" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
		            <span class="warning">This document library does not contain any files.</span>
	            </erp:EmptyRepeaterPanel>
	
	            <dga:DataGridAdapter 
                  ID="DataGridAdapterFiles" 
                  Runat="Server" 
                  DataGridToBind="DataGridFiles" 
                  SortHistory="1" 
                  AutoDataBind="False"
                  BaseTypeName="Xenosynth.Web.UI.CmsFile,Xenosynth.Modules.Cms"
                  HideEmptyPager="True"
                  OnDataGridBinding="DataGridAdapterFiles_DataGridBinding"
                  />
    
            </MainPanelTemplate>
        </xs:SlidePanel>
        
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:DocumentLibraryTasks ID="TemplateGalleryTasks1" runat="server" CurrentLibrary="<%# CurrentDirectory %>" />
    <xs:SearchFiles runat="server" />
</asp:Content>
