using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json; 

public class Facturas{

    public int Id {get; set;}
    public int IdServicio {get; set;}
    public double Total {get; set;}
    public string metodo {get; set;}
    public string fecha {get; set;}
    public Facturas(int Id, int IdServicio, double Total, string metodo){
        this.Id = Id;
        this.IdServicio = IdServicio;
        this.Total = Total;
        this.metodo = metodo;
        fecha = "";

    }

     public string getHash()
        {
            string data = JsonConvert.SerializeObject(this); 
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
}