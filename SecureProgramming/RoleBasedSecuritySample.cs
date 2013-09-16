using System;
using System.Security.Principal;
using System.Security.Permissions;
using System.Security;

namespace RoleBasedSecuritySample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            AppDomain appdomain = AppDomain.CurrentDomain;
            appdomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            Console.WriteLine("Current user: " + identity.Name);

            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                Console.WriteLine("Do something the administrator is allowed to do.");
            }
            else
            {
                Console.WriteLine("Do nothing.");
            }

            try
            {
                DoSomething();
            }
            catch (SecurityException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        [PrincipalPermission(SecurityAction.Demand, Role = "Users")]
        public static void DoSomething()
        {
            Console.WriteLine("Do something...");
        }
    }
}
