using System;

public class NodoAVLSerializado
{
    public int ID { get; set; }
    public string Repuesto { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }

    public NodoAVLSerializado(int ID, string Repuesto, string Detalles, double Costo)
    {
        this.ID = ID;
        this.Repuesto = Repuesto;
        this.Detalles = Detalles;
        this.Costo = Costo;
    }
}