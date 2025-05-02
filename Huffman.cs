using System;
using System.Text;
public class Huffman
{
    public char Caracter;
    public int Frecuencia;
    public Huffman Izquierda;
    public Huffman Derecha;
    public bool EsHoja => Izquierda == null && Derecha == null;
    private Dictionary<char, string> tablaCodigos;

    public string Comprimir(string texto)
    {
        var frecuencias = getFrecuencias(texto);
        var arbol = ConstruirArbol(frecuencias);
        tablaCodigos = new Dictionary<char, string>();
        ConstruirCodigos(arbol, "");
        return CodificarTexto(texto);
    }

    public Dictionary<char, int> getFrecuencias(string texto)
    {
        var dict = new Dictionary<char, int>();
        foreach (char c in texto)
        {
            if (!dict.ContainsKey(c)) dict[c] = 0;
            dict[c]++;
        }
        return dict;
    }

    public Huffman ConstruirArbol(Dictionary<char, int> frecuencias)
    {
        var cola = new PriorityQueue<Huffman, int>();
        foreach (var kvp in frecuencias)
        {
            cola.Enqueue(new Huffman { Caracter = kvp.Key, Frecuencia = kvp.Value }, kvp.Value);
        }

        while (cola.Count > 1)
        {
            Huffman izq = cola.Dequeue();
            Huffman der = cola.Dequeue();
            Huffman nuevo = new Huffman
            {
                Frecuencia = izq.Frecuencia + der.Frecuencia,
                Izquierda = izq,
                Derecha = der
            };
            cola.Enqueue(nuevo, nuevo.Frecuencia);
        }

        return cola.Dequeue();
    }

    private void ConstruirCodigos(Huffman nodo, string codigo)
    {
        if (nodo == null) return;
        if (nodo.EsHoja)
        {
            tablaCodigos[nodo.Caracter] = codigo;
        }
        else
        {
            ConstruirCodigos(nodo.Izquierda, codigo + "0");
            ConstruirCodigos(nodo.Derecha, codigo + "1");
        }
    }

    private string CodificarTexto(string texto)
    {
        StringBuilder resultado = new StringBuilder();
        foreach (char c in texto)
        {
            resultado.Append(tablaCodigos[c]);
        }
        return resultado.ToString();
    }

    public string Descomprimir(string textoCodificado, Huffman arbol)
{
    StringBuilder resultado = new StringBuilder();
    Huffman actual = arbol;

    foreach (char bit in textoCodificado)
    {
        actual = bit == '0' ? actual.Izquierda : actual.Derecha;

        if (actual.EsHoja)
        {
            resultado.Append(actual.Caracter);
            actual = arbol;
        }
    }

    return resultado.ToString();
}

public string ComprimirConTabla(string texto)
{
    var frecuencias = getFrecuencias(texto);
    var arbol = ConstruirArbol(frecuencias);
    tablaCodigos = new Dictionary<char, string>();
    ConstruirCodigos(arbol, "");

    string codificado = CodificarTexto(texto);

    StringBuilder cabecera = new StringBuilder();
    foreach (var par in frecuencias)
    {
        cabecera.AppendLine(((int)par.Key) + ":" + par.Value);
    }

    return cabecera.ToString() + "---\n" + codificado;
}

public string DescomprimirConTabla(string contenidoArchivo)
{
    string[] partes = contenidoArchivo.Split(new string[] { "---\n" }, StringSplitOptions.None);
    if (partes.Length != 2)
        throw new Exception("Formato inválido del archivo comprimido.");

    string cabecera = partes[0];
    string codificado = partes[1];

    Dictionary<char, int> frecuencias = new Dictionary<char, int>();
    string[] lineas = cabecera.Split('\n', StringSplitOptions.RemoveEmptyEntries);
    foreach (string linea in lineas)
    {
        string[] partesLinea = linea.Split(':');
        int charCode = int.Parse(partesLinea[0]);
        int frecuencia = int.Parse(partesLinea[1]);
        frecuencias[(char)charCode] = frecuencia;
    }

    Huffman arbol = ConstruirArbol(frecuencias);
    return Descomprimir(codificado, arbol);
}


}
