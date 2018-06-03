using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardGameServer.Security
{
    public class sqlInjection
    {
        public static string[] Words = 
        { 
            "--",
            "\"",
             "'",
            ";--",
            ";",
            "/*",
            "*/",
            "@@",
            "@",
            "char",
            "nchar",
            "varchar",
            "nvarchar",
            "alter",
            "begin",
            "cast",
            "create",
            "cursor",
            "declare",
            "delete",
            "drop",
            "end",
            "exec",
            "execute",
            "fetch",
            "insert",
            "kill",
            "select",
            "sys",
            "sysobjects",
            "syscolumns",
            "table",
            "update"
        };
    }
}
