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
        private readonly ITimeService timeService;

        public DbTeamService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int AddTeamMember(string name)
        {
            TeamMember teamMember = new TeamMember(name, this.timeService);
            this.context.Add(teamMember);
            this.context.SaveChanges();
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
        }

        public void UpdateMemberName(int memberId, string name)
        {
            TeamMember teamMember = this.context.TeamMembers.Find(memberId);
            teamMember.Name = name;
            this.context.SaveChanges();
        }
    }
}
