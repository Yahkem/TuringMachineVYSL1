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

            State currentState = InputState;
            currentState.IsActive = true;
            int idx = 1;

            while (!currentState.IsOutput)
            {
                char curChar = tape[idx];

                if (!Alphabet.Contains(curChar))
                    throw new InvalidOperationException("Alphabet doesn't contain this character");

                var transition = currentState.GetTransition(curChar);

                if (transition == null)
                {
                    Console.WriteLine($"There is no transition for char '{curChar}' in state #{currentState.Id}");
                    break;
                }
                // zapis znak
                tape = $"{tape.Substring(0, idx)}{transition.LetterToWrite}{tape.Substring(idx + 1)}";

                currentState.IsActive = false;
                currentState = transition.EndState;
                currentState.IsActive = true;

                idx = transition.DirectionToGo == Direction.Left ? --idx :
                    transition.DirectionToGo == Direction.Right ? ++idx : idx;

                if (idx < 0) // extend tape
                {
                    idx = 0;
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

        public string GetEncoded()
        {
            if (States == null || States.Count == 0)
                return string.Empty;

            string result = "111";
            var alphabetList = new List<char>(Alphabet);
            
            foreach (var state in States)
            {
                foreach (Transition t in state.OutputTransitions)
                {
                    // id+1, beginning is 0
                    result += new string('0', (int)t.BeginState.Id + 1) + "1";

                    int indexOfLetter = alphabetList.IndexOf(t.LetterToRead);
                    result += new string('0', indexOfLetter + 1) + "1";
                    
                    result += new string('0', (int)t.EndState.Id + 1) + "1";
                    
                    indexOfLetter = alphabetList.IndexOf(t.LetterToWrite);
                    result += new string('0', indexOfLetter + 1) + "1";
                    
                    result += (t.DirectionToGo == Direction.Left ? "0" :
                        t.DirectionToGo == Direction.Right ? "00" : "000") + "11";
                }
            }
            
            return result + "1";
        }
    }
}
