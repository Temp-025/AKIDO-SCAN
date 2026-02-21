using Microsoft.AspNetCore.Identity;

namespace EnterpriseGatewayPortal.Core.Utilities
{
    public class PasswordHelper : IPasswordHelper
    {
        private readonly IPasswordHasher<object> _passwordHasher;

        public PasswordHelper(IPasswordHasher<object> passwordHasher)
        {
            _passwordHasher = passwordHasher;
        }
        public string hashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password cannot be null or empty", nameof(password));
            }

            return _passwordHasher.HashPassword(null, password);
        }

        public bool verifyPassword(string storedHash, string enteredPassword)
        {
            if (string.IsNullOrEmpty(storedHash))
                throw new ArgumentNullException("Stored Password cannot be null or empty", nameof(storedHash));

            if (string.IsNullOrEmpty(enteredPassword))
                return false;

            var result = _passwordHasher.VerifyHashedPassword(
                null,
                storedHash,
                enteredPassword
            );

            return result == PasswordVerificationResult.Success ||
                result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
