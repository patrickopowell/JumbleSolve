using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JumbleSolve
{
    class JumbleSolve
    {

        public class Key
        {
            public string key;
            public int count;

            public Key(string str, int i)
            {
                key = str;
                count = i;
            }
        }

        private Dictionary<Key, string> dictionary;
        private int count = 0;

        public JumbleSolve()
        {
            dictionary = new Dictionary<Key, string>();

            GetDictionary();

            Run();
        }

        private void Run()
        {
            string orig = "";
            while (true)
            {

                Console.Write("Enter jumbled word: ");
                orig = Console.ReadLine();
                if (orig == "exit\t") break;

                char[] origStr = orig.ToCharArray();
                Array.Sort(origStr);

                orig = new string(origStr);

                foreach (KeyValuePair<Key, string> key in dictionary)
                {
                    if (getKey(key.Key) == orig)
                        Console.WriteLine("Found solution: " + key.Value);
                }
            }
            Console.WriteLine("Thank you for solving!");

            System.Threading.Thread.Sleep(2000);
        }

        private string getKey(Key key)
        {
            return key.key;
        }

        private void GetDictionary()
        {
            WebRequest req = WebRequest.Create("https://raw.githubusercontent.com/dwyl/english-words/master/words.txt");

            WebResponse resp = req.GetResponse();

            StreamReader dict = new StreamReader(resp.GetResponseStream());


            string line;

            while ((line = dict.ReadLine()) != null)
            {
                line = line.Trim();

                char[] str = line.ToCharArray();
                Array.Sort(str);
                string key = new string(str);

                Key insert = new Key(key, count++);

                if (!dictionary.ContainsKey(insert))
                    dictionary.Add(insert, line);
            }
        }

        static void Main(string[] args)
        {
            new JumbleSolve();
        }
    }
}
