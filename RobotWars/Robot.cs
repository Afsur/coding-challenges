using System;
using System.Text.RegularExpressions;

namespace RobotWars
{
    public class Robot
    {
   
        private uint robot_XCoordinates;
        private uint robot_YCoordinates;
        private CompassDirection robot_CompassDirection;
        private GameArena gameArena;
        private string userInput;
        

        public Robot()
        {
        }

        public Robot(string userInput, GameArena gameArena1)
        {            
            this.gameArena = gameArena1;
            this.userInput = userInput;
            string[] param = userInput.Split(' ');
            string errorMessage = "";
            bool result = false;
            
            if (param.Length == 3)
            {
                //X Coordinate
                result = uint.TryParse(param[0].ToString(), out uint number);
                if (result)
                {
                    this.robot_XCoordinates = number;
                }
                else 
                {
                    errorMessage = $"Robot's X co-ordinates is not a valid value {param[0]}";
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
                }

                //Y Coordinate
                result = uint.TryParse(param[1].ToString(), out number);
                if (result)
                {
                    this.robot_YCoordinates = number;
                }
                else
                {
                    errorMessage = $"Robot's Y co-ordinates is not a valid value {param[1]}";
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
                }
                //Compass Value needs to be a letter
                result = Regex.IsMatch(param[2], @"^[a-zA-Z]+$") && param[2].Length == 1;
                if (result)
                {
                    CheckForValidLettersPassedIn(param[2], "nNeEsSwW", "Enter the correct letter i.e. 'N','E','S' or 'W'");
                    SetTheCompassDirectionFromPassedInParameter(param[2]);
                }
                else
                {
                    errorMessage = $"Robot's Compass value is not a valid value i.e. not 'N','E','S' or 'W' - {param[1]}.";
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
                }

                //Now check if the Robot is not out of the arena
                if (result)
                {
                    CheckRobotsLocationInGameArena();
                }
            }
            else
            {
                errorMessage = $"Expecting 3 parameters for the Robot.  Please try again with two numbers and either 'N','E','S' or 'W'.  And space between all three.";
                ThrowArgumentExceptionErrorMessageBackToClient(errorMessage,"User Input");                
            }
        }

        private void SetTheCompassDirectionFromPassedInParameter(string compassDirectionFromPassedInParameter)
        {
            switch (compassDirectionFromPassedInParameter)
            {
                case "N":
                    this.robot_CompassDirection = CompassDirection.North;
                    break;
                case "E":
                    this.robot_CompassDirection = CompassDirection.East;
                    break;
                case "S":
                    this.robot_CompassDirection = CompassDirection.South;
                    break;
                case "W":
                    this.robot_CompassDirection = CompassDirection.West;
                    break;
            }
        }

        private void ThrowArgumentExceptionErrorMessageBackToClient(string errorMessage, string paramName)
        {
            throw new ArgumentException(errorMessage,paramName);
        }

        private void CheckRobotsLocationInGameArena()
        {
            string parameterName = "robot_XCoordinate and robot_YCoordinate";
            string errorMessage = "";
            if (this.gameArena.Latitude < this.robot_YCoordinates || this.gameArena.Longlitude < this.robot_XCoordinates)
            {
                errorMessage = $"The position of the robot is outside the game arena.  " +
                    $"You need to enter the location within co-ordinates {this.gameArena.Longlitude} and {this.gameArena.Latitude}";
                ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, parameterName);
            }
        }

        public CompassDirection CompassDirection { get; internal set; }
        public string FinalLocation { get; internal set; }

        public void Move(string movingInstructions)
        {   
            CheckForValidLettersPassedIn(movingInstructions, "lLrRmM", "Enter the correct letters i.e. 'L','R' and 'M' only");

            bool instructionsOutSideGameArena = false;          
            
            //Process each instruction step.
            for (int i = 0; i < movingInstructions.Length; i++)
            {                
                switch (movingInstructions[i].ToString().ToUpper())
                {
                    case "L":
                        switch (this.robot_CompassDirection)
                        {
                            case CompassDirection.North:
                                this.robot_CompassDirection = CompassDirection.West;
                                break;
                            case CompassDirection.West:
                                this.robot_CompassDirection = CompassDirection.South;
                                break;
                            case CompassDirection.South:
                                this.robot_CompassDirection = CompassDirection.East;
                                break;
                            case CompassDirection.East:
                                this.robot_CompassDirection = CompassDirection.North;
                                break;
                        }
                        break;
                    case "R":
                        switch (this.robot_CompassDirection)
                        {
                            case CompassDirection.North:
                                this.robot_CompassDirection = CompassDirection.East;
                                break;
                            case CompassDirection.West:
                                this.robot_CompassDirection = CompassDirection.North;
                                break;
                            case CompassDirection.South:
                                this.robot_CompassDirection = CompassDirection.West;
                                break;
                            case CompassDirection.East:
                                this.robot_CompassDirection = CompassDirection.South;
                                break;
                        }                        
                        break;
                    case "M":
                        switch (this.robot_CompassDirection)
                        {
                            case CompassDirection.North:
                                if (this.robot_YCoordinates + 1 <= this.gameArena.Latitude)
                                {
                                    this.robot_YCoordinates += 1;
                                }
                                else
                                {
                                    instructionsOutSideGameArena = true;
                                }                                
                                break;
                            case CompassDirection.West:
                                if (this.robot_XCoordinates - 1 >= 0)
                                {
                                    this.robot_XCoordinates -= 1;
                                }
                                else
                                {
                                    instructionsOutSideGameArena = true;
                                }
                                break;
                            case CompassDirection.South:
                                if (this.robot_YCoordinates - 1 >= 0)
                                {
                                    this.robot_YCoordinates -= 1;
                                }
                                else
                                {
                                    instructionsOutSideGameArena = true;
                                }
                                break;
                            case CompassDirection.East:
                                if (this.robot_XCoordinates + 1 <= this.gameArena.Longlitude)
                                {
                                    this.robot_XCoordinates += 1;
                                }
                                else
                                {
                                    instructionsOutSideGameArena = true;
                                }
                                break;
                        }
                        break;
                }
                if (instructionsOutSideGameArena)
                {
                    throw new ArgumentException("The instruction takes you outside the Gaming Arena", movingInstructions);
                }
                
                this.FinalLocation = this.robot_XCoordinates + " " + this.robot_YCoordinates + " " + GetCompassName(this.robot_CompassDirection);
            }
        }


        private void CheckForValidLettersPassedIn(string lettersPassedIn, string allowableLetters, string errorMessage)
        {
            foreach (char c in lettersPassedIn)
            {
                if (!allowableLetters.Contains(c.ToString()))
                {                  
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "Letter parameter" );
                }
            }
        }

        private string GetCompassName(CompassDirection robot_CompassDirection)
        {
            string compassName = "";
            switch (this.robot_CompassDirection)
            {
                case CompassDirection.North:
                    compassName = "N";
                    break;
                case CompassDirection.East:
                    compassName = "E";
                    break;
                case CompassDirection.South:
                    compassName = "S";
                    break;
                case CompassDirection.West:
                    compassName = "W";
                    break;
            }
            return compassName;
        }
    }
}