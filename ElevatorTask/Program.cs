using Business.Services;
using Repository;
using Repository.DataAccess;
using Repository.Models;
using System;

namespace ElevatorTask
{
    internal class Program
    {
        private const string quit = "q";
        private const string selectionCallElevator = "a";
        private const string selectionGoingToFloor = "b";

        static void Main(string[] args)
        {
            Console.WriteLine("Please create building");

            Console.WriteLine("Please enter the building floors");
            int buildingFloors = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Please enter the required edlevators number");
            int elevatorsCount = Convert.ToInt32(Console.ReadLine());

            BuildingRepository building = new BuildingRepository(buildingFloors, elevatorsCount);
            Console.WriteLine($"Building successfully created: Building has {buildingFloors} floors and {elevatorsCount} elevators");
            Building currentBuilding = building.RetrieveBuilding();

            var manager = new ElevatorManager();
            string input = "";
            int elevatorId;
            while (input != quit)
            {
                Console.WriteLine("Select your action:");
                Console.WriteLine("Press 'q' to quit, press 'a' to call elevator or press 'b' if you are in elevator");
                input = Console.ReadLine();
                if (input == selectionCallElevator)
                {
                    Console.WriteLine("Enter Floor:");
                    input = Console.ReadLine();

                    int floor;
                    if (int.TryParse(input, out floor))
                    {
                        Elevator currentElevator = manager.GetClosestElevator(currentBuilding, floor);
                        manager.ElevatorCall(currentBuilding, floor, currentElevator.Id);
                        currentElevator.IsBusy = true;
                        Console.WriteLine($"Elevator ID is: {currentElevator.Id}");
                    }
                    else
                    {
                        Console.WriteLine("You have pressed an incorrect floor, Please try again");
                    }
                }
                else if(input == selectionGoingToFloor)
                {
                    Console.WriteLine("Enter Floor:");
                    input = Console.ReadLine();
                    Console.WriteLine("Enter elevator ID:");
                    elevatorId = Convert.ToInt32(Console.ReadLine());
                    Elevator currentElevator = currentBuilding.Elevators.Find(elevator => elevator.Id == elevatorId);
                    int floor;
                    if (int.TryParse(input, out floor))
                    {
                        manager.ElevatorCall(currentBuilding, floor, elevatorId);
                    }
                    else
                    {
                        Console.WriteLine("You have pressed an incorrect floor, Please try again");
                    }
                    currentElevator.IsBusy = false;
                }
                else if (input == quit)
                {
                    Console.WriteLine("GoodBye!");
                }
                else
                {
                    Console.WriteLine("You have pressed an incorrect choice, Please try again");
                }
            }
        }
    }
}
