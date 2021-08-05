using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Models;

namespace HelloWorldWeb.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamInfo teamInfo;

        public TeamService()
        {
            this.teamInfo = new TeamInfo
            {
                Name = "Team 3",
                TeamMembers = new List<string>(new string[]
                {
                    "Radu",
                    "Teona",
                    "Claudia",
                    "Dragos",
                    "Leon",
                    "George",
                }),
            };
        }

        public TeamInfo GetTeamInfo()
        {
            return this.teamInfo;
        }

        public void RemoveMember(int memberIndex)
        {
            this.teamInfo.TeamMembers.RemoveAt(memberIndex);
        }

        public void AddTeamMember(string name)
        {
            this.teamInfo.TeamMembers.Add(name);
        }
    }
}