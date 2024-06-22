using EducationSystem.App.Mappers.RolesMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Role;

namespace EducationSystem.App.Interactor.RoleInteractors
{
    public class UserRoleInteractor
    {
        private IGenericRepository<UserRole> _genericRepository;
        private IUnitWork _unitWork;

        public UserRoleInteractor(IGenericRepository<UserRole> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы 

        // Создание
        public Response<IEnumerable<UserRoleDto>> FirstCreate()
        {
            List<UserRole> output = new();
            try
            {
                if (_genericRepository.GetAllEnumerableWithoutLink().Count()!=0)
                {
                    return new Response<IEnumerable<UserRoleDto>>("Стандартные роли уже созданы","StandartUserRole was created");
                }
                output.Add(new("SuperAdmin", "Администратор с повышенными правами", 1));
                output.Add(new("Administrator", "Администратор", 1));
                output.Add(new("Teacher", "Роль для учителя или преподавателя", 2));
                output.Add(new("Student", "Роль для ученика или студента", 3));
                output.Add(new("Parent", "Роль для Родителя", 4));
                foreach (var item in output)
                {
                    _genericRepository.Insert(item);
                }
                _unitWork.Commit();
                if (_genericRepository.GetAllEnumerableWithoutLink().Count() != 0)
                {
                    return new Response<IEnumerable<UserRoleDto>>(output.Select(s=>s.ToDto()));
                }
                else
                    return new Response<IEnumerable<UserRoleDto>>("Ошибка, стандартные роли не созданы", "StandartUserRole error");
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<UserRoleDto>>("Ошибка создания стандартных ролей", ex.Message);
            }
            
        }
        public Response<IEnumerable<UserRoleDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<UserRoleDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<UserRoleDto>>("Ошибка чтения", ex.Message);
            }
        }
    }
}
