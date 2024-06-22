using EducationSystem.App.Interactor.ModelsInteractors.ClassInteractors;
using EducationSystem.Shared.InputData.ClassInput;
using EducationSystem.Shared.Models.ClassDto;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;

namespace EducationSystem.Api.Controllers.ModelsControllers.ClassControllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : Controller
    {
        private ItemInteractor _interactor;
        public ItemController(ItemInteractor interactor)
        {
            _interactor = interactor;
        }
        [HttpPost("Create")]
        public async Task<Response<ItemDto>> Insert(ItemInput newEntity)
        {
            return await _interactor.Insert(newEntity.Name,newEntity.Description);
        }
        [HttpGet("FindById/{id}")]
        public async Task<Response<ItemDto>> Find(int id)
        {
            return await _interactor.GetByIdAsync(id);
        }
        [HttpPut("Update/{id}")]
        public async Task<Response<ItemDto>> Update(int id, ItemInput newData)
        {
            return await _interactor.Update(id, newData.Name, newData.Description);
        }
        [HttpDelete("HideOrShow/{id}")]
        public async Task<Response<ItemDto>> HideOrShow(int id)
        {
            return await _interactor.HideOrShow(id);
        }
        [HttpDelete("Delete/{id}")]
        public async Task<Response<ItemDto>> Delete(int id)
        {
            return await _interactor.Delete(id);
        }
        [HttpGet("GetAllEnumerable/{isHidden}")]
        public Response<IEnumerable<ItemDto>> GetAllEnumerable(bool isHidden)
        {
            return _interactor.GetAllEnumerable(isHidden);
        }
        [HttpGet("GetPageEnumerable/{isHidden}/{start}/{count}")]
        public Response<IEnumerable<ItemDto>> GetPageByEnumerable(bool isHidden, int start, int count)
        {
            return _interactor.GetPageEnumerable(isHidden, start, count);
        }
    }
}
