<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RecentFileList.ascx.cs" Inherits="Xenosynth.Admin.Controls.WebParts.RecentFileList" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>					

<asp:DataGrid runat="server" ID="DataGridRecentFiles">
    <Columns>
         <asp:TemplateColumn HeaderText="Title"  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn" HeaderStyle-Wrap="false">
		    <ItemTemplate>
			    <asp:HyperLink ID="HyperLink2" Runat="server" Title='<%# Eval("Title") %>'  CssClass='<%# Eval("FileType.CssClass") + " action" %>' 
			    NavigateUrl='<%# Eval("Url") %>' 
			    ><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false) %></asp:HyperLink>
		    </ItemTemplate>
	    </asp:TemplateColumn>
	    <asp:TemplateColumn HeaderText="Type"  HeaderStyle-Wrap="false"  ItemStyle-Width="20%">
		    <ItemTemplate>
			    <%# Eval("FileType.Name") %>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
	    <asp:TemplateColumn HeaderText="Version"  HeaderStyle-Wrap="false"  ItemStyle-Width="5%">
		    <ItemTemplate>
			    <%# Eval("File.Version")%>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
	    <asp:TemplateColumn HeaderText="Modified"  HeaderStyle-Wrap="false" ItemStyle-Width="20%">
		    <ItemTemplate>
			    <%# Eval("File.DateModified", "{0:d}")%>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
    </Columns>
</asp:DataGrid>
<erp:EmptyRepeaterPanel runat="server" control="DataGridRecentFiles" ID="EmptyRepeaterPanel1" >
    You have no recent files.
</erp:EmptyRepeaterPanel>