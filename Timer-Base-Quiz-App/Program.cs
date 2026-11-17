using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp
{

    class Question
    {
        public string Text { get; set; }
        public string[] Options { get; set; }
        public char CorrectOption { get; set; }
    }

    class Program
    {
        // Using async Task Main so the timer runs smoothly
  
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                var quizData = new Dictionary<string, List<Question>>
            {
                {
                    "1", new List<Question>
                    {
                        new Question
                        {
                            Text = "Which company developed C#?",
                            Options = new[] { "A) Apple", "B) Microsoft", "C) Google", "D) Oracle" },
                            CorrectOption = 'B'
                        },
                        new Question
                        {
                            Text = "Which keyword is used for an integer in C#?",
                            Options = new[] { "A) num", "B) integer", "C) int", "D) number" },
                            CorrectOption = 'C'
                        },
                        new Question
                        {
                            Text = "What is the full form of OOP?",
                            Options = new[] { "A) Object Oriented Programming", "B) Open Online Program", "C) Object Order Process", "D) None" },
                            CorrectOption = 'A'
                        }
                    }
                },
                {
                    "2", new List<Question>
                    {
                        new Question
                        {
                            Text = "What is the capital of Pakistan?",
                            Options = new[] { "A) Karachi", "B) Lahore", "C) Islamabad", "D) Quetta" },
                            CorrectOption = 'C'
                        },
                        new Question
                        {
                            Text = "Which is the largest ocean in the world?",
                            Options = new[] { "A) Pacific", "B) Atlantic", "C) Indian", "D) Arctic" },
                            CorrectOption = 'A'
                        },
                        new Question
                        {
                            Text = "In which direction does the sun rise?",
                            Options = new[] { "A) West", "B) North", "C) South", "D) East" },
                            CorrectOption = 'D'
                        }
                    }
                }
            };

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("       TIMER-BASED QUIZ APP");
            Console.WriteLine("====================================");
            Console.ResetColor();

            Console.WriteLine("Select a Category:\n1. C# Programming\n2. General Knowledge");
            Console.Write("\nEnter your option (1 or 2): ");
            string choice = Console.ReadLine();

            if (!quizData.ContainsKey(choice))
            {
                Console.WriteLine("Invalid option! Closing the app.");
                return;
            }

            List<Question> selectedQuestions = quizData[choice];
            int score = 0;

            for (int i = 0; i < selectedQuestions.Count; i++)
            {
                Console.Clear();
                Question q = selectedQuestions[i];

                Console.WriteLine($"Question {i + 1} / {selectedQuestions.Count}");
                Console.WriteLine("------------------------------------");
                Console.WriteLine(q.Text + "\n");

                foreach (var opt in q.Options)
                {
                    Console.WriteLine(opt);
                }
                Console.WriteLine("------------------------------------");

                // Start Timer Logic (10 Seconds)
                int timeLeft = 10;
                char userAnswer = '\0'; // Empty character

                while (timeLeft > 0)
                {
                    // Keep the cursor on the same line to update the timer
                    Console.SetCursorPosition(0, 10);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"Time remaining: {timeLeft:D2} seconds | Press your answer (A/B/C/D): ");
                    Console.ResetColor();

                    // Check every 1 second in 10 intervals (100ms) for instant key detection
                    for (int ms = 0; ms < 10; ms++)
                    {
                        if (Console.KeyAvailable)
                        {
                            userAnswer = char.ToUpper(Console.ReadKey(true).KeyChar);
                            break;
                        }
                        await Task.Delay(100);
                    }

                    if (userAnswer != '\0') break; // Stop the loop if the user pressed a key
                    timeLeft--;
                }

                Console.WriteLine("\n");

                // Check Answer
                if (userAnswer == '\0')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"TIME'S UP! The correct answer was {q.CorrectOption}.");
                }
                else if (userAnswer == q.CorrectOption)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"AWESOME! {userAnswer} is the correct answer.");
                    score++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"WRONG ANSWER! You pressed {userAnswer}, the correct answer was {q.CorrectOption}.");
                }

                Console.ResetColor();
                Console.WriteLine("\nPlease wait 2 seconds for the next question...");
                await Task.Delay(2000);
            }

            // Step 4: Final Result Card
            Console.Clear();
            double percentage = ((double)score / selectedQuestions.Count) * 100;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("         FINAL RESULT CARD");
            Console.WriteLine("====================================");
            Console.ResetColor();

            Console.WriteLine($"Total Questions : {selectedQuestions.Count}");
            Console.WriteLine($"Correct Answers : {score}");
            Console.WriteLine($"Wrong Answers   : {selectedQuestions.Count - score}");
            Console.WriteLine($"Percentage      : {percentage:F1}%");
            Console.WriteLine("====================================");

            if (percentage >= 70)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Result: PASS! Excellent work.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Result: FAIL! Please try again.");
            }
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Press Key 'R' go to view new window...");
                Console.WriteLine("Press any other key to exit....");

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.R)
                {
                    break;
                           
                }
            Console.ResetColor();
            Console.ReadKey();
        }
    }
}
}
