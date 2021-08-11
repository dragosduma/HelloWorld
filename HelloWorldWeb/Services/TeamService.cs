using System.Collections.Generic;
using System.Linq;
using HelloWorldWeb.Models;

namespace HelloWorldWeb.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamInfo teamInfo;
        private readonly ITimeService timeService;

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
            TeamMember teamMember = new TeamMember(name, this.timeService);

            this.teamInfo.TeamMembers.Add(teamMember);
            return teamMember.Id;
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember member = this.teamInfo.TeamMembers.Single(element => element.Id == memberId);
            member.Name = name;
        }
    }
}