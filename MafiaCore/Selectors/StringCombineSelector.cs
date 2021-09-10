using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MafiaCore.Selectors
{
    public class StringCombineSelector : ISelector<string>
    {
        public ISelector<string> LeftOperandSelector;
        public ISelector<string> RightOperandSelector;
        public string Spacer = "";

        public string Select(ExecutionParams executionContext)
        {
            string left = LeftOperandSelector.Select(executionContext);
            string right = RightOperandSelector.Select(executionContext);

            return left + Spacer + right;
        }
    }
}
