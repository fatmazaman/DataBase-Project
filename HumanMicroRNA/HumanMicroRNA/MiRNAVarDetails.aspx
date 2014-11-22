<%@ Page Title="Human MicroRNA-Var Details" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="MiRNAVarDetails.aspx.cs" Inherits="HumanMicroRNA.MiRNAVarDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table width="90%" cellpadding="1" cellspacing="0" align="center">
        <tr>
            <td colspan="2">
                <fieldset runat="server">
                    <legend runat="server">Stem-loop Sequence<asp:Label ID="lblMiRNAIDsl" runat="server" /></legend>
                    <table cellpadding="0" cellspacing="5" width="100%">
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblAccession" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:HyperLink ID="hlAccession" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblMiRNAID" runat="server" Text="miRNA ID" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:Label ID="lblMiRNAIDValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="10%" align="left">
                                <asp:Label ID="lblMiRNADesc" runat="server" Text="Description" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:Label ID="lblMiRNADescValue" runat="server" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td width="10%" align="left">
                                <asp:Label ID="lblSequence" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:TextBox ID="tbSequenceValue" runat="server" ReadOnly="true" BorderStyle="Solid"
                                    BorderColor="Gray" BorderWidth="1px" Style="overflow: hidden; white-space: pre-wrap"
                                    ForeColor="#696969" TextMode="MultiLine" Wrap="true" Width="98%" Height="50px"
                                    Font-Names="Helvetica Neue , Lucida Grande, Segoe UI, Arial, Helvetica, Verdana, sans-serif" />
                            </td>
                        </tr>
                        <tr valign="top">
                            <td width="10%" align="left">
                                <asp:Label ID="lblComment" runat="server" Text="Comment" Font-Bold="true" />
                            </td>
                            <td width="90%" align="left">
                                <asp:TextBox ID="tbCommentValue" runat="server" ReadOnly="true" BorderStyle="Solid"
                                    BorderColor="Gray" BorderWidth="1px" Style="overflow: hidden;" ForeColor="#696969"
                                    TextMode="MultiLine" Wrap="true" Width="98%" Height="50px" Font-Names="Helvetica Neue , Lucida Grande, Segoe UI, Arial, Helvetica, Verdana, sans-serif" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td width="50%">
                <fieldset id="Fieldset1" runat="server">
                    <legend id="Legend1" runat="server">Mature Sequence<asp:Label ID="lblMiRNA5p" runat="server" /></legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblAccession5p" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblAccession5pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblPreviousID5p" runat="server" Text="Previous ID" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblPreviousID5pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblSequence5pStart" runat="server" Text="Sequence Start" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence5pStartValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblSequence5pEnd" runat="server" Text="Sequence End" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence5pEndValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblSequence5p" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence5pValue" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
            <td width="50%">
                <fieldset id="Fieldset2" runat="server">
                    <legend id="Legend2" runat="server">Mature Sequence<asp:Label ID="lblMiRNA3p" runat="server" /></legend>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblAccession3p" runat="server" Text="Accession" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblAccession3pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblPreviousID3p" runat="server" Text="Previous ID" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblPreviousID3pValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblSequence3pStart" runat="server" Text="Sequence Start" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence3pStartValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="40%" align="left">
                                <asp:Label ID="lblSequence3pEnd" runat="server" Text="Sequence End" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence3pEndValue" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td width="32%" align="left">
                                <asp:Label ID="lblSequence3p" runat="server" Text="Sequence" Font-Bold="true" />
                            </td>
                            <td width="68%" align="left">
                                <asp:Label ID="lblSequence3pValue" runat="server" />
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left">
                <fieldset id="Fieldset3" runat="server">
                    <legend id="Legend3" runat="server">Variants</legend>
                    <asp:ListView ID="lvVariants" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="lvFirstTD">
                                    <asp:HyperLink ID="hlVariant" runat="server" Target="_blank" NavigateUrl='<%# Eval("var_id")%>'
                                        Text='<%# Eval("var_id_non_link")%>' />
                                </td>
                                <td align="center">
                                    <%#Eval("chromosome_num")%>
                                </td>
                                <td align="center">
                                    <%# Eval("chromosome_range_from_num")%>
                                </td>
                                <td align="center">
                                    <%#Eval("chromosome_range_to_num")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #D1DFF1;">
                                <td class="lvFirstTD">
                                    <asp:HyperLink ID="hlVariant" runat="server" Target="_blank" NavigateUrl='<%# Eval("var_id")%>'
                                        Text='<%# Eval("var_id_non_link")%>' />
                                </td>
                                <td align="center">
                                    <%#Eval("chromosome_num")%>
                                </td>
                                <td align="center">
                                    <%# Eval("chromosome_range_from_num")%>
                                </td>
                                <td align="center">
                                    <%#Eval("chromosome_range_to_num")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <LayoutTemplate>
                            <table cellpadding="0" cellspacing="0" align="center" class="lvTable">
                                <tr style="background-color: #E5E5FE">
                                    <th class="lvHeader">
                                        <asp:Label ID="lblMiRNAID" runat="server" Text="Variant ID" Font-Bold="true" />
                                    </th>
                                    <th class="lvHeader">
                                        <asp:Label ID="lblAccession" runat="server" Text="Chromosome" Font-Bold="true" />
                                    </th>
                                    <th class="lvHeader">
                                        <asp:Label ID="lblChromosome" runat="server" Text="Range From" Font-Bold="true" />
                                    </th>
                                    <th class="lvHeader">
                                        <asp:Label ID="lblStart" runat="server" Text="Range To" Font-Bold="true" />
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server">
                                </tr>
                                <tr>
                                    <td align="center" colspan="4">
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:ListView>
                </fieldset>
                <asp:BulletedList ID="blVardIDs" runat="server" DisplayMode="HyperLink">
                </asp:BulletedList>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <fieldset id="Fieldset4" runat="server">
                    <legend id="Legend4" runat="server">Prediction of microRNA Targets
                        <asp:Label runat="server" ID="lblGeneMiRFamilyID" /></legend>
                    <asp:UpdatePanel ID="upGeneInfo" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lbDownloadGeneInfo" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnlGeneDownload" runat="server" Visible="false">
                                To download the "Prediction of MicroRNA Targets" records below as a comma seperated
                                value file (.csv format), click on the following&nbsp;&nbsp;<asp:LinkButton runat="server"
                                    ID="lbDownloadGeneInfo" Text="link" OnClick="lbDownloadGeneInfo_Clicked" />.<br />
                            </asp:Panel>
                            <asp:ListView ID="lvGeneInfo" runat="server" OnPagePropertiesChanging="lvGeneInfo_PagePropertiesChanging">
                                <ItemTemplate>
                                    <tr>
                                        <%--<td align="center">
                                    <%#Eval("mir_family_id")%>
                                </td>--%>
                                        <td align="center">
                                            <asp:HyperLink ID="hlVariant" runat="server" Target="_blank" NavigateUrl='<%# Eval("gene_id")%>'
                                                Text='<%#Eval("gene_id_non_link")%>' />
                                        </td>
                                        <td align="center">
                                            <%# Eval("gene_symbol_id")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("transcript_id")%>
                                        </td>
                                        <td class="lvFirstTD">
                                            <%# Eval("species_id")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("utr_start_num")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("utr_end_num")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("msa_start_num")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("msa_end_num")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("seed_match_text")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("pct_text")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr style="background-color: #D1DFF1;">
                                        <%--<td align="center">
                                    <%#Eval("mir_family_id")%>
                                </td>--%>
                                        <td align="center">
                                            <asp:HyperLink ID="hlVariant" runat="server" Target="_blank" NavigateUrl='<%# Eval("gene_id")%>'
                                                Text='<%#Eval("gene_id_non_link")%>' />
                                        </td>
                                        <td align="center">
                                            <%# Eval("gene_symbol_id")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("transcript_id")%>
                                        </td>
                                        <td class="lvFirstTD">
                                            <%# Eval("species_id")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("utr_start_num")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("utr_end_num")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("msa_start_num")%>
                                        </td>
                                        <td align="center">
                                            <%# Eval("msa_end_num")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("seed_match_text")%>
                                        </td>
                                        <td align="center">
                                            <%#Eval("pct_text")%>
                                        </td>
                                    </tr>
                                </AlternatingItemTemplate>
                                <EmptyDataTemplate>
                                    <table width="100%" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td align="center">
                                                <b>There exists no data for this microRNA.</b>
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table cellpadding="0" cellspacing="0" align="center" class="lvTable">
                                        <tr style="background-color: #E5E5FE">
                                            <%--<th class="lvHeader">
                                        <asp:Label ID="lblmiRFamilyID" runat="server" Text="miR Family ID" Font-Bold="true" />
                                    </th>--%>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblGeneID" runat="server" Text="Gene ID" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblGeneSymbol" runat="server" Text="Gene Symbol ID" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblTranscriptID" runat="server" Text="Transcript ID" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblSpeciesID" runat="server" Text="Species ID" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblutrStart" runat="server" Text="UTR Start" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblutrEnd" runat="server" Text="UTR End" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblMSAStart" runat="server" Text="MSA Start" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblMSAEnd" runat="server" Text="MSA End" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblSeedMatch" runat="server" Text="Seed Match" Font-Bold="true" />
                                            </th>
                                            <th class="lvHeader">
                                                <asp:Label ID="lblPCT" runat="server" Text="PCT" Font-Bold="true" />
                                            </th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server">
                                        </tr>
                                        <tr style="background: lightgray;">
                                            <td colspan="10">
                                                <asp:DataPager ID="_Grid_Pager" runat="server" PageSize="25" PagedControlID="lvGeneInfo">
                                                    <Fields>
                                                        <asp:TemplatePagerField>
                                                            <PagerTemplate>
                                                                <table style="width: 100%" cellpadding="3" cellspacing="0">
                                                                    <tr>
                                                                        <td style="text-align: left; text-indent: 7px">
                                                                            Rows per page:
                                                                            <asp:DropDownList ID="_ddlPage_Size" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                                                                <asp:ListItem>10</asp:ListItem>
                                                                                <asp:ListItem>25</asp:ListItem>
                                                                                <asp:ListItem>50</asp:ListItem>
                                                                                <asp:ListItem>100</asp:ListItem>
                                                                                <asp:ListItem>500</asp:ListItem>
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
                                </LayoutTemplate>
                            </asp:ListView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </fieldset>
            </td>
        </tr>
    </table>
</asp:Content>
