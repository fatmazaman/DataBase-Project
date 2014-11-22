using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HumanMicroRNA.DataLayer.SNP;
using System.Data;

namespace HumanMicroRNA.BusinessLayer.SNP
{
    /// <summary>
    /// The following class is designed in order to handle all
    /// of the business layer operations.
    /// </summary>
    public class BusSNP
    {
        private DbSNP _snp;

        /// <constructor>
        /// Constructor BusMicroRNA
        /// </constructor>
        public BusSNP()
        {
            _snp  = new DbSNP();
        }

        /// <summary>
        /// The following method is designed in order to import the data from the snp file
        /// downloaded from the ftp server to the snp table in the HumanMircoRNA database.
        /// </summary>
        /// <param name="filePath">DataTable miRNADt</param>
        /// <param name="tableName">string tableName</param>
        /// <param name="batchSize">bool batchSize</param>
        /// <param name="columnMapping">bool columnMapping</param>
        /// <returns>Returns true for successful bulk copy; otherwise false.</returns>
        public static bool ImportSNP(DataTable snpDt, string tableName, bool batchSize, bool columnMapping)
        {
            return (DbSNP.ImportSNP(snpDt, tableName, batchSize, columnMapping));
        }
    }
}
