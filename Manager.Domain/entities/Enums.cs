using System.ComponentModel;

namespace Manager.Domain.entities
{
    public enum Role
    {
        [Description("Administrador")]
        Administrator,

        [Description("Normal")]
        Normal
    }
}