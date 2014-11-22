using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HumanMicroRNA.BusinessLayer.MicroRNA;
using HumanMicroRNA.BusinessLayer.Var;
using iSharpToolkit.Extensions;
using HumanMicroRNA.BusinessLayer.Base;
using iSharpToolkit.Data.Helper;
using iSharpToolkit.Extensions.Collections;
using iSharpToolkit.Web.UI;

namespace HumanMicroRNA
{
    public partial class MiRNAVarDetails : System.Web.UI.Page
    {
        #region Page Events
        /// <summary>
        /// The following method is designed to be executed at page load.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string miRNAID = Request.QueryString["mID"].Trim();
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(miRNAID))
                {
                    DataTable miRNARecordDt = GetUniqueMiRNAByMiRNAID(miRNAID);
                    if (miRNARecordDt.Rows.Count > 0)
                    {
                        lblMiRNAIDsl.Text = " " + miRNARecordDt.Rows[0]["mirna_id"].ToString();
                        hlAccession.Text = miRNARecordDt.Rows[0]["mirna_acc_text"].ToString();
                        hlAccession.Target = "_blank";
                        hlAccession.NavigateUrl = BusConstants.miRNAURLLink + miRNARecordDt.Rows[0]["mirna_acc_text"].ToString();
                        lblMiRNAIDValue.Text = miRNARecordDt.Rows[0]["mirna_id"].ToString();
                        lblMiRNADescValue.Text = miRNARecordDt.Rows[0]["mirna_desc"].ToString();

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["pre_mature_comment"].ToString()))
                            tbCommentValue.Text = miRNARecordDt.Rows[0]["pre_mature_comment"].ToString();
                        else
                            tbCommentValue.Text = "N/A";
                        tbSequenceValue.Text = miRNARecordDt.Rows[0]["pre_mature_sequence_text"].ToString().
                                    Insert(miRNARecordDt.Rows[0]["pre_mature_sequence_text"].ToString().Length / 2, "\n");


                        lblMiRNA5p.Text = " " + miRNARecordDt.Rows[0]["mature_5p_id"].ToString();
                        lblAccession5pValue.Text = miRNARecordDt.Rows[0]["mature_5p_acc"].ToString();
                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_5p_id"].ToString()))
                            lblPreviousID5pValue.Text = miRNARecordDt.Rows[0]["mature_5p_id"].ToString().Replace("-5p", string.Empty);
                        else
                            lblPreviousID5pValue.Text = "N/A";

                        lblSequence5pValue.Text = miRNARecordDt.Rows[0]["mature_5p_sequence_text"].ToString();
                        lblSequence5pStartValue.Text = miRNARecordDt.Rows[0]["mature_5p_from_num"].ToString();
                        lblSequence5pEndValue.Text = miRNARecordDt.Rows[0]["mature_5p_to_num"].ToString();

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_id"].ToString()))
                            lblMiRNA3p.Text = " " + miRNARecordDt.Rows[0]["mature_3p_id"].ToString();
                        else
                            lblMiRNA3p.Text = "N/A";

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_acc"].ToString()))
                            lblAccession3pValue.Text = miRNARecordDt.Rows[0]["mature_3p_acc"].ToString();
                        else
                            lblAccession3pValue.Text = "N/A";

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_id"].ToString()))
                            lblPreviousID3pValue.Text = miRNARecordDt.Rows[0]["mature_3p_id"].ToString().Replace("-5p", string.Empty);
                        else
                            lblPreviousID3pValue.Text = "N/A";

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_sequence_text"].ToString()))
                            lblSequence3pValue.Text = miRNARecordDt.Rows[0]["mature_3p_sequence_text"].ToString();
                        else
                            lblSequence3pValue.Text = "N/A";

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_from_num"].ToString()))
                            lblSequence3pStartValue.Text = miRNARecordDt.Rows[0]["mature_3p_from_num"].ToString();
                        else
                            lblSequence3pStartValue.Text = "N/A";

                        if (!string.IsNullOrEmpty(miRNARecordDt.Rows[0]["mature_3p_to_num"].ToString()))
                            lblSequence3pEndValue.Text = miRNARecordDt.Rows[0]["mature_3p_to_num"].ToString();
                        else
                            lblSequence3pEndValue.Text = "N/A";

                        DataTable miRNAVarDt = BusVar.GetAllVarsByMiRNAID(miRNARecordDt.Rows[0]["human_micro_rna_id"].ToString().ToInt32());
                        miRNAVarDt.Columns.Add("var_id_non_link", typeof(string));
                        if (miRNAVarDt.Rows.Count > 0)
                        {
                            foreach (DataRow item in miRNAVarDt.Rows)
                            {
                                item["var_id_non_link"] = item["var_id"];
                                if (item["var_id"] != null)
                                    if (item.Field<string>("var_id").StartsWith("esv") || item.Field<string>("var_id").StartsWith("nsv"))
                                        item["var_id"] = BusConstants.VarURLLink + item["var_id"].ToString();
                                    else
                                        item["var_id"] = BusConstants.GetVarLink(item["var_id"].ToString());
                            }
                        }
                        lvVariants.DataSource = miRNAVarDt;
                        lvVariants.DataBind();
                        //EnumerableRowCollection<string> varID = miRNAVarDt.AsEnumerable().Where(a => a.Field<string>("var_id").StartsWith("esv") ||
                        //                                       a.Field<string>("var_id").StartsWith("nsv")).Select(a => a.Field<string>("var_id"));

                        DataTable geneInfoDt = BusMicroRNA.GetAllGeneInfoByMiRNAID(miRNAID);

                        if (geneInfoDt.Rows.Count > 0)
                        {
                            pnlGeneDownload.Visible = true;
                            lvGeneInfo.DataSource = geneInfoDt;

                            lblGeneMiRFamilyID.Text = "for: " + geneInfoDt.Rows[0]["mir_family_id"].ToString();
                        }
                        else
                        {
                            pnlGeneDownload.Visible = false;
                            lvGeneInfo.DataSource = null;
                        }
                        lvGeneInfo.DataBind();
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// The following method is designed in order to get the
        /// information about a specific miRNA based on the id
        /// passed to it.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>DataTable of miRNA record(s).</returns>
        private static DataTable GetUniqueMiRNAByMiRNAID(string miRNAID)
        {
            DataTable dt = new DataTable();
            dt = BusMicroRNA.GetUniqueMiRNAByMiRNAID(miRNAID);
            return dt;
        }
        #endregion

        #region Events
        /// <summary>
        /// The following method is designed in order to download the gene information into
        /// a csv file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void lbDownloadGeneInfo_Clicked(object sender, EventArgs e)
        {
            DataTable geneInfo = new DataTable();
            geneInfo = BusMicroRNA.GetAllGeneInfoByMiRNAID(Request.QueryString["mID"].Trim());

            string toString = geneInfo.ToCSV().ToString();

            CommonHelper.WriteResponseCSV(ref toString, "GeneInfo.csv");
        }
        #endregion

        #region DataPager Events
        /// <summary>
        /// The following event is designed in order to be executed when
        /// the drop down list index is changed.
        /// </summary>
        /// <param name="sender">object sende</param>
        /// <param name="e">EventArgs e</param>
        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataPager dp = lvGeneInfo.FindControl("_Grid_Pager") as DataPager;
            DataTable dt = BusMicroRNA.GetAllGeneInfoByMiRNAID(Request.QueryString["mID"].Trim());
            DropDownList ddl = (DropDownList)sender;
            dp.PageSize = ddl.SelectedValue == "ALL" ? (dt.Rows.Count) : int.Parse(ddl.SelectedValue);
            //lvGeneInfo.DataSource = dt;
            lvGeneInfo.DataBind();
        }
        /// <summary>
        /// The following event is designed in order to be executed at
        /// the PreRender time of the ddtPageSize.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DataPager dp = lvGeneInfo.FindControl("_Grid_Pager") as DataPager;
            DataTable dt = BusMicroRNA.GetAllGeneInfoByMiRNAID(Request.QueryString["mID"].Trim());
            DropDownList ddl = (DropDownList)sender;
            ddl.SelectedValue = dp.PageSize == (dt.Rows.Count) ? "ALL" : dp.PageSize.ToString();
        }

        protected void lvGeneInfo_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager dp = lvGeneInfo.FindControl("_Grid_Pager") as DataPager;
            //set current page startindex, max rows and rebind to false
            dp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);

            //rebind List View
            DataTable dt = BusMicroRNA.GetAllGeneInfoByMiRNAID(Request.QueryString["mID"].Trim());

            //DropDownList ddl = lvGeneInfo.FindControl("_ddlPage_Size") as DropDownList;
            //ddl.SelectedValue = dp.PageSize == (dt.Rows.Count) ? "ALL" : dp.PageSize.ToString();

            lvGeneInfo.DataSource = dt;
            lvGeneInfo.DataBind();
        }
        #endregion
    }
}