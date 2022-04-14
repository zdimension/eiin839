using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace Shared
{
    public static class Logging
    {
        public static void Log(string message, 
            [CallerMemberName] string member = null!, 
            [CallerFilePath] string file = null!, 
            [CallerLineNumber] int line = 0)
        {
            Debug.WriteLine("[{0,22} : {1,2}] [{2,16}] {3}", Path.GetFileName(file), line, member, message);
        }
    }
}
