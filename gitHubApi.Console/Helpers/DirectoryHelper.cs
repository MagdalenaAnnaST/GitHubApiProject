using System;
using System.Collections.Generic;
using System.Text;

namespace GitHubApi.Helpers
{
    public static class DirectoryHelper
    {
        public static string GetProjectDirectory()
        {
            return AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
        }
    }
}
