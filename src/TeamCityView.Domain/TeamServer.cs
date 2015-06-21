using TeamCitySharp;

namespace TeamCityView.Domain
{
    public class TeamServer : ITeamServer
    {
        private readonly TeamCityClient _client;

        public TeamServer()
        {
            _client = new TeamCityClient("http://es-ci-server.cloudapp.net/");
        }

        public ITeamServer Connect()
        {
            _client.ConnectAsGuest();
            return this;
        }


    }
}
