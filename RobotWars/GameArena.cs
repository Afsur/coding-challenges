using System;

namespace RobotWars
{
    public class GameArena
    {
        
        const uint minValue = 3; //set a minimum value for both x and y otherwise its a very small game arean.

        public GameArena(string arenaUpperRightCoordinates)
        {            
            string[] param = arenaUpperRightCoordinates.Split(' ');
            bool result;
            string errorMessage;
            if (param.Length == 2)
            {
                result = uint.TryParse(param[0].ToString(), out uint number);
                if (result && number >= minValue)
                {
                    this.Longlitude = number;
                }
                else
                {
                    errorMessage = $"Arena's X co-ordinates is not a valid value. Make sure its greater than or equal to 3 as a minimum - you passed in {param[0]}.";
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
                }
                result = uint.TryParse(param[1].ToString(), out number);
                if (result && number >= minValue)
                {
                    this.Latitude = number;
                }
                else
                {
                    errorMessage = $"Arena's Y co-ordinates is not a valid value. Make sure its greater than or equal to 3 as a minimum - you passed in {param[1]}.";
                    ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
                }
            }
            else
            {
               errorMessage = $"Please provide two positive(and greater then '3') numbers with a space between them for the upper-right coordinates.";
               ThrowArgumentExceptionErrorMessageBackToClient(errorMessage, "User Input");
            }
        }

        private void ThrowArgumentExceptionErrorMessageBackToClient(string errorMessage, string parameterPassedIn)
        {
            throw new ArgumentException(errorMessage, parameterPassedIn);
        }


        public uint Longlitude { get; private set; }
        public uint Latitude { get; private set; }

        public bool ValidArenaSize()
        {
            bool result = false;
            if (this.Longlitude > 0 && this.Latitude > 0)
            {
                result = true;
            }
            return result;
        }
    }
}