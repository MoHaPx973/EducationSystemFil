using EducationSystem.App.Interactor.RelationshipsInteractors;
using EducationSystem.Shared.InputData.RelationshipsInput;
using EducationSystem.Shared.OutputData;
using EducationSystem.Shared.Relationships;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.RelationshipsControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemInCurriculumController : Controller
    {
        private ItemInCurriculumInteractor _interactor;

        public ItemInCurriculumController(ItemInCurriculumInteractor interactor)
        {
            _interactor = interactor;
        }

        [HttpPost("Create")]
        public async Task<Response<ItemInCurriculumDto>> Insert(ItemInCurriculumInput newEntity)
        {
            return await _interactor.Insert(newEntity.ItemId, newEntity.CurriculumId,newEntity.NumberOfHours);
        }
        [HttpGet("GetAllEnumerable")]
        public Response<IEnumerable<ItemInCurriculumDto>> GetAllEnumerableAsync()
        {
            return _interactor.GetAllEnumerable();
        }
        [HttpGet("GetByCurriculumIdAsync/{curriculumId}")]
        public async Task<Response<IEnumerable<ItemInCurriculumDto>>> GetByCurriculumIdAsync(int curriculumId)
        {
            return await _interactor.GetByCurriculumIdAsync(curriculumId);
        }

        [HttpDelete("Delete/{itemId}/{curriculumId}")]
        public async Task<Response<ItemInCurriculumDto>> Delete(int itemId, int curriculumId)
        {
            return await _interactor.Delete(itemId, curriculumId);
        }
        [HttpPut("Update")]
        public async Task<Response<ItemInCurriculumDto>> Update(ItemInCurriculumInput newEntity)
        {
            return await _interactor.Update(newEntity.ItemId, newEntity.CurriculumId, newEntity.NumberOfHours);
        }
    }
}
