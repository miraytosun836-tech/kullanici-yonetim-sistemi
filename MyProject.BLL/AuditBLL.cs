using MyProject.DAL;

namespace MyProject.BLL
{
    public class AuditBLL
    {
        private readonly AuditLogDAL _dal = new AuditLogDAL();

        public void Write(string eventType, string? description, int? userId = null)
        {
            _dal.Insert(userId, eventType, description);
        }
    }
}