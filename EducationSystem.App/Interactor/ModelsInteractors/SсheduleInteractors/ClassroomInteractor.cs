using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;


namespace EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors
{
    public class ClassroomInteractor
    {
        private IGenericRepository<Classroom> _genericRepository;
        private IUnitWork _unitWork;

        public ClassroomInteractor(IGenericRepository<Classroom> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание
        public async Task<Response<ClassroomDto>> Insert(int number, string? description)
        {
            Classroom Instance = new();
            try
            {
                Instance = new(number, description);
                _genericRepository.Insert(Instance);
            }

            catch (NullReferenceException ex)
            {
                return new Response<ClassroomDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<ClassroomDto>> GetByIdAsync(int number)
        {
            try
            {
                Classroom? Instance = await _genericRepository.GetByIdAsyncWithoutLink(number);
                if (Instance == null)
                {
                    return new Response<ClassroomDto>("Класс не найден", $"id = {number}");
                }
                return new Response<ClassroomDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<ClassroomDto>> Update(int number, string? description)
        {
            Classroom? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(number);
                if (Instance == null) { return new Response<ClassroomDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"number={number}"); }
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка, данные введены не верно, класс не найден", ex.Message);
            }
            try
            {
                //Instance.Number = number;
                Instance.Description = description;

                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<ClassroomDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<ClassroomDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<ClassroomDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ClassroomDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<ClassroomDto>> GetPageEnumerable(int start, int count)
        {
            try
            {
                return new Response<IEnumerable<ClassroomDto>>(_genericRepository.GetAllEnumerableWithoutLink().Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ClassroomDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление

        public async Task<Response<ClassroomDto>> Delete(int id)
        {
            Classroom? instance = new();
            try
            {
                instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (instance == null) { return new Response<ClassroomDto>("Ошибка, данные введены не верно, информация о классе не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка, данные введены не верно, класс не найден", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка удаления", ex.Message);
            }
        }


        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<ClassroomDto>> SaveChance(Classroom instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<ClassroomDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            return new Response<ClassroomDto>(instance.ToDto());
        }
    }
}
