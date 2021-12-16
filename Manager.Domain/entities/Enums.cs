using System.ComponentModel;

namespace Manager.Domain.entities
{
    public enum Role
    {
        [Description("Administrator")]
        Administrator,

        [Description("Normal")]
        Normal
    }
}