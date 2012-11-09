<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PathBrowser.ascx.cs" Inherits="Xenosynth.Admin.Content.PathBrowser" %>
<table class="filePathBrowser">
    <tr>
        <td class="label">Path:</td>
        <td width="100%">
            <div class="path">
            <asp:Repeater ID="RepeaterDirectoryPath" Runat="server">
	            <ItemTemplate> / 
	                <asp:HyperLink ID="HyperLink1" Runat="server"
	                    ToolTip='<%# DataBinder.Eval(Container.DataItem, "FileType.DefaultAction") + " - " + DataBinder.Eval(Container.DataItem, "Title")%>' 
	                    NavigateUrl='<%# GetDefaultUrl(Container.DataItem) %>' 
					    >
					    <%# 
				   	        (string)DataBinder.Eval(Container.DataItem, "Title")
                            + (
					            (int)DataBinder.Eval(Container.DataItem, "Version") > 1 ? " (Version:" + DataBinder.Eval(Container.DataItem, "Version") + ")" :  ""
					        )
					    %>    
			        </asp:HyperLink>
	            </ItemTemplate>
            </asp:Repeater>
            </div>
        </td>
        <%if (DropDownListView.Items.Count > 0) { %>
        <td class="label">
            View:
        </td>
        <td>
            <asp:DropDownList ID="DropDownListView" runat="server" AutoPostBack="True">
            </asp:DropDownList>
        </td>
        <% } %>
    </tr>
</table>