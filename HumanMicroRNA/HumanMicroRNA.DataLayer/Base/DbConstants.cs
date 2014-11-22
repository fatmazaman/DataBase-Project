using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanMicroRNA.DataLayer.Base
{
    public class DbConstants
    {
        public const string BCPCommand = "exec xp_cmdShell 'bcp.exe'";

        public const string DataBase = "HumanMicroRNA";

        public const string SchemaRNA = "rna";

        public const string SchemaSNP = "snp";

        public const string SchemaVar = "dbvar";

        public const string Schemdbo = "dbo";

        public const string mirnaTable = "mirna";

        public const string hmrnaVarAssnTable = "hmrna_var_assn";

        public const string PasswordResetTable = "password_reset_request";
    }
}
