                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
                AddPageIdParameter(cmd);
        protected void AddPageIdParameter(OracleCommand cmd)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            int pageId = sessionHandler.GetPageId();
            cmd.Parameters.Add("page_id", OracleDbType.Int32).Value = pageId;
            }

