using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTutorial
{
    public class FileManager
    {
        public readonly static object o = new object();
        public string ReadText()
        {
            String line;
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(@"D:\My C# Projects\New folder\ThreadTutorial\ThreadTutorial\test.txt");

                //Read the first line of text
                line = sr.ReadLine();

                string a = "";
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the lie to console window
                    a += line;
                    Console.WriteLine(line);
                    //Read the next line
                    line = sr.ReadLine();
                }

                //close the file
                sr.Close();                
                return a;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return "Exception: " + e.Message;
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }

        }

        public void WriteText(string a)
        {
            try
            {
                lock (o)
                {
                    string b = ReadText() + "/" + a;
                    //Pass the filepath and filename to the StreamWriter Constructor
                    StreamWriter sw = new StreamWriter(@"D:\My C# Projects\New folder\ThreadTutorial\ThreadTutorial\test.txt");

                   
                    //Write a line of text
                    sw.WriteLine(b);
                    //Close the file
                    sw.Close();
                    Thread.Sleep(3000);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Executing finally block.");
            }
        }
    }
}
