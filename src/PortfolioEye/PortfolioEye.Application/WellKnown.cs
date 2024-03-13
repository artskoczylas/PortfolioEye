namespace PortfolioEye.Application;

public static class WellKnown
{
    public static class Claims
    {
        public static string Id => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public static string Name => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name";
        public static string Email => "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
    }
}