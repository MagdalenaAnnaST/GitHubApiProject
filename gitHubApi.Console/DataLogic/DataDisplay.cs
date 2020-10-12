

using GitHubApi.Dto;

namespace GitHubApi.DataLogic
{
    public class DataDisplay
    {
        public string GetCommitDescription (CommitDto commitDto)
        {
            return $"[{commitDto.Repository}]/[{commitDto.Sha}]: {commitDto.Message} [{commitDto.CommiterName}]";
        }
    }
}
