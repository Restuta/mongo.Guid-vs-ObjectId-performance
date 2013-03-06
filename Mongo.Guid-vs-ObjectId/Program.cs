using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Restuta.ConsoleExtensions.Colorfull;

namespace Mongo.Guid_vs_ObjectId
{
    internal delegate void Write(ColorfullString colorfullString);
    internal delegate void WriteLine(ColorfullString colorfullString);
        
    class Program
    {
        static readonly Write W = ColorfullConsole.Write;
        static readonly WriteLine WL = ColorfullConsole.WriteLine;

        static void Main(string[] args)
        {
            WL("test".Red());
        }
    }
}
