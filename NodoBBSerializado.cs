using System;

public class NodoBBSerializado{

    public int ID { get; set; }
    public int IdRepuesto { get; set; }
    public int IdVehiculo { get; set; }
    public string Detalles { get; set; }
    public double Costo { get; set; }

    public NodoBBSerializado(int ID, int IdRepuesto, int IdVehiculo, string Detalles, double Costo)
    {
        this.ID = ID;
        this.IdRepuesto = IdRepuesto;
        this.IdVehiculo = IdVehiculo;
        this.Detalles = Detalles;
        this.Costo = Costo;
    }
}
