using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineLol
{
    interface IState
    {
        uint Id { get; set; }
        bool IsActive { get; set; }
        bool IsOutput { get; set; }
        bool IsInput { get; set; }
        IEnumerable<ITransition> OutputTransitions { get; set; }
        IEnumerable<ITransition> InputTransitions { get;set; }
    }
}
