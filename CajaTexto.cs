using System;
using Gtk;

class CajaTexto:Entry
{
    public int x=0;
    public int y=0;
public CajaTexto(int ancho, int alto, int x, int y){
    this.x=x;
        this.y =y;
        this.SetSizeRequest(ancho,alto);

}

}