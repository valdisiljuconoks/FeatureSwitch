<%@ Page Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="Index.aspx.cs" Inherits="FeatureSwitch.EPiServer.modules._protected.FeatureSwitch.EPiServer.Index" %>
<%@ Import Namespace="FeatureSwitch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="FullRegion" runat="server">
    <div class="epi-contentContainer epi-padding-large">
        <div class="epi-contentArea epi-paddingHorizontal">
            <h1 class="EP-prefix">FeatureSwitch Control Panel</h1>
        </div>
        <table class="epi-default" cellspacing="0" style="border-collapse: collapse; border-style: None;">
            <tr>
                <th class="epitableheading" scope="col">Enabled</th>
                <th class="epitableheading" scope="col">Name</th>
                <th class="epitableheading" scope="col">Full Type Name</th>
            </tr>

            <asp:Repeater runat="server" ID="rptFeatures" OnItemDataBound="OnFeaturesRepeaterItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:CheckBox runat="server" ID="chkEnabled" AutoPostBack="True" OnCheckedChanged="chkEnabled_OnCheckedChanged"/>
                        </td>
                        <td><%# Eval("Name") %></td>
                        <td><%# ((BaseFeature) Container.DataItem).GetType().FullName %></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
</asp:Content>