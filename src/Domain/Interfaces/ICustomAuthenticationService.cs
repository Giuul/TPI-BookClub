using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICustomAuthenticationService
    {
        Task<string?> AutenticarAsync(AuthenticationRequest authenticationRequest);
    }
}