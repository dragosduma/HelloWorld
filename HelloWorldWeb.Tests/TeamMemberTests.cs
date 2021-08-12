using HelloWorldWeb.Models;
using HelloWorldWeb.Services;
using Moq;
using System;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class TeamServiceTest
    {
        private Mock<ITimeService> timeMock;
        public TeamServiceTest()
        {
            InitializeTimeServiceMock();
        }

        internal void InitializeTimeServiceMock()
        {
            timeMock = new Mock<ITimeService>();
            timeMock.Setup(_ => _.GetDate()).Returns(new DateTime(2021, 08, 11));           
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
            InitializeTimeServiceMock();
            var timeService = timeMock.Object;
            var newTeamMember = new TeamMember("Intern", timeService);
            newTeamMember.Birthdate = new DateTime(1990, 09,30);           
            //Act
            int age = newTeamMember.GetAge();
            //Assert
            timeMock.Verify(_ => _.GetDate(), Times.AtMostOnce());
            Assert.Equal(30,age);
        }
    }
}
