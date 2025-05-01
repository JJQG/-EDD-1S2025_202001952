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



    public void graficosM(ArbolMerkle arbol) {

    StringBuilder dot = new StringBuilder();
    dot.AppendLine("digraph G {");
    dot.AppendLine("node [shape=record, style=filled, fillcolor=lightgray];");

    if (arbol.Root != null)
    {
        GenerarDOTMerkle(arbol.Root, ref dot);
    }

    dot.AppendLine("}");
    archivoGrafica("REPORTES/arbol_merkle.dot", "REPORTES/arbol_merkle.svg", dot.ToString());
    }



    private void GenerarDOTMerkle(NodoM nodo, ref StringBuilder dot)
{
    string nodoId = "node" + nodo.GetHashCode().ToString(); // ID único

    string etiqueta = "";

    if (nodo.f != null)
    {
        etiqueta = $"ID: {nodo.f.Id}\\nServicio: {nodo.f.IdServicio}\\nTotal: {nodo.f.Total}\\nHash: {nodo.Hash}";
    }
    else
    {
        etiqueta = $"Hash: {nodo.Hash}";
    }

    dot.AppendLine($"  {nodoId} [label=\"{etiqueta}\"];");

    if (nodo.Izquierdo != null)
    {
        string hijoIzqId = "node" + nodo.Izquierdo.GetHashCode().ToString();
        dot.AppendLine($"  {nodoId} -> {hijoIzqId};");
        GenerarDOTMerkle(nodo.Izquierdo, ref dot);
    }

    if (nodo.Derecho != null)
    {
        string hijoDerId = "node" + nodo.Derecho.GetHashCode().ToString();
        dot.AppendLine($"  {nodoId} -> {hijoDerId};");
        GenerarDOTMerkle(nodo.Derecho, ref dot);
    }
}







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

    

