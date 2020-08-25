using System;
using System.Collections.Generic;
using System.IO;
using manipuladorArquivos;

namespace manipuladorDiretorios{


public class Diretorio{

private string date;
private string name ;
private string path;

private int sub_init = 6;
private int sub_end=6;
private List<Diretorio> subDiretorios;
private int arq_init;
private List<Arquivo> arquivos;



public Diretorio(string n,string p){
this.name = n;

this.path = p+"/"+this.name;



if(!Directory.Exists(this.path)){                
    Directory.CreateDirectory(this.path);
    Arquivo info = new Arquivo("info",this.path);
    this.arq_init=this.sub_end+1;
    this.date = DateTime.Today.ToString();
    info.ArqWrite(this.name);
    info.ArqWrite(this.path);
    info.ArqWrite(this.date);
    info.ArqWrite(this.sub_init.ToString());
    info.ArqWrite(this.sub_end.ToString());
    info.ArqWrite(this.arq_init.ToString());
    info.ArqWrite("NONE");
    this.addArquivo(info);

 
}else{
 
    Arquivo info = new Arquivo("info",this.path);  
    string[] conteudo = info.readArq();
    this.date = conteudo[2];
    this.sub_init = int.Parse(conteudo[3]);
    this.sub_end = int.Parse(conteudo[4]);
    this.arq_init=int.Parse(conteudo[5]);
    this.addArquivo(info);
    if(!(conteudo[this.sub_init]=="NONE")){
        for(int i =this.sub_init;i<=sub_end;i++){
           
            this.subDiretorios.Add(new Diretorio(conteudo[i]));
        }

    }
    
        for(int j=this.arq_init+1;j<conteudo.Length;j++){
            this.addArquivo(new Arquivo(conteudo[j]));
        }
    


}


}


public Diretorio(string p){

this.path = p;



if(Directory.Exists(this.path)){
               
 Arquivo info = new Arquivo("info",this.path);
    this.arq_init=this.sub_end+1;
    string[] conteudo = info.readArq();
    this.name = conteudo[0];
    this.date = conteudo[2];
    this.sub_init = int.Parse(conteudo[3]);
    this.sub_end = int.Parse(conteudo[4]);
    this.arq_init=int.Parse(conteudo[5]);
    this.addArquivo(info);

    if(!(conteudo[this.sub_init]=="NONE")){
        for(int i =this.sub_init;i<=sub_end;i++){
            
            this.subDiretorios.Add(new Diretorio(conteudo[i]));
        }

    }

   
        for(int j=this.arq_init+1;j<conteudo.Length;j++){
            this.addArquivo(new Arquivo(conteudo[j]));
        }
}
    





}


public string getName(){
    return this.name;
}

public string getDate(){
    return this.date;
}

public string getPath(){
    return this.path;
}

public List<string> getConteudo(){
    List<string> s = new List<string>();

    if(this.subDiretorios!=null){
    foreach(Diretorio d in this.subDiretorios){
        s.Add(d.getName());
    }
    }
    if(this.arquivos!=null){
    foreach(Arquivo a in this.arquivos){
        s.Add(a.getName());
    }
    }

    return s;
    
}
public void addSubDiretorio(Diretorio d){
    if(this.subDiretorios==null){
        this.subDiretorios = new List<Diretorio>();
    }

    this.subDiretorios.Add(d);
    List<string>s = new List<string>();
    this.sub_end++;
    this.arq_init=this.sub_end+1;
    s.Add(this.name);
    s.Add(this.path);
    s.Add(this.date);
    s.Add(this.sub_init.ToString());
    s.Add(this.sub_end.ToString());
    s.Add(this.arq_init.ToString());

    foreach(Diretorio r in this.subDiretorios){
        s.Add(r.getPath());
    }
    foreach(Arquivo a in this.arquivos){
        s.Add(a.getPath());
    }

    this.arquivos[0].Rewrite(s);

    
}


public void addArquivo(Arquivo a){
    
    if(this.arquivos == null){
        this.arquivos = new List<Arquivo>();
    }
    this.arquivos.Add(a);
    List<string>s = new List<string>();
    s.Add(this.name);
    s.Add(this.path);
    s.Add(this.date);
    s.Add(this.sub_init.ToString());
    s.Add(this.sub_end.ToString());
    s.Add(this.arq_init.ToString());


    if(this.subDiretorios!=null){
    foreach(Diretorio d in this.subDiretorios){
        s.Add(d.getPath());
    }}else{
        s.Add("NONE");
    }
    foreach(Arquivo r in this.arquivos){
        s.Add(r.getPath());
    }
    this.arquivos[0].Rewrite(s);

}

public void criaArquivo(string n){
    Arquivo a = new Arquivo(n,this.path);
    this.addArquivo(a);
}

    public Diretorio criaSubdiretorio(string n){
        Diretorio d = new Diretorio(n,this.path);
        this.addSubDiretorio(d);
        return d;
    }

    
    public void removeDiretorio(){
        int i =0;
        foreach(Arquivo a in this.arquivos){
            i++;
        }

        if((this.subDiretorios == null )&& (i<=1)){
            this.arquivos[0].removeArq();
            Directory.Delete(this.path);
        }else{
            Console.WriteLine("Não é possivel remover um diretorio não vazio...");
        }
    }

    public List<Diretorio> GetDiretorios(){

        return this.subDiretorios;

    }

    public List<Arquivo> GetArquivos(){
        return this.arquivos;
    }

}



}