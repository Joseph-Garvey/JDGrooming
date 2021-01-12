using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JDGrooming.Classes
{
    public static class Verification
    {
        /// <summary>
        /// Verifies if the supplied text is a valid (format) email.
        /// </summary>
        /// <param name="email">Email to test</param>
        /// <returns>True if format is valid.</returns>
        public static bool VerifyEmail(String email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return (address.Address == email) && (email.Length <= 255);
            }
            catch
            {
                return false;
            }
        }

        public static bool VerifyPostcode(String postcode)
        {
            if(postcode.Length > 8) { return false; } // while this Regex works ^[A-Z]{1,2}[0-9][A-Z0-9]? ?[0-9][A-Z]{2}$ it does not cover special cases, see https://en.wikipedia.org/wiki/Postcodes_in_the_United_Kingdom#Validation
            // ^(([A-Z]{1,2}[0-9][A-Z0-9]?|ASCN|STHL|TDCU|BBND|[BFS]IQQ|PCRN|TKCA) ?[0-9][A-Z]{2}|BFPO ?[0-9]{1,4}|(KY[0-9]|MSR|VG|AI)[ -]?[0-9]{4}|[A-Z]{2} ?[0-9]{2}|GE ?CX|GIR ?0A{2}|SAN ?TA1)$
            return ApplyRegex(postcode, @"^([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([AZa-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9]?[A-Za-z])))) [0-9][A-Za-z]{2})$");
        }

        public static bool VerifyPhoneNumber(String number)
        {
            if(number.Length > 32) { return false; }
            return ApplyRegex(number, @"^(((\+44\s?\d{4}|\(?0\d{4}\)?)\s?\d{3}\s?\d{3})|((\+44\s?\d{3}|\(?0\d{3}\)?)\s?\d{3}\s?\d{4})|((\+44\s?\d{2}|\(?0\d{2}\)?)\s?\d{4}\s?\d{4}))(\s?\#(\d{4}|\d{3}))?$");
        }

        private static bool ApplyRegex(String test, String regexstring)
        {
            Regex regex = new Regex(regexstring);
            if (regex.Match(test).Success) { return true; }
            return false;
        }
    }
}
