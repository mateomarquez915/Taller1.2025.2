using System.ComponentModel;

namespace Taller1.Shared.Enums;

public enum UserType
{
    [Description("Administrador")]
    Admin = 0,

    [Description("Usuario")]
    User = 1
}