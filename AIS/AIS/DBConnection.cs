        public void ProcessSBPAuditValidation(int observationId, string action, string remarks)
            {
            sessionHandler = new SessionHandler();
            sessionHandler._httpCon = this._httpCon;
            sessionHandler._session = this._session; sessionHandler._configuration = this._configuration;
            var loggedInUser = sessionHandler.GetSessionUser();

            this.AddSBPReviewHistory(observationId, "Internal Audit", loggedInUser.PPNumber, remarks, action);

            if (action == "Validate")
                this.UpdateSBPObservationStatus(observationId, "CLOSED");
            else if (action == "ReferBack")
                this.UpdateSBPObservationStatus(observationId, "REFERRED BACK");
            else if (action == "RaiseQuery")
                this.UpdateSBPObservationStatus(observationId, "QUERY RAISED");
            }

