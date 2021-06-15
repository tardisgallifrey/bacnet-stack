using System;
using System.Collections.Generic;
using System.Linq;

namespace BACnetProject
{
    public class TextThings
    {
        //This class based on code from:
        //https://stackoverflow.com/questions/33474706/c-sharp-remove-unwanted-characters-from-a-string

        // create a lookup hashset
        private static HashSet<char> _allowedChars = new HashSet<char>("0123456789abcdefghijklmnopqrstuvwxyz-.,".ToArray());

        //This uses Linq to strip out unwanted characters
        //If they aren't in the Hash Set created above, they aren't added to the output string.
        //Used when a bacnet property ready returns a formatted list
        public string FilterString(string str)
        {
            return new String(str.Where(ch => _allowedChars.Contains(ch)).ToArray());
        }
    }
}