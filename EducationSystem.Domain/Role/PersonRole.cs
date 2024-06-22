using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Domain.Role
{
    public class PersonRole
    {
        public PersonRole() { }
        public PersonRole(string name, string description)
        {
            Name = name;
            Description = description;
        }
        public int Id { get; set; }
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
        public string Description
        {
            get => _description;
            set
            {
                if (!string.IsNullOrWhiteSpace(_description) && (string.IsNullOrEmpty(value)))
                {
                    return;
                }
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new NullReferenceException("Ошибка заполнения описания");
                }
                _description = value;
            }
        }

        string _name = string.Empty;
        string _description = string.Empty;


    }
}
