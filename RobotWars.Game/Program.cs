using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotWars;

namespace RobotWars.Game
{
    class Program
    {
        static void Main(string[] args)
        {
            GameArena gameArena = null;            
            Robot robot;
            List<string> robotOutputs = new List<string>();


            try {
                Console.WriteLine("Enter the size of the Gaming Arena or type 'exit' to stop entering data.");
                while (true)
                {
                    var input = Console.ReadLine();
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            gameArena = new GameArena(input);
                            if (gameArena.ValidArenaSize())
                            {                               
                                break;
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                while (true)
                {                    
                    var inputRobotPosition = Console.ReadLine();                    
                    if (inputRobotPosition.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        try
                        {
                            robot = new Robot(inputRobotPosition, gameArena);
                            var inputMove = Console.ReadLine();
                            robot.Move(inputMove);
                            robotOutputs.Add(robot.FinalLocation);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                }

                Console.WriteLine("Expected output");
                foreach (string rbOutput in robotOutputs)
                {
                    Console.WriteLine(rbOutput);
                }

                Console.ReadLine();
                

            }
            catch (ArgumentException ex)
            {
                ex.Message.ToString();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
    }
}
