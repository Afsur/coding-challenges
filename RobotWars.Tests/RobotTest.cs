using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace RobotWars
{
    [TestClass]
    public class RobotTest
    {        
        readonly GameArena gameArena = new GameArena("5 5");

        [TestMethod]
        public void CreateRobot_WithStartingCoordinateParameterWithNoSpace()
        {
            string userInput = "12N";
            try
            {
                Robot rb = new Robot(userInput, gameArena);
            }
            catch (ArgumentException)
            {
                //Success we are expecting an ArgumentException
                return;
            }
            Assert.Fail("Creating a Robot did NOT throw an ArgumentException with bad userInput parameter");
        }

        [TestMethod]
        public void CreateRobot_WithStartingCoordinateParameterWithSpaceButBadValuesVersion1()
        {
            string userInput = "z 12 llN";
            try
            {
                Robot rb = new Robot(userInput, gameArena);
            }
            catch (ArgumentException)
            {
                //Success we are expecting an ArgumentException
                return;
            }
            Assert.Fail("Creating a Robot did NOT throw an ArgumentException with bad userInput parameter");
        }

        [TestMethod]
        public void CreateRobot_WithStartingCoordinateParameterWithSpaceButBadValuesVersion2()
        {
            string userInput = "1 12 llN";
            try
            {
                Robot rb = new Robot(userInput, gameArena);
            }
            catch (ArgumentException)
            {
                //Success we are expecting an ArgumentException
                return;
            }
            Assert.Fail("Creating a Robot did NOT throw an ArgumentException with bad userInput parameter");
        }

        [TestMethod]
        public void CreateRobot_WithStartingCoordinateParameterWithSpaceButCheckingIfRobotIsNotOutOfArena()
        {
            string userInput = "1 12 N";
            try
            {
                Robot rb = new Robot(userInput, gameArena);
            }
            catch (ArgumentException)
            {
                //Success we are expecting an ArgumentException
                return;
            }
            Assert.Fail("Creating a Robot did NOT throw an ArgumentException with bad userInput parameter");
        }


        [TestMethod]
        public void CreateRobot_WithGoodDataTest()
        {
            //Arrange            
            bool isAreanaValidSize = gameArena.ValidArenaSize();            
            string expectedRobotFinalLocation = "1 3 N";

            //Act
            Robot rb = null;
            string RobotFinalLocation = "";

            if (isAreanaValidSize)
            {
                rb = new Robot("1 2 N",gameArena);
                rb.Move("LMLMLMLMM");
                RobotFinalLocation = rb.FinalLocation;
            }

            //Assert
            Assert.IsTrue(isAreanaValidSize);
            Assert.IsNotNull(rb, "Robot is null");
            Assert.AreEqual(expectedRobotFinalLocation, RobotFinalLocation);
        }

        //[TestMethod]        
        //public void CreateRobot_WithBadXandYCoordinatesTest()
        //{
        //    //Arrange            
        //    bool isAreanaValidSize = gameArena.ValidArenaSize();

        //    //Act
        //    try {
        //        if (isAreanaValidSize)
        //        {
        //            uint Robot_XCoordinates = gameArena.Longlitude + 1;
        //            uint Robot_YCoordinates = gameArena.Latitude + 1;
        //            CompassDirection Robot_CompassDirection = CompassDirection.North;
        //            Robot rb = new Robot(Robot_XCoordinates, Robot_YCoordinates, Robot_CompassDirection, gameArena);
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        //Test was a success
        //        return;
        //    }
        //    Assert.Fail("Creating the Robot with bad coordinates did NOT throw an ArgumentException");
        //}

        //[TestMethod]        
        //public void CreateRobot_WithBadMovingInstructionsTest()
        //{
        //    //Arrange            
        //    bool isAreanaValidSize = gameArena.ValidArenaSize();

        //    //Act
        //    try
        //    {
        //        if (isAreanaValidSize)
        //        {
        //            uint Robot_XCoordinates = 1;
        //            uint Robot_YCoordinates = 2;
        //            CompassDirection Robot_CompassDirection = CompassDirection.North;
        //            Robot rb = new Robot(Robot_XCoordinates, Robot_YCoordinates, Robot_CompassDirection, gameArena);
        //            rb.Move("LMLM___*LMLMM");
        //        }
        //    }
        //    catch (ArgumentException)
        //    {
        //        //Test was a success
        //        return;
        //    }
        //    Assert.Fail("Creating the Robot with bad moving instructions did NOT throw an ArgumentException");
        //}



    }
}
