using EducationSystem.App.Interactor.FileInteractors;
using EducationSystem.App.Interactor.ModelsInteractors;
using EducationSystem.App.Mappers.FilePathMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.Domain.AuthModels;
using EducationSystem.Domain.Files;
using EducationSystem.Shared.Files;
using EducationSystem.Shared.Models.Helper;
using EducationSystem.Shared.OutputData;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace EducationSystem.Api.Controllers.FilesController
{
    [ApiController]
    [Route("[controller]")]
    public class FileDataController : Controller
    {
        private FileInteractor _interactor;
        private IGenericRepository<FilePath> _genericRepository;
        private IUnitWork unitWork;
        public FileDataController(IGenericRepository<FilePath> genericRepository, IUnitWork unitWork, FileInteractor interactor)
        {
            _genericRepository = genericRepository;
            this.unitWork = unitWork;
            _interactor = interactor;
        }
        //[HttpGet("GetAllById/{id}")]
        //public async Task<Response<IEnumerable<FilePathDto>>> GetAll(int id)
        //{
        //    IEnumerable<FilePath> filePaths = _genericRepository.GetAllEnumerableWithoutLink();
        //    int a = filePaths.Count();
        //    IEnumerable<FilePathDto> outputFilePaths = filePaths.Where(x => x.PersonId == id).Select(t=>t.ToDto());
        //    int b = outputFilePaths.Count();
        //    return new Response<IEnumerable<FilePathDto>>(outputFilePaths);
        //}

        [HttpPost("Send")]
        public async Task<bool> InsertFileTeacher(FileByte file)
        {
            return await _interactor.InsertFileTeacher(file);
        }
        [HttpPost("GetByParams")]
        public async Task<Response<IEnumerable<FilePathDto>>> GetByParams(FileByte file)
        {
           return await _interactor.GetByParams(file);
        }
        //public async Task<Response<IEnumerable<FilePathDto>>> DeleteById(int id)
        //{
        //    return await _interactor.GetByParams(file);
        //}

        [HttpGet("GetByPath/{id}")]
        public async Task<ActionResult> GetByPath(int id)
        {
            string newPath = Directory.GetCurrentDirectory() + $@"\FileStorage\";
            FilePath _path = await _genericRepository.GetByIdAsyncWithoutLink(id);

            using (var nfs = new FileStream(newPath + _path.Path, FileMode.Open))
            {
                var ms = new MemoryStream();
                await nfs.CopyToAsync(ms);
                return File(ms.ToArray(), "application/x-rar-compressed", _path.Name);
            }
        }
    }
}
