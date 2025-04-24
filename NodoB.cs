using System;

public class NodoB
{
    public List<Facturas> Claves;
    public List<NodoB> Hijos;
    public bool EsHoja ;

    public const int ORDEN = 5;
    public const int MAX_CLAVES = ORDEN - 1;
    public const int MIN_CLAVES = (ORDEN / 2) - 1;

    public NodoB()
    {
        Claves = new List<Facturas>();
        Hijos = new List<NodoB>();
     EsHoja = true;
    }

    public bool lleno()
    {
        return Claves.Count >= MAX_CLAVES;
    }

    public bool MinimoClaves()
    {
        return Claves.Count >= MIN_CLAVES;
    }
}