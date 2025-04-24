using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;


public class ListaSimple
{
    public Nodo cabeza; 

    public ListaSimple()
    {
        cabeza = null;
    }

      public void Insertar(int id, string Nombre, string Apellido, string Correo, int edad, string contrasena)
    {
        if(Buscar(id, Correo)){

            Console.WriteLine("Ya existe este usuario");
        }else{
        Nodo nuevoNodo = new Nodo(id,  Nombre,  Apellido,  Correo,  edad,  contrasena);
        if (cabeza == null)
        {
            cabeza = nuevoNodo;
        }
        else
        {
            Nodo temp = cabeza;
            while (temp.Siguiente != null)
            {
                temp = temp.Siguiente;
            }
            temp.Siguiente = nuevoNodo; 
        }

        }
        
        
    }

    public void Mostrar()
    {
        if (cabeza == null)
        {
            Console.WriteLine("La lista está vacía");
            return;
        }

        Nodo temp = cabeza;
        while (temp != null)
        {
            Console.WriteLine(temp.ID + " - "+temp.Nombres);
            temp = temp.Siguiente;
        }
        Console.WriteLine();
    }

    public bool Buscar(int id, string Correo)
    {
        Nodo temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id || temp.Correo == Correo)
                return true;
            temp = temp.Siguiente;
        }
        return false;
    }

    public bool Ingresar(string nom, string contra)
    {
        Nodo temp = cabeza;
        while (temp != null)
        {
            if (temp.Correo == nom && temp.Contrasenia == contra)
                return true;
            temp = temp.Siguiente;
        }
        return false;
    }

    public Nodo BuscarNodo(int id)
    {
        Nodo temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id )
                return temp;
            temp = temp.Siguiente;
        }
        return null;
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
        Nodo temp = cabeza;
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

    public void Editar(int id, string Nombre, string Apellido, string Correo, int Edad, string Contrasena)
{
    Nodo nodo = BuscarNodo(id);
    
    if (nodo == null)
    {
        Console.WriteLine("No se encontró el nodo con el ID proporcionado.");
        return;
    }

    nodo.Nombres = Nombre;
    nodo.Apellidos = Apellido;
    nodo.Correo = Correo;
    nodo.Edad = Edad;
    nodo.Contrasenia = Contrasena;
    
    GuardarEnArchivoJson("archivosJson/listaSimple.json");
    Console.WriteLine("Nodo actualizado correctamente.");
}


    public List<NodoSSerializado> getLista()
    {
        List<NodoSSerializado> listaSerializable = new List<NodoSSerializado>();
        Nodo actual = cabeza;
        while (actual != null)
        {
            listaSerializable.Add(new NodoSSerializado(actual.ID, actual.Nombres, actual.Apellidos, actual.Correo, actual.Edad,actual.Contrasenia));
            actual = actual.Siguiente;
        }
        return listaSerializable;
    }

    public void GuardarEnArchivoJson(string rutaArchivo)
    {
        List<NodoSSerializado> listaSerializable = getLista();
        string jsonString = JsonSerializer.Serialize(listaSerializable);
        File.WriteAllText(rutaArchivo, jsonString);
    }

    public void CargarDesdeArchivoJson(string rutaArchivo)
    {
        if (File.Exists(rutaArchivo))
        {
            string jsonString = File.ReadAllText(rutaArchivo);
            List<NodoSSerializado> listaSerializable = JsonSerializer.Deserialize<List<NodoSSerializado>>(jsonString);
            foreach (var nodo in listaSerializable)
            {
                Insertar(nodo.ID, nodo.Nombres,  nodo.Apellidos,  nodo.Correo, nodo.Edad, nodo.Contrasenia);
            }
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }

    
}