using System;
using Gtk;

public class ComboBox : ComboBoxText
{

    public int x=0;
    public int y =0;
    public ComboBox(string op1, string op2, string op3, int x, int y, int ancho, int alto){

        this.x = x;
        this.y = y;
        this.AppendText(op1);
        this.AppendText(op2);
        this.AppendText(op3);
        this.SetSizeRequest(ancho,alto);
    }

}