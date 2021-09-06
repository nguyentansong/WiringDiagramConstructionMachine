using System;
using System.Threading.Tasks;

namespace WiringDiagramConstructionMachine.IServices
{
    public interface IGetSerialDeviceService
    {
        Task<string> GetSerial();
    }
}
