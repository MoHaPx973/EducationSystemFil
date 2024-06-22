using EducationSystem.App.Mappers.ModelsMappers.SсheduleMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models.SсheduleModels;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;

namespace EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors
{
    public class StudyDayInteractor
    {
        private IGenericRepository<StudyDay> _genericRepository;
        private IUnitWork _unitWork;

        public StudyDayInteractor(IGenericRepository<StudyDay> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            _unitWork = unitWork;
        }

        // Методы

        // Создание множества дней с начала учебного года
        public async Task <Response<IEnumerable<StudyDayDto>>> InsertMany(int firstSeptemberDateNumber,bool isLeap,int year)
        {
            int weekNumber = 1;
            int numberDays;
            if (isLeap)
                numberDays = 366;
            else numberDays = 365;

            DateTime dateTime = new DateTime(year,9,1);
            List<StudyDay> days = new List<StudyDay>();
            for (int i = 1; i < numberDays; i++)
            {
                if (firstSeptemberDateNumber<7)
                {
                    days.Add(new StudyDay(dateTime,firstSeptemberDateNumber,weekNumber));
                    dateTime = dateTime.AddDays(1);
                    firstSeptemberDateNumber++;
                    
                }
                else
                {
                    weekNumber++;
                    firstSeptemberDateNumber = 1;
                    dateTime = dateTime.AddDays(1);
                }
            }
            foreach (StudyDay day in days) 
            {
                _genericRepository.Insert(day);
                await _unitWork.Commit();
            }
            
            return new Response<IEnumerable<StudyDayDto>>(days.Select(e=>e.ToDto()));
           
        }

        public async Task<Response<IEnumerable<StudyDayDto>>> GetAll()
        {
            try
            {
                return new Response<IEnumerable<StudyDayDto>>(_genericRepository.GetAllEnumerableWithoutLink().Select(s => s.ToDto()));
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<StudyDayDto>>("Ошибка чтения", ex.Message);
            }
        }

		// Поиск по идентификатору
		public async Task<Response<StudyDayDto>> GetByIdAsync(int id)
		{
			try
			{
				StudyDay? Instance = await _genericRepository.GetByIdAsyncWithoutLink(id);
				if (Instance == null)
				{
					return new Response<StudyDayDto>("День не найден", $"id = {id}");
				}
				return new Response<StudyDayDto>(Instance.ToDto());
			}
			catch (Exception ex)
			{
				return new Response<StudyDayDto>("Ошибка при чтении из базы данных", ex.Message);
			}
		}

		public async Task<Response<StudyDayDto>> GetByDateAsync(DateTime date)
        {
            try
            {
                StudyDay? Instance = await _genericRepository.GetByDateAsyncWithoutLink(date);
                if (Instance == null)
                {
                    return new Response<StudyDayDto>("День не найден", $"date = {date}");
                }
                return new Response<StudyDayDto>(Instance.ToDto());
            }
            catch (Exception ex)
            {
                return new Response<StudyDayDto>("Ошибка при чтении из базы данных", ex.Message);
            }
        }

        // Удаление года
        public async Task<Response<IEnumerable<StudyDayDto>>> Delete(bool isLeap, int year)
        {
            int numberDays;
            if (isLeap)
                numberDays = 366;
            else numberDays = 365;

            DateTime dateTime = new DateTime(year, 9, 1);
            List<StudyDay> days = new List<StudyDay>();
            for (int i = 1; i < numberDays; i++)
            {
                days.Add(await _genericRepository.GetByDateAsyncWithoutLink(dateTime));
                dateTime = dateTime.AddDays(1);
            }
            foreach (StudyDay day in days)
            {
                if (day!=null)
                {
                    _genericRepository.DeleteWithoutLink(day);
                    await _unitWork.Commit();
                }
                
            }

            return new Response<IEnumerable<StudyDayDto>>(days.Select(e => e.ToDto()));

        }

    }
}
