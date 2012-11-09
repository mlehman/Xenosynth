<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UsersOnline.ascx.cs" Inherits="Xenosynth.Admin.Controls.WebParts.UsersOnline" %>

<asp:DataGrid runat="server" ID="DataGridUsers">
    <Columns>
	    <asp:TemplateColumn HeaderText="UserName"  HeaderStyle-Wrap="false">
		    <ItemTemplate>
			    <%# Eval("UserName")%>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
	    <asp:TemplateColumn HeaderText="Email"  HeaderStyle-Wrap="false">
		    <ItemTemplate>
			    <%# Eval("Email") %>
		    </ItemTemplate>
	    </asp:TemplateColumn> 
    </Columns>
</asp:DataGrid>