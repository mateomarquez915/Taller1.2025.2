using Microsoft.AspNetCore.Components;
using MudBlazor;
using Taller1.Frontend.Repositories;
using Taller1.Shared.DTOs;

namespace Taller1.Frontend.Components.Pages.Auth;

public partial class ChangePassword
{
    private ChangePasswordDTO changePasswordDTO = new();
    private bool loading;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [CascadingParameter] private IMudDialogInstance MudDialog { get; set; } = null!;

    private async Task ChangePasswordAsync()
    {
        {
            loading = true;
            var responseHttp = await Repository.PostAsync("/api/accounts/changePassword", changePasswordDTO);
            loading = false;

            if (responseHttp.Error)
            {
                string? message = await responseHttp.GetErrorMessageAsync();
                string cleanMessage = "Error al cambiar la contraseña";

                if (!string.IsNullOrEmpty(message))
                {
                    try
                    {
                        if (message.Contains("\"errors\":{"))
                        {
                            var errorsStart = message.IndexOf("\"errors\":{") + 10;
                            var errorsSection = message.Substring(errorsStart);

                            var msgStart = errorsSection.IndexOf("[\"") + 2;
                            var msgEnd = errorsSection.IndexOf("\"]", msgStart);

                            if (msgStart > 1 && msgEnd > msgStart)
                            {
                                cleanMessage = errorsSection.Substring(msgStart, msgEnd - msgStart);
                            }
                        }
                    }
                    catch
                    {
                        cleanMessage = "Error al cambiar la contraseña. Verifica los datos.";
                    }
                }

                Snackbar.Add(cleanMessage, Severity.Error);
                return;
            }
            MudDialog.Close();
            Snackbar.Add("Contraseña modificada con éxito.", Severity.Success);
            NavigationManager.NavigateTo("/EditUser");
        }
    }

    private void ReturnAction()
    {
        MudDialog.Cancel();
        NavigationManager.NavigateTo("/EditUser");
    }
}