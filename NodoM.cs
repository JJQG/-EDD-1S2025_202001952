using System;

public class NodoM
{
    public string Hash;
        public NodoM Left;
        public NodoM Right;

        public Facturas f;

        public NodoM(Facturas f)
        {
            this.f = f;
            Hash = f.GetHash();
            Left = null;
            Right = null;
        }

       /* public NodoM(MerkleNode left, MerkleNode right)
        {
            Factura = null;
            Left = left;
            Right = right;
            Hash = CalculateHash(left.Hash, right?.Hash);

        }*/

    

 
}