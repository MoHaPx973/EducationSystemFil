using EducationSystem.App.Mappers.FilePathMappers;
using EducationSystem.App.Storage.GenericInterfaces;
using EducationSystem.App.Storage.ModelsInterfaces;
using EducationSystem.Domain.Files;
using EducationSystem.Domain.Models;
using EducationSystem.Domain.Role;
using EducationSystem.Shared.Files;
using EducationSystem.Shared.OutputData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationSystem.App.Interactor.FileInteractors
{
    public class FileInteractor
    {
        private IGenericRepository<FilePath> _genericRepository;
        private IUnitWork unitWork;

        public FileInteractor(IGenericRepository<FilePath> genericRepository, IUnitWork unitWork)
        {
            _genericRepository = genericRepository;
            this.unitWork = unitWork;
        }

        public async Task<bool> InsertFileTeacher(FileByte file)
        {
            try
            {
                FilePath _path = new FilePath();
                _path.Path = $@"{file.Path}\{file.PersonId}{file.TypeId}{file.ItemNumber}{file.ClassNumber}{file.Name}";
                _path.PersonId = file.PersonId;
                _path.Name = file.Name;
                _path.ItemNumber = file.ItemNumber;
                _path.ClassNumber = file.ClassNumber;
                _path.TypeId = file.TypeId;

                _genericRepository.Insert(_path);
                await unitWork.Commit();
                // save

                string newPath = Directory.GetCurrentDirectory() + @"\FileStorage\"; // можно в статик


                string pathOfPain = newPath + _path.Path;

                using (var nfs = new FileStream(pathOfPain, FileMode.Create))
                {
                    await (new MemoryStream(file.Data)).CopyToAsync(nfs);
                }
                return true;
            }
            catch
            {
                return false;
            }
           
        }
        public async Task<Response<IEnumerable<FilePathDto>>> GetByParams(FileByte file)
        {
            IEnumerable<FilePath> filePaths = _genericRepository.GetAllEnumerableWithoutLink();
            IEnumerable<FilePathDto> outputFilePaths = filePaths.Where(x => x.PersonId == file.PersonId).
                Where(x => x.TypeId == file.TypeId).Where(x => x.ClassNumber == file.ClassNumber)
                .Where(x => x.ItemNumber == file.ItemNumber).Select(t => t.ToDto());
            return new Response<IEnumerable<FilePathDto>>(outputFilePaths);
        }
        //public async Task<Response<bool>> GetByParams(FileByte file)
        //{
        //    IEnumerable<FilePath> filePaths = _genericRepository.GetAllEnumerableWithoutLink();
        //    IEnumerable<FilePathDto> outputFilePaths = filePaths.Where(x => x.PersonId == file.PersonId).
        //        Where(x => x.TypeId == file.TypeId).Where(x => x.ClassNumber == file.ClassNumber)
        //        .Where(x => x.ItemNumber == file.ItemNumber).Select(t => t.ToDto());
        //    return new Response<IEnumerable<FilePathDto>>(outputFilePaths);
        //}

    }
}
