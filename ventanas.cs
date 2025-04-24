using System;
using Gtk;
public class Ventana : Window
{
    public Ventana(string titulo, int ancho, int alto):base(titulo)
    {
        SetDefaultSize(ancho, alto);
        ShowAll();
    }

 

    
}