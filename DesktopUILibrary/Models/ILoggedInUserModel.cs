﻿using System;

namespace DesktopUILibrary.Models
{
    public interface ILoggedInUserModel
    {
        DateTime CreateDate { get; set; }
        string EmailAddress { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Token { get; set; }
        void ResetUserModel();
    }
}