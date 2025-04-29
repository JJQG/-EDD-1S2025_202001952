using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

public class graficar
{
    public void graficosSimples(Blockchain lista)
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph ListaSimplementeEnlazada{");
        dot.AppendLine("  rankdir=LR;"); 
            
            Block actual = lista.cabeza;
            int nodoId = 0;

            while (actual != null)
            {
            dot.AppendLine($"  node{nodoId} [label=\"{"INDEX: "+actual.Index}\n{"Time Stamp: "+actual.Timestamp}\n{"DATA: {ID: "+actual.ID+", Nombre: "+actual.Nombres+", Correo: "+actual.Correo+", Edad: "+actual.Edad+"}"} \n{"Nonce: "+actual.Nonce}\n{"Prev Hash: "+actual.ultimoHash}\n{"HASH: "+actual.Hash}\" shape = box];");
            
            if (actual.siguiente != null)
            {
                dot.AppendLine($"  node{nodoId} -> node{nodoId + 1};");
            }
            
            actual = actual.siguiente;
            nodoId++;
        }

            dot.AppendLine("}");
        

        archivoGrafica("REPORTES/lista_Simple.dot", "REPORTES/lista_Simple.svg", dot.ToString());
    }


    public void graficosDobles(ListaDoble lista)
    {
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph ListaDoblementeEnlazada {");
        dot.AppendLine("  rankdir=LR;");  

        NodoD actual = lista.cabeza;
        int nodoId = 0;
        
        while (actual != null)
        {
            dot.AppendLine($"  node{nodoId} [label=\"{"ID :"+actual.ID}\n{"Usuario ID: "+ actual.IdUsuario}\n{"Marca: "+actual.Marca}\n{"Modelo: "+actual.Modelo}\n{"Placa: "+actual.Placa}\" shape = box];");
            
            if (actual.Siguiente != null)
            {
                dot.AppendLine($"  node{nodoId} -> node{nodoId + 1};");
            }
            if (actual.Anterior != null)
            {
                dot.AppendLine($"  node{nodoId} -> node{nodoId - 1} ;");
            }
            actual = actual.Siguiente;
            nodoId++;
        }

        dot.AppendLine("}");

        archivoGrafica("REPORTES/lista_doble.dot", "REPORTES/lista_doble.svg", dot.ToString());
       
    }


    public void graficosAVL(ArbolAVL arbol)
{
    StringBuilder dot = new StringBuilder();
    dot.AppendLine("digraph ArbolAVL {");
    dot.AppendLine("  rankdir=TB;");  

    GenerarDotArbolAVL(arbol.raiz, ref dot);

    dot.AppendLine("}");

    archivoGrafica("REPORTES/arbol_avl.dot", "REPORTES/arbol_avl.svg", dot.ToString());
}
//recursividad
private void GenerarDotArbolAVL(NodoAVL nodo, ref StringBuilder dot)
{
    if (nodo == null)
        return;

  
    dot.AppendLine($"  {nodo.ID} [label=\"ID: {nodo.ID}\\nRepuesto: {nodo.repuesto}\\nCosto: {nodo.costo}\" shape=box];");

    // conectar nodo izquierdo
    if (nodo.Izquierdo != null)
    {
        dot.AppendLine($"  {nodo.ID} -> {nodo.Izquierdo.ID};");
        GenerarDotArbolAVL(nodo.Izquierdo, ref dot);
    }

    // conectar nodo derecho
    if (nodo.Derecho != null)
    {
        dot.AppendLine($"  {nodo.ID} -> {nodo.Derecho.ID};");
        GenerarDotArbolAVL(nodo.Derecho, ref dot);
    }
}

public void graficosABB(ArbolBinarioBusqueda arbol)
{
    StringBuilder dot = new StringBuilder();
    dot.AppendLine("digraph G {");
    dot.AppendLine("  rankdir=TB;");  

    GenerarDOTArbolBB(arbol.raiz, ref dot);

    dot.AppendLine("}");
     archivoGrafica("REPORTES/arbol_abb.dot", "REPORTES/arbol_abb.svg", dot.ToString());
}

private void GenerarDOTArbolBB(NodoBB nodo, ref StringBuilder dot)
{
    if (nodo == null)
        return;

    dot.AppendLine($"  {nodo.ID} [label=\"ID: {nodo.ID}\\n Vehiculo: {nodo.IdVehiculo} || Repuesto: {nodo.IdRepuesto} \\n  {nodo.Detalles}\\n Costos: {nodo.Costo}\"shape=box];");

    if (nodo.Izquierda != null)
    {
        dot.AppendLine($"    {nodo.ID} -> {nodo.Izquierda.ID};");
        GenerarDOTArbolBB(nodo.Izquierda, ref dot);
    }

    if (nodo.Derecha != null)
    {
        dot.AppendLine($"    {nodo.ID} -> {nodo.Derecha.ID};");
         GenerarDOTArbolBB(nodo.Derecha, ref dot);
    }

    
   
}

/* public void graficosAB(ArbolB arbol)
    {

        
        StringBuilder dot = new StringBuilder();
        dot.AppendLine("digraph G {");
        dot.AppendLine("node [shape=record];");

         GenerarDOTArbolB(arbol.raiz, ref dot);

        dot.AppendLine("}");
        archivoGrafica("REPORTES/arbol_ab.dot", "REPORTES/arbol_ab.svg", dot.ToString());
    }

   private void  GenerarDOTArbolB(NodoB nodo, ref StringBuilder dot)
    {
        string nodoID = nodo.GetHashCode().ToString();

        dot.AppendLine($"{nodoID} [label=\"");

        for (int i = 0; i < nodo.Claves.Count; i++)
        {
            if (i > 0)
                dot.Append("|");
            dot.Append($"<f{i}> |Id: {nodo.Claves[i].Id}, |Id Servicio: {nodo.Claves[i].IdServicio}, Total: {nodo.Claves[i].Total}|");
        }
        //dot.AppendLine($"{nodo.Claves[1].Id} [ label=\" ID Servicio: {nodo.Claves[1].IdServicio} \\n Total: {nodo.Claves[1].Total}];");

         if (nodo.Claves.Count > 0)
            dot.Append($"<f{nodo.Claves.Count}>");
            dot.Append("\"];");

        if (!nodo.EsHoja)
        {
            for (int i = 0; i < nodo.Hijos.Count; i++)
            {
                string hijoID = nodo.Hijos[i].GetHashCode().ToString();
                dot.AppendLine($"{nodoID} -> {hijoID};");

                GenerarDOTArbolB(nodo.Hijos[i], ref dot);
            }
        }
    }*/

    public void archivoGrafica(string ruta, string guardar, string contenido){

        File.WriteAllText(ruta, contenido);

        
        var process = new Process();
        process.StartInfo.FileName = "dot";
        process.StartInfo.Arguments = $"-Tsvg {ruta} -o {guardar}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        string output = process.StandardOutput.ReadToEnd();
        string errors = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (!string.IsNullOrEmpty(errors))
        {
            Console.WriteLine(errors);
        }
        else
        {
            Console.WriteLine("Cargado");
        }
    }


    
}

    

