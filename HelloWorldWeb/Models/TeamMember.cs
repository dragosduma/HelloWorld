// <copyright file="TeamInfo.cs" company="Principal33">
// Copyright (c) Principal33. All rights reserved.
// </copyright>

using System;
using System.Diagnostics;
using HelloWorldWeb.Services;

namespace HelloWorldWeb.Models
{
    [DebuggerDisplay("{Name}[{Id}]")]
    public class TeamMember
    {
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