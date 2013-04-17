using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ThoughtWorks.RailRoad.Repositories.Tests.Integration
{
    [TestClass]
    [DeploymentItem("RouteNodesData.txt")]
    public class RouteRepositoryTest
    {
        [TestMethod]
        public void GetAllRouteNodes_LocalFile_GetAllRouteNodes()
        {
            var routeRepository = new RouteRepository();
            var nodes = routeRepository.GetAllRouteNodes();
            Assert.IsTrue(nodes.Any());
        }

        [TestMethod]
        public void GetRouteNodesByCityName_CityName_GetAllNodesWithOrigin_A()
        {
            var routeRepository = new RouteRepository();
            var nodes = routeRepository.GetRouteNodesByCityName("A");
            Assert.IsTrue(nodes.All(n=>n.Origin.Name == "A"));
        }

        [TestMethod]
        public void GetRouteNodeByDestination_GetNodeForAB_GetABNode()
        {
            var routeRepository = new RouteRepository();
            var nodes = routeRepository.GetRouteNodeByDestination("A","B");
            Assert.IsNotNull(nodes);
        }

        [TestMethod]
        public void GetRouteNodeByDestination_GetNodeForAZ_GetNullNode()
        {
            var routeRepository = new RouteRepository();
            var nodes = routeRepository.GetRouteNodeByDestination("A", "Z");
            Assert.IsNull(nodes);
        }
    }
}
