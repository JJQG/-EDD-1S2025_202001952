using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class Blockchain
{
    public Block cabeza = null; 
    public Block cola; 
    public int Size = 0;

   
    public Block insertar(int ID, string Nombres, string Apellidos, string Correo, int Edad, string Contrasenia){

        Block newBlock = new Block(Size,  ID, Nombres, Apellidos, Correo, Edad, Contrasenia);

        if(cabeza == null)
        {
            newBlock.ultimoHash = "0000";
            cola = newBlock;
            cabeza = newBlock;
        }
        else 
        {
        newBlock.siguiente = cabeza;
        newBlock.ultimoHash = cola.Hash;
        cola = newBlock;
        cabeza = newBlock;
          
        }

        Size+=1;

        return newBlock;


    }

    public void imprimir()
    {
        Block temp = cabeza;

        while(temp != null)
        {
            Console.WriteLine("Index: " + temp.Index);
            Console.WriteLine("Nombre: " + temp.Nombres);
            Console.WriteLine("Contraseña: " + temp.Contrasenia);
            Console.WriteLine("PreviusHash: " + temp.ultimoHash);
            Console.WriteLine("Hash: " + temp.Hash);
            Console.WriteLine("---------------------");
            
            temp = temp.siguiente;
        }
    }

    public bool Buscar(int id)
    {
        Block temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id )
                return true;
            temp = temp.siguiente;
        }
        return false;
    }

    public bool Ingresar(string nom, string contra)
    {

      /*  Block usu = Buscar(nom);
            

            if(usu != null)
            {
                Console.WriteLine(usu.Contrasenia);
               
                string contrasena= Block.gSHA256(contra);
                Console.WriteLine(contrasena);

                if(contrasena == usu.Contrasenia)
                {
                     return true;
                }

            }*/
        Block temp = cabeza;
        while (temp != null)
        {
            if (temp.Correo == nom && temp.Contrasenia == contra)
                return true;
            temp = temp.siguiente;
        }
        return false;
    }

    public Block BuscarNodo(int id)
    {
        Block temp = cabeza;
        while (temp != null)
        {
            if (temp.ID == id )
                return temp;
            temp = temp.siguiente;
        }
        return null;
    }
    public List<NodoSSerializado> getLista()
    {
        List<NodoSSerializado> listaSerializable = new List<NodoSSerializado>();
        Block actual = cabeza;
        while (actual != null)
        {
            listaSerializable.Add(new NodoSSerializado(actual.ID, actual.Nombres, actual.Apellidos, actual.Correo, actual.Edad,actual.Contrasenia));
            actual = actual.siguiente;
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
                insertar(nodo.ID, nodo.Nombres,  nodo.Apellidos,  nodo.Correo, nodo.Edad, nodo.Contrasenia);
            }
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }

}

