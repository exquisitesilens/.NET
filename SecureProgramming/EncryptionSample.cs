using System.Text;
using System.Security.Cryptography;
using System.IO;
using System;
using System.Windows.Forms;

namespace EncryptionSample
{
    class Algorithms
    {
        public static byte[] EncryptAES(byte[] plaintext, string password, byte[] salt, byte[] initVector)
        {
            int keySize = 16; // = 128 bit
            MemoryStream stream = new MemoryStream();

            using (Aes aes = new AesManaged())
            {
                aes.Key = Algorithms.DeriveKeyFromPassword(password, keySize, salt);
                aes.IV = initVector;
                aes.Padding = PaddingMode.PKCS7;

                using (CryptoStream cs = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(plaintext, 0, plaintext.Length);
                }
            }
            return stream.ToArray();
        }

        public static byte[] DecryptAES(byte[] cryptotext, string password, byte[] salt, byte[] initVector)
        {
            int keySize = 16; // = 128 bit
            MemoryStream stream = new MemoryStream();

            using (Aes aes = new AesManaged())
            {
                aes.Key = Algorithms.DeriveKeyFromPassword(password, keySize, salt);
                aes.IV = initVector;
                aes.Padding = PaddingMode.PKCS7;

                using (CryptoStream cs = new CryptoStream(stream, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cryptotext, 0, cryptotext.Length);
                    cs.FlushFinalBlock();
                }
            }
            return stream.ToArray();
        }

        public static byte[] HashSHA(byte[] inputData)
        {
            return new SHA512Managed().ComputeHash(inputData);
        }

        public static byte[] DeriveKeyFromPassword(string password, int keySize, byte[] salt)
        {
            int iterationCount = 1000;
            Rfc2898DeriveBytes keyDerivator = new Rfc2898DeriveBytes(password, salt, iterationCount);
            return keyDerivator.GetBytes(keySize);
        }

        public static byte[] GetRandomBytes(int blockSize)
        {
            RNGCryptoServiceProvider randomNumberGenerator = new RNGCryptoServiceProvider();
            byte[] buf = new byte[blockSize];
            randomNumberGenerator.GetBytes(buf);
            return buf;
        }

        public static string GetHexValue(byte[] input)
        {
            StringBuilder hexString = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                hexString.Append(input[i].ToString("X2"));
            }
            return hexString.ToString();
        }
    }
}


namespace EncryptionSample
{
    public partial class Cryptobox : Form
    {
        private UnicodeEncoding encoding;

        public Cryptobox()
        {
            InitializeComponent();
            encoding = new UnicodeEncoding();
            currentAlgo.Text = "AES";
        }

        private void startEncryption_Click(object sender, EventArgs e)
        {

            if (currentAlgo.Text.Equals("AES"))
                doAES();
            if (currentAlgo.Text.Equals("RSA"))
                doRSA();
            if (currentAlgo.Text.Equals("SHA"))
                doSHA();

        }

        private void doAES()
        {
            byte[] salt = Algorithms.GetRandomBytes(8);
            byte[] initVector = Algorithms.GetRandomBytes(16);

            byte[] result = Algorithms.EncryptAES(encoding.GetBytes(plaintext.Text), password.Text, salt, initVector);

            byte[] plainAgain = Algorithms.DecryptAES(result, password.Text, salt, initVector);
            string test = encoding.GetString(plainAgain);
            encrypted.Text = Algorithms.GetHexValue(result);
        }

        private void doRSA()
        {
        }

        private void doSHA()
        {
            byte[] hash = Algorithms.HashSHA(encoding.GetBytes(plaintext.Text));
            encrypted.Text = Algorithms.GetHexValue(hash);
        }

        private void usePasswordCB_CheckedChanged(object sender, EventArgs e)
        {
            password.ReadOnly = !usePasswordCB.Checked;
        }
    }

}