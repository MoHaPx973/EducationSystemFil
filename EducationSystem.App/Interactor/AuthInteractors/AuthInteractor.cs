using EducationSystem.App.Storage.AuthInterfaces;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.OutputData;
using System.ComponentModel.DataAnnotations;
using EducationSystem.App.Mappers.AuthMapper;
using EducationSystem.Domain.Role;
using EducationSystem.App.CustomException;
using EducationSystem.Shared.Auth;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Interactor.AuthInteractors
{
    public class AuthInteractor
    {
        private IGenericRepository<User> _genericRepository;
        public IAuthRepository _repository;
        private IUnitWork _unitWork;
        private IPersonRepository _persongenericRepository;
        private IGenericRepository<UserRole> _roleRepository;
        private IAuthenticationService _authservice;

        public AuthInteractor(IGenericRepository<User> genericRepository, IUnitWork unitWork,
            IPersonRepository persongenericRepository, IAuthRepository authRepository, IAuthenticationService authservice, IGenericRepository<UserRole> roleRepository)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
            _persongenericRepository = persongenericRepository;
            _repository = authRepository;
            _authservice = authservice;
            _roleRepository = roleRepository;   
        }

        // Методы

        // Создание
        public async Task<Response<UserDto>> Insert(string? login, string? password, int roleId, int personId)
        {
            User Instance = new();
            try
            {
                UserRole Role = await CheckRole(roleId);
                Person NewLinkPerson = await CheckPerson(personId);
                Instance = new(login,password,Role, NewLinkPerson);
                _genericRepository.Insert(Instance);
            }
            catch (RoleNotFoundException ex)
            {
                return new Response<UserDto>("Ошибка,данные о роли введены не верно", ex.Message);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<UserDto>("Ошибка,данные о пользователе введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<UserDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                User? instance = await _repository.GetByIdAsync(id);
                if (instance == null)
                {
                    return new Response<UserDto>("Пользователь не найден", $"id = {id}");
                }
                return new Response<UserDto>(instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }
        public Response<IEnumerable<UserDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<UserDto>>(_repository.GetAllEnumerable().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<UserDto>>("Ошибка чтения", ex.Message);
            }
        }

        public async Task<Response<UserDto>> GetByLoginAsync(string login)
        {
            try
            {
                User? instance = await _repository.GetByLogin(login);
                if (instance == null)
                {
                    return new Response<UserDto>("Пользователь не найден", $"login = {login}");
                }
                instance.Password = "hidden";
                return new Response<UserDto>(instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<UserDto>> Delete(int id)
        {
            User? instance = new User();
            try
            {
                instance = await _repository.GetByIdAsync(id);
                if (instance == null) { return new Response<UserDto>("Ошибка, данные введены неверно, информация об учителе не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка, данные введены неверно,учитель не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка удаления", ex.Message);

            }
        }


        // Вспомогательные методы

        // Сохранения в базу данных
        private async Task<Response<UserDto>> SaveChance(User instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<UserDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            User load = await _repository.GetByIdAsync(instance.Id);
            return new Response<UserDto>(load.ToDto());
        }
        private async Task<Person> CheckPerson(int personId)
        {

            Person person = await _persongenericRepository.GetByIdAsync(personId);
            if (person == null)
            {
                throw new PersonNotFoundException($"personId = {personId} not found");
            }
            return person;
        }

        private async Task<UserRole> CheckRole(int roleId)
        {
            UserRole userRole = await _roleRepository.GetByIdAsyncWithoutLink(roleId);
            if (userRole == null)
            {
                throw new RoleNotFoundException($"roleid = {roleId} not found");
            }
            return userRole;
        }

        public async Task<Response<string>> AuthenticateAsync(string? login, string? password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ValidationException("Не все поля были заполнены");
                }

                var user = await _repository.GetByLogin(login);

                if (user == null)
                {
                    throw new NullReferenceException("Данные введены не верно");
                }

                var authenticationResult = _authservice.Authenticate(login, password, user.Password, user.Role.Name);

                if (authenticationResult == null)
                {
                    throw new ValidationException("Данные введены не верно");
                }

                return new Response<string>(authenticationResult);
            }
            catch (NullReferenceException ex)
            {
                return new Response<string>("", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<string>("", ex.Message);
            }
        }
    }
}
