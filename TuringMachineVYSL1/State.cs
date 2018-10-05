using System;
using System.Collections.Generic;
using System.Text;

namespace TuringMachineVYSL1
{
    class State
    {
        public uint Id { get; set; }
        public bool IsActive { get; set; } = false;
        public bool IsOutput { get; set; }
        public bool IsInput { get; set; }
        public HashSet<Transition> OutputTransitions { get; set; } = new HashSet<Transition>();
        public HashSet<Transition> InputTransitions { get; set; } = new HashSet<Transition>();

        public State(uint id, bool isOutput = false, bool isInput = false)
        {
            this.Id = id;
            this.IsOutput = isOutput;
            this.IsInput = isInput;
        }

        public void SetInputTransitions(params Transition[] transitions)
        {
            foreach (var item in transitions)
            {
                item.EndState = this;
            }

            this.InputTransitions = new HashSet<Transition>(transitions);
        }

        public void SetOutputTransitions(params Transition[] transitions)
        {
            foreach (var item in transitions)
            {
                item.BeginState = this;
            }

            this.OutputTransitions = new HashSet<Transition>(transitions);
        }

        public Transition GetTransition(char c)
        {
            if (IsActive)
            {
                foreach (var transition in OutputTransitions)
                {
                    if (transition.LetterToRead == c)
                        return transition;
                }
            }

            return null;
        }


    }
}
