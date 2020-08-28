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
public static FileSystem StartSystem(){
    if(filesystem ==null){
        filesystem = new FileSystem();
    }
    return filesystem;
}

//////////////////////////////////////////////////////// criação de elementos do sistema///////////////////////////////
public void createDiretorio(string nome){
this.atual = this.atual.criaSubdiretorio(nome);
}

public void createArquivo(string nome){
this.atual.criaArquivo(nome);
}
//////////////////////////////////////////////////////////////////////// deleção de elementos ////////////////////////////////////
public Boolean deleteDiretorioRecursivamente(string p){
    if(p=="./root"){
        return false;
    }
    Diretorio r ;
    if((r= this.BuscaDiretorio(p))!=null){ 
        this.deleteAllSubDiretorios(r);
        r.removeArquivos();
        r.removeDiretorio();
        return true;
    }

    return false;

}

public Boolean deleteArquivo(string p){
    string [] r = p.Split('/');
    Diretorio d=this.root;

    for(int i=1;i<r.Length-1;i++){
        if(d.GetDiretorios()==null && (i==r.Length-2)){
            if(d.removeArquivo(r[r.Length-1])){
                return true;
            }else{
                return false;
            }
        }
        if(d.GetDiretorios()!=null){
        foreach(Diretorio e in d.GetDiretorios()){
             if(e.getName()==r[i+1]){
                 d = e;
                 break;
             }
        }
        }
    }

    
     return false;
}       
private void deleteAllSubDiretorios(Diretorio e){
    e.removeSubDiretorios();
}
private void deleteAllArquivos(Diretorio e){
List<Arquivo> ls = e.GetArquivos();
foreach(Arquivo a in ls){
    a.removeArq();
}
}

//////////////////////////////////////////////////////////////////busca de elementos ////////////////////////////////////////
public Diretorio BuscaDiretorio(string path){
    if(path == this.root.getPath()){
            return this.root;
            }
    if(path==this.atual.getPath())return this.atual;
    string[] result = path.Split('/');
    Diretorio busca = this.root;
    
    for(int i=2;i<result.Length;i++){
        List<Diretorio> ls = busca.GetDiretorios();
    if(ls!=null){    
    foreach(Diretorio d in ls){
        if(d.getName()==result[i]){
            busca = d;
            break;
        }else{
            busca = null;
        }
    }
    }
    if(busca==null){ return null;}
    }
    return busca;
}
private Diretorio BuscaDiretorio(string path,Diretorio e){
    if(e.getPath()==path)return e;
    List<Diretorio> ls = this.root.GetDiretorios();
    foreach(Diretorio d in ls){
        Diretorio r =  BuscaDiretorio(path,d);
        if(r!=null)return r;
    }
    return null;
}



///////////////////////////////////////////////////////////////////////// Navegação //////////////////////

public Boolean entraDiretorio(string n){
    Diretorio e =BuscaDiretorio(this.atual.getPath()+'/'+n);
    if((e!=null)){
        this.atual = e;
        return true;
    }
    return false;
}

public Boolean saiDiretorio(){
    if(this.atual.getPath()=="root"){
        return false;
    }

    string [] r = this.atual.getPath().Split('/');
    string aux =r[0];
    for(int i =1;i< r.Length-1;i++){
        aux = aux+'/'+r[i];
    }    
    Diretorio d;
    if((d = this .BuscaDiretorio(aux))!= null){
    this.atual = d;
    return true;
    }
    return false;
}

public string[] getDiretorioinfo(){

    string [] r = new string[2];
    r[0]= this.atual.getName();
    r[1]= this.atual.getDate();
    return r;
}

}

}







