using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HelloWorldWeb.Tests
{
    public class StartupTests
    {
        [Fact]
        public void ConvertHerokuStringToASPNETString() 
        {
            //Assume
            string herokuConnectionString = "postgres://ngjrrljdgzxowu:2e21b91daecf909af06a476c1d724c3b6ce57792810e0720f913d3c2ee1b32b1@ec2-34-251-245-108.eu-west-1.compute.amazonaws.com:5432/d7j42vqq2cj92r";
            //Act
            string aspNetConnectionString = Startup.ConvertHerokuStringToAspnetString(herokuConnectionString);
            //Assert
            Assert.Equal("Host=ec2-34-251-245-108.eu-west-1.compute.amazonaws.com;Port=5432;Username=ngjrrljdgzxowu;Password=2e21b91daecf909af06a476c1d724c3b6ce57792810e0720f913d3c2ee1b32b1;Database=d7j42vqq2cj92r;Pooling=true;SSL Mode=Require;TrustServerCertificate=True;", aspNetConnectionString);
        }
    }
}
