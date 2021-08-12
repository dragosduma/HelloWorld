using HelloWorldWeb.Models;
using HelloWorldWeb.Services;
using Moq;
using System;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class TeamServiceTest
    {
        private readonly ITimeService timeService;

        public TeamServiceTest()
        {
            var mock = new Mock<ITimeService>();
            mock.Setup(_ => _.GetDate()).Returns(new DateTime(2021, 08, 11));
            timeService = mock.Object; 
        }

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
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            TeamService teamService = new TeamService();

            // Act
            teamService.RemoveMember(2);

            // Assert
            Assert.Equal(6, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            ITeamService teamService = new TeamService();
            var targetTeamMember = teamService.GetTeamInfo().TeamMembers[0];
            var memberId = targetTeamMember.Id;
            // Act
            teamService.UpdateMemberName(memberId, "UnitTest");

            // Assert
            Assert.Equal("UnitTest", teamService.GetTeamMemberById(memberId).Name);
        }

        [Fact]
        public void CheckIdProblem()
        {
            //Assume
            ITeamService teamService = new TeamService();
            var memberToBeDeleted = teamService.GetTeamInfo().TeamMembers[teamService.GetTeamInfo().TeamMembers.Count-2];
            var newMemberName = "Borys";
            //Act
            teamService.RemoveMember(memberToBeDeleted.Id);
            var id = teamService.AddTeamMember(newMemberName);
            teamService.RemoveMember(id);
            //Assert
            var member = teamService.GetTeamInfo().TeamMembers.Find(element => element.Name == "Borys");
            Assert.Null(member);
        }

        [Fact]
        public void TestForGetAgeMethod()
        {
            //Assume
            var newTeamMember = new TeamMember("Intern", this.timeService);
            newTeamMember.Birthdate = new DateTime(1990, 09,30);
            //Act
            int age = newTeamMember.GetAge();
            //Assert
            Assert.Equal(30,age);
        }
    }
}
