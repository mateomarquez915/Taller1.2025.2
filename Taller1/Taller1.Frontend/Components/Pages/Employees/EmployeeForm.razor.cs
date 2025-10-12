using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Taller1.Shared.Entities;

namespace Taller1.Frontend.Components.Pages.Employees;

public partial class EmployeeForm
{
    private EditContext editContext = null!;

    [EditorRequired, Parameter] public Employee employee { get; set; } = null!;
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }

    protected override void OnInitialized()
    {
        editContext = new(employee);
    }
}