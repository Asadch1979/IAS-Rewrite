# IAS-Rewrite

This project now includes a basic SBP Compliance workflow found under
`Views/Complaince/SBPCompliance`. It contains placeholder screens for each
stage of the SBP observation process.

The backend exposes API endpoints for managing SBP observations under
`ApiCallsController`. These endpoints wrap the Oracle package procedures in
`PKG_T_AU_SBP_COMPLIANCE` for adding observations, assignments, responses and
review history.
