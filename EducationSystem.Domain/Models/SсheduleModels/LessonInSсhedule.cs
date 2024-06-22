using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.Domain.Models.SсheduleModels
{
    public class LessonInSсhedule
    {
        public LessonInSсhedule() { }
        public LessonInSсhedule(Item linkItem, Person teacher, ClassSchedule linkSchedule, Classroom roomNumber, LessonTime time, StudyDay day)
        {
            LinkItem = linkItem;
            Teacher = teacher;
            LinkSchedule = linkSchedule;
            RoomNumber = roomNumber;
            Time = time;
            Day = day;
        }
        public int Id { get; set; }
        public Item LinkItem
        {
            get => _item;
            set
            {
                if ((_item != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки предмета");
                }
                _item = value;
            }
        }
        public Person Teacher
        {
            get => _teacher;
            set
            {
                if ((_teacher != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки учителя");
                }
                _teacher = value;
            }
        }
        public ClassSchedule LinkSchedule
        {
            get => _classSchedule;
            set
            {
                if ((_classSchedule != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки расписания");
                }
                _classSchedule = value;
            }
        }
        public Classroom RoomNumber
        {
            get => _classroom;
            set
            {
                if ((_classroom != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки кабинета");
                }
                _classroom = value;
            }
        }
        public LessonTime Time
        {
            get => _lessonTime;
            set
            {
                if ((_lessonTime != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки времени занятия");
                }
                _lessonTime = value;
            }
        }
        public StudyDay Day
        {
            get => _day;
            set
            {
                if ((_day != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки дня занятия");
                }
                _day = value;
            }
        }

        private Item _item =new();
        private Person _teacher = new();
        private ClassSchedule _classSchedule = new();
        private Classroom _classroom = new();
        private LessonTime _lessonTime = new();
        private StudyDay _day = new();
    }
}
