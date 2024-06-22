using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using System.Transactions;

namespace EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors
{
    public class LessonInSсheduleInteractor
    {
        private IGenericRepository<LessonInSсhedule> _genericRepository;
        private IGenericRepository<Item> _itemRepository;
        private IPersonRepository _teacherRepository;
        private IClassSheduleRepository _classScheduleRepository;
        private IGenericRepository<Classroom> _classRoomRepository;
        private IGenericRepository<LessonTime> _lessonTimeRepository;
        private IGenericRepository<StudyDay> _dayTimeRepository;
        private ILessonInSheduleRepository _repository;
        private IItemInCurriculumRepository _itemInCurriculumRepository;
        private IUnitWork _unitWork;

        public LessonInSсheduleInteractor(IGenericRepository<LessonInSсhedule> genericRepository, IPersonRepository teacherRepository,
            IClassSheduleRepository classScheduleRepository, IGenericRepository<Classroom> classRoomRepository,
            IGenericRepository<LessonTime> lessonTimeRepository, ILessonInSheduleRepository repository, IUnitWork unitWork, IGenericRepository<Item> itemRepository,
            IGenericRepository<StudyDay> dayTimeRepository, IItemInCurriculumRepository itemInCurriculumRepository)
        {
            _genericRepository = genericRepository;
            _teacherRepository = teacherRepository;
            _classScheduleRepository = classScheduleRepository;
            _classRoomRepository = classRoomRepository;
            _lessonTimeRepository = lessonTimeRepository;
            _repository = repository;
            _unitWork = unitWork;
            _itemRepository = itemRepository;
            _dayTimeRepository = dayTimeRepository;
            _itemInCurriculumRepository = itemInCurriculumRepository;
        }


        // Методы

        // Создание
        public async Task<Response<LessonInSсheduleDto>> Insert(int itemId,int personId, int classScheduleId, int classroomId, int lessonTimeId, DateTime date)
        {
            LessonInSсhedule Instance = new();
            try
            {
                //PersonRole Role = await CheckRole(roleId);
                Person teacher = await CheckPerson(personId);
                ClassSchedule classSchedule = await CheckClassSchedule(classScheduleId);
                Classroom classroom = await CheckClassroom(classroomId);
                LessonTime lessonTime = await CheckLessonTime(lessonTimeId);
                StudyDay day = await CheckDay(date);
                Item linkItem = await CheckItem(itemId, classSchedule);

                Instance = new(linkItem, teacher, classSchedule, classroom, lessonTime, day);
                _genericRepository.Insert(Instance);
            }
            catch (ItemNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные о предмете введены не верно", ex.Message);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные об учителе введены не верно", ex.Message);
            }
            catch (ClassScheduleNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные о расписании введены не верно", ex.Message);
            }
            catch (ClassroomNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные о кабинете введены не верно", ex.Message);
            }
            catch (LessonTimeNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные о времени занятия введены не верно", ex.Message);
            }
            catch (StudyDayNotFoundException ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка,данные о дне занятия введены не верно", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<LessonInSсheduleDto>> GetByIdAsync(int id)
        {
            try
            {
                LessonInSсhedule? Instance = await _repository.GetByIdAsync(id);
                if (Instance == null)
                {
                    return new Response<LessonInSсheduleDto>("Урок в расписании не найден", $"id = {id}");
                }
                return new Response<LessonInSсheduleDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<LessonInSсheduleDto>> Update(int id, int itemId, int personId, int classScheduleId, int classroomId, int lessonTimeId, DateTime date)
        {
            LessonInSсhedule? Instance = new ();
            try
            {
                Instance = await _repository.GetByIdAsync(id);
                if (Instance == null) { return new Response<LessonInSсheduleDto>("Ошибка, данные введены не верно, информация об уроке в расписании не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка, данные введены не верно, урок в расписании не найден", ex.Message);
            }
            try
            {
                ClassSchedule classSchedule = new();
                try
                {
                    Person teacher = await CheckPerson(personId);
                    Instance.Teacher = teacher;
                }
                catch (PersonNotFoundException ex) { }
                try
                {
                    classSchedule = await CheckClassSchedule(classScheduleId);
                    Instance.LinkSchedule = classSchedule;
                }
                catch (ClassScheduleNotFoundException ex) { classSchedule = Instance.LinkSchedule; }
                try
                {
                    Classroom classroom = await CheckClassroom(classroomId);
                    Instance.RoomNumber = classroom;
                }
                catch (ClassroomNotFoundException ex) { }
                try
                {
                    LessonTime lessonTime = await CheckLessonTime(lessonTimeId);
                    Instance.Time = lessonTime;
                }
                catch (LessonTimeNotFoundException ex) { }
                try
                {
                    StudyDay day = await CheckDay(date);
                    Instance.Day = day;
                }
                catch (StudyDayNotFoundException ex) { }

                try
                {
                    Item linkItem = await CheckItem(itemId, classSchedule);
                    Instance.LinkItem = linkItem;
                    _genericRepository.Insert(Instance);
                }
                catch (ItemNotFoundException ex) { }

                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<LessonInSсheduleDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<LessonInSсheduleDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>(_repository.GetAllEnumerable().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<LessonInSсheduleDto>> GetAllByScheduleId(int scheduleId)
        {
            try
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>(_repository.GetAllEnumerable().Where(c=>c.LinkSchedule.Id== scheduleId).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>("Ошибка чтения", ex.Message);
            }
        }
        
        public Response<IEnumerable<LessonInSсheduleDto>> GetAllByTeacherId(int teacherId)
        {
            try
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>(_repository.GetAllEnumerable().Where(c => c.Teacher.Id == teacherId).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<LessonInSсheduleDto>> GetPageEnumerable(int start, int count)
        {
            try
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>(_repository.GetAllEnumerable().Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonInSсheduleDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<LessonInSсheduleDto>> Delete(int id)
        {
            LessonInSсhedule? instance = new();
            try
            {
                instance = await _repository.GetByIdAsync(id);
                if (instance == null) { return new Response<LessonInSсheduleDto>("Ошибка, данные введены не верно, информация о уроке в расписании не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка, данные введены не верно, урок в расписании не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка удаления", ex.Message);
            }
        }


        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<LessonInSсheduleDto>> SaveChance(LessonInSсhedule instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<LessonInSсheduleDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            LessonInSсhedule load = await _repository.GetByIdAsync(instance.Id);
            return new Response<LessonInSсheduleDto>(load.ToDto());
        }

        //// Проверка роли
        //private async Task<PersonRole> CheckRole(int roleId)
        //{
        //    PersonRole personRole = await _roleRepository.GetByIdAsyncWithoutLink(roleId);
        //    if (personRole == null)
        //    {
        //        throw new RoleNotFoundException($"roleid = {roleId} not found");
        //    }
        //    return personRole;
        //}

        private async Task<Item> CheckItem(int itemId, ClassSchedule classSchedule)
        {
            Item entity = await _itemRepository.GetByIdAsyncWithoutLink(itemId);
            if (entity == null)
            {
                throw new ItemNotFoundException($"itemId = {itemId} not found");
            }
            List<ItemInCurriculum> list = _itemInCurriculumRepository.GetByCurriculumIdAsync(classSchedule.LinkClass.LinkCurriculum.Id).ToList();
            foreach (var item in list)
            {
                if (item.ItemId==entity.Id)
                {
                    return entity; 
                }
            }
            throw new ItemNotFoundException($"itemId = {itemId} not found in curriculum");
        }

        private async Task<Person> CheckPerson(int personId)
        {
            Person entity = await _teacherRepository.GetByIdAsync(personId);
            if (entity == null)
            {
                throw new PersonNotFoundException($"personId = {personId} not found");
            }
            if (entity.Role.Id != 2)
            {
                throw new PersonNotFoundException($"personId = {personId} not teacher");
            }
            return entity;
        }

        private async Task<ClassSchedule> CheckClassSchedule(int classScheduleId)
        {
            ClassSchedule entity = await _classScheduleRepository.GetByIdAsync(classScheduleId);
            if (entity == null)
            {
                throw new ClassScheduleNotFoundException($"classScheduleId = {classScheduleId} not found");
            }
            return entity;
        }

        private async Task<Classroom> CheckClassroom(int classroomId)
        {
            Classroom entity = await _classRoomRepository.GetByIdAsyncWithoutLink(classroomId);
            if (entity == null)
            {
                throw new ClassroomNotFoundException($"ClassroomId = {classroomId} not found");
            }
            return entity;
        }
        private async Task<LessonTime> CheckLessonTime(int lessonTimeId)
        {
            LessonTime entity = await _lessonTimeRepository.GetByIdAsyncWithoutLink(lessonTimeId);
            if (entity == null)
            {
                throw new LessonTimeNotFoundException($"lessonTimeId = {lessonTimeId} not found");
            }
            return entity;
        }
        private async Task<StudyDay> CheckDay(DateTime date)
        {
            List<StudyDay> listEntity = _dayTimeRepository.GetAllAsyncWithoutLink();
            StudyDay entity = listEntity.FirstOrDefault(d=>d.Date==date);
			if (entity == null)
            {
                throw new StudyDayNotFoundException($"date = {date} not found");
            }
            return entity;
        }
    }
}
