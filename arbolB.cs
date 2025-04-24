using System;

public class ArbolB
{
    public NodoB raiz;
    private const int ORDEN = 5;
    private const int MAX_CLAVES = ORDEN - 1;
    private const int MIN_CLAVES = (ORDEN / 2) - 1;

    public ArbolB()
    {
        raiz = new NodoB();
    }

    public void Insertar(int Id, int IdServicio, double total)
    {
        Facturas f = new Facturas(Id, IdServicio,total);

        if (raiz.lleno())
        {
            //Realizamos una division
            NodoB nuevaRaiz = new NodoB();
            nuevaRaiz.EsHoja = false;
            nuevaRaiz.Hijos.Add(raiz);
            DividirHijo(nuevaRaiz, 0);
            raiz = nuevaRaiz;
        }

        InsertarNoLleno(raiz, f);
    }

    public void InsertarNoLleno(NodoB nodo, Facturas f)
    {

        int i = nodo.Claves.Count - 1;

        if (nodo.EsHoja)
        {
            while (i >= 0 && f.Id < nodo.Claves[i].Id)
            {
                i--;
            }
            nodo.Claves.Insert(i + 1, f);
        }
        else
        {

            while (i >= 0 && f.Id < nodo.Claves[i].Id)
            {
                i--;
            }
            i++;

            if (nodo.Hijos[i].lleno())
            {
                DividirHijo(nodo, i);
                if (f.Id > nodo.Claves[i].Id)
                {
                    i++;
                }
            }

            InsertarNoLleno(nodo.Hijos[i], f);

        }


    }

    public void DividirHijo(NodoB padre, int indiceHijo)
    {

        NodoB hijoCompleto = padre.Hijos[indiceHijo]; //Temporal
        NodoB nuevoHijo = new NodoB();

        nuevoHijo.EsHoja = hijoCompleto.EsHoja;

        Facturas fMedio = hijoCompleto.Claves[MIN_CLAVES];

        //Mover la mitad de las claves
        for (int i = MIN_CLAVES + 1; i < MAX_CLAVES; i++)
        {
            nuevoHijo.Claves.Add(hijoCompleto.Claves[i]);
        }

        if (!hijoCompleto.EsHoja)
        {
            for (int i = (ORDEN / 2); i < ORDEN; i++)
            {
                nuevoHijo.Hijos.Add(hijoCompleto.Hijos[i]);
            }
            hijoCompleto.Hijos.RemoveRange((ORDEN / 2), hijoCompleto.Hijos.Count - (ORDEN / 2));

        }

        hijoCompleto.Claves.RemoveRange(MIN_CLAVES, hijoCompleto.Claves.Count - MIN_CLAVES);

        padre.Hijos.Insert(indiceHijo + 1, nuevoHijo);

        int j = 0;
        while (j < padre.Claves.Count && padre.Claves[j].Id < fMedio.Id)
        {
            j++;
        }

        padre.Claves.Insert(j, fMedio);


    }



    public Facturas Buscar(int id)
    {
        return BuscarRecursivo(raiz, id);
    }

    private Facturas BuscarRecursivo(NodoB nodo, int id)
    {
        int i = 0;
        while (i < nodo.Claves.Count && id > nodo.Claves[i].Id)
        {
            i++;
        }

        if (i < nodo.Claves.Count && id == nodo.Claves[i].Id)
        {
            return nodo.Claves[i];
        }

        if (nodo.EsHoja)
        {
            return null;
        }

        return BuscarRecursivo(nodo.Hijos[i], id);

    }


    public void Eliminar(int id)
    {
        EliminarRecursivo(raiz, id);

      if (raiz.Claves.Count == 0 && !raiz.EsHoja)
        {
            NodoB antiguaRaiz = raiz;
            raiz = raiz.Hijos[0];
        }
    }

    private void EliminarRecursivo(NodoB nodo, int id)
    {
        int indice = EncontrarIndice(nodo, id);

        if (indice < nodo.Claves.Count && nodo.Claves[indice].Id == id)
        {
            if (nodo.EsHoja)
            {
                nodo.Claves.RemoveAt(indice);
            }
            else
            {
                EliminarDeNodoInterno(nodo, indice);
            }
        }
        else
        {
            if (nodo.EsHoja)
            {
                Console.WriteLine($"El elemento con Id {id} no existe en el árbol");
                return;
            }

            bool ultimoHijo = (indice == nodo.Claves.Count);

            if (!nodo.Hijos[indice].MinimoClaves())
            {
                RellenarHijo(nodo, indice);
            }
            if (ultimoHijo && indice > nodo.Hijos.Count - 1)
            {
                EliminarRecursivo(nodo.Hijos[indice - 1], id);
            }
            else
            {
                EliminarRecursivo(nodo.Hijos[indice], id);
            }
        }
    }

    private int EncontrarIndice(NodoB nodo, int id)
    {
        int indice = 0;
        while (indice < nodo.Claves.Count && nodo.Claves[indice].Id < id)
        {
            indice++;
        }
        return indice;
    }


    private void EliminarDeNodoInterno(NodoB nodo, int indice)
    {
        Facturas clave = nodo.Claves[indice];

        if (nodo.Hijos[indice].Claves.Count > MIN_CLAVES)
        {
            Facturas predecesor = ObtenerPredecesor(nodo, indice);
            nodo.Claves[indice] = predecesor;
            EliminarRecursivo(nodo.Hijos[indice], predecesor.Id);
        }
        else if (nodo.Hijos[indice + 1].Claves.Count > MIN_CLAVES)
        {
            Facturas sucesor = ObtenerSucesor(nodo, indice);
            nodo.Claves[indice] = sucesor;
            EliminarRecursivo(nodo.Hijos[indice + 1], sucesor.Id);
        }
        else
        {
            FusionarNodos(nodo, indice);
            EliminarRecursivo(nodo.Hijos[indice], clave.Id);
        }
    }

    private void FusionarNodos(NodoB nodo, int indice)
    {
        NodoB hijo = nodo.Hijos[indice];
        NodoB hermano = nodo.Hijos[indice + 1];

        hijo.Claves.Add(nodo.Claves[indice]);

        for (int i = 0; i < hermano.Claves.Count; i++)
        {
            hijo.Claves.Add(hermano.Claves[i]);
        }
        if (!hijo.EsHoja)
        {
            for (int i = 0; i < hermano.Hijos.Count; i++)
            {
                hijo.Hijos.Add(hermano.Hijos[i]);
            }
        }

        nodo.Claves.RemoveAt(indice);
        nodo.Hijos.RemoveAt(indice + 1);
    }




    private Facturas ObtenerPredecesor(NodoB nodo, int indice)
    {
        NodoB actual = nodo.Hijos[indice];
        while (!actual.EsHoja)
        {
            actual = actual.Hijos[actual.Claves.Count];
        }
        return actual.Claves[actual.Claves.Count - 1];
    }


    private Facturas ObtenerSucesor(NodoB nodo, int indice)
    {
        NodoB actual = nodo.Hijos[indice + 1];
        while (!actual.EsHoja)
        {
            actual = actual.Hijos[0];
        }
        return actual.Claves[0];
    }


    private void RellenarHijo(NodoB nodo, int indice)
    {
        if (indice > 0 && nodo.Hijos[indice - 1].Claves.Count > MIN_CLAVES)
        {
            TomaPrestadoDelAnterior(nodo, indice);
        }
        else if (indice < nodo.Claves.Count && nodo.Hijos[indice + 1].Claves.Count > MIN_CLAVES)
        {
            TomaPrestadoDelSiguiente(nodo, indice);
        }
        else
        {
            if (indice < nodo.Claves.Count)
            {
                FusionarNodos(nodo, indice);
            }
            else
            {
                FusionarNodos(nodo, indice - 1);
            }
        }
    }

    private void TomaPrestadoDelAnterior(NodoB nodo, int indice)
    {
        NodoB hijo = nodo.Hijos[indice];
        NodoB hermano = nodo.Hijos[indice - 1];

        hijo.Claves.Insert(0, nodo.Claves[indice - 1]);

        if (!hijo.EsHoja)
        {
            hijo.Hijos.Insert(0, hermano.Hijos[hermano.Claves.Count]);
            hermano.Hijos.RemoveAt(hermano.Claves.Count);
        }

        nodo.Claves[indice - 1] = hermano.Claves[hermano.Claves.Count - 1];
        hermano.Claves.RemoveAt(hermano.Claves.Count - 1);
    }

    private void TomaPrestadoDelSiguiente(NodoB nodo, int indice)
    {
        NodoB hijo = nodo.Hijos[indice];
        NodoB hermano = nodo.Hijos[indice + 1];

        hijo.Claves.Add(nodo.Claves[indice]);

        if (!hijo.EsHoja)
        {
            hijo.Hijos.Add(hermano.Hijos[0]);
            hermano.Hijos.RemoveAt(0);
        }

        // Actualizar la clave del padre
        nodo.Claves[indice] = hermano.Claves[0];
        hermano.Claves.RemoveAt(0);
    }

    public int generarID(){
        int ID =1;

        while(Buscar(ID) != null){
            ID +=1;
        }

        return ID;
    }
}


