<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BrowseFiles.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.BrowseFiles" %>
<%@ Register TagPrefix="xs" TagName="SearchFiles" Src="SearchFiles.ascx" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls" Assembly="Xenosynth.Modules.Cms.Admin" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="xs" TagName="PathBrowser" Src="PathBrowser.ascx" %>
<%@ Register TagPrefix="xs" TagName="FileExplorer" Src="FileExplorer.ascx" %>
<%@ Register TagPrefix="xs" TagName="SlidePanel" Src="SlidePanel.ascx" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>File Browser</title>
</head>
<body>
    <script language="javascript">
 
     function onSelected(fileID){
        window.opener.document.getElementById('<%= Request["TargetID"] %>').value = fileID;
        window.close(); 
     }
     
    </script>

    <div style="background-color: #ffffff;">
    <form id="form1" runat="server">
    <xs:PathBrowser  runat="server" CurrentFile="<%# CurrentDirectory %>" DefaultUrl='<%# "BrowseFiles.aspx?TargetID=" + Request["TargetID"] %>' ID="PathBrowser1" />
	
	        <asp:DataGrid ID="DataGridFiles" 
		        Runat="Server"
		        EnableViewState="False"
		        DataKeyField="ID"
		        PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
		        >
		        <Columns>
		            
			        <asp:TemplateColumn  HeaderStyle-Wrap="false">
			            <HeaderTemplate><asp:LinkButton ID="LinkButton1" Runat="Server" CommandName="Sort" CommandArgument="Title">Title
						        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("Title") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate>
					        <asp:HyperLink ID="HyperLink1" Runat="server" 
					            Title='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "Title", "Browse - {0}") : DataBinder.Eval(Container.DataItem, "Title", "Select - {0}") %>'
					            CssClass='<%# Eval("FileType.CssClass") + ((bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? "Browse action" :  " action") %>' 
					            NavigateUrl='<%# (bool)DataBinder.Eval(Container.DataItem, "FileType.IsDirectory") ? DataBinder.Eval(Container.DataItem, "ID", "BrowseFiles.aspx?FileID={0}&TargetID=" + Request["TargetID"]) : "#" %>' 
					            ><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false)%></asp:HyperLink>
				        </ItemTemplate>
			        </asp:TemplateColumn>
        			
			        <asp:TemplateColumn  >
			            <HeaderTemplate><asp:LinkButton ID="LinkButton3"  Runat="Server" CommandName="Sort" CommandArgument="FileTypeID">Type
						        <asp:Image ID="Image3"  runat="server" ImageUrl='<%# "~/images/icon_" + DataGridAdapterFiles.GetSortOrder("FileTypeID") + ".gif" %>' /></asp:LinkButton>
				        </HeaderTemplate>
				        <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "FileType.Name") %></ItemTemplate>
			        </asp:TemplateColumn>
        			<asp:TemplateColumn  HeaderStyle-Wrap="false" ItemStyle-Width="60">
				        <ItemTemplate>
					        <a class="action add" href="#" onclick="javascript: onSelected('<%# Eval("FileID") %>');return false;">select</a>
				        </ItemTemplate>
			        </asp:TemplateColumn>		
		        </Columns>
	        </asp:DataGrid>
        	
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
       </div>
    </form>
</body>
</html>
