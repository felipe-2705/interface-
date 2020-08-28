using System;
using Interface;
using manipuladorArquivos;
using manipuladorDiretorios;
using fileSystem;


namespace projetito
{
    class Program
    {
        static void Main(string[] args)
        {
        
            FileSystem fl = FileSystem.StartSystem();
            fl.entraDiretorio("teste");
            string[] r = fl.getDiretorioinfo();
            Console.WriteLine(r[0]);
            fl.saiDiretorio();
            r=fl.getDiretorioinfo();
            Console.WriteLine(r[0]);
        }
    }
}
