<%@ Page Title="Admin - Data Maintenance" Language="C#" MasterPageFile="~/Admin/AdminMaster.Master"
    AutoEventWireup="true" CodeBehind="DataMaintenance.aspx.cs" Inherits="HumanMicroRNA.Admin.DataMaintenance" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="Assets/JScript/ModalPopUp.js" type="text/javascript"></script>
    <script src="Assets/JQuery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        var divName;
        /*-------------------------------------------------------------------------
        The functions below are designed for the download Data process.
        Function 'function DownloadData(popup)' is designed to call a webmethod in
        order to download the data from different sources.
        Function 'onDownloadDataComplete(result, userContext, methodName)' is 
        designed to be executed when the DownloadData is finished its execution
        successfully.
        Function 'onDownloadDataError(result, userContext, methodName)' is designed
        to be executed when the DownloadData is finished its execution
        unsuccessfully.
        -------------------------------------------------------------------------*/
        function DownloadData(popup) {
            divName = popup;
            ShowModalPopup(popup);

            PageMethods.DownloadData(onDownloadDataComplete, onDownloadDataError);
        }
        function onDownloadDataComplete(result, userContext, methodName) {
            HideModalPopup(divName);
        }
        function onDownloadDataError(result, userContext, methodName) {

        }
        /*-------------------------------------------------------------------------
        The functions below are designed for the Update Data process.
        Function 'function UpdateMiRNA(popup)' is designed to call a webmethod in
        order to update the data after it has been downloaded.
        Function 'onUpdateDataComplete(result, userContext, methodName)' is 
        designed to be executed when the UpdateMiRNA is finished its execution
        successfully.
        Function 'onUpdateDataError(result, userContext, methodName)' is designed
        to be executed when the UpdateMiRNA is finished its execution
        unsuccessfully.
        -------------------------------------------------------------------------*/
        function UpdateMiRNA(popup) {
            divName = popup;
            ShowModalPopup(popup);

            PageMethods.UpdateMiRNAData(onUpdateDataComplete, onUpdateDataError);
        }
        function onUpdateDataComplete(result, userContext, methodName) {
            HideModalPopup(divName);
        }
        function onUpdateDataError(result, userContext, methodName) {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="dvDownloadPopup" style="display: none; width: 600px; max-height: 200px;
        border: 4px solid #2B547E; background-color: #FFFFFF;">
        <table width="100%" cellpadding="5" cellspacing="0" align="center">
            <tr>
                <td class="table_forgot_pssd_td" colspan="2">
                    <b>Downloading Data Files...</b>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <img src="Assets/Images/downloading.gif" style="height: 87px; width: 145px" />
                </td>
                <td align="justify">
                    The application is in the process of downloading data files from multiple FTP Sources.
                    This might take few minutes.<br />
                    Do not close the browser or navigate away from this page as you will interrupt the
                    download proces. Please be patient while the data is being downloaded.
                </td>
            </tr>
        </table>
    </div>
    <div id="dvUpdatePopup" style="display: none; width: 600px; max-height: 200px; border: 4px solid #2B547E;
        background-color: #FFFFFF;">
        <table width="100%" cellpadding="5" cellspacing="0" align="center">
            <tr>
                <td class="table_forgot_pssd_td">
                    <b>Updating Data Files...</b>
                </td>
            </tr>
            <tr>
                <td align="justify">
                    The application is in the process of updating the data in the database from the
                    newly downloaded files.<br />
                    Do not close the browser or navigate away from this page as you will interrupt the
                    update proces. Please be patient while the data is being updated.
                </td>
            </tr>
        </table>
    </div>
    <table width="100%" cellpadding="5" cellspacing="0" align="center">
        <tr>
            <td>
                The Data Maintenance page is designed in order to download the appropriate files
                required to generate the data for the application.<br />
                <ul>
                    <li>The link below is the FTP source which contains the Variant information for the
                        Homo Sapiens.<br />
                        <a href="ftp://ftp.ncbi.nlm.nih.gov/pub/dbVar/data/Homo_sapiens/by_assembly/NCBI36/gvf/"
                            target="_blank">ftp://ftp.ncbi.nlm.nih.gov/pub/dbVar/data/Homo_sapiens/by_assembly/NCBI36/gvf</a></li>
                    <li>The link below is the FTP source which contains the SNP information for the Homo
                        Sapiens.<br />
                        <a href="ftp://ftp.ncbi.nih.gov/snp/organisms/human_9606/database/organism_data/"
                            target="_blank">ftp://ftp.ncbi.nih.gov/snp/organisms/human_9606/database/organism_data</a>
                    </li>
                    <li>The link below holds the mirBase information for the Homo Sapiens<br />
                        <a href="ftp://mirbase.org/pub/mirbase/CURRENT" target="_blank">ftp://mirbase.org/pub/mirbase/CURRENT</a>
                    </li>
                </ul>
            </td>
        </tr>
        <tr>
            <td align="left">
                <input type="button" id='download_data' value="Download Data" onclick="DownloadData('dvDownloadPopup')"
                    class="download_update_button" />&nbsp;&nbsp;
                <input type="button" id='update_data' value="Update Data" onclick="UpdateMiRNA('dvUpdatePopup')"
                    class="download_update_button" />
            </td>
        </tr>
    </table>
</asp:Content>
