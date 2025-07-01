using AIS.Models.IID;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace AIS.Controllers
{
    public List<CAObservationModel> GetObservations(int? divisionId = null, string status = null)
        {
        sessionHandler = new SessionHandler();
        sessionHandler._httpCon = this._httpCon;
        sessionHandler._session = this._session;
        sessionHandler._configuration = this._configuration;

        var con = this.DatabaseConnection();
        con.Open();

        List<CAObservationModel> observations = new List<CAObservationModel>();

        using (OracleCommand cmd = con.CreateCommand())
            {
            cmd.CommandText = "pkg_CA.Get_Observations";
            cmd.CommandType = CommandType.StoredProcedure;

            // Parameters for procedure
            cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = (object)divisionId ?? DBNull.Value;
            cmd.Parameters.Add("p_status", OracleDbType.Varchar2, 50).Value = (object)status ?? DBNull.Value;
            cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                while (rdr.Read())
                    {
                    var model = new CAObservationModel
                        {
                        ObservationId = rdr["OBSERVATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["OBSERVATION_ID"]),
                        OMObservationId = rdr["OM_OBSERVATION_ID"]?.ToString(),
                        AIROMNo = rdr["AIR_OM_NO"]?.ToString(),
                        PDPParaNo = rdr["PDP_PARA_NO"]?.ToString(),
                        ARPSEParaNo = rdr["ARPSE_PARA_NO"]?.ToString(),
                        ObservationText = rdr["OBSERVATION_TEXT"]?.ToString(),
                        DateEntered = rdr["DATE_ENTERED"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["DATE_ENTERED"]),
                        EnteredBy = rdr["ENTERED_BY"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ENTERED_BY"]),
                        CurrentStatus = rdr["CURRENT_STATUS"]?.ToString(),
                        AssignedDivisionId = rdr["ASSIGNED_DIVISION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ASSIGNED_DIVISION_ID"]),
                        AssignedDepartmentId = rdr["ASSIGNED_DEPARTMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ASSIGNED_DEPARTMENT_ID"]),
                        LegacyFlag = rdr["LEGACY_FLAG"]?.ToString(),
                        LegacyYear = rdr["LEGACY_YEAR"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["LEGACY_YEAR"]),
                        SupportingDocs = rdr["SUPPORTING_DOCS"]?.ToString(),
                        OriginalObsDate = rdr["ORIGINAL_OBS_DATE"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(rdr["ORIGINAL_OBS_DATE"])
                        // Add other fields as per your model/table
                        };
                    observations.Add(model);
                    }
                }
            }

        con.Close();
        return observations;
        }

    // Example for calling an Insert/Update SP (with output param)
    public int AddObservation(CAObservationModel model)
        {
        sessionHandler = new SessionHandler();
        sessionHandler._httpCon = this._httpCon;
        sessionHandler._session = this._session;
        sessionHandler._configuration = this._configuration;

        var con = this.DatabaseConnection();
        con.Open();
        int newId = 0;

        using (OracleCommand cmd = con.CreateCommand())
            {
            cmd.CommandText = "pkg_CA.Add_Observation";
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("p_observation_text", OracleDbType.Varchar2, 2000).Value = model.ObservationText;
            cmd.Parameters.Add("p_entered_by", OracleDbType.Int32).Value = model.EnteredBy;
            cmd.Parameters.Add("p_legacy_flag", OracleDbType.Char, 1).Value = model.LegacyFlag ?? "N";
            cmd.Parameters.Add("p_legacy_year", OracleDbType.Int32).Value = (object)model.LegacyYear ?? DBNull.Value;
            cmd.Parameters.Add("p_supporting_docs", OracleDbType.Varchar2, 500).Value = model.SupportingDocs;
            cmd.Parameters.Add("p_original_obs_date", OracleDbType.Date).Value = (object)model.OriginalObsDate ?? DBNull.Value;
            cmd.Parameters.Add("o_observation_id", OracleDbType.Int32).Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();
            newId = Convert.ToInt32(cmd.Parameters["o_observation_id"].Value.ToString());
            }

        con.Close();
        return newId;
        }

    // Add more methods for responses, audit trail, etc.
    }
}
