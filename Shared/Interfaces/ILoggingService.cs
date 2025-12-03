using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Interfaces
{
    public interface ILoggingService
    {


        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(Exception ex, string message);

    }
}
