using EducationSystem.Domain.Models.Helper;
using EducationSystem.Domain.Role;

namespace EducationSystem.Domain.Models
{
	public class Person:DbModel
	{
		public Person() 
		{
			IsHidden = false;
        }
		public Person(string surname, string name, string middleName,string phone,string email,DateTime birthday,bool gender,string adress, PersonRole role)
		{
            Surname = surname;
            Name = name;
			MiddleName = middleName;
            Phone = phone;
            Email = email;
            Birthday = birthday;
            if (gender)
            {
                Gender = "мужской";
            }
            else
            {
                Gender = "женский";
            }
            Address = adress;
            Role = role;
            
		}
		public int Id { get; set; }
		public string Surname
		{
			get => _surname;
			set
			{
				if (!string.IsNullOrWhiteSpace(_surname) && (string.IsNullOrEmpty(value)))
				{
					return;
				}
				if (string.IsNullOrWhiteSpace(value))
				{
					throw new NullReferenceException("Ошибка заполнения фамилии");
				}
				_surname = value;
			}
		}
        public string Name
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
        public string MiddleName
        {
            get => _middleName;
            set
            {
                if (!string.IsNullOrWhiteSpace(_middleName) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения отчества");
                }
                _middleName = value;
            }
        }
        public string Phone
        {
            get => _phone;
            set
            {
                if (!string.IsNullOrWhiteSpace(_phone) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения номера");
                }
                _phone = value;
            }
        }
        public string? Email
        {
            get => _email;
            set
            {
                if (!string.IsNullOrWhiteSpace(_email) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }
                _email = value;
            }
        }
        public DateTime Birthday
        {
            get => _birthday;
            set
            {
                if ((_birthday != new DateTime()) && (value == new DateTime()))
                {
                    return;
                }
                if (value == new DateTime())
                {
                    throw new NullReferenceException("Ошибка заполнения дня рождения");
                }
                _birthday = value;
            }
        }
        public string Gender { get; set; }
        public string Address
        {
            get => _adress;
            set
            {
                if (!string.IsNullOrWhiteSpace(_adress) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения адресса");
                }
                _adress = value;
            }
        }
        public PersonRole Role
        {
            get => _personRole;
            set
            {
                if ((_personRole != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки роли");
                }
                _personRole = value;
            }
        }

        private string _surname = string.Empty;
        private string _name = string.Empty;
        private string _middleName = string.Empty;
        private string _phone = string.Empty;
        private string _email = string.Empty;
        private DateTime _birthday = new();
        private bool _gender = true;
        private string _adress = string.Empty;
        private PersonRole _personRole = new();
	}
}

