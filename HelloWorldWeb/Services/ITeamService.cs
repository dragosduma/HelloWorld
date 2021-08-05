using HelloWorldWeb.Models;

namespace HelloWorldWeb.Services
{
    public interface ITeamService
    {
        void AddTeamMember(string name);

        public void RemoveMember(int memberIndex);

        TeamInfo GetTeamInfo();
    }
}