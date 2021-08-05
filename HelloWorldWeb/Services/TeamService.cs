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
                TeamMembers = new List<TeamMember>(),
            };

            this.teamInfo.TeamMembers.Add(new TeamMember("Teona"));
            this.teamInfo.TeamMembers.Add(new TeamMember("Radu"));
            this.teamInfo.TeamMembers.Add(new TeamMember("George"));
            this.teamInfo.TeamMembers.Add(new TeamMember("Dragos"));
            this.teamInfo.TeamMembers.Add(new TeamMember("Claudia"));
            this.teamInfo.TeamMembers.Add(new TeamMember("Leon"));
        }

        public TeamInfo GetTeamInfo()
        {
            return this.teamInfo;
        }

        public TeamMember GetTeamMemberById(int id)
        {
            foreach (TeamMember teamMember in this.teamInfo.TeamMembers)
            {
                if (id == teamMember.Id)
                {
                    return teamMember;
                }
            }

            return null;
        }

        public void RemoveMember(int memberIndex)
        {
            this.teamInfo.TeamMembers.Remove(this.GetTeamMemberById(memberIndex));
        }

        public int AddTeamMember(string name)
        {
            TeamMember teamMember = new TeamMember(name);

            this.teamInfo.TeamMembers.Add(teamMember);
            return teamMember.Id;
        }
    }
}