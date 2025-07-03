                cmd.Parameters.Add("ANNEXURE_REF_ID", OracleDbType.Int32).Value = string.IsNullOrEmpty(pm.ANNEXURE_REF_ID) ? 0 : Convert.ToInt32(pm.ANNEXURE_REF_ID);
