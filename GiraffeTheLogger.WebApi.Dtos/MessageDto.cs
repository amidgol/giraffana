﻿using System;
namespace GiraffeTheLogger.WebApi.Dtos
{
    public class MessageDto
    {
        public string AppTitle { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public int? ErrorCode { get; set; }
        public long? UserRef { get; set; }
        public DateTime CreationDateTime { get; set; }
        public DateTime ReceiveDateTime { get; set; }
    }
}