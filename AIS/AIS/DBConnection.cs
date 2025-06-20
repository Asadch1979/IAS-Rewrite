
        public List<FADAuditEmpModel> GetAuditEmployees()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADAuditEmpModel> list = new List<FADAuditEmpModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_Get_Audit_EMP";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("io_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADAuditEmpModel mEmp = new FADAuditEmpModel();
                    mEmp.ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]);
                    mEmp.PPNO = rdr["PPNO"].ToString();
                    mEmp.NAME = rdr["NAME"].ToString();
                    mEmp.RANK = rdr["RANK"].ToString();
                    mEmp.DESIGNATION = rdr["DESIGNATION"].ToString();
                    mEmp.PLACEMENT = rdr["PLACEMENT"].ToString();
                    mEmp.QUALIFICATION = rdr["QUALIFICATION"].ToString();
                    mEmp.SPECIALIZATION = rdr["SPECIALIZATION"].ToString();
                    mEmp.CERTIFICATION = rdr["CERTIFICATION"].ToString();
                    mEmp.TOTAL_EXPERIENCE = rdr["TOTAL_EXPERIENCE"].ToString();
                    mEmp.AUDIT_EXPERIENCE = rdr["AUDIT_EXPERIENCE"].ToString();
                    list.Add(mEmp);
                    }
                }
            con.Dispose();
            return list;
            }

        public string UpdateAuditEmployee(FADAuditEmpModel m)
            {
            string resp = "";
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_update_audit_emp";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = m.ID;
                cmd.Parameters.Add("ppno", OracleDbType.Varchar2).Value = m.PPNO;
                cmd.Parameters.Add("rank", OracleDbType.Varchar2).Value = m.RANK;
                cmd.Parameters.Add("designation", OracleDbType.Varchar2).Value = m.DESIGNATION;
                cmd.Parameters.Add("placement", OracleDbType.Varchar2).Value = m.PLACEMENT;
                cmd.Parameters.Add("qualification", OracleDbType.Varchar2).Value = m.QUALIFICATION;
                cmd.Parameters.Add("specialization", OracleDbType.Varchar2).Value = m.SPECIALIZATION;
                cmd.Parameters.Add("certification", OracleDbType.Varchar2).Value = m.CERTIFICATION;
                cmd.Parameters.Add("tot_exp", OracleDbType.Varchar2).Value = m.TOTAL_EXPERIENCE;
                cmd.Parameters.Add("audit_exp", OracleDbType.Varchar2).Value = m.AUDIT_EXPERIENCE;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    resp = rdr[0].ToString();
                }
            con.Dispose();
            return resp;
            }

        public List<FADAuditManpowerModel> GetAuditManpower()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADAuditManpowerModel> list = new List<FADAuditManpowerModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_Get_Audit_Manpower";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("io_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADAuditManpowerModel mm = new FADAuditManpowerModel();
                    mm.ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]);
                    mm.COMPANY = rdr["COMPANY"].ToString();
                    mm.RANK = rdr["RANK"].ToString();
                    mm.PLACEMENT = rdr["PLACEMENT"].ToString();
                    mm.EXISTING = rdr["EXISTING"].ToString();
                    mm.ADDITIONAL_REQUIRED = rdr["ADDITIONAL_REQUIRED"].ToString();
                    list.Add(mm);
                    }
                }
            con.Dispose();
            return list;
            }

        public string UpdateAuditManpower(FADAuditManpowerModel m)
            {
            string resp = "";
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_update_audit_manpower";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = m.ID;
                cmd.Parameters.Add("company", OracleDbType.Varchar2).Value = m.COMPANY;
                cmd.Parameters.Add("rank", OracleDbType.Varchar2).Value = m.RANK;
                cmd.Parameters.Add("placement", OracleDbType.Varchar2).Value = m.PLACEMENT;
                cmd.Parameters.Add("existing", OracleDbType.Varchar2).Value = m.EXISTING;
                cmd.Parameters.Add("additional", OracleDbType.Varchar2).Value = m.ADDITIONAL_REQUIRED;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    resp = rdr[0].ToString();
                }
            con.Dispose();
            return resp;
            }

        public List<FADAuditBudgetModel> GetAuditBudget()
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<FADAuditBudgetModel> list = new List<FADAuditBudgetModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_Get_Audit_budget";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("io_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    FADAuditBudgetModel mb = new FADAuditBudgetModel();
                    mb.ID = rdr["ID"] == DBNull.Value ? 0 : Convert.ToInt32(rdr["ID"]);
                    mb.GL_CODE = rdr["GL_CODE"].ToString();
                    mb.GL_HEADING = rdr["GL_HEADING"].ToString();
                    mb.EXISTING = rdr["EXISTING"].ToString();
                    mb.UTILIZATION = rdr["UTILIZATION"].ToString();
                    mb.REMAND = rdr["REMAND"].ToString();
                    mb.RATIONALIZATION = rdr["RATIONALIZATION"].ToString();
                    mb.STATUS = rdr["STATUS"].ToString();
                    list.Add(mb);
                    }
                }
            con.Dispose();
            return list;
            }

        public string UpdateAuditBudget(FADAuditBudgetModel m)
            {
            string resp = "";
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_update_audit_budget";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("id", OracleDbType.Int32).Value = m.ID;
                cmd.Parameters.Add("gl_code", OracleDbType.Varchar2).Value = m.GL_CODE;
                cmd.Parameters.Add("gl_heading", OracleDbType.Varchar2).Value = m.GL_HEADING;
                cmd.Parameters.Add("existing", OracleDbType.Varchar2).Value = m.EXISTING;
                cmd.Parameters.Add("utilization", OracleDbType.Varchar2).Value = m.UTILIZATION;
                cmd.Parameters.Add("remand", OracleDbType.Varchar2).Value = m.REMAND;
                cmd.Parameters.Add("rationalization", OracleDbType.Varchar2).Value = m.RATIONALIZATION;
                cmd.Parameters.Add("status", OracleDbType.Varchar2).Value = m.STATUS;
                cmd.Parameters.Add("io_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    resp = rdr[0].ToString();
                }
            con.Dispose();
            return resp;
            }

        public List<DropDownModel> GetHrRanks()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_hr_rank";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetHrDesignations()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_hr_designation";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetHrPosting()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_hr_posting";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetQualifications()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_qualification";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetQualificationSpecialization()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_qualification_specialization";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetCertifications()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_certification";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["ID"].ToString();
                    d.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<DropDownModel> GetGLHeads()
            {
            List<DropDownModel> list = new List<DropDownModel>();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_ad.P_get_GL_Heads";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    DropDownModel d = new DropDownModel();
                    d.ID = rdr["GL_CODE"].ToString();
                    d.DESCRIPTION = rdr["GL_HEADING"].ToString();
                    list.Add(d);
                    }
                }
            con.Dispose();
            return list;
            }
