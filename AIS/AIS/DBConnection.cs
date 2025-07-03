                cmd.Parameters.Add("ANNEXURE_REF_ID", OracleDbType.Int32).Value = string.IsNullOrEmpty(pm.ANNEXURE_REF_ID) ? 0 : int.Parse(pm.ANNEXURE_REF_ID);
