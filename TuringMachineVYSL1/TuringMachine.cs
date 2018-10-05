using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineVYSL1
{
    class TuringMachine
    {
        private HashSet<State> _states;
        public HashSet<State> States
        { 
            get
            {
                return _states;
            }
            set
            {
                foreach (State item in value)
                {
                    if (item.IsInput)
                    {
                        item.IsActive = true;
                        InputState = item;
                    }
                    if (item.IsOutput)
                    {
                        OutputStates.Add(item);
                    }
                };

                this._states = value;
            }
        }

        public State InputState { get; set; }
        public HashSet<State> OutputStates { get; set; } = new HashSet<State>();
        public HashSet<char> Alphabet { get; set; }
        public char EmptyChar { get; set; }

        public TuringMachine(IEnumerable<char> alphabetLetters, char emptyChar = '#')
        {
            Alphabet = new HashSet<char>(alphabetLetters);
            this.EmptyChar = emptyChar;
            Alphabet.Add(emptyChar);
        }

        public (State, string) Run(string str)
        {
            string tape = $"{EmptyChar}{str}{EmptyChar}";

            State currentState = (State)InputState;
            currentState.IsActive = true;
            int idx = 1;

            while (!currentState.IsOutput)
            {
                char curChar = tape[idx];

                if (!Alphabet.Contains(curChar))
                    throw new InvalidOperationException("alphabet doesn't contain this character");

                var transition = currentState.GetTransition(curChar);

                // zapis znak
                tape = $"{tape.Substring(0, idx)}{transition.LetterToWrite}{tape.Substring(idx + 1)}";

                currentState.IsActive = false;
                currentState = (State)transition.EndState;
                currentState.IsActive = true;

                idx = transition.DirectionToGo == Direction.Left ? --idx :
                    transition.DirectionToGo == Direction.Right ? ++idx : idx;

                if (idx < 0)
                {
                    idx = 0;
                    // TODO?
                    tape = EmptyChar + tape;
                }
                else if (idx >= tape.Length)
                {
                    idx = tape.Length;
                    tape += EmptyChar;
                }
            }

            return (currentState, tape);
        }
    }
}
