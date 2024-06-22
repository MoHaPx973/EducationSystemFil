using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors
{
    public class ItemInteractor
    {
        private IGenericRepository<Item> _genericRepository;
        private IUnitWork _unitWork;

        public ItemInteractor(IGenericRepository<Item> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание
        public async Task<Response<ItemDto>> Insert(string? name,string? description)
        {
            Item Instance = new();
            try
            {
                Instance = new(name, description);
                Instance.DateCreate = DateTime.Today;
                _genericRepository.Insert(Instance);
            }

            catch (NullReferenceException ex)
            {
                return new Response<ItemDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<ItemDto>> GetByIdAsync(int id)
        {
            try
            {
                Item? Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (Instance == null)
                {
                    return new Response<ItemDto>("Предмет не найден", $"id = {id}");
                }
                return new Response<ItemDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<ItemDto>> Update(int Id, string? name, string? description)
        {
            Item? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(Id);
                if (Instance == null) { return new Response<ItemDto>("Ошибка, данные введены не верно, информация о предмете не найдена", $"id={Id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка, данные введены не верно, предмет не найден", ex.Message);
            }
            try
            {
                Instance.Name = name;
                Instance.Description = description;

                Instance.DateUpdate = DateTime.Now;
                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ItemDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<ItemDto>> GetAllEnumerable(bool isHidden)
        {
            try
            {
                return new Response<IEnumerable<ItemDto>>(_genericRepository.GetAllEnumerableWithoutLink().Where(h => h.IsHidden == isHidden).Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ItemDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<ItemDto>> GetPageEnumerable(bool isHidden, int start, int count)
        {
            try
            {
                return new Response<IEnumerable<ItemDto>>(_genericRepository.GetAllEnumerableWithoutLink().Where(h => h.IsHidden == isHidden).Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ItemDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление
        public async Task<Response<ItemDto>> HideOrShow(int id)
        {
            Item? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (Instance == null) { return new Response<ItemDto>("Ошибка, данные введены не верно, информация о предмете не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка, данные введены не верно, предмет не найден", ex.Message);
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

        public async Task<Response<ItemDto>> Delete(int id)
        {
            Item? instance = new();
            try
            {
                instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (instance == null) { return new Response<ItemDto>("Ошибка, данные введены не верно, информация о предмете не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка, данные введены не верно, предмет не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка удаления", ex.Message);
            }
        }


        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<ItemDto>> SaveChance(Item instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<ItemDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            return new Response<ItemDto>(instance.ToDto());
        }
    }
}
