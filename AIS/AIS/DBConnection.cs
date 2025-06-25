        // SBP Compliance Operations ------------------------------------------------------

        public int AddSBPObservation(string observationText, int divisionId, string attachmentPath)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            int observationId = 0;
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.ADD_OBSERVATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_text", OracleDbType.Clob).Value = observationText;
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = divisionId;
                cmd.Parameters.Add("p_created_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("p_attachment_path", OracleDbType.Varchar2).Value = attachmentPath;
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var val = cmd.Parameters["p_observation_id"].Value;
                if (val != null && val != DBNull.Value)
                    observationId = Convert.ToInt32(val.ToString());
                }
            con.Dispose();
            return observationId;
            }

        public void UpdateSBPObservationStatus(int observationId, string status)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.UPDATE_OBSERVATION_STATUS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = status;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public int AssignSBPObservation(int observationId, string assignedRole, int assignedId, string instructions)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            int assignmentId = 0;
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.ASSIGN_OBSERVATION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_assigned_to_role", OracleDbType.Varchar2).Value = assignedRole;
                cmd.Parameters.Add("p_assigned_to_id", OracleDbType.Int32).Value = assignedId;
                cmd.Parameters.Add("p_instructions", OracleDbType.Clob).Value = instructions;
                cmd.Parameters.Add("p_assigned_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("p_assignment_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var val = cmd.Parameters["p_assignment_id"].Value;
                if (val != null && val != DBNull.Value)
                    assignmentId = Convert.ToInt32(val.ToString());
                }
            con.Dispose();
            return assignmentId;
            }

        public void UpdateSBPAssignmentStatus(int assignmentId, string status)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.UPDATE_ASSIGNMENT_STATUS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_assignment_id", OracleDbType.Int32).Value = assignmentId;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = status;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public int AddSBPResponse(int observationId, int departmentId, string responseText, string attachmentPath)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            int responseId = 0;
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.ADD_RESPONSE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_department_id", OracleDbType.Int32).Value = departmentId;
                cmd.Parameters.Add("p_response_text", OracleDbType.Clob).Value = responseText;
                cmd.Parameters.Add("p_submitted_by", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("p_attachment_path", OracleDbType.Varchar2).Value = attachmentPath;
                cmd.Parameters.Add("p_response_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();
                var val = cmd.Parameters["p_response_id"].Value;
                if (val != null && val != DBNull.Value)
                    responseId = Convert.ToInt32(val.ToString());
                }
            con.Dispose();
            return responseId;
            }

        public void UpdateSBPResponseStatus(int responseId, string status)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.UPDATE_RESPONSE_STATUS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_response_id", OracleDbType.Int32).Value = responseId;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = status;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public void AddSBPReviewHistory(int observationId, string reviewerRole, int reviewerId, string comments, string action)
            {
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.ADD_REVIEW_HISTORY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("p_reviewer_role", OracleDbType.Varchar2).Value = reviewerRole;
                cmd.Parameters.Add("p_reviewer_id", OracleDbType.Int32).Value = reviewerId;
                cmd.Parameters.Add("p_comments", OracleDbType.Clob).Value = comments;
                cmd.Parameters.Add("p_action", OracleDbType.Varchar2).Value = action;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public List<SBPObservation> GetSBPObservations(int divisionId = 0, string status = "")
            {
            var con = this.DatabaseConnection();
            con.Open();
            List<SBPObservation> list = new List<SBPObservation>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.FETCH_OBSERVATIONS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_division_id", OracleDbType.Int32).Value = divisionId == 0 ? (object)DBNull.Value : divisionId;
                cmd.Parameters.Add("p_status", OracleDbType.Varchar2).Value = string.IsNullOrEmpty(status) ? (object)DBNull.Value : status;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    SBPObservation ob = new SBPObservation();
                    ob.OBSERVATION_ID = Convert.ToInt32(rdr["OBSERVATION_ID"].ToString());
                    ob.OBSERVATION_TEXT = rdr["OBSERVATION_TEXT"].ToString();
                    ob.DIVISION_ID = Convert.ToInt32(rdr["DIVISION_ID"].ToString());
                    ob.STATUS = rdr["STATUS"].ToString();
                    ob.CREATED_BY = Convert.ToInt32(rdr["CREATED_BY"].ToString());
                    ob.CREATED_ON = Convert.ToDateTime(rdr["CREATED_ON"].ToString());
                    ob.ATTACHMENT_PATH = rdr["ATTACHMENT_PATH"].ToString();
                    ob.DATE_RECEIVED = Convert.ToDateTime(rdr["DATE_RECEIVED"].ToString());
                    list.Add(ob);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<SBPAssignment> GetSBPAssignments(int observationId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            List<SBPAssignment> list = new List<SBPAssignment>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.FETCH_ASSIGNMENTS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    SBPAssignment asg = new SBPAssignment();
                    asg.ASSIGNMENT_ID = Convert.ToInt32(rdr["ASSIGNMENT_ID"].ToString());
                    asg.OBSERVATION_ID = Convert.ToInt32(rdr["OBSERVATION_ID"].ToString());
                    asg.ASSIGNED_TO_ROLE = rdr["ASSIGNED_TO_ROLE"].ToString();
                    asg.ASSIGNED_TO_ID = Convert.ToInt32(rdr["ASSIGNED_TO_ID"].ToString());
                    asg.INSTRUCTIONS = rdr["INSTRUCTIONS"].ToString();
                    asg.ASSIGNED_BY = Convert.ToInt32(rdr["ASSIGNED_BY"].ToString());
                    asg.ASSIGNED_ON = Convert.ToDateTime(rdr["ASSIGNED_ON"].ToString());
                    asg.STATUS = rdr["STATUS"].ToString();
                    list.Add(asg);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<SBPResponse> GetSBPResponses(int observationId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            List<SBPResponse> list = new List<SBPResponse>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.FETCH_RESPONSES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    SBPResponse rsp = new SBPResponse();
                    rsp.RESPONSE_ID = Convert.ToInt32(rdr["RESPONSE_ID"].ToString());
                    rsp.OBSERVATION_ID = Convert.ToInt32(rdr["OBSERVATION_ID"].ToString());
                    rsp.DEPARTMENT_ID = Convert.ToInt32(rdr["DEPARTMENT_ID"].ToString());
                    rsp.RESPONSE_TEXT = rdr["RESPONSE_TEXT"].ToString();
                    rsp.SUBMITTED_BY = Convert.ToInt32(rdr["SUBMITTED_BY"].ToString());
                    rsp.SUBMITTED_ON = Convert.ToDateTime(rdr["SUBMITTED_ON"].ToString());
                    rsp.STATUS = rdr["STATUS"].ToString();
                    rsp.ATTACHMENT_PATH = rdr["ATTACHMENT_PATH"].ToString();
                    list.Add(rsp);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<SBPReviewHistory> GetSBPReviewHistory(int observationId)
            {
            var con = this.DatabaseConnection();
            con.Open();
            List<SBPReviewHistory> list = new List<SBPReviewHistory>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "PKG_T_AU_SBP_COMPLIANCE.FETCH_REVIEW_HISTORY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("p_observation_id", OracleDbType.Int32).Value = observationId;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    SBPReviewHistory rh = new SBPReviewHistory();
                    rh.OBSERVATION_ID = Convert.ToInt32(rdr["OBSERVATION_ID"].ToString());
                    rh.REVIEWER_ROLE = rdr["REVIEWER_ROLE"].ToString();
                    rh.REVIEWER_ID = Convert.ToInt32(rdr["REVIEWER_ID"].ToString());
                    rh.COMMENTS = rdr["COMMENTS"].ToString();
                    rh.ACTION = rdr["ACTION"].ToString();
                    rh.REVIEWED_ON = Convert.ToDateTime(rdr["REVIEWED_ON"].ToString());
                    list.Add(rh);
                    }
                }
            con.Dispose();
            return list;
            }


