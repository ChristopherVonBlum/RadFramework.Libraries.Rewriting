namespace RewritingContracts
{
    public interface IAuthorizationServiceAware
    {
        IAuthorizationService AuthentificationService { get; set; }
    }
}