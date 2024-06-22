using EducationSystem.App.Interactor.ModelsInteractors;
using EducationSystem.Shared.InputData;
using EducationSystem.Shared.Models;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private PersonInteractor _interactor;
        public PersonController(PersonInteractor personinteractor)
        {
            _interactor = personinteractor;
        }

        //[HttpPut("{buildingObjectId}/documents/{documentId}")]
        [HttpPost("Create")]
        public async Task<Response<PersonDto>> Insert(PersonInput newEntity)
        {
            return await _interactor.Insert(newEntity.Surname, newEntity.Name, newEntity.MiddleName,newEntity.Phone,newEntity.Email,newEntity.Birthday,newEntity.Gender,newEntity.Address, newEntity.RoleId);

        }
        [HttpGet("FindById/{id}")]
        public async Task<Response<PersonDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<PersonDto>> Update(int id, PersonInput newData)
        {
            return await _interactor.Update(id, newData.Surname, newData.Name, newData.MiddleName, newData.Phone, newData.Email, newData.Birthday, newData.Address, newData.RoleId);
        }
        [HttpDelete("HideOrShow/{id}")]
        public async Task<Response<PersonDto>> HideOrShow(int id)
        {
            return await _interactor.HideOrShow(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<PersonDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable/{isHidden}")]
        public Response<IEnumerable<PersonDto>> GetAllEnumerable(bool isHidden)
        {
            return _interactor.GetAllEnumerable(isHidden);
        }
        [HttpGet("GetPageEnumerable/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<PersonDto>> GetPageByEnumerable(bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerable(isHidden, start, count);
        }
        [HttpGet("GetPageEnumerableByParams/{start}/{count}/{roleId}/{isHidden}/{sortType}")]
        public Response<IEnumerable<PersonDto>> GetPageEnumerableByParams(int start, int count, int roleId, bool isHidden, int sortType)
        {
            return _interactor.GetPageEnumerableByParams(start, count, roleId, isHidden, sortType);

        }

        //		[HttpGet("GetAllByClassId")]
        //		public Response<IEnumerable<PersonDto>> GetAllByClassId(int classId)
        //		{
        //			return _personinteractor.GetAllByClassId(classId);
        //		}

    }
}
