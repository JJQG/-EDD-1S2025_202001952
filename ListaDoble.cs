using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



public class ListaDoble
{
    public NodoD cabeza;
    public NodoD cola;

    public ListaDoble()
    {
        cabeza = null;
        cola = null;
    }



    public void Insertar(int id, int IdUsuario, string Marca, int Modelo, string Placa)
    {

        if(Buscar(id)){

            Console.WriteLine("Ya existe este usuario");
        }else{
        NodoD nuevoNodo = new NodoD(id , IdUsuario, Marca, Modelo,Placa);
        if (cola == null)
        {
            cabeza = nuevoNodo;
            cola = nuevoNodo;
        }
        else
        {
            cola.Siguiente = nuevoNodo;
            nuevoNodo.Anterior = cola;
            cola = nuevoNodo;
        }
        }
    }

    public void Eliminar(int id)
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía");
            return;
        }

        if (cabeza.ID == id)
        {
            cabeza = cabeza.Siguiente;  
            return;
        }
        NodoD temp = cabeza;
        while (temp.Siguiente != null && temp.Siguiente.ID != id)
        {
            temp = temp.Siguiente;
        }

        if (temp.Siguiente == null)
        {
            Console.WriteLine("El nodo con el valor {0} no se encuentra", id);
        }
        else
        {
            temp.Siguiente = temp.Siguiente.Siguiente; 
        }
    }
    public bool Buscar(int id)
    {
        NodoD temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id )
                return true;
            temp = temp.Siguiente;
        }
        return false;
    }

    public NodoD BuscarNodo(int id)
    {
        NodoD temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id )
                return temp;
            temp = temp.Siguiente;
        }
        return null;
    }

    public void Editar(int id, int IdUsuario, string Marca, int Modelo, string Placa)
{
    NodoD nodo = BuscarNodo(id);
    
    if (nodo == null)
    {
        Console.WriteLine("No se encontró el nodo con el ID proporcionado.");
        return;
    }

    nodo.IdUsuario = IdUsuario;
    nodo.Marca = Marca;
    nodo.Modelo = Modelo;
    nodo.Placa = Placa;
    GuardarEnArchivoJson("archivosJson/listaDoble.json");
    Console.WriteLine("Nodo actualizado correctamente.");
}

    public List<NodoDSerializado> getLista()
    {
        List<NodoDSerializado> listaSerializable = new List<NodoDSerializado>();
        NodoD actual = cabeza;
        while (actual != null)
        {
            listaSerializable.Add(new NodoDSerializado(actual.ID, actual.IdUsuario, actual.Marca, actual.Modelo, actual.Placa));
            actual = actual.Siguiente;
        }
        return listaSerializable;
    }

    public void GuardarEnArchivoJson(string rutaArchivo)
    {
        List<NodoDSerializado> listaSerializable = getLista();
        string jsonString = JsonSerializer.Serialize(listaSerializable);
        File.WriteAllText(rutaArchivo, jsonString);
    }

    public void CargarDesdeArchivoJson(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            string jsonString = File.ReadAllText(rutaArchivo);
            List<NodoDSerializado> listaSerializable = JsonSerializer.Deserialize<List<NodoDSerializado>>(jsonString);
            foreach (var nodo in listaSerializable)
            {
                Insertar(nodo.ID, nodo.ID_Usuario,  nodo.Marca,  nodo.Modelo,  nodo.Placa);
            }
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }

    public void Imprimir()
    {
        NodoD actual = cabeza;
        while (actual != null)
        {
            Console.WriteLine(actual.ID + " - "+actual.Marca);
            actual = actual.Siguiente;
        }
        Console.WriteLine();
    }

   
}


        
