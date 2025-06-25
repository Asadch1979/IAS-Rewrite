using System;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using AIS.Models;

namespace AIS.Services
{
    /// <summary>
    /// Simple wrapper around PKG_HR stored procedures.
    /// Only minimal methods are implemented for demo purposes.
    /// </summary>
    public class HrService
    {
        private readonly IConfiguration _configuration;

        public HrService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private OracleConnection CreateConnection()
        {
            var builder = new OracleConnectionStringBuilder
            {
                UserID = _configuration["ConnectionStrings:DBUserName"],
                Password = _configuration["ConnectionStrings:DBUserPassword"],
                DataSource = _configuration["ConnectionStrings:DBDataSource"],
                Pooling = true
            };
            return new OracleConnection(builder.ConnectionString);
        }

        public void AddStaffPosition(StaffPosition model, int createdBy)
        {
            using var con = CreateConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PKG_HR.ADD_STAFF_POSITION";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_PPNO", OracleDbType.Varchar2).Value = model.PPNO;
            cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = model.Name;
            cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2).Value = model.Rank;
            cmd.Parameters.Add("P_DESIGNATION", OracleDbType.Varchar2).Value = model.Designation;
            cmd.Parameters.Add("P_PLACEMENT", OracleDbType.Varchar2).Value = model.Placement;
            cmd.Parameters.Add("P_HIGHEST_QUAL", OracleDbType.Varchar2).Value = model.HighestQualification;
            cmd.Parameters.Add("P_SPECIALIZATION", OracleDbType.Varchar2).Value = model.Specialization;
            cmd.Parameters.Add("P_AUDIT_CERT", OracleDbType.Varchar2).Value = model.AuditCertification;
            cmd.Parameters.Add("P_TOT_EXPERIENCE", OracleDbType.Decimal).Value = model.TotalExperience;
            cmd.Parameters.Add("P_AUDIT_EXPERIENCE", OracleDbType.Decimal).Value = model.AuditExperience;
            cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = model.ZoneId;
            cmd.Parameters.Add("P_COMPANY", OracleDbType.Varchar2).Value = model.Company;
            cmd.Parameters.Add("P_CREATED_BY", OracleDbType.Int32).Value = createdBy;
            cmd.ExecuteNonQuery();
        }

        public void AddManpowerDemand(ManpowerDemand model)
        {
            using var con = CreateConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PKG_HR.ADD_MANPOWER_DEMAND";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_STAFF_POSITION_ID", OracleDbType.Int32).Value = model.Id;
            cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2).Value = model.Rank;
            cmd.Parameters.Add("P_PLACEMENT", OracleDbType.Varchar2).Value = model.Placement;
            cmd.Parameters.Add("P_EXISTING", OracleDbType.Int32).Value = model.Existing;
            cmd.Parameters.Add("P_ADDITIONAL_REQ", OracleDbType.Int32).Value = model.AdditionalRequired;
            cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = model.ZoneId;
            cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = model.Status;
            cmd.Parameters.Add("P_SUBMITTED_BY", OracleDbType.Int32).Value = model.SubmittedBy;
            cmd.ExecuteNonQuery();
        }

        public void UpdateDemandStatus(int id, string status, int updatedBy, string remarks)
        {
            using var con = CreateConnection();
            con.Open();
            using var cmd = con.CreateCommand();
            cmd.CommandText = "PKG_HR.UPDATE_MANPOWER_DEMAND_STATUS";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
            cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = status;
            cmd.Parameters.Add("P_UPDATED_BY", OracleDbType.Int32).Value = updatedBy;
            cmd.Parameters.Add("P_REMARKS", OracleDbType.Varchar2).Value = remarks;
            cmd.ExecuteNonQuery();
        }
    }
}
