using System.Collections.Generic;
using System.Linq;
using HelloWorldWeb.Models;
using Microsoft.AspNetCore.SignalR;

namespace HelloWorldWeb.Services
{
    public class TeamService : ITeamService
    {
        private readonly TeamInfo teamInfo;
        private readonly IBroadcastService broadcastService;

        public TeamService(IBroadcastService broadcastService)
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
            this.broadcastService = broadcastService;
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
            this.broadcastService.TeamMemberDeleted(id);
        }

        public int AddTeamMember(string name)
        {
            TeamMember teamMember = new TeamMember();
            this.teamInfo.TeamMembers.Add(teamMember);
            return teamMember.Id;
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember member = this.teamInfo.TeamMembers.Single(element => element.Id == memberId);
            member.Name = name;
            this.broadcastService.UpdateTeamMember(name, memberId);
        }
    }
}