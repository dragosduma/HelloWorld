using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldWeb.Data;
using HelloWorldWeb.Models;

namespace HelloWorldWeb.Services
{
    public class DbTeamService : ITeamService
    {
        private readonly ApplicationDbContext context;
        private readonly IBroadcastService broadcastService;

        public DbTeamService(ApplicationDbContext context, IBroadcastService broadcastService)
        {
            this.context = context;
            this.broadcastService = broadcastService;
        }

        public int AddTeamMember(string name)
        {
            TeamMember teamMember = new TeamMember() { Name = name };
            this.context.Add(teamMember);
            this.context.SaveChanges();
            this.broadcastService.NewTeamMemberAdded(teamMember.Name, teamMember.Id);
            return teamMember.Id;
        }

        public TeamInfo GetTeamInfo()
        {
            TeamInfo teamInfo = new TeamInfo();
            teamInfo.Name = "Team Name";
            teamInfo.TeamMembers = this.context.TeamMembers.ToList();
            return teamInfo;
        }

        public TeamMember GetTeamMemberById(int v)
        {
            throw new NotImplementedException();
        }

        public void RemoveMember(int memberIndex)
        {
            var teamMember = this.context.TeamMembers.Find(memberIndex);
            this.context.TeamMembers.Remove(teamMember);
            this.context.SaveChanges();

            this.broadcastService.TeamMemberDeleted(memberIndex);
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember teamMember = this.context.TeamMembers.Find(memberId);
            teamMember.Name = name;
            this.context.SaveChanges();
            this.broadcastService.UpdateTeamMember(name, memberId);
        }
    }
}
