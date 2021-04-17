using System;
using System.Collections.Generic;
using System.Linq;
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
                Console.WriteLine($"{ex} Try again... Please enter one character: ");
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
            int[] revolvingKey = new int[clean_text.Length];
            int counter = 0;
            string encryptedString = "";

            for (int i = 0; i < revolvingKey.Length; i++)
            {
                revolvingKey[i] = clean_mkey[counter];
                counter++;
                if(counter > clean_mkey.Length-1)
                {
                    counter = 0;
                }
            }

            for (int i = 0; i < revolvingKey.Length; i++)
            {
                int keyValue = revolvingKey[i] - 64;
                int newASCIILocation = clean_text[i] + keyValue;
                char substring = (char)newASCIILocation;
                encryptedString += substring;
            }
            return encryptedString;
        }
        
        public static string ContiEnc(int[] clean_text, int[] clean_mkey)
        {
            int[] contiKey = clean_mkey.Concat(clean_text).ToArray();
            string encryptedString = "";

            for (int i = 0; i < clean_text.Length; i++)
            {
                int keyValue = contiKey[i] - 64;
                int newASCIILocation = clean_text[i] + keyValue;
                if(newASCIILocation > 90)
                {
                    newASCIILocation = newASCIILocation - 26;
                }
                char substring = (char)newASCIILocation;
                encryptedString += substring;
            }
            return encryptedString;
        }

        public static string SingleDec(string enc_single, int[] clean_skey)
        {
           int[] asciiCharacters = Clean(enc_single);

           int movePlaces = clean_skey[0] - 64;
           string decryptedString = "";

           for (int i = 0; i < asciiCharacters.Length; i++)
           {
               int newASCIILocation = asciiCharacters[i] - movePlaces;
               char substring = (char)newASCIILocation;
               decryptedString += substring;
           }
           return decryptedString;
        }
        
        public static string MultiDec(string enc_multi, int[] clean_mkey)
        {
            int[] asciiCharacters = Clean(enc_multi);

            int[] revolvingKey = new int[asciiCharacters.Length];
            int counter = 0;
            for (int i = 0; i < revolvingKey.Length; i++)
            {
                revolvingKey[i] = clean_mkey[counter];
                counter++;
                if (counter > clean_mkey.Length - 1)
                {
                    counter = 0;
                }
            }

            string decryptedString = "";
            for (int i = 0; i < revolvingKey.Length; i++)
            {
                int keyValue = revolvingKey[i] - 64;
                int newASCIILocation = enc_multi[i] - keyValue;
                char substring = (char)newASCIILocation;
                decryptedString += substring;
            }
            return decryptedString;
        }
        
        public static string ContiDec(string enc_conti, int[] clean_mkey)
        {
            int[] encodedASCII = Clean(enc_conti);
            int[] key = new int[clean_mkey.Length];
            Array.Copy(clean_mkey, key, clean_mkey.Length);

            int[] keyBuilder = new int[clean_mkey.Length];
            string decryptedString = "";
            int counter = 0;

            while (decryptedString.Length < encodedASCII.Length)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if(counter < encodedASCII.Length)
                    {
                        keyBuilder[i] = encodedASCII[counter] - (key[i] - 64);
                        counter++;
                        if(keyBuilder[i] > 90)
                        {
                            keyBuilder[i] = keyBuilder[i] - 26;
                        }

                        if(keyBuilder[i] < 65)
                        {
                            keyBuilder[i] = keyBuilder[i] + 26;
                        }
                        char substring = (char)keyBuilder[i];
                        decryptedString += substring;
                    }

                }
                Array.Copy(keyBuilder, key, keyBuilder.Length);

            }
            return decryptedString;
        }
    }
}
