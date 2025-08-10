using AIS.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;

namespace AIS.Controllers
    {
    public partial class DBConnection
        {
        public string CreateSampleDataAfterEngagementApproval(int ENG_ID)
            {
            string resp = "";
            string email = "";
            string email_cc = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_add_sample_data";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["REMARKS"].ToString();
                    //email = rdr["email"].ToString();
                    // email_cc = rdr["email_cc"].ToString();
                    }
                }
            con.Dispose();

            if (resp == "N")
                {
                EmailNotification.NotifyAuditSampleIssue(ENG_ID.ToString(), email, email_cc);
                }
            return resp;
            }
        

        public string CreateExceptionDataAfterEngagementApproval(int ENG_ID)
            {
            string resp = "";
            string email = "";
            string email_cc = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            var con = this.DatabaseConnection(); con.Open();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_add_exception_data";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["REMARKS"].ToString();
                    //email = rdr["email"].ToString();
                    // email_cc = rdr["email_cc"].ToString();
                    }
                }
            con.Dispose();

            if (resp == "N")
                {
                EmailNotification.NotifyAuditExceptionIssue(ENG_ID.ToString(), email, email_cc);
                }
            return resp;
            }

        public List<BiometSamplingModel> GetBiometSamplingDetails(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<BiometSamplingModel> responseList = new List<BiometSamplingModel>();
            var loggedInUser = sessionHandler.GetSessionUser();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_Account";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        BiometSamplingModel record = new BiometSamplingModel()
                            {
                            BRANCH_CODE = rdr["branchcode"].ToString(),
                            ACCOUNT_NO = rdr["oldaccountno"].ToString(),
                            ACCOUNT_TITLE = rdr["name"].ToString(),
                            CUSTOMER_NAME = rdr["customername"].ToString(),
                            DOB = rdr["dob"].ToString(),
                            PHONE_CELL = rdr["phonecell"].ToString(),
                            CNIC = rdr["cnic"].ToString(),
                            CNIC_EXPIRY_DATE = rdr["cnicexpirydate"].ToString(),
                            OPENING_DATE = rdr["openingdate"].ToString(),
                            BMVS_VERIFIED = rdr["bmvs_verified"].ToString(),
                            PURPOSE = rdr["purpose"].ToString(),
                            ACCOUNT_TYPE = rdr["acc_type"].ToString(),
                            ACCOUNT_CATEGORY = rdr["acc_category"].ToString(),
                            RISK = rdr["risk"].ToString()
                            };

                        responseList.Add(record);
                        }
                    }
                }
            con.Dispose();
            return responseList;
            }

        public List<AccountTransactionSampleModel> GetBiometAccountTransactionSamplingDetails(int ENG_ID, string AC_NO)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<AccountTransactionSampleModel> responseList = new List<AccountTransactionSampleModel>();
            var loggedInUser = sessionHandler.GetSessionUser();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_account_transcations";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("AC_number", OracleDbType.Varchar2).Value = AC_NO;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        AccountTransactionSampleModel record = new AccountTransactionSampleModel()
                            {
                            TransactionMasterCode = rdr["transactionmastercode"].ToString(),
                            Description = rdr["description"].ToString(),
                            Remarks = rdr["REMARKS"].ToString(),
                            TransactionDate = rdr["transactiondate"].ToString(),
                            AuthorizationDate = rdr["authorizationdate"].ToString(),
                            DrAmount = rdr["dramount"].ToString(),
                            CrAmount = rdr["cramount"].ToString(),
                            ToAccountId = rdr["toaccountid"].ToString(),
                            ToAccountTitle = rdr["toaccounttitle"].ToString(),
                            ToAccountNo = rdr["toaccountno"].ToString(),
                            ToAccBranchId = rdr["to_acc_branchid"].ToString(),
                            InstrumentNo = rdr["instrumentno"].ToString()
                            };
                        responseList.Add(record);
                        }
                    }
                }
            con.Dispose();
            return responseList;
            }

        public List<AccountDocumentBiometSamplingModel> GetBiometAccountDocumentsSamplingDetails(string AC_NO)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;

            var con = this.DatabaseConnection();
            con.Open();

            List<AccountDocumentBiometSamplingModel> responseList = new List<AccountDocumentBiometSamplingModel>();
            var loggedInUser = sessionHandler.GetSessionUser();

            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_GET_ACCOUNT_DOC ";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("AC_number", OracleDbType.Varchar2).Value = AC_NO;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                using (OracleDataReader rdr = cmd.ExecuteReader())
                    {
                    while (rdr.Read())
                        {
                        AccountDocumentBiometSamplingModel record = new AccountDocumentBiometSamplingModel()
                            {
                            OldAccountNo = rdr["OLDACCOUNTNO"].ToString(),
                            PageNo = rdr["PAGENO"].ToString(),
                            Name = rdr["NAME"].ToString(),
                            DocImage = rdr["DOC_IMAGE"] as byte[], // Assuming DOC_IMAGE is a BLOB in the database
                            DocRemarks = rdr["DOC_REMARKS"].ToString()
                            };
                        responseList.Add(record);
                        }
                    }
                }
            con.Dispose();
            return responseList;
            }

        public List<ListOfSamplesModel> GetListOfSamples(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ListOfSamplesModel> list = new List<ListOfSamplesModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_GET_SAMPLE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;

                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ListOfSamplesModel chk = new ListOfSamplesModel();
                    chk.SAMPLE_ID = Convert.ToInt32(rdr["S_ID"].ToString());
                    chk.SAMPLE_TYPE = rdr["SAMPLE_TYPE"].ToString();
                    chk.SAMPLE_PERCENTAGE = rdr["SAMPLE_PERCENTAGE"].ToString();
                    chk.TOTAL_COUNT = rdr["samp_tot"].ToString();
                    chk.SAMPLE_COUNT = rdr["sample_final"].ToString();
                    chk.LOAN_STATUS = rdr["sample_final"].ToString();
                    chk.SAMPLE_INDICATOR = rdr["IND"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public List<LoanCaseSampleModel> GetLoanSamples(string INDICATOR, int STATUS_ID, int ENG_ID, int SAMPLE_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanCaseSampleModel> list = new List<LoanCaseSampleModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_GET_LOANS_SAMPLE";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                // cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("S_ID", OracleDbType.Varchar2).Value = SAMPLE_ID;
                cmd.Parameters.Add("LStatus", OracleDbType.Int32).Value = STATUS_ID;
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanCaseSampleModel chk = new LoanCaseSampleModel();
                    chk.LOAN_DISB_ID = rdr["loan_disb_id"].ToString();
                    chk.TYPE = rdr["TYPE"].ToString();
                    chk.SCHEME = rdr["SCHEME"].ToString();
                    chk.L_PURPOSE = rdr["L_PURPOSE"].ToString();
                    chk.LC_NO = rdr["LC_NO"].ToString();
                    chk.CNIC = rdr["CNIC"].ToString();
                    chk.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    chk.APP_DATE = Convert.ToDateTime(rdr["APP_DATE"]);
                    chk.DISB_DATE = Convert.ToDateTime(rdr["DISB_DATE"]);
                    chk.DEV_AMOUNT = Convert.ToDecimal(rdr["DEV_AMOUNT"]);
                    chk.OUTSTANDING = Convert.ToDecimal(rdr["OUTSTANDING"]);
                    list.Add(chk);
                    }

                }
            con.Dispose();
            return list;
            }

        public List<LoanCaseSampleModel> GetLoanExceptions(string INDICATOR, int STATUS_ID, int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanCaseSampleModel> list = new List<LoanCaseSampleModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_GET_LOANS_Exceptions";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                // cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("LStatus", OracleDbType.Int32).Value = STATUS_ID;
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanCaseSampleModel chk = new LoanCaseSampleModel();
                    chk.LOAN_DISB_ID = rdr["loan_disb_id"].ToString();
                    chk.TYPE = rdr["TYPE"].ToString();
                    chk.SCHEME = rdr["SCHEME"].ToString();
                    chk.L_PURPOSE = rdr["L_PURPOSE"].ToString();
                    chk.LC_NO = rdr["LC_NO"].ToString();
                    chk.CNIC = rdr["CNIC"].ToString();
                    chk.CUSTOMERNAME = rdr["CUSTOMERNAME"].ToString();
                    chk.APP_DATE = Convert.ToDateTime(rdr["APP_DATE"]);
                    chk.DISB_DATE = Convert.ToDateTime(rdr["DISB_DATE"]);
                    chk.DEV_AMOUNT = Convert.ToDecimal(rdr["DEV_AMOUNT"]);
                    chk.OUTSTANDING = Convert.ToDecimal(rdr["OUTSTANDING"]);
                    list.Add(chk);
                    }

                }
            con.Dispose();
            return list;
            }

        public List<LoanCaseSampleDocumentsModel> GetLoanSamplesDocuments(int ENG_ID, string LOAN_DISB_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanCaseSampleDocumentsModel> list = new List<LoanCaseSampleDocumentsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_Loan_Documents";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                // cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("E_ID", OracleDbType.Varchar2).Value = ENG_ID;
                cmd.Parameters.Add("L_DISB_ID", OracleDbType.Varchar2).Value = LOAN_DISB_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanCaseSampleDocumentsModel chk = new LoanCaseSampleDocumentsModel();
                    chk.IMAGE_ID = rdr["IMAGEID"].ToString();
                    chk.BRANCH_CODE = rdr["branchcode"].ToString();
                    chk.LOAN_APP_ID = rdr["loan_app_id"].ToString();
                    chk.CNIC = rdr["cnic"].ToString();
                    chk.CUSTOMER_NAME = rdr["customername"].ToString();
                    chk.LOAN_CASE_NO = rdr["loan_case_no"].ToString();
                    chk.LOAN_DISB_ID = rdr["loan_disb_id"].ToString();
                    chk.DOC_NAME = rdr["docname"].ToString();
                    list.Add(chk);

                    }

                }
            con.Dispose();
            return list;
            }

        public List<LoanCaseSampleDocumentsModel> GetLoanSamplesDocumentData(int IMAGE_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanCaseSampleDocumentsModel> list = new List<LoanCaseSampleDocumentsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_Loan_Documents_image";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                // cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = INDICATOR;
                cmd.Parameters.Add("image_ID", OracleDbType.Varchar2).Value = IMAGE_ID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanCaseSampleDocumentsModel chk = new LoanCaseSampleDocumentsModel();

                    if (rdr["imagedata"] != DBNull.Value)
                        {
                        byte[] imageBytes = (byte[])rdr["imagedata"];
                        chk.IMAGE_DATA = Convert.ToBase64String(imageBytes);
                        }
                    else
                        {
                        chk.IMAGE_DATA = string.Empty;
                        }

                    list.Add(chk);
                    }


                }
            con.Dispose();
            return list;
            }

        public List<LoanCaseSampleTransactionsModel> GetLoanSamplesTransactions(int ENG_ID, string LOAN_DISB_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<LoanCaseSampleTransactionsModel> list = new List<LoanCaseSampleTransactionsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_Loan_Transactions";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Varchar2).Value = ENG_ID;
                cmd.Parameters.Add("L_DISB_ID", OracleDbType.Varchar2).Value = LOAN_DISB_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    LoanCaseSampleTransactionsModel chk = new LoanCaseSampleTransactionsModel
                        {
                        DESCRIPTION = rdr["description"]?.ToString(),
                        MANUAL_VOUCHER_NO = rdr["manualvoucherno"]?.ToString(),
                        TRANSACTION_DATE = rdr["transactiondate"]?.ToString(),
                        DR_AMOUNT = rdr["dramount"] != DBNull.Value ? Convert.ToDecimal(rdr["dramount"]) : 0,
                        CR_AMOUNT = rdr["cramount"] != DBNull.Value ? Convert.ToDecimal(rdr["cramount"]) : 0,
                        LN_ACCOUNT_ID = rdr["ln_accountid"]?.ToString(),
                        CREATED_ON = rdr["createdon"]?.ToString(),
                        REMARKS = rdr["remarks"]?.ToString(),
                        REJECTION_DATE = rdr["rejectiondate"]?.ToString(),
                        REVERSAL_DATE = rdr["reversaldate"]?.ToString(),
                        WORKING_DATE = rdr["workingdate"]?.ToString(),
                        AUTHORIZATION_DATE = rdr["authorizationdate"]?.ToString(),
                        MCO_RECEIPT_NO = rdr["mco_receipt_no"]?.ToString(),
                        MCO_BOOK_NO = rdr["mco_book_no"]?.ToString()
                        };

                    list.Add(chk);
                    }


                }
            con.Dispose();
            return list;
            }

        public List<AuditeeEntitiesModel> GetSampleEntities()
            {

            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<AuditeeEntitiesModel> entitiesList = new List<AuditeeEntitiesModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_GET_SAMPLE_ENTITIES";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    AuditeeEntitiesModel entity = new AuditeeEntitiesModel();
                    if (rdr["eng_id"].ToString() != "" && rdr["eng_id"].ToString() != null)
                        entity.ENG_ID = Convert.ToInt32(rdr["eng_id"]);

                    if (rdr["E_NAME"].ToString() != "" && rdr["E_NAME"].ToString() != null)
                        entity.NAME = rdr["E_NAME"].ToString();

                    entitiesList.Add(entity);
                    }
                }
            con.Dispose();
            return entitiesList;

            }

        public string RegenerateSampleofLoan(int ENG_ID, int LOAN_SAMPLE_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            string resp = "";
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_add_sample_data_update";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Varchar2).Value = ENG_ID;
                cmd.Parameters.Add("SID", OracleDbType.Varchar2).Value = LOAN_SAMPLE_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["remarks"].ToString();
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<CDMSMasterTransactionModel> GetCDMSMasterTransactions(string ENTITY_ID, DateTime START_DATE, DateTime END_DATE, string CNIC_NO, string ACC_NO)
            {

            var con = this.DatabaseConnection(); con.Open();
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();
            List<CDMSMasterTransactionModel> list = new List<CDMSMasterTransactionModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_get_account_transcations_master";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENTITY_ID;
                cmd.Parameters.Add("AC_number", OracleDbType.Varchar2).Value = ACC_NO;
                cmd.Parameters.Add("CNIC_NO", OracleDbType.Varchar2).Value = CNIC_NO;
                cmd.Parameters.Add("ST_DATE", OracleDbType.Date).Value = START_DATE;
                cmd.Parameters.Add("ED_DATE", OracleDbType.Date).Value = END_DATE;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                    {
                    CDMSMasterTransactionModel m = new CDMSMasterTransactionModel
                        {
                        TRANSACTION_ID = rdr["transactionid"]?.ToString(),
                        ENTITY_NAME = rdr["b_name"]?.ToString(),
                        OLD_ACCOUNT_NO = rdr["oldaccountno"]?.ToString(),
                        CNIC = rdr["cnic"]?.ToString(),
                        ACCOUNT_NAME = rdr["title"]?.ToString(),
                        CUSTOMER_NAME = rdr["customername"]?.ToString(),
                        TR_MASTER_CODE = rdr["transactionmastercode"]?.ToString(),
                        DESCRIPTION = rdr["description"]?.ToString(),
                        REMARKS = rdr["remarks"]?.ToString(),
                        TRANSACTION_DATE = rdr["transactiondate"]?.ToString(),
                        AUTHORIZATION_DATE = rdr["authorizationdate"]?.ToString(),
                        DR_AMOUNT = rdr["dramount"]?.ToString(),
                        CR_AMOUNT = rdr["cramount"]?.ToString(),
                        TO_ACCOUNT_ID = rdr["toaccountid"]?.ToString(),
                        TO_ACCOUNT_TITLE = rdr["toaccounttitle"]?.ToString(),
                        TO_ACCOUNT_NO = rdr["toaccountno"]?.ToString(),
                        TO_ACC_BRANCH_ID = rdr["to_acc_branchid"]?.ToString(),
                        INSTRUMENT_NO = rdr["instrumentno"]?.ToString()
                        };
                    list.Add(m);
                    }
                }
            con.Dispose();
            return list;

            }

        public List<ListOfReportsModel> GetListOfreports(int ENG_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            List<ListOfReportsModel> list = new List<ListOfReportsModel>();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.T_AU_EXCEPTION_REPORT";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    ListOfReportsModel chk = new ListOfReportsModel();
                    chk.REPORT_ID = Convert.ToInt32(rdr["R_ID"].ToString());
                    chk.REPORT_TITLE = rdr["REPORT_TITLE"].ToString();
                    chk.DISCRIPTION = rdr["DISCRIPTION"].ToString();
                    chk.LOAN_STATUS = rdr["loan_status"].ToString();
                    chk.REPORT_INDICATOR = rdr["IND"].ToString();
                    list.Add(chk);
                    }
                }
            con.Dispose();
            return list;
            }

        public string AddExceptionAccountReport(string IND, int REPORT_ID, string REPORT_TITLE, string DESCRIPTION, string TYPE, int LOAN_STATUS_ID)

            {
            // NOTE: duplicate removed during partials normalization (see de-dup rules).
            string resp = "";
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection(); con.Open();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.P_Add_new_exp_report";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("IND", OracleDbType.Varchar2).Value = IND;
                cmd.Parameters.Add("REPORT_ID", OracleDbType.Int32).Value = REPORT_ID;
                cmd.Parameters.Add("REPORT_TITLE", OracleDbType.Varchar2).Value = REPORT_TITLE;
                cmd.Parameters.Add("DESCRIPTION", OracleDbType.Varchar2).Value = DESCRIPTION;
                cmd.Parameters.Add("R_TYPE", OracleDbType.Varchar2).Value = TYPE;
                cmd.Parameters.Add("L_Status", OracleDbType.Int32).Value = LOAN_STATUS_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                OracleDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                    {
                    resp = rdr["remarks"].ToString();
                    }
                }
            con.Dispose();
            return resp;
            }

        public List<AccountExceptionsModel> GetAccountExceptions(int ENG_ID, int RPT_ID)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session;
            sessionHandler._configuration = this._configuration;
            var con = this.DatabaseConnection();
            con.Open();
            List<AccountExceptionsModel> responseList = new List<AccountExceptionsModel>();
            var loggedInUser = sessionHandler.GetSessionUser();
            using (OracleCommand cmd = con.CreateCommand())
                {
                cmd.CommandText = "pkg_sm.p_exception_get_Account";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.Add("E_ID", OracleDbType.Int32).Value = ENG_ID;
                cmd.Parameters.Add("RPTID", OracleDbType.Int32).Value = RPT_ID;
                cmd.Parameters.Add("P_NO", OracleDbType.Int32).Value = loggedInUser.PPNumber;
                cmd.Parameters.Add("R_ID", OracleDbType.Int32).Value = loggedInUser.UserRoleID;
                cmd.Parameters.Add("ENT_ID", OracleDbType.Int32).Value = loggedInUser.UserEntityID;
                cmd.Parameters.Add("T_CURSOR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                using (OracleDataReader rdr = cmd.ExecuteReader())
                    while (rdr.Read())
                        {
                        AccountExceptionsModel rd = new AccountExceptionsModel();
                        rd.ACCOUNT_NO = rdr["oldaccountno"].ToString();
                        rd.ACCOUNT_TITLE = rdr["title"].ToString();
                        rd.CUSTOMER_NAME = rdr["customername"].ToString();
                        rd.MASTER_CODE = rdr["transactionmastercode"].ToString();
                        rd.TR_DESCRIPTION = rdr["description"].ToString();
                        rd.TR_DATE = rdr["tr_date"].ToString();
                        rd.TR_AUTHDATE = rdr["tr_authdate"].ToString();
                        rd.DR_AMOUNT = rdr["dramount"].ToString();
                        rd.CR_AMOUNT = rdr["cramount"].ToString();

                        responseList.Add(rd);
                        }

                }
            con.Dispose();
            return responseList;
            }

        }
    }

