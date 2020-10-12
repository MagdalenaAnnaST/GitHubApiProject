using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubApi.Dto
{
    public struct CommitDto
    {
        public string CommiterName { get; set; }
        public string Message { get; set; }
        public string Sha { get; set; }
        public string Repository { get; set; }
        public string User { get; set; }
    }
}
