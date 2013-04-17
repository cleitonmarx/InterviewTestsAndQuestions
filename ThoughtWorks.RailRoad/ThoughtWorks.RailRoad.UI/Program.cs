using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThoughtWorks.RailRoad.Domain.Exceptions;
using ThoughtWorks.RailRoad.Domain.Locations;
using ThoughtWorks.RailRoad.UI.Application;
using ThoughtWorks.RailRoad.UI.Util;

namespace ThoughtWorks.RailRoad.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeConsole();

            ConsoleUtil.DrawTextWithBorder(
                0, 0, 3, "Problem: TRAINS", ConsoleColor.Blue, ConsoleColor.Yellow, ConsolePosition.Center
            );
            
            var allQuestionsText = new StringBuilder();
            allQuestionsText.AppendLine(GetTextForDistanceOutput(1, "A","B","C"));
            allQuestionsText.AppendLine(GetTextForDistanceOutput(2, "A", "D"));
            allQuestionsText.AppendLine(GetTextForDistanceOutput(3, "A", "D", "C"));
            allQuestionsText.AppendLine(GetTextForDistanceOutput(4, "A", "E", "B", "C", "D"));
            allQuestionsText.AppendLine(GetTextForDistanceOutput(5, "A", "E", "D"));
            allQuestionsText.AppendLine(GetTextForStopsNumberOutput(6, 1, 3, "C", "C"));
            allQuestionsText.AppendLine(GetTextForStopsNumberOutput(7, 4, 4, "A", "C"));
            allQuestionsText.AppendLine(GetTextForShortestRoute(8, "A","C"));
            allQuestionsText.AppendLine(GetTextForShortestRoute(9, "B", "B"));
            allQuestionsText.AppendLine(GetTextForOutput10());

            Console.Write(allQuestionsText.ToString());
            Console.ReadKey();
        }

        private static string GetTextForDistanceOutput(int questionNumber, params string[] citiesNames)
        {
            var result = GetResourceText("DistanceRoutesResponse"); 
            try
            {
                var route = RouteApplicationFacade.GetRouteByCityNames(citiesNames);
                result = string.Format(result, questionNumber, route, route.Distance);
            }
            catch (NoSuchRouteException exception)
            {
                result = string.Format(
                    result, questionNumber, citiesNames.Select(t => t).Aggregate((c, n) => c + n), exception.Message
                );
            }
            return result;
        }

        private static string GetTextForStopsNumberOutput(
            int questionNumber, int minStops, int maxStops, params string[] citiesNames
        )
        {
            var routes = RouteApplicationFacade.GetRoutesByDestinationAndStops(citiesNames[0], citiesNames[1], minStops, maxStops);
            var routesGenerated = GetGeneratedRoutesText(routes);
            var responsePart = minStops == maxStops ? GetResourceText("ExactlyStops") : GetResourceText("MaxStops");
            responsePart = string.Format(responsePart, maxStops);

            return string.Format(GetResourceText("StopQuestionResponse"), questionNumber, citiesNames[0],
                citiesNames[1], responsePart, routes.Count(), routesGenerated
            );
        }

        private static string GetTextForShortestRoute(int questionNumber, params string[] citiesNames)
        {
            var shortestRoute = RouteApplicationFacade.GetShortestRouteByDestination(citiesNames[0], citiesNames[1]);
            return string.Format(GetResourceText("ShortestRouteQuestionResponse"), questionNumber,
                citiesNames[0], citiesNames[1], shortestRoute.Distance, shortestRoute
            );
        }

        private static string GetTextForOutput10()
        {
            var routes = RouteApplicationFacade.GetRoutesByDestinationAndDistance("C", "C", 1, 30);
            var routesGenerated = GetGeneratedRoutesText(routes);
            var message = string.Format(GetResourceText("Question10Notes"), routes.Count());
            return string.Format(GetResourceText("Question10Response"), routes.Count(), routesGenerated, message);
        }

        private static string GetResourceText(string name)
        {
            return AppResource.ResourceManager.GetString(name);
        }

        private static string GetGeneratedRoutesText(IEnumerable<Route> routes)
        {
            return routes.Aggregate(
                GetResourceText("GeneratedRotes"), (current, route) => current + string.Format("[{0}] ", route)
            );
        }

        private static void InitializeConsole()
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(140,50);
        }
    }

    
}
