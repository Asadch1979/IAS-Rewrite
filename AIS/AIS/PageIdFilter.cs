using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AIS
{
    public class PageIdFilter : IActionFilter
    {
        private readonly DBConnection _dbConnection;
        private readonly SessionHandler _sessionHandler;
        private readonly IHttpContextAccessor _accessor;

        public PageIdFilter(DBConnection dbConnection, SessionHandler sessionHandler, IHttpContextAccessor accessor)
        {
            _dbConnection = dbConnection;
            _sessionHandler = sessionHandler;
            _accessor = accessor;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var path = context.HttpContext.Request.Path.Value ?? string.Empty;
            int pageId = _dbConnection.GetPageIdByPath(path);

            _sessionHandler._httpCon = _accessor;
            _sessionHandler._session = _accessor.HttpContext.Session;
            _sessionHandler._configuration = _dbConnection._configuration;
            if (pageId > 0)
                _sessionHandler.SetPageId(pageId);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
