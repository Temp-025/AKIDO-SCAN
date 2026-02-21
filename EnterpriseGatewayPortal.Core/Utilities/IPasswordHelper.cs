namespace EnterpriseGatewayPortal.Core.Utilities
{
    public interface IPasswordHelper
    {
        string hashPassword(string password);
        bool verifyPassword(string storedHash, string enteredPassword);
    }
}
