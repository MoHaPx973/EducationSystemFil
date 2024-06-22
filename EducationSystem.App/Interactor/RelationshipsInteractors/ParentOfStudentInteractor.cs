using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.RelationMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;

namespace EducationSystem.App.Interactor.RelationshipsInteractors
{
    public class ParentOfStudentInteractor
    {
        private IGenericRepository<ParentsOfStudents> _genericRepository;
        private IPersonRepository _personRepository;
        private IParentOfStudentRepository _repository;
        private IUnitWork _unitWork;

        public ParentOfStudentInteractor(IGenericRepository<ParentsOfStudents> genericRepository, IPersonRepository personRepository,
                                        IParentOfStudentRepository repository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _personRepository = personRepository;
            _repository = repository;
            _unitWork = unitWork;
        }

        // Создание
        public async Task<Response<ParentsOfStudentsDto>> Insert(int parentId, int studentId)
        {
            ParentsOfStudents Instance = new();
            try
            {
                Person parent = await CheckPerson(parentId, 4);
                Person student = await CheckPerson(studentId, 3);
                Instance = new(parent, student);
                _genericRepository.Insert(Instance);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (RoleNotFoundException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные об ролях персон введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<ParentsOfStudentsDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>(_repository.GetAllEnumerable().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>("Ошибка чтения", ex.Message);
            }
        }
        public async Task<Response<IEnumerable<ParentsOfStudentsDto>>> GetByParentIdAsync(int parentId)
        {
            try
            {
                await CheckPerson(parentId, 4);
                return new Response<IEnumerable<ParentsOfStudentsDto>>(_repository.GetByParentIdAsync(parentId).Select(s => s.ToDto()));
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>("Ошибка, данные о родителе введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>("Ошибка чтения", ex.Message);
            }
        }
        public async Task<Response<IEnumerable<ParentsOfStudentsDto>>> GetByStudentIdAsync(int studentId)
        {
            try
            {
                await CheckPerson(studentId, 3);
                return new Response<IEnumerable<ParentsOfStudentsDto>>(_repository.GetByStudentIdAsync(studentId).Select(s => s.ToDto()));
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>("Ошибка, данные об ученике введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ParentsOfStudentsDto>>("Ошибка чтения", ex.Message);
            }
        }

        // удаление
        public async Task<Response<ParentsOfStudentsDto>> Delete(int parentId, int studentId)
        {
            ParentsOfStudents? instance = new();
            try
            {
                await CheckPerson(parentId,4);
                await CheckPerson(studentId, 3);
                instance = _repository.GetOneByParentIdStudentId(parentId,studentId);
                if (instance != null)
                {
                    _genericRepository.DeleteWithoutLink(instance);
                    _unitWork.Commit();
                    return new Response<ParentsOfStudentsDto>(instance.ToDto());
                }
                else
                    return new Response<ParentsOfStudentsDto>("Связь не найдена", "Relationships not found");
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (RoleNotFoundException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные об ролях персон введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка удаления", ex.Message);
            }
        }

        // Сохранение в базу данных
        private async Task<Response<ParentsOfStudentsDto>> SaveChance(ParentsOfStudents instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<ParentsOfStudentsDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            return new Response<ParentsOfStudentsDto>(instance.ToDto());
        }

        // Проверка 
        private async Task<Person> CheckPerson(int personId, int roleId)
        {
            Person person = await _personRepository.GetByIdAsync(personId);
            if (person == null)
            {
                throw new PersonNotFoundException($"personId = {personId} not found");
            }
            if (person.Role.Id != roleId)
            {
                throw new RoleNotFoundException($"personRoleId {roleId} = {person.Role.Id} not correct");
            }
            return person;
        }
    }
}
