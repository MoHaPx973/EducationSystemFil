using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers.AssessmentMapper;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.AssessmentInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.ClassInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.AssessmentModels;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Relationships;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.Models.AssessmentDto;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Interactor.AssessmentInteractor
{
    public class FinalAssessmentInteractor
    {
        private IGenericRepository<FinalAssessment> _genericRepository;
        private IPersonRepository _personRepository;
        private IStudentInClassRepository _studentInClassRepository;
        private ISchoolClassRepository _classRepository;
        private IItemInCurriculumRepository _itemRepository;
        private IFinalAssessmentRepository _repository;
        private IUnitWork _unitWork;

        public FinalAssessmentInteractor(IGenericRepository<FinalAssessment> genericRepository, IPersonRepository personRepository, IStudentInClassRepository studentInClassRepository, 
            ISchoolClassRepository classRepository, IItemInCurriculumRepository itemRepository,
            IFinalAssessmentRepository repository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _personRepository = personRepository;
            _studentInClassRepository = studentInClassRepository;
            _classRepository = classRepository;
            _itemRepository = itemRepository;
            _repository = repository;
            _unitWork = unitWork;
        }

        public async Task<Response<FinalAssessmentDto>> Insert
            (int studentId, int teacherId,int classId,int itemId, int point, int systemTeachingNumber, string descripton)
        {
            FinalAssessment Instance = new();
            try
            {
                Person student = await CheckPersonById(studentId,3);
                Person teacher = await CheckPersonById(teacherId, 2);
                SchoolClass studentClass = await CheckClass(classId);
                int SystemTeachingNumber = CheckSystemTeachingNumber(systemTeachingNumber, studentClass.LinkCurriculum.SystemTeaching);
                ItemInCurriculum itemInCurriculum = await CheckItem(itemId, studentClass.LinkCurriculum.Id);
                Instance = new(student, teacher, studentClass, itemInCurriculum, point, SystemTeachingNumber,descripton);
                _genericRepository.Insert(Instance);

            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<FinalAssessmentDto>("Ошибка,данные об оценке введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<FinalAssessmentDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<FinalAssessmentDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }
        public Response<FinalAssessmentDto> GetById(int assessmentId)
        {
            try
            {
                return new Response<FinalAssessmentDto>(_repository.GetById(assessmentId).ToDto());
            }
            catch (Exception ex)
            {
                return new Response<FinalAssessmentDto>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<FinalAssessmentDto>> GetAllByStudentId(int studentId)
        {
            try
            {
                return new Response<IEnumerable<FinalAssessmentDto>>(_repository.GetAllEnumerableByStudentId(studentId).Select(a=>a.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FinalAssessmentDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<FinalAssessmentDto>> GetByStudentIdClassId(int studentId,int classId)
        {
            try
            {
                return new Response<IEnumerable<FinalAssessmentDto>>(_repository.GetAllEnumerableByStudentIdByClassId(studentId,classId).Select(a => a.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FinalAssessmentDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<FinalAssessmentDto>> GetByStudentIdClassIdSystemTeachingNumber(int studentId, int classId,int systemTeaching)
        {
            try
            {
                return new Response<IEnumerable<FinalAssessmentDto>>(_repository.GetAllEnumerableByStudentIdByClassId(studentId, classId).Where(s=>s.SystemTeachingNumber==systemTeaching).Select(a => a.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<FinalAssessmentDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Вспомогательные методы
        private async Task<Response<FinalAssessmentDto>> SaveChance(FinalAssessment instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<FinalAssessmentDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            FinalAssessment load = await _repository.GetByIdAsync(instance.Id);
            return new Response<FinalAssessmentDto>(load.ToDto());
        }

        // Проверка ученика и учителя

        private async Task<Person> CheckPersonById(int personId, int roleId)
        {

            Person _person = await _personRepository.GetByIdAsync(personId);
            if (_person == null)
                throw new PersonNotFoundException($"Ошибка, персона не найдена _person = {personId}");
            if (_person.Role.Id == roleId)
                return _person;
            else
                throw new PersonRoleNotCorrect($"Ошибка _person = {personId} roleId = {_person.Role.Id} != {roleId}");
        }
        private async Task<SchoolClass> CheckClass(int classId)
        {
            SchoolClass _class = await _classRepository.GetByIdAsync(classId);
            if (_class == null)
                throw new ClassNotFoundException($"Ошибка, класс не найден classId = {classId}");
            IEnumerable<StudentInClass> studentInClass = _studentInClassRepository.GetByClassIdAsync(classId);
            foreach (var item in studentInClass)
            {
                if (item.ClassId == classId)
                    return _class;
            }
            throw new StudentNotInClass($"Студент не учится в указанном классе classId={classId}");
        }
        private async Task<ItemInCurriculum> CheckItem(int itemId,int curriculumId)
        {
            ItemInCurriculum itemInCurriculum = _itemRepository.GetOneByItemIdCurriculumId(itemId, curriculumId);
            if (itemInCurriculum == null)
                throw new ItemNotInCurriculum("Предмет в учебном плане не найден");
                    return itemInCurriculum;
        }
        private int CheckSystemTeachingNumber(int systemTeachingNumber, int systemTeaching)
        {
            if (systemTeachingNumber <= systemTeaching)
                return systemTeachingNumber;
            else throw new Exception("Учебный цикл не найден");
        }
        //private async Task<Curriculum> CheckCurriculum(int curriculumId)
        //{
        //    Curriculum curriculum = await _curriculumRepository.GetByIdAsyncWithoutLink(curriculumId);
        //    if (curriculum == null)
        //    {
        //        throw new CurriculumNotFoundException($"curriculumId = {curriculumId} not found");
        //    }
        //    return curriculum;
        //}
    }
}
