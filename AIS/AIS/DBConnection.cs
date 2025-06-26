
        // ----- Manpower & Staff Position procedures -----

        public void AddStaffPosition(StaffPosition sp)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "ADD_STAFF_POSITION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_PPNO", OracleDbType.Varchar2).Value = sp.PPNO;
                cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = sp.Name;
                cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2).Value = sp.Rank;
                cmd.Parameters.Add("P_DESIGNATION", OracleDbType.Varchar2).Value = sp.Designation;
                cmd.Parameters.Add("P_PLACEMENT", OracleDbType.Varchar2).Value = sp.Placement;
                cmd.Parameters.Add("P_HIGHEST_QUAL", OracleDbType.Varchar2).Value = sp.HighestQualification;
                cmd.Parameters.Add("P_SPECIALIZATION", OracleDbType.Varchar2).Value = sp.Specialization;
                cmd.Parameters.Add("P_AUDIT_CERT", OracleDbType.Varchar2).Value = sp.AuditCertification;
                cmd.Parameters.Add("P_TOT_EXPERIENCE", OracleDbType.Int32).Value = sp.TotalExperience;
                cmd.Parameters.Add("P_AUDIT_EXPERIENCE", OracleDbType.Int32).Value = sp.AuditExperience;
                cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = sp.ZoneId;
                cmd.Parameters.Add("P_COMPANY", OracleDbType.Varchar2).Value = sp.Company;
                cmd.Parameters.Add("P_CREATED_BY", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public void UpdateStaffPosition(StaffPosition sp)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "UPDATE_STAFF_POSITION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = sp.Id;
                cmd.Parameters.Add("P_PPNO", OracleDbType.Varchar2).Value = sp.PPNO;
                cmd.Parameters.Add("P_NAME", OracleDbType.Varchar2).Value = sp.Name;
                cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2).Value = sp.Rank;
                cmd.Parameters.Add("P_DESIGNATION", OracleDbType.Varchar2).Value = sp.Designation;
                cmd.Parameters.Add("P_PLACEMENT", OracleDbType.Varchar2).Value = sp.Placement;
                cmd.Parameters.Add("P_HIGHEST_QUAL", OracleDbType.Varchar2).Value = sp.HighestQualification;
                cmd.Parameters.Add("P_SPECIALIZATION", OracleDbType.Varchar2).Value = sp.Specialization;
                cmd.Parameters.Add("P_AUDIT_CERT", OracleDbType.Varchar2).Value = sp.AuditCertification;
                cmd.Parameters.Add("P_TOT_EXPERIENCE", OracleDbType.Int32).Value = sp.TotalExperience;
                cmd.Parameters.Add("P_AUDIT_EXPERIENCE", OracleDbType.Int32).Value = sp.AuditExperience;
                cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = sp.ZoneId;
                cmd.Parameters.Add("P_COMPANY", OracleDbType.Varchar2).Value = sp.Company;
                cmd.Parameters.Add("P_UPDATED_BY", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public void AddManpowerDemand(ManpowerDemand d)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "ADD_MANPOWER_DEMAND";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_STAFF_POSITION_ID", OracleDbType.Int32).Value = d.Id;
                cmd.Parameters.Add("P_RANK", OracleDbType.Varchar2).Value = d.Rank;
                cmd.Parameters.Add("P_PLACEMENT", OracleDbType.Varchar2).Value = d.Placement;
                cmd.Parameters.Add("P_EXISTING", OracleDbType.Int32).Value = d.Existing;
                cmd.Parameters.Add("P_ADDITIONAL_REQ", OracleDbType.Int32).Value = d.AdditionalRequired;
                cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = d.ZoneId;
                cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = d.Status;
                cmd.Parameters.Add("P_SUBMITTED_BY", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public void UpdateManpowerDemandStatus(int id, string status, string remarks)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection();
            con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "UPDATE_MANPOWER_DEMAND_STATUS";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = id;
                cmd.Parameters.Add("P_STATUS", OracleDbType.Varchar2).Value = status;
                cmd.Parameters.Add("P_UPDATED_BY", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                if (!string.IsNullOrEmpty(remarks))
                    cmd.Parameters.Add("P_REMARKS", OracleDbType.Varchar2).Value = remarks;
                else
                    cmd.Parameters.Add("P_REMARKS", OracleDbType.Varchar2).Value = DBNull.Value;
                cmd.ExecuteNonQuery();
                }
            con.Dispose();
            }

        public List<StaffPosition> GetStaffPositions(int zoneId)
            {
            List<StaffPosition> list = new List<StaffPosition>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "GET_STAFF_POSITION";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_ZONE_ID", OracleDbType.Int32).Value = zoneId;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    StaffPosition sp = new StaffPosition();
                    sp.Id = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]);
                    sp.PPNO = rdr["PPNO"].ToString();
                    sp.Name = rdr["NAME"].ToString();
                    sp.Rank = rdr["RANK"].ToString();
                    sp.Designation = rdr["DESIGNATION"].ToString();
                    sp.Placement = rdr["PLACEMENT"].ToString();
                    sp.HighestQualification = rdr["HIGHEST_QUAL"].ToString();
                    sp.Specialization = rdr["SPECIALIZATION"].ToString();
                    sp.AuditCertification = rdr["AUDIT_CERT"].ToString();
                    sp.TotalExperience = rdr["TOT_EXPERIENCE"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["TOT_EXPERIENCE"]);
                    sp.AuditExperience = rdr["AUDIT_EXPERIENCE"] == DBNull.Value ? 0 : Convert.ToDecimal(rdr["AUDIT_EXPERIENCE"]);
                    sp.ZoneId = rdr["ZONE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ZONE_ID"]);
                    sp.Company = rdr["COMPANY"].ToString();
                    list.Add(sp);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<ManpowerDemand> GetDemandSummary(string company)
            {
            List<ManpowerDemand> list = new List<ManpowerDemand>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "GET_DEMAND_SUMMARY";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_COMPANY", OracleDbType.Varchar2).Value = company;
                cmd.Parameters.Add("P_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ManpowerDemand d = new ManpowerDemand();
                    d.Id = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]);
                    d.Rank = rdr["RANK"].ToString();
                    d.Placement = rdr["PLACEMENT"].ToString();
                    d.Existing = rdr["EXISTING"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["EXISTING"]);
                    d.AdditionalRequired = rdr["ADDITIONAL_REQ"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ADDITIONAL_REQ"]);
                    d.ZoneId = rdr["ZONE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ZONE_ID"]);
                    d.Status = rdr["STATUS"].ToString();
                    d.SubmittedBy = rdr["SUBMITTED_BY"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["SUBMITTED_BY"]);
                    d.HeadFadRemarks = rdr["HEAD_FAD_REMARKS"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }
