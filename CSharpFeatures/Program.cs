using System;
using System.IO;
using System.Text.Json;

namespace CSharpFeatures
{
    class Program
    {
        static void Main(string[] args)
        {
            
            TeamMember teamMember = new TeamMember() { Name = "Member1" };
            string jsonString = JsonSerializer.Serialize(teamMember);
            Console.WriteLine(jsonString);
            File.WriteAllText("TeamMember.json", jsonString);
            var readText = File.ReadAllTextAsync("TeamMember.json");
            readText.Wait();
            var expectedOutput = readText.Result;
            var teamMemberDeserialized = JsonSerializer.Deserialize<TeamMember>(expectedOutput);
            Console.WriteLine(teamMemberDeserialized);

            Console.Write("What would you like? ");
            var customerInput = Console.ReadLine();
            Func<string, string, string, string, Coffee> recipe = (customerInput == "FlatWhite")? FlatWhite : Espresso;
            
            Coffee coffee = MakeCoffee("grain","milk","water","sugar", recipe);
            if (coffee == null)
            {
                Console.WriteLine("Sorry, your order can't be completed");
            }
            else 
            { 
                Console.WriteLine($"Here is your coffee: {coffee}."); 
            }
        }

        static Coffee MakeCoffee(string grains, string milk, string water, string sugar, Func<string, string, string, string, Coffee> recipe)
        {
            Coffee coffee = null;
            try
            {
                Console.WriteLine("Start preparing coffee.");
                coffee = recipe(grains, milk, water, sugar);
                return coffee;
            }
            catch (RecipeUnavailableException e)
            {
                Console.WriteLine($"Something went wrong: {e.Message}.");
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something went wrong, see exception details: {e.Message}");
            }
            finally
            {
                Console.WriteLine("Finished.");
            }
            return coffee;
        }

        static Coffee Espresso(string grains, string milk, string water, string sugar)
        {
            throw new RecipeUnavailableException();
        }
        static Coffee FlatWhite(string grains, string milk, string water, string sugar)
        {
            return new Coffee("FlatWhite");
        }
    }
}
