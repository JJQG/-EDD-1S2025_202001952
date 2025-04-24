using System;
using System.Data.Common;
using System.Text.Json;

public class ArbolBinarioBusqueda
{
    public  NodoBB raiz;
    public string texto = "";
    public List<NodoBBSerializado> listaSerializable = new List<NodoBBSerializado>();

    public ArbolBinarioBusqueda()
    {
        raiz = null;
    }

    public void Insertar(int id, int IdRepuesto, int idVehiculo, string detalles, double costo)
    {
        raiz = InsertarRecursivo(raiz, id, IdRepuesto, idVehiculo, detalles, costo);
    }

    private NodoBB InsertarRecursivo(NodoBB raiz, int id, int IdRepuesto, int idVehiculo, string detalles, double costo)
    {
        if (raiz == null)
        {
            raiz = new NodoBB(id, IdRepuesto, idVehiculo, detalles, costo);
            return raiz;
        }

        if (id < raiz.ID)
            raiz.Izquierda = InsertarRecursivo(raiz.Izquierda, id, IdRepuesto, idVehiculo, detalles, costo);
        else if (id > raiz.ID)
            raiz.Derecha = InsertarRecursivo(raiz.Derecha, id, IdRepuesto, idVehiculo, detalles, costo);

        return raiz;
    }

    public List<NodoBBSerializado> getLista()
    {
        //List<NodoAVLSerializado> listaSerializable = new List<NodoAVLSerializado>();
        listaSerializable.Clear();
        RecorridoInOrden(raiz);
        return listaSerializable;
    }

    public void GuardarEnArchivoJson(string rutaArchivo)
    {
        List<NodoBBSerializado> listaSerializable = getLista();
        string jsonString = JsonSerializer.Serialize(listaSerializable);
        File.WriteAllText(rutaArchivo, jsonString);
    }

    public bool Buscar(int valor)
    {
        return BuscarRecursivo(raiz, valor);
    }

    private bool BuscarRecursivo(NodoBB raiz, int valor)
    {
        if (raiz == null)
            return false;

        if (valor == raiz.ID)
            return true;

        if (valor < raiz.ID)
            return BuscarRecursivo(raiz.Izquierda, valor);

        return BuscarRecursivo(raiz.Derecha, valor);
    }

    ///////////////////////////////////IN///////////////////////////////////////////////////
    public void RecorridoInOrden(NodoBB nodo)
    {
        if (nodo != null)
        {
            RecorridoInOrden(nodo.Izquierda);
            texto += $"{nodo.ID}\t{nodo.IdVehiculo}\t{nodo.IdRepuesto}\t{nodo.Detalles}\t{nodo.Costo}\n";
            RecorridoInOrden(nodo.Derecha);
        }
    }

/////////////////////////////////PRE/////////////////////////////////////////////////////
    public void RecorridoPreOrden(NodoBB nodo)
    {
        if (nodo != null)
        {
            texto += $"{nodo.ID}\t{nodo.IdVehiculo}\t{nodo.IdRepuesto}\t{nodo.Detalles}\t{nodo.Costo}\n";
            RecorridoPreOrden(nodo.Izquierda);
            RecorridoPreOrden(nodo.Derecha);
        }
    }

    

///////////////////////////////////POST///////////////////////////////////////////////////
    public void RecorridoPostOrden(NodoBB nodo )
    {
        if (nodo != null)
        {
            RecorridoPostOrden(nodo.Izquierda);
            RecorridoPostOrden(nodo.Derecha );
             texto += $"{nodo.ID}\t{nodo.IdVehiculo}\t{nodo.IdRepuesto}\t{nodo.Detalles}\t{nodo.Costo}\n";
            }
         
    }

   

    public void CargarDesdeArchivoJson(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            string jsonString = File.ReadAllText(rutaArchivo);
            List<NodoBBSerializado> servicios = JsonSerializer.Deserialize<List<NodoBBSerializado>>(jsonString);

            foreach (var servicio in servicios)
            {
                Insertar(servicio.ID, servicio.IdVehiculo, servicio.IdRepuesto, servicio.Detalles, servicio.Costo);
            }
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }

    


}
