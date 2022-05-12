
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.Repository
{
    public class RepositoryTests
    {
        [Fact]
        public void CreateBuilding_BuildingIsCreated()
        {
            int floors = 10;
            List<Elevator> elevatorsList = new List<Elevator>
            {
                new Elevator(1, 10),
                new Elevator(2, 8),
                new Elevator(3, 1),
            };
            Building building = new Building(floors, elevatorsList);

            Assert.Equal(building.Floors, floors);
            Assert.Equal(building.Elevators.Count, 3);
        }
        [Fact]
        public void CreateElevator_validData_ElevatorIsCreated()
        {
            int id = 1;
            int currentFloor = 1;
            Elevator elevator = new Elevator(id, currentFloor);

            Assert.Equal(elevator.Id, id);
            Assert.Equal(elevator.CurrentFloor, currentFloor);
        }
        [Fact]
        public void GenerateBuilding_InvalidCountOfFloors_ExceptionThrowed()
        {
            bool exceptionThrowed = false;

            try
            {
                BuildingRepository buildingRepository = new BuildingRepository(1, 2);
            }
            catch
            {
                exceptionThrowed = true;
            }
            Assert.True(exceptionThrowed);
        }
        [Fact]
        public void GenerateBuilding_ValidData_BuildingIsGenerated()
        {
            int floors = 8;
            int countOfElevators = 3;
            BuildingRepository buildingRepository = new BuildingRepository(floors, countOfElevators);

            Assert.Equal(buildingRepository.Building.Floors, floors);
            Assert.Equal(buildingRepository.Elevators.Count, countOfElevators);
        }
    }
}
