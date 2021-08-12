using Process_Pension.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Process_Pension.Interfaces
{
    public interface IProcessPensionInput
    {
        Task<int> ProcessPensionInput(ProcessPensionInput pensionInput);
    }
}
