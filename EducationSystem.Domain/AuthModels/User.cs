using EducationSystem.Domain.Models;
using EducationSystem.Domain.Role;
using System.ComponentModel.DataAnnotations.Schema;

namespace EducationSystem.Domain.AuthModels
{
	public class User
	{
		public User() { }

        public User(string login, string password, UserRole role, Person linkPerson)
        {
            Login = login;
            Password = password;
            Role = role;
            LinkPerson = linkPerson;
        }

        public int Id { get; set; }
		public string Login
		{
			get => _name;
			set
			{
				if (!string.IsNullOrWhiteSpace(_name) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения имени");
				}
				_name = value;
			}
		}
		public string Password
		{
			get => _password;
			set
			{
				if (!string.IsNullOrWhiteSpace(_password) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения пароля");
				}
				_password = value;
			}
		}
        [ForeignKey("UserRole")]
        public UserRole Role
		{
			get => _role;
			set
			{
				if ((_role != null) && (value == null))
				{
					return;
				}
				if (value == null)
				{
					throw new NullReferenceException("Ошибка привязки роли");
				}
				_role = value;
			}
		}
		public Person LinkPerson
		{
			get => _person;
			set
			{
				if ((_person != null) && (value == null))
				{
					return;
				}
				if (value == null)
				{
					throw new NullReferenceException("Ошибка привязки персоны");
				}
                _person = value;
			}
		}

        private string _name = string.Empty;
		private string _password = string.Empty;
		private Person _person = new();
		private UserRole _role = new();

	}
}
