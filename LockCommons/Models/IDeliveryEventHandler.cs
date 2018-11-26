using System.Threading.Tasks;

namespace LockCommons.Models
{
    public interface IEventHandler
    {
        event ProcessEventDelegate LockEventProcessHandler;

        Task<bool> ProcessEvent(byte[] data);
    }
}