using EducationSystem.App.Mappers.OtherMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Other;
using EducationSystem.Shared.Other;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.OtherInteractor
{
    public class NewsInteractor
    {
        private IGenericRepository<NewsData> _genericRepository;
        private IUnitWork _unitWork;

        public NewsInteractor(IGenericRepository<NewsData> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание
        public async Task<Response<NewsDto>> Insert(string? title, string? text, string? path)
        {
            NewsData Instance = new();
            try
            {
                Instance = new(title,text,path);
                _genericRepository.Insert(Instance);
            }
            catch (Exception ex)
            { }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<NewsDto>> GetAllEnumerable()
        {
            Response<IEnumerable<NewsDto>> news = new();
            try
            {
                news = new Response<IEnumerable<NewsDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
                news.Value = news.Value.Where(i=>i.Title!="0");
                return news;
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<NewsDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<NewsDto>> GetAllEnumerableChat()
        {
            Response<IEnumerable<NewsDto>> news = new();
            try
            {
                news = new Response<IEnumerable<NewsDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
                news.Value = news.Value.Where(i => i.Title == "0");
                return news;
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<NewsDto>>("Ошибка чтения", ex.Message);
            }
        }

        public async Task<Response<NewsDto>> Delete(int id)
        {
            NewsData? instance = new();
            try
            {
                instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (instance == null) { return new Response<NewsDto>("Ошибка, данные введены не верно, информация о персоне не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<NewsDto>("Ошибка, данные введены не верно,персона не найдена", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<NewsDto>("Ошибка удаления", ex.Message);
            }
        }


        // Вспомогательные методы
        private async Task<Response<NewsDto>> SaveChance(NewsData instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<NewsDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            NewsData load = await _genericRepository.GetByIdAsyncWithoutLink(instance.Id);
            return new Response<NewsDto>(load.ToDto());
        }
    }
}
