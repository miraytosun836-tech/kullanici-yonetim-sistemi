using MyProject.DAL;
using MyProject.Entities;
using MyProject.Helpers;

namespace MyProject.BLL
{
    public class UserBLL
    {
        private readonly UserDAL _userDal = new UserDAL();
        private readonly RoleDAL _roleDal = new RoleDAL();
        private readonly PasswordHasher _hasher = new PasswordHasher();
        private readonly PasswordHistoryDAL _historyDal = new PasswordHistoryDAL();
        private readonly AuditBLL _audit = new AuditBLL();

        public void SeedInitialAdminIfNotExist()
        {
            const string adminEmail = "admin@mcbu.edu.tr";

            if (_userDal.GetByEmail(adminEmail) != null)
                return;

            Role? adminRole = _roleDal.GetByName("Admin");
            if (adminRole == null)
                throw new InvalidOperationException("Roles tablosunda Admin yok. SQL scriptini çalıştır.");

            PasswordHashResult hashed = _hasher.HashPassword("AdminSalihli1");

            var admin = new User
            {
                Email = adminEmail,
                FirstName = "Sistem",
                LastName = "Yoneticisi",
                BirthDate = null,
                PasswordHash = hashed.Hash,
                PasswordSalt = hashed.Salt,
                PasswordIterations = hashed.Iterations,
                RoleId = adminRole.RoleId,
                IsActive = true,
                MustChangePassword = false,
                CreatedBy = null
            };

            _userDal.InsertUser(admin);
        }
        public LoginResult Authenticate(string email, string password)
        {
            User? user = _userDal.GetByEmail(email);

            if (user == null)
            {
                _audit.Write("LOGIN_FAILED", "Başarısız giriş denemesi.");
                return LoginResult.Fail("E-posta veya parola hatalı.");
            }

            if (!user.IsActive)
            {
                _audit.Write("LOGIN_FAILED", "Pasif hesap giriş denemesi.", user.UserId);
                return LoginResult.Fail("Hesabınız pasif durumdadır.");
            }

            bool ok = _hasher.Verify(
                password,
                user.PasswordHash,
                user.PasswordSalt ?? "",
                user.PasswordIterations ?? 0);

            if (!ok)
            {
                _audit.Write("LOGIN_FAILED", "Başarısız giriş denemesi.", user.UserId);
                return LoginResult.Fail("E-posta veya parola hatalı.");
            }

            CurrentSession.Start(user.UserId, user.Email, user.RoleName ?? "");
            _audit.Write("LOGIN_SUCCESS", "Kullanıcı başarılı giriş yaptı.", user.UserId);

            return new LoginResult
            {
                IsSuccess = true,
                Message = "Giriş başarılı.",
                RoleName = user.RoleName ?? "",
                MustChangePassword = user.MustChangePassword
            };
        }
        public void Logout()
        {
            CurrentSession.Clear();
        }
        private void EnsureAdmin()
        {
            if (!CurrentSession.IsAuthenticated || CurrentSession.RoleName != "Admin")
                throw new UnauthorizedAccessException("Bu işlem için Admin yetkisi gerekir.");
        }

        public List<User> GetAllUsers()
        {
            EnsureAdmin();
            return _userDal.GetAll();
        }
        public void SetUserActive(int userId, bool isActive)
        {
            EnsureAdmin();

            if (CurrentSession.UserId == userId)
                throw new InvalidOperationException("Kendi hesabınızı pasif yapamazsınız.");

            _userDal.SetActive(userId, isActive);
            _audit.Write("USER_STATUS_CHANGED", "Kullanıcı durumu değiştirildi. UserId=" + userId, CurrentSession.UserId);
        }
        public void UpdateUserRole(int userId, int roleId)
        {
            EnsureAdmin();

            if (CurrentSession.UserId == userId)
                throw new InvalidOperationException("Kendi rolünüzü değiştiremezsiniz.");

            _userDal.UpdateRole(userId, roleId);
            _audit.Write("ROLE_CHANGED", "Kullanıcı rolü değiştirildi. UserId=" + userId, CurrentSession.UserId);
        }
        public List<Role> GetAllRoles()
        {
            EnsureAdmin();
            return _roleDal.GetAll();
        }

        public CreateUserResult CreateUser(
            string email,
            string firstName,
            string lastName,
            DateTime? birthDate,
            int roleId)
        {
            EnsureAdmin();

            if (!EmailValidator.IsValid(email))
                return CreateUserResult.Fail("Geçerli bir e-posta giriniz.");

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                return CreateUserResult.Fail("Ad ve soyad zorunludur.");

            if (_userDal.GetByEmail(email.Trim()) != null)
                return CreateUserResult.Fail("Bu e-posta zaten kayıtlı.");

            string tempPassword = TemporaryPasswordGenerator.Generate();
            PasswordHashResult hashed = _hasher.HashPassword(tempPassword);

            var user = new User
            {
                Email = email.Trim(),
                FirstName = firstName.Trim(),
                LastName = lastName.Trim(),
                BirthDate = birthDate,
                PasswordHash = hashed.Hash,
                PasswordSalt = hashed.Salt,
                PasswordIterations = hashed.Iterations,
                RoleId = roleId,
                IsActive = true,
                MustChangePassword = true,
                CreatedBy = CurrentSession.UserId
            };
            _userDal.InsertUser(user);
            _audit.Write("USER_CREATED", "Yeni kullanıcı: " + user.Email, CurrentSession.UserId);

            bool emailSent = false;
            string message = "Kullanıcı oluşturuldu.";
            var emailService = new EmailService();

            if (emailService.IsConfigured)
            {
                try
                {
                    emailService.Send(
                        user.Email,
                        "Geçici parolanız",
                        "Geçici parolanız: " + tempPassword +
                        "\nİlk girişte değiştirmeniz gerekir.");
                    emailSent = true;
                    message += " Geçici parola e-posta ile gönderildi.";
                }
                catch (Exception)
                {
                    message += " E-posta gönderilemedi. Geçici parolayı not et.";
                }
            }
            else
            {
                message += " E-posta ayarı yok. Geçici parolayı Admin görür.";
            }

            return new CreateUserResult
            {
                IsSuccess = true,
                Message = message,
                GeneratedPassword = emailSent ? null : tempPassword,
                EmailSent = emailSent
            };

        }

        public string? ChangePassword(string newPassword, string confirmPassword, string? oldPassword, bool requireOldPassword)
        {
            if (!CurrentSession.IsAuthenticated || CurrentSession.UserId == null)
                return "Oturum bulunamadı.";

            if (newPassword != confirmPassword)
                return "Yeni parola tekrarı eşleşmiyor.";
            

            User? user = _userDal.GetByEmail(CurrentSession.Email);
            if (user == null)
                return "Kullanıcı bulunamadı.";
            if (requireOldPassword)
            {
                bool oldOk = _hasher.Verify(
                    oldPassword ?? "",
                    user.PasswordHash,
                    user.PasswordSalt ?? "",
                    user.PasswordIterations ?? 0);

                if (!oldOk)
                    return "Eski parola hatalı.";
            }

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
                null);

            _audit.Write("PASSWORD_CHANGED", "Kullanıcı parolasını değiştirdi.", user.UserId);
            return null;
        }
     }
    }
    public class LoginResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public string RoleName { get; set; } = "";
        public bool MustChangePassword { get; set; }

        public static LoginResult Fail(string message)
        {
            return new LoginResult { IsSuccess = false, Message = message };
        }
    }
    public class CreateUserResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = "";
        public string? GeneratedPassword { get; set; }
        public bool EmailSent { get; set; }

        public static CreateUserResult Fail(string message)
        {
            return new CreateUserResult { IsSuccess = false, Message = message };
        }
    }
