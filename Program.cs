using System;
using Interface;
using manipuladorArquivos;
using manipuladorDiretorios;

namespace projetito
{
    class Program
    {
        static void Main(string[] args)
        {
            Diretorio d = new Diretorio("./root/teste");

            d.removeDiretorio();


        }
    }
}
