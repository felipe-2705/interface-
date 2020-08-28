using System;
using System.IO;
using System.Collections.Generic;


namespace manipuladorArquivos{



public class Arquivo{
    
    private const string defaultPath = "./";
    private string path; // salva o caminha completo do arquivo 
    private string name; 
    private int header_end = 4; // guarda a linha onde o header termina 
    private int textbody; // guarda onde o corpo do conteudo do arquivo de fato inicia. 
    private string date; // ultima data de alteração do arquivo 
    

    public Arquivo(string n, string path){
        this.name =  n ;
        this.path = path+"/"+this.name;
        this.textbody = this.header_end +1;
        if(!File.Exists(this.path)){
            this.date = DateTime.Today.ToString();
            using(StreamWriter sw = File.CreateText(this.path)){
       
                this.header(sw);
                
                
                }
        }else{
            this.Reload();
        }


    }

    public Arquivo(string path){
        this.path = path;
        if(File.Exists(this.path)){
            this.Reload();
            this.textbody = this.header_end +1;
        }else{

            Console.WriteLine("ERRO Arquvio nao Encontrado");
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
    private void header(StreamWriter sw){
        sw.WriteLine((this.header_end.ToString()));
        sw.WriteLine((this.header_end + 1).ToString()); // TextBody init 
        sw.WriteLine(this.path);
        sw.WriteLine(this.name);
        sw.WriteLine(this.date);
    }

    public string[] readArq()
    {   List<string> result = new List<string>();
        int size=0;
        using(StreamReader sr = File.OpenText(this.path)){
           string aux;
           for(int i= 0;i<=this.header_end;i++){
               sr.ReadLine();
           } 
           while((aux = sr.ReadLine())!=null){
               size++;
               result.Add(aux);
           }
        }
        string[] r=  new string[size];
        int j=0;
        foreach(string s in result){
            r[j]=s;
            j++;
        }

        return r;
    }

    private void Reload(){
        using(StreamReader sr = File.OpenText(this.path)){
               
                    
                    this.header_end = Convert.ToInt32(sr.ReadLine());
                    this.textbody = Convert.ToInt32(sr.ReadLine());
                    sr.ReadLine();
                    this.name = sr.ReadLine();
                    this.date = sr.ReadLine();
                
        }
    }

    public string info(){
        string result="Info: \n";
       
        
                  result= result+"Fim do head: "+this.header_end.ToString()+"\n";
                  result= result+"Inicio do arq: "+this.textbody.ToString()+"\n";
                  result= result+"Path: "+this.path+"\n";
                  result= result+"Nome: "+this.name+"\n";
                  result= result+"Data: "+this.date+"\n";
                

        return result;

    }

    public void ArqWrite(string s ){
        
        using(StreamWriter sw = File.AppendText(this.path)){
            sw.WriteLine(s);
        }

    }

    public void Rewrite(List<string> ls){
        List<string> old = new List<string>();
        using(StreamReader sr = File.OpenText(this.path)){
            
            int i =0;
           
            
           while(i<=this.header_end){
            old.Add(sr.ReadLine());  
            i++;
            }

        }
        using(StreamWriter sw = File.CreateText(this.path)){
            foreach(string s in old){
                sw.WriteLine(s);
            }
            foreach(string s in ls){
                sw.WriteLine(s);
            }
        }

    }



    public void arqRemoveLine(int n ){
       List<string> ls =  new List<string>();

       using(StreamReader sr = File.OpenText(this.path)){
           string aux;
           int line =0;
           while((aux = sr.ReadLine())!= null){
               
               if(!(line == this.textbody+n)){
                   ls.Add(aux);
                    
               }
               line++;

           }

       }

       using(StreamWriter sw = File.CreateText(this.path)){
            foreach(string s in ls){
                sw.WriteLine(s);
            }
       }

    }



    public void editLine(int l,int i,int j){
           List<string> ls =  new List<string>();
           using(StreamReader sr = File.OpenText(this.path)){
               string aux;
               int line = 0;
               while((aux=sr.ReadLine())!=null){

                   if(!(line==this.textbody+l)){
                        ls.Add(aux);
                   }else{
                    
                       aux = aux.Remove(i,(j-i)+1);
                       ls.Add(aux);

                   }
                   line++;
               }

           }



           using(StreamWriter sw = File.CreateText(this.path)){
            foreach(string s in ls){
                sw.WriteLine(s);
            }
       }
    }


    public void editLine(int l,string old,string nova){
           List<string> ls =  new List<string>();
           using(StreamReader sr = File.OpenText(this.path)){
               string aux;
               int line = 0;
               while((aux=sr.ReadLine())!=null){

                   if(!(line==this.textbody+l)){
                        ls.Add(aux);
                   }else{
                       aux = aux.Replace(old,nova);
                       ls.Add(aux);

                   }
                   line++;
               }

           }



           using(StreamWriter sw = File.CreateText(this.path)){
            foreach(string s in ls){
                sw.WriteLine(s);
            }
       }
    }


    public void removeArq(){
        File.Delete(this.path);
    }


   
}



    

}