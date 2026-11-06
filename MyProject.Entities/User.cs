namespace MyProject.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Email { get; set; } = "";
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public DateTime? BirthDate { get; set; }
        public string PasswordHash { get; set; } = "";
        public string? PasswordSalt { get; set; }
        public int? PasswordIterations { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool MustChangePassword { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public int? CreatedBy { get; set; }

        public string? RoleName { get; set; }
        public string FullName => $"{FirstName} {LastName}".Trim();
    }
}