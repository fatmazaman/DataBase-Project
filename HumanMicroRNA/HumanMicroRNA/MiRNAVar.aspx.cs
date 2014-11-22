using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using HumanMicroRNA.BusinessLayer.MicroRNA;
using System.Collections;
using iSharpToolkit.Extensions.Collections;
using iSharpToolkit.Web.UI;

namespace HumanMicroRNA
{
    public partial class MiRNAVar : System.Web.UI.Page
    {
        #region Page Events
        /// <summary>
        /// The following event is designed in order to be executed at 
        /// Page Load.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// The following event is designed in order to be executed
        /// at Page PreRender time.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                odsMiRNAReport.SelectParameters["miRNAID"].DefaultValue = tbMiRNA.Text.Trim();
                odsMiRNAReport.SelectParameters["accession"].DefaultValue = tbAccession.Text.Trim();
                odsMiRNAReport.SelectParameters["chromosome"].DefaultValue = tbChromosomeSearch.Text.Trim();
                odsMiRNAReport.SelectParameters["variant"].DefaultValue = tbVariant.Text.Trim();

                lvMiRNAReport.DataBind();
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// The following event is designed in order to be executed when the
        /// search button is clicked.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string miRNAID = tbMiRNA.Text.Trim();
            string accession = tbAccession.Text.Trim();
            string chromosome = tbChromosomeSearch.Text.Trim();
            string variant = tbVariant.Text.Trim();

            odsMiRNAReport.SelectParameters["miRNAID"].DefaultValue = tbMiRNA.Text.Trim();
            odsMiRNAReport.SelectParameters["accession"].DefaultValue = tbAccession.Text.Trim();
            odsMiRNAReport.SelectParameters["chromosome"].DefaultValue = tbChromosomeSearch.Text.Trim();
            odsMiRNAReport.SelectParameters["variant"].DefaultValue = tbVariant.Text.Trim();

            lvMiRNAReport.DataBind();
            DataSet ds = GetDS(odsMiRNAReport);
            if (ds.Tables.Count > 0)
                if (ds.Tables[0].Rows.Count > 0)
                    ibMiRNADownload.Visible = true;
                else
                    ibMiRNADownload.Visible = false;
            else
                ibMiRNADownload.Visible = false;
            //BusMicroRNA.GetMiRNABySearchCriteria(miRNAID, accession, chromosome, variant);
        }
        /// <summary>
        /// The followind method is designed in order to download the MiRNA
        /// records into a csv file.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void ibMiRNADownload_Click(object sender, EventArgs e)
        {
            DataSet MiRNARecords = GetDS(odsMiRNAReport);

            string toString = MiRNARecords.Tables[0].ToCSV().ToString();

            CommonHelper.WriteResponseCSV(ref toString, "MiRNA.csv");
        }
        #endregion

        #region Private Methoda
        /// <summary>
        /// The following method is designed in order to return
        /// a dataset out of an object data source.
        /// </summary>
        /// <param name="ods">ObjectDataSource ods</param>
        /// <returns>Data as a dataset.</returns>
        private DataSet GetDS(ObjectDataSource ods)
        {
            var ds = new DataSet();
            var dv = (DataView)ods.Select();
            if (dv != null && dv.Count > 0)
            {
                var dt = dv.ToTable();
                ds.Tables.Add(dt);
            }
            return ds;
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
            DataPager dp = lvMiRNAReport.FindControl("_Grid_Pager") as DataPager;

            string miRNAID = tbMiRNA.Text.Trim();
            string accession = tbAccession.Text.Trim();
            string chromosome = tbChromosomeSearch.Text.Trim();
            string variant = tbVariant.Text.Trim();

            DataTable busMicroRNAGetMiRNABySearchCriteria =
                BusMicroRNA.GetMiRNABySearchCriteria(miRNAID, accession, chromosome, variant);

            DropDownList ddl = (DropDownList)sender;
            dp.PageSize = ddl.SelectedValue == "ALL" ?
                (busMicroRNAGetMiRNABySearchCriteria.Rows.Count) : int.Parse(ddl.SelectedValue);
            lvMiRNAReport.DataBind();
        }
        /// <summary>
        /// The following event is designed in order to be executed at
        /// the PreRender time of the ddtPageSize.
        /// </summary>
        /// <param name="sender">object sender</param>
        /// <param name="e">EventArgs e</param>
        protected void ddlPageSize_PreRender(object sender, EventArgs e)
        {
            DataPager dp = lvMiRNAReport.FindControl("_Grid_Pager") as DataPager;

            string miRNAID = tbMiRNA.Text.Trim();
            string accession = tbAccession.Text.Trim();
            string chromosome = tbChromosomeSearch.Text.Trim();
            string variant = tbVariant.Text.Trim();

            DropDownList ddl = (DropDownList)sender;
            ddl.SelectedValue = dp.PageSize ==
                (BusMicroRNA.GetMiRNABySearchCriteria(miRNAID, accession, chromosome, variant).Rows.Count) ? "ALL" : dp.PageSize.ToString();
        }
        #endregion
    }
}