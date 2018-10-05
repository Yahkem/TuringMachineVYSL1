using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineVYSL1
{
    class Transition
    {
        private State _beginState;
        public State BeginState {
            get { return _beginState; }
            set
            {
                if (value.OutputTransitions == null)
                    value.OutputTransitions = new HashSet<Transition>(new Transition[] { this });
                else
                    value.OutputTransitions.Add(this);

                _beginState = value;
            }
        }
        public State EndState { get; set; }
        public char LetterToRead { get; set; }
        public char LetterToWrite { get; set; }
        public Direction DirectionToGo { get; set; }

        public Transition(char toRead, char toWrite, Direction toGo, State endState)
        {
            this.LetterToRead = toRead;
            this.LetterToWrite = toWrite;
            this.DirectionToGo = toGo;
            this.EndState = endState;
            endState.InputTransitions.Add(this);
        }
    }
}
