using EducationSystem.App.Mappers.RolesMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Role;


namespace EducationSystem.App.Interactor.RoleInteractors
{
    public class PersonRoleInteractor
    {
        private IGenericRepository<PersonRole> _genericRepository;
        private IUnitWork _unitWork;

        public PersonRoleInteractor(IGenericRepository<PersonRole> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы 

        // Создание
        public Response<IEnumerable<PersonRoleDto>> FirstCreate()
        {
            List<PersonRole> output = new();
            try
            {
                if (_genericRepository.GetAllEnumerableWithoutLink().Count() != 0)
                {
                    return new Response<IEnumerable<PersonRoleDto>>("Стандартные роли уже созданы", "StandartPersonRole was created");
                }
                    output.Add(new("Администратор", "Администратор"));
                    output.Add(new("Преподаватель", "Роль для учителя или преподавателя"));
                    output.Add(new("Студент", "Роль для ученика или студента"));
                    output.Add(new("Родитель", "Роль для Родителя"));
                foreach (var item in output)
                {
                    _genericRepository.Insert(item);
                }
                _unitWork.Commit();
                if (_genericRepository.GetAllEnumerableWithoutLink().Count() != 0)
                {
                    return new Response<IEnumerable<PersonRoleDto>>(output.Select(s => s.ToDto()));
                }
                else
                    return new Response<IEnumerable<PersonRoleDto>>("Ошибка, стандартные роли не созданы", "StandartPersonRole error");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PersonRoleDto>>("Ошибка создания стандартных ролей", ex.Message);
            }

        }
        public Response<IEnumerable<PersonRoleDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<PersonRoleDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<PersonRoleDto>>("Ошибка чтения", ex.Message);
            }
        }
    }
}
