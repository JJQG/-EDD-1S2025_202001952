using System;

public class Facturas{

    public int Id {get; set;}
    public int IdServicio {get; set;}
    public double Total {get; set;}
    public Facturas(int Id, int IdServicio, double Total){
        this.Id = Id;
        this.IdServicio = IdServicio;
        this.Total = Total;

    }
}