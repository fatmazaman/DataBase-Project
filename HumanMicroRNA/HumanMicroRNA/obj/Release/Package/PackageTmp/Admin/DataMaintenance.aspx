<%@ Page Title="Admin - Data Maintenance" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="DataMaintenance.aspx.cs" Inherits="HumanMicroRNA.Admin.DataMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Assets/JScript/PopUpScript.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="6000" />
    <asp:UpdateProgress runat="server" ID="upDownloadData">
        <ProgressTemplate>
            <div id="blanket" style="display: none;" onresize="ResizeWindow();">
            </div>
            <div id="popUpDiv" style="display: none;">
                Loading...
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="upnlDownloadData">
        <ContentTemplate>
            <asp:Button ID="btnDownloadData" runat="server" OnClick="btnDownloadData_Click" Text="Download Data"
                OnClientClick="popup('popUpDiv')" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="progressbar"></div>
    <div id="result"></div><br />
    <asp:Button ID="btnUpdateMiRNA" runat="server" Text="Update MIRNA" OnClick="btn_UpdateMIRNA"  />
</asp:Content>
