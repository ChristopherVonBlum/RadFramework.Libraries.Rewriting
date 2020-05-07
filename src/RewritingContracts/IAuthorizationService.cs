using System;

namespace RewritingContracts
{
    public interface IAuthorizationService
    {
        bool IsAuthorized(Type serviceType, string serviceKey, string memberName);
    }
}