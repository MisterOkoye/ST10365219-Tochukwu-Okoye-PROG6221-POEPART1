using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace RecipeApplication
{
    class Program
    {
        // this is able to flag the application on whether the data has been cleared.
        public static bool isClear = false;

        static void Main(string[] args)
        {
            // Creating an instance of RecipeManager to manage recipes
            RecipeManager recipeManager = new RecipeManager();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("WELCOME TO THE RECIPE REPOSITORY!");
            Console.ResetColor();

            recipeManager.EnterRecipe();
          //  Console.Clear();

            recipeManager.ViewRecipe();

            while (true)
            {
                Console.WriteLine("\nMENU:");
                Console.WriteLine("1. Scale Recipe");
                Console.WriteLine("2. Reset Quantities");
                Console.WriteLine("3. Clear All Data");
                Console.WriteLine("4. Exit");

                int choice = getChoice("Enter your choice: ");

                switch (choice)
                {
                    case 1:
                        recipeManager.ScaleRecipe();
                        recipeManager.ViewRecipe();
                        break;
                    case 2:
                        recipeManager.ResetQuantities();
                        recipeManager.ViewRecipe();
                        break;
                    case 3:
                        recipeManager.ClearData();
                        if (isClear == false) // if the data was not cleared, the program will run the 'ViewRecipe()' method
                        {
                            recipeManager.ViewRecipe();
                            break;
                        }
                        recipeManager.EnterRecipe();
                        recipeManager.ViewRecipe();
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Good-Bye, exiting...");
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        Console.ResetColor();
                        break;
                }
            }
        }

        // Method to get an integer choice from the user, this method is able to validate the user's input to make sure
        // that it is a valid integer
        public static int getChoice(string message)
        {
            int input;
            Console.WriteLine(message);
            while (!int.TryParse(Console.ReadLine(), out input))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input, please enter a valid integer.");
                Console.ResetColor();
                Console.WriteLine(message);
            }
            return input;
        }

        // Method to get a double quantity from the user, likewise this method is able to validate the user's input to make sure
        // that it is a valid double value
        public static double getQuantities(string message)
        {
            double input;
            Console.WriteLine(message);
            while (!double.TryParse(Console.ReadLine(), out input))
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input, please enter a valid number.");
                Console.ResetColor();
                Console.WriteLine(message);
            }
            return input;
        }
    }

    class RecipeManager
    {
        private string[] ingredients;
        private string[] steps;
        private double[] quantities;
        private string[] units;

        public void EnterRecipe()
        {
            Console.WriteLine("\nEnter the details for the recipe");

            int ingredientCount = Program.getChoice("Number of ingredients:");
            Console.Clear();
            ingredients = new string[ingredientCount];
            quantities = new double[ingredientCount];
            units = new string[ingredientCount];

            // Looping through each ingredient to get details
            for (int i = 0; i < ingredientCount; i++)
            {
                Console.Write($"[{i + 1}] Ingredient Name: ");
                ingredients[i] = Console.ReadLine();
                Console.Clear();

                double quantData = Program.getQuantities($"Quantity of {ingredients[i]}: ");
                quantities[i] = quantData;

                String choiceTry;
                int choice = 0;

                do
                {
                    Console.Clear();

                    if (choice > 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice, please enter a valid integer\n");
                        Console.ResetColor();
                    };

                    Console.WriteLine("Choose unit of measurement:");
                    Console.WriteLine("1. Teaspoon(s)");
                    Console.WriteLine("2. Tablespoon(s)");
                    Console.WriteLine("3. Cup(250ml)");
                    Console.WriteLine("Enter the unit of measurement for: ");
                    choiceTry = Console.ReadLine();
                    choice++;

                } while (choiceTry != "1" && choiceTry != "2" && choiceTry != "3");
                choice = int.Parse(choiceTry);

                string unit = "";
                switch (choice)
                {
                    case 1:
                        unit = "Teaspoon(s)";
                        break;
                    case 2:
                        unit = "Tablespoon(s)";
                        break;
                    case 3:
                        unit = "Cup(250ml)";
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice, recipe was not added.");
                        Console.ResetColor();
                        break;
                }

                units[i] = unit;

                Console.Clear();
            }

            int stepCount = Program.getChoice("Number of steps:");
            steps = new string[stepCount]; // Initialize the steps array
            Console.Clear();
            // Looping through each step to get details
            for (int i = 0; i < stepCount; i++)
            {
                Console.Write($"Enter step {i + 1}: ");
                steps[i] = Console.ReadLine();
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Recipe Successfully Added.\n");
            Console.ResetColor();
        }

        public void ViewRecipe()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("RECIPE DETAILS");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ingredients:");
            Console.ResetColor();
            int recipeCount = 0;

            for (int i = 0; i < ingredients.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {quantities[i]} {units[i]} of {ingredients[i]}");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nSteps:");
            Console.ResetColor();
            for (int i = 0; i < steps.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        // ArrayList to store scaling factors
        ArrayList scaleFactor = new ArrayList();

        // Method to scale the recipe
        public void ScaleRecipe()
        {
            Console.Clear();
            Console.WriteLine("Choose scaling factor:");
            Console.WriteLine("1. 0.5 (half)");
            Console.WriteLine("2. 2 (double)");
            Console.WriteLine("3. 3 (triple)");
            Console.WriteLine("Enter your choice:");

            // Getting user choice for scaling factor
            int choice = int.Parse(Console.ReadLine());
            double factor = 1.0;
            switch (choice)
            {
                case 1:
                    factor = 0.5;
                    break;
                case 2:
                    factor = 2.0;
                    break;
                case 3:
                    factor = 3.0;
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid choice, recipe remains unchanged.");
                    Console.ResetColor();
                    return;
            }

            // Storing the scaling factor into the Arraylist(a where each factor the user entered will be stored)
            scaleFactor.Add(factor);
            Console.Clear();

            // Scaling each quantity
            for (int i = 0; i < quantities.Length; i++)
            {
                quantities[i] *= factor;
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Recipe Scaled By Factor {factor}.\n");
            Console.ResetColor();
        }

        // Method to reset quantities to original values
        public void ResetQuantities()
        {
            double scaleAmount = 1;

            // Calculating the total scaling factor
            foreach (double item in scaleFactor)
            {
                scaleAmount *= item;
            }

            // Resetting each quantity, by dividing eac quantites by the multiplication of all the scaling factor entered by the user, thus the original quantities is retrieved.
            for (int i = 0; i < quantities.Length; i++)
            {
                quantities[i] /= scaleAmount;
            }
            Console.Clear();
            scaleFactor.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Quantities Reset To Original Values.\n");
            Console.ResetColor();
        }

        // Method to clear all the data
        public void ClearData()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Are you sure, you want to clear all your data (Y/N)?");
            Console.ResetColor();
            if (Console.ReadLine().ToUpper() == "Y")
            {
                // Clearing all data
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("All Data Has Been Cleared.");
                Console.ResetColor();
                ingredients = new string[0];
                steps = new string[0];
                quantities = new double[0];
                units = new string[0];
                scaleFactor.Clear();
                Program.isClear = true;
            }
            else
            {
                Program.isClear = false;
            }
        }
    }
}
