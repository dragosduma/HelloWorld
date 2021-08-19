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
        }
    }
}
