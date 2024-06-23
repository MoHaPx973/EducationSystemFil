using EducationSystem.App.Interactor.ModelsInteractors;
using EducationSystem.App.Interactor.OtherInteractor;
using EducationSystem.Shared.InputData;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.Other;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.OtherControllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : Controller
    {
        private NewsInteractor _interactor;
        public NewsController(NewsInteractor personinteractor)
        {
            _interactor = personinteractor;
        }

        [HttpPost("Create")]
        public async Task<Response<NewsDto>> Insert(NewsDto newEntity)
        {
            return await _interactor.Insert(newEntity.Title, newEntity.Text, newEntity.Path);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<NewsDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<NewsDto>> GetAllEnumerable()
        {
            return _interactor.GetAllEnumerable();
        }
        [HttpGet("GetAllEnumerableChat")]
        public Response<IEnumerable<NewsDto>> GetAllEnumerableChat()
        {
            return _interactor.GetAllEnumerableChat();
        }

    }
}
