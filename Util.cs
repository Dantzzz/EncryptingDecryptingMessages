using System;
using System.Collections.Generic;
using System.Text;

namespace EncryptingDecryptingMessages
{
    class Util
    {
        public static string GetPlainText()
        {
            Console.WriteLine("Enter plain text: ");
            return Console.ReadLine();
        }

        public static string GetSingleKey()
        {
            try 
            {
                Console.WriteLine("Enter a value for single key: ");
                char single_keyEntry = Char.Parse(Console.ReadLine());
                return single_keyEntry.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Try again... Please enter one character: ");
                GetSingleKey();
                return null;
            }
        }

        public static string GetMultiKey()
        {
            Console.WriteLine("Enter a value for multi key: ");
            string multi_keyEntry = Console.ReadLine();
            return multi_keyEntry;
        }

        public static int[] Clean(string plain_text)
        {
            string upperPlainText = plain_text.ToUpper();
            char[] singleCharacters = upperPlainText.ToCharArray();
            List<int> asciiCharacters = new List<int>();

            for (int i = 0; i < singleCharacters.Length; i++)
            {
                int toCheck = (int)singleCharacters[i];
                if (toCheck >= 65 && toCheck <= 90) 
                {
                    asciiCharacters.Add(toCheck);
                }
            }

            return asciiCharacters.ToArray();
        }
        
        public static string SingleEnc(int[] clean_text, int[] clean_skey)
        {
            int movePlaces = clean_skey[0] - 64;
            string encryptedString = "";

            for (int i = 0; i < clean_text.Length; i++)
            {
                int newASCIILocation = clean_text[i] + movePlaces;
                char substring = (char)newASCIILocation;
                encryptedString += substring;
            }
            return encryptedString;
        }
        
        public static string MultiEnc(int[] clean_text, int[] clean_mkey)
        {
            string encryptedString = "";
            

            for (int i = 0; i < clean_text.Length; i++)
            {
                int keyValue = clean_mkey[i] - 64;
                int newASCIILocation = clean_text[i] + keyValue;
                char substring = (char)newASCIILocation;
                encryptedString += substring;
            }
            return encryptedString;
        }
        
        public static string ContiEnc(int[] clean_text, int[] clean_mkey)
        {
            throw new NotImplementedException();
        }

        public static string SingleDec(string enc_single, int[] clean_skey)
        {
            throw new NotImplementedException();
        }
        
        public static string MultiDec(string enc_multi, int[] clean_mkey)
        {
            throw new NotImplementedException();
        }
        
        public static string ContiDec(string enc_conti, int[] clean_mkey)
        {
            throw new NotImplementedException();
        }
    }
}
