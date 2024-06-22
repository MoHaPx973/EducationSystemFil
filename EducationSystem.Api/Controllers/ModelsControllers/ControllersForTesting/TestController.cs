using EducationSystem.App.Mappers.ModelsMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Models.ClassModels;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using EducationSystem.App.Mappers.ModelsMappers.ClassMappers;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.ControllersForTesting
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : Controller
    {
        IGenericRepository<Person> _repository;
        IGenericRepository<Item> _repository2;
        IUnitWork _unitWork;
        IGenericRepository<PersonRole> _interactor;

        public TestController(IGenericRepository<Person> repository, IUnitWork unitWork, IGenericRepository<PersonRole> interactor, IGenericRepository<Item> repository2)
        {
            _repository = repository;
            _unitWork = unitWork;
            _interactor = interactor;
            _repository2 = repository2;
        }

        //[HttpPost("CreateTestPerson")]
        //public async Task<Response<IEnumerable<PersonDto>>> CreateTestPerson()
        //{
        //    List<Person> output = new();

        //    IEnumerable<PersonRole> ListRole;
        //    ListRole = _interactor.GetAllEnumerableWithoutLink();
        //    List<PersonRole> list = ListRole.ToList();

        //    //public Person(string surname, string name, string middleName, PersonRole role)
        //    int name=0;
        //    int surname=0;
        //    int middleName = 0;
        //    Random rand = new Random();
        //    for (int i = 0; i < 50; i++)
        //    {
        //        output.Add(new Person($"Surname{surname}", $"Name{name}", $"Middlename{middleName}", list[rand.Next(1,3)]));
        //        name = rand.Next(30);
        //        surname = rand.Next(30);
        //        middleName = rand.Next(30);
        //    }
        //    foreach (var item in output)
        //    {
        //        _repository.Insert(item);
        //    }
        //    _unitWork.Commit();
        //    return new Response<IEnumerable<PersonDto>>(output.Select(s => s.ToDto()));
        //}

        
    }
}
