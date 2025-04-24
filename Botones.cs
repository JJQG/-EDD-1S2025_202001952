using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gtk;
public class Botones : Button
{
    public int x=0;
    public int y =0;


    public Botones(string texto, int ancho, int alto, int x ,int y):base(texto)
    {
        this.x=x;
        this.y =y;
        this.SetSizeRequest(ancho,alto);
    }

    public void abrir(Ventana v){
        //v("texto",500,300); // Ahora MiVentana debería encontrarse
            v.Show();

    }

    

   

    



}