using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.ModelsInteractors
{
    public class PersonInteractor
    {
        private IGenericRepository<Person> _genericRepository;
        private IGenericRepository<PersonRole> _roleRepository;
        private IPersonRepository _repository;
        private IUnitWork _unitWork;
        public PersonInteractor(IGenericRepository<Person> genericRepository, IUnitWork unitWork, IGenericRepository<PersonRole> roleRepository, IPersonRepository personRepository)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
            _roleRepository = roleRepository;
            _repository = personRepository;
        }

        // Методы

        // Создание
        public async Task<Response<PersonDto>> Insert(string? surname, string? name, string? middleName, string? phone, string? email, DateTime birthday, bool gender, string? address, int roleId)
        {
            Person Instance = new();
            try
            {
                PersonRole Role = await CheckRole(roleId);
                Instance = new(surname,name,middleName,phone,email,birthday,gender,address, Role);
                Instance.DateCreate = DateTime.Today;
                _genericRepository.Insert(Instance);
            }
            catch (RoleNotFoundException ex)
            {
                return new Response<PersonDto>("Ошибка,данные о роли введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<PersonDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<PersonDto>> GetByIdAsync(int id)
        {
            try
            {
                Person? Instance = await _repository.GetByIdAsync(id);
                if (Instance == null)
                {
                    return new Response<PersonDto>("Персона не найдена", $"id = {id}");
                }
                return new Response<PersonDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<PersonDto>> Update(int Id, string? surname, string? name, string? middleName, string? phone, string? email, DateTime birthday, string? address, int roleId)
        {
            Person? Instance = new Person();
            try
            {
                Instance = await _repository.GetByIdAsync(Id);
                if (Instance == null) { return new Response<PersonDto>("Ошибка, данные введены не верно, информация о персоне не найдена", $"id={Id}"); }
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка, данные введены не верно, персона не найдена", ex.Message);
            }
            try
            {
                Instance.Surname = surname;
                Instance.Name = name;
                Instance.MiddleName = middleName;
                Instance.Phone = phone;
                Instance.Email = email;
                Instance.Birthday = birthday;
                Instance.Address = address;
                try
                {
                    PersonRole Role = await CheckRole(roleId);
                    Instance.Role = Role;
                }
                catch (RoleNotFoundException ex)
                {

                }
                Instance.DateUpdate = DateTime.Now;
                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<PersonDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<PersonDto>> GetAllEnumerable(bool isHidden)
        {
            try
            {
                return new Response<IEnumerable<PersonDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PersonDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<PersonDto>> GetPageEnumerable(bool isHidden, int start, int count)
        {
            try
            {
                return new Response<IEnumerable<PersonDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PersonDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<PersonDto>> HideOrShow(int id)
        {
            Person? Instance = new Person();
            try
            {
                Instance = await _repository.GetByIdAsync(id);
                if (Instance == null) { return new Response<PersonDto>("Ошибка, данные введены не верно, информация о персоне не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка, данные введены не верно, персона не найдена", ex.Message);
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

        public async Task<Response<PersonDto>> Delete(int id)
        {
            Person? instance = new Person();
            try
            {
                instance = await _repository.GetByIdAsync(id);
                if (instance == null) { return new Response<PersonDto>("Ошибка, данные введены не верно, информация о персоне не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка, данные введены не верно,персона не найдена", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка удаления", ex.Message);
            }
        }

        // Методы для интерефейса
        public Response<IEnumerable<PersonDto>> GetPageEnumerableByParams(int start, int count, int roleId, bool isHidden, int sortType)
        {
            try
            {
                var value = new Response<IEnumerable<Person>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden));
                if (roleId != 0)
                {
                    value.Value = value.Value.Where(r => r.Role.Id == roleId);
                }
                switch (sortType)
                {
                    case 1: value.Value = value.Value.OrderBy(i => i.Id); break;
                    case 2: value.Value = value.Value.OrderBy(s => s.Surname).ThenBy(n => n.Name); break;
                    case 3: value.Value = value.Value.OrderBy(r => r.Role.Id); break;
                }
                Response<IEnumerable<PersonDto>> _response = new();
                _response.Value = value.Value.Skip(start * count).Take(count).Select(t => t.ToDto());
                return _response;
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PersonDto>>("Ошибка чтения", ex.Message);
            }
        }


        // Вспомогательные методы
        private async Task<Response<PersonDto>> SaveChance(Person instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<PersonDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            Person load = await _repository.GetByIdAsync(instance.Id);
            return new Response<PersonDto>(load.ToDto());
        }

        // Проверка роли
        private async Task<PersonRole> CheckRole(int roleId)
        {
            PersonRole personRole = await _roleRepository.GetByIdAsyncWithoutLink(roleId);
            if (personRole == null) 
            {
                throw new RoleNotFoundException($"roleid = {roleId} not found");
            }
            return personRole;
        }
    }
}
