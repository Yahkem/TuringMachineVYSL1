﻿using System;
using System.Collections.Generic;

namespace TuringMachineVYSL1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Napiste slovo: ");
            string word = Console.ReadLine(); // momomomomomomo

            Console.Write("Napiste znaky abecedy (jako retezec - mezera se bere jako normalni znak): ");
            string strAlphabet = Console.ReadLine(); // mo10x
            char[] alphabet = strAlphabet.ToCharArray();

            Console.Write("Napiste znak vyjadrujici 'blank' (normalne '#'): ");
            char emptyChar = Console.ReadKey().KeyChar;
            Console.WriteLine(); Console.WriteLine();

            State q0 = new State(0, isInput: true),
                q1 = new State(1), q2 = new State(2),
                q3 = new State(3), q4 = new State(4),
                q5 = new State(5), q6 = new State(6, isOutput: true);

            Direction l = Direction.Left,
                r = Direction.Right,
                s = Direction.None;

            q0.SetOutputTransitions(
                new Transition('m', 'x', l, q1),
                new Transition('o', 'x', l, q1)
            );
            q1.SetOutputTransitions(
                new Transition(emptyChar, emptyChar, l, q2),
                new Transition('x', 'x', l, q1)
            );
            q2.SetOutputTransitions(
               new Transition(emptyChar, '1', r, q3),
               new Transition('1', '0', l, q4),
               new Transition('0', '1', r, q3)
            );
            q4.SetOutputTransitions(
                new Transition('0', '1', s, q3),
                new Transition('1', '0', l, q4),
                new Transition(emptyChar, '1', r, q3)
            );
            q3.SetOutputTransitions(
                new Transition('1', '1', r, q3),
                new Transition('0', '0', r, q3),
                new Transition(emptyChar, emptyChar, r, q5)
            );
            q5.SetOutputTransitions(
                new Transition('x', 'x', r, q5),
                new Transition('o', 'x', l, q1),
                new Transition('m', 'x', l, q1),
                new Transition(emptyChar, emptyChar, s, q6)
            );

            TuringMachine machine = new TuringMachine(alphabet, emptyChar);

            machine.States = new HashSet<State>() { q0,q1,q2,q3,q4,q5,q6 };

            (State finalState, string resultOnTape) = machine.Run(word);
            Console.WriteLine($"Is the final state output? {(finalState.IsOutput ? "Yes":"No")}\nTape result={resultOnTape}");
            
            Console.ReadKey();
        }
    }
}
