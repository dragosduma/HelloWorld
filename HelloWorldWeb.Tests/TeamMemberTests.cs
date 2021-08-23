using HelloWorldWeb.Models;
using HelloWorldWeb.Services;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class TeamServiceTest
    {
        private Mock<ITimeService> timeMock;
        private Mock<IHubContext<MessageHub>> messageHubMock = null;
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
            TeamService teamService = new TeamService(null);

            // Act
            teamService.AddTeamMember("intern");

            // Assert
            Assert.Equal(7, teamService.GetTeamInfo().TeamMembers.Count);           
        }

        [Fact]
        public void RemoveMemberFromTheTeam()
        {
            // Assume
            TeamService teamService = new TeamService(null);

            // Act
            teamService.RemoveMember(2);

            // Assert
            Assert.Equal(6, teamService.GetTeamInfo().TeamMembers.Count);
        }

        [Fact]
        public void UpdateMemberName()
        {
            // Assume
            ITeamService teamService = new TeamService(null);
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
            ITeamService teamService = new TeamService(null);
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

        [Fact]
        public void CheckmessageHubLine()
        {
            //Assume
            InitializeMessageHubMock();
            hubAllClientsMock.Setup(_ => _.SendAsync("NewTeamMemberAdded", "Radu", 7, It.IsAny<CancellationToken>()));
            var messageHub = messageHubMock.Object;
            //Act
            messageHub.Clients.All.SendAsync("NewTeamMemberAdded", "Radu", 7);

            //Assert
            hubAllClientsMock.Verify(hubAllClients => hubAllClients.SendAsync("NewTeamMemberAdded", "Radu", 7, It.IsAny<CancellationToken>()), Times.Once(),"I expect send async to be called once.");
           // Mock.Get(hubAllClientsMock).Verify(_ => _.SendAsync("NewTeamMemberAdded", "Radu", 7), Times.Once());
        }

        private Mock<IClientProxy> hubAllClientsMock;
        private Mock<IHubClients> hubClientsMock;
        private void InitializeMessageHubMock()
        {
            // https://www.codeproject.com/Articles/1266538/Testing-SignalR-Hubs-in-ASP-NET-Core-2-1
            hubAllClientsMock = new Mock<IClientProxy>();
            hubClientsMock = new Mock<IHubClients>();
            hubClientsMock.Setup(_ => _.All).Returns(hubAllClientsMock.Object);
            messageHubMock = new Mock<IHubContext<MessageHub>>();
            messageHubMock.SetupGet(_ => _.Clients).Returns(hubClientsMock.Object);
        }

        private IHubContext<MessageHub> GetMockedMessageHub()
        {
            if (messageHubMock == null)
            {
                InitializeMessageHubMock();
            }
            return messageHubMock.Object;
        }
    }
}
