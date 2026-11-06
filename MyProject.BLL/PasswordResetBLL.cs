using MyProject.DAL;
using MyProject.Entities;
using MyProject.Helpers;

namespace MyProject.BLL
{
    public class ResetRequestResult
    {
        public string Message { get; set; } =
            "Hesap mevcutsa parola sıfırlama kodu oluşturuldu.";
        public string? TokenForTest { get; set; }
    }

    public class PasswordResetBLL
    {
        private readonly UserDAL _userDal = new UserDAL();
        private readonly PasswordResetTokenDAL _tokenDal = new PasswordResetTokenDAL();
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private readonly PasswordHistoryDAL _historyDal = new PasswordHistoryDAL();
        private readonly AuditBLL _audit = new AuditBLL();
        public ResetRequestResult RequestReset(string email)
        {
            var result = new ResetRequestResult();

            User? user = _userDal.GetByEmail(email.Trim());
            if (user == null || !user.IsActive)
                return result;

            string token = TokenGenerator.CreateToken();
            string tokenHash = TokenGenerator.HashToken(token);
            _tokenDal.Insert(user.UserId, tokenHash, DateTime.Now.AddMinutes(15));
            _audit.Write("RESET_REQUESTED", "Parola sıfırlama talebi.", user.UserId);
            var emailService = new EmailService();
            if (emailService.IsConfigured)
            {
                try
                {
                    emailService.Send(
                        user.Email,
                        "Parola sıfırlama",
                        "Sıfırlama kodunuz 15 dakika geçerlidir:\n" + token);
                    result.Message = "Hesap mevcutsa parola sıfırlama kodu gönderildi.";
                }
                catch (Exception)
                {
                    result.TokenForTest = token;
                    result.Message = "E-posta gönderilemedi. Kodu not et.";
                }
            }
            else
            {
                result.TokenForTest = token;
            }

            return result;
        }
        public string? ResetPassword(string token, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(token))
                return "Sıfırlama kodunu yapıştır.";

            if (newPassword != confirmPassword)
                return "Yeni parola tekrarı eşleşmiyor.";

            string tokenHash = TokenGenerator.HashToken(token.Trim());
            PasswordResetToken? saved = _tokenDal.GetValid(tokenHash);
            if (saved == null)
                return "Bağlantının süresi dolmuş veya bağlantı geçersiz.";

            User? user = _userDal.GetById(saved.UserId);
            if (user == null || !user.IsActive)
                return "Bağlantının süresi dolmuş veya bağlantı geçersiz.";

            var policy = new PasswordPolicyValidator();
            var check = policy.Validate(newPassword, user.FirstName, user.LastName, user.BirthDate);
            if (!check.IsValid)
                return string.Join(Environment.NewLine, check.Errors);

            bool sameAsCurrent = _hasher.Verify(
                newPassword,
                user.PasswordHash,
                user.PasswordSalt ?? "",
                user.PasswordIterations ?? 0);

            if (sameAsCurrent)
                return "Yeni parola mevcut parola ile aynı olamaz.";

            foreach (PasswordHistory old in _historyDal.GetLast(user.UserId, 3))
            {
                bool usedBefore = _hasher.Verify(
                    newPassword,
                    old.PasswordHash,
                    old.PasswordSalt ?? "",
                    old.PasswordIterations ?? 0);

                if (usedBefore)
                    return "Yeni parola son üç paroladan biriyle aynı olamaz.";
            }
            PasswordHashResult hashed = _hasher.HashPassword(newPassword);
            _userDal.SavePasswordChange(
                user.UserId,
                user.PasswordHash,
                user.PasswordSalt,
                user.PasswordIterations,
                hashed.Hash,
                hashed.Salt,
                hashed.Iterations,
                saved.TokenId);

            _audit.Write("PASSWORD_RESET", "Parola sıfırlama kodu ile yenilendi.", user.UserId);
            return null;
        }
    }
}