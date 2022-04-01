using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;
using System.Text;
using Microsoft.SqlServer.Server;

[Serializable]
[SqlUserDefinedAggregate(Format.UserDefined, 
    IsInvariantToNulls = true, // use clr serializzation to serialize the intermediate rsult
    IsInvariantToDuplicates = false, // optimizer propierty
    IsInvariantToOrder = false,
    MaxByteSize = 800)
]
public class SqlConcatItemColor : IBinarySerialize
{
    public void Init()
    {
        // Inserire qui il codice
        this.auxConcatResult = new StringBuilder();
    }

    public void Accumulate(SqlString Value)
    {
        // Inserire qui il codice
        if (Value.IsNull)
            return;
        if ((this.auxConcatResult.ToString()).Contains(Value.ToString()))
            return;
        
        _ = this.auxConcatResult.Append(Value.Value).Append(',');

    }

    public void Merge (SqlConcatItemColor Group)
    {
        // Inserire qui il codice
        this.auxConcatResult.Append(Group.auxConcatResult);
    }

    public SqlString Terminate ()
    {
        // Inserire qui il codice
        string output = string.Empty;
        if (this.auxConcatResult != null && this.auxConcatResult.Length >0)
            output = this.auxConcatResult.ToString(0, this.auxConcatResult.Length - 1);
        return new SqlString (output);
    }

    public void Read(BinaryReader r)
    {
        //throw new NotImplementedException();
        auxConcatResult = new StringBuilder(r.ReadString());
    }

    public void Write(BinaryWriter w)
    {
        //throw new NotImplementedException();
        w.Write(this.auxConcatResult.ToString());
    }

    // Campo membro segnaposto
    private StringBuilder auxConcatResult;
}
