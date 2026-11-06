namespace MyProject.Helpers
{
    public static class CurrentSession
    {
        public static int? UserId { get; private set; }
        public static string Email { get; private set; } = "";
        public static string RoleName { get; private set; } = "";
        public static bool IsAuthenticated => UserId.HasValue;

        public static void Start(int userId, string email, string roleName)
        {
            UserId = userId;
            Email = email;
            RoleName = roleName;
        }

        public static void Clear()
        {
            UserId = null;
            Email = "";
            RoleName = "";
        }
    }
}