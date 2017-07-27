using System;
using System.Reflection;
using System.Text.RegularExpressions;
using Unbroken.LaunchBox.Plugins;

namespace ToggleCompleted
{
    class PluginLoader : ISystemEventsPlugin
    {
        internal static void Remap()
        {
            AppDomain.CurrentDomain.AssemblyResolve += ResolveThirdPartyLibrary;
        }

        private static Assembly ResolveThirdPartyLibrary(object sender, ResolveEventArgs args)
        {
            // this is courtesy of Nielk1 and attempts to load differing versions of the not-exactly-standard refs to the bb exe and wpf dll

            if (Regex.IsMatch(args.Name, @"BigBox, Version=[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+, Culture=neutral, PublicKeyToken=97d6238f04304129"))
            {
                return Assembly.LoadFrom("BigBox.exe");
            }

            if (Regex.IsMatch(args.Name, @"Unbroken.LaunchBox.Wpf, Version=[0-9]+\.[0-9]+\.[0-9]+\.[0-9]+, Culture=neutral, PublicKeyToken=97d6238f04304129"))
            {
                return Assembly.LoadFrom("metadata/Unbroken.LaunchBox.Wpf.dll");
            }

            return null;
        }

        public void OnEventRaised(string eventType)
        {
            if (eventType == "PluginInitialized")
            {
                Remap();
            }
        }
    }
}
