using NUnit.Framework;
using SpecsFor;
using TeamCityView.Domain;

namespace TeamCityView.Tests.Unit.Domain
{
    public static class TeamServerTests
    {
         public class when_connecting_as_guest : SpecsFor<TeamServer>
         {
             protected override void InitializeClassUnderTest()
             {
                 SUT = new TeamServer("es-ci-server.cloudapp.net");
             }

             protected override void When()
             {
                 SUT.Connect();
             }

             [Test]
             public void then_should_connect_list_projects()
             {
                 SUT.ListBuildStatusesForTeam("SPITFIRE");
             }
         }
    }
}