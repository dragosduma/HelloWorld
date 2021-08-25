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
            //Assume
            Mock<IBroadcastService> broadcastServiceMock = new Mock<IBroadcastService>();
            var broadcastService = broadcastServiceMock.Object;
            var teamService = new TeamService(broadcastService);

            //Act
            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            teamService.AddTeamMember("George");

            //Assert
            Assert.Equal(initialCount + 1, teamService.GetTeamInfo().TeamMembers.Count);
            broadcastServiceMock.Verify(_ => _.NewTeamMemberAdded(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            //var teamService = new TeamService(GetMockedMessageHub().Object);
            Mock<IBroadcastService> broadcastServiceMock = new Mock<IBroadcastService>();
            var broadcastService = broadcastServiceMock.Object;
            var teamService = new TeamService(broadcastService);

            int initialCount = teamService.GetTeamInfo().TeamMembers.Count;
            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.RemoveMember(id);

            // Assert
            Assert.Equal(initialCount - 1, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            //var teamService = new TeamService(GetMockedMessageHub().Object);
            Mock<IBroadcastService> broadcastServiceMock = new Mock<IBroadcastService>();
            var broadcastService = broadcastServiceMock.Object;
            var teamService = new TeamService(broadcastService);

            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.UpdateMemberName(id, "UnitTest");

            // Assert
            var member = teamService.GetTeamMemberById(id);
            Assert.Equal("UnitTest", member.Name);
        }

        [Fact]
        public void CheckIdProblem()
        {
            // Assume
            //var teamService = new TeamService(GetMockedMessageHub().Object);
            Mock<IBroadcastService> broadcastServiceMock = new Mock<IBroadcastService>();
            var broadcastService = broadcastServiceMock.Object;
            var teamService = new TeamService(broadcastService);

            var id = teamService.GetTeamInfo().TeamMembers[0].Id;

            // Act
            teamService.RemoveMember(id);
            int memberId = teamService.AddTeamMember("Test");
            teamService.RemoveMember(memberId);

            // Assert
            int lastIndex = teamService.GetTeamInfo().TeamMembers.Count;
            Assert.NotEqual("Test", teamService.GetTeamInfo().TeamMembers[lastIndex - 1].Name);
        }

        [Fact]
        public void TestForGetAgeMethod()
        {
            //Assume
            InitializeTimeServiceMock();
            var timeService = timeMock.Object;
            var newTeamMember = new TeamMember();
            newTeamMember.Birthdate = new DateTime(1990, 09,30);           
            //Act
            int age = newTeamMember.GetAge();
            //Assert
            timeMock.Verify(_ => _.GetDate(), Times.AtMostOnce());
            Assert.Equal(30,age);
        }
    }
}
