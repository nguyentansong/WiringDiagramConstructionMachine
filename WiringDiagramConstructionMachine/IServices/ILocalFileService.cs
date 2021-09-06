using System;
using System.IO;
using System.Threading.Tasks;

namespace WiringDiagramConstructionMachine.IServices
{
    public interface ILocalFileService
    {
        Task<string> SaveFileToDisk(Stream stream, string fileName);
    }
}
