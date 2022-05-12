using Repository.Models;
using System;
using System.Collections.Generic;

namespace Repository.DataAccess
{
    public class BuildingRepository
    {
        public Building Building { get; set; }
        public List<Elevator> Elevators { get; set; }

        public BuildingRepository(int floorsNumber, int elevatorsNumber)
        {
            if (floorsNumber < 2 || elevatorsNumber < 2)
            {
                throw new Exception("Building not valid for elevators");
            }
            Elevators = new List<Elevator>();
            for (int i = 1; i <= elevatorsNumber; i++)
            {
                Elevators.Add(new Elevator(i, 1));
            }
            Building = new Building(floorsNumber, Elevators);
        }
        public Building RetrieveBuilding()
        {
            return Building;
        }
        public Elevator RetrieveElevatorById(int elevatorId)
        {
            return Elevators.Find(elevator => elevator.Id == elevatorId);
        }
    }
}
