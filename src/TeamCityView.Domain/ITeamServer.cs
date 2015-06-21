namespace TeamCityView.Domain
{
    public interface ITeamServer
    {
        ITeamServer Connect();
        IBuildStatus[] ListBuildStatusesForTeam(string teamName);
    }
}