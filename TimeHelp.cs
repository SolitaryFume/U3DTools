using System;

namespace U3DTools
{
    public class TimeHelp
    {
        private static DateTime t = new DateTime(1970,1,1,0,0,0,0);

        public static long GetTimeStamp() 
        {
            TimeSpan ts = DateTime.UtcNow - t;
            return Convert.ToInt64(ts.TotalSeconds);
        }
    }
}