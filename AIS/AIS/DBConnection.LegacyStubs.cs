using System.Collections.Generic;
using System.Threading.Tasks;
using AIS.Models;

namespace AIS.Controllers
{
    public partial class DBConnection
    {
        // Legacy stub methods added to satisfy references throughout the application

        public int GetLoggedInUserEngId()
        {
            return 0;
        }

        public string GetRiskDescByID(int risk_id = 0)
        {
            return string.Empty;
        }

        public int GetExpectedCountOfAuditEntitiesOnCriteria(int CRITERIA_ID)
        {
            return 0;
        }

        public Task<List<AuditeeResponseEvidenceModel>> GetAttachedFilesFromDirectory(string subfolder)
        {
            return Task.FromResult(new List<AuditeeResponseEvidenceModel>());
        }

        public Task<List<AuditeeResponseEvidenceModel>> GetAttachedAuditeeEvidencesFromDirectory(string subfolder)
        {
            return Task.FromResult(new List<AuditeeResponseEvidenceModel>());
        }

        public Task<List<AuditeeResponseEvidenceModel>> GetAttachedCAUEvidencesFromDirectory(string subfolder)
        {
            return Task.FromResult(new List<AuditeeResponseEvidenceModel>());
        }

        public bool DeleteSubFolderDirectoryFromServer(string subfolder)
        {
            return false;
        }

        public bool DeleteSubFolderDirectoryInAuditeeEvidenceFromServer(string subfolder)
        {
            return false;
        }

        public bool DeleteSubFolderDirectoryInCAUEvidenceFromServer(string subfolder)
        {
            return false;
        }
    }
}

