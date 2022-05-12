using Business;
using Business.Services;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTests
{
    public class ElevatorServiceTests
    {
        ElevatorServices elevatorServices = new ElevatorServices();
        ElevatorManager elevatorManager = new ElevatorManager();

        [Fact]
        public void TestNearestElevatorCall()
        {
            List<Elevator> elevatorsList = new List<Elevator>{
                new Elevator(1, 10),
                new Elevator(2, 3),
                new Elevator(3, 8)
            };
            elevatorsList[0].IsBusy = true;
            elevatorsList[1].IsBusy = true;
            Building building = new Building(12, elevatorsList);
            int elevatorExpectedElevatorId = 3;
            Elevator elevatorComing = elevatorManager.GetClosesedElevator(building, 4);

            Assert.Equal(elevatorExpectedElevatorId, elevatorComing.Id);
        }
        [Fact]
        public void ElevatorCall_ElevatorComeToMyFloorUp()
        {
            List<Elevator> elevatorsList = new List<Elevator>{
                new Elevator(1, 1)
            };
            Building building = new Building(12, elevatorsList);
            elevatorManager.ElevatorCall(building, 4, 1);
            int elevatorExpectedFloor = 4;

            Assert.Equal(elevatorExpectedFloor, elevatorsList[0].CurrentFloor);
        }
        [Fact]
        public void ElevatorCall_ElevatorComeToMyFloorDown()
        {
            List<Elevator> elevatorsList = new List<Elevator>{
                new Elevator(1, 12)
            };
            Building building = new Building(12, elevatorsList);
            elevatorManager.ElevatorCall(building, 4, 1);
            int elevatorExpectedFloor = 4;

            Assert.Equal(elevatorExpectedFloor, elevatorsList[0].CurrentFloor);
        }
        [Fact]
        public void ElevatorStoped_ElevatorStop()
        {
            Elevator elevator = new Elevator(1, 10);
            elevatorServices.Stop(10, ref elevator);
 
            Assert.Equal(ElevatorStatus.Stoped, elevator.Status);
        }
        [Fact]
        public void ElevatorMovesUp_ElevatorFloorIsEqualToCallFloor()
        {
            Elevator elevator = new Elevator(1, 1);
            elevatorServices.MoveUp(3, ref elevator);
            int expectedFloor = 3;
            Assert.Equal(expectedFloor, elevator.CurrentFloor);

        }
        [Fact]
        public void ElevatorMovesDown_ElevatorFloorIsEqualToCallFloor()
        {
            Elevator elevator = new Elevator(1, 10);
            elevatorServices.MoveDown(9, ref elevator);
            int expectedFloor = 9;
            Assert.Equal(expectedFloor, elevator.CurrentFloor);

        }
        [Fact]
        public void ElevatorDoorOpen_ElevatorDoorIsOpened()
        {
            Elevator elevator = new Elevator(1, 5);
            elevatorServices.OpenDoor(ref elevator);

            Assert.Equal(DoorStatus.Open, elevator.ElevatorDoorStatus);
        }
        [Fact]
        public void ElevatorDoorClose_ElevatorDoorIsClosed()
        {
            Elevator elevator = new Elevator(1, 5);
            elevatorServices.CloseDoor(ref elevator);

            Assert.Equal(DoorStatus.Closed, elevator.ElevatorDoorStatus);
        }
    }
}
