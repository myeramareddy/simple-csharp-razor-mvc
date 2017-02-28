using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HelloEthos.Models
{
    public class FunStringsModel
    {
        [DisplayName("INPUT")]
        public string input { get; set; }

        [DisplayName("REVERSE WORDS")]
        public string reverseWordOrder { get; set; }

        [DisplayName("REVERSE CHARS IN WORD")]
        public string reverseCharOrder_w_wordOrder { get; set; }

        [DisplayName("ALPHA SORT")]
        public string alphaSortWords { get; set; }

        [DisplayName("SHA-384 ENCRYPTED")]
        public string sha384Encrypt { get; set; }

        public FunStringsModel(String fsInput, bool spaceCare)
        {
            /*if ((isStringBlank(fsInput))) { //taken care of using @data_val_required
                this.input = "";
                return;
            }*/
            this.input = fsInput;
            this.reverseWordOrder = reverseWordsInString(fsInput, spaceCare);
            this.reverseCharOrder_w_wordOrder = reverseCharOrderMaintainingWordOrder(fsInput);
            this.alphaSortWords = alphaSort(fsInput);
            this.sha384Encrypt = getSHA384Hash(fsInput, null);
        }


        private static bool isStringBlank(String input)
        {
            if (input == null || input.Trim().Equals(""))
            {
                return true;
            }
            else return false;
        }

        //http://www.obviex.com/samples/hash.aspx
        public static string getSHA384Hash(string plainText, byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash = new SHA384Managed();


            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        private static String alphaSort(String input)
        {
            StringBuilder sb = new StringBuilder();

            String[] words = input.Split(' ');
            Array.Sort(words); //merge/quick sort

            foreach (String s in words)
            {
                sb.Append(s).Append(" ");
            }

            return sb.ToString().Trim();
        }

        private static String reverseWordsInString(String input, bool spaceCare)
        {
            //Extend functionality - dont ignore white space chars (\n,\t, ' ')
            return reverseWordsIgnoreSpaces(input);
        }

        private static String reverseWordsIgnoreSpaces(String input)
        {
            StringBuilder sb = new StringBuilder();

            String[] words = input.Split(' ');
            for (int i = words.Length - 1; i >= 0; i--)
            {
                sb.Append(words[i]).Append(' ');
            }
            return sb.ToString().Trim();
        }

        private static String reverseCharOrderMaintainingWordOrder(String input)
        {
            StringBuilder sb = new StringBuilder();

            String[] words = input.Split(' ');
            foreach (var word in words)
            {
                String tmp = "";
                char[] charArr = word.ToCharArray();
                for (int i = charArr.Length - 1; i >= 0; i--)
                {
                    tmp += charArr[i];
                }
                sb.Append(tmp).Append(' ');
            }

            return sb.ToString().Trim();
        }

        //MERGE SORT
        /*public int[] mergeSort(int[] arr) {
            if (arr == null || arr.Length <= 1)
            {
                return arr;
            }

            int mid = arr.Length / 2;
            
            //extend to c# equivalent..
            int[] a = mergeSort(Arrays.copyOfRange(arr, 0, mid));
            int[] b = mergeSort(Arrays.copyOfRange(arr, mid, arr.length));  

            int[] x = merge(a, b);
            return x;
        }*/

        private int[] merge(int[] a, int[] b)
        {

            if (a.Length <= 0)
            {
                return b;
            }
            else if (b.Length <= 0)
            {
                return a;
            }

            int[] c = new int[a.Length + b.Length];
            int aP = 0; int bP = 0; int cP = 0;

            while (aP < a.Length && bP < b.Length)
            {
                if (a[aP] < b[bP])
                {
                    c[cP] = a[aP];
                    aP++;
                }
                else
                {
                    c[cP] = b[bP];
                    bP++;
                }
                cP++;
            }

            while (aP < a.Length)
            {
                c[cP] = a[aP];
                aP++; cP++;
            }

            while (bP < b.Length)
            {
                c[cP] = b[bP];
                bP++; cP++;
            }
            return c;
        }

    }
}