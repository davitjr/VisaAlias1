using AcbaVisaAliasApi.Application;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public static class VisaAliasDB
    {
        public static async Task SaveCorrelationId(string correlation, int visaAliasActionType, string guId)
        {
            string AccOperBaseConn = DBConnections.AppLog;

            string sqlStr = "INSERT INTO tbl_Visa_Alias_Correlation_Id(Correlation_Id, VisaAliasAction, Visa_Alias_GuId)" +
                            " VALUES(@correlation, @VisaAliasAction, @GuId)";

            using SqlConnection dbconn = new(AccOperBaseConn);
            await dbconn.OpenAsync();

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.Text
            };

            cmd.Parameters.Add("@correlation", SqlDbType.NVarChar).Value = correlation;
            cmd.Parameters.Add("@VisaAliasAction", SqlDbType.Int).Value = visaAliasActionType;
            cmd.Parameters.Add("@GuId", SqlDbType.NVarChar).Value = guId;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                throw new Exception(dr["description"].ToString());
            }
        }

    }
}
