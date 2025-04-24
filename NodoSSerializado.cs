using System;

public class NodoSSerializado
{
    public int ID { get; set; }
    public string Nombres { get; set; }
    public string Apellidos { get; set; }
    public string Correo { get; set; }
    public int Edad { get; set; }
    public string Contrasenia { get; set; }
    public NodoSSerializado(int ID, string Nombres, string Apellidos, string Correo, int Edad, string Contrasenia)
    {
        this.ID = ID;
        this.Nombres = Nombres;
        this.Apellidos = Apellidos;
        this.Correo = Correo;
        this.Edad = Edad;
        this.Contrasenia = Contrasenia;
    }
}