using HelloWorldWeb.Services;
using System;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class TeamServiceTest
    {
        [Fact]
        public void AddTeamMemberToTheTeam()
        {
            // Assume
            TeamService teamService = new TeamService();

            // Act
            teamService.AddTeamMember("intern");

            // Assert
            Assert.Equal(7, teamService.GetTeamInfo().TeamMembers.Count);           
        }

        [Fact]
        public void RemoveMemberFromTheTeamAfterAdding()
        {
            // Assume
            TeamService teamService = new TeamService();

            // Act
            teamService.AddTeamMember("intern");
            teamService.RemoveMember(2);

            // Assert
            Assert.Equal(6, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            ITeamService teamService = new TeamService();

            // Act
            teamService.UpdateMemberName(1, "UnitTest");

            // Assert
            var member = teamService.GetTeamMemberById(1);
            Assert.Equal("UnitTest", member.Name);
        }
    }
}
