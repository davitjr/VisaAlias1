using AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias;
using AcbaVisaAliasApi.Application.DTOs.VisaAlias;
using AcbaVisaAliasApi.Infrastructure.ServiceDTOs.AcbaVisaAlias;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Action = AcbaVisaAliasApi.Application.DTOs.AcbaVisaAlias.Action;

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

            await CollectParameters(request, actionType, cmd);

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

        }

        private async Task CollectParameters<T>(T request, short actionType, SqlCommand cmd)
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
                        cmd.Parameters.Add("@ExpireDate", SqlDbType.SmallDateTime).Value = actionRequest.ExpiryDate ;
                    }

                    break;
                case
                   var actionRequesType when actionRequesType == typeof(UpdateAliasRequest):
                    {
                        UpdateAliasRequest actionRequest = (UpdateAliasRequest)(object)request;

                        cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = actionRequest.Guid;
                        cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = actionRequest.SetNumber;
                        cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = actionRequest.RecipientPrimaryAccountNumber;
                        cmd.Parameters.Add("@cardType", SqlDbType.NVarChar).Value = actionRequest.CardType;
                        cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                        cmd.Parameters.Add("@ConsentDateTime", SqlDbType.NVarChar).Value = actionRequest.ConsentDateTime;
                        cmd.Parameters.Add("@ExpireDate", SqlDbType.SmallDateTime).Value = actionRequest.ExpiryDate;
                    }

                    break;
                case
    var actionRequesType when actionRequesType == typeof(VisaAliasChangeRequest):
                    {
                        VisaAliasChangeRequest actionRequest = (VisaAliasChangeRequest)(object)request;
                        CreateVisaAliasRequest aliasRequest = await GetVisaAlias(actionRequest.CustomerNumber);
                        switch (actionRequest.Action)
                        {
                            case Action.Update:
                                {
                                    cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = aliasRequest.Guid;
                                    cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = 88;
                                    cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = aliasRequest.RecipientPrimaryAccountNumber;
                                    cmd.Parameters.Add("@cardType", SqlDbType.NVarChar).Value = aliasRequest.CardType;
                                    cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                                    cmd.Parameters.Add("@ConsentDateTime", SqlDbType.NVarChar).Value = aliasRequest.ConsentDateTime;
                                }

                                break;
                            case Action.Delete:
                                {
                                    cmd.Parameters.Add("@setNumber", SqlDbType.Int).Value = 88;
                                    cmd.Parameters.Add("@guid", SqlDbType.NVarChar).Value = aliasRequest.Guid;
                                    cmd.Parameters.Add("@alias", SqlDbType.NVarChar).Value = actionRequest.Alias;
                                }

                                break;
                            default:
                                break;
                        }
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

        public async Task<DeleteVisaAliasRequest> GetVisaAliasForDeleteWithCard(DeleteVisaAliasWithCardRequest deleteVisaAliasWithCardRequest)
        {
            DeleteVisaAliasRequest result = new();

            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "SELECT TOP 1 [Guid], Alias FROM tbl_visa_alias WHERE RecipientPrimaryAccountNumber = @cardNumber";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.Text,
            };

            cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = deleteVisaAliasWithCardRequest.Alias;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                result.Guid = dr["Guid"].ToString();
                result.Alias = dr["Alias"].ToString();
            }

            return result;
        }

        public async Task<CreateVisaAliasRequest> GetVisaAlias(ulong customerNumber)
        {
            CreateVisaAliasRequest result = new();

            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "SELECT TOP 1 * FROM tbl_visa_alias WHERE CustomerNumber = @customerNumber";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.Text,
            };

            cmd.Parameters.Add("@customerNumber", SqlDbType.BigInt).Value = customerNumber;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            while (await dr.ReadAsync())
            {
                result.RecipientPrimaryAccountNumber = dr["RecipientPrimaryAccountNumber"].ToString();
                result.CardType = dr["CardType"].ToString();
                result.Guid = dr["Guid"].ToString();
                result.Alias = dr["Alias"].ToString();
                result.AliasType = dr["AliasType"].ToString();
                result.IssuerName = dr["IssuerName"].ToString();
            }

            return result;
        }

        public async Task<VisaAliasActionHistoryResponse> GetVisaAliasHistoryWithCardAsync(string cardNumber)
        {
            VisaAliasActionHistoryResponse result = new();

            using SqlConnection dbconn = new(AccOperBase);

            await dbconn.OpenAsync();

            string sqlStr = "SELECT  Left(EmbossingName, CHARINDEX(' ', EmbossingName) - 1) AS FirstName, Right(EmbossingName, LEN(EmbossingName) - CHARINDEX(' ', EmbossingName)) AS LastName,  * FROM" +
                " ((SELECT top 1 cardtype, cardnumber, EmbossingName, RIGHT(ExpiryDate, 4) + '-' + LEFT(ExpiryDate, 2) AS ExpiryDate  FROM tbl_visa_applications WHERE cardnumber = @cardNumber  ORDER BY givenDate DESC) va" +
                " LEFT JOIN(SELECT TOP 1 * FROM tbl_visa_alias WHERE RecipientPrimaryAccountNumber = @cardNumber ORDER BY id DESC) alias" +
                " ON alias.RecipientPrimaryAccountNumber = va.cardnumber INNER JOIN tbl_type_of_card tc on tc.id = va.cardtype ) ";

            await using SqlCommand cmd = new(sqlStr, dbconn)
            {
                CommandType = CommandType.Text,
            };

            cmd.Parameters.Add("@cardNumber", SqlDbType.NVarChar).Value = cardNumber;

            using SqlDataReader dr = await cmd.ExecuteReaderAsync();

            if (await dr.ReadAsync())
            {
                result.CardNumber = dr["RecipientPrimaryAccountNumber"].ToString();
                result.CardType = dr["ApplicationsCardType"].ToString();
                result.Alias = dr["Alias"].ToString();
                result.ActionDate = !string.IsNullOrEmpty(dr["ConsentDateTime"].ToString()) ? Convert.ToDateTime(dr["ConsentDateTime"].ToString()) : (DateTime?)null;
                result.RecipientFirstName = dr["FirstName"].ToString();
                result.RecipientLastName = dr["LastName"].ToString();
                if (!string.IsNullOrEmpty(dr["RecipientPrimaryAccountNumber"].ToString()))
                {
                    result.Status = "Գործող";
                }

                result.OperDay = DateTime.Now;
                result.ExpiryDate = dr["ExpiryDate"].ToString();
                result.Guid = dr["Guid"].ToString();
            }

            return result;
        }
    }
}
