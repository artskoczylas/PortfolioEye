namespace PortfolioEye.Client.Extensions
{
    public static class IdentityExtension
    {
        public static string GetInitials(this System.Security.Principal.IIdentity identity)
        {
            return (identity.Name != null && identity.Name.Length > 2) ? identity.Name.Substring(0, 2).ToUpper() : "NA";
        }
    }
}
