<%@ Page Title="Human MicroRNA-Var" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MiRNAVar.aspx.cs" Inherits="HumanMicroRNA.MiRNAVar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .ui-autocomplete
        {
            max-height: 300px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden; /* add padding to account for vertical scrollbar */
            padding-right: 20px;
        }
        * html .ui-autocomplete
        {
            height: 300px;
        }
    </style>
    <link href="Assets/Styles/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="Assets/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="Assets/Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            LoadAutoComplete();
        });
        function LoadAutoComplete() {
            var urlText = '<%=ResolveUrl("~/AppWebService.asmx/GetMiRNAAccession")%>';

            $(".tba").autocomplete({
                source: function (request, response) {
                    $.ajax({

                        type: 'POST',
                        url: urlText,
                        data: "{'Accession': '" + request.term + "'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (msg) {
                            //alert("error");
                        }
                    });
                },
                minLength: 2
            });

            $(".tbm").autocomplete({
                source: function (request, response) {
                    $.ajax({

                        type: 'POST',
                        url: '<%=ResolveUrl("~/AppWebService.asmx/GetMiRNAID")%>',
                        data: "{'miRNAID': '" + request.term + "'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (msg) {
                            //alert("error");
                        }
                    });
                },
                minLength: 2
            });

            $(".tbv").autocomplete({
                source: function (request, response) {
                    $.ajax({

                        type: 'POST',
                        url: '<%=ResolveUrl("~/AppWebService.asmx/GetVarID")%>',
                        data: "{'varID': '" + request.term + "'}",
                        contentType: 'application/json; charset=utf-8',
                        dataType: 'json',
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (msg) {
                            //alert("error");
                        }
                    });
                },
                minLength: 2
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="100%" cellpadding="0" class="miRNAVarTable">
        <tr valign="middle">
            <td>
                <table cellpadding="1px" width="80%" align="right">
                    <tr valign="bottom">
                        <td>
                            <asp:Label ID="lblMiRNA" runat="server" Text="miRNA" Font-Bold="true" /><br />
                            <asp:TextBox ID="tbMiRNA" runat="server" class="tbm" Width="130" />
                        </td>
                        <td>
                            <asp:Label ID="lblAccession" runat="server" Text="Accession" Font-Bold="true" /><br />
                            <asp:TextBox ID="tbAccession" runat="server" class="tba" Width="130" />
                        </td>
                        <td>
                            <asp:Label ID="lblVariant" runat="server" Text="Variant" Font-Bold="true" /><br />
                            <asp:TextBox ID="tbVariant" runat="server" class="tbv" Width="130" />
                        </td>
                        <td>
                            <asp:Label ID="lblChromosomeSearch" runat="server" Text="Chromosome" Font-Bold="true" /><br />
                            <asp:TextBox ID="tbChromosomeSearch" runat="server" class="tb" Width="130" />
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="MiRNAVarSearchButton"
                                OnClick="btnSearch_Click" />
                        </td>
                        <td>
                            <asp:ImageButton runat="server" ID="ibMiRNADownload" ImageUrl="~/Assets/Images/csv_file.png"
                                Width="35px" Height="40px" OnClick="ibMiRNADownload_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr style="background-color: Gray;" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:ListView ID="lvMiRNAReport" runat="server" DataSourceID="odsMiRNAReport">
                    <ItemTemplate>
                        <tr>
                            <td align="center">
                                <asp:HyperLink ID="hlMirnaID" runat="server" Text="View" Target="_blank" NavigateUrl='<%#"MiRNAVarDetails.aspx?mID=" + Eval("mirna_id") %>' />
                            </td>
                            <td class="lvFirstTD">
                                <%#Eval("mirna_id")%>
                            </td>
                            <td align="center">
                                <%#Eval("mirna_acc_text")%>
                            </td>
                            <td align="center">
                                <%# Eval("xsome_num") %>
                            </td>
                            <td align="center">
                                <%#Eval("range_from_num")%>
                            </td>
                            <td align="center">
                                <%#Eval("range_to_num")%>
                            </td>
                            <td align="center">
                                <%#Eval("strand_text")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr style="background-color: #D1DFF1;">
                            <td align="center">
                                <asp:HyperLink ID="hlMirnaID" runat="server" Text="View" Target="_blank" NavigateUrl='<%#"MiRNAVarDetails.aspx?mID=" + Eval("mirna_id") %>' />
                            </td>
                            <td class="lvFirstTD">
                                <%#Eval("mirna_id")%>
                            </td>
                            <td align="center">
                                <%#Eval("mirna_acc_text")%>
                            </td>
                            <td align="center">
                                <%# Eval("xsome_num") %>
                            </td>
                            <td align="center">
                                <%#Eval("range_from_num")%>
                            </td>
                            <td align="center">
                                <%#Eval("range_to_num")%>
                            </td>
                            <td align="center">
                                <%#Eval("strand_text")%>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <EmptyDataTemplate>
                        <table width="100%" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="center">
                                    <b>There exists no data for this search.</b>
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <table cellpadding="0" cellspacing="0" align="center" class="lvTable">
                            <tr style="background-color: #E5E5FE">
                                <th class="lvHeader">
                                    <asp:Label ID="lblView" runat="server" Text="View" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblMiRNAID" runat="server" Text="miRNA ID" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblAccession" runat="server" Text="Accession" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblChromosome" runat="server" Text="Chromosome" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblStart" runat="server" Text="Start" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblEnd" runat="server" Text="End" Font-Bold="true" />
                                </th>
                                <th class="lvHeader">
                                    <asp:Label ID="lblStrand" runat="server" Text="Strand" Font-Bold="true" />
                                </th>
                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                            <tr style="background-color: lightgray;">
                                <td align="center" colspan="7">
                                    <table border="0" cellpadding="3" width="100%" align="center">
                                        <tr>
                                            <td>
                                                <asp:DataPager ID="_Grid_Pager" runat="server" PageSize="25" PagedControlID="lvMiRNAReport">
                                                    <Fields>
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <table style="width: 100%" cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td style="text-align: left; text-indent: 7px">
                                                                            Rows per page:
                                                                            <asp:DropDownList ID="_ddlPage_Size" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged"
                                                                                OnPreRender="ddlPageSize_PreRender">
                                                                                <asp:ListItem>10</asp:ListItem>
                                                                                <asp:ListItem>25</asp:ListItem>
                                                                                <asp:ListItem>50</asp:ListItem>
                                                                                <asp:ListItem>100</asp:ListItem>
                                                                                <asp:ListItem>500</asp:ListItem>
                                                                                <asp:ListItem Value="ALL">ALL</asp:ListItem>
                                                                            </asp:DropDownList>
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                </td>
                                                                <td style="text-align: Right;">
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                        <asp:NextPreviousPagerField ButtonCssClass="PagingButton" FirstPageText=" First "
                                                            PreviousPageText=" Previous " RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="true"
                                                            ShowPreviousPageButton="true" ShowLastPageButton="false" ShowNextPageButton="false" />
                                                        <asp:NumericPagerField ButtonCount="5" NumericButtonCssClass="PagingButton" CurrentPageLabelCssClass="current"
                                                            NextPreviousButtonCssClass="PagingButton" />
                                                        <asp:NextPreviousPagerField ButtonCssClass="PagingButton" LastPageText=" Last " NextPageText=" Next "
                                                            RenderDisabledButtonsAsLabels="true" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                                            ShowLastPageButton="true" ShowNextPageButton="true" />
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                </td> </tr> </table>
                                                            </PagerTemplate>
                                                        </asp:TemplatePagerField>
                                                    </Fields>
                                                </asp:DataPager>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
            </td>
        </tr>
    </table>
    <asp:ObjectDataSource ID="odsMiRNAReport" runat="server" SelectMethod="GetMiRNABySearchCriteria"
        TypeName="HumanMicroRNA.BusinessLayer.MicroRNA.BusMicroRNA">
        <SelectParameters>
            <asp:Parameter Name="miRNAID" Type="String" />
            <asp:Parameter Name="accession" Type="String" />
            <asp:Parameter Name="chromosome" Type="String" />
            <asp:Parameter Name="variant" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
