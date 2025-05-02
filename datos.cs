using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Gtk;
public class datos
{

   
     public Leer le = new Leer();
     public graficar g = new graficar();

    


    public void UsuariosMasivos(Blockchain l )
 {
        string filePath = le.leer();

        l.CargarDesdeArchivoJson(filePath);
        l.GuardarEnArchivoJson("archivosJson/listaSimple.json");
          //g.graficosSimples(l);
       
    
    }
    public void crearUsusario(Blockchain l ,int id, string nombre, string apellido, string correo, int edad,string contrasena){
        
        l.insertar(id, nombre,apellido,correo, edad, contrasena);
        l.GuardarEnArchivoJson("archivosJson/listaSimple.json");
        //g.graficosSimples(l);
        //l.Imprimir();
        //dotnet Console.WriteLine("hola");
    }



public void VehiculosMasivos(ListaDoble d)
 {
        string filePath = le.leer();

        d.CargarDesdeArchivoJson(filePath);
        d.GuardarArchivoComprimido("archivosJson/listaDoble.edd");
       // d.GuardarEnArchivoJson("archivosJson/listaDoble.json");
          //g.graficosDobles(d);
    }

    public void crearVehiculo(ListaDoble d ,int id, int IdUsuario, string marca, int modelo,string placa){
        
        d.Insertar(id, IdUsuario,marca, modelo, placa);
        d.GuardarArchivoComprimido("archivosJson/listaDoble.edd");
      //  d.GuardarEnArchivoJson("archivosJson/listaDoble.json");
        //g.graficosDobles(d);
        //l.Imprimir();
        //dotnet Console.WriteLine("hola");
    }

    

    public void RepuestoMasivos(ArbolAVL a)
 {
        string filePath = le.leer();

        a.CargarDesdeArchivoJson(filePath);
        a.GuardarArchivoComprimido("archivosJson/arbolAVL.edd");
       // a.GuardarEnArchivoJson("archivosJson/arbolAVL.json");
         // g.graficosAVL(a);
    }

    public void generarServicio(ArbolBinarioBusqueda bus ,int id, int IdRepuesto, int idVehiculo, string detalles, double costo){
        
        bus.Insertar( id,  IdRepuesto,  idVehiculo,  detalles,  costo);
        bus.GuardarEnArchivoJson("archivosJson/arbolBB.json");
        //g.graficosABB(bus);
    }

    public void generarFactura(ArbolMerkle b ,int id, int IdServicio, double Total, string metodo){
        
        b.Insertar( id,  IdServicio,  Total ,metodo);
        //g.graficosABB(b);
    }

    
}