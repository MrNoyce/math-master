namespace dev275x.studentlist
{
    static class Constants
    {
        /// Name of file containing list of students. 
        internal const string StudentList = "students.txt"; 

        /// Command line argument to show all students.
        internal const string ShowAll  = "-a";

        /// Command line argument to show a random student from the list.
        internal const string ShowRandom  = "-r";

        /// Command line argument to display a count of the number of students in the list.
        internal const string ShowCount = "-count";

        /// Command line argument to find a student in the list.
        internal const string FindEntry = "?";

        /// Command line argument to add an entry to the list. 
        internal const string AddEntry = "+";

        internal const char StudentEntryDelimiter = ',';

    }

}