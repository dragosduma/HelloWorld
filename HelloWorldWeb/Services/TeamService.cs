using System.Collections.Generic;
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
            this.AddTeamMember("Teona");
            this.AddTeamMember("Radu");
            this.AddTeamMember("George");
            this.AddTeamMember("Dragos");
            this.AddTeamMember("Claudia");
            this.AddTeamMember("Leon");
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

        public void RemoveMember(int id)
        {
            this.teamInfo.TeamMembers.Remove(this.GetTeamMemberById(id));
        }

        public int AddTeamMember(string name)
        {
            TeamMember teamMember = new TeamMember() { Name = name };
            this.teamInfo.TeamMembers.Add(teamMember);
            return teamMember.Id;
        }

        public void UpdateMemberName(int memberId, string name)
        {
            int index = this.teamInfo.TeamMembers.FindIndex(element => element.Id == memberId);
            this.teamInfo.TeamMembers[index].Name = name;
        }
    }
}