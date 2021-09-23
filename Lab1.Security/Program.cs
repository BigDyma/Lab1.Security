using System;
using System.Text.Json;

namespace Lab1.Security
{
    class Program
    {
        static void Main(string[] args)
        {
            AuditFile audit = new AuditFile(@"C:\Users\dumitru.strelet\source\repos\Lab1.Security\Lab1.Security\Lib\CIS_Cisco_IOS_15_v4.1.0_Level_1.audit");

            var result = audit.ResultJSON;

            var finalresult = JsonSerializer.Serialize(result);

        }
    }
}
