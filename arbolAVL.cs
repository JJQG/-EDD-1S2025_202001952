using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;



public class ArbolAVL
{
    public NodoAVL raiz;
    public string texto;
    public List<NodoAVLSerializado> listaSerializable = new List<NodoAVLSerializado>();

    public void Insertar(int id, string repuesto, string detalles, double costo)
    {
        if(Buscar(id)){
            Console.WriteLine("este servicio ya existe");
        }else{
            raiz = Insertar(raiz, id, repuesto, detalles, costo);
        }
        
    }

    public NodoAVL Insertar(NodoAVL nodo, int id, string repuesto, string detalles, double costo)
    {
        if (nodo == null)
        {
            return new NodoAVL(id, repuesto, detalles, costo);
        }

        if (id < nodo.ID)
        {
            nodo.Izquierdo = Insertar(nodo.Izquierdo, id, repuesto, detalles, costo);
        }
        else if (id > nodo.ID)
        {
            nodo.Derecho = Insertar(nodo.Derecho, id, repuesto, detalles, costo);
        }
        else
        {
            return nodo;
        }

        nodo.Altura = 1 + Math.Max(getAltura(nodo.Izquierdo), getAltura(nodo.Derecho));
        int balance = getBalance(nodo);

        // rotación a la izquierda
        if (balance > 1 && id < nodo.Izquierdo.ID)
        {
            return RotacionDerecha(nodo);
        }

        // rotación a la derecha
        if (balance < -1 && id > nodo.Derecho.ID)
        {
            return RotacionIzquierda(nodo);
        }
        // izquierda-derecha
        if (balance > 1 && id > nodo.Izquierdo.ID)
        {
            nodo.Izquierdo = RotacionIzquierda(nodo.Izquierdo);
            return RotacionDerecha(nodo);
        }
        // derecha-izquierda
        if (balance < -1 && id < nodo.Derecho.ID)
        {
            nodo.Derecho = RotacionDerecha(nodo.Derecho);
            return RotacionIzquierda(nodo);
        }

        return nodo;
    }

    private int getAltura(NodoAVL nodo)
    {
        return nodo == null ? 0 : nodo.Altura;
    }

    private int getBalance(NodoAVL nodo)
    {
        return nodo == null ? 0 : getAltura(nodo.Izquierdo) - getAltura(nodo.Derecho);
    }

    private NodoAVL RotacionDerecha(NodoAVL y)
    {
        NodoAVL x = y.Izquierdo;
        NodoAVL T2 = x.Derecho;

        x.Derecho = y;
        y.Izquierdo = T2;

        y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;
        x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;

        return x;
    }

    private NodoAVL RotacionIzquierda(NodoAVL x)
    {
        NodoAVL y = x.Derecho;
        NodoAVL T2 = y.Izquierdo;

        y.Izquierdo = x;
        x.Derecho = T2;

        x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;
        y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;

        return y;
    }

public Boolean Buscar(int id)
{
    if(BuscarNodo(raiz, id) != null){
        return true;

    }
    return false;
    
}
    public NodoAVL BuscarNodo(int id)
{
    return BuscarNodo(raiz, id);
}

private NodoAVL BuscarNodo(NodoAVL nodo, int id)
{
    if (nodo == null || nodo.ID == id)
    {
        return nodo;
    }

    if (id < nodo.ID)
    {
        return BuscarNodo(nodo.Izquierdo, id);
    }
    else
    {
        return BuscarNodo(nodo.Derecho, id);
    }
}

    public void Editar(int id, string nuevoRepuesto, string nuevosDetalles, double nuevoCosto)
{
    NodoAVL nodo = BuscarNodo(id);
    
    if (nodo == null)
    {
        Console.WriteLine("No se encontró el nodo con el ID proporcionado.");
        return;
    }

    nodo.repuesto = nuevoRepuesto;
    nodo.detalles = nuevosDetalles;
    nodo.costo = nuevoCosto;
    
    Console.WriteLine("Nodo actualizado correctamente.");
}

public List<NodoAVLSerializado> getLista()
    {
        //List<NodoAVLSerializado> listaSerializable = new List<NodoAVLSerializado>();
        listaSerializable.Clear();
        RecorridoInOrden(raiz);
        return listaSerializable;
    }

    public void GuardarEnArchivoJson(string rutaArchivo)
    {
        List<NodoAVLSerializado> listaSerializable = getLista();
        string jsonString = JsonSerializer.Serialize(listaSerializable);
        File.WriteAllText(rutaArchivo, jsonString);
    }

///////////////////////////////////IN///////////////////////////////////////////////////
    public void RecorridoInOrden(NodoAVL nodo)
    {
        if (nodo != null)
        {
            RecorridoInOrden(nodo.Izquierdo);
            texto += $"{nodo.ID}\t{nodo.repuesto}\t{nodo.detalles}\t{nodo.costo}\n";
            listaSerializable.Add(new NodoAVLSerializado(nodo.ID, nodo.repuesto, nodo.detalles, nodo.costo));
           RecorridoInOrden(nodo.Derecho);
        }
    }

/////////////////////////////////PRE/////////////////////////////////////////////////////
    public void RecorridoPreOrden(NodoAVL nodo)
    {
        if (nodo != null)
        {
            texto += $"{nodo.ID}\t{nodo.repuesto}\t{nodo.detalles}\t{nodo.costo}\n";
            RecorridoPreOrden(nodo.Izquierdo);
            RecorridoPreOrden(nodo.Derecho);
        }
    }

    

///////////////////////////////////POST///////////////////////////////////////////////////
    public void RecorridoPostOrden(NodoAVL nodo )
    {
        if (nodo != null)
        {
            RecorridoPostOrden(nodo.Izquierdo);
            RecorridoPostOrden(nodo.Derecho );
            texto += $"{nodo.ID}\t{nodo.repuesto}\t{nodo.detalles}\t{nodo.costo}\n";
            }
         
    }

//////////////////////////////////////////////////////////////////////////////////////



    public void CargarDesdeArchivoJson(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            string jsonString = File.ReadAllText(rutaArchivo);
            List<NodoAVLSerializado> repuestos = JsonSerializer.Deserialize<List<NodoAVLSerializado>>(jsonString);

            foreach (var repuesto in repuestos)
            {
                Insertar(repuesto.ID, repuesto.Repuesto, repuesto.Detalles, repuesto.Costo);
            }
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }
}
