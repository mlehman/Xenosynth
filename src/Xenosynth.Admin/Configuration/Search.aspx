<%@ Page Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Xenosynth.Admin.Configuration.Search" Title="Untitled Page" %>
<%@ Register TagPrefix="xs" TagName="MessageBox" Src="~/Controls/MessageBox.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Main" runat="server">
<xs:MessageBox runat="server" ID="MessageBox1" CssClass="message" />
	<fieldset>
		<legend>Search Index Details</legend>
		
		<asp:Button Runat="server" CssClass="submit" Text="Rebuild Index &raquo;" OnClick="ButtonRebuild_OnClick" ID="Button1"/>
	</fieldset>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Side" runat="server">
</asp:Content>
