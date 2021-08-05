// <copyright file="TeamInfo.cs" company="Principal33">
// Copyright (c) Principal33. All rights reserved.
// </copyright>

namespace HelloWorldWeb.Models
{
    public class TeamMember
    {
        private static int idCount = 0;

        public TeamMember(string name)
        {
            this.Name = name;
            this.Id = idCount;
            idCount++;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}