﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClinicaWeb.Models
{
    public class AuthenticationSuccessful
    {
        public bool Success { get; set; }
    }

    public class AuthenticationFailed
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}