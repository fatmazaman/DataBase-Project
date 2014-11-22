using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using HumanMicroRNA.BusinessLayer.MicroRNA;
using System.Data;
using System.Web.Script.Services;

namespace HumanMicroRNA
{
    /// <summary>
    /// Summary description for AppWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AppWebService : System.Web.Services.WebService
    {
        /// <summary>
        /// The following WebMethod is designed in order to
        /// return the Accession IDs based on the parameter passed to it.
        /// </summary>
        /// <param name="Accession">string Accession</param>
        /// <returns>List of Accessions</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetMiRNAAccession(string Accession)
        {
            List<string> miRNAAccession = BusMicroRNA.GetMiRNAAccession(Accession);

            return miRNAAccession;
        }
        /// <summary>
        /// The following WebMethod is designed in order to
        /// return the MiRNA IDs based on the parameter passed to it.
        /// </summary>
        /// <param name="miRNAID">string miRNAID</param>
        /// <returns>List of MiRNAs</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetMiRNAID(string miRNAID)
        {
            List<string> miRNAIDList = BusMicroRNA.GetMiRNAID(miRNAID);

            return miRNAIDList;
        }
        /// <summary>
        /// The following WebMethod is designed in order to
        /// return the Var IDs based on the parameter passed to it.
        /// </summary>
        /// <param name="varID">string varID</param>
        /// <returns>List of Variant IDs</returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public List<string> GetVarID(string varID)
        {
            List<string> varIDList = BusMicroRNA.GeVarID(varID);

            return varIDList;
        }
    }
}
