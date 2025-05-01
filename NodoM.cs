using System;
using System.Security.Cryptography;
using System.Text;

public class NodoM
{
    public string Hash;
        public NodoM Izquierdo;
        public NodoM Derecho;

        public Facturas f;

        public NodoM(Facturas f)
        {
            this.f = f;
            Hash = f.getHash();
            Izquierdo = null;
            Derecho = null;
        }

       public NodoM(NodoM left, NodoM right)
        {
            f = null;
            Izquierdo = left;
            Derecho = right;
            Hash = setHash(left.Hash, right?.Hash);

        }

    private string setHash(string leftHash, string rightHash){

            string combined = leftHash + (rightHash ?? leftHash);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }
    

 
}