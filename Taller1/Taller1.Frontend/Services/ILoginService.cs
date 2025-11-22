namespace Taller1.Frontend.Services
{
    public interface ILoginService
    {
        Task LoginAsync(string token);

        Task LogoutAsync();
    }
}