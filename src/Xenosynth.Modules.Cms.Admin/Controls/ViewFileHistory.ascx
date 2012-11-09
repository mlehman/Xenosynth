<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewFileHistory.ascx.cs" Inherits="Xenosynth.Modules.Cms.Admin.Controls.ViewFileHistory" %>
<%@ Register TagPrefix="erp" Namespace="Fluent.EmptyRepeaterPanel" Assembly="Fluent.EmptyRepeaterPanel" %>
<%@ Register TagPrefix="dga" Namespace="Fluent" Assembly="Fluent.DataGridAdapter" %>

<asp:DataGrid ID="DataGridAuditLog" Runat="Server"
    AutoGenerateColumns="False"
    GridLines="None"
    DataKeyField="ID"
    CssClass="grid"
    Width="100%"
    HeaderStyle-CssClass="gridHeader"
    AlternatingItemStyle-CssClass="altRow"
    PageSize='<%# Xenosynth.XenosynthContext.Current.Configuration["xenosynth.preferences.paging.pageSize"].Value %>'
    AllowPaging="True"
    >
    <Columns>
        <asp:TemplateColumn  HeaderStyle-CssClass="modifiedColumn" ItemStyle-CssClass="modifiedColumn">
            <HeaderTemplate><asp:LinkButton ID="LinkButton1" Runat="Server" CommandName="Sort" CommandArgument="EventDate">Event Date
		            <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/App_Themes/" + Page.Theme + "/images/icon_" + DataGridAdapterAuditLog.GetSortOrder("EventDate") + ".gif" %>' /></asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "EventDate", "{0:g}")%></ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate><asp:LinkButton ID="LinkButton2" Runat="Server" CommandName="Sort" CommandArgument="EventName">Event
		            <asp:Image ID="Image2" runat="server" ImageUrl='<%# "~/App_Themes/" + Page.Theme + "/images/icon_" + DataGridAdapterAuditLog.GetSortOrder("EventName") + ".gif" %>' /></asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "EventName")%></ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate><asp:LinkButton ID="LinkButton3" Runat="Server" CommandName="Sort" CommandArgument="Username">Username
		            <asp:Image ID="Image3" runat="server" ImageUrl='<%# "~/App_Themes/" + Page.Theme + "/images/icon_" + DataGridAdapterAuditLog.GetSortOrder("Username") + ".gif" %>' /></asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Username")%></ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate><asp:LinkButton ID="LinkButton4" Runat="Server" CommandName="Sort" CommandArgument="IP">IP
		            <asp:Image ID="Image4" runat="server" ImageUrl='<%# "~/App_Themes/" + Page.Theme + "/images/icon_" + DataGridAdapterAuditLog.GetSortOrder("IP") + ".gif" %>' /></asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "IP")%></ItemTemplate>
        </asp:TemplateColumn>
        <asp:TemplateColumn>
            <HeaderTemplate><asp:LinkButton ID="LinkButton5" Runat="Server" CommandName="Sort" CommandArgument="Detail">Detail
		            <asp:Image ID="Image5" runat="server" ImageUrl='<%# "~/App_Themes/" + Page.Theme + "/images/icon_" + DataGridAdapterAuditLog.GetSortOrder("Detail") + ".gif" %>' /></asp:LinkButton>
            </HeaderTemplate>
            <ItemTemplate><%# DataBinder.Eval(Container.DataItem, "Detail")%></ItemTemplate>
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
<erp:EmptyRepeaterPanel runat="server" control="DataGridAuditLog" ID="Emptyrepeaterpanel2" NAME="Emptyrepeaterpanel1">
    <span class="warning">This file has no history.</span>
</erp:EmptyRepeaterPanel>
<dga:DataGridAdapter 
    ID="DataGridAdapterAuditLog" 
    Runat="Server" 
    DataGridToBind="DataGridAuditLog" 
    SortHistory="1" 
    AutoDataBind="True"
    HideEmptyPager="True"
    OnDataGridBinding="DataGridAdapterAuditLog_DataGridBinding"
    />