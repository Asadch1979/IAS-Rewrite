        public List<RoleRespModel> GetRoleResponsibleForChecklistDetail()
                cmd.CommandText = "pkg_fad.p_get_role_responsible";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
        public string GetUserName(string PPNUMBER)
            string userName = "";
                cmd.CommandText = "pkg_hd.p_ppno_name";
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = PPNUMBER;
                    userName = rdr["EMPLOYEE_NAME"].ToString();
            return userName;

        public List<AuditeeEntitiesModel> GetAuditeeEntitiesForOldParas(int ENTITY_ID = 0)
            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            var con = this.DatabaseConnection(); con.Open();

                cmd.CommandText = "pkg_ais.P_GetAuditeeEntitiesForOldParas";
                cmd.Parameters.Add("ENTITY_ID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    if (rdr["ENTITY_ID"].ToString() != "" && rdr["ENTITY_ID"].ToString() != null)
                        entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    if (rdr["entity_code"].ToString() != "" && rdr["entity_code"].ToString() != null)
                        entity.CODE = Convert.ToInt32(rdr["entity_code"]);
                    if (rdr["entity_name"].ToString() != "" && rdr["entity_name"].ToString() != null)
                        entity.NAME = rdr["entity_name"].ToString();
                    entitiesList.Add(entity);
            return entitiesList;

        

        public List<AuditeeEntitiesModel> GetProcOwnerForChecklistDetail()
            var con = this.DatabaseConnection(); con.Open();
            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
                cmd.CommandText = "pkg_fad.p_get_process_owner";

                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    if (rdr["ENTITY_ID"].ToString() != "" && rdr["ENTITY_ID"].ToString() != null)
                        entity.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    if (rdr["name"].ToString() != "" && rdr["name"].ToString() != null)
                        entity.NAME = rdr["name"].ToString();
                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;
            }
        public string GeneratePassword(int length = 8)
            {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] password = new char[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
                {
                byte[] data = new byte[length];
                rng.GetBytes(data);
                for (int i = 0; i < length; i++)
                    {
                    password[i] = validChars[data[i] % validChars.Length];
            return new string(password);
        public bool ChangePassword(string Password, string NewPassowrd)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;

            var loggedInUser = sessionHandler.GetSessionUser();
            var enc_pass = getMd5Hash(DecryptPassword(Password));
            bool correctPass = false;
            bool res = false;
            var enc_new_pass = getMd5Hash(DecryptPassword(NewPassowrd));
                cmd.CommandText = "pkg_lg.p_get_user";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("enc_pass", OracleDbType.Varchar2).Value = enc_pass;
                    if (rdr["USERID"].ToString() != null && rdr["USERID"].ToString() != "")
                        {
                        correctPass = true;
                        res = true;
                        }

                    }
                if (correctPass)
                    {
                    cmd.CommandText = "pkg_lg.P_ChangePassword";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("PP_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("enc_pass", OracleDbType.Varchar2).Value = enc_new_pass;
                    cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    cmd.ExecuteReader();
                    res = true;
            return res;

        public List<BranchModel> GetZoneBranches(int zone_code = 0, bool sessionCheck = true)

            List<BranchModel> branchList = new List<BranchModel>();
                cmd.CommandText = "pkg_hd.P_GetOldParasEntityid";
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = zone_code;
                    BranchModel br = new BranchModel();
                    br.BRANCHID = Convert.ToInt32(rdr["branchentityid"]);
                    br.BRANCHNAME = rdr["branchname"].ToString();
                    branchList.Add(br);
            return branchList;
        public BranchModel AddBranch(BranchModel br)
            {
            return br;
            }
        public BranchModel UpdateBranch(BranchModel br)
            {
            return br;
            }
        public ControlViolationsModel AddControlViolation(ControlViolationsModel cv)
            {
            return cv;
            }
        public List<DivisionModel> GetDivisions(bool sessionCheck = true)
            List<DivisionModel> divList = new List<DivisionModel>();
                cmd.CommandText = sqlParams.GetDivisionQueryFromParams();
                    DivisionModel div = new DivisionModel();
                    div.DIVISIONID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    div.NAME = rdr["NAME"].ToString();
                    div.CODE = rdr["CODE"].ToString();
                    div.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    if (rdr["ACTIVE"].ToString() == "Y")
                        div.ISACTIVE = "Active";
                    else if (rdr["ACTIVE"].ToString() == "N")
                        div.ISACTIVE = "InActive";
                    else
                        div.ISACTIVE = rdr["ACTIVE"].ToString();
                    divList.Add(div);
            return divList;
            }

        public List<AuditObservationTemplateModel> GetAuditObservationTemplates(int activity_id)
            {
            List<AuditObservationTemplateModel> templateList = new List<AuditObservationTemplateModel>();
            return templateList;
       
       

        public List<AuditPlanModel> GetAuditPlan(int period_id = 0)
            var con = this.DatabaseConnection(); con.Open();
            List<AuditPlanModel> planList = new List<AuditPlanModel>();

                string _sql = "pkg_ais.p_get_audit_plan";
                cmd.Parameters.Add("AUDITED_BY", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.CommandText = _sql;
                    AuditPlanModel plan = new AuditPlanModel();
                    plan.PLAN_ID = Convert.ToInt32(rdr["PLAN_ID"]);
                    plan.AUDITPERIOD_ID = Convert.ToInt32(rdr["AUDITPERIOD_ID"]);
                    if (rdr["NO_OF_DAYS_AUDIT"].ToString() != null && rdr["NO_OF_DAYS_AUDIT"].ToString() != "")
                        plan.NO_OF_DAYS_AUDIT = Convert.ToInt32(rdr["NO_OF_DAYS_AUDIT"]);
                    if (rdr["AUDITZONE_ID"].ToString() != null && rdr["AUDITZONE_ID"].ToString() != "")
                        plan.AUDITZONE_ID = Convert.ToInt32(rdr["AUDITZONE_ID"]);
                    if (rdr["BRANCH_ID"].ToString() != null && rdr["BRANCH_ID"].ToString() != "")
                        plan.BRANCH_ID = Convert.ToInt32(rdr["BRANCH_ID"]);
                    if (rdr["DIVISION_ID"].ToString() != null && rdr["DIVISION_ID"].ToString() != "")
                        plan.DIVISION_ID = Convert.ToInt32(rdr["DIVISION_ID"]);
                    if (rdr["DEPARTMENT_ID"].ToString() != null && rdr["DEPARTMENT_ID"].ToString() != "")
                        plan.DEPARTMENT_ID = Convert.ToInt32(rdr["DEPARTMENT_ID"]);
                    if (rdr["PLAN_STATUS_ID"].ToString() != null && rdr["PLAN_STATUS_ID"].ToString() != "")
                        plan.PLAN_STATUS_ID = Convert.ToInt32(rdr["PLAN_STATUS_ID"]);
                    if (rdr["BRANCH_SIZE_ID"].ToString() != null && rdr["BRANCH_SIZE_ID"].ToString() != "")
                        plan.BRANCH_SIZE_ID = Convert.ToInt32(rdr["BRANCH_SIZE_ID"]);
                    if (rdr["RISK_LEVEL_ID"].ToString() != null && rdr["RISK_LEVEL_ID"].ToString() != "")
                        plan.RISK_LEVEL_ID = Convert.ToInt32(rdr["RISK_LEVEL_ID"]);
                    if (rdr["SUB_ENTITY_ID"].ToString() != null && rdr["SUB_ENTITY_ID"].ToString() != "")
                        plan.SUB_ENTITY_ID = Convert.ToInt32(rdr["SUB_ENTITY_ID"]);
                    plan.DEPARTMENT_NAME = rdr["DEPARTMENT_NAME"].ToString();
                    plan.BRANCH_NAME = rdr["BRANCH_NAME"].ToString();
                    plan.DIVISION_NAME = rdr["DIVISION_NAME"].ToString();
                    plan.AUDITZONE_NAME = rdr["AUDITZONE_NAME"].ToString();
                    planList.Add(plan);
            return planList;
        public List<RiskProcessDefinition> GetRiskProcessDefinition()
            List<RiskProcessDefinition> pdetails = new List<RiskProcessDefinition>();
                cmd.CommandText = "pkg_lg.P_GetRiskProcessDefinition";
                    RiskProcessDefinition proc = new RiskProcessDefinition();
                    proc.P_ID = Convert.ToInt32(rdr["T_ID"]);
                    if (rdr["ENTITY_TYPE"].ToString() != null && rdr["ENTITY_TYPE"].ToString() != "")
                        proc.RISK_ID = Convert.ToInt32(rdr["ENTITY_TYPE"]);
                    proc.P_NAME = rdr["HEADING"].ToString();
                    pdetails.Add(proc);
            return pdetails;
       






        public List<PreConcludingModel> GetEntityObservationDetails(int ENG_ID = 0)
            var con = this.DatabaseConnection(); con.Open();
            List<PreConcludingModel> list = new List<PreConcludingModel>();
                cmd.CommandText = "pkg_hd.P_get_audit_pre_Concluding";
                cmd.Parameters.Add("engid", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    PreConcludingModel chk = new PreConcludingModel();
                    chk.OBS_ID = rdr["id"].ToString();
                    chk.OBS_STATUS = rdr["ob_status"].ToString();
                    chk.FINAL_PARA_NO = rdr["final_para_no"].ToString();
                    chk.HEADING = rdr["headings"].ToString();
                    chk.STATUS = rdr["STATUS"].ToString();
                    chk.OBS_RISK = rdr["severity"].ToString();
                    list.Add(chk);
            return list;
        public List<GlHeadDetailsModel> GetGlheadDetails(int engId = 0, int gl_code = 0)
            int ENG_ID = this.GetLoggedInUserEngId();


            List<GlHeadDetailsModel> list = new List<GlHeadDetailsModel>();

                cmd.CommandText = "pkg_ai.p_getglheadsummary";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = engId;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                OracleDataReader rdr = cmd.ExecuteReader();
                    GlHeadDetailsModel GlHeadDetails = new GlHeadDetailsModel();
                    GlHeadDetails.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);
                    GlHeadDetails.GL_TYPEID = Convert.ToInt32(rdr["GL_TYPEID"]);
                    GlHeadDetails.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    // GlHeadDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    //GlHeadDetails.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    //GlHeadDetails.DATETIME = Convert.ToDateTime(rdr["DATETIME"]);
                    GlHeadDetails.BALANCE = Convert.ToDouble(rdr["BALANCE"]);
                    if (rdr["DEBIT"].ToString() != null && rdr["DEBIT"].ToString() != "")
                        GlHeadDetails.DEBIT = Convert.ToDouble(rdr["DEBIT"]);
                    if (rdr["CREDIT"].ToString() != null && rdr["CREDIT"].ToString() != "")
                        GlHeadDetails.CREDIT = Convert.ToDouble(rdr["CREDIT"]);
                    list.Add(GlHeadDetails);
            return list;
        public GlHeadSubDetailsModel GetGlheadSubDetails(int gltypeid = 0)

            GlHeadSubDetailsModel GlHeadSubDetails = new GlHeadSubDetailsModel();
            List<GlHeadSubDetailsModel> GlSubHeadList = new List<GlHeadSubDetailsModel>();
                cmd.CommandText = "pkg_ai.p_getglheadsum";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("gltypeid", OracleDbType.Int32).Value = gltypeid;
                    GlHeadSubDetailsModel GHSD = new GlHeadSubDetailsModel();

                    GHSD.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    GHSD.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);

                    GHSD.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    GHSD.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    //GHSD.DATETIME = Convert.ToDateTime(rdr["DATETIME"]);
                    GHSD.BALANCE = Convert.ToDouble(rdr["BALANCE"]);
                    GHSD.DEBIT = Convert.ToDouble(rdr["DEBIT"]);
                    GHSD.CREDIT = Convert.ToDouble(rdr["CREDIT"]);
                    GlSubHeadList.Add(GHSD);
                    GlHeadSubDetails.GL_SUBDETAILS = GlSubHeadList;
            return GlHeadSubDetails;

        public List<LoanCaseModel> GetLoanCaseDetails(int lid = 0, string type = "", int ENG_ID = 0)


            List<LoanCaseModel> list = new List<LoanCaseModel>();
            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_ai.P_GetLoanCaseDetails";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("loantype", OracleDbType.Varchar2).Value = type;
                    LoanCaseModel LoanCaseDetails = new LoanCaseModel();
                    //LoanCaseDetails.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);
                    LoanCaseDetails.CNIC = Convert.ToDouble(rdr["CNIC"]);
                    LoanCaseDetails.LOAN_CASE_NO = Convert.ToInt32(rdr["LOAN_CASE_NO"]);
                    LoanCaseDetails.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    LoanCaseDetails.FATHERNAME = rdr["FATHERNAME"].ToString();
                    LoanCaseDetails.DISBURSED_AMOUNT = Convert.ToDouble(rdr["DISBURSED_AMOUNT"]);
                    LoanCaseDetails.PRIN = Convert.ToDouble(rdr["PRIN"]);
                    LoanCaseDetails.MARKUP = Convert.ToDouble(rdr["MARKUP"]);
                    LoanCaseDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    // LoanCaseDetails.LOAN_DISB_ID = Convert.ToDouble(rdr["LOAN_DISB_ID"]);
                    LoanCaseDetails.DISB_DATE = Convert.ToDateTime(rdr["DISB_DATE"]);
                    LoanCaseDetails.DISB_STATUSID = Convert.ToInt32(rdr["DISB_STATUSID"]);
                    list.Add(LoanCaseDetails);
            return list;
        public List<LoanCasedocModel> GetLoanCaseDocuments(int ENG_ID)
            List<LoanCasedocModel> list = new List<LoanCasedocModel>();



            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_ais.P_GetLoanCaseDocuments";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    LoanCasedocModel LoanCaseDetails = new LoanCasedocModel();
                    LoanCaseDetails.TEAM_MEM_PPNO = Convert.ToString(rdr["TEAM_MEM_PPNO"]);
                    LoanCaseDetails.BRANCHCODE = Convert.ToString(rdr["BRANCHCODE"]);
                    LoanCaseDetails.LOAN_APP_ID = Convert.ToString(rdr["LOAN_APP_ID"]);
                    LoanCaseDetails.CNIC = Convert.ToString(rdr["CNIC"]);
                    LoanCaseDetails.LOAN_CASE_NO = Convert.ToString(rdr["LOAN_CASE_NO"]);
                    LoanCaseDetails.GLSUBCODE = Convert.ToString(rdr["GLSUBCODE"]);
                    LoanCaseDetails.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    LoanCaseDetails.LOAN_DISB_ID = Convert.ToString(rdr["LOAN_DISB_ID"]);
                    LoanCaseDetails.DOCUMENTS = rdr["DOCUMENTS"].ToString();
                    LoanCaseDetails.IMAGES = rdr["IMAGES"].ToString();
                    list.Add(LoanCaseDetails);
            return list;
        public List<GlHeadDetailsModel> GetIncomeExpenceDetails(int bid = 0, int ENG_ID = 0)
            var con = this.DatabaseConnection(); con.Open();
            List<GlHeadDetailsModel> list = new List<GlHeadDetailsModel>();

                cmd.CommandText = "pkg_ai.P_GetIncomeExpenceDetails";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    GlHeadDetailsModel GlHeadDetails = new GlHeadDetailsModel();
                    //GlHeadDetails.TEAM_MEM_PPNO = Convert.ToDouble(rdr["TEAM_MEM_PPNO"]);
                    GlHeadDetails.NAME = rdr["NAME"].ToString();
                    GlHeadDetails.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    GlHeadDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    // GlHeadDetails.DESCRIPTION = rdr["DESCRIPTION"].ToString();



                    //GlHeadDetails.DAY_END_BALANCE_DATE = Convert.ToDateTime(rdr["DAY_END_BALANCE_DATE"]);
                    // GlHeadDetails.BALANCE = Convert.ToDouble(rdr["BALANCE"]);
                    if (rdr["DEBIT"].ToString() != null && rdr["DEBIT"].ToString() != "")
                        GlHeadDetails.DEBIT = Convert.ToDouble(rdr["DEBIT"]);
                    if (rdr["CREDIT"].ToString() != null && rdr["CREDIT"].ToString() != "")
                        GlHeadDetails.CREDIT = Convert.ToDouble(rdr["CREDIT"]);
                    list.Add(GlHeadDetails);
            return list;
        public List<DepositAccountModel> GetDepositAccountdetails()
            List<DepositAccountModel> depositacclist = new List<DepositAccountModel>();
            /*

           var con = this.DatabaseConnection(); con.Open();
            
            {

                cmd.CommandText = "pkg_ais.P_GetDepositAccountdetails";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                {
                    DepositAccountModel depositaccdetails = new DepositAccountModel();
                    depositaccdetails.NAME = rdr["NAME"].ToString();
                    depositacclist.Add(depositaccdetails);
           con.Dispose();*/
            return depositacclist;
            }
        public List<DepositAccountModel> GetDepositAccountSubdetails(string bname = "")
            int ENG_ID = this.GetLoggedInUserEngId();
            var con = this.DatabaseConnection(); con.Open();
            List<DepositAccountModel> depositaccsublist = new List<DepositAccountModel>();
                cmd.CommandText = "pkg_ai.P_GetDepositAccountSubdetails";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    DepositAccountModel depositaccsubdetails = new DepositAccountModel();
                    depositaccsubdetails.BRANCH_NAME = rdr["BRANCH_NAME"].ToString();
                    if (rdr["ACC_NUMBER"].ToString() != null && rdr["ACC_NUMBER"].ToString() != "")
                        depositaccsubdetails.ACC_NUMBER = Convert.ToDouble(rdr["ACC_NUMBER"]);
                    if (rdr["ACCOUNTCATEGORY"].ToString() != null && rdr["ACCOUNTCATEGORY"].ToString() != "")
                        depositaccsubdetails.ACCOUNTCATEGORY = rdr["ACCOUNTCATEGORY"].ToString();
                    if (rdr["CUSTOMERNAME"].ToString() != null && rdr["CUSTOMERNAME"].ToString() != "")
                        depositaccsubdetails.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    if (rdr["BMVS_VERIFIED"].ToString() != null && rdr["BMVS_VERIFIED"].ToString() != "")
                        depositaccsubdetails.BMVS_VERIFIED = rdr["BMVS_VERIFIED"].ToString();
                    if (rdr["OPENINGDATE"].ToString() != null && rdr["OPENINGDATE"].ToString() != "")
                        {
                        depositaccsubdetails.OPENINGDATE = Convert.ToDateTime(rdr["OPENINGDATE"]);
                        }
                    if (rdr["CNIC"].ToString() != null && rdr["CNIC"].ToString() != "")
                        {
                        depositaccsubdetails.CNIC = Convert.ToDouble(rdr["CNIC"]);
                        }
                    if (rdr["TITLE"].ToString() != null && rdr["TITLE"].ToString() != "")
                        depositaccsubdetails.TITLE = rdr["TITLE"].ToString();
                    if (rdr["ACCOCUNTSTATUS"].ToString() != null && rdr["ACCOCUNTSTATUS"].ToString() != "")
                        depositaccsubdetails.ACCOUNTSTATUS = rdr["ACCOCUNTSTATUS"].ToString();
                    if (rdr["LASTTRANSACTIONDATE"].ToString() != null && rdr["LASTTRANSACTIONDATE"].ToString() != "")
                        {
                        depositaccsubdetails.LASTTRANSACTIONDATE = Convert.ToDateTime(rdr["LASTTRANSACTIONDATE"]);
                        }
                    if (rdr["CNICEXPIRYDATE"].ToString() != null && rdr["CNICEXPIRYDATE"].ToString() != "")
                        {
                        depositaccsubdetails.CNICEXPIRYDATE = Convert.ToDateTime(rdr["CNICEXPIRYDATE"]);
                        }
                    depositaccsublist.Add(depositaccsubdetails);
                    }
                }
            con.Dispose();
            return depositaccsublist;
            }
        public List<LoanCaseModel> GetBranchDesbursementAccountdetails(int bid = 0)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;

            int brId = Convert.ToInt32(loggedInUser.UserPostingBranch);
            List<LoanCaseModel> list = new List<LoanCaseModel>();
            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_ai.P_GetBranchDesbursementAccountdetails";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    LoanCaseModel LoanCaseDetails = new LoanCaseModel();
                    //  LoanCaseDetails.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);
                    LoanCaseDetails.CNIC = Convert.ToDouble(rdr["CNIC"]);
                    LoanCaseDetails.LOAN_CASE_NO = Convert.ToInt32(rdr["LOAN_CASE_NO"]);
                    LoanCaseDetails.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    LoanCaseDetails.FATHERNAME = rdr["FATHERNAME"].ToString();
                    LoanCaseDetails.DISBURSED_AMOUNT = Convert.ToDouble(rdr["DISBURSED_AMOUNT"]);
                    LoanCaseDetails.PRIN = Convert.ToDouble(rdr["PRIN"]);
                    LoanCaseDetails.MARKUP = Convert.ToDouble(rdr["MARKUP"]);
                    LoanCaseDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    //  LoanCaseDetails.LOAN_DISB_ID = Convert.ToDouble(rdr["LOAN_DISB_ID"]);
                    LoanCaseDetails.DISB_DATE = Convert.ToDateTime(rdr["DISB_DATE"]);
                    LoanCaseDetails.DISB_STATUSID = Convert.ToInt32(rdr["DISB_STATUSID"]);
                    list.Add(LoanCaseDetails);
            return list;
        public List<ObservationResponsiblePPNOModel> GetOldParasObservationResponsiblePPNOsUpdatedByImp(int PARA_ID, string PARA_CATEGORY, int AU_OBS_ID)
            List<ObservationResponsiblePPNOModel> list = new List<ObservationResponsiblePPNOModel>();

                cmd.CommandText = "pkg_hd.P_GetOldParasforfinalsettlement_responsibles";
                cmd.Parameters.Add("paraRef", OracleDbType.Varchar2).Value = PARA_ID;
                cmd.Parameters.Add("OBSID", OracleDbType.Varchar2).Value = AU_OBS_ID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                    ObservationResponsiblePPNOModel usr = new ObservationResponsiblePPNOModel();
                    usr.EMP_NAME = rdr["EMP_NAME"].ToString();
                    usr.PP_NO = rdr["PP_NO"].ToString();
                    usr.LOAN_CASE = rdr["LOANCASE"].ToString();
                    usr.LC_AMOUNT = rdr["LCAMOUNT"].ToString();
                    usr.ACCOUNT_NUMBER = rdr["ACCNUMBER"].ToString();
                    usr.ACC_AMOUNT = rdr["ACAMOUNT"].ToString();
                    list.Add(usr);
            return list;
        public int GetLoggedInUserEngId()
            var con = this.DatabaseConnection(); con.Open();
            int engId = 0;
                cmd.CommandText = "pkg_lg.P_GetLoggedInUserEngId";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    engId = Convert.ToInt32(rdr["eng_plan_id"]);
            return engId;
        public string GetRiskDescByID(int risk_id = 0)
            string response = "";
                cmd.CommandText = "pkg_ais.P_GetRiskDescByID";
                cmd.Parameters.Add("risk_id", OracleDbType.Int32).Value = risk_id;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    response = rdr["DESCRIPTION"].ToString();
                    }
            return response;
        



        public List<ManageObservations> GetFinalizedDraftObservations(int ENG_ID = 0, int OBS_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            if (ENG_ID == 0)
                ENG_ID = this.GetLoggedInUserEngId();
            List<ManageObservations> list = new List<ManageObservations>();
                cmd.CommandText = "pkg_hd.P_GetFinalizedDraftObservations";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    ManageObservations chk = new ManageObservations();
                    chk.OBS_ID = Convert.ToInt32(rdr["OBS_ID"]);
                    chk.OBS_RISK_ID = Convert.ToInt32(rdr["OBS_RISK_ID"]);
                    chk.OBS_STATUS_ID = Convert.ToInt32(rdr["OBS_STATUS_ID"]);
                    if (rdr["MEMO_NO"].ToString() != null && rdr["MEMO_NO"].ToString() != "")
                        chk.MEMO_NO = Convert.ToInt32(rdr["MEMO_NO"]);

                    if (rdr["DRAFT_PARA"].ToString() != null && rdr["DRAFT_PARA"].ToString() != "")
                        chk.DRAFT_PARA_NO = Convert.ToInt32(rdr["DRAFT_PARA"]);

                    if (rdr["FINAL_PARA"].ToString() != null && rdr["FINAL_PARA"].ToString() != "")
                        chk.FINAL_PARA_NO = Convert.ToInt32(rdr["FINAL_PARA"]);

                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.HEADING = rdr["TITLE"].ToString();
                    chk.OBS_STATUS = rdr["OBS_STATUS"].ToString();
                    chk.OBS_RISK = rdr["OBS_RISK"].ToString();
                    chk.PERIOD = rdr["PERIOD"].ToString();
                    list.Add(chk);

            return list;
        public List<ManageObservations> GetFinalizedDraftObservationsBranch(int ENG_ID = 0, int OBS_ID = 0)
            if (ENG_ID == 0)
                ENG_ID = this.GetLoggedInUserEngId();
            List<ManageObservations> list = new List<ManageObservations>();
                cmd.CommandText = "pkg_hd.P_GetFinalizedDraftObservationsbranch";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ManageObservations chk = new ManageObservations();
                    chk.OBS_ID = Convert.ToInt32(rdr["OBS_ID"]);
                    chk.OBS_RISK_ID = Convert.ToInt32(rdr["OBS_RISK_ID"]);
                    chk.OBS_STATUS_ID = Convert.ToInt32(rdr["OBS_STATUS_ID"]);
                    if (rdr["MEMO_NO"].ToString() != null && rdr["MEMO_NO"].ToString() != "")
                        chk.MEMO_NO = Convert.ToInt32(rdr["MEMO_NO"]);
                    if (rdr["DRAFT_PARA"].ToString() != null && rdr["DRAFT_PARA"].ToString() != "")
                        chk.DRAFT_PARA_NO = Convert.ToInt32(rdr["DRAFT_PARA"]);
                    if (rdr["FINAL_PARA"].ToString() != null && rdr["FINAL_PARA"].ToString() != "")
                        chk.FINAL_PARA_NO = Convert.ToInt32(rdr["FINAL_PARA"]);

                    chk.PROCESS = rdr["PROCESS"].ToString();
                    chk.SUB_PROCESS = rdr["SUB_PROCESS"].ToString();
                    chk.Checklist_Details = rdr["CHECK_LIST_DETAIL"].ToString();
                    chk.HEADING = rdr["HEADINGS"].ToString();

                    chk.AUD_REPLY = this.GetLatestAuditorResponse(chk.OBS_ID);
                    chk.HEAD_REPLY = this.GetLatestDepartmentalHeadResponse(chk.OBS_ID);
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.OBS_STATUS = rdr["OBS_STATUS"].ToString();
                    chk.OBS_RISK = rdr["OBS_RISK"].ToString();
                    chk.PERIOD = rdr["PERIOD"].ToString();
                    // chk.RESPONSIBLE_PPs = this.GetObservationResponsiblePPNOs(chk.OBS_ID);
                    list.Add(chk);

                    }

            return list;
        public List<ControlViolationsModel> GetViolationsForChecklistDetail()
            var con = this.DatabaseConnection(); con.Open();
            List<ControlViolationsModel> controlViolationList = new List<ControlViolationsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                cmd.CommandText = "pkg_fad.p_get_violations";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    ControlViolationsModel v = new ControlViolationsModel();
                    v.ID = Convert.ToInt32(rdr["S_GR_ID"]);
                    v.V_NAME = rdr["DESCRIPTION"].ToString();
                    if (rdr["MAX_NUMBER"].ToString() != null && rdr["MAX_NUMBER"].ToString() != "")
                        v.MAX_NUMBER = Convert.ToInt32(rdr["MAX_NUMBER"]);
                    v.STATUS = "Y";
                    controlViolationList.Add(v);
            con.Dispose();
            return controlViolationList;

        public int GetExpectedCountOfAuditEntitiesOnCriteria(int CRITERIA_ID)
            int count = 0;


                cmd.CommandText = "pkg_pg.P_get_Criteria_ent_count";
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = CRITERIA_ID;
                OracleDataReader rdr2 = cmd.ExecuteReader();


                while (rdr2.Read())
                    if (rdr2["NO_OF_ENTITY"].ToString() != null && rdr2["NO_OF_ENTITY"].ToString() != "")
                        count = Convert.ToInt32(rdr2["NO_OF_ENTITY"]);
            return count;
        public bool DeletePendingCriteria(int CID = 0)
            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_pg.P_DeletePendingCriteria";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = CID;
                cmd.ExecuteReader();
            return true;
        public bool SubmitAuditCriteriaForApproval(int PERIOD_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_pg.P_SubmitAuditCriteriaForApproval";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                string emailSubject = "IAS~ Notification regarding submission of Audit Criteria";
                /* string emailBody = $@"
                 Dear {userFullName},


                 Your password has been successfully reset. Please find your new login details below:

                 Username: {PPNumber}
                 Password: {pass}

                 For security reasons, we recommend that you change this password immediately after logging in.

                 If you did not request this password reset, please contact our support team immediately.


                 Best Regards,

                 Internal Audit System (IAS)
 ";
                 EmailConfiguration email = new EmailConfiguration();
                 email.ConfigEmail(userEmail, userCCEmail, emailSubject, emailBody); */

                /*cmd.CommandText = "pkg_ais_email.P_ADDAUDITCRITERIA";
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr2 = cmd.ExecuteReader();
                while (rdr2.Read())
                {
                    if (rdr2["email_to"].ToString() != "" && rdr2["email_to"].ToString() != null)
                    {
                        email_to = rdr2["email_to"].ToString();

                    }
                    if (rdr2["email_cc"].ToString() != "" && rdr2["email_cc"].ToString() != null)
                    {
                        email_cc = rdr2["email_cc"].ToString();

                    }
                    if (rdr2["subject"].ToString() != "" && rdr2["subject"].ToString() != null)
                    {
                        email_subject = rdr2["subject"].ToString();

                    }
                    if (rdr2["email_body"].ToString() != "" && rdr2["email_body"].ToString() != null)
                    {
                        email_body = rdr2["email_body"].ToString();

                    }
                    EmailConfiguration email = new EmailConfiguration();
                    email.ConfigEmail(email_to, email_cc, email_subject, email_body);
                }*/

            return true;
        public List<COSORiskModel> GetCOSORiskForDepartment(int PERIOD_ID = 0)
            List<COSORiskModel> list = new List<COSORiskModel>();
                cmd.CommandText = "pkg_ais.P_GetCOSORiskForDepartment";
                cmd.Parameters.Add("PERIOD_ID", OracleDbType.Int32).Value = PERIOD_ID;
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    COSORiskModel chk = new COSORiskModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.DEPT_NAME = rdr["DEPT_NAME"].ToString();
                    chk.RATING_FACTORS = rdr["RATING_FACTORS"].ToString();
                    chk.WEIGHT_ASSIGNED = Convert.ToInt32(rdr["WEIGHT_ASSIGNED"]);
                    chk.SUB_FACTORS = Convert.ToInt32(rdr["SUB_FACTORS"]);
                    chk.MAX_SCORE = Convert.ToInt32(rdr["MAX_SCORE"]);
                    chk.FINAL_SCORE = Convert.ToInt32(rdr["FINAL_SCORE"]);
                    chk.NO_OF_OBSERVATIONS = Convert.ToInt32(rdr["NO_OF_OBSERVATIONS"]);
                    chk.WEIGHTED_AVERAGE_SCORE = Convert.ToInt32(rdr["WEIGHTED_AVERAGE_SCORE"]);
                    chk.AUDIT_RATING = rdr["AUDIT_RATING"].ToString();
                    chk.FINAL_AUDIT_RATING = rdr["FINAL_AUDIT_RATING"].ToString();
                    chk.STATUS = rdr["STATUS"].ToString();
                    list.Add(chk);
            return list;
        public List<COSORiskModel> GetCOSORiskForBranches(int PERIOD_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<COSORiskModel> list = new List<COSORiskModel>();
                cmd.CommandText = "pkg_ais.P_GetCOSORiskForDepartment";
                cmd.Parameters.Add("PERIOD_ID", OracleDbType.Int32).Value = PERIOD_ID;
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    COSORiskModel chk = new COSORiskModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.DEPT_NAME = rdr["DEPT_NAME"].ToString();
                    chk.RATING_FACTORS = rdr["RATING_FACTORS"].ToString();
                    chk.WEIGHT_ASSIGNED = Convert.ToInt32(rdr["WEIGHT_ASSIGNED"]);
                    chk.SUB_FACTORS = Convert.ToInt32(rdr["SUB_FACTORS"]);
                    chk.MAX_SCORE = Convert.ToInt32(rdr["MAX_SCORE"]);
                    chk.FINAL_SCORE = Convert.ToInt32(rdr["FINAL_SCORE"]);
                    chk.NO_OF_OBSERVATIONS = Convert.ToInt32(rdr["NO_OF_OBSERVATIONS"]);
                    chk.WEIGHTED_AVERAGE_SCORE = Convert.ToInt32(rdr["WEIGHTED_AVERAGE_SCORE"]);
                    chk.AUDIT_RATING = rdr["AUDIT_RATING"].ToString();
                    chk.FINAL_AUDIT_RATING = rdr["FINAL_AUDIT_RATING"].ToString();
                    chk.STATUS = rdr["STATUS"].ToString();
                    list.Add(chk);
            return list;
        public CAUOMAssignmentResponseModel CAUOMAssignment(CAUOMAssignmentModel om)
            string encodedMsg = "";
            if (om.CONTENTS_OF_OM != "")
                encodedMsg = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);


            string encodedReply = "";
            if (om.CONTENTS_OF_OM != "")
                encodedReply = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);
            CAUOMAssignmentResponseModel resp = new CAUOMAssignmentResponseModel();

            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "PKG_CM.P_CAU_OM";
                cmd.Parameters.Add("OM_NO", OracleDbType.Varchar2).Value = om.OM_NO;
                cmd.Parameters.Add("ENCODED_MSG", OracleDbType.Clob).Value = encodedMsg;
                cmd.Parameters.Add("DIV_ID", OracleDbType.Int32).Value = om.DIV_ID;
                cmd.Parameters.Add("key_id", OracleDbType.Varchar2).Value = CAU_KEY;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("insp_year", OracleDbType.Int32).Value = om.INS_YEAR;

                    if (rdr["OM_ID"].ToString() != null && rdr["OM_ID"].ToString() != "")
                        resp.ID = Convert.ToInt32(rdr["OM_ID"].ToString());
                    }

                if (resp.ID > 0)
                    {
                    cmd.CommandText = "PKG_CM.P_CAU_OM_REPLY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("OM_ID", OracleDbType.Int32).Value = resp.ID;
                    cmd.Parameters.Add("OM_NO", OracleDbType.Varchar2).Value = om.OM_NO;
                    cmd.Parameters.Add("ENCODED_MSG", OracleDbType.Clob).Value = encodedReply;
                    cmd.Parameters.Add("EVIDANCE", OracleDbType.Clob).Value = "";
                    cmd.Parameters.Add("DIV_ID", OracleDbType.Int32).Value = om.DIV_ID;
                    cmd.Parameters.Add("key_id", OracleDbType.Varchar2).Value = CAU_KEY;
                    cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader rdr2 = cmd.ExecuteReader();
                    while (rdr2.Read())
                        {
                        if (rdr2["REF_OUT"].ToString() != null && rdr2["REF_OUT"].ToString() != "")
                            resp.RESPONSE = rdr2["REF_OUT"].ToString();
                        }
            return resp;
        public CAUOMAssignmentResponseModel CAUOMAssignmentAIR(CAUOMAssignmentAIRModel om)

            string encodedMsg = "";
            if (om.CONTENTS_OF_OM != "")
                encodedMsg = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);


            string encodedReply = "";
            if (om.CONTENTS_OF_OM != "")
                encodedReply = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);
            CAUOMAssignmentResponseModel resp = new CAUOMAssignmentResponseModel();
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "PKG_CM.P_CAU_AIR";
                cmd.Parameters.Add("OM_ID", OracleDbType.Int32).Value = om.OM_NO;
                cmd.Parameters.Add("PARA_NO", OracleDbType.Varchar2).Value = om.PARA_NO;
                cmd.Parameters.Add("ENCODED_MSG", OracleDbType.Clob).Value = encodedMsg;
                cmd.Parameters.Add("STAGE", OracleDbType.Int32).Value = 2;
                cmd.Parameters.Add("STATUS", OracleDbType.Int32).Value = 2;
                cmd.Parameters.Add("key_id", OracleDbType.Varchar2).Value = CAU_KEY;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    if (rdr["AIRID"].ToString() != null && rdr["AIRID"].ToString() != "")
                        resp.ID = Convert.ToInt32(rdr["AIRID"].ToString());
                if (resp.ID > 0)
                    cmd.CommandText = "PKG_CM.P_CAU_AIR_REPLY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("AIRID", OracleDbType.Int32).Value = om.OM_NO;
                    cmd.Parameters.Add("PARA_ID", OracleDbType.Varchar2).Value = om.PARA_NO;
                    cmd.Parameters.Add("ENCODED_MSG", OracleDbType.Clob).Value = encodedReply;
                    cmd.Parameters.Add("EVIDANCE", OracleDbType.Clob).Value = 2;
                    cmd.Parameters.Add("DIV_ID", OracleDbType.Int32).Value = om.DIV_ID;
                    cmd.Parameters.Add("STATUS", OracleDbType.Int32).Value = 2;
                    cmd.Parameters.Add("key_id", OracleDbType.Varchar2).Value = CAU_KEY;
                    cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    OracleDataReader rdr2 = cmd.ExecuteReader();
                    while (rdr2.Read())
                        {
                        if (rdr2["REF_OUT"].ToString() != null && rdr2["REF_OUT"].ToString() != "")
                            resp.RESPONSE = rdr2["REF_OUT"].ToString();
                        }
            return resp;
        public CAUOMAssignmentResponseModel CAUOMAssignmentPDP(CAUOMAssignmentPDPModel om)
            string encodedMsg = "";
            if (om.CONTENTS_OF_OM != "")
                encodedMsg = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);
            CAUOMAssignmentResponseModel resp = new CAUOMAssignmentResponseModel();
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "PKG_CM.T_CAU_PDP";
                cmd.Parameters.Add("DAC_DATES", OracleDbType.Date).Value = om.DAC_DATES;
                cmd.Parameters.Add("Para_id", OracleDbType.Varchar2).Value = om.PARA_ID;
                cmd.Parameters.Add("DAC_Recommendation", OracleDbType.Clob).Value = encodedMsg;
                cmd.Parameters.Add("Report_frequency", OracleDbType.Varchar2).Value = om.REPORT_FREQUENCY;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                OracleDataReader rdr2 = cmd.ExecuteReader();
                while (rdr2.Read())
                    {
                    if (rdr2["REF_OUT"].ToString() != null && rdr2["REF_OUT"].ToString() != "")
                        resp.RESPONSE = rdr2["REF_OUT"].ToString();
            return resp;
        public CAUOMAssignmentResponseModel CAUOMAssignmentARPSE(CAUOMAssignmentARPSEModel om)

            string encodedMsg = "";
            if (om.CONTENTS_OF_OM != "")
                encodedMsg = encoderDecoder.Encrypt(om.CONTENTS_OF_OM);
            CAUOMAssignmentResponseModel resp = new CAUOMAssignmentResponseModel();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "PKG_CM.T_CAU_ARPSE";
                cmd.Parameters.Add("PAC_DATES", OracleDbType.Date).Value = om.PAC_DATES;
                cmd.Parameters.Add("Para_id", OracleDbType.Varchar2).Value = om.PARA_ID;
                cmd.Parameters.Add("PAC_DIRECTIVE", OracleDbType.Clob).Value = encodedMsg;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = om.STATUS;
                cmd.Parameters.Add("aprse_year", OracleDbType.Int32).Value = om.PRINTING_DATE;
                OracleDataReader rdr2 = cmd.ExecuteReader();
                while (rdr2.Read())
                    {
                    if (rdr2["REF_OUT"].ToString() != null && rdr2["REF_OUT"].ToString() != "")
                        resp.RESPONSE = rdr2["REF_OUT"].ToString();
            return resp;
        public CAUOMAssignmentModel CAUGetPreAddedOM(string OM_NO, string INS_YEAR)
            CAUOMAssignmentModel resp = new CAUOMAssignmentModel();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "PKG_CM.P_CAU_OM_Get";
                cmd.Parameters.Add("OMNO", OracleDbType.Varchar2).Value = OM_NO;
                cmd.Parameters.Add("insp_year", OracleDbType.Varchar2).Value = INS_YEAR;
                    if (rdr["ID"].ToString() != null && rdr["ID"].ToString() != "")
                        resp.ID = Convert.ToInt32(rdr["ID"].ToString());
                    resp.DIV_ID = Convert.ToInt32(rdr["DIV_ID"].ToString());
                    resp.CONTENTS_OF_OM = rdr["CONTENTS_OF_OM"].ToString();
                    resp.CONTENTS_OF_OM = rdr["CONTENTS_OF_OM"].ToString();
            return resp;
        public List<CAUOMAssignmentModel> CAUGetAssignedOMs()
            List<CAUOMAssignmentModel> list = new List<CAUOMAssignmentModel>();
                cmd.CommandText = "pkg_cm.P_CAUGetAssignedOMs";
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    CAUOMAssignmentModel chk = new CAUOMAssignmentModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.DIV_ID = Convert.ToInt32(rdr["DIV_ID"]);
                    chk.STATUS = Convert.ToInt32(rdr["STATUS"]);
                    chk.OM_NO = rdr["OM_NO"].ToString();
                    chk.STATUS_DES = rdr["DISCRIPTION"].ToString();
                    chk.CONTENTS_OF_OM = encoderDecoder.Decrypt(rdr["CONTENTS_OF_OM"].ToString());
                    list.Add(chk);
            return list;

        public List<AuditCCQModel> GetCCQ(int ENTITY_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditCCQModel> list = new List<AuditCCQModel>();
                cmd.CommandText = "pkg_pg.P_GetCCQ";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    AuditCCQModel chk = new AuditCCQModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    if (rdr["ENTITY_ID"].ToString() != null && rdr["ENTITY_ID"].ToString() != "")
                        {
                        chk.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                        chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                        }
                        {
                        chk.ENTITY_NAME = "";

                        }

                    chk.QUESTIONS = rdr["QUESTIONS"].ToString();
                    if (rdr["CONTROL_VIOLATION_ID"].ToString() != null && rdr["CONTROL_VIOLATION_ID"].ToString() != "")
                        {
                        chk.CONTROL_VIOLATION_ID = Convert.ToInt32(rdr["CONTROL_VIOLATION_ID"]);
                        chk.CONTROL_VIOLATION = rdr["VIOLATION_NAME"].ToString();

                        }
                    else
                        {
                        chk.CONTROL_VIOLATION = "";
                        }
                    if (rdr["RISK_ID"].ToString() != null && rdr["RISK_ID"].ToString() != "")
                        {
                        chk.RISK_ID = Convert.ToInt32(rdr["RISK_ID"].ToString());
                        chk.RISK = rdr["RISK_DEF"].ToString();
                        }
                    else
                        {
                        chk.RISK = "";
                        }

                    chk.STATUS = rdr["STATUS"].ToString();
                    list.Add(chk);
            return list;
        public bool UpdateCCQ(AuditCCQModel ccq)
            bool resp = false;
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_pg.P_UpdateCCQ";
                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = ccq.ID;
                cmd.Parameters.Add("QUESTIONS", OracleDbType.Varchar2).Value = ccq.QUESTIONS;
                cmd.Parameters.Add("CONTROL_VIOLATION_ID", OracleDbType.Int32).Value = ccq.CONTROL_VIOLATION_ID;
                cmd.Parameters.Add("RISK_ID", OracleDbType.Int32).Value = ccq.RISK_ID;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = ccq.STATUS;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                resp = true;
            return resp;
        public bool AuditeeOldParaResponse(AuditeeOldParasResponseModel ob)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            bool success = false;
            var loggedInUser = sessionHandler.GetSessionUser();
            ob.REPLIEDBY = Convert.ToInt32(loggedInUser.PPNumber);
                cmd.CommandText = "pkg_ais.P_AuditeeOldParaResponse";
                cmd.Parameters.Add("OBSID", OracleDbType.Int32).Value = ob.AU_OBS_ID;
                cmd.Parameters.Add("REPLY", OracleDbType.Clob).Value = ob.REPLY;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                success = true;
            return success;
        public List<OldParasModel> GetOldParas(string AUDITED_BY, string AUDIT_YEAR)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetOldParas";
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = AUDITED_BY;
                cmd.Parameters.Add("AUDITPERIOD", OracleDbType.Int32).Value = AUDIT_YEAR;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.TYPE_ID = rdr["TYPE_ID"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ANNEXURE = rdr["ANNEXURE"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = rdr["VOL_I_II"].ToString();
                    chk.AUDITED_BY = rdr["AUDITED_BY"].ToString();
                    chk.AUDITEDBY = rdr["AUDITEDBY"].ToString();
                    list.Add(chk);
            return list;
        public List<OldParasModel> GetOldSettledParasForResponse(int ENTITY_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetOldParasForResponse";
                cmd.Parameters.Add("UserEntityId", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.NEW_PARA_ID = rdr["AU_OBS_ID"].ToString();
                    chk.OLD_PARA_ID = rdr["REF_P"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = "";
                    chk.IND = rdr["IND"].ToString();
                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString();
                    list.Add(chk);
            return list;
        public List<OldParasModel> GetCurrentParasForStatusChangeRequest(int ENTITY_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetnewParasForResponse";
                cmd.Parameters.Add("UserEntityId", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ENTITY_ID;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["gist_of_para"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = "";
                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString();
                    list.Add(chk);
            return list;
        public List<OldParasModel> GetCurrentParasForStatusChangeRequestReview()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetnewParasForResponse_reviewer";
                cmd.Parameters.Add("UserEntityId", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["gist_of_para"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.MAKER_REMARKS = rdr["remarks"].ToString();
                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString() == "6" ? "Settled" : "Un-settled";
                    list.Add(chk);
        public List<OldParasModel> GetCurrentParasForStatusChangeRequestAuthorize()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_fad.p_GetnewParasForResponseAuthorize";
                cmd.Parameters.Add("UserEntityId", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.PARA_RISK = rdr["PARA_RISK"].ToString();
                    chk.GIST_OF_PARAS = rdr["gist_of_para"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.MAKER_REMARKS = rdr["remarks"].ToString();
                    chk.REVIEWER_REMARKS = rdr["reviewer_comments"].ToString();
                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString() == "6" ? "Settled" : "Un-settled";
                    list.Add(chk);
            return list;
        public List<OldParasModel> GetManageLegacyParas()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetOldParas";
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.TYPE_ID = rdr["TYPE_ID"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ANNEXURE = rdr["ANNEXURE"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = rdr["VOL_I_II"].ToString();
                    chk.AUDITED_BY = rdr["AUDITED_BY"].ToString();
                    chk.AUDITEDBY = rdr["AUDITEDBY"].ToString();
                    list.Add(chk);
            return list;
        public List<AuditeeOldParasModel> GetCurrentParasEntitiesForStatusChange()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
                cmd.CommandText = "pkg_hd.P_GetEntitiesForNewPara";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    AuditeeOldParasModel chk = new AuditeeOldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    chk.ENG_ID = rdr["ENG_ID"].ToString();
                    chk.ENTITY_NAME = rdr["NAME"].ToString();

                    list.Add(chk);
            return list;
        public List<AuditeeOldParasModel> GetLegacyParasEntitiesFAD()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
                cmd.CommandText = "pkg_FAD.P_GetEntitiesForLegacyPara";
                cmd.Parameters.Add("PP_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    AuditeeOldParasModel chk = new AuditeeOldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    chk.ENTITY_NAME = rdr["NAME"].ToString();

                    list.Add(chk);
            return list;
        public List<AuditeeOldParasModel> GetSettledParasEntitiesForMonitoringFAD()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
                cmd.CommandText = "pkg_FAD.P_GET_SETTLED_PARA_ENTITIES";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    AuditeeOldParasModel chk = new AuditeeOldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    chk.ENTITY_NAME = rdr["NAME"].ToString();

                    list.Add(chk);
            return list;
        public List<OldParasModel> GetLegacyParasForUpdateFAD(int ENTITY_ID, string PARA_REF = "", int PARA_ID = 0)
            var loggedInUser = sessionHandler.GetSessionUser();
            List<OldParasModel> list = new List<OldParasModel>();

                cmd.CommandText = "pkg_fad.P_GetLeagacyObservations";
                cmd.Parameters.Add("entityId", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("paraRef", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    OldParasModel chk = new OldParasModel();
                    chk.ID = Convert.ToInt32(rdr["ID"]);
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.RISK_ID = rdr["RISK"].ToString();
                    chk.ENTITY_CODE = rdr["ENTITY_CODE"].ToString();
                    chk.TYPE_ID = rdr["TYPE_ID"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    if (PARA_REF != null)
                        {
                        chk.PROCESS = Convert.ToInt32(rdr["PROCESS"].ToString());
                        chk.SUB_PROCESS = Convert.ToInt32(rdr["SUB_PROCESS"].ToString());
                        chk.PROCESS_DETAIL = Convert.ToInt32(rdr["PROCESS_DETAIL"].ToString());
                        chk.PARA_TEXT = rdr["PARA_TEXT"].ToString();


                        }

                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ANNEXURE = rdr["ANNEXURE"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = rdr["VOL_I_II"].ToString();

                    if (PARA_REF != null)
                        chk.PARA_RESP = this.GetLegacyParaResponsiblePersonsFAD(PARA_REF);
                    list.Add(chk);



            return list;
        public string AddResponsibilityToLegacyParasFAD(ObservationResponsiblePPNOModel RESP_PP, string REF_P, int P_ID)
            string responseRes = "";
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_fad.p_add_para_responsibility";
                cmd.Parameters.Add("refid", OracleDbType.Int32).Value = P_ID;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = RESP_PP.PP_NO;
                cmd.Parameters.Add("AZ_Entity_id", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("user_ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("lC_no", OracleDbType.Varchar2).Value = RESP_PP.LOAN_CASE;
                cmd.Parameters.Add("LC_AMOUNT", OracleDbType.Varchar2).Value = RESP_PP.LC_AMOUNT;
                cmd.Parameters.Add("AC_NO", OracleDbType.Varchar2).Value = RESP_PP.ACCOUNT_NUMBER;
                cmd.Parameters.Add("AC_AMOUNT", OracleDbType.Varchar2).Value = RESP_PP.ACC_AMOUNT;
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = REF_P;
                OracleDataReader rdr2 = cmd.ExecuteReader();
                while (rdr2.Read())
                    responseRes = rdr2["REMARKS"].ToString();


            return responseRes;
        public string UpdateLegacyParasWithResponsibilityNoChanges(AddLegacyParaModel LEGACY_PARA)
            string resp = "";
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_fad.P_reviewed_legacy_Para";
                cmd.Parameters.Add("ref_id", OracleDbType.Varchar2).Value = LEGACY_PARA.REF_P;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    if (rdr["REF"].ToString() != "" && rdr["REF"].ToString() != null && rdr["REF"].ToString() == "2")
                        {
                        resp = rdr["REMARKS"].ToString();
                        return resp;
                        }
                    else
                        {
                        resp = rdr["REMARKS"].ToString();
                        }

            return resp;
        public string UpdateLegacyParasWithResponsibilityFAD(AddLegacyParaModel LEGACY_PARA)
            string resp = "";
            string responseRes = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_fad.P_update_legacy_Para_text";
                cmd.Parameters.Add("ref_id", OracleDbType.Varchar2).Value = LEGACY_PARA.REF_P;
                cmd.Parameters.Add("obtext", OracleDbType.Clob).Value = LEGACY_PARA.PARA_TEXT;
                cmd.Parameters.Add("process_id", OracleDbType.Int32).Value = LEGACY_PARA.PROCESS_ID;
                cmd.Parameters.Add("subprocessid", OracleDbType.Int32).Value = LEGACY_PARA.SUB_PROCESS_ID;
                cmd.Parameters.Add("checklistid", OracleDbType.Int32).Value = LEGACY_PARA.CHECKLIST_DETAIL_ID;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("risk_id", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    if (rdr["REF"].ToString() != "" && rdr["REF"].ToString() != null && rdr["REF"].ToString() == "2")
                        {
                        resp = rdr["REMARKS"].ToString();
                        return resp;
                        }
                    else
                        {
                        resp = rdr["REMARKS"].ToString();
                        }

                    }
                if (LEGACY_PARA.RESP_PP != null)
                    {
                    if (LEGACY_PARA.RESP_PP.Count > 0)
                        {
                        foreach (ObservationResponsiblePPNOModel respRow in LEGACY_PARA.RESP_PP)
                            {
                            responseRes = "";
                            cmd.CommandText = "pkg_fad.p_add_para_responsibility";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("refid", OracleDbType.Int32).Value = LEGACY_PARA.ID;
                            cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = respRow.PP_NO;
                            cmd.Parameters.Add("AZ_Entity_id", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                            cmd.Parameters.Add("user_ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                            cmd.Parameters.Add("lC_no", OracleDbType.Varchar2).Value = respRow.LOAN_CASE;
                            cmd.Parameters.Add("LC_AMOUNT", OracleDbType.Varchar2).Value = respRow.LC_AMOUNT;
                            cmd.Parameters.Add("AC_NO", OracleDbType.Varchar2).Value = respRow.ACCOUNT_NUMBER;
                            cmd.Parameters.Add("AC_AMOUNT", OracleDbType.Varchar2).Value = respRow.ACC_AMOUNT;
                            cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = LEGACY_PARA.REF_P;
                            cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                            OracleDataReader rdr2 = cmd.ExecuteReader();
                            while (rdr2.Read())
                                {
                                responseRes = rdr2["REMARKS"].ToString();

                                }
                            }
                        }

            return resp + "<br/>" + responseRes;
        public List<AuditeeOldParasModel> GetOutstandingParas(string ENTITY_ID)
            {
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
            return list;
            }
        public List<OldParasModel> GetOldParasAuditYear()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<OldParasModel> list = new List<OldParasModel>();

                cmd.CommandText = "pkg_ais.P_GetOldParasAuditYear";
                    OldParasModel chk = new OldParasModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    list.Add(chk);
            return list;
        public List<OldParasModel> GetOutstandingParasAuditYear()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<OldParasModel> list = new List<OldParasModel>();

                cmd.CommandText = "pkg_ais.P_GetOutstandingParasAuditYear";
                    OldParasModel chk = new OldParasModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    list.Add(chk);
            return list;
        public bool AddOldParas(OldParasModel jm)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

                List<int> PP_NOs = new List<int>();
                jm.STATUS = 1;
                jm.ENTERED_BY = loggedInUser.PPNumber;
                if (jm.RESPONSIBLE_PP_NO != "" && jm.RESPONSIBLE_PP_NO != null)
                    {
                    PP_NOs = jm.RESPONSIBLE_PP_NO.Split(',').Select(int.Parse).ToList();
                    }
                cmd.CommandText = "pkg_hd.P_AddOldParas";
                cmd.Parameters.Add("PROCESS", OracleDbType.Int32).Value = jm.PROCESS;
                cmd.Parameters.Add("SUBPROCESS", OracleDbType.Int32).Value = jm.SUB_PROCESS;
                cmd.Parameters.Add("PROCESSDETAIL", OracleDbType.Int32).Value = jm.PROCESS_DETAIL;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("REPLYTEXT", OracleDbType.Clob).Value = jm.PARA_TEXT;
                cmd.Parameters.Add("PID", OracleDbType.Clob).Value = jm.ID;
                cmd.ExecuteReader();
                foreach (int pp in PP_NOs)
                    cmd.CommandText = "pkg_ais.P_AddOldParasResponsibilityAssigned";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add("REF_P", OracleDbType.Int32).Value = jm.ID;
                    cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = pp;
                    cmd.ExecuteReader();
            return true;
        public bool UpdateOldParasStatus(int ID, int NEW_STATUS)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            bool success = false;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_UpdateOldParasFadsettleunsettle";
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("PID", OracleDbType.Int32).Value = ID;
                cmd.Parameters.Add(" NEW_STATUS ", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.ExecuteReader();
                success = true;
            return success;
        public List<UserWiseOldParasPerformanceModel> GetUserWiseOldParasPerformance()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<UserWiseOldParasPerformanceModel> list = new List<UserWiseOldParasPerformanceModel>();

                cmd.CommandText = "pkg_ais.P_GetUserWiseOldParasPerformance";
                cmd.Parameters.Add("UserEntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    UserWiseOldParasPerformanceModel chk = new UserWiseOldParasPerformanceModel();
                    chk.AUDIT_ZONEID = rdr["AUDIT_ZONEID"].ToString();
                    chk.ZONENAME = rdr["ZONENAME"].ToString();
                    chk.PARA_ENTERED = rdr["PARA_ENTERED"].ToString();
                    chk.PPNO = rdr["PPNO"].ToString();
                    list.Add(chk);
            return list;
        public List<AuditeeEntitiesModel> GetObservationEntities()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeEntitiesModel> list = new List<AuditeeEntitiesModel>();
                cmd.CommandText = "pkg_hd.P_get_audit_pre_Concluding_entities";
                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    AuditeeEntitiesModel chk = new AuditeeEntitiesModel();
                    chk.CODE = Convert.ToInt32(rdr["CODE"].ToString());
                    chk.NAME = rdr["entity_name"].ToString();
                    chk.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"].ToString());
                    chk.ENG_ID = Convert.ToInt32(rdr["eng_id"].ToString());
                    chk.TYPE_ID = Convert.ToInt32(rdr["TYPE_ID"].ToString());
                    list.Add(chk);
            return list;
        public List<AuditeeEntitiesModel> GetObservationEntitiesForPreConcluding(int pageId = 0)
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeEntitiesModel> list = new List<AuditeeEntitiesModel>();
                cmd.CommandText = "pkg_hd.P_get_audit_pre_Concluding_entities";
                cmd.Parameters.Add("userentityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                //cmd.Parameters.Add("PAGE_ID", OracleDbType.Int32).Value = pageId;

                    AuditeeEntitiesModel chk = new AuditeeEntitiesModel();
                    chk.NAME = rdr["ENTITY_NAME"].ToString();
                    chk.ENG_ID = Convert.ToInt32(rdr["ENG_ID"].ToString());
                    //  chk.TYPE_ID = Convert.ToInt32(rdr["TYPE_ID"].ToString());
                    list.Add(chk);
            return list;

        public List<StaffPositionModel> GetStaffPosition()

            List<StaffPositionModel> list = new List<StaffPositionModel>();
                cmd.CommandText = "pkg_ai.P_GetStaffPosition";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;


                    StaffPositionModel staffposition = new StaffPositionModel();
                    staffposition.PPNO = Convert.ToInt32(rdr["PPNO"]);
                    staffposition.EMPLOYEE_NAME = Convert.ToString(rdr["EMPLOYEE_NAME"]);

                    staffposition.QUALIFICATION = Convert.ToString(rdr["QUALIFICATION"]);
                    staffposition.DATE_OF_POSTING = Convert.ToDateTime(rdr["DATE_OF_POSTING"]);
                    staffposition.DESIGNATION = Convert.ToString(rdr["DESIGNATION"]);
                    staffposition.RANK_DESC = Convert.ToString(rdr["RANK_DESC"]);
                    staffposition.PLACE_OF_POSTING = Convert.ToString(rdr["PLACE_OF_POSTING"]);


                    list.Add(staffposition);
            return list;
        public bool AddDivisionalHeadRemarksOnFunctionalLegacyPara(int CONCERNED_DEPT_ID = 0, string COMMENTS = "", int REF_PARA_ID = 0)
                cmd.CommandText = "pkg_ais.P_AddDivisionalHeadRemarksOnFunctionalLegacyPara";
                cmd.Parameters.Add("CONCERNED_DEPTID", OracleDbType.Int32).Value = CONCERNED_DEPT_ID;
                cmd.Parameters.Add("COMMENTS", OracleDbType.Varchar2).Value = COMMENTS;
                cmd.Parameters.Add("REF_PARAID", OracleDbType.Int32).Value = REF_PARA_ID;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
            return true;
        [Obsolete]
        public void SaveImage(string base64img, string outputImgFilename = "image.jpg")
            var folderPath = System.IO.Path.Combine(_env.WebRootPath, "Auditee_Evidences");
            if (!System.IO.Directory.Exists(folderPath))
                System.IO.Directory.CreateDirectory(folderPath);
            System.IO.File.WriteAllBytes(Path.Combine(folderPath, outputImgFilename), Convert.FromBase64String(base64img));
        [Obsolete]
        public void DeleteImage(string Filename = "image.jpg")
            var filePath = System.IO.Path.Combine(_env.WebRootPath, "Auditee_Evidences", Filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        public string CreateAuditReport(int ENG_ID)
            {
            List<ManageObservations> list = new List<ManageObservations>();
            string filename = "";
            return filename;

            /*list = this.GetManagedObservations(ENG_ID, 0);
            var folderPath = "";
            string entityname = list[0].ENTITY_NAME;
            string period = list[0].PERIOD;
            using (MemoryStream mem = new MemoryStream())
            {
                StringBuilder sb = new StringBuilder();
                //Table For Practice
                sb.Append(@"<center><h1><u>Audit Report on " + entityname + " </u></h1><h3>" + period + "</h3><h3>Version: Draft</h3></center>");

                sb.Append(@"<br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><br/><h1>Audit Observations</h1>");



                foreach(var item in list)
                {
                    List<object> outText = new List<object>();

                    outText=this.GetObservationText(item.OBS_ID,0);
                    sb.Append("<h3 style='margin-top:50px;'>Memo No : "+item.MEMO_NO+"</h3>");
                    sb.Append("<div style='margin-top:10px;'>"+ outText [0]+ "</div>");
                    sb.Append("<h3 style='margin-top:10px;'>Auditee Reply</h3>");
                    sb.Append("<div style='margin-top:10px;'>" + outText[1] + "</div>");

                }              

               
                string path = "";
               
                //ltTable.Text = sb.ToString();
                folderPath = System.IO.Path.Combine(_env.WebRootPath, "Audit_Reports");
                if (!System.IO.Directory.Exists(folderPath))
                {
                    System.IO.Directory.CreateDirectory(folderPath);
                }
                filename = "DraftReport_" + ENG_ID + ".Pdf"; ;
                //path = Path.Combine(contentRootPath, filename + ".Pdf");
                path = Path.Combine(folderPath, filename);

                PdfWriter writer = new PdfWriter(path);
                PdfDocument pdf = new PdfDocument(writer);
                pdf.SetDefaultPageSize(iText.Kernel.Geom.PageSize.A0);                

                ConverterProperties converterProperties = new ConverterProperties();
                PdfDocument pdfDocument = new PdfDocument(writer);
                
                iText.Layout.Document document = HtmlConverter.ConvertToDocument(sb.ToString(), pdfDocument, converterProperties);



                var xmlParse = new XMLParser();
                xmlParse.Parse(new StringReader(sb.ToString()));
                xmlParse.Flush();

                document.Close();

                
            }
            return filename;
            */
            }
        public List<Glheadsummaryyearlymodel> GetGlheadDetailsyearwise(int engId = 0, int gl_code = 0)
            int ENG_ID = this.GetLoggedInUserEngId();
            var con = this.DatabaseConnection(); con.Open();


            List<Glheadsummaryyearlymodel> list = new List<Glheadsummaryyearlymodel>();

                cmd.CommandText = "pkg_ai.p_getglheadsummary_Yearly";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = engId;

                    Glheadsummaryyearlymodel GlHeadDetails = new Glheadsummaryyearlymodel();
                    GlHeadDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    GlHeadDetails.BRANCHID = Convert.ToInt32(rdr["BRANCHID"]);
                    GlHeadDetails.GLSUBNAME = rdr["GLSUBNAME"].ToString();

                    if (rdr["BALANCE_2021"].ToString() != null && rdr["BALANCE_2021"].ToString() != "")
                        GlHeadDetails.BALANCE_2021 = Convert.ToDouble(rdr["BALANCE_2021"]);
                    if (rdr["DEBIT_2021"].ToString() != null && rdr["DEBIT_2021"].ToString() != "")
                        GlHeadDetails.DEBIT_2021 = Convert.ToDouble(rdr["DEBIT_2021"]);
                    if (rdr["CREDIT_2021"].ToString() != null && rdr["CREDIT_2021"].ToString() != "")
                        GlHeadDetails.CREDIT_2021 = Convert.ToDouble(rdr["CREDIT_2021"]);
                    if (rdr["BALANCE_2022"].ToString() != null && rdr["BALANCE_2022"].ToString() != "")
                        GlHeadDetails.BALANCE_2022 = Convert.ToDouble(rdr["BALANCE_2022"]);
                    if (rdr["DEBIT_2022"].ToString() != null && rdr["DEBIT_2022"].ToString() != "")
                        GlHeadDetails.DEBIT_2022 = Convert.ToDouble(rdr["DEBIT_2022"]);
                    if (rdr["CREDIT_2022"].ToString() != null && rdr["CREDIT_2022"].ToString() != "")
                        GlHeadDetails.CREDIT_2022 = Convert.ToDouble(rdr["CREDIT_2022"]);

                    GlHeadDetails.COL1 = rdr["COL1"].ToString();
                    GlHeadDetails.COL2 = rdr["COL2"].ToString();
                    GlHeadDetails.COL3 = rdr["COL3"].ToString();

                    GlHeadDetails.LAST_CREDIT = rdr["LAST_CREDIT"].ToString();
                    GlHeadDetails.LAST_DEBIT = rdr["LAST_DEBIT"].ToString();
                    GlHeadDetails.LAST_BALANCE = rdr["LAST_BALANCE"].ToString();

                    GlHeadDetails.CURRENT_CREDIT = rdr["CURRENT_CREDIT"].ToString();
                    GlHeadDetails.CURRENT_DEBIT = rdr["CURRENT_DEBIT"].ToString();
                    GlHeadDetails.CURRENT_BALANCE = rdr["CURRENT_BALANCE"].ToString();

                    list.Add(GlHeadDetails);
            return list;

        public List<DepositAccountCatModel> GetDepositCat()
            int ENG_ID = this.GetLoggedInUserEngId();

            var con = this.DatabaseConnection(); con.Open();
            List<DepositAccountCatModel> list = new List<DepositAccountCatModel>();

                cmd.CommandText = "pkg_AI.P_GetDepositACCOUNTCATEGORY";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;

                    DepositAccountCatModel depcat = new DepositAccountCatModel();

                    depcat.BRANCH_NAME = rdr["BRANCH_NAME"].ToString();
                    depcat.ACCOUNTCATEGORY = rdr["ACCOUNTCATEGORY"].ToString();
                    depcat.ACCOUNTCATEGORYID = Convert.ToInt32(rdr["ACCOUNTCATEGORYID"]);

                    depcat.ACCOCUNTSTATUS = rdr["ACCOCUNTSTATUS"].ToString();

                    if (rdr["AMOUNT"].ToString() != null && rdr["AMOUNT"].ToString() != "")
                        depcat.AMOUNT = Convert.ToDouble(rdr["AMOUNT"]);

                    list.Add(depcat);
            return list;

        public List<DepositAccountCatDetailsModel> GetDepositAccountcatdetails(int catid = 0)

            int ENG_ID = this.GetLoggedInUserEngId();
            var con = this.DatabaseConnection(); con.Open();
            List<DepositAccountCatDetailsModel> depositaccsublist = new List<DepositAccountCatDetailsModel>();
                cmd.CommandText = "pkg_AIS.P_GetDepositACCOUNTCATEGORY_details";
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("catid", OracleDbType.Int32).Value = catid;

                    DepositAccountCatDetailsModel depositaccsubdetails = new DepositAccountCatDetailsModel();

                    depositaccsubdetails.BRANCH_NAME = rdr["BRANCH_NAME"].ToString();
                    if (rdr["ACC_NUMBER"].ToString() != null && rdr["ACC_NUMBER"].ToString() != "")
                        depositaccsubdetails.ACC_NUMBER = Convert.ToDouble(rdr["ACC_NUMBER"]);
                    if (rdr["ACCOUNTCATEGORY"].ToString() != null && rdr["ACCOUNTCATEGORY"].ToString() != "")
                        depositaccsubdetails.ACCOUNTCATEGORY = rdr["ACCOUNTCATEGORY"].ToString();

                    if (rdr["CUSTOMERNAME"].ToString() != null && rdr["CUSTOMERNAME"].ToString() != "")
                        depositaccsubdetails.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    if (rdr["BMVS_VERIFIED"].ToString() != null && rdr["BMVS_VERIFIED"].ToString() != "")
                        depositaccsubdetails.BMVS_VERIFIED = rdr["BMVS_VERIFIED"].ToString();


                    if (rdr["OPENINGDATE"].ToString() != null && rdr["OPENINGDATE"].ToString() != "")
                        {
                        depositaccsubdetails.OPENINGDATE = Convert.ToDateTime(rdr["OPENINGDATE"]);
                        }
                    if (rdr["CNIC"].ToString() != null && rdr["CNIC"].ToString() != "")
                        {
                        depositaccsubdetails.CNIC = Convert.ToDouble(rdr["CNIC"]);
                        }
                    if (rdr["TITLE"].ToString() != null && rdr["TITLE"].ToString() != "")
                        depositaccsubdetails.TITLE = rdr["TITLE"].ToString();


                    if (rdr["ACCOCUNTSTATUS"].ToString() != null && rdr["ACCOCUNTSTATUS"].ToString() != "")
                        depositaccsubdetails.ACCOUNTSTATUS = rdr["ACCOCUNTSTATUS"].ToString();
                    if (rdr["LASTTRANSACTIONDATE"].ToString() != null && rdr["LASTTRANSACTIONDATE"].ToString() != "")
                        {
                        depositaccsubdetails.LASTTRANSACTIONDATE = Convert.ToDateTime(rdr["LASTTRANSACTIONDATE"]);
                        }
                    if (rdr["CNICEXPIRYDATE"].ToString() != null && rdr["CNICEXPIRYDATE"].ToString() != "")
                        {
                        depositaccsubdetails.CNICEXPIRYDATE = Convert.ToDateTime(rdr["CNICEXPIRYDATE"]);
                        }
                    depositaccsublist.Add(depositaccsubdetails);
            return depositaccsublist;
        public List<LoanSchemeModel> GetLoansScheme(int engId)
            int ENG_ID = this.GetLoggedInUserEngId();

            List<LoanSchemeModel> list = new List<LoanSchemeModel>();
            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_ai.P_preauditinfo_loan_scheme";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = engId;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;


                    LoanSchemeModel LoanSchemeDetails = new LoanSchemeModel();

                    LoanSchemeDetails.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    //LoanSchemeDetails.DISB_STATUSID = Convert.ToInt32(rdr["DISB_STATUSID"]);
                    LoanSchemeDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    LoanSchemeDetails.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    LoanSchemeDetails.DISBURSED_AMOUNT = Convert.ToDouble(rdr["DISBURSED_AMOUNT"]);



                    LoanSchemeDetails.PRIN_OUT = Convert.ToDouble(rdr["PRIN_OUT"]);
                    LoanSchemeDetails.MARKUP_OUT = Convert.ToDouble(rdr["MARKUP_OUT"]);



                    list.Add(LoanSchemeDetails);
            return list;
        public List<LoanSchemeYearlyModel> GetLoansSchemeYearly(int engId)
            int ENG_ID = this.GetLoggedInUserEngId();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<LoanSchemeYearlyModel> list = new List<LoanSchemeYearlyModel>();
                cmd.CommandText = "pkg_ai.P_preauditinfo_loan_scheme_yearly";
                cmd.Parameters.Add("ENG_ID", OracleDbType.Int32).Value = engId;
                cmd.Parameters.Add("PPNumber", OracleDbType.Int32).Value = loggedInUser.PPNumber;


                    LoanSchemeYearlyModel LoanSchemeDetails = new LoanSchemeYearlyModel();

                    LoanSchemeDetails.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    LoanSchemeDetails.DISB_STATUSID = Convert.ToInt32(rdr["DISB_STATUSID"]);
                    LoanSchemeDetails.GLSUBCODE = Convert.ToInt32(rdr["GLSUBCODE"]);
                    LoanSchemeDetails.GLSUBNAME = rdr["GLSUBNAME"].ToString();
                    if (rdr["DISBURSED_AMOUNT_2021"].ToString() != null && rdr["DISBURSED_AMOUNT_2021"].ToString() != "")
                        LoanSchemeDetails.DISBURSED_AMOUNT_2021 = Convert.ToDouble(rdr["DISBURSED_AMOUNT_2021"]);


                    if (rdr["PRIN_OUT_2021"].ToString() != null && rdr["PRIN_OUT_2021"].ToString() != "")
                        LoanSchemeDetails.PRIN_OUT_2021 = Convert.ToDouble(rdr["PRIN_OUT_2021"]);
                    if (rdr["MARKUP_OUT_2021"].ToString() != null && rdr["MARKUP_OUT_2021"].ToString() != "")
                        LoanSchemeDetails.MARKUP_OUT_2021 = Convert.ToDouble(rdr["MARKUP_OUT_2021"]);
                    if (rdr["DISBURSED_AMOUNT_2022"].ToString() != null && rdr["DISBURSED_AMOUNT_2022"].ToString() != "")
                        LoanSchemeDetails.DISBURSED_AMOUNT_2022 = Convert.ToDouble(rdr["DISBURSED_AMOUNT_2022"]);


                    if (rdr["PRIN_OUT_2022"].ToString() != null && rdr["PRIN_OUT_2022"].ToString() != "")
                        LoanSchemeDetails.PRIN_OUT_2022 = Convert.ToDouble(rdr["PRIN_OUT_2022"]);
                    if (rdr["MARKUP_OUT_2022"].ToString() != null && rdr["MARKUP_OUT_2022"].ToString() != "")
                        LoanSchemeDetails.MARKUP_OUT_2022 = Convert.ToDouble(rdr["MARKUP_OUT_2022"]);
                    list.Add(LoanSchemeDetails);
            return list;
        public List<FadOldParaReportModel> GetFadBranchesParas(int PROCESS_ID = 0, int SUB_PROCESS_ID = 0, int PROCESS_DETAIL_ID = 0)
            List<FadOldParaReportModel> list = new List<FadOldParaReportModel>();
                cmd.CommandText = "PKG_rpt.r_functionalresp";

                cmd.Parameters.Add("CID", OracleDbType.Int32).Value = PROCESS_ID;
                cmd.Parameters.Add("SID", OracleDbType.Int32).Value = SUB_PROCESS_ID;
                cmd.Parameters.Add("CDID", OracleDbType.Int32).Value = PROCESS_DETAIL_ID;

                    FadOldParaReportModel para = new FadOldParaReportModel();

                    para.PERIOD = Convert.ToInt32(rdr["PERIOD"].ToString());
                    para.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    para.PROCESS = rdr["PROCESS"].ToString();
                    para.SUB_PROCESS = rdr["SUB_PROCESS"].ToString();
                    para.VIOLATION = rdr["VIOLATION"].ToString();
                    para.OBS_TEXT = rdr["OBS_TEXT"].ToString();
                    para.OBS_RISK = rdr["OBS_RISK"].ToString();
                    para.OBS_STATUS = rdr["OBS_STATUS"].ToString();
                    list.Add(para);
            return list;
        public DraftReportSummaryModel GetDraftReportSummary(int ENG_ID = 0, int OBS_ID = 0)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ManageObservations> paras = new List<ManageObservations>();
            DraftReportSummaryModel list = new DraftReportSummaryModel();

            if (loggedInUser.UserLocationType == "Z")
                paras = this.GetManagedObservationsForBranches(ENG_ID, OBS_ID);
            else
                paras = this.GetManagedObservations(ENG_ID, OBS_ID);
                }
            foreach (var p in paras)
                {
                list.Total++;
                if (p.OBS_STATUS_ID == 7)
                    list.Dropped++;
                if (p.OBS_STATUS_ID == 5)
                    list.AddtoDraft++;
                if (p.OBS_STATUS_ID == 4)
                    list.Settled++;
                if (p.OBS_RISK_ID == 3)
                    list.Low++;
                if (p.OBS_RISK_ID == 2)
                    list.Medium++;
                if (p.OBS_RISK_ID == 1)
                    list.High++;

            return list;

        public List<SettledPostCompliancesModel> GetSettledPostCompliancesForMonitoring(string MONTH_NAME, string YEAR)
            List<SettledPostCompliancesModel> list = new List<SettledPostCompliancesModel>();
                cmd.CommandText = "pkg_hd.P_GetSettledParasForReview";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("MON", OracleDbType.Varchar2).Value = MONTH_NAME;
                cmd.Parameters.Add("YR", OracleDbType.Varchar2).Value = YEAR;

                    SettledPostCompliancesModel chk = new SettledPostCompliancesModel();
                    chk.COM_ID = rdr["COM_ID"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.ENTITY_NAME = rdr["NAME"].ToString();
                    chk.COM_KEY = rdr["COM_KEY"].ToString();
                    chk.NEW_PARA_ID = rdr["NEW_PARAID"].ToString() == "" ? 0 : Convert.ToInt32(rdr["NEW_PARAID"].ToString());
                    chk.OLD_PARA_ID = rdr["old_para_id"].ToString() == "" ? 0 : Convert.ToInt32(rdr["old_para_id"].ToString());

                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString();
                    chk.INDICATOR = rdr["ind"].ToString();
                    chk.PARA_RISK = rdr["rsk"].ToString();
                    chk.GIST_OF_PARAS = rdr["gist_of_paras"].ToString();
                    chk.AUDIT_PERIOD = rdr["audit_period"].ToString();
                    chk.PARA_NO = rdr["para_no"].ToString();
                    chk.SETTLED_ON = rdr["STELLED_ON"].ToString();
                    chk.COM_STAGE = rdr["COM_STAGE"].ToString();
                    chk.COM_STATUS = rdr["COM_STATUS"].ToString();
                    chk.COM_CYCLE = rdr["COM_CYCLE"].ToString();

                    chk.COMPLIANCE_UNIT = rdr["COM_UNIT"].ToString();
                    chk.COMPLIANCE_SETTLEMENT_OFFICER = rdr["SETTLED_BY"].ToString();
                    chk.COMPLIANCE_UNIT_INCHARGE = rdr["REVIEWED_BY"].ToString();

                    chk.NEXT_R_ID = "";
                    chk.PREV_R_ID = "";
                    chk.PREV_ROLE = "";
                    chk.NEXT_ROLE = "";


                    list.Add(chk);

            return list;

        public GetOldParasBranchComplianceTextModel GetOldParasBranchComplianceTextForImpIncharge(int PID, string Ref_P, string PARA_CATEGORY, string REPLY_DATE, string OBS_ID)
            GetOldParasBranchComplianceTextModel chk = new GetOldParasBranchComplianceTextModel();
                cmd.CommandText = "pkg_hd.P_GetOldParasforsettlementext";
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("pid", OracleDbType.Int32).Value = PID;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("refP", OracleDbType.Varchar2).Value = Ref_P;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;

                    chk.PARA_TEXT = rdr["para_text"].ToString();
                    chk.GIST_OF_PARA = rdr["gist_of_paras"].ToString();
                    //chk.RESPONSIBLE_PPs = this.GetOldParasObservationResponsiblePPNOs(Ref_P, chk.PARA_CATEGORY);
                    //chk.EVIDENCES = this.GetOldParasEvidences(Ref_P, chk.PARA_CATEGORY, REPLY_DATE, OBS_ID);
            return chk;
        public GetOldParasBranchComplianceTextModel GetOldParasReferredBackBranchComplianceTextForImpIncharge(int PID, string Ref_P, string PARA_CATEGORY, string REPLY_DATE, string OBS_ID)
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            GetOldParasBranchComplianceTextModel chk = new GetOldParasBranchComplianceTextModel();
            using (OracleCommand cmd = con.CreateCommand())
                cmd.CommandText = "pkg_hd.P_GetOldParasforsettlementext_referedack";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("pid", OracleDbType.Int32).Value = PID;
                cmd.Parameters.Add("refP", OracleDbType.Varchar2).Value = Ref_P;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    chk.PARA_TEXT = rdr["para_text"].ToString();
                    chk.GIST_OF_PARA = rdr["gist_of_paras"].ToString();
                    //chk.RESPONSIBLE_PPs = this.GetOldParasObservationResponsiblePPNOs(Ref_P, chk.PARA_CATEGORY);
                    //chk.EVIDENCES = this.GetOldParasEvidences(Ref_P, chk.PARA_CATEGORY, REPLY_DATE, OBS_ID);
            con.Dispose();
            return chk;
        public GetOldParasBranchComplianceTextModel GetOldParasBranchComplianceTextForHeadAZ(int PID, string Ref_P, string OBS_ID, string PARA_CATEGORY, string REPLY_DATE)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            GetOldParasBranchComplianceTextModel chk = new GetOldParasBranchComplianceTextModel();
                cmd.CommandText = "pkg_hd.P_GetOldParasforfinalsettlementext";
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("refP", OracleDbType.Varchar2).Value = Ref_P;
                cmd.Parameters.Add("PID", OracleDbType.Int32).Value = PID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;

                    chk.PARA_TEXT = rdr["para_text"].ToString();
                    chk.GIST_OF_PARA = rdr["gist_of_paras"].ToString();
                    //chk.RESPONSIBLE_PPs = this.GetOldParasObservationResponsiblePPNOs(Ref_P, chk.PARA_CATEGORY);
                    //chk.UPDATED_RESPONSIBLE_PPs_BY_IMP = this.GetOldParasObservationResponsiblePPNOsUpdatedByImp(PID, chk.PARA_CATEGORY, 0);
                    //chk.EVIDENCES = this.GetOldParasEvidences(Ref_P, chk.PARA_CATEGORY, REPLY_DATE, OBS_ID);
            return chk;
        public List<GetOldParasforComplianceSettlement> GetComplianceForImpZone()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetOldParasforComplianceSettlement> list = new List<GetOldParasforComplianceSettlement>();
                cmd.CommandText = "pkg_hd.P_GetOldParasforsettlement";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    GetOldParasforComplianceSettlement chk = new GetOldParasforComplianceSettlement();
                    chk.ID = Convert.ToInt32(rdr["ID"].ToString());
                    chk.REF_P = rdr["ref_p"].ToString();
                    chk.AU_OBS_ID = rdr["AU_OBS_ID"].ToString();
                    chk.REPORTINGOFFICE = rdr["Reportingoffice"].ToString();
                    chk.AUDITEENAME = rdr["auditeename"].ToString();
                    chk.AUDITPERIOD = rdr["audit_period"].ToString();
                    chk.PARANO = rdr["para_no"].ToString();
                    chk.GISTOFPARA = rdr["headings"].ToString();
                    chk.REPLIEDDATE = rdr["replieddate"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.RISK = rdr["RISK"].ToString();
                    chk.REMARKS = rdr["remarks"].ToString();
                    chk.SEQUENCE = rdr["SEQUENCE"].ToString();
                    chk.AUDITED_BY = rdr["auditedby"].ToString();

        public List<GetOldParasforComplianceSettlement> GetReferredBackParasComplianceForImpZone()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetOldParasforComplianceSettlement> list = new List<GetOldParasforComplianceSettlement>();
                cmd.CommandText = "pkg_hd.P_GetOldParasforsettlement_ref";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    GetOldParasforComplianceSettlement chk = new GetOldParasforComplianceSettlement();
                    chk.ID = Convert.ToInt32(rdr["ID"].ToString());
                    chk.REF_P = rdr["ref_p"].ToString();
                    chk.AU_OBS_ID = rdr["AU_OBS_ID"].ToString();
                    chk.REPORTINGOFFICE = rdr["Reportingoffice"].ToString();
                    chk.AUDITEENAME = rdr["auditeename"].ToString();
                    chk.AUDITPERIOD = rdr["audit_period"].ToString();
                    chk.PARANO = rdr["para_no"].ToString();
                    chk.GISTOFPARA = rdr["headings"].ToString();
                    chk.REPLIEDDATE = rdr["replieddate"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.RISK = rdr["risk"].ToString();
                    chk.HEAD_REF_REMARKS = rdr["remarks"].ToString();
                    chk.SEQUENCE = rdr["SEQUENCE"].ToString();
                    chk.AUDITED_BY = rdr["auditedby"].ToString();

        public List<GetOldParasforComplianceSettlement> GetOldParasBranchComplianceSubmission()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetOldParasforComplianceSettlement> list = new List<GetOldParasforComplianceSettlement>();
                cmd.CommandText = "pkg_hd.P_GetOldParasforsettlement";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    GetOldParasforComplianceSettlement chk = new GetOldParasforComplianceSettlement();
                    chk.ID = Convert.ToInt32(rdr["ID"].ToString());
                    chk.REF_P = rdr["ref_p"].ToString();
                    chk.REPORTINGOFFICE = rdr["Reportingoffice"].ToString();
                    chk.AUDITEENAME = rdr["auditeename"].ToString();
                    chk.AUDITPERIOD = rdr["audit_period"].ToString();
                    chk.PARANO = rdr["para_no"].ToString();
                    chk.GISTOFPARA = rdr["gistofpara"].ToString();
                    chk.AMOUNT = rdr["amount_involved"].ToString();
                    chk.REPLY = rdr["reply"].ToString();
                    chk.REMARKS = rdr["remarks"].ToString();
                    chk.REVIEWER_REMARKS = rdr["REVIEWER_REMARKS"].ToString();
                    chk.SUBMITTED = rdr["submitted"].ToString();
                    chk.C_STATUS = rdr["c_status"].ToString();
                    chk.VOL_I_II = rdr["c_status"].ToString();
        public List<GetOldParasForFinalSettlement> GetOldParasForFinalSettlement()
            var loggedInUser = sessionHandler.GetSessionUser();
            List<GetOldParasForFinalSettlement> list = new List<GetOldParasForFinalSettlement>();
                cmd.CommandText = "pkg_hd.P_GetOldParasforfinalsettlement";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;

                    GetOldParasForFinalSettlement chk = new GetOldParasForFinalSettlement();
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.AUDITEENAME = rdr["AUDITEENAME"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GISTOFPARA = rdr["GISTOFPARA"].ToString();
                    chk.RISK = rdr["RISK"].ToString();
                    chk.IMP_REMARKS = rdr["REMARKS"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.REPLIEDDATE = rdr["REPLIEDDATE"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"].ToString());
                    chk.ID = Convert.ToInt32(rdr["ID"].ToString());
                    chk.AU_OBS_ID = rdr["AU_OBS_ID"].ToString();
                    chk.SEQUENCE = rdr["SEQUENCE"].ToString();
                    chk.AUDITED_BY = rdr["AUDITEDBY"].ToString();
        public string AddOldParasStatusUpdate(string OBS_ID, string REFID, string REMARKS, int NEW_STATUS, string PARA_CATEGORY, string SETTLE_INDICATOR, string SEQUENCE, string AUDITED_BY)
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_AddOldParasImpRemarks";
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("REFID", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("REMARK", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = SETTLE_INDICATOR;
                cmd.Parameters.Add("R_STATUS", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.Parameters.Add("SEQ_ID", OracleDbType.Varchar2).Value = SEQUENCE;
                cmd.Parameters.Add("AUDIT_ID", OracleDbType.Varchar2).Value = AUDITED_BY;
                    resp = rdr["remarks"].ToString();
            return resp;
        public string AddOldParasStatusPartiallySettle(string OBS_ID, string REFID, string REMARKS, int NEW_STATUS, string PARA_CATEGORY, string SETTLE_INDICATOR, List<ObservationResponsiblePPNOModel> RESPONSIBLES_ARR, string SEQUENCE, string AUDITED_BY, string PARA_TEXT)
            string resp = "";
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_AddOldParasImpRemarks_partial_comp";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("O_B", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("REFID", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("REMARK", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("PARA_T", OracleDbType.Clob).Value = PARA_TEXT;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = "P";
                cmd.Parameters.Add("R_STATUS", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.Parameters.Add("SEQ_ID", OracleDbType.Varchar2).Value = SEQUENCE;

                    resp = rdr["remarks"].ToString();
                    }

                if (RESPONSIBLES_ARR != null)
                    {
                    if (RESPONSIBLES_ARR.Count > 0)
                        {
                        foreach (ObservationResponsiblePPNOModel pp in RESPONSIBLES_ARR)
                            {
                            cmd.CommandText = "pkg_hd.p_add_para_responsibility_partial_comp";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                            cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                            cmd.Parameters.Add("USER_PPNO", OracleDbType.Int32).Value = pp.PP_NO;
                            cmd.Parameters.Add("LC_NO", OracleDbType.Varchar2).Value = pp.LOAN_CASE;
                            cmd.Parameters.Add("LC_AMOUNT", OracleDbType.Varchar2).Value = pp.ACCOUNT_NUMBER;
                            cmd.Parameters.Add("AC_NO", OracleDbType.Varchar2).Value = pp.LC_AMOUNT;
                            cmd.Parameters.Add("AC_AMOUNT", OracleDbType.Varchar2).Value = pp.ACC_AMOUNT;
                            cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = REFID;
                            cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                            cmd.Parameters.Add("A_C", OracleDbType.Varchar2).Value = pp.RESP_ACTIVE;
                            cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                            OracleDataReader rdr2 = cmd.ExecuteReader();
                            while (rdr.Read())
                                {
                                resp = resp + "<br/>" + rdr2["remarks"].ToString();
                                }
                            }
                        }

            return resp;
        public string AddOldParasheadStatusUpdate(int PARA_ID, string REMARKS, int NEW_STATUS, string PARA_REF, string PARA_INDICATOR, string PARA_CATEGORY, int AU_OBS_ID, string SEQUENCE, string AUDITED_BY, string ENTITY_ID)
            string resp = "";
            var con = this.DatabaseConnection(); con.Open();
                cmd.CommandText = "pkg_hd.P_AddFinalsettlement";
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_C", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("OBSID", OracleDbType.Int32).Value = AU_OBS_ID;
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("REMARK", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("STATUS", OracleDbType.Varchar2).Value = PARA_INDICATOR;
                cmd.Parameters.Add("R_STATUS", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.Parameters.Add("SEQ_ID", OracleDbType.Varchar2).Value = SEQUENCE;
                cmd.Parameters.Add("AUDIT_ID", OracleDbType.Varchar2).Value = AUDITED_BY;
                cmd.Parameters.Add("AUDITEE_ID", OracleDbType.Varchar2).Value = ENTITY_ID;
                    resp = rdr["remarks"].ToString();

            return resp;
        public List<AuditeeOldParasModel> GetOldParasForMonitoring(int ENTITY_ID = 0)
            var con = this.DatabaseConnection(); con.Open();
            List<AuditeeOldParasModel> list = new List<AuditeeOldParasModel>();
                cmd.CommandText = "pkg_hd.P_GET_ALL_PARAS_MONITORING";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = ENTITY_ID;
                    AuditeeOldParasModel chk = new AuditeeOldParasModel();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.PARA_CATEGORY = rdr["IND"].ToString();
                    chk.MEMO_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.PARA_ID = rdr["OLD_PARA_ID"].ToString();
                    chk.OBS_ID = rdr["OBS_ID"].ToString();
                    chk.PARA_RISK = rdr["PARA_RISK"].ToString();
                    list.Add(chk);
            return list;
        public string GetParaText(string ref_p)
            string resp = "";
                cmd.CommandText = "pkg_hd.P_GetOldParastext";
                cmd.Parameters.Add("para_ref", OracleDbType.Varchar2).Value = ref_p;
                    resp = rdr["PARA_TEXT"].ToString();
            return resp;
        public string GetAllParaText(string PARA_ID, string OBS_ID, string PARA_CATEGORY)
            string resp = "";
                cmd.CommandText = "pkg_hd.P_GET_ALL_PARA_TEXT";
                cmd.Parameters.Add("CAT", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("PARA_ID", OracleDbType.Varchar2).Value = PARA_ID;
                    resp = rdr["PARA_TEXT"].ToString();
            return resp;
        public List<AuditeeOldParasPpnoModel> GetOldParasForMonitoringPpno(int ppno)
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeOldParasPpnoModel> list = new List<AuditeeOldParasPpnoModel>();
                cmd.CommandText = "pkg_hd.p_ppno_para";
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = ppno;
                    AuditeeOldParasPpnoModel model = new AuditeeOldParasPpnoModel();
                    model.ComId = rdr["COM_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["COM_ID"]);
                    model.OldParaId = rdr["OLD_PARA_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["OLD_PARA_ID"]);
                    model.NewParaId = rdr["NEW_PARA_ID"] == DBNull.Value ? (int?)null : Convert.ToInt32(rdr["NEW_PARA_ID"]);
                    model.EntityName = rdr["NAME"]?.ToString();
                    model.AuditPeriod = rdr["AUDIT_PERIOD"]?.ToString();
                    model.Amount = rdr["AMOUNT"] == DBNull.Value ? (decimal?)null : Convert.ToDecimal(rdr["AMOUNT"]);
                    model.Annex = rdr["CODE"]?.ToString();
                    model.ParaStatus = rdr["PARA_STATUS"]?.ToString();
                    model.Ind = rdr["IND"]?.ToString();
                    model.ParaNo = rdr["PARA_NO"]?.ToString();
                    model.GistOfParas = rdr["GIST_OF_PARAS"]?.ToString();
                    list.Add(model);
        public string AddChangeStatusRequestForSettledPara(string REFID, string OBS_ID, string INDICATOR, int NEW_STATUS, string REMARKS)
            string resp = "";
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.p_changestatusrequestforsettledpara";
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("au_obs_id", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("ind", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("NewStatus", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("remarks", OracleDbType.Varchar2).Value = REMARKS;
                    resp = rdr["Remark"].ToString();

            return resp;
        public string ReviewerAddChangeStatusRequestForSettledPara(string REFID, string IND, string REMARKS, string Action_IND)
            string resp = "";
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_ChangeStatusRequestForSettledPara_new_reviewer";
                cmd.Parameters.Add("obsid", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("Remark", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("ind", OracleDbType.Varchar2).Value = Action_IND;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    resp = rdr["Remark"].ToString();

            return resp;
        public string AuthorizerAddChangeStatusRequestForSettledPara(string REFID, string IND, int NEW_STATUS, string REMARKS, string Action_IND)
            string resp = "";
                cmd.CommandText = "pkg_fad.P_AuthorizeChangeStatusRequestForSettledPara_new";
                cmd.Parameters.Add("obs_id", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("remark", OracleDbType.Varchar2).Value = REMARKS;
                cmd.Parameters.Add("indicator", OracleDbType.Varchar2).Value = Action_IND;
                    resp = rdr["Remarks"].ToString();
            return resp;
        public string AddChangeStatusRequestForCurrentPara(string REFID, int NEW_STATUS, string REMARKS)
            string resp = "";
                cmd.CommandText = "pkg_hd.P_ChangeStatusRequestForSettledPara_new";
                cmd.Parameters.Add("obs_id", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("NewStatus", OracleDbType.Int32).Value = NEW_STATUS;
                cmd.Parameters.Add("remarks", OracleDbType.Varchar2).Value = REMARKS;
                    resp = rdr["Remark"].ToString();
            return resp;

        public string AddAuthorizeChangeStatusRequestForSettledPara(string REFID, string OBS_ID, string IND, string Action_IND)

            string resp = "";
                cmd.CommandText = "pkg_fad.p_authorizechangestatusrequestforsettledpara";
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = REFID;
                cmd.Parameters.Add("au_obs_id", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("ind", OracleDbType.Varchar2).Value = Action_IND;
                    resp = rdr["Remark"].ToString();
            return resp;
        public List<OldParasAuthorizeModel> GetOldSettledParasForResponseAuthorize()
            List<OldParasAuthorizeModel> list = new List<OldParasAuthorizeModel>();
                cmd.CommandText = "pkg_fad.p_getoldparasforresponseauthorize";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    OldParasAuthorizeModel chk = new OldParasAuthorizeModel();
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.AU_OBS_ID = rdr["AU_OBS_ID"].ToString();
                    chk.IND = rdr["IND"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ANNEXURE = rdr["ANNEXURE"].ToString();
                    chk.AMOUNT_INVOLVED = rdr["AMOUNT_INVOLVED"].ToString();
                    chk.VOL_I_II = rdr["VOL_I_II"].ToString();
                    chk.PARA_STATUS = rdr["PARA_STATUS"].ToString();
                    chk.PARA_CHANGE_REQUEST_STATUS = rdr["TEMP_STATUS_FOR_CHANGE"].ToString();

                    chk.REMARKS = rdr["REMARKS"].ToString();




        public string AddObservationGistAndRecommendation(int OBS_ID = 0, string GIST_OF_PARA = "", string AUDITOR_RECOMMENDATION = "")
            string resp = "";
            List<AuditPlanReportModel> planList = new List<AuditPlanReportModel>();

                string _sql = "pkg_hd.P_audit_pre_Concluding";
                cmd.Parameters.Add("obsid", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("gist", OracleDbType.Varchar2).Value = GIST_OF_PARA;
                cmd.Parameters.Add("recom", OracleDbType.Varchar2).Value = AUDITOR_RECOMMENDATION;
                cmd.CommandText = _sql;
                    resp = rdr["remarks"].ToString();
            return resp;



        public List<AuditConcludingEntitiesModel> GetAuditConcludingEntities()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditConcludingEntitiesModel> list = new List<AuditConcludingEntitiesModel>();
                cmd.CommandText = "pkg_hd.p_get_audit_concluding_entities";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;

                    AuditConcludingEntitiesModel chk = new AuditConcludingEntitiesModel();

                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.ENG_ID = Convert.ToInt32(rdr["ENG_ID"].ToString());
                    chk.TYPE_ID = Convert.ToInt32(rdr["TYPE_ID"].ToString());
                    list.Add(chk);



        public string ConcludeDraftAuditReport(int ENG_ID)
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            if (ENG_ID == 0)
                ENG_ID = this.GetLoggedInUserEngId();
                cmd.CommandText = "pkg_hd.P_Audit_Concluding";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    resp = rdr["REMARKS"].ToString();
            return resp;

        public string SubmitPreConcluding(int ENG_ID)
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_audit_pre_submission";
                cmd.Parameters.Add("engid", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    resp = rdr["remarks"].ToString();
            return resp;
        public string UpdateAuditParaForFinalization(int OBS_ID, string ANNEX_ID, string PROCESS_ID, int SUB_PROCESS_ID, int PROCESS_DETAIL_ID, int RISK_ID, int FINAL_PARA_NO, string GIST_OF_PARA, string TEXT_PARA, string AMOUNT_INV, string NO_INST)
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
                cmd.CommandText = "pkg_hd.P_audit_para_update_svz_az";
                cmd.Parameters.Add("OBID", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("ANXID", OracleDbType.Int32).Value = ANNEX_ID;
                cmd.Parameters.Add("PROCID", OracleDbType.Int32).Value = PROCESS_ID;
                cmd.Parameters.Add("SUB_PROCID", OracleDbType.Int32).Value = SUB_PROCESS_ID;
                cmd.Parameters.Add("PROC_DETID", OracleDbType.Int32).Value = PROCESS_DETAIL_ID;
                cmd.Parameters.Add("RISKID", OracleDbType.Int32).Value = RISK_ID;
                cmd.Parameters.Add("FINAL_PARA", OracleDbType.Int32).Value = FINAL_PARA_NO;
                cmd.Parameters.Add("PARA_GIST", OracleDbType.Varchar2).Value = GIST_OF_PARA;
                cmd.Parameters.Add("TEXT_OF_PARA", OracleDbType.Clob).Value = TEXT_PARA;
                cmd.Parameters.Add("AMOUNT_INV", OracleDbType.Int32).Value = AMOUNT_INV;
                cmd.Parameters.Add("NO_INST", OracleDbType.Int32).Value = NO_INST;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    resp = rdr["remarks"].ToString();
            return resp;
        public string UpdateAuditParaForFinalizationHO(int OBS_ID, string VIOLATION_ID, int VIOLATION_NATURE_ID, int RISK_ID, string GIST_OF_PARA, string TEXT_PARA)
            string resp = "";

                cmd.CommandText = "pkg_hd.P_audit_para_update_head_dept";
                cmd.Parameters.Add("OBID", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("VID", OracleDbType.Int32).Value = VIOLATION_ID;
                cmd.Parameters.Add("VNATURE_ID", OracleDbType.Int32).Value = VIOLATION_NATURE_ID;
                cmd.Parameters.Add("RISKID", OracleDbType.Int32).Value = RISK_ID;
                cmd.Parameters.Add("PARA_GIST", OracleDbType.Varchar2).Value = GIST_OF_PARA;
                cmd.Parameters.Add("TEXT_OF_PARA", OracleDbType.Clob).Value = TEXT_PARA;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    resp = rdr["remarks"].ToString();
                }
            con.Dispose();
            return resp;
            }

        public List<ObservationResponsiblePPNOModel> GetLegacyParaResponsiblePersonsFAD(string PARA_REF)
            List<ObservationResponsiblePPNOModel> list = new List<ObservationResponsiblePPNOModel>();
                cmd.CommandText = "pkg_fad.p_get_legacy_para_responsibles";
                cmd.Parameters.Add("paraRef", OracleDbType.Varchar2).Value = PARA_REF;
                    ObservationResponsiblePPNOModel rp = new ObservationResponsiblePPNOModel();

                    rp.LOAN_CASE = rdr["LOAN_CASE"].ToString();
                    rp.EMP_NAME = rdr["EMP_NAME"].ToString();
                    rp.LC_AMOUNT = rdr["LC_AMOUNT"].ToString();
                    rp.ACCOUNT_NUMBER = rdr["ACCOUNT_NUMBER"].ToString();
                    rp.ACC_AMOUNT = rdr["AC_AMOUNT"].ToString();
                    rp.PP_NO = rdr["PP_NO"].ToString();
                    list.Add(rp);
            return list;






        public string DeleteLegacyParaResponsibility(string PARA_REF, int PARA_ID, int PP_NO)
            string resp = "Failed to delete responsibility, Please try again";
                cmd.CommandText = "pkg_fad.p_delete_para_responsibility";
                cmd.Parameters.Add("refp", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("refid", OracleDbType.Int32).Value = PARA_ID;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = PP_NO;
                // cmd.Parameters.Add("USER_PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    if (rdr["REMARKS"].ToString() != null && rdr["REMARKS"].ToString() != "")
                        resp = rdr["REMARKS"].ToString();
            return resp;

        public List<AuditEntitiesModel> GetAuditeeEntitiesType()


            List<AuditEntitiesModel> entitiesList = new List<AuditEntitiesModel>();
                cmd.CommandText = "pkg_hd.P_GetAuditEntitiestype";
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    AuditEntitiesModel entity = new AuditEntitiesModel();
                    entity.TYPE_ID = Convert.ToInt32(rdr["TYPEID"]);
                    entity.ENTITYTYPEDESC = rdr["E_NAME"].ToString();
                    entitiesList.Add(entity);
            return entitiesList;

        public List<AuditEntitiesModel> GetAuditEntitiesByTypeId(int ENTITY_TYPE_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;

            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditEntitiesModel> entitiesList = new List<AuditEntitiesModel>();
                cmd.CommandText = "pkg_hd.P_GetAuditEntities";
                cmd.Parameters.Add("TYPEID", OracleDbType.Int32).Value = ENTITY_TYPE_ID;
                cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    AuditEntitiesModel entity = new AuditEntitiesModel();
                    entity.TYPE_ID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    entity.ENTITYTYPEDESC = rdr["E_NAME"].ToString();
                    entitiesList.Add(entity);
            return entitiesList;

        public List<AuditPeriodModel> GetAuditYearForAddLegacyPara()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;

            var loggedInUser = sessionHandler.GetSessionUser();

            List<AuditPeriodModel> entitiesList = new List<AuditPeriodModel>();
                cmd.CommandText = "pkg_hd.P_GetAuditYear";
                    AuditPeriodModel entity = new AuditPeriodModel();
                    entity.AUDITPERIODID = Convert.ToInt32(rdr["audit_year"]);
                    entity.DESCRIPTION = rdr["period"].ToString();
                    entitiesList.Add(entity);
            return entitiesList;


        public List<AuditNatureModel> GetAuditNatureForAddLegacyPara()


            List<AuditNatureModel> entitiesList = new List<AuditNatureModel>();
                cmd.CommandText = "pkg_hd.P_GetAuditnature";
                    AuditNatureModel entity = new AuditNatureModel();
                    entity.N_ID = Convert.ToInt32(rdr["NID"]);
                    entity.DESCRIPTION = rdr["DESCRIPTION"].ToString();
                    entitiesList.Add(entity);
            return entitiesList;

        public string AddNewLegacyPara(AddNewLegacyParaModel LEGACY_PARA)
            string resp = "";

            List<AuditNatureModel> entitiesList = new List<AuditNatureModel>();
                cmd.CommandText = "pkg_hd.P_add_legacy_Para";
                cmd.Parameters.Add("typeid", OracleDbType.Int32).Value = LEGACY_PARA.ENTITY_TYPE_ID;
                cmd.Parameters.Add("audityear", OracleDbType.Varchar2).Value = LEGACY_PARA.AUDIT_YEAR;
                cmd.Parameters.Add("PARANO", OracleDbType.Varchar2).Value = LEGACY_PARA.PARA_NO;
                cmd.Parameters.Add("GIST", OracleDbType.Varchar2).Value = LEGACY_PARA.GIST_OF_PARA;
                cmd.Parameters.Add("ANEXURE", OracleDbType.Varchar2).Value = LEGACY_PARA.ANNEXURE;
                cmd.Parameters.Add("amount", OracleDbType.Varchar2).Value = LEGACY_PARA.AMOUNT;
                cmd.Parameters.Add("VOL", OracleDbType.Varchar2).Value = LEGACY_PARA.VOL_I_II;
                cmd.Parameters.Add("Entityid", OracleDbType.Int32).Value = LEGACY_PARA.ENTITY_ID;
                cmd.Parameters.Add("USER_ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("nature", OracleDbType.Int32).Value = LEGACY_PARA.NATURE_ID;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    resp = rdr["remarks"].ToString();

            return resp;

        public string ReferBackLegacyPara(string PARA_REF, int PARA_ID)
            string resp = "";

            List<AuditNatureModel> entitiesList = new List<AuditNatureModel>();
                cmd.CommandText = "pkg_fad.P_referback_legacy_para";
                cmd.Parameters.Add("ref_id", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("ppno", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    resp = rdr["remarks"].ToString();
            return resp;
        public List<AddNewLegacyParaModel> GetAddedLegacyParaForAuthorize()
            List<AddNewLegacyParaModel> list = new List<AddNewLegacyParaModel>();

                cmd.CommandText = "pkg_hd.P_get_legacy_para_to_authorize";
                cmd.Parameters.Add("ENTITY_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    AddNewLegacyParaModel lpara = new AddNewLegacyParaModel();
                    lpara.PARA_REF = rdr["REF_P"].ToString();
                    lpara.ANNEXURE = rdr["ANNEXURE"].ToString();
                    lpara.VOL_I_II = rdr["VOL_I_II"].ToString();
                    lpara.PARA_NO = rdr["PARA_NO"].ToString();
                    lpara.GIST_OF_PARA = rdr["GIST_OF_PARAS"].ToString();
                    lpara.AUDIT_YEAR = rdr["AUDIT_YEAR"].ToString();
                    lpara.E_CODE = rdr["E_CODE"].ToString();
                    lpara.NATURE = rdr["NATURE"].ToString();
                    lpara.E_NAME = rdr["E_NAME"].ToString();
                    lpara.AMOUNT = rdr["AMOUNT_INVOLVED"].ToString();
                    list.Add(lpara);

        public string AuthorizeLegacyParaAddition(string PARA_REF)
            string resp = "";

                cmd.CommandText = "pkg_hd.P_Authorize_legacy_para_addition";
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    resp = rdr["Remarks"].ToString();
            return resp;

        public string DeleteLegacyParaAdditionRequest(string PARA_REF)
            var loggedInUser = sessionHandler.GetSessionUser();

                cmd.CommandText = "pkg_hd.P_referedback_Del_para";
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = PARA_REF;
                cmd.Parameters.Add("PPNO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                    resp = rdr["Remarks"].ToString();


        //get_audit_performance_for_dashboard









        public string GetNewParaText(string OBS_ID)

                cmd.CommandText = "pkg_hd.P_GetnewParastext";
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                    resp = rdr["text"].ToString();






        public List<ZoneModel> GetZonesForAnnexureAssignment()
            List<ZoneModel> zoneList = new List<ZoneModel>();
                cmd.CommandText = "pkg_fad.P_Get_Auditee_Parent_FAD";
                    ZoneModel z = new ZoneModel();
                    z.ZONEID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    z.ZONENAME = rdr["NAME"].ToString();
                    zoneList.Add(z);
            return zoneList;
        public List<BranchModel> GetZoneBranchesForAnnexureAssignment(int ENTITY_ID = 0)
            var con = this.DatabaseConnection(); con.Open();
            List<BranchModel> branchList = new List<BranchModel>();
                cmd.CommandText = "pkg_fad.P_Get_Auditee_Child_FAD";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = ENTITY_ID;
                    BranchModel br = new BranchModel();
                    br.BRANCHID = Convert.ToInt32(rdr["ENTITY_ID"]);
                    br.BRANCHNAME = rdr["NAME"].ToString();
                    branchList.Add(br);
            return branchList;

        public List<AllParaForAnnexureAssignmentModel> GetAllParasForAnnexureAssignment(int ENTITY_ID = 0)
            List<AllParaForAnnexureAssignmentModel> list = new List<AllParaForAnnexureAssignmentModel>();
                cmd.CommandText = "pkg_fad.P_Get_all_paras_fad";
                cmd.Parameters.Add("EntityID", OracleDbType.Int32).Value = ENTITY_ID;
                    AllParaForAnnexureAssignmentModel chk = new AllParaForAnnexureAssignmentModel();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    chk.OBS_ID = rdr["OBS_ID"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.GIST_OF_PARAS = rdr["GIST_OF_PARAS"].ToString();
                    chk.ENTITY_NAME = rdr["NAME"].ToString();
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.ANNEX_CODE = rdr["ANNEX_ID"].ToString();
                    chk.ANNEX_ID = rdr["ID"].ToString();
                    chk.ANNEXURE = rdr["NAME"].ToString();
        public string AssignAnnexureWithPara(string OBS_ID, string REF_P, string ANNEX_ID, string PARA_CATEGORY)
            string resp = "";
            List<AllParaForAnnexureAssignmentModel> list = new List<AllParaForAnnexureAssignmentModel>();
                cmd.CommandText = "pkg_fad.P_Update_paras_annex_fad";
                cmd.Parameters.Add("CAT", OracleDbType.Varchar2).Value = PARA_CATEGORY;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = REF_P;
                cmd.Parameters.Add("ANEX", OracleDbType.Varchar2).Value = ANNEX_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Varchar2).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Varchar2).Value = loggedInUser.UserRoleID;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    resp = rdr["REMARKS"].ToString();

            return resp;





        #region BAC PROCEDURE CALLS
        public List<BACAgendaModel> GetBACAgenda(int MEETING_NO)
            var con = this.DatabaseConnection(); con.Open();
            List<BACAgendaModel> pdetails = new List<BACAgendaModel>();
                cmd.CommandText = "pkg_bac.P_BAC_AGENDA";
                cmd.Parameters.Add("Meeting", OracleDbType.Int32).Value = MEETING_NO;
                    BACAgendaModel zb = new BACAgendaModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.MEETING_NO = rdr["meeting_no"].ToString();
                    zb.MEMO_NO = rdr["memo_no"].ToString();
                    zb.SUBJECT = rdr["subject"].ToString();
                    zb.REMARKS = rdr["remarks"].ToString();
                    pdetails.Add(zb);
            return pdetails;

        public List<BACAgendaModel> GetBACAMeetingSummary(int MEETING_NO)
            var con = this.DatabaseConnection(); con.Open();
            List<BACAgendaModel> pdetails = new List<BACAgendaModel>();
                cmd.CommandText = "pkg_bac.P_Bac_get_actionable_sum";
                cmd.Parameters.Add("User_entityid", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    BACAgendaModel zb = new BACAgendaModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.MEETING_NO = rdr["meeting_no"].ToString();
                    zb.MEMO_NO = rdr["memo_no"].ToString();
                    zb.SUBJECT = rdr["subject"].ToString();
                    zb.REMARKS = rdr["remarks"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        public List<BACAgendaActionablesSummaryModel> GetBACAgendaActionablesConsolidatedSummary()
            var con = this.DatabaseConnection(); con.Open();
            List<BACAgendaActionablesSummaryModel> pdetails = new List<BACAgendaActionablesSummaryModel>();
            using (OracleCommand cmd = con.CreateCommand())
                cmd.CommandText = "pkg_bac.P_Bac_get_actionable_snap";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    BACAgendaActionablesSummaryModel zb = new BACAgendaActionablesSummaryModel();
                    zb.TOTAL = Convert.ToInt32(rdr["total"].ToString());
                    zb.COMPLETED = rdr["completed"].ToString();
                    zb.UN_COMPLETED = rdr["un_completed"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        public List<BACAgendaActionablesSummaryModel> GetBACAgendaActionablesSummary()
            List<BACAgendaActionablesSummaryModel> pdetails = new List<BACAgendaActionablesSummaryModel>();
                cmd.CommandText = "pkg_bac.P_Bac_get_actionable_sum";
                    BACAgendaActionablesSummaryModel zb = new BACAgendaActionablesSummaryModel();
                    zb.TOTAL = Convert.ToInt32(rdr["total"].ToString());
                    zb.MEETING_NO = rdr["meeting_number"].ToString();
                    zb.COMPLETED = rdr["completed"].ToString();
                    zb.UN_COMPLETED = rdr["un_completed"].ToString();
                    zb.RESPONSIBLES = rdr["RESPONSIBLE"].ToString();
                    zb.MANAGEMENT_RESPONSE = rdr["RESPONSE"].ToString();
                    zb.REFERENCE = rdr["BAC_DIRECTIVES"].ToString();
                    zb.CIA_REMARKS = rdr["CIA_REMARKS"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        public List<BACAgendaActionablesModel> GetBACAgendaActionables(string STATUS)
            List<BACAgendaActionablesModel> pdetails = new List<BACAgendaActionablesModel>();
                cmd.CommandText = "pkg_bac.P_Bac_get_actionable";
                cmd.Parameters.Add("status", OracleDbType.Varchar2).Value = STATUS;
                    BACAgendaActionablesModel zb = new BACAgendaActionablesModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.MEETING_NO = rdr["meeting_number"].ToString();
                    zb.ITEM_HEADING = rdr["item_heading"].ToString();
                    zb.BAC_DIRECTION = rdr["bac_direction"].ToString();
                    zb.ASSIGN_TO = rdr["assign_to"].ToString();
                    zb.TIMELINE = rdr["time_line"].ToString();
                    zb.OPEN_TIMELINE = rdr["open_time_line"].ToString();
                    zb.DUE_DATE = rdr["due_date"].ToString();
                    zb.REPORT_FREQUENCY = rdr["rpt_frequency"].ToString();
                    zb.ENTERED_BY = rdr["entered_by"].ToString();
                    zb.ENTERED_ON = rdr["entered_on"].ToString();
                    zb.DELAY = rdr["delay"].ToString();
                    zb.STATUS = rdr["status"].ToString();
                    pdetails.Add(zb);
            return pdetails;

        public List<BACAgendaActionablesModel> GetBACAgendaActionablesWithMeetingNo(string STATUS, string MEETING_NO)
            List<BACAgendaActionablesModel> pdetails = new List<BACAgendaActionablesModel>();
                cmd.CommandText = "pkg_bac.P_Bac_get_actionable_meetings_with_status";
                cmd.Parameters.Add("meeting", OracleDbType.Varchar2).Value = MEETING_NO;
                cmd.Parameters.Add("A_Status", OracleDbType.Varchar2).Value = STATUS;
                    BACAgendaActionablesModel zb = new BACAgendaActionablesModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.MEETING_NO = rdr["meeting_number"].ToString();
                    zb.ITEM_HEADING = rdr["item_heading"].ToString();
                    zb.BAC_DIRECTION = rdr["bac_direction"].ToString();
                    zb.ASSIGN_TO = rdr["assign_to"].ToString();
                    zb.TIMELINE = rdr["time_line"].ToString();
                    zb.OPEN_TIMELINE = rdr["open_time_line"].ToString();
                    zb.DUE_DATE = rdr["due_date"].ToString();
                    zb.REPORT_FREQUENCY = rdr["rpt_frequency"].ToString();
                    zb.ENTERED_BY = rdr["entered_by"].ToString();
                    zb.ENTERED_ON = rdr["entered_on"].ToString();
                    zb.DELAY = rdr["delay"].ToString();
                    zb.STATUS = rdr["status"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        public List<BACCIAAnalysisOptionsModel> GetBACCIAAnalysisOptions()
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            List<BACCIAAnalysisOptionsModel> pdetails = new List<BACCIAAnalysisOptionsModel>();
                cmd.CommandText = "pkg_bac.P_CIA_ANALYSIS";
                    BACCIAAnalysisOptionsModel zb = new BACCIAAnalysisOptionsModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.HEADING = rdr["heading"].ToString();
                    zb.AUDIT_COMMENTS = rdr["audit_comments"].ToString();
                    zb.MONITORING = rdr["monitoring"].ToString();
                    zb.AUTOMATION = rdr["automation"].ToString();
                    pdetails.Add(zb);
            return pdetails;

        public List<BACCIAAnalysisModel> GetBACCIAAnalysis(int processId)

            List<BACCIAAnalysisModel> pdetails = new List<BACCIAAnalysisModel>();
                cmd.CommandText = "pkg_bac.P_CIA_ANALYSIS_DETAILS";
                cmd.Parameters.Add("a_id", OracleDbType.Varchar2).Value = processId;
                    BACCIAAnalysisModel zb = new BACCIAAnalysisModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.COUNT = rdr["total"].ToString();
                    zb.INDICATOR = rdr["indicator"].ToString();
                    zb.ANNEX = rdr["annex"].ToString();
                    zb.HEADING = rdr["heading"].ToString();
                    zb.OLDCOUNT = rdr["old_total"].ToString();
                    zb.NEWCOUNT = rdr["new_total"].ToString();
                    zb.AUDITCOMMENTS = rdr["audit_comments"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        #endregion




        public List<FunctionalAnnexureWiseObservationModel> GetAnalysisDetailPara(int PROCESS_ID)

            List<FunctionalAnnexureWiseObservationModel> pdetails = new List<FunctionalAnnexureWiseObservationModel>();
                cmd.CommandText = "pkg_bac.P_CIA_ANALYSIS_DETAILS_PARA";
                cmd.Parameters.Add("a_id", OracleDbType.Varchar2).Value = PROCESS_ID;
                cmd.Parameters.Add("r_id", OracleDbType.Varchar2).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("ent_id", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                    FunctionalAnnexureWiseObservationModel zb = new FunctionalAnnexureWiseObservationModel();
                    zb.ID = Convert.ToInt32(rdr["id"].ToString());
                    zb.NAME = rdr["name"].ToString();
                    zb.PARA_CATEGORY = rdr["para_category"].ToString();
                    zb.PARA_NO = rdr["para_no"].ToString();
                    zb.AUDIT_PERIOD = rdr["audit_period"].ToString();
                    pdetails.Add(zb);
            return pdetails;
        public List<FunctionalAnnexureWiseObservationModel> GetAnalysisSummaryPara(int PROCESS_ID)

            List<FunctionalAnnexureWiseObservationModel> pdetails = new List<FunctionalAnnexureWiseObservationModel>();
                cmd.CommandText = "pkg_bac.P_CIA_ANALYSIS_SUMMARY";
                cmd.Parameters.Add("a_id", OracleDbType.Varchar2).Value = PROCESS_ID;
                cmd.Parameters.Add("r_id", OracleDbType.Varchar2).Value = loggedInUser.UserGroupID;
                cmd.Parameters.Add("ent_id", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                    FunctionalAnnexureWiseObservationModel zb = new FunctionalAnnexureWiseObservationModel();

                    zb.P_NAME = rdr["p_name"].ToString();
                    zb.NAME = rdr["name"].ToString();
                    zb.PARA_NO = rdr["para_no"].ToString();
                    zb.AUDIT_PERIOD = rdr["audit_period"].ToString();
                    pdetails.Add(zb);
            return pdetails;



        public string ParaShiftedTo(int OBS_ID, int NEW_ENT_ID, int OLD_ENT_ID, string P_IND)
            var resp = "";

                cmd.CommandText = "pkg_fad.P_PARA_SHIFTING";
                cmd.Parameters.Add("NEW_ENT_ID", OracleDbType.Varchar2).Value = NEW_ENT_ID;
                cmd.Parameters.Add("OLD_ENT_ID", OracleDbType.Varchar2).Value = OLD_ENT_ID;
                cmd.Parameters.Add("O_ID", OracleDbType.Varchar2).Value = OBS_ID;
                cmd.Parameters.Add("P_IND", OracleDbType.Varchar2).Value = P_IND;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Varchar2).Value = loggedInUser.UserRoleID;
        public List<SettledParasMonitoringModel> GetSettledParasForMonitoring(int ENTITY_ID)
            List<SettledParasMonitoringModel> list = new List<SettledParasMonitoringModel>();

                cmd.CommandText = "pkg_fad.P_GET_SETTLED_PARA_DETAILS";
                cmd.Parameters.Add("ENT_ID", OracleDbType.Varchar2).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Varchar2).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("AUDITEE_ID", OracleDbType.Int32).Value = ENTITY_ID;
                    SettledParasMonitoringModel chk = new SettledParasMonitoringModel();
                    chk.REPORTING_OFFICE = rdr["REPORTING_OFFICE"].ToString();
                    chk.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    chk.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    chk.AU_OBS_ID = rdr["AU_OBS_ID"].ToString();
                    chk.REF_P = rdr["REF_P"].ToString();
                    chk.SETTLED_BY = rdr["SETTLED_BY"].ToString();
                    chk.SETTLED_ON = rdr["SETTLED_ON"].ToString();
                    chk.RISK = rdr["RISK"].ToString();
                    chk.PARA_NO = rdr["PARA_NO"].ToString();
                    chk.PARA_CATEGORY = rdr["PARA_CATEGORY"].ToString();
                    chk.COMPLIANCE_CYCLE = rdr["COMPLIANCE_CYCLE"].ToString();
                    chk.AUDITED_BY = rdr["AUDITEDBY"].ToString();
                    chk.ENTITY_ID = rdr["ENTITY_ID"].ToString();


        public List<ComplianceHistoryModel> GetSettledParaComplianceHistory(string REF_P, string OBS_ID)

            List<ComplianceHistoryModel> stList = new List<ComplianceHistoryModel>();

                cmd.CommandText = "pkg_fad.P_GET_SETTLED_PARA_DETAILS_PARA_COMPLIANCE";
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = REF_P;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                    ComplianceHistoryModel st = new ComplianceHistoryModel();

                    st.REMARKS = rdr["remarks"].ToString();
                    st.ATTENDED_BY = rdr["attended_by"].ToString();

                    st.NAME = rdr["EMP_NAME"].ToString();
                    st.DESIGNATION = rdr["DESIGNATION"].ToString();
                    st.COM_SEQ_NO = rdr["COMPLIANCE_CYCLE"].ToString();
                    stList.Add(st);
            return stList;

        public string SaveSettledParaCompliacne(string REF_P, string OBS_ID, string COMMENTS)

            string resp = "";
                cmd.CommandText = "pkg_fad.P_GET_SETTLED_PARA_DETAILS_PARA_COMPLIANCE";
                cmd.Parameters.Add("REFP", OracleDbType.Varchar2).Value = REF_P;
                cmd.Parameters.Add("OBS_ID", OracleDbType.Varchar2).Value = OBS_ID;
                    ComplianceHistoryModel st = new ComplianceHistoryModel();

                    st.REMARKS = rdr["remarks"].ToString();
                    st.ATTENDED_BY = rdr["attended_by"].ToString();

                    st.NAME = rdr["EMP_NAME"].ToString();
                    st.DESIGNATION = rdr["DESIGNATION"].ToString();
                    st.COM_SEQ_NO = rdr["COMPLIANCE_CYCLE"].ToString();
                    resp = "";
            return resp;













        //------------------- Special Audit Plan 
        public List<SpecialAuditPlanModel> GetSaveSpecialAuditPlan()
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var list = new List<SpecialAuditPlanModel>();
            try
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        cmd.CommandText = "pkg_pg.P_GET_Specical_Audit_for_Approval";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                var review = new SpecialAuditPlanModel
                                    {
                                    REPORTING_OFFICE = rdr["reporting"].ToString(),
                                    REPORTING_OFFICE_ID = rdr["reporting_id"].ToString(),
                                    ENTITY_NAME = rdr["auditee"].ToString(),
                                    ENTITY_ID = rdr["auditee_id"].ToString(),
                                    AUDITED_BY = rdr["auditor"].ToString(),
                                    AUDITED_BY_ID = rdr["auditor_id"].ToString(),
                                    PLAN_ID = rdr["P_ID"].ToString(),
                                    AUDIT_PERIOD = rdr["period"].ToString(),
                                    AUDIT_PERIOD_ID = rdr["period_id"].ToString(),
                                    NO_DAYS = rdr["no_of_days"].ToString(),
                                    NATURE = rdr["nature"].ToString(),
                                    NATURE_ID = rdr["nature_id"].ToString(),
                                    // FIELD_VISIT = rdr["visit"].ToString(),
                                    };
                                list.Add(review);
                                }
                            }
                        }
                    }
            catch (Exception)
                {

                throw;
                }

        public string AddSpecialAuditPlan(string NATURE, string PERIOD, string ENTITY_ID, string NO_DAYS, string PLAN_ID, string INDICATOR)
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            string resp = "";
            var list = new List<SpecialAuditPlanModel>();
            try
                using (var con = this.DatabaseConnection())
                    con.Open();
                    using (var cmd = con.CreateCommand())
                        {
                        cmd.CommandText = "pkg_pg.P_ADD_Special_Audit_Plan";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = PLAN_ID;
                        cmd.Parameters.Add("NOOFDAYS", OracleDbType.Int32).Value = NO_DAYS;
                        cmd.Parameters.Add("Nature", OracleDbType.Int32).Value = NATURE;
                        cmd.Parameters.Add("AUDITPERIODID", OracleDbType.Int32).Value = PERIOD;
                        cmd.Parameters.Add("ENTITYID", OracleDbType.Int32).Value = ENTITY_ID;
                        cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)
                {
                throw;
                }
            return resp;
            }
        public string DeleteSpecialAuditPlan(string PLAN_ID, string INDICATOR)
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;

            try
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        {
                        cmd.CommandText = "pkg_pg.P_Update_Special_Audit";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = PLAN_ID;
                        cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;

                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)
                {

                throw;
                }

        public string SubmitSpecialAuditPlan(string PLAN_ID, string INDICATOR)
            string resp = "";
            try
                {
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        cmd.CommandText = "pkg_pg.P_Update_Special_Audit";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ID", OracleDbType.Int32).Value = PLAN_ID;
                        cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)
                {

                throw;
                }
            return resp;
        public string RequestDeleteDuplicatePara(int NEW_PARA_ID = 0, int OLD_PARA_ID = 0, string INDICATOR = "", string REMARKS = "")
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            string resp = "";
            try
                {
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        cmd.CommandText = "pkg_hd.P_ADD_DUPLICATE_PARAS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("o_para_id", OracleDbType.Int32).Value = OLD_PARA_ID;
                        cmd.Parameters.Add("n_para_id", OracleDbType.Int32).Value = NEW_PARA_ID;
                        cmd.Parameters.Add("p_ind", OracleDbType.Varchar2).Value = INDICATOR;
                        cmd.Parameters.Add("r_remarks", OracleDbType.Varchar2).Value = REMARKS;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)
                {

                throw;
                }
            return resp;
        public List<AuditeeEntitiesModel> GetDuplicateParasAuthorizationEntityList()
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<AuditeeEntitiesModel> list = new List<AuditeeEntitiesModel>();
                cmd.CommandText = "pkg_hd.P_GET_DUPLICATE_PARAS_ENT_FOR_AUTH";

                    AuditeeEntitiesModel chk = new AuditeeEntitiesModel();
                    chk.NAME = rdr["name"].ToString();
                    chk.ENTITY_ID = Convert.ToInt32(rdr["ENTITY_ID"].ToString());
        public List<DuplicateDeleteManageParaModel> GetDuplicateParasForAuthorization()
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<DuplicateDeleteManageParaModel> list = new List<DuplicateDeleteManageParaModel>();
                cmd.CommandText = "pkg_hd.P_GET_DUPLICATE_PARAS_FOR_AUTH";
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;

                    DuplicateDeleteManageParaModel chk = new DuplicateDeleteManageParaModel();
                    chk.DId = Convert.ToInt32(rdr["d_id"]);
                    chk.OldParaId = Convert.ToInt32(rdr["old_para_id"]);
                    chk.NewParaId = Convert.ToInt32(rdr["new_para_id"]);
                    chk.EntityId = rdr["entity_id"].ToString();
                    chk.EntityName = rdr["EntityName"].ToString();
                    chk.AuditPeriod = rdr["audit_period"].ToString();
                    chk.ParaGist = rdr["gist_of_paras"].ToString();
                    chk.ParaNo = rdr["para_no"].ToString();
                    chk.Ind = rdr["ind"].ToString();
                    chk.Risk = rdr["risk"].ToString();
                    chk.Instances = rdr["instances"].ToString();
                    chk.Amount = rdr["amount"].ToString();
                    chk.Annex = rdr["annex"].ToString();
                    chk.AddedBy = rdr["added_by"].ToString();
                    chk.AddedOn = Convert.ToDateTime(rdr["added_on"]);
                    chk.Remarks = rdr["remarks"].ToString();
                    list.Add(chk);
            return list;
        public string RejectDeleteDuplicatePara(int D_PARA_ID = 0)
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            string resp = "";
            try
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        {
                        cmd.CommandText = "pkg_hd.P_REJECT_DUPLICATE_PARAS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("did", OracleDbType.Int32).Value = D_PARA_ID;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)

                throw;

        public string AuthDeleteDuplicatePara(int D_PARA_ID = 0, string INDICATOR = "", string REMARKS = "")
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            string resp = "";

            try
                using (var con = this.DatabaseConnection())
                    con.Open();

                    using (var cmd = con.CreateCommand())
                        {
                        cmd.CommandText = "pkg_hd.P_AUTH_DUPLICATE_PARAS";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("did", OracleDbType.Int32).Value = D_PARA_ID;
                        cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                        cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                        cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                        cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                        using (var rdr = cmd.ExecuteReader())
                            {
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            }
                        }
            catch (Exception)
                {

                throw;
                }


        public string AddResponsiblePersonsToObservation(int NEW_PARA_ID, int OLD_PARA_ID, string INDICATOR, ObservationResponsiblePPNOModel RESPONSIBLE, int paraStatus)
            if (paraStatus < 8)
                return AddInitialResponsibilityAssignment(NEW_PARA_ID, RESPONSIBLE, INDICATOR);
                }
            else
                {
                return UpdateResponsibilityAssignment(NEW_PARA_ID, OLD_PARA_ID, INDICATOR, RESPONSIBLE);


        public ObservationModel GetObservationDetailsById(int OBS_ID)
            ObservationModel resp = new ObservationModel();
                cmd.CommandText = "pkg_hd.P_GET_OBSERVATION_DETAILS_FROM_ID";
                cmd.Parameters.Add("obid", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    resp.ANNEXURE_ID = rdr["annex_id"].ToString();
                    resp.PROCESS_ID = Convert.ToInt32(rdr["t_id"].ToString());
                    resp.SUBCHECKLIST_ID = Convert.ToInt32(rdr["s_id"].ToString());
                    resp.CHECKLISTDETAIL_ID = Convert.ToInt32(rdr["d_id"].ToString());
                    resp.RISKMODEL_ID = Convert.ToInt32(rdr["severity"].ToString());
                    resp.HEADING = rdr["headings"].ToString();
                    resp.OBSERVATION_TEXT = rdr["text"].ToString();
                    resp.AUDITEE_REPLY = rdr["reply"].ToString();
                    resp.AUDITOR_RECOM = rdr["recommendation"].ToString();
                    resp.AMOUNT_INVOLVED = rdr["amount_involved"].ToString();
                    resp.NO_OF_INSTANCES = rdr["no_of_instances"].ToString();
                    resp.DSA_ISSUED = rdr["DSA"].ToString();
                    resp.RESPONSIBLE_PPNO = this.GetObservationResponsiblePPNOs(OBS_ID);
        public ObservationModel GetObservationDetailsByIdHO(int OBS_ID)
            var loggedInUser = sessionHandler.GetSessionUser();
            ObservationModel resp = new ObservationModel();
                cmd.CommandText = "pkg_hd.P_GET_OBSERVATION_DETAILS_FROM_ID_HO";
                cmd.Parameters.Add("obid", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    if (!string.IsNullOrEmpty(rdr["control_violation"].ToString()))
                        resp.PROCESS_ID = Convert.ToInt32(rdr["control_violation"].ToString());
                    if (!string.IsNullOrEmpty(rdr["nature_id"].ToString()))
                        resp.SUBCHECKLIST_ID = Convert.ToInt32(rdr["nature_id"].ToString());
                    resp.RISKMODEL_ID = Convert.ToInt32(rdr["severity"].ToString());
                    resp.HEADING = rdr["headings"].ToString();
                    resp.OBSERVATION_TEXT = rdr["text"].ToString();
                    resp.AUDITEE_REPLY = rdr["reply"].ToString();
                    resp.AUDITOR_RECOM = rdr["recommendation"].ToString();

            return resp;
        public ObservationModel GetObservationDetailsByIdForPreConcluding(int OBS_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            ObservationModel resp = new ObservationModel();
                cmd.CommandText = "pkg_hd.P_GET_OBSERVATION_DETAILS_FROM_ID_PRE_CON";
                cmd.Parameters.Add("obid", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    {

                    resp.ANNEXURE_ID = rdr["annex_id"].ToString();
                    resp.PROCESS_ID = Convert.ToInt32(rdr["t_id"].ToString());
                    resp.SUBCHECKLIST_ID = Convert.ToInt32(rdr["s_id"].ToString());
                    resp.CHECKLISTDETAIL_ID = Convert.ToInt32(rdr["d_id"].ToString());
                    resp.RISKMODEL_ID = Convert.ToInt32(rdr["severity"].ToString());
                    resp.HEADING = rdr["headings"].ToString();
                    resp.FINAL_PARA_NO = Convert.ToInt32(rdr["Final_PARA_NO"].ToString());
                    resp.OBSERVATION_TEXT = rdr["text"].ToString();
                    resp.AUDITEE_REPLY = rdr["reply"].ToString();
                    resp.AUDITOR_RECOM = rdr["recommendation"].ToString();
                    resp.HEAD_RECOM = rdr["head_recom"].ToString();
                    resp.QA_RECOM = rdr["qa_recom"].ToString();
                    resp.QA_GIST = rdr["qa_gist"].ToString();
                    resp.AMOUNT_INVOLVED = rdr["amount_involved"].ToString();
                    resp.NO_OF_INSTANCES = rdr["no_of_instances"].ToString();
                    resp.RESPONSIBLE_PPNO = this.GetObservationResponsiblePPNOs(OBS_ID);

                    }
        public ObservationModel GetObservationDetailsByIdForPreConcludingHO(int OBS_ID)
            var loggedInUser = sessionHandler.GetSessionUser();
            ObservationModel resp = new ObservationModel();
                cmd.CommandText = "pkg_hd.P_GET_OBSERVATION_DETAILS_FROM_ID_PRE_CON_HO";
                cmd.Parameters.Add("obid", OracleDbType.Int32).Value = OBS_ID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    resp.ANNEXURE_ID = rdr["annex_id"].ToString();
                    resp.PROCESS_ID = Convert.ToInt32(rdr["t_id"].ToString());
                    resp.SUBCHECKLIST_ID = Convert.ToInt32(rdr["s_id"].ToString());
                    resp.CHECKLISTDETAIL_ID = Convert.ToInt32(rdr["d_id"].ToString());
                    resp.RISKMODEL_ID = Convert.ToInt32(rdr["severity"].ToString());
                    resp.HEADING = rdr["headings"].ToString();
                    resp.OBSERVATION_TEXT = rdr["text"].ToString();
                    resp.AUDITEE_REPLY = rdr["reply"].ToString();
                    resp.AUDITOR_RECOM = rdr["recommendation"].ToString();
                    resp.HEAD_RECOM = rdr["head_recom"].ToString();
                    resp.QA_RECOM = rdr["qa_recom"].ToString();
                    resp.QA_GIST = rdr["qa_gist"].ToString();
                    resp.AMOUNT_INVOLVED = rdr["amount_involved"].ToString();
                    resp.NO_OF_INSTANCES = rdr["no_of_instances"].ToString();
                    resp.RESPONSIBLE_PPNO = this.GetObservationResponsiblePPNOs(OBS_ID);

            return resp;
        public string UpdateGMAndReportingLineOffice(int ENTITY_ID, int GM_OFF_ID, int REP_OFF_ID)
            if (GM_OFF_ID > 0)
                this.UpdateGMOffice(GM_OFF_ID, ENTITY_ID);

            if (REP_OFF_ID > 0)
                this.UpdateReportingLine(REP_OFF_ID, ENTITY_ID);

        public async Task<string> UploadAuditReport(int ENG_ID)

            var loggedInUser = sessionHandler.GetSessionUser();
            string resp = "";
            List<AuditeeResponseEvidenceModel> AUDIT_REPORT = new List<AuditeeResponseEvidenceModel>();
                AUDIT_REPORT = await this.GetUploadedAuditReportsFromDirectory(ENG_ID.ToString());
                int index = 1;
                if (AUDIT_REPORT != null)
                    if (AUDIT_REPORT.Count > 0)
                        {
                        foreach (var item in AUDIT_REPORT)
                            {
                            cmd.CommandText = "pkg_hd.P_UPLOAD_AUDIT_REPORT";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                            cmd.Parameters.Add("AREP", OracleDbType.Clob).Value = item.IMAGE_DATA;
                            cmd.Parameters.Add("REP_TYPE", OracleDbType.Varchar2).Value = item.IMAGE_TYPE;
                            cmd.Parameters.Add("REP_NAME", OracleDbType.Varchar2).Value = item.FILE_NAME;
                            cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                            cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                            cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                            cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                            OracleDataReader rdr = cmd.ExecuteReader();
                            while (rdr.Read())
                                {
                                resp = rdr["remarks"].ToString();
                                }
                            index++;
                            }
                        }

                this.DeleteAuditReportSubFolderDirectoryFromServer(ENG_ID.ToString());

            return resp;
        public List<FinalAuditReportModel> GetAuditReports(int ENG_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            List<FinalAuditReportModel> repList = new List<FinalAuditReportModel>();
            var loggedInUser = sessionHandler.GetSessionUser();


                cmd.CommandText = "pkg_hd.P_GET_FINAL_AUDIT_REPORT";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    {
                    FinalAuditReportModel z = new FinalAuditReportModel();
                    z.ID = rdr["ID"].ToString();
                    z.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    z.ENTITY_NAME = rdr["ENTITY_NAME"].ToString();
                    z.AUDIT_PERIOD = rdr["AUDIT_PERIOD"].ToString();
                    repList.Add(z);
                    }
            return repList;
        public AuditeeResponseEvidenceModel GetAuditReportContent(string FILE_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var resp = new AuditeeResponseEvidenceModel();
                cmd.CommandText = "pkg_hd.P_GET_AUDIT_REPORT_CONTENT";
                cmd.Parameters.Add("FILE_ID", OracleDbType.Varchar2).Value = FILE_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    resp = new AuditeeResponseEvidenceModel
                        {

                        FILE_ID = (rdr["id"].ToString()),
                        IMAGE_TYPE = (rdr["doc_type"].ToString()),
                        IMAGE_NAME = (rdr["doc_name"].ToString())
                        };

                    // Handle CLOB data
                    var clob = rdr.GetOracleClob(rdr.GetOrdinal("FILE_DATA"));
                    if (clob != null)
                        {
                        resp.IMAGE_DATA = clob.Value; // Get the entire CLOB data as a string
                        }

            return resp;
        public FinalAuditReportModel GetCheckAuditReportExisits(int ENG_ID)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            FinalAuditReportModel resp = new FinalAuditReportModel();
            var loggedInUser = sessionHandler.GetSessionUser();


                cmd.CommandText = "pkg_hd.P_GET_CHECK_AUDIT_REPORT_UPLOADED";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                    resp.ID = rdr["ID"].ToString();
                    resp.DOC_TYPE = rdr["doc_type"].ToString();
                    resp.DOC_NAME = rdr["doc_name"].ToString();
            return resp;














        public List<ObservationReversalModel> GetEngagementDetailsForFadReview(int ENTITY_ID = 0)
            List<ObservationReversalModel> resp = new List<ObservationReversalModel>();
                cmd.CommandText = "pkg_fad.p_get_audit_engagement";
                cmd.Parameters.Add("ent_id", OracleDbType.Int32).Value = ENTITY_ID;
                    ObservationReversalModel os = new ObservationReversalModel();
                    os.PLAN_ID = rdr["plan_id"].ToString();
                    os.ENG_ID = rdr["ENG_ID"].ToString();
                    os.TEAM_NAME = rdr["TEAM_NAME"].ToString();
                    os.AUDIT_START_DATE = rdr["AUDIT_STARTDATE"].ToString();
                    os.AUDIT_END_DATE = rdr["AUDIT_ENDDATE"].ToString();
                    os.OP_START_DATE = rdr["OP_STARTDATE"].ToString();
                    os.OP_END_DATE = rdr["OP_ENDDATE"].ToString();
                    os.ENTITY_ID = rdr["ENTITY_ID"].ToString();
                    os.AUDITED_BY_ID = rdr["Auditby_Id"].ToString();
                    os.STATUS_ID = rdr["STATUS_ID"].ToString();
                    os.STATUS = rdr["STATUS"].ToString();
                    os.REPORT_ID = rdr["RPT_ID"].ToString();
                    resp.Add(os);
            return resp;
        public List<EngagementObservationsForStatusReversalModel> GetAuditDetailsFAD(int ENG_ID = 0)
            List<EngagementObservationsForStatusReversalModel> resp = new List<EngagementObservationsForStatusReversalModel>();
                cmd.CommandText = "pkg_fad.p_get_audit_glance";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                    EngagementObservationsForStatusReversalModel os = new EngagementObservationsForStatusReversalModel();
                    os.ID = rdr["ID"].ToString();
                    os.MEMO_NO = rdr["MEMO_NO"].ToString();
                    os.FINAL_PARA = rdr["FINAL_PARA_NO"].ToString();
                    os.GIST = rdr["GIST"].ToString();
                    os.MEMO_DATE = rdr["MEMO_DATE"].ToString();
                    os.HEADING = rdr["HEADINGS"].ToString();
                    os.RISK = rdr["RISK"].ToString();
                    os.STATUS = rdr["STATUS"].ToString();
                    resp.Add(os);
            return resp;
        public List<FADAuditParasReviewModel> GetObservationDetailsForReport(int OBS_ID = 0)
            List<FADAuditParasReviewModel> resp = new List<FADAuditParasReviewModel>();
                cmd.CommandText = "pkg_fad.p_get_audit_observtion";
                cmd.Parameters.Add("OB_ID", OracleDbType.Int32).Value = OBS_ID;
                    FADAuditParasReviewModel os = new FADAuditParasReviewModel();
                    os.MEMO_NO = rdr["MEMO"].ToString();
                    os.PARA_NO = rdr["PARA_NO"].ToString();
                    os.ANNEX = rdr["ANNEX"].ToString();
                    os.PROCESS = rdr["HEADINGS"].ToString();
                    os.SUB_PROCESS = rdr["ASSIGNED_TO"].ToString();
                    os.CHECK_LIST = rdr["CHECK_LIST"].ToString();
                    os.OBS_GIST = rdr["GIST"].ToString();
                    os.PARA_TEXT = rdr["PARA_TEXT"].ToString();
                    os.AMOUNT_INV = rdr["AMOUNT_INV"].ToString();
                    os.NO_INSTANCES = rdr["NO_INSTANCES"].ToString();
                    os.PPNO = rdr["PPNO"].ToString();
                    os.RESP_ROLE = rdr["RESP_ROLE"].ToString();
                    os.RESP_AMOUNT = rdr["RESP_AMOUNT"].ToString();
                    os.AUDITEE_REPLY = rdr["auditee_reply"].ToString();
                    os.AUDITOR_COMMENTS = rdr["auditor_comments"].ToString();
                    os.HEADCOMMENTS = rdr["HEAD_COMMENTS"].ToString();
                    os.ROOT_CAUSE = rdr["ROOT_CAUSE"].ToString();
                    resp.Add(os);
            return resp;
        public List<AuditReportModel> GetAuditReportForFadReview(int RPT_ID = 0, int ENG_ID = 0)
            List<AuditReportModel> resp = new List<AuditReportModel>();
                cmd.CommandText = "pkg_fad.p_get_audit_Report";
                cmd.Parameters.Add("ENGID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("RPT_ID", OracleDbType.Int32).Value = RPT_ID;
                    AuditReportModel or = new AuditReportModel();
                    or.ID = rdr["id"].ToString();
                    or.ENG_ID = rdr["eng_id"].ToString();
                    or.AUDIT_REPORT = rdr["audit_report"].ToString();
                    or.DOC_TYPE = rdr["doc_type"].ToString();
                    resp.Add(or);
            return resp;
        public List<MenuPagesModel> GetMenuPagesId(string Page_Path)
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<MenuPagesModel> modelList = new List<MenuPagesModel>();
                cmd.CommandText = "pkg_lg.p_GetTopMenuPages";
                cmd.Parameters.Add("Page_Path", OracleDbType.Varchar2).Value = Page_Path;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                    MenuPagesModel menuPage = new MenuPagesModel();
                    menuPage.Id = Convert.ToInt32(rdr["ID"]);
                    menuPage.Menu_Id = Convert.ToInt32(rdr["MENU_ID"]);
                    menuPage.Page_Name = rdr["PAGE_NAME"].ToString();
                    menuPage.Page_Path = rdr["PAGE_PATH"].ToString();
                    menuPage.Page_Order = Convert.ToInt32(rdr["PAGE_ORDER"]);
                    menuPage.Status = rdr["STATUS"].ToString();
                    menuPage.Sub_Menu = rdr["Sub_Menu"].ToString();
                    menuPage.Sub_Menu_Id = rdr["Sub_Menu_Id"].ToString();
                    menuPage.Sub_Menu_Name = rdr["Sub_Menu_Name"].ToString();
                    menuPage.Status = rdr["STATUS"].ToString();
                    if (rdr["HIDE_MENU"].ToString() != null && rdr["HIDE_MENU"].ToString() != "")
                        menuPage.Hide_Menu = Convert.ToInt32(rdr["HIDE_MENU"]);
                    modelList.Add(menuPage);
            return modelList;
















