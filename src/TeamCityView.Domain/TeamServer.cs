using System;
using System.Collections.Generic;
using System.Linq;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;

namespace TeamCityView.Domain
{
    public class TeamServer : ITeamServer
    {
        private readonly TeamCityClient _client;
        //private TimeSpan _detectsBuildsFrom = TimeSpan.FromDays(500); //TimeSpan.FromDays(1);

        //"http://es-ci-server.cloudapp.net/"

        public TeamServer(string server)
        {
            _client = new TeamCityClient(server);
        }

        public ITeamServer Connect()
        {
            _client.ConnectAsGuest();
            return this;
        }

        public Build[] ListBuildStatusesForTeam(string teamName)
        {
            List<User> users = _client.Users.AllUsersByUserGroup(teamName);
            List<Build> builds = ListLatestBuildsForUsersInLast24Hours(users);

            return builds
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .ToArray();
        }

        private List<Build> ListLatestBuildsForUsersInLast24Hours(List<User> users)
        {
            var result = new List<Build>();

            foreach (User user in users)
            {
                result.AddRange(_client.Builds.ByUserName(user.Username));
            }

            return result;
        }
    }
}
