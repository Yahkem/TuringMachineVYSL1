using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineLol
{
    interface ITuringMachine
    {
        IEnumerable<IState> States { get; set; }
        IEnumerable<ITransition> Transitions { get; set; }
        IState InputState { get; set; }
        IEnumerable<IState> OutputStates { get; set; }
        IEnumerable<char> Alphabet { get; set; }
        char EmptyChar { get; set; }

        bool Test(string str);
    }
}
