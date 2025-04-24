using System;
using Gtk;
public class Labels : Label
{
    public int x=0;
    public int y =0;

    public Labels(string texto, int x ,int y):base(texto)
    {
        this.x=x;
        this.y =y;
    }
}