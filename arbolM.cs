using System;
using System.Text.Json;

public class ArbolMerkle
    {

        public List<NodoM> Leaves;
        public NodoM Root;

        public ArbolMerkle()
        {
            Leaves = new List<NodoM>();
            Root = null;
        }


        public void Insertar(int id, int id_servicio, double total)
        {
            foreach(var leaf in Leaves)
            {
                if(leaf.f.Id == id)
                {
                    Console.WriteLine("Error: Ya existe una factura con el ID:", Convert.ToString(id));
                }
            }

            Facturas f = new Facturas(id, id_servicio, total);

            NodoM newLeaf = new NodoM(f);
            Leaves.Add(newLeaf);


            Construir();
        }


        public void Construir()
        {
            if(Leaves.Count == 0)
            {
                Root = null;
                return;
            }


            List<NodoM> currentLevel = new List<NodoM>(Leaves);

            while(currentLevel.Count > 1)
            {
                List<NodoM> nextLevel = new List<NodoM>();

                for(int i = 0; i < currentLevel.Count; i +=2)
                {

                    NodoM left = currentLevel[i];
                    NodoM right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
                    NodoM parent = new NodoM(left, right);

                    nextLevel.Add(parent);

                }

                currentLevel = nextLevel;
            }

            Root = currentLevel[0];

        }

        
    
    }