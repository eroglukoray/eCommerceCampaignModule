using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness.Service.Command
{
    public interface ICommandService
    {
        void Execute(string command, string[] arguments);
        TimeSpan GetTime();
        void ReadCommandFileAndExecute(string scenario);
    }
}
