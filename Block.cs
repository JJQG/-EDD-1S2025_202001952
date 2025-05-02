using System;
using System.Security.Cryptography;
using System.Text;

public class Block
{
    public int Index { get; set; }
    public int ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasenia { get; set; }

    public string Timestamp { get; set; }

    public int Nonce { get; set; }

    public string ultimoHash { get; set; }
    public string Hash { get; set; }

    public Block siguiente;
    public Block anterior;

    public Block(int Index,  int ID, string Nombres, string Apellidos, string Correo, int Edad, string Contrasenia)
    {
        this.Index = Index;
        this.ID = ID;
        this.Nombres = Nombres;
        this.Apellidos = Apellidos;
        this.Correo = Correo;
        this.Edad = Edad;
        this.Contrasenia = Contrasenia;

        Timestamp = DateTime.Now.ToString("dd-MM-yy::HH:mm:ss");

        ultimoHash="";
        
        Nonce = 0;

        Hash = setHash();
    }


    public string getSHA256(){
            Contrasenia = gSHA256(Contrasenia);
            string input = Index.ToString() + Timestamp + ID.ToString()+ Nombres+Apellidos+ Correo+ Edad.ToString()+ Contrasenia + Nonce.ToString() + ultimoHash;
       
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(input));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
  }

    private string setHash()
    {
        string hash = getSHA256();
        while (!hash.StartsWith("0000"))
        {
            Nonce++;
            hash = getSHA256();
        }
        return hash;
    }

    public static string gSHA256(string str){
            
            SHA256 sha256 = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
  }

  
}