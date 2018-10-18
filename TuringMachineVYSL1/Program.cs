using System;
using System.Collections.Generic;

namespace TuringMachineVYSL1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console settings
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            ConsoleColor origForeColor = Console.ForegroundColor;

            // Turing Machine settings
            Direction 
                l = Direction.Left,
                r = Direction.Right,
                s = Direction.None;
            const char emptyChar = '#',
                a = 'a', b = 'b', w = 'λ';
            char[] alphabet = new char[] { a, b, w };
            TuringMachine machine = new TuringMachine(alphabet, emptyChar);
            
            while (true)
            {
                // Input
                Console.Write("Write a word: ");
                string word = Console.ReadLine();

                // State setting
                State q0 = new State(0, isInput: true),
                    q1 = new State(1), q2 = new State(2),
                    q3 = new State(3), q4 = new State(4),
                    q5 = new State(5, isOutput: true);

                q0.SetOutputTransitions(
                    new Transition(a, w, r, q1),
                    new Transition(b, w, r, q4)
                );
                q1.SetOutputTransitions(
                    new Transition(a, a, r, q1),
                    new Transition(b, w, l, q2),
                    new Transition(w, w, r, q1)
                );
                q2.SetOutputTransitions(
                   new Transition(a, a, l, q2),
                   new Transition(b, b, l, q2),
                   new Transition(w, w, l, q2),
                   new Transition(emptyChar, emptyChar, r, q3)
                );
                q3.SetOutputTransitions(
                    new Transition(w, w, r, q3),
                    new Transition(a, a, s, q0),
                    new Transition(b, b, s, q0),
                    new Transition(emptyChar, emptyChar, s, q5)
                );
                q4.SetOutputTransitions(
                    new Transition(b, b, r, q4),
                    new Transition(a, w, l, q2),
                    new Transition(w, w, r, q4)
                );

                // Put states here
                machine.States = new HashSet<State>() { q0, q1, q2, q3, q4, q5 };

                // results
                (State finalState, string resultOnTape) = machine.Run(word);

                Console.Write("Is the final state output: ");
                
                Console.ForegroundColor = finalState.IsOutput ? ConsoleColor.Green : ConsoleColor.Red;
                Console.WriteLine(finalState.IsOutput ? "Yes" : "No");
                Console.ForegroundColor = origForeColor;
                
                Console.Write("Result on tape: ");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(resultOnTape);
                Console.ForegroundColor = origForeColor;

                Console.WriteLine($"Encoded Turing machine: {machine.GetEncoded()}");

                Console.Write("Again (y/n)? ");
                if (char.ToLower(Console.ReadKey().KeyChar) == 'n')
                    break;

                Console.WriteLine("\n=====\n=====");
            }
        }
    }
}
