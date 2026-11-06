namespace MyProject.Helpers
{
    public class PasswordValidationResult
    {
        public bool IsValid { get; }
        public List<string> Errors { get; }

        public PasswordValidationResult(bool isValid, List<string> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }
    }

    public class PasswordPolicyValidator
    {
        public PasswordValidationResult Validate(
            string password,
            string firstName,
            string lastName,
            DateTime? birthDate)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(password) || password.Length < 6)
                errors.Add("Parola en az 6 karakter olmalıdır.");

            if (password == null || !password.Any(char.IsUpper))
                errors.Add("En az bir büyük harf olmalıdır.");

            if (password == null || !password.Any(char.IsLower))
                errors.Add("En az bir küçük harf olmalıdır.");

            if (password == null || !password.Any(char.IsDigit))
                errors.Add("En az bir rakam olmalıdır.");

            if (!string.IsNullOrEmpty(password))
            {
                string p = password.ToLowerInvariant();

                if (!string.IsNullOrWhiteSpace(firstName) &&
                    p.Contains(firstName.ToLowerInvariant()))
                    errors.Add("Parola adınızı içeremez.");

                if (!string.IsNullOrWhiteSpace(lastName) &&
                    p.Contains(lastName.ToLowerInvariant()))
                    errors.Add("Parola soyadınızı içeremez.");

                if (birthDate.HasValue &&
                    p.Contains(birthDate.Value.Year.ToString()))
                    errors.Add("Parola doğum yılınızı içeremez.");

                if (ContainsSequentialDigits(password))
                    errors.Add("Parola ardışık rakam dizisi içeremez.");
            }

            return new PasswordValidationResult(errors.Count == 0, errors);
        }

        public bool ContainsSequentialDigits(string password)
        {
            const string forward = "0123456789";
            const string backward = "9876543210";

            for (int i = 0; i <= forward.Length - 3; i++)
            {
                string a = forward.Substring(i, 3);
                string b = backward.Substring(i, 3);
                if (password.Contains(a) || password.Contains(b))
                    return true;
            }

            return false;
        }
    }
}