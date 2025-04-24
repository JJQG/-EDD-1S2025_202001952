using System;

public class Nodo
{
    public int ID;  
    public string Nombres;
    public string Apellidos;
    public string Correo;
    public int Edad;
    public string Contrasenia;
    public Nodo Siguiente;
    public Nodo(int ID, string Nombres, string Apellidos, string Correo, int Edad, string Contrasenia)
    {
        this.ID = ID;
        this.Nombres = Nombres;
        this.Apellidos = Apellidos;
        this.Correo = Correo;
        this.Edad = Edad;
        this.Contrasenia = Contrasenia;
        Siguiente = null;
    }
}