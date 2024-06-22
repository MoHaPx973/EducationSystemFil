using EducationSystem.App.CustomException;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces.RelationInterfaces;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Relationships;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;
using EducationSystem.App.Mappers.RelationMappers;
using EducationSystem.Domain.Models;
using EducationSystem.Shared.Models;

namespace EducationSystem.App.Interactor.RelationshipsInteractors
{
    public class ItemInCurriculumInteractor
    {
        private IGenericRepository<ItemInCurriculum> _genericRepository;
        private IGenericRepository<Item> _itemRepository;
        private IGenericRepository<Curriculum> _curriculumRepository;
        private IItemInCurriculumRepository _repository;
        private IUnitWork _unitWork;

        public ItemInCurriculumInteractor(IGenericRepository<ItemInCurriculum> genericRepository, 
            IGenericRepository<Item> itemRepository, IGenericRepository<Curriculum> curriculumRepository,
            IItemInCurriculumRepository repository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _itemRepository = itemRepository;
            _curriculumRepository = curriculumRepository;
            _repository = repository;
            _unitWork = unitWork;
        }

        // Создание
        public async Task<Response<ItemInCurriculumDto>> Insert(int itemId, int curriculumId,int numberOfHours)
        {
            ItemInCurriculum Instance = new();
            try
            {
                Item item = await CheckItem(itemId);
                Curriculum curriculum = await CheckCurriculum(curriculumId);
                Instance = new(item, curriculum, numberOfHours);
                _genericRepository.Insert(Instance);
            }
            catch (ItemNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные о предмете введены не верно", ex.Message);
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные об учебном плане введены не верно", ex.Message);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<ItemInCurriculumDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<ItemInCurriculumDto>>(_repository.GetAllEnumerable().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка чтения", ex.Message);
            }
        }
        public async Task<Response<IEnumerable<ItemInCurriculumDto>>> GetByCurriculumIdAsync(int curriculumId)
        {
            try
            {
                await CheckCurriculum(curriculumId);
                return new Response<IEnumerable<ItemInCurriculumDto>>(_repository.GetByCurriculumIdAsync(curriculumId).Select(s => s.ToDto()));
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка, данные об учебном плане введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка чтения", ex.Message);
            }
        }

        //public async Task<Response<IEnumerable<ItemInCurriculumDto>>> GetByCurriculumIdSystemTeachingNumber(int curriculumId,int systemTeachingNumber)
        //{
        //    try
        //    {
        //        await CheckCurriculum(curriculumId);
        //        return new Response<IEnumerable<ItemInCurriculumDto>>(_repository.GetByCurriculumIdAsync(curriculumId).Where(s=>s.SystemTeachingNumber== systemTeachingNumber).Select(s => s.ToDto()));
        //    }
        //    catch (CurriculumNotFoundException ex)
        //    {
        //        return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка, данные ою учебном плане введены не верно", ex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response<IEnumerable<ItemInCurriculumDto>>("Ошибка чтения", ex.Message);
        //    }
        //}

        // Удаление
        public async Task<Response<ItemInCurriculumDto>> Delete(int itemId, int curriculumId)
        {
            ItemInCurriculum? instance = new();
            try
            {
                await CheckItem(itemId);
                await CheckCurriculum(curriculumId);
                instance = _repository.GetOneByItemIdCurriculumId(itemId, curriculumId);
                if (instance != null)
                {
                    _genericRepository.DeleteWithoutLink(instance);
                    _unitWork.Commit();
                    return new Response<ItemInCurriculumDto>(instance.ToDto());
                }
                else
                    return new Response<ItemInCurriculumDto>("Связь не найдена", "Relationships not found");
            }
            catch (ItemNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные о предмете введены не верно", ex.Message);
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные об учебном плане введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка удаления", ex.Message);
            }
        }
        public async Task<Response<ItemInCurriculumDto>> Update(int itemId, int curriculumId, int numberOfHours)
        {
            ItemInCurriculum? instance = new();
            try
            {
                await CheckItem(itemId);
                await CheckCurriculum(curriculumId);
                instance = _repository.GetOneByItemIdCurriculumId(itemId, curriculumId);
                instance.NumberOfHours = numberOfHours;
                if (instance != null)
                {
                    _genericRepository.Update(instance);
                    _unitWork.Commit();
                    return new Response<ItemInCurriculumDto>(instance.ToDto());
                }
                else
                    return new Response<ItemInCurriculumDto>("Связь не найдена", "Relationships not found");
            }
            catch (ItemNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные о предмете введены не верно", ex.Message);
            }
            catch (CurriculumNotFoundException ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка, данные об учебном плане введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка удаления", ex.Message);
            }
        }

        // Сохранение в базу данных
        private async Task<Response<ItemInCurriculumDto>> SaveChance(ItemInCurriculum instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<ItemInCurriculumDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            ItemInCurriculum load = _repository.GetOneByItemIdCurriculumId(instance.ItemId,instance.CurriculumId);
            return new Response<ItemInCurriculumDto>(load.ToDto());
        }

        // Проверка роли
        private async Task<Item> CheckItem(int itemId)
        {
            Item item = await _itemRepository.GetByIdAsyncWithoutLink(itemId);
            if (item == null)
            {
                throw new ItemNotFoundException($"itemId = {itemId} not found");
            }
            return item;
        }
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
