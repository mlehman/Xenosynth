<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="Xenosynth.Admin.Home.SearchResults" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="Search" Src="~/Controls/Search.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">

    <asp:Repeater runat="server" ID="RepeaterSearchResults">
        <ItemTemplate>
            <div>
                <div><%# Eval("Name") %></div>
                <div><%# Eval("Description") %></div>
                <br />
            </div>
        </ItemTemplate>
    </asp:Repeater>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
    <xs:Search runat="server" />
</asp:Content>
