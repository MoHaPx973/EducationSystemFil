using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.ClassInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors
{
    public class SchoolClassInteractor
    {
        private IGenericRepository<SchoolClass> _genericRepository;
        private IGenericRepository<Curriculum> _curriculumRepository;
        private ISchoolClassRepository _repository;
        private IUnitWork _unitWork;
        public SchoolClassInteractor(IGenericRepository<SchoolClass> genericRepository, IUnitWork unitWork, IGenericRepository<Curriculum> curriculumRepository, ISchoolClassRepository repository)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
            _curriculumRepository = curriculumRepository;
            _repository = repository;
        }

        // Методы

        // Создание
        public async Task<Response<SchoolClassDto>> Insert(int number,string? letter,int dateTime,int curriculumId)
        {
            SchoolClass Instance = new();
            try
            {
                Curriculum curriculum = await CheckCurriculum(curriculumId);
                Instance = new(number,letter,dateTime,curriculum);
                Instance.DateCreate = DateTime.Today;
                _genericRepository.Insert(Instance);
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<SchoolClassDto>("Ошибка,данные об учебном плане введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<SchoolClassDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<SchoolClassDto>> GetByIdAsync(int id)
        {
            try
            {
                SchoolClass? Instance = await _repository.GetByIdAsync(id);
                if (Instance == null)
                {
                    return new Response<SchoolClassDto>("Класс не найден", $"id = {id}");
                }
                return new Response<SchoolClassDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<SchoolClassDto>> Update(int Id, int number, string? letter, int dateTime, int curriculumId)
        {
            SchoolClass? Instance = new();
            try
            {
                Instance = await _repository.GetByIdAsync(Id);
                if (Instance == null) { return new Response<SchoolClassDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"id={Id}"); }
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка, данные введены не верно, класс не найден", ex.Message);
            }
            try
            {
                Instance.Number = number;
                Instance.Letter = letter;
                Instance.YearFormation = dateTime;
                try
                {
                    Curriculum curriculum = await CheckCurriculum(curriculumId);
                    Instance.LinkCurriculum = curriculum;
                }
                catch (CurriculumNotFoundException ex)
                {

                }
                Instance.DateUpdate = DateTime.Now;
                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<SchoolClassDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<SchoolClassDto>> GetAllEnumerable(bool isHidden)
        {
            try
            {
                return new Response<IEnumerable<SchoolClassDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<SchoolClassDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<SchoolClassDto>> GetPageEnumerable(bool isHidden, int start, int count)
        {
            try
            {
                return new Response<IEnumerable<SchoolClassDto>>(_repository.GetAllEnumerable().Where(h => h.IsHidden == isHidden).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<SchoolClassDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<SchoolClassDto>> GetAllByYear(int year)
        {
            try
            {
                return new Response<IEnumerable<SchoolClassDto>>
                    (_repository.GetAllEnumerable().Where(h => h.IsHidden == false).Where(y=>y.YearFormation==year).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<SchoolClassDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<SchoolClassDto>> HideOrShow(int id)
        {
            SchoolClass? Instance = new();
            try
            {
                Instance = await _repository.GetByIdAsync(id);
                if (Instance == null) { return new Response<SchoolClassDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка, данные введены не верно, класс не найден", ex.Message);
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

        public async Task<Response<SchoolClassDto>> Delete(int id)
        {
            SchoolClass? instance = new();
            try
            {
                instance = await _repository.GetByIdAsync(id);
                if (instance == null) { return new Response<SchoolClassDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка, данные введены не верно,класс не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка удаления", ex.Message);
            }
        }

        // Вспомогательные методы

        // Сохранение в базу данных

        private async Task<Response<SchoolClassDto>> SaveChance(SchoolClass instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<SchoolClassDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            SchoolClass load = await _repository.GetByIdAsync(instance.Id);
            return new Response<SchoolClassDto>(load.ToDto());
        }

        // Проверка роли
        private async Task<Curriculum> CheckCurriculum(int curriculumId)
        {
            Curriculum curriculum = await _curriculumRepository.GetByIdAsyncWithoutLink(curriculumId);
            if (curriculum == null)
            {
                throw new CurriculumNotFoundException($"curriculumId = {curriculumId} not found");
            }
            return curriculum;
        }
    }
}
