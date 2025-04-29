using System;
using System.Security.Cryptography;
using System.Text;

public class Facturas{

    public int Id {get; set;}
    public int IdServicio {get; set;}
    public double Total {get; set;}
    public Facturas(int Id, int IdServicio, double Total){
        this.Id = Id;
        this.IdServicio = IdServicio;
        this.Total = Total;

    }

     public string GetHash()
        {
         //   string data = JsonConvert.SerializeObject(this); // Serializar la factura a JSON
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes("data"));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
}