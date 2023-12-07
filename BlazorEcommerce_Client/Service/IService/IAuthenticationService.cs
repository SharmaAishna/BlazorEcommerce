using EcommerceModel;

namespace BlazorEcommerce_Client.Service.IService
{
    public interface IAuthenticationService
    {
        Task<SignUpRequestDTO> RegisterUser(SignUpRequestDTO signUpRequestDTO);
        Task<SignInResponseDTO> Login(SignInRequestDTO signInRequestDTO);
        Task Logout();
    }
}
