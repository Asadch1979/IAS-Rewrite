using AIS.Models;
using Microsoft.AspNetCore.Http;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;

namespace AIS.Controllers
    {
    public partial class DBConnection
        {
        public List<CAObservation> GetCAObservations(int? divisionId = null, string status = null)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<CAObservation> observations = new List<CAObservation>();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.GET_OBSERVATIONS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = (object)divisionId ?? DBNull.Value;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = (object)status ?? DBNull.Value;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        var model = new CAObservation
                            {
                            ObservationID = rdr["OBSERVATION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["OBSERVATION_ID"]),
                            ObservationText = rdr["OBSERVATION_TEXT"]?.ToString(),
                            DateEntered = rdr["DATE_ENTERED"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(rdr["DATE_ENTERED"]),
                            EnteredBy = rdr["ENTERED_BY"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ENTERED_BY"]),
                            CurrentStatus = rdr["CURRENT_STATUS"]?.ToString(),
                            AssignedDivisionID = rdr["ASSIGNED_DIVISION_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ASSIGNED_DIVISION_ID"]),
                            AssignedDepartmentID = rdr["ASSIGNED_DEPARTMENT_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ASSIGNED_DEPARTMENT_ID"]),
                            LegacyFlag = rdr["LEGACY_FLAG"]?.ToString(),
                            LegacyYear = rdr["LEGACY_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["LEGACY_YEAR"]),
                            SupportingDocuments = rdr["SUPPORTING_DOCS"]?.ToString()
                            };
                        observations.Add(model);
                        }
                    }
                }

            con.Close();
            return observations;
            }

        public int CACreateObservation(string observationText, int divisionId, int departmentId, string attachmentPath)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            int observationId = 0;
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.ADD_OBSERVATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_text", OracleDbType.Clob).Value = observationText;
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = divisionId;
                cmd.Parameters.Add("p_department_id", OracleDbType.Int32).Value = departmentId;
                cmd.Parameters.Add("p_entered_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("p_attachment_path", OracleDbType.Varchar2).Value = attachmentPath;
                cmd.Parameters.Add("o_observation_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                observationId = Convert.ToInt32(cmd.Parameters["o_observation_id"].Value.ToString());
                }
            con.Close();
            return observationId;
            }

        public void CASubmitToHeadFAD(int observationId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.SUBMIT_TO_HEADFAD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CAAssignToDivision(int observationId, int divisionId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.ASSIGN_DIVISION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = divisionId;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CAAssignToDepartment(int observationId, int departmentId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.ASSIGN_DEPARTMENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_department_id", OracleDbType.Int32).Value = departmentId;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CADepartmentResponse(int observationId, string responseText, string attachmentPath)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.DEPARTMENT_RESPONSE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_response_text", OracleDbType.Clob).Value = responseText;
                cmd.Parameters.Add("p_submitted_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("p_attachment_path", OracleDbType.Varchar2).Value = attachmentPath;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CAReviewAndForward(int observationId, string action, string remarks)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.REVIEW_AND_FORWARD";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_action", OracleDbType.Varchar2).Value = action;
                cmd.Parameters.Add("p_remarks", OracleDbType.Clob).Value = remarks;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CARejectOrReferBack(int observationId, string remarks)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.REJECT_OR_REFER_BACK";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_remarks", OracleDbType.Clob).Value = remarks;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CAFinalizeObservation(int observationId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.FINALIZE_OBSERVATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }

        public void CAEnterLegacyObservation(HttpRequest request)
            {
            var form = request.Form;
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_CA.ENTER_LEGACY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_year", OracleDbType.Int32).Value = Convert.ToInt32(form["legacy_year"]);
                cmd.Parameters.Add("p_observation_text", OracleDbType.Clob).Value = form["observation_text"];
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = Convert.ToInt32(form["division_id"]);
                cmd.Parameters.Add("p_department_id", OracleDbType.Int32).Value = Convert.ToInt32(form["department_id"]);
                cmd.ExecuteNonQuery();
                }
            con.Close();
            }
        }
    }
