﻿namespace TechTalk.SpecFlow.CodeBehindGenerator.Shared.Response
{
    public class ShutdownResponse : BaseResponse
    {
        public ShutdownResponse(int id)
        {
            ServerProcessId = id;
        }

        public int ServerProcessId { get; set; }
    }
}