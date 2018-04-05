using System;
using System.Collections.Generic;

namespace QPD.PartialMenuLibrary
{
    public class MenuItem
    {
        public string Title { get; }
        public string AspAreaName { get; } = string.Empty;
        public string AspControllerName { get; }
        public string AspActionName { get; }
        public List<string> AspRouteElems { get; } = new List<string>();

        public MenuItem(string title, string controllerName, string actionName)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
        }

        public MenuItem(string title, string controllerName, string actionName, List<string> routeElems)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
            if (routeElems.Count > 0)
            {
                AspRouteElems.AddRange(routeElems);
            }
            else
                throw new ArgumentException("routeElems don`t have elements");
        }

        public MenuItem(string title, string controllerName, string actionName, string areaName)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
            AspAreaName = areaName;
        }

        public MenuItem(string title, string controllerName, string actionName, string areaName, List<string> routeElems)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
            AspAreaName = areaName;
            if (routeElems.Count > 0)
            {
                AspRouteElems.AddRange(routeElems);
            }
        }
    }
}
