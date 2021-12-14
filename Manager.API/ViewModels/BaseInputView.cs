using System;

namespace Manager.API.ViewModels
{
    public class BaseInputView
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}