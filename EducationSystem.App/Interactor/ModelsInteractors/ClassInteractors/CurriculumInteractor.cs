using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;


namespace EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors
{
    public class CurriculumInteractor
    {
        private IGenericRepository<Curriculum> _genericRepository;
        private IUnitWork _unitWork;

        public CurriculumInteractor(IGenericRepository<Curriculum> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание
        public async Task<Response<CurriculumDto>> Insert(int number, int yearFormation, int systemTeaching, string? description)
        {
            Curriculum Instance = new();
            try
            {
                Instance = new(number,yearFormation,systemTeaching,description);
                Instance.DateCreate = DateTime.Today;
                _genericRepository.Insert(Instance);
            }
           
            catch (NullReferenceException ex)
            {
                return new Response<CurriculumDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<CurriculumDto>> GetByIdAsync(int id)
        {
            try
            {
                Curriculum? Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (Instance == null)
                {
                    return new Response<CurriculumDto>("Учебный план не найден", $"id = {id}");
                }
                return new Response<CurriculumDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<CurriculumDto>> Update(int Id, int number, int yearFormation, int systemTeaching, string? description)
        {
            Curriculum? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(Id);
                if (Instance == null) { return new Response<CurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане не найдена", $"id={Id}"); }
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
            }
            try
            {
                Instance.Number = number;
                Instance.YearFormation = yearFormation;
                Instance.SystemTeaching = systemTeaching;
                Instance.Description = description;
                
                Instance.DateUpdate = DateTime.Now;
                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<CurriculumDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<CurriculumDto>> GetAllEnumerable(bool isHidden)
        {
            try
            {
                return new Response<IEnumerable<CurriculumDto>>(_genericRepository.GetAllEnumerableWithoutLink().Where(h => h.IsHidden == isHidden).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<CurriculumDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<CurriculumDto>> GetPageEnumerable(bool isHidden, int start, int count)
        {
            try
            {
                return new Response<IEnumerable<CurriculumDto>>(_genericRepository.GetAllEnumerableWithoutLink().Where(h => h.IsHidden == isHidden).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<CurriculumDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<CurriculumDto>> HideOrShow(int id)
        {
            Curriculum? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (Instance == null) { return new Response<CurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
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

        public async Task<Response<CurriculumDto>> Delete(int id)
        {
            Curriculum? instance = new();
            try
            {
                instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (instance == null) { return new Response<CurriculumDto>("Ошибка, данные введены не верно, информация об учебном плане не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка, данные введены не верно, учебный план не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка удаления", ex.Message);
            }
        }

        // Методы для интерфейса
        public Response<IEnumerable<CurriculumDto>> GetPageEnumerableByYear(int year,bool isHidden, int start, int count)
        {
            if (year == 0) 
            {
                return GetPageEnumerable(isHidden,start,count);
            }
            else
            try
            {
                return new Response<IEnumerable<CurriculumDto>>(_genericRepository.GetAllEnumerableWithoutLink().Where(h => h.IsHidden == isHidden).Where(y=>y.YearFormation==year).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<CurriculumDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<CurriculumDto>> SaveChance(Curriculum instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<CurriculumDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            return new Response<CurriculumDto>(instance.ToDto());
        }
    }
}
