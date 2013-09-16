using System;
using System.IO;
using System.IO.IsolatedStorage;

namespace IsolatedStorageSample
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            IsolatedStorageFile isoStore = IsolatedStorageFile.GetStore
                (IsolatedStorageScope.User | IsolatedStorageScope.Domain | IsolatedStorageScope.Assembly, null, null);
            isoStore.CreateDirectory("TopLevelDirectory");
            isoStore.CreateDirectory("TopLevelDirectory/SecondLevel");
            isoStore.CreateDirectory("AnotherTopLevelDirectory/InsideDirectory");
            IsolatedStorageFileStream isoStream1 =
                new IsolatedStorageFileStream("InTheRoot.txt", FileMode.Create, isoStore);
            Console.WriteLine("Created a new file in the root.");
            isoStream1.Close();
        }
    }
}