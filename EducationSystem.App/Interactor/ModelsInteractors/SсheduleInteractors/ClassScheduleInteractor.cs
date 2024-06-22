using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.SheduleInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;


namespace EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors
{
    public class ClassScheduleInteractor
    {
        private IGenericRepository<ClassSchedule> _genericRepository;
        private IGenericRepository<SchoolClass> _classRepository;
        private IClassSheduleRepository _repository;
        private IUnitWork _unitWork;

        public ClassScheduleInteractor(IGenericRepository<ClassSchedule> genericRepository, 
            IGenericRepository<SchoolClass> classRepository, IClassSheduleRepository repository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _classRepository = classRepository;
            _repository = repository;
            _unitWork = unitWork;
        }


        // Методы

        // Создание
        public async Task<Response<ClassScheduleDto>> Insert(int number, string? description, int linkClassId)
        {
            ClassSchedule Instance = new();
            try
            {
                SchoolClass schoolClass = await CheckClass(linkClassId);
                Instance = new(number, description, schoolClass);
                Instance.DateCreate = DateTime.Today;
                _genericRepository.Insert(Instance);
            }
            catch (ClassNotFoundException ex)
            {
                return new Response<ClassScheduleDto>("Ошибка,данные о классе введены неверно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ClassScheduleDto>("Ошибка, данные введены неверно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<ClassScheduleDto>> GetByIdAsync(int id)
        {
            try
            {
                ClassSchedule? Instance = await _repository.GetByIdAsync(id);
                if (Instance == null)
                {
                    return new Response<ClassScheduleDto>("Расписание не найдено", $"id = {id}");
                }
                return new Response<ClassScheduleDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }
        public Response<ClassScheduleDto> GetByClassId(int classId)
        {
            try
            {
                ClassSchedule? Instance = _repository.GetAllEnumerable().FirstOrDefault(i => i.LinkClass.Id == classId);
                if (Instance == null)
                {
                    return new Response<ClassScheduleDto>("Расписание не найдено", $"classId = {classId}");
                }
                return new Response<ClassScheduleDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<ClassScheduleDto>> Update(int id,int number, string? description, int linkClassId)
        {
            ClassSchedule? Instance = new ClassSchedule();
            try
            {
                Instance = await _repository.GetByIdAsync(id);
                if (Instance == null) { return new Response<ClassScheduleDto>("Ошибка, данные введены неверно, информация о расписании не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка, данные введены неверно, расписание не найдено", ex.Message);
            }
            try
            {
                Instance.Number = number;
                Instance.Description = description;
                try
                {
                    SchoolClass linkclass = await CheckClass(linkClassId);
                    Instance.LinkClass = linkclass;
                }
                catch (ClassNotFoundException ex)
                {

                }
                Instance.DateUpdate = DateTime.Now;
                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ClassScheduleDto>("Данные введены неверное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<ClassScheduleDto>> GetAllEnumerable(bool isHidden)
        {
            try
            {
                return new Response<IEnumerable<ClassScheduleDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ClassScheduleDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<ClassScheduleDto>> GetPageEnumerable(bool isHidden, int start, int count)
        {
            try
            {
                return new Response<IEnumerable<ClassScheduleDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ClassScheduleDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<ClassScheduleDto>> HideOrShow(int id)
        {
            ClassSchedule? Instance = new ();
            try
            {
                Instance = await _repository.GetByIdAsync(id);
                if (Instance == null) { return new Response<ClassScheduleDto>("Ошибка, данные введены неверно, информация о расписании не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка, данные введены неверно, расписание не найдено", ex.Message);
            }
            Instance.IsHidden = !Instance.IsHidden;
            if (Instance.IsHidden)
            {
                Instance.DateHidden = DateTime.Now;
            }
            else
                Instance.DateHidden = null;
            return await SaveChance(Instance);
        }

        public async Task<Response<ClassScheduleDto>> Delete(int id)
        {
            ClassSchedule? instance = new ();
            try
            {
                instance = await _repository.GetByIdAsync(id);
                if (instance == null) { return new Response<ClassScheduleDto>("Ошибка, данные введены неверно, информация о расписании не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка, данные введены неверно,расписание не найдена", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка удаления", ex.Message);
            }
        }

        
        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<ClassScheduleDto>> SaveChance(ClassSchedule instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<ClassScheduleDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            ClassSchedule load = await _repository.GetByIdAsync(instance.Id);
            return new Response<ClassScheduleDto>(load.ToDto());
        }

        // Проверка роли
        private async Task<SchoolClass> CheckClass(int id)
        {
            SchoolClass personRole = await _classRepository.GetByIdAsyncWithoutLink(id);
            if (personRole == null)
            {
                throw new ClassNotFoundException($"class id = {id} not found");
            }
            return personRole;
        }
    }
}
