using System;

public class NodoAVL
{
    public int ID;
    public string repuesto;
    public string detalles;
    public double costo;
    public NodoAVL Izquierdo;
    public NodoAVL Derecho;
    public int Altura;

    public NodoAVL(int ID, string repuesto, string detalles, double costo)
    {
        this.ID = ID;
        this.repuesto = repuesto;
        this.detalles = detalles;
        this.costo = costo;
        Izquierdo = null;
        Derecho = null;
        Altura = 1;
    }
}