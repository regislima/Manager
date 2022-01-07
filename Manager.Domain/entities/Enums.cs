using System.ComponentModel;

namespace Manager.Domain.Entities
{
    public enum Role
    {
        [Description("Administrator")]
        Administrator,

        [Description("Normal")]
        Normal
    }
}