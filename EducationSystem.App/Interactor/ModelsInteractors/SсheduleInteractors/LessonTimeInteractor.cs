using EducationSystem.App.CustomException;
using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors
{
    public class LessonTimeInteractor
    {
        private IGenericRepository<LessonTime> _genericRepository;
        private IUnitWork _unitWork;

        public LessonTimeInteractor(IGenericRepository<LessonTime> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание
        public async Task<Response<LessonTimeDto>> Insert(int number,string? startTime,string? endTime)
        {
            LessonTime Instance = new();
            try
            {
                Instance.Number = number;
                Instance._startTime = startTime;
                Instance._endTime = endTime;
                _genericRepository.Insert(Instance);
            }

            catch (NullReferenceException ex)
            {
                return new Response<LessonTimeDto>("Ошибка, данные введены не верно", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка при записи в базу данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Поиск по идентификатору
        public async Task<Response<LessonTimeDto>> GetByIdAsync(int id)
        {
            try
            {
                LessonTime? Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (Instance == null)
                {
                    return new Response<LessonTimeDto>("Время урока не найдено", $"id = {id}");
                }
                return new Response<LessonTimeDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Обновление данных
        public async Task<Response<LessonTimeDto>> Update(int number, string? startTime, string? endTime)
        {
            LessonTime? Instance = new();
            try
            {
                Instance = await _genericRepository.GetByIdAsyncWithoutLink(number);
                if (Instance == null) { return new Response<LessonTimeDto>("Ошибка, данные введены не верно, Время урока не найдено", $"number={number}"); }
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка, данные введены не верно, Время урока не найдено", ex.Message);
            }
            try
            {
                Instance.Number = number;
                Instance._startTime = startTime;
                Instance._endTime = endTime;

                _genericRepository.Update(Instance);
            }
            catch (NullReferenceException ex)
            {
                return new Response<LessonTimeDto>("Данные введены не верное", ex.Message);
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка изменения данных", ex.Message);
            }
            return await SaveChance(Instance);
        }

        // Вывод всех данных из списка 
        public Response<IEnumerable<LessonTimeDto>> GetAllEnumerable()
        {
            try
            {
                return new Response<IEnumerable<LessonTimeDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonTimeDto>>("Ошибка чтения", ex.Message);
            }
        }

        public Response<IEnumerable<LessonTimeDto>> GetPageEnumerable(int start, int count)
        {
            try
            {
                return new Response<IEnumerable<LessonTimeDto>>(_genericRepository.GetAllEnumerableWithoutLink().Skip(start * count).Take(count).Select(t => t.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<LessonTimeDto>>("Ошибка чтения", ex.Message);
            }
        }

        // Удаление

        public async Task<Response<LessonTimeDto>> Delete(int id)
        {
            LessonTime? instance = new();
            try
            {
                instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
                if (instance == null) { return new Response<LessonTimeDto>("Ошибка, данные введены не верно, информация о Времи урока не найдена", $"id={id}"); }
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка, данные введены не верно, Время урока не найдено", ex.Message);
            }
            try
            {
                _genericRepository.DeleteWithoutLink(instance);
                return await SaveChance(instance);
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка удаления", ex.Message);
            }
        }


        // Вспомогательные методы

        // Сохранение в базу данных
        private async Task<Response<LessonTimeDto>> SaveChance(LessonTime instance)
        {
            try
            {
                await _unitWork.Commit();
            }
            catch (Exception ex)
            {
                return new Response<LessonTimeDto>("Ошибка сохранения в базу данных", ex.Message);
            }
            return new Response<LessonTimeDto>(instance.ToDto());
        }

        // Convert string to double
        private double ConvertTimeToDouble(string time)
        {
            string[] parts = time.Split(':');
            if (parts.Length == 2)
            {
                int hours, minutes;
                if (int.TryParse(parts[0], out hours) && int.TryParse(parts[1], out minutes))
                {
                    double result = hours + (double)minutes / 60;
                    return result;
                }
                else
                {
                    throw new TimeNotFoundException("Ошибка заполнения времени");
                }
            }
            else
            {
                throw new TimeNotFoundException("Ошибка заполнения времени");
            }
        }
    }
}
