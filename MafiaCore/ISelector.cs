using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore
{
    public interface ISelector<T>
    {
        T Select(ExecutionParams executionContext);
    }
}
