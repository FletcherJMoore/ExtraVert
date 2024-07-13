// See https://aka.ms/new-console-template for more information
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Win32.SafeHandles;
using static System.Runtime.InteropServices.JavaScript.JSType;

List<Plant> plants  = new List<Plant>()
{
new Plant()
    {
        Species = "Sweet pea",
        AskingPrice = 20,
        Sold = false,
        City = "Nashville",
        ZIP = 37073,
        LightNeeds = 4,
        AvailableUntil = new DateTime(2025, 2, 23)
    },

new Plant()
    {
        Species = "Carnation",
        AskingPrice = 10,
        Sold =true,
        City = "New York",
        ZIP = 10009,
        LightNeeds = 3,
        AvailableUntil = new DateTime(2023, 8, 23)
    },

new Plant()
    {
        Species = "Black-eyed Susan",
        AskingPrice = 100,
        Sold = false,
        City = "Cincinnati",
        ZIP = 45201,
        LightNeeds = 2,
        AvailableUntil = new DateTime(2024, 6, 4)
    },

new Plant()
    {
        Species = "Red valerian",
        AskingPrice = 30,
        Sold = false,
        City = "Chicago",
        ZIP = 60018,
        LightNeeds = 1,
        AvailableUntil = new DateTime(2024, 7, 12)
    },

new Plant()
    {
        Species = "Daisy",
        AskingPrice = 5,
        Sold = true,
        City = "Gatlinburg",
        ZIP = 37738,
        LightNeeds = 5,
        AvailableUntil = new DateTime(2024, 1, 9)
    },
};

string choice = null;
while (choice != "0")
{
    Console.WriteLine(@"Choose an option:
                        0. Exit
                        1. Display all plants
                        2. Post a plant to be adopted
                        3. Adopt a plant 
                        4. Delist a plant
                        5. Plant of the day
                        6. Search Plant");

    choice = Console.ReadLine();
    if (choice == "0")
    {
        Console.WriteLine("Goodbye!");
    }
    else if (choice == "1")
    {
        ViewPlantDetails();
    }
    else if (choice == "2")
    {
        CreateNewPlant();
    }
    else if (choice == "3")
    {
        AdoptPlant();
    }
    else if (choice == "4")
    {
        DelistPlants();
    }
    else if (choice == "5") 
    {
        PlantOfTheDay();
    }
    else if (choice == "6")
    {
        Search();
    }
    else if (choice == "7")
    {
        PlantStatistics();
    }
}

void PlantStatistics()
{
    double lowestPrice = plants.Min(plants => plants.AskingPrice);
    foreach (Plant plant in plants)
    {
        if (plant.AskingPrice == lowestPrice)
        {
            Console.WriteLine($"The plant with the lowest price is the {plant.Species} which costs {plant.AskingPrice} dollars.");
        }
    }
    double numberofPlants = plants.Count();
    Console.WriteLine($"There are {numberofPlants} plants listed currently.");

    double highestNeeds = plants.Max(plants => plants.LightNeeds);
    foreach (Plant plant in plants)
    {
        if (plant.LightNeeds == highestNeeds)
        {
            Console.WriteLine($"The plant with the highest light needs is the {plant.Species} with a value of {plant.LightNeeds}");
        }
    }
    int totalLightNeeds = 0;
    int numberOfPlants = plants.Count;
    foreach (Plant plant in plants)
    {
        totalLightNeeds += plant.LightNeeds;
    }
    double avgLightNeeds = (double)totalLightNeeds / numberOfPlants;
    Console.WriteLine($"The average plant's light needs is {avgLightNeeds}");
}

void PlantOfTheDay()
    {
    Random random = new Random();

    int randomInteger = random.Next(1, 5);

    Plant plantOfTheDay = plants[randomInteger];

    Console.WriteLine($"Plant of the day is {plantOfTheDay.Species}");
}
void ViewPlantDetails()
{
    ListPlants();

    Plant chosenPlant = null;

    while (chosenPlant == null)
    {
        Console.WriteLine("Please enter a plant number: ");
        try
        {
            int response = int.Parse(Console.ReadLine().Trim());
            chosenPlant = plants[response - 1];
        }
         catch (FormatException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }

    Console.WriteLine(@$"A {chosenPlant.Species} in {chosenPlant.City} {(chosenPlant.Sold ? "was sold" : "is available")} for {chosenPlant.AskingPrice} and is available until {chosenPlant.AvailableUntil}.");
}

decimal totalValue = 0.0M;
foreach (Plant plant in plants)
{
    if (!plant.Sold)
    {
        totalValue += plant.AskingPrice;
    }
}
Console.WriteLine($"Total inventory value: ${totalValue}");

for (int i = 0; i < plants.Count; i++)
{
    Console.WriteLine($"{i + 1}. {plants[i].Species}");
}

void ListPlants()
{
    decimal totalValue = 0.0M;
    foreach (Plant plant in plants)
    {
        if (!plant.Sold)
        {
            totalValue += plant.AskingPrice;
        }
    }
    Console.WriteLine($"Total inventory value: ${totalValue}");
    Console.WriteLine("Plants:");
    for (int i = 0; i < plants.Count; i++) 
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species}");
    }
}

void CreateNewPlant()
{
    Console.WriteLine("Post a new plant!");

    Console.WriteLine("Type the Plant Species");
    string? species = Console.ReadLine()!;

    Console.WriteLine("Enter Price");
    int askingPrice = int.Parse(Console.ReadLine()!.Trim());

    Console.WriteLine("Enter Light Needs (1-5)");
    int lightNeeds = int.Parse(Console.ReadLine()!.Trim());

    Console.WriteLine("Enter City");
    string city = Console.ReadLine()!;

    Console.WriteLine("Enter ZIP");
    int zipCode = int.Parse(Console.ReadLine()!.Trim());

    Console.WriteLine("Enter Expiration year");
    int year = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter Expiration Month");
    int month = int.Parse(Console.ReadLine());

    Console.WriteLine("Enter Expiration Day");
    int day = int.Parse(Console.ReadLine());

    DateTime expirationDate = new DateTime(year, month, day);

    Plant plant = new Plant
    {
        Species = species,
        LightNeeds = lightNeeds,
        AskingPrice = askingPrice,  
        City = city,
        ZIP = zipCode,
        AvailableUntil = expirationDate
    };

    plants.Add(plant);

    Console.WriteLine("Plant has been posted!");
}

void AdoptPlant()
{
    // create a new empty List to store the latest products
    List<Plant> plantsToAdopt = new List<Plant>();

    Console.WriteLine("Plants available for adoption:");
    
    for (int i = 0; i < plantsToAdopt.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plantsToAdopt[i].Species}");
    }

    Plant adoptedPlant = null!;

    while (adoptedPlant == null)
    {
        Console.WriteLine("Please enter the number of the plant you'd like to adopt.");
        try
        {
            int response = int.Parse(Console.ReadLine()!.Trim());
            adoptedPlant = plantsToAdopt[response - 1];
            adoptedPlant.Sold = true;
        }
        catch (FormatException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }
    Console.WriteLine($"Congrats! You are the proud owner of the {adoptedPlant.Species} plant!");
}
void DelistPlants()
{
    Console.WriteLine("Plants available for adoption:");

    for (int i = 0; i < plants.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {plants[i].Species}");
    }

    Plant RemovePlant = null!;
    while (RemovePlant == null)
    {
        Console.WriteLine("Choose the number of the plant you'd like to remove.");
        try
        {
            int response = int.Parse(Console.ReadLine()!.Trim());
            RemovePlant = plants[response - 1];
        }
        catch (FormatException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Please choose an existing item only!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            Console.WriteLine("Do Better!");
        }
    }
    plants.Remove(RemovePlant);

    Console.WriteLine($"you have succesfully removed the {RemovePlant.Species} plant!");
}

void Search()
{
    Console.WriteLine($"Enter a maximum light needs number between 1 and 5");
    int maxLightNeeds;
    if (!int.TryParse(Console.ReadLine(), out maxLightNeeds) || maxLightNeeds < 1 || maxLightNeeds > 5)
    {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 5 ");
    }
    else
    {
        List<Plant> searchedPlant = new List<Plant>();

        foreach (var plant in plants)
        {
            if (plant.LightNeeds <= maxLightNeeds)
            {
                searchedPlant.Add(plant);
                Console.WriteLine(plant.Species);
            }
        }
    }
}