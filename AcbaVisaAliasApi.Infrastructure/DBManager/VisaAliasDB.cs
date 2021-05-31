using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace AcbaVisaAliasApi.Infrastructure.DBManager
{
    public class VisaAliasDB : IVisaAliasDB
    {
        private readonly string AccOperBase;

        public VisaAliasDB(IConfiguration configuration)
        {
            AccOperBase = configuration.GetConnectionString("AccOperBase");
        }

        public async Task<List<VisaAliasActionHistoryResponse>> GetVisaAliasHistory(ulong customerNumber)
        {
            List<VisaAliasActionHistoryResponse> result = new();

            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "select a.*, b.ActionDescriptionArm from  tbl_visa_alias_action_history a " +
                            "inner join tbl_type_of_visa_alias_action b on a.ActionType = b.ActionType WHERE CustomerNumber = @CustomerNumber";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.Text,
            };

            cmd.Parameters.Add("@CustomerNumber", SqlDbType.BigInt).Value = customerNumber;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                result.Add(new VisaAliasActionHistoryResponse
                {
                    SetNumber = Convert.ToInt32(dr["SetNumber"].ToString()),
                    Guid = dr["Guid"].ToString(),
                    CustomerNumber = Convert.ToUInt64(dr["CustomerNumber"].ToString()),
                    CardNumber = dr["CardNumber"].ToString(),
                    CardType = dr["CardType"].ToString(),
                    Alias = dr["Alias"].ToString(),
                    ActionDate = Convert.ToDateTime(dr["ActionDate"].ToString()),
                    ActionType = dr["ActionDescriptionArm"].ToString()
                });
            }

            return result;
        }

        public async Task InsertVisaAliasactionHisoty<T>(T request, short actionType)
        {

            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "pr_Insert_Visa_Alias_Action_History";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.StoredProcedure,
            };

            CollectParameters(request, actionType, cmd);

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

        }

        private static void CollectParameters<T>(T request, short actionType, SqlCommand cmd)
        {
            cmd.Parameters.Add("@actionType", SqlDbType.SmallInt).Value = actionType;

            switch (typeof(T))
            {
                case
                    var actionRequesType when actionRequesType == typeof(CreateAliasRequest):
                    {
                        CreateAliasRequest actionRequest = (CreateAliasRequest)(object)request;

                        cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = actionRequest.SetNumber;
                        cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = actionRequest.Guid;
                        cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = actionRequest.RecipientPrimaryAccountNumber;
                        cmd.Parameters.Add("@cardType", SqlDbType.NVarChar).Value = actionRequest.CardType;
                        cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                        cmd.Parameters.Add("@Country", SqlDbType.NVarChar).Value = actionRequest.Country;
                        cmd.Parameters.Add("@RecipientFirstName", SqlDbType.NVarChar).Value = actionRequest.RecipientFirstName;
                        cmd.Parameters.Add("@recipientLastName", SqlDbType.NVarChar).Value = actionRequest.RecipientLastName;
                        cmd.Parameters.Add("@IssuerName", SqlDbType.NVarChar).Value = actionRequest.IssuerName;
                        cmd.Parameters.Add("@ConsentDateTime", SqlDbType.NVarChar).Value = actionRequest.ConsentDateTime;
                        cmd.Parameters.Add("@AliasType", SqlDbType.NVarChar).Value = actionRequest.AliasType;
                    }
                    break;
                case
                   var actionRequesType when actionRequesType == typeof(UpdateAliasRequest):
                    {
                        UpdateAliasRequest actionRequest = (UpdateAliasRequest)(object)request;

                        cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = actionRequest.SetNumber;
                        cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = actionRequest.Guid;
                        cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = actionRequest.RecipientPrimaryAccountNumber;
                        cmd.Parameters.Add("@cardType", SqlDbType.NVarChar).Value = actionRequest.CardType;
                        cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                        cmd.Parameters.Add("@IssuerName", SqlDbType.NVarChar).Value = actionRequest.IssuerName;
                        cmd.Parameters.Add("@ConsentDateTime", SqlDbType.NVarChar).Value = actionRequest.ConsentDateTime;
                        cmd.Parameters.Add("@AliasType", SqlDbType.NVarChar).Value = actionRequest.AliasType;
                        cmd.Parameters.Add("@newGuid", SqlDbType.NVarChar).Value = actionRequest.NewGuid;
                    }
                    break;
                case
                   var actionRequesType when actionRequesType == typeof(DeleteAliasRequest):
                    {
                        DeleteAliasRequest actionRequest = (DeleteAliasRequest)(object)request;

                        cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = actionRequest.SetNumber;
                        cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = actionRequest.Guid;
                        cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                    }
                    break;
                default:
                    {
                        break;
                    }
            }
        }
    }
}
