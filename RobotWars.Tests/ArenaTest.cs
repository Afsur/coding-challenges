using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotWars;
using System;

namespace RobotWars
{
    [TestClass]
    public class ArenaTest
    {
        [TestMethod]
        public void CreateGameArena_WithUpperRightCoordinatesAsOneParameterWithNoSpace()
        {
            //Arrange

            //Act
            try
            {
                var arenaUpperRightCoordinates = "55";
                var areana = new GameArena(arenaUpperRightCoordinates);
            }
            catch (ArgumentException)
            {
                //Test succeeded
                return;
            }
            Assert.Fail("Creating the Gaming Arena with bad upper-right coordinates did NOT throw an ArgumentException");
        }

        [TestMethod]
        public void CreateGameArena_WithUpperRightCoordinatesAsTwoParametersWithSpace()
        {
            //Arrange
            uint expectedXCoordinate = 5;
            uint expectedYCoordinate = 6;

            //Act
            var arenaUpperRightCoordinates = "5 6";
            GameArena gameArena = new GameArena(arenaUpperRightCoordinates);
            uint actualXCoordinate = gameArena.Longlitude;
            uint actualYCoordinate = gameArena.Latitude;           

            //Assert
            Assert.IsNotNull(gameArena);
            Assert.AreEqual(expectedYCoordinate, actualYCoordinate);
            Assert.AreEqual(expectedXCoordinate, actualXCoordinate);

        }

        [TestMethod]
        public void CreateGameArena_WithUpperRightCoordinatesAsTwoParametersWithSpaceAndWithZeroCoordinate()
        {
            //Arrange

            //Act
            try
            {
                string arenaUpperRightCoordinates = "6 0";
                GameArena gameArena = new GameArena(arenaUpperRightCoordinates);
            }
            catch (ArgumentException)
            {
                //Success, we are expecting an exception here.
                return;
            }
        }

        [TestMethod]
        public void CreateGameArena_WithUpperRightCoordinatesAsTwoParametersWithSpaceAndWithLessThan3ForEachParameter()
        {
            //Arrange

            //Act
            try
            {
                string arenaUpperRightCoordinates = "2 2";
                GameArena gameArena = new GameArena(arenaUpperRightCoordinates);
            }
            catch (ArgumentException)
            {
                //Success, we are expecting an exception here.
                return;
            }
        }

    }


}
