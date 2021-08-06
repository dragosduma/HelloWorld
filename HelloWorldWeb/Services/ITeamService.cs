using HelloWorldWeb.Models;

namespace HelloWorldWeb.Services
{
    public interface ITeamService
    {
        int AddTeamMember(string name);

        public void RemoveMember(int memberIndex);

        TeamInfo GetTeamInfo();

        public TeamMember GetTeamMemberById(int v);

        public void UpdateMemberName(int memberId, string name);
    }
}