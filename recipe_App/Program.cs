using System;
using System.Collections.Generic;

class Ingredient
{
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
}

class Recipe
{
    public string RecipeID { get; set; }
    public List<Ingredient> Ingredients { get; }
    public List<string> Steps { get; }

    // Constructor
    public Recipe(string recipeID)
    {
        RecipeID = recipeID;
        Ingredients = new List<Ingredient>();
        Steps = new List<string>();
    }

    // Method to add an ingredient to the recipe
    public void AddIngredient(string name, double quantity, string unit)
    {
        Ingredients.Add(new Ingredient { Name = name, Quantity = quantity, Unit = unit });
    }

    // Method to add a step to the recipe
    public void AddStep(string description)
    {
        Steps.Add(description);
    }

    // Method to display the recipe
    public void DisplayRecipe()
    {
        Console.WriteLine($"Recipe: {RecipeID}");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            Console.WriteLine($"- {ingredient.Quantity} {GetAbbreviatedUnit(ingredient.Unit)} {ingredient.Name}");
        }
        Console.WriteLine("Steps:");
        for (int i = 0; i < Steps.Count; i++)
        {
            Console.WriteLine($"Step {i + 1}: {Steps[i]}");
        }
    }

    // Method to scale the recipe
    public void ScaleRecipe(double factor)
    {
        foreach (var ingredient in Ingredients)
        {
            ingredient.Quantity *= factor;
        }
    }

    // Method to reset ingredient quantities
    public void ResetQuantities()
    {
        ScaleRecipe(1.0);
    }

    // Method to clear the recipe
    public void ClearRecipe()
    {
        Ingredients.Clear();
        Steps.Clear();
    }

    // Method to get the total quantity of a specific ingredient
    public double GetTotalQuantityOfIngredient(string ingredientName)
    {
        double totalQuantity = 0;
        foreach (var ingredient in Ingredients)
        {
            if (ingredient.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase))
            {
                totalQuantity += ingredient.Quantity;
            }
        }
        return totalQuantity;
    }

    // Private method to get abbreviated unit
    private string GetAbbreviatedUnit(string unit)
    {
        switch (unit.ToLower())
        {
            case "grams":
                return "g";
            case "kilograms":
                return "kg";
            case "milliliters":
                return "ml";
            case "liters":
                return "l";
            case "teaspoon":
                return "tsp";
            case "cups":
                return "cup";
            case "whole":
                return "";
            default:
                return unit;
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the Recipe Application!");
        string recipeName;

        // Loop until a valid recipe name is entered
        do
        {
            Console.Write("Enter recipe name: ");
            recipeName = Console.ReadLine();

            if (ContainsDigit(recipeName))
            {
                Console.WriteLine("Recipe name cannot contain numbers. Please try again.");
            }

        } while (ContainsDigit(recipeName));

        // Create a new recipe instance
        Recipe myRecipe = new Recipe(recipeName);

        // Prompt the user to enter the number of ingredients
        int numIngredients;
        bool isValidNumIngredients = false;

        // Loop until a valid number of ingredients is entered
        do
        {
            Console.Write("Enter the number of ingredients: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out numIngredients) && numIngredients > 0)
            {
                isValidNumIngredients = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than zero.");
            }
        } while (!isValidNumIngredients);

        // Loop to input ingredients
        for (int i = 0; i < numIngredients; i++)
        {
            Console.Write($"Ingredient {i + 1} name: ");
            string ingredientName = Console.ReadLine();

            double ingredientQuantity;
            bool isValidQuantity = false;

            // Loop until a valid quantity is entered
            do
            {
                Console.Write("Quantity: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out ingredientQuantity) && ingredientQuantity > 0)
                {
                    isValidQuantity = true;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid number greater than zero.");
                }
            } while (!isValidQuantity);

            string ingredientUnit;
            // Loop until a valid unit is entered
            do
            {
                Console.WriteLine("Available units:");
                DisplayUnits();
                Console.Write("Select unit: ");
                ingredientUnit = Console.ReadLine();

                if (!IsValidUnit(ingredientUnit))
                {
                    Console.WriteLine("Invalid unit. Please choose from the provided list.");
                }

            } while (!IsValidUnit(ingredientUnit));

            // Add ingredient to the recipe
            myRecipe.AddIngredient(ingredientName, ingredientQuantity, ingredientUnit);
        }

        // Prompt the user to enter the number of steps
        int numSteps;
        bool isValidNumSteps = false;

        // Loop until a valid number of steps is entered
        do
        {
            Console.Write("Enter the number of steps: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out numSteps) && numSteps > 0)
            {
                isValidNumSteps = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number greater than zero.");
            }
        } while (!isValidNumSteps);

        // Loop to input steps
        for (int i = 0; i < numSteps; i++)
        {
            Console.Write($"Step {i + 1}: ");
            string stepDescription = Console.ReadLine();
            myRecipe.AddStep(stepDescription);
        }

        // Display recipe details
        Console.WriteLine("\nYour recipe details:");
        myRecipe.DisplayRecipe();

        // Prompt to scale the recipe
        Console.WriteLine("\nDo you want to scale the recipe? Enter '0.5' (half), '2' (double), '3' (triple), or '1' to keep original quantities.");
        double scaleFactor;
        bool isValidScaleFactor = false;

        // Loop until a valid scale factor is entered
        do
        {
            Console.Write("Enter the scaling factor: ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out scaleFactor) && scaleFactor > 0)
            {
                isValidScaleFactor = true;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid scaling factor.");
            }
        } while (!isValidScaleFactor);

        if (scaleFactor != 1)
        {
            // Scale the recipe
            myRecipe.ScaleRecipe(scaleFactor);
            Console.WriteLine($"\nRecipe scaled by a factor of {scaleFactor}. New quantities:");
            myRecipe.DisplayRecipe();
        }

        // Prompt to reset quantities
        Console.WriteLine("\nDo you want to reset the quantities to the original values? (yes/no)");
        string resetInput = Console.ReadLine().ToLower();

        if (resetInput == "yes" || resetInput == "y")
        {
            // Reset quantities
            myRecipe.ResetQuantities();
            Console.WriteLine("\nQuantities reset to original values:");
            myRecipe.DisplayRecipe();
        }
        else if (resetInput == "no" || resetInput == "n")
        {
            Console.WriteLine("\nQuantities will remain unchanged:");
            myRecipe.DisplayRecipe();
        }
        else
        {
            Console.WriteLine("\nInvalid input. Quantities will remain unchanged:");
            myRecipe.DisplayRecipe();
        }

        // Prompt to clear the recipe
        Console.WriteLine("\nDo you want to clear the recipe and start over? (yes/no)");
        string clearInput = Console.ReadLine().ToLower();

        if (clearInput == "yes" || clearInput == "y")
        {
            // Clear the recipe
            myRecipe.ClearRecipe();
            Console.WriteLine("\nRecipe cleared. You can now enter a new recipe.");
        }
        else if (clearInput == "no" || clearInput == "n")
        {
            Console.WriteLine("\nRecipe will remain unchanged:");
            myRecipe.DisplayRecipe();
        }
        else
        {
            Console.WriteLine("\nInvalid input. Recipe will remain unchanged:");
            myRecipe.DisplayRecipe();
        }
    }

    // Method to check if a string contains a digit
    static bool ContainsDigit(string input)
    {
        foreach (char c in input)
        {
            if (char.IsDigit(c))
            {
                return true;
            }
        }
        return false;
    }

    // Method to display available units
    static void DisplayUnits()
    {
        Console.WriteLine("- g");
        Console.WriteLine("- kg");
        Console.WriteLine("- ml");
        Console.WriteLine("- l");
        Console.WriteLine("- tsp");
        Console.WriteLine("- cups");
        Console.WriteLine("- whole");
    }

    // Method to check if a unit is valid
    static bool IsValidUnit(string unit)
    {
        List<string> validUnits = new List<string> { "g", "kg", "ml", "l", "tsp", "cups", "whole" };
        return validUnits.Contains(unit.ToLower());
    }
}
