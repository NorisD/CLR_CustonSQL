using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString SqlFormatStr(SqlString input)
    {
        if (input.IsNull) { 
            return SqlString.Null;
        }
        if (input.Value.Equals("HOLA"))
        {
            return  new SqlString("BIEN TODO.."); 
        }

        StringBuilder builder = new StringBuilder();
        var sDec = "";
        var sInt = "";
        StringBuilder Inversa = new StringBuilder();
        StringBuilder Compone = new StringBuilder();
        int i;
        try
        {
            string[] catena = input.Value.Split('.');

            if (input.Value.Contains("."))
                sDec = catena[1];

            

            /////-----
            var x = catena[0].Trim().Length;
            sInt = catena[0].Trim();
            

            for (i = 0; i < x; i++)
            {
                Inversa.Insert(0, sInt[i]);
            }


            // ---
            for (i = 0; i < x; i++)
            {
                Compone.Insert(0, Inversa[i]);
                if ((i == 2 || i == 5 || i == 8 || i == 11 || i == 14 || i == 17) && i+1 < x)
                    Compone.Insert(0,'.');
            }
            builder.Append(Compone);
            builder.Append(',' + sDec);
            

            return builder.ToString();
        }
        catch (Exception ex) { return new SqlString("Errore "+ex.Message.ToString()); }
    }
    
}
