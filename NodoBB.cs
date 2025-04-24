using System;

public class NodoBB
{
    public int ID;
    public int IdRepuesto;
    public int IdVehiculo;
    public string Detalles;
    public double Costo;
    public NodoBB Izquierda;
    public NodoBB Derecha;

    public NodoBB(int ID, int IdRepuesto, int IdVehiculo, string Detalles, double Costo)
    {
        this.ID = ID;
        this.IdRepuesto = IdRepuesto;
        this.IdVehiculo = IdVehiculo;
        this.Detalles = Detalles;
        this.Costo = Costo;
        Izquierda = null;
        Derecha = null;
    }
}