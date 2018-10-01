using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineLol
{
    interface ITransition
    {
        IState BeginState { get; set; }
        IState EndState { get; set; }
        char LetterToRead { get; set; }
        char LetterToWrite { get; set; }
        Direction DirectionToGo { get; set; }
    }
}
