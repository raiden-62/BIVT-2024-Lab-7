﻿using System;
using System.Linq;


namespace Lab_7
{
    public class Purple_2 //1) Hell knows how you should calculate total score 2) No test data to figure out either
    {
        public struct Participant
        {
            private string _name;
            private string _surname;
            private int _distance;
            private int[] _marks;

            private bool _hasJumpedStandard;


            public string Name => _name;
            public string Surname => _surname;
            public int Distance => _distance;
            public int[] Marks
            {
                get
                {
                    if (_marks == null) return default(int[]);

                    var newArray = new int[_marks.Length];
                    Array.Copy(_marks, newArray, _marks.Length);
                    return newArray;
                }
            }

            public int Result
            {
                get
                {
                    if (_marks == null) return 0;
                    int sum = _marks.Sum() - _marks.Max() - _marks.Min(); ;
                    sum += 60 + (_distance - 120) * 2 > 0 ? 60 + (_distance - 120) * 2 : 0;
                    if (_hasJumpedStandard) sum += 60;
                    return sum;
                }
            }

            public Participant(string name, string surname)
            {
                _name = name;
                _surname = surname;
                _distance = 0;
                _marks = new int[5];
                _hasJumpedStandard = false;
            }

            public void Jump(int distance, int[] marks, int target)
            {
                if (_distance != 0 || marks == null || _marks == null || marks.Length != 5) return; //needed or not?
                _distance = distance;
                if (distance > target) _hasJumpedStandard = true;
                Array.Copy(marks, _marks, marks.Length);
            }

            public static void Sort(Participant[] array)
            {
                if (array == null) return;
                for (int i = 0; i < array.Length; i++)
                {
                    Participant key = array[i];
                    int j = i - 1;

                    while (j >= 0 && array[j].Result < key.Result)
                    {
                        array[j + 1] = array[j];
                        j = j - 1;
                    }
                    array[j + 1] = key;
                }
            }

            public void Print()
            {
                Console.WriteLine(_name + " " + _surname);
                Console.WriteLine(Result);
                Console.WriteLine($"Distance: {_distance}");
                Console.Write("Marks: ");
                foreach (double var in _marks)
                {
                    Console.Write(var + "  ");
                }
                Console.WriteLine();
            }
        }

        public abstract class SkiJumping
        {
            private string _name;
            private int _standard;
            private Participant[] _participants;

            public string Name => _name;
            public int Standard => _standard;
            public Participant[] Participants => _participants;

            public SkiJumping(string name, int standard)
            {
                _name = name;
                _standard = standard;
                _participants = new Participant[0];
            }

            public void Add(Participant jumper)
            {
                Array.Resize(ref _participants, _participants.Length + 1);
                _participants[_participants.Length - 1] = jumper;
            }
            public void Add(Participant[] jumpers)
            {
                foreach (var jumper in jumpers) Add(jumper);
            }

            public void Jump(int distance, int[] marks)
            {
                foreach (var jumper in _participants)
                {
                    if (jumper.Distance == 0)
                    {
                        jumper.Jump(distance, marks, Standard);
                        break;
                    }
                }
            }

            public void Print()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Standard);
                foreach (var participant in _participants) participant.Print();
            }
        }

        public class JuniorSkiJumping : SkiJumping
        {
            public JuniorSkiJumping() : base("100m", 100) { }
        }

        public class ProSkiJumping : SkiJumping
        {
            public ProSkiJumping() : base("150", 150) { }
        }
    }
}


