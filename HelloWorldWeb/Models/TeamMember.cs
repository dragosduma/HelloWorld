﻿// <copyright file="TeamInfo.cs" company="Principal33">
// Copyright (c) Principal33. All rights reserved.
// </copyright>

using System;

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

        public DateTime Birthdate { get; set; }

        public int GetAge()
        {
            var age = DateTime.Now.Subtract(this.Birthdate).Days;
            age = age / 365;



            return age;
        }
    }
}