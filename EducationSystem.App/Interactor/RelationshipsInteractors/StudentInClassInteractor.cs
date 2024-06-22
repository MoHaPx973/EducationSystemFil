using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.RelationMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;

namespace EducationSystem.App.Interactor.RelationshipsInteractors
{
    public class StudentInClassInteractor
    {
        private IGenericRepository<StudentInClass> _genericRepository;
        private IGenericRepository<Person> _personRepository;
        private IGenericRepository<SchoolClass> _classRepository;
        private IStudentInClassRepository _repository;
        private IUnitWork _unitWork;

        public StudentInClassInteractor(IGenericRepository<StudentInClass> genericRepository, 
            IGenericRepository<Person> personRepository, IGenericRepository<SchoolClass> classRepository, 
            IStudentInClassRepository repository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _personRepository = personRepository;
            _classRepository = classRepository;
            _repository = repository;
            _unitWork = unitWork;
        }


        // Создание
        public async Task<Response<StudentInClassDto>> Insert(int studentId, int classId)
        {
            StudentInClass Instance = new();
            try
            {
                Person person = await CheckPerson(studentId);
                SchoolClass schoolClass = await CheckClass(classId);
                Instance = new(person,schoolClass);
                _genericRepository.Insert(Instance);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (ClassNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о классе введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<StudentInClassDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<StudentInClassDto>> GetAllEnumerable(bool isStuding)
        {
            try
            {
                return new Response<IEnumerable<StudentInClassDto>>(_repository.GetAllEnumerable(isStuding).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<StudentInClassDto>>("Ошибка чтения", ex.Message);
            }
        }
        public async Task<Response<IEnumerable<StudentInClassDto>>> GetAllEnumerableByStudentId(int studentId)
        {
            try
            {
                await CheckPerson(studentId);
                return new Response<IEnumerable<StudentInClassDto>>(_repository.GetByStudentIdAsync(studentId).Select(s => s.ToDto()));
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<IEnumerable<StudentInClassDto>>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<StudentInClassDto>>("Ошибка чтения", ex.Message);
            }
        }
        public async Task<Response<IEnumerable<StudentInClassDto>>> GetAllEnumerableByClassId(int classId)
        {
            try
            {
                await CheckClass(classId);
                return new Response<IEnumerable<StudentInClassDto>>(_repository.GetByClassIdAsync(classId).Select(s => s.ToDto()));
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<IEnumerable<StudentInClassDto>>("Ошибка, данные о классе введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<StudentInClassDto>>("Ошибка чтения", ex.Message);
            }
        }
        // Удаление
        public async Task<Response<StudentInClassDto>> Delete(int studentId,int classId)
        {
            StudentInClass? instance = new StudentInClass();
            try
            {
                await CheckPerson(studentId);
                await CheckClass(classId);
                instance = _repository.GetOneByStudentIdClassId(studentId,classId);
                if (instance != null)
                {
                    _genericRepository.DeleteWithoutLink(instance);
                    _unitWork.Commit();
                    return new Response<StudentInClassDto>(instance.ToDto());
                }
                else
                    return new Response<StudentInClassDto>("Связь не найдена","Relationships not found");
            }
            catch(ClassNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о классе введены не верно", ex.Message);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<StudentInClassDto>("Ошибка удаления", ex.Message);
            }
        }
        public async Task<Response<StudentInClassDto>> HideOrShow(int studentId, int classId)
        {
            StudentInClass? instance = new StudentInClass();
            try
            {
                await CheckPerson(studentId);
                await CheckClass(classId);
                instance = _repository.GetOneByStudentIdClassId(studentId, classId);
                instance.IsStuding = !instance.IsStuding;
                if (instance != null)
                {
                    _genericRepository.Update(instance);
                    _unitWork.Commit();
                    return new Response<StudentInClassDto>(instance.ToDto());
                }
                else
                    return new Response<StudentInClassDto>("Связь не найдена", "Relationships not found");
            }
            catch (ClassNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о классе введены не верно", ex.Message);
            }
            catch (PersonNotFoundException ex)
            {
                return new Response<StudentInClassDto>("Ошибка, данные о персоне введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<StudentInClassDto>("Ошибка удаления", ex.Message);
            }
        }

        // Сохранение в базу данных
        private async Task<Response<StudentInClassDto>> SaveChance(StudentInClass instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<StudentInClassDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            StudentInClass load = _repository.GetOneByStudentIdClassId(instance.StudentId, instance.ClassId);
            return new Response<StudentInClassDto>(load.ToDto());
        }


        // Проверка роли
        private async Task<Person> CheckPerson(int personId)
        {
            Person person = await _personRepository.GetByIdAsyncWithoutLink(personId);
            if (person == null)
            {
                throw new PersonNotFoundException($"personId = {personId} not found");
            }
            return person;
        }
        private async Task<SchoolClass> CheckClass(int classId)
        {
            SchoolClass schoolClass = await _classRepository.GetByIdAsyncWithoutLink(classId);
            if (schoolClass == null)
            {
                throw new ClassNotFoundException($"classId = {classId} not found");
            }
            return schoolClass;
        }
    }
}
