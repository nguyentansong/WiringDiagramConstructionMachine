using System;
using System.Threading.Tasks;

namespace WiringDiagramConstructionMachine.IServices
{
    public interface IPdfService
    {
        Task View(string url, string name);
    }
}
