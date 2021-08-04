// <copyright file="ErrorViewModel.cs" company="Principal33">
// Copyright (c) Principal33. All rights reserved.
// </copyright>

using System;

namespace HelloWorldWeb.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(this.RequestId);
    }
}
