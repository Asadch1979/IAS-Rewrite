        public string AddResponsiblePersonsToObservation(int NEW_PARA_ID, int OLD_PARA_ID, string INDICATOR, ObservationResponsiblePPNOModel RESPONSIBLE, int paraStatus)
                cmd.CommandText = paraStatus < 8 ? "pkg_ar.P_responibilityassigned" : "pkg_ar.P_Update_responsibility";
                if (paraStatus >= 8)
                    cmd.Parameters.Add("O_Para_ID", OracleDbType.Int32).Value = OLD_PARA_ID;
                if (paraStatus >= 8)
                    cmd.Parameters.Add("U_D_action", OracleDbType.Varchar2).Value = RESPONSIBLE.ACTION;
