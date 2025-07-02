            ob.ANNEXURE_REF_ID = annexureRefId;

        public string SaveAuditObservationCAU(ObservationModel ob, int annexureRefId)
                cmd.Parameters.Add("ANNEXURE_REF_ID", OracleDbType.Int32).Value = ob.ANNEXURE_REF_ID;
        public AnnexureInstructionModel UpdateAnnexureInstructions(int annexureId,
                    cmd.Parameters.Add("o_annexure_id", OracleDbType.Int32).Direction = ParameterDirection.Output;
                    return new AnnexureInstructionModel
                        {
                        InstructionsTitle = instructionTitle,
                        InstructionsDate = instructionDate,
                        InstructionsDetails = instructionDetails,
                        AnnexureRefId = cmd.Parameters["o_annexure_id"].Value == DBNull.Value ? 0 : Convert.ToInt32(cmd.Parameters["o_annexure_id"].Value)
                        };
