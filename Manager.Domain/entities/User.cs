using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Manager.Core.Exceptions;
using Manager.Domain.Validators;

namespace Manager.Domain.entities
{
    public class User : Base
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public Role Role { get; private set; }

        // Entity Framework Core
        protected User() { }

        public User(long id, string name, string email, string password, Role role)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            CreatedAt = DateTime.Now;
            _errors = new List<string>();
        }

        public void ChangeName(string name)
        {
            Name = Name;
        }

        public void ChangeEmail(string email)
        {
            Email = email;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

        public void ChangeRole(Role role)
        {
            Role = role;
        }

        public override bool Validate()
        {
            UserValidator validator = new UserValidator();
            ValidationResult result = validator.Validate(this);

            if (!result.IsValid)
            {
                result.Errors.ForEach(error => _errors.Add(error.ErrorMessage));

                throw new DomainException("Campos inválidos", _errors);
            }

            return true;
        }
    }
}