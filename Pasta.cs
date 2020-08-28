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
    this.arquivos.Add(info);
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
    this.arquivos = new List<Arquivo>();
    this.arquivos.Add(info);

    if((conteudo[this.sub_init]!="NONE")){
        this.subDiretorios = new List<Diretorio>();
        for(int i =this.sub_init;i<=sub_end;i++){
            
            this.subDiretorios.Add(new Diretorio(conteudo[i]));
        }

    }

   
        for(int j=this.arq_init+1;j<conteudo.Length;j++){
            this.arquivos.Add(new Arquivo(conteudo[j]));
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
    if(this.subDiretorios[0]!=d){
        this.sub_end++;
    }
    this.arq_init = this.sub_end+1;
    this.updateInfo();
    
}


public void addArquivo(Arquivo a){
    
    if(this.arquivos == null){
        this.arquivos = new List<Arquivo>();
    }
    this.arquivos.Add(a);
    this.updateInfo();
  
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

    
    public Boolean removeDiretorio(){
        int i =0;
        foreach(Arquivo a in this.arquivos){
            i++;
        }

        if((this.subDiretorios == null )&& (i<=1)){
            this.arquivos[0].removeArq();
            Directory.Delete(this.path);
            return true;
        }else{
            return false;
        }
    }
    public Boolean removeArquivo(string n){

        foreach(Arquivo a in this.arquivos){
            if(a.getName()==n){
                this.arquivos.Remove(a);
                a.removeArq();
                this.updateInfo();
                return true;
            }
        }
        return false;

    }
    public Boolean removeArquivos(){
        foreach(Arquivo a in this.arquivos){
            if(a.getName()=="info"){

            }else{
                a.removeArq();
            }

        }
        return true;
    }
    public  Boolean removeSubDiretorios(){
            if(this.subDiretorios!=null){
            foreach(Diretorio d in this.subDiretorios){
                    foreach(Arquivo a in d.GetArquivos()){
                    a.removeArq();
                    }
                    d.removeSubDiretorios();
                    if(d.removeDiretorio()){
                        this.subDiretorios.Remove(d);
                        this.sub_end--;
                    }else{
                        return false;
                    }
                   
                }
            }

            this.arq_init= this.sub_end+1;
            this.updateInfo();
            return true;

    }
    public List<Diretorio> GetDiretorios(){

        return this.subDiretorios;

    }

    public List<Arquivo> GetArquivos(){
        return this.arquivos;
    }

    private Boolean updateInfo(){
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
        }
        }else{
            s.Add("NONE");
        }
        foreach(Arquivo r in this.arquivos){
            s.Add(r.getPath());
        }
        this.arquivos[0].Rewrite(s);
        return true;
    }
}



}