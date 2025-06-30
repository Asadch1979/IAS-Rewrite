using AIS.Models.IID;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Linq;

namespace AIS.Controllers
{
    public partial class DBConnection
    {
        public int SubmitComplaint(ComplaintModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_COMPLAINT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_nature", OracleDbType.Varchar2).Value = model.Nature ?? string.Empty;
                cmd.Parameters.Add("p_contents", OracleDbType.Clob).Value = model.Contents ?? string.Empty;
                cmd.Parameters.Add("p_uploaded_complaint", OracleDbType.Varchar2).Value = model.ComplaintFile?.FileName ?? string.Empty;
                cmd.Parameters.Add("p_uploaded_ffr", OracleDbType.Varchar2).Value = model.FfrFile?.FileName ?? string.Empty;
                string evidenceNames = model.OtherEvidence != null ? string.Join(",", model.OtherEvidence.Select(f => f.FileName)) : string.Empty;
                cmd.Parameters.Add("p_uploaded_evidence", OracleDbType.Varchar2).Value = evidenceNames;
                cmd.Parameters.Add("p_action_required", OracleDbType.Varchar2).Value = model.ActionRequired ?? string.Empty;
                cmd.Parameters.Add("p_submitted_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("o_complaint_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var id = Convert.ToInt32(cmd.Parameters["o_complaint_id"].Value.ToString());
                con.Dispose();
                return id;
            }
        }

        public void AddAssessment(AssessmentModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_ASSESSMENT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_complaint_id", OracleDbType.Int32).Value = model.ComplaintId;
                cmd.Parameters.Add("p_received_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("p_assessment", OracleDbType.Clob).Value = model.Assessment ?? string.Empty;
                cmd.Parameters.Add("p_recommendation", OracleDbType.Varchar2).Value = model.Recommendation ?? string.Empty;
                cmd.Parameters.Add("o_assessment_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddHeadReview(HeadReviewModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_HEAD_REVIEW";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_complaint_id", OracleDbType.Int32).Value = model.ComplaintId;
                cmd.Parameters.Add("p_assessment_id", OracleDbType.Int32).Value = 0;
                cmd.Parameters.Add("p_reviewed_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("p_directions", OracleDbType.Clob).Value = model.Directions ?? string.Empty;
                cmd.Parameters.Add("p_assigned_to_unit", OracleDbType.Int32).Value = model.AssignedUserId;
                cmd.Parameters.Add("p_referred_back_comments", OracleDbType.Clob).Value = model.Comments ?? string.Empty;
                cmd.Parameters.Add("p_action", OracleDbType.Varchar2).Value = model.ReferBack ? "Refer" : "Forward";
                cmd.Parameters.Add("o_review_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddInvestigationPlan(InvestigationPlanModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_INV_PLAN";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_complaint_id", OracleDbType.Int32).Value = model.ComplaintId;
                cmd.Parameters.Add("p_plan_details", OracleDbType.Clob).Value = model.Plan ?? string.Empty;
                cmd.Parameters.Add("p_submitted_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = "Pending";
                cmd.Parameters.Add("o_plan_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddPlanApproval(PlanApprovalModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_PLAN_APPROVAL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_plan_id", OracleDbType.Int32).Value = model.PlanId;
                cmd.Parameters.Add("p_approved_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("p_is_approved", OracleDbType.Varchar2).Value = model.Action ?? string.Empty;
                cmd.Parameters.Add("p_edited_plan", OracleDbType.Clob).Value = model.UpdatedPlan ?? string.Empty;
                cmd.Parameters.Add("p_further_actions", OracleDbType.Clob).Value = model.FurtherAction ?? string.Empty;
                cmd.Parameters.Add("o_approval_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddInquiryReport(InquiryReportModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_INQUIRY_REPORT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_complaint_id", OracleDbType.Int32).Value = model.ComplaintId;
                cmd.Parameters.Add("p_name_complainant", OracleDbType.Varchar2).Value = string.Empty;
                cmd.Parameters.Add("p_name_accused", OracleDbType.Varchar2).Value = string.Empty;
                cmd.Parameters.Add("p_gist", OracleDbType.Clob).Value = model.ReportText ?? string.Empty;
                cmd.Parameters.Add("p_proceedings", OracleDbType.Clob).Value = string.Empty;
                cmd.Parameters.Add("p_findings", OracleDbType.Clob).Value = string.Empty;
                cmd.Parameters.Add("p_recommendation", OracleDbType.Clob).Value = string.Empty;
                cmd.Parameters.Add("p_uploaded_report", OracleDbType.Varchar2).Value = model.ReportFile?.FileName ?? string.Empty;
                string reportEvidence = model.Evidence != null ? string.Join(",", model.Evidence.Select(f => f.FileName)) : string.Empty;
                cmd.Parameters.Add("p_uploaded_evidence", OracleDbType.Varchar2).Value = reportEvidence;
                cmd.Parameters.Add("p_submitted_on", OracleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("p_submitted_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("o_report_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddAnalysis(AnalysisModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_ANALYSIS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_report_id", OracleDbType.Int32).Value = model.ReportId;
                cmd.Parameters.Add("p_policy_gaps", OracleDbType.Clob).Value = model.PolicyGaps ?? string.Empty;
                cmd.Parameters.Add("p_control_gaps", OracleDbType.Clob).Value = model.ControlGaps ?? string.Empty;
                cmd.Parameters.Add("p_procedural_violations", OracleDbType.Clob).Value = model.ProceduralViolations ?? string.Empty;
                cmd.Parameters.Add("p_forward_to", OracleDbType.Varchar2).Value = string.Empty;
                cmd.Parameters.Add("p_comments", OracleDbType.Clob).Value = model.Comments ?? string.Empty;
                cmd.Parameters.Add("p_analyzed_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("o_analysis_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddFinalApproval(FinalApprovalModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_FINAL_APPROVAL";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_report_id", OracleDbType.Int32).Value = model.ReportId;
                cmd.Parameters.Add("p_comments", OracleDbType.Clob).Value = model.Comments ?? string.Empty;
                cmd.Parameters.Add("p_approved", OracleDbType.Varchar2).Value = model.ReferBack ? "N" : "Y";
                cmd.Parameters.Add("p_approved_by", OracleDbType.Int32).Value = loggedInUser.ID;
                cmd.Parameters.Add("o_final_approval_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public void AddCaseStudy(CaseStudyModel model)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.ADD_CASE_STUDY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_complaint_id", OracleDbType.Int32).Value = model.ComplaintId;
                cmd.Parameters.Add("p_origin_process_owner", OracleDbType.Varchar2).Value = model.OriginatingProcessOwner ?? string.Empty;
                cmd.Parameters.Add("p_name_complainant", OracleDbType.Varchar2).Value = model.Name ?? string.Empty;
                cmd.Parameters.Add("p_branch", OracleDbType.Varchar2).Value = model.Branch ?? string.Empty;
                cmd.Parameters.Add("p_gist", OracleDbType.Clob).Value = model.Gist ?? string.Empty;
                cmd.Parameters.Add("p_outcome", OracleDbType.Clob).Value = model.Outcome ?? string.Empty;
                cmd.Parameters.Add("p_modus_operandi", OracleDbType.Clob).Value = model.ModusOperandi ?? string.Empty;
                cmd.Parameters.Add("p_gaps", OracleDbType.Clob).Value = model.Gaps ?? string.Empty;
                cmd.Parameters.Add("p_root_cause", OracleDbType.Clob).Value = model.RootCause ?? string.Empty;
                cmd.Parameters.Add("p_actions_rec", OracleDbType.Clob).Value = model.ActionsRecommended ?? string.Empty;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = model.Status ?? string.Empty;
                cmd.Parameters.Add("o_case_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
            }
            con.Dispose();
        }

        public DataTable GetReports(ReportFilterModel filter)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.GET_REPORTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_filter", OracleDbType.Varchar2).Value = filter?.Nature ?? string.Empty;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                var dt = new DataTable();
                using (var rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                }
                con.Dispose();
                return dt;
            }
        }

        public DataTable GetComplaintsByUser(int userId)
        {
            string resp = string.Empty;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
            {
                cmd.CommandText = "PKG_INQ.GET_COMPLAINTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("p_user_id", OracleDbType.Int32).Value = userId;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                var dt = new DataTable();
                using (var rdr = cmd.ExecuteReader())
                {
                    dt.Load(rdr);
                }
                con.Dispose();
                return dt;
            }
        }
    }
}
