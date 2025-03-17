using UnityEngine;

namespace AppRestriction.Models
{
    public class ApplicationInfo
    {
        public string Name { get; }
        public string ProcessName { get; }
        public Texture2D Icon { get; }
        public bool isSupressed { get; set; }

        public ApplicationInfo(string name, string processName, Texture2D icon)
        {
            Name = name;
            ProcessName = processName;
            Icon = icon;
        }
    }
}