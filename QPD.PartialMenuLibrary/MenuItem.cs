using System;
using System.Collections.Generic;

namespace QPD.PartialMenuLibrary
{
    public class MenuItem
    {
        public string Title { get; set; }
        public string AspAreaName { get; set; } = string.Empty;
        public string AspControllerName { get; set; }
        public string AspActionName { get; set; }

        public MenuItem() { }

        public MenuItem(string title, string controllerName, string actionName)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
        }

        public MenuItem(string title, string controllerName, string actionName, string areaName)
        {
            Title = title;
            AspControllerName = controllerName;
            AspActionName = actionName;
            AspAreaName = areaName;
        }
    }
}
