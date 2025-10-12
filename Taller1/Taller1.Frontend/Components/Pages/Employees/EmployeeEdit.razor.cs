using Microsoft.AspNetCore.Components;
using MudBlazor;
using Taller1.Frontend.Repositories;
using Taller1.Shared.Entities;

namespace Taller1.Frontend.Components.Pages.Employees;

public partial class EmployeeEdit
{
    private Employee? employee;

    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<Employee>($"api/employees/{Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                NavigationManager.NavigateTo("employees");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
            }
        }
        else
        {
            employee = responseHttp.Response;
        }
    }

    private async Task EditAsync()
    {
        // Forzar la actualización del modelo antes de enviar
        StateHasChanged();

        var responseHttp = await Repository.PutAsync($"api/employees", employee);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError!, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Registro guardado.", Severity.Success);
    }

    private void Return()
    {
        NavigationManager.NavigateTo("employees");
    }
}