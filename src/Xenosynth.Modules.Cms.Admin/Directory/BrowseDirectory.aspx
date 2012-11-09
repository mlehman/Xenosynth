<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="BrowseDirectory.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Content._Default" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Modules.Cms.Admin" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="DirectoryTasks" Src="DirectoryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<asp:Content runat="server" ContentPlaceHolderID="Side">
    <xs:DirectoryTasks runat="server" CurrentDirectory="<%# CurrentDirectory %>" />
	<xs:SearchFiles runat="server" />
</asp:Content>
			
<asp:Content runat="server"  ContentPlaceHolderID="Main">
	
	<asp:UpdatePanel ID="UpdatePanelMessageBox" runat="server" UpdateMode="Always">
	<ContentTemplate>			
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	</ContentTemplate>
	</asp:UpdatePanel>
	
	<xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDirectory %>"   ID="PathBrowser1" />
	
	<xs:SlidePanel runat="Server">
	    <SidePanelTemplate>
	       <xs:FileExplorer ID="FileExplorer1" runat="server" CurrentFile="<%# CurrentDirectory %>" />
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
		        EnableViewState="True"
		        DataKeyField="ID"
		        OnDeleteCommand="DataGridFiles_OnDeleteCommand"
		        OnItemCommand="DataGridFiles_OnItemCommand"
		        AllowPaging="true"
		        PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
		        >
		            <Columns>
			            <asp:TemplateColumn HeaderStyle-Wrap="false">
			                <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="Title">Title
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Title") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate>
				                <div class="td_text"><div class="td_action">
					            <asp:HyperLink Runat="server" 
					                Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					                CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					                NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "DefaultActionUrl") %>' 
					                ><%# DataBinder.Eval(Container.DataItem, "Title") %></asp:HyperLink>
					            </div></div>
				            </ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn HeaderStyle-Wrap="false">
			                <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="FileName">File Name
						            <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileName") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileName") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn HeaderStyle-Wrap="false" HeaderStyle-Width="100">
			                <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="FileTypeID">Type
						            <asp:Image  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileTypeID") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileType.Name") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn HeaderStyle-Wrap="false" HeaderStyle-Width="60">
			                <HeaderTemplate ><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="Version">Version
						            <asp:Image ID="Image1"  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Version") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "Version") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn HeaderStyle-Wrap="false" HeaderStyle-Width="60">
			                <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="State">State
						            <asp:Image  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("State") + ".gif" %>' /></asp:LinkButton>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "State") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn  HeaderStyle-Wrap="false"  HeaderStyle-Width="75">
			                <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="DateModified">
						            <nobr>
						                Modified
						                <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("DateModified") + ".gif" %>' /></asp:LinkButton>
						            </nobr>
				            </HeaderTemplate>
				            <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "DateModified", "{0:d}") %></div></div></ItemTemplate>
			            </asp:TemplateColumn>
            			
			            <asp:TemplateColumn SortExpression="SortOrder" HeaderStyle-Wrap="false" HeaderStyle-Width="75" ItemStyle-Wrap="false">
			                <HeaderTemplate><asp:LinkButton Runat="Server" CommandName="Sort" CommandArgument="SortOrder">
			                        <nobr>
			                            Order
					                    <asp:Image runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("SortOrder") + ".gif" %>' /></asp:LinkButton>
				                    </nobr>
				            </HeaderTemplate>
				            <ItemTemplate>
					            <xs:SortControl Runat="server"
						            FileID='<%# DataBinder.Eval(Container.DataItem,"ID") %>'
						            SortOrder='<%# DataBinder.Eval(Container.DataItem,"SortOrder") %>'
						            Count='<%# CurrentDirectory.Files.Count %>'
						             />
				            </ItemTemplate>
			            </asp:TemplateColumn>
			            <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" HeaderStyle-Wrap="false" HeaderStyle-Width="50">
				            <ItemTemplate>
				                <nobr>
				                    <asp:HyperLink ID="HyperLink2" Runat="server" Title="Edit" 
				                        CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				                        NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
				                    <asp:LinkButton ID="DeletePageButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					                <ab:AlertButton runat="server" Control="DeletePageButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Delete {0}?") %>' DialogMode="Confirm" />
				                </nobr>
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
		        <span class="warning">This directory does not contain any files.</span>
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