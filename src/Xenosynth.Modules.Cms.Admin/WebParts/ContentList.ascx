<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentList.ascx.cs" Inherits="Xenosynth.Admin.Controls.WebParts.ContentList" %>
<%@ Register TagPrefix="reporting" Namespace="Fluent.Reporting" Assembly="Fluent.Reporting" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Import namespace="Xenosynth.Web" %>
<%@ Import namespace="Xenosynth.Web.UI" %>

<asp:DataGrid runat="server" ID="DataGridFiles">
    <Columns>
         <asp:TemplateColumn HeaderText="Title"  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn" HeaderStyle-Wrap="false">
		    <ItemTemplate>
			    <asp:HyperLink Runat="server" Title='<%# Eval("Title") %>' CssClass='<%# Eval("FileType.CssClass") + " action" %>' NavigateUrl='<%# Eval("DefaultActionUrl") %>' ><%# RestrictedLengthColumn.FormatRestrictedLength(DataBinder.Eval(Container.DataItem, "Title"), 35, "...", false) %></asp:HyperLink>
		    </ItemTemplate>
	    </asp:TemplateColumn>
	    <asp:TemplateColumn HeaderText="Type"  HeaderStyle-Wrap="false"  ItemStyle-Width="20%">
		    <ItemTemplate>
			    <%# Eval("FileType.Name") %>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
	    <asp:TemplateColumn HeaderText="Version"  HeaderStyle-Wrap="false"  ItemStyle-Width="5%">
		    <ItemTemplate>
			    <%# Eval("Version") %>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
	    <asp:TemplateColumn HeaderText="Modified"  HeaderStyle-Wrap="false"  ItemStyle-Width="20%">
		    <ItemTemplate>
			    <%# Eval("DateModified", "{0:d}") %>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
    </Columns>
</asp:DataGrid>
<erp:EmptyRepeaterPanel runat="server" control="DataGridFiles" ID="EmptyRepeaterPanel1" >
    You have no unpublished files.
</erp:EmptyRepeaterPanel>