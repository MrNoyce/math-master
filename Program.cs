using System;
using System.IO;
using System.Linq;

namespace dev275x.studentlist
{
    class Program
    {
        // The Main method 
        static void Main(string[] args)
        {
            /* Check arguments */
            if (args == null || args.Length != 1)
            {
                ShowUsage();
                return; // Exit early.
            }

            // Every operation requires us to load the student list.
            var studentList = LoadStudentList(Constants.StudentList);
            
            //TODO: Handle Case when Student list is empty
            if (args[0] == Constants.ShowAll) 
            {

                //TODO: This action occurs multiple times. Consider refactoring into a method.
                var students = studentList.Split(Constants.StudentEntryDelimiter);
                foreach(var student in students) 
                {
                    Console.WriteLine(student);
                }
            }
            else if (args[0]== Constants.ShowRandom)
            {
                var words = studentList.Split(Constants.StudentEntryDelimiter);
                var rand = new Random();
                var randomIndex = rand.Next(0,words.Length);
                Console.WriteLine(words[randomIndex]);
            }
            else if (args[0].StartsWith(Constants.AddEntry))
            {
                var newEntry = args[0].Substring(1);

                // Write
                // But we're in trouble if there are ever duplicates entered
                UpdateStudentList(studentList + Constants.StudentEntryDelimiter + newEntry, Constants.StudentList);
            }
            else if (args[0].StartsWith(Constants.FindEntry))
            {
                var students = studentList.Split(Constants.StudentEntryDelimiter);
                var searchTerm = args[0].Substring(1);

                //Using the 'Any' LINQ method to return whether or not
                // any item matches the given predicate.
                if (students.Any(s => s.Trim() == searchTerm))
                {
                    System.Console.WriteLine($"Entry '{searchTerm}' found");
                }
                else
                {
                    System.Console.WriteLine($"Entry '{searchTerm}' does not exist");
                }

            }
            else if (args[0].Contains(Constants.ShowCount))
            {
                var words = studentList.Split(Constants.StudentEntryDelimiter);
                Console.WriteLine(String.Format("{0} words found", words.Length));
            }
            else
            {
                ShowUsage();
            }
        }

        // Reads data from the given file. 
        static string LoadStudentList(string fileName)
        {

            // The 'using' construct does the heavy lifting of flushing a stream
            // and releasing system resources the stream was using.
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var reader = new StreamReader(fileStream))
            {

                // The format of our student list is that it is two lines.
                // The first line is a comma-separated list of student names. 
                // The second line is a timestamp. 
                // Let's just retrieve the first line, which is the student names. 
                return reader.ReadLine();
            }
        }

        // Writes the given string of data to the file with the given file name.
        //This method also adds a timestamp to the end of the file. 
        static void UpdateStudentList(string content, string fileName)
        {
            var timestamp = String.Format("List last updated on {0}", DateTime.Now);

            // The 'using' construct does the heavy lifting of flushing a stream
            // and releasing system resources the stream was using.
            using (var fileStream = new FileStream(fileName,FileMode.Open))
            using (var writer = new StreamWriter(fileStream))
            {
                writer.WriteLine(content);
                writer.WriteLine(timestamp);
            }
        }

        static void ShowUsage()
        {
            Console.WriteLine("Usage: dotnet dev275x.students.dll (-a | -r | -c | +WORD | ?WORD)");
        }
    }
}