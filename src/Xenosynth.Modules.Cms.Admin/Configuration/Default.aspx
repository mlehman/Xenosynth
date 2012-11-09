<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Configuration.Default" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="ConfigurationTabControl" Src="ConfigurationTabControl.ascx" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <xs:ConfigurationTabControl ID="ConfigurationTabControl1" runat="server"  Selected="File Types" />
    <div class="formPanel">
 
        <asp:DataGrid ID="DataGridFileTypes" Runat="Server"
	    AutoGenerateColumns="False"
	    GridLines="None"
	    DataKeyField="ID"
	    CssClass="grid"
	    Width="100%"
	    HeaderStyle-CssClass="gridHeader"
	    AlternatingItemStyle-CssClass="altRow"
	    >
	    <Columns>
            <asp:TemplateColumn  HeaderStyle-CssClass="fileNameColumn" ItemStyle-CssClass="fileNameColumn" HeaderStyle-Wrap="false" HeaderText="Name" >
		        <ItemTemplate>
			        <asp:HyperLink Runat="server" 
			            CssClass='<%# Eval("CssClass") + " action" %>' 
			            NavigateUrl='<%# "EditFileType.aspx?FileTypeID=" + Eval("ID") %>' 
			            ><%# Eval("Name")%></asp:HyperLink>
		        </ItemTemplate>
	        </asp:TemplateColumn>
		    <asp:BoundColumn DataField="Description" HeaderText="Description" />
		    <asp:BoundColumn DataField="IsDirectory" HeaderText="Is Directory" />
		    <asp:BoundColumn DataField="IsVersioned" HeaderText="Is Versioned" />
	    </Columns>
    </asp:DataGrid>
    <erp:EmptyRepeaterPanel runat="server" control="DataGridFileTypes" >
	    <span class="warning">There are no registered file types.</span>
    </erp:EmptyRepeaterPanel>

    </div>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
</asp:Content>
