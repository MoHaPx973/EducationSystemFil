using EducationSystem.App.Interactor.ModelsInteractors.SсheduleInteractors;
using EducationSystem.Shared.InputData.ScheduleInput;
using EducationSystem.Shared.Models.ScheduleDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.SсheduleControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassScheduleController : Controller
    {
        private ClassScheduleInteractor _interactor;
        public ClassScheduleController(ClassScheduleInteractor personinteractor)
        {
            _interactor = personinteractor;
        }

        //[HttpPut("{buildingObjectId}/documents/{documentId}")]
        [HttpPost("Create")]
        public async Task<Response<ClassScheduleDto>> Insert(ClassScheduleInput newEntity)
        {
            return await _interactor.Insert(newEntity.Number, newEntity.Description, newEntity.ClassId);
        }
        [HttpGet("FindById/{id}")]
        public async Task<Response<ClassScheduleDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpGet("GetByClassId/{classId}")]
        public async Task<Response<ClassScheduleDto>> GetByClassId(int classId)
        {
            return _interactor.GetByClassId(classId);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<ClassScheduleDto>> Update(int id, ClassScheduleInput newData)
        {
            return await _interactor.Update(id, newData.Number, newData.Description, newData.ClassId);
        }
        [HttpDelete("HideOrShow/{id}")]
        public async Task<Response<ClassScheduleDto>> HideOrShow(int id)
        {
            return await _interactor.HideOrShow(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<ClassScheduleDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable/{isHidden}")]
        public Response<IEnumerable<ClassScheduleDto>> GetAllEnumerable(bool isHidden)
        {
            return _interactor.GetAllEnumerable(isHidden);
        }
        [HttpGet("GetPageEnumerable/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<ClassScheduleDto>> GetPageByEnumerable(bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerable(isHidden, start, count);
        }

        //		[HttpGet("GetAllByClassId")]
        //		public Response<IEnumerable<PersonDto>> GetAllByClassId(int classId)
        //		{
        //			return _personinteractor.GetAllByClassId(classId);
        //		}

    }
}
