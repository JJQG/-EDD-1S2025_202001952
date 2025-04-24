using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
public class NodoD
{
    public int ID;
    public int IdUsuario;
    public string Marca;
    public int Modelo;
    public string Placa;
    public NodoD Siguiente;
    public NodoD Anterior;

    public NodoD(int ID, int IdUsuario, string Marca, int Modelo, string Placa)
    {

        this.ID = ID;
        this.IdUsuario = IdUsuario;
        this.Marca = Marca;
        this.Modelo = Modelo;
        this.Placa = Placa;
        Siguiente = null;
        Anterior = null;
    }
}

