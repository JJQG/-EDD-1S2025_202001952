using System;

public class Blockchain
{
    public Block cabeza = null; 
    public Block cola; 
    public int Size = 0;

   
    public Block insertar(int ID, string Nombres, string Apellidos, string Correo, int Edad, string Contrasenia){

        //Encriptar la contraseña
       // string contraseñaEncriptada = Block.getSHA256();


        Block newBlock = new Block(Size,  ID, Nombres, Apellidos, Correo, Edad, Contrasenia );

        if(cabeza == null)
        {
            newBlock.ultimoHash = "0000";
            cola = newBlock;
            cabeza = newBlock;
        }
        else 
        {
            //newBlock.ultimoHash = cola.Hash;

            
        newBlock.siguiente = cabeza;
        newBlock.ultimoHash = cola.Hash;
        cola = newBlock;
        cabeza = newBlock;

            /*Block temp = cabeza;
            
                temp = temp.siguiente;
               temp.ultimoHash = cola.Hash;
            
            temp.siguiente = newBlock; */
        }

        /*string valor = Convert.ToString(newBlock.Index) + nombre + contraseña; //resto de informacion
        newBlock.Hash = Encrypt.GetSHA256(valor);*/

      

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
}

