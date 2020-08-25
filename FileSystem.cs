using System;
using System.IO;
using System.Collections.Generic;
using manipuladorArquivos;
using manipuladorDiretorios;



namespace fileSystem{

public class FileSystem{

private const string rootPath = "./root";

private Diretorio root;

private Diretorio atual;

private static FileSystem filesystem; 

private FileSystem(){


this.root = new Diretorio(rootPath);
this.atual =  this.root;

}


public FileSystem StartSystem(){
    if(filesystem ==null){
        filesystem = new FileSystem();
    }

    return filesystem;

}


public void createDiretorio(string nome){

this.atual = this.atual.criaSubdiretorio(nome);

}


public void createArquivo(string nome){

this.atual.criaArquivo(nome);
}

public void deleteDiretorio(string path){
    List<Diretorio> ls = this.atual.GetDiretorios();
    Diretorio r;
    foreach(Diretorio d in ls){
        if(d.getPath() == path){
            r = d;
        }
    }




}

private void deleteAllSubDiretorios(Diretorio e){
List<Diretorio> ls = e.GetDiretorios();

foreach(Diretorio d in ls){
    this.deleteAllSubDiretorios(d);
}
this.deleteAllArquivos(e);
e.removeDiretorio();


}

private void deleteAllArquivos(Diretorio e){
List<Arquivo> ls = e.GetArquivos();

foreach(Arquivo a in ls){
    a.removeArq();
}

}


}

}







