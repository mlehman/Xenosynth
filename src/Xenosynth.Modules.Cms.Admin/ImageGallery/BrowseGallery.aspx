<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="BrowseGallery.aspx.cs" Inherits="Xenosynth.Admin.Content.ImageGallery.BrowseGallery" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="../PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="../FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Modules.Cms.Admin" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="ImageGalleryTasks" Src="ImageGalleryTasks.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="../SlidePanel.ascx" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="../SearchFiles.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:ImageGalleryTasks runat="server" CurrentGallery="<%# CurrentDirectory %>"  />
    <xs:SearchFiles runat="server" />
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
   
	<xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDirectory %>" ID="PathBrowser1" />
	<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	
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
        	    

	        <asp:DataGrid ID="DataGridFiles" 
		        Runat="Server"
		        EnableViewState="False"
		        DataKeyField="ID"
		        OnDeleteCommand="DataGridFiles_OnDeleteCommand"
		        OnItemCommand="DataGridFiles_OnItemCommand"
		        Visible="True"
		        AllowPaging="True"
		        PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
		        >
		        <Columns>
			        <asp:TemplateColumn HeaderStyle-Wrap="false" ItemStyle-Wrap="false">
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
			        <asp:TemplateColumn >
			            <HeaderTemplate><asp:LinkButton ID="LinkButton2" Runat="Server" CommandName="Sort" CommandArgument="FileName">File Name
						        <asp:Image ID="Image2" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileName") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileName") %></div></div></ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderStyle-Width="100">
			            <HeaderTemplate><asp:LinkButton ID="LinkButton3"  Runat="Server" CommandName="Sort" CommandArgument="FileTypeID">Type
						        <asp:Image ID="Image3"  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileTypeID") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "FileType.Name") %></div></div></ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderStyle-Width="60">
			            <HeaderTemplate><asp:LinkButton  Runat="Server" CommandName="Sort" CommandArgument="Version">Version
						        <asp:Image  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Version") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "Version")%></div></div></ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderStyle-Width="60">
			            <HeaderTemplate><asp:LinkButton ID="LinkButton4"  Runat="Server" CommandName="Sort" CommandArgument="State">State
						        <asp:Image ID="Image4"  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("State") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "State") %></div></div></ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderStyle-Width="75" HeaderStyle-Wrap="false">
			            <HeaderTemplate><asp:LinkButton ID="LinkButton5"  Runat="Server" CommandName="Sort" CommandArgument="DateModified">Modified
						        <asp:Image ID="Image5" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("DateModified") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><div class="td_text"><div class="td_text"><%# DataBinder.Eval(Container.DataItem, "DateModified", "{0:d}") %></div></div></ItemTemplate>
			        </asp:TemplateColumn>
			        <asp:TemplateColumn HeaderStyle-Width="75" SortExpression="SortOrder" HeaderStyle-Wrap="false">
			            <HeaderTemplate><asp:LinkButton ID="LinkButton6" Runat="Server" CommandName="Sort" CommandArgument="SortOrder">Order
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
			        <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" HeaderStyle-Width="75">
				        <ItemTemplate>
				            <nobr>
				            <asp:HyperLink  Runat="server" 
					                    Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					                    CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					                    NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "Url") %>' 
					                    ></asp:HyperLink>
				            <asp:HyperLink  Runat="server" Title="Edit" 
				                CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				                NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
				            
				            <asp:LinkButton ID="DeletePageButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					        <ab:AlertButton runat="server" Control="DeletePageButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Permanently delete {0}?") %>' DialogMode="Confirm" />
        				
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
        	
	        <asp:DataList runat="server" ID="DataListThumbnails" RepeatDirection="Horizontal" RepeatLayout="Flow">
	            <ItemTemplate>
	                <div style="float: left; margin: 2px; height: 140px; width: 104px;">
	                    <div style="border: solid 1px #999999; height: 100px; width: 104px;">
	                        <asp:HyperLink  Runat="server" style="display: block; height: 100px; width: 100px; text-align: center; display: table-cell; vertical-align: middle;"
					            Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					            NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "Url") %>' 
					            >
	                            <img src="<%# CurrentDirectory.Url + "_t/" + Eval("Filename") %>" align="middle" border="0" />
	                         </asp:HyperLink>
	                    </div>
	                    <div style="text-align: center; padding: 2px;">
        	                
	                        <div style="text-align: right;">
        	                    
	                            <asp:HyperLink  Runat="server" Title="Edit" 
				                    CssClass='<%# "action " + DataBinder.Eval(Container.DataItem, "FileType.CssClass", "{0}") + "Edit" %>' 
				                    NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "FileType.EditUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}")  %>'  ></asp:HyperLink>
        				        
	                            <asp:HyperLink  Runat="server" 
					                Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "View - {0}") %>'
					                CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					                NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "FileType.BrowseUrl") + DataBinder.Eval(Container.DataItem, "ID", "?FileID={0}") : DataBinder.Eval(Container.DataItem, "Url") %>' 
					                ></asp:HyperLink>
        					        
					             <asp:LinkButton ID="DeletePageButton" Title="Delete" Runat="server" CssClass="action delete" CommandName="Delete"></asp:LinkButton>
					            <ab:AlertButton ID="AlertButton2" runat="server" Control="DeletePageButton" Message='<%# DataBinder.Eval(Container.DataItem, "Title", "Permanently delete {0}?") %>' DialogMode="Confirm" />
        	                
	                        </div>
        	                
	                        <%# Eval("Title") %>
        	                
	                    </div>
	                </div>
	            </ItemTemplate>
	        </asp:DataList>
        	
		            </ContentTemplate>
	        </asp:UpdatePanel>
        	
	        <% if(DataGridFiles.Visible) { %>
	        <div class="selectedActions" >
	            <fieldset style="display: inline;">
	                <legend>Select Files</legend>
	                <asp:LinkButton Runat="server"  Title="Publish" CssClass="action publish" ID="ButtonPublishSelected" OnClick="ButtonPublishSelected_OnClick"> Publish </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonPublishSelected" Message="Publish selected files?" DialogMode="Confirm" ID="Alertbutton2"/>
		            <asp:LinkButton Runat="server"  Title="Delete" CssClass="action delete" ID="ButtonDeleteSelected" OnClick="ButtonDeleteSelected_OnClick"> Delete </asp:LinkButton>
		            <ab:AlertButton runat="server" Control="ButtonDeleteSelected" Message="Permanently delete selected files?" DialogMode="Confirm" ID="Alertbutton1"/>
	            </fieldset>
	        </div>
	        <% } %>
    	
        
    	
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