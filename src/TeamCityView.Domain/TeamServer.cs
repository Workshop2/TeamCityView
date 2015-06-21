using System;
using System.Collections.Generic;
using System.Linq;
using TeamCitySharp;
using TeamCitySharp.DomainEntities;
using TeamCitySharp.Locators;

namespace TeamCityView.Domain
{
    public class TeamServer : ITeamServer
    {
        private readonly TeamCityClient _client;
        private readonly TimeSpan _detectBuildsFrom = TimeSpan.FromDays(1);//TimeSpan.FromDays(500); //TimeSpan.FromDays(1);

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
            List<Build> builds = ListLatestBuildsForUsersInLast24Hours(users);//ListLatestBuildsForUsers(users);

            return builds
                .OrderByDescending(x => x.StartDate)
                .Take(10)
                .ToArray();
        }

        private List<Build> ListLatestBuildsForUsers(List<User> users)
        {
            var result = new List<Build>();

            foreach (User user in users)
            {
                result.AddRange(_client.Builds.ByUserName(user.Username));
            }

            return result;
        }

        private List<Build> ListLatestBuildsForUsersInLast24Hours(List<User> users)
        {
            var result = new List<Build>();
            const int maxResults = 20;
            DateTime fromDate = DateTime.Now.Add(-_detectBuildsFrom).Date;

            foreach (User user in users)
            {
                var builds = _client.Builds.ByBuildLocator(BuildLocator.WithDimensions(null, UserLocator.WithUserName(user.Username),
                    agentName: null, status: null, personal: null, canceled: null, running: null, pinned: null,
                    maxResults: maxResults, startIndex: null, sinceBuild: null, sinceDate: fromDate, tags: null,
                    branch: null));

                result.AddRange(builds);
            }

            return result;
        }
    }
}
