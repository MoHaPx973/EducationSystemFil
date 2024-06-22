using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using System.Data;

namespace EducationSystem.Domain.Relationships
{
    public class ItemInCurriculum
    {
        public ItemInCurriculum() { }
        public ItemInCurriculum(Item item, Curriculum curriculum, int numberOfHours)
        {
            ItemId = item.Id;
            CurriculumId = curriculum.Id;
            LinkItem = item;
            LinkCurriculum = curriculum;
            NumberOfHours = numberOfHours;
        }
        public int ItemId { get; set; }
        public int CurriculumId { get; set; }
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
        public Curriculum LinkCurriculum
        {
            get => _curriculum;
            set
            {
                if ((_curriculum != null) && (value == null))
                {
                    return;
                }
                if (value == null)
                {
                    throw new NullReferenceException("Ошибка привязки учебного плана");
                }
                _curriculum = value;
            }
        }
        public int NumberOfHours
        {
            get => _number;
            set
            {
                if ((_number != 0) && (value == 0))
                {
                    return;
                }
                if (value == 0)
                {
                    throw new NullReferenceException("Ошибка при записи количества часов");
                }
                _number = value;
            }
        }

        private Item _item = new();
        private Curriculum _curriculum=new();
        private int _number;

    }
}
