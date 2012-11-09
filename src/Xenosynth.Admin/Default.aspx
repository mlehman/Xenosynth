<%@ Page language="c#" MasterPageFile="~/Default.Master" Codebehind="Default.aspx.cs" AutoEventWireup="True" Inherits="Xenosynth.Admin._Default" %>
<%@ Register TagPrefix="xs" Namespace="Xenosynth.Admin.Controls.WebParts" Assembly="Xenosynth.Admin" %>

<asp:Content id="Main" ContentPlaceHolderID="Main" runat="server">

	<asp:WebPartManager runat="server" ID="WebPartManager1" ></asp:WebPartManager>
	
	<div class="webPartManagerControls">
	    <% if (WebPartManager1.DisplayMode != WebPartManager.BrowseDisplayMode){ %>
	        <asp:LinkButton ID="LinkButton1" OnClick="LinkButtonBrowse_OnClick" runat="server" CssClass="edit action">Finished</asp:LinkButton> 
	    <% } %>
	    <% if (WebPartManager1.DisplayMode != WebPartManager.EditDisplayMode){ %>
            <asp:LinkButton ID="LinkButtonEdit" OnClick="LinkButtonEdit_OnClick" runat="server" CssClass="edit action">Customize</asp:LinkButton> 
        <% } %>
        <% if (WebPartManager1.DisplayMode != WebPartManager.CatalogDisplayMode){ %>
            <asp:LinkButton ID="LinkButtonCatalog" OnClick="LinkButtonCatalog_OnClick" runat="server" CssClass="edit action">Add Web Parts</asp:LinkButton> 
	    <% } %>
	</div>
	
	
	
	<table border="0" cellpadding="0" cellspacing="10" width="100%" >
	    <tr>
	        <td valign="top">
	            <asp:CatalogZone runat="server" ID="CatalogZone1" >
	                <ZoneTemplate>
	                    <xs:RegisteredCatalogZone ID="RegisteredCatalogZone1" runat="server" Title="Available Web Parts" />
	                    <asp:PageCatalogPart ID="PageCatalogPart1" runat="server" Title="Closed Web Parts" />
	                </ZoneTemplate>
	            </asp:CatalogZone>
            	
	            <asp:EditorZone runat="server" ID="EditorZone" >
	                <ZoneTemplate>
	                    <asp:AppearanceEditorPart ID="AppearanceEditorPart1" runat="server" />
	                    <asp:BehaviorEditorPart ID="BehaviorEditorPart1" runat="server" />
	                    <asp:LayoutEditorPart ID="LayoutEditorPart1" runat="server" />
	                    <asp:PropertyGridEditorPart ID="PropertyGridEditorPart1" runat="server" />
	                </ZoneTemplate>
	            </asp:EditorZone>
	        </td>
	        <td width="50%" valign="top">
	            <asp:WebPartZone ID="WebPartZoneLeft" runat="server" >
    	            
	            </asp:WebPartZone>
	        </td>
	        <td width="50%" valign="top">
	            <asp:WebPartZone ID="WebPartZoneRight" runat="server">
    	             
	            </asp:WebPartZone>
	        </td>
	    </tr>
	</table>
	
	
	
</asp:Content>
