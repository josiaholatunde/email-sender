using System;
using System.Collections.Generic;

namespace EmailSender.Helpers
{
    public class ApiResponse
    {
        public Object Data { get; set; }

        public string Message { get; set; }

        public Object Errors { get; set; }
    }
}