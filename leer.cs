using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gtk;


public class Leer
{
    string archivoSeleccionado = ""; 
    public string leer()
    {try
    {
    

        FileChooserDialog fileChooser = new FileChooserDialog(
            "Selecciona un archivo JSON", null,FileChooserAction.Open, "Cancelar", ResponseType.Cancel,"Abrir", ResponseType.Accept);

       
        fileChooser.Filter = new FileFilter();
        fileChooser.Filter.AddPattern("*.json");

        // Ejecutamos el diálogo
        if (fileChooser.Run() == (int)ResponseType.Accept)
        {
            // Obtener la ruta del archivo seleccionado
            archivoSeleccionado = fileChooser.Filename;
            
        }

        // Cerrar el diálogo
        fileChooser.Destroy();
        fileChooser=null;
   
    }
    catch (Exception ex)
    {
    Console.WriteLine($"Error: {ex.Message}");
    
    }

        return archivoSeleccionado;

    }


    
    
     
}