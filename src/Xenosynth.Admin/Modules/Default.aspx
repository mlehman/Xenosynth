<%@ Page MasterPageFile="~/Default.Master" language="c#" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin.Modules._Default" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register namespace="Fluent.DataBinding" tagprefix="DataBinding" assembly="Fluent.DataBinding" %>
<%@ Register TagPrefix="ab" Namespace="Fluent" Assembly="Fluent.AlertButton" %>
<%@ Register TagPrefix="xs" TagName="ModuleTasks" Src="~/Modules/ModuleTasks.ascx" %>

<asp:Content ContentPlaceHolderID="Side" runat="server" >
    <xs:ModuleTasks runat="server" ID="ModuleTasks1"/>	
</asp:Content>
<asp:Content ContentPlaceHolderID="Main" runat="server" >

	<asp:DataGrid ID="DataGridModules" runat="Server" 
        AllowPaging="false"
        >
            <Columns>
                <asp:TemplateColumn HeaderText="Module">
                    <ItemTemplate>
                        <a  class="module action" runat="server"
				            href='<%# DataBinder.Eval(Container.DataItem, "DefaultUrl") %>'
				            ><%# DataBinder.Eval(Container.DataItem, "Name") %></a>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Description">
                    <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Description") %>
                    </ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="Tasks" HeaderStyle-CssClass="tasksColumn" ItemStyle-CssClass="tasksColumn" >
				    <ItemTemplate>
				        <asp:HyperLink ToolTip="Open Module"  runat="server" CssClass="module action" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "ResourceFolder", "~/Modules/{0}/default.aspx") %>'></asp:HyperLink>
				    </ItemTemplate>
			    </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
</asp:Content>