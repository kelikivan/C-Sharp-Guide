using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    // just stores a couple of journal entries and ways of
    // working with them
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        public void AddEntry(string text)
        {
            entries.Add(text);
        }

        public void RemoveEntry(int index)
        {
            if (index < entries.Count && index >= 0)
                entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }

        #region // breaks single responsibility principle
        public void Save(string filename, bool overwrite = false)
        {
            File.WriteAllText(filename, ToString());
        }
        public void Load(string filename)
        {
        }
        public void Load(Uri uri)
        {
        }
        #endregion
    }

    // handles the responsibility of persisting objects
    public class PersistenceManager
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite && File.Exists(filename))
                File.WriteAllText(filename, journal.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("I cried today.");
            j.AddEntry("I ate a bug.");
            Console.WriteLine(j);

            var p = new PersistenceManager();
            var filename = @"C:\Users\kelik\Documents\journal.txt";
            p.SaveToFile(j, filename, true);

            var psi = new ProcessStartInfo
            {
                FileName = filename,
                UseShellExecute = true
            };
            Process.Start(psi);
        }
    }
}