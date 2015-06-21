using TeamCitySharp.DomainEntities;

namespace TeamCityView.Domain
{
    public interface ITeamServer
    {
        ITeamServer Connect();
        Build[] ListBuildStatusesForTeam(string teamName);
    }
}