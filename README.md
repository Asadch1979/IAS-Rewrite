# IAS-Rewrite

This project now includes a basic SBP Compliance workflow found under
`Views/FAD`. It contains placeholder screens for each
stage of the SBP observation process.

The backend exposes API endpoints for managing SBP observations under
`ApiCallsController`. These endpoints wrap the Oracle package procedures in
`PKG_T_AU_SBP_COMPLIANCE` for adding observations, assignments, responses and
review history.

## Updated SBP Compliance Flow

The workflow now includes an Internal Audit validation step. The high level process is:

1. **Compliance/ICFR** records an observation and assigns it to the relevant division.
2. **Div/Group Head** assigns the observation to a department with instructions.
3. **Department** enters its response.
4. **Div/Group Head** reviews and forwards (or rejects).
5. **Group Head** reviews and forwards (or rejects).
6. **Compliance/COO** performs a final review and forwards the case to **Internal Audit**.
7. **Internal Audit** either validates the observation, refers it back, or raises a query.
   Referred or queried observations return to Compliance/ICFR and the cycle repeats.

### Diagram

```
[Compliance/ICFR] --(Assign)--> [Div/Group Head] --(Assign)--> [Department] --(Response)-->
[Div/Group Head] --(Review)--> [Group Head] --(Review)--> [Compliance/COO] --(Review)-->
[Internal Audit]
      |        |
      |        +-- (Refer Back / Query) --> [Compliance/ICFR] --(cycle repeats)
      |
      +-- (Validate) --> [CLOSE]
```

### Role Matrix

| **Role**        | **What They See**                                      | **What They Can Do**                                                                            | **Views Used**                                                     |
| --------------- | ------------------------------------------------------ | ----------------------------------------------------------------------------------------------- | ------------------------------------------------------------------ |
| Compliance/ICFR | All observations, full workflow, referred back/queried | Add SBP observation, assign to Division, respond to Audit queries/referrals, view full workflow | Index, AddObservation, AssignDivision, ViewHistory, RespondToAudit |
| Div/Group Head  | Assigned observations, dept assignment, history        | Assign to department, review/approve/reject, view workflow                                      | AssignDepartment, ReviewResponse, ViewHistory                      |
| Department      | Assigned observations, response form, workflow history | Enter compliance response, upload docs, submit response, view workflow                          | EnterResponse, ViewHistory                                         |
| Group Head      | Observations for group                                 | Review, forward, reject, view workflow                                                          | ReviewByGroupHead, ViewHistory                                     |
| Compliance/COO  | Escalated obs, workflow history                        | Final review, forward to Audit, return/reject, view workflow                                    | ReviewByCompliance, ViewHistory                                    |
| Internal Audit  | All obs, responses, history                            | Validate (close), refer back/reject (with remarks), raise query (request info/evidence)         | AuditValidation, ViewHistory                                       |

The SBP Compliance actions are now handled by `FADController`, which
includes an `AuditValidation` action and the corresponding view for
Internal Audit to perform these steps.

### API Endpoint

The `ApiCallsController` exposes an `sbp_audit_validation` endpoint which wraps
the `ProcessSBPAuditValidation` method in `DBConnection`. This endpoint accepts
the observation id, action and remarks so that the Internal Audit team can
validate, refer back or raise queries on an observation.

The new `ViewHistory` page displays the review timeline for each observation.

The `AddObservation` screen now submits data through the `sbp_add_observation`
API endpoint, which in turn calls `DBConnection.AddSBPObservation` to execute
the `PKG_T_AU_SBP_COMPLIANCE.ADD_OBSERVATION` stored procedure.

## Commercial Audit Observation Module

The Commercial Audit (CA) menu provides a basic ARPSE PARA workflow.  Screens exist for creating observations, assigning them through the various review stages and capturing legacy data.

`Controllers/CA/CaController` exposes actions for each stage (`Index`, `CreateObservation`, `AssignObservation`, `AssignToDepartment`, `DepartmentResponse`, `ReviewObservation`, `HeadFADReview`, `LegacyEntry` and `ObservationAuditTrail`).  Each action checks role based permissions and loads the common top menu lists.

`ApiCallsController` now contains stubbed endpoints such as `ca_submit_to_headfad`, `ca_assign_division`, `ca_department_response` and others.  These call corresponding placeholder methods in `DBConnection` which will later persist Commercial Audit data.

The `LegacyEntry` page allows authorized users to capture historical observations with fields for year, text, division, department, status and supporting documents.  Submitted data is posted to the `ca_enter_legacy_observation` endpoint.
