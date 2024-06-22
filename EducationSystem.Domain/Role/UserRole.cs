using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Domain.Role
{
    public class UserRole//:PersonRole
    {
        public UserRole() { }
        public UserRole(string name,string description,int priority) 
        {
            Name = name;
            Description = description;
            Priority = priority;
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
        public int Priority
        {
            get => _priority;
            set
            {
                if ((_priority != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка привязки приоритета");
                }
                _priority = value;
            }
        }

        string _name = string.Empty;
        string _description = string.Empty;
        int _priority;
    }
}
