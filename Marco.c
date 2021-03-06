
#include "Marco.h"
#include <stdio.h>
#include <string.h>
#include<stdlib.h>

struct vaccino
{
    char s[20];
    float num;
    struct vaccino *next;
};

struct vaccino *LetturaFile(FILE *file, char path[], char permesso[]);
FILE* ApriFile(char f[], char permesso[]);
void ViewLista(struct vaccino *p);

int main()
{
    FILE *fileptr; //Puntatore al file
    struct vaccino *pLista=NULL;
    
    //Leggo da file
    LetturaFile(fileptr, "Vaccini.txt", "r");
    
    //Visualizza i >90
    ViewLista(pLista);
    return 0;
}

/*Leggo tutti gli interi presenti nel file .txt e li metto nella struttura vaccino**/
struct vaccino *LetturaFile(FILE *file, char path[], char permesso[])
{
    char riga[50];
    int cont=1;
    struct vaccino *p, *pNext;
    float vf[50];
    
    file= ApriFile(path, permesso);
    //Scorrre il file fino alla fine
    while (feof(file)==0)
    {
        fscanf(file, "%s", riga);
        char * token = strtok(riga, " ");
        
        //A ogni giro restituisce il valore delimitato dal carattere di split
        while( token != NULL ) {
           printf( " %s\n", token ); //printing each token
           token = strtok(NULL, " ");
            
            
            p = (struct vaccino*) malloc(sizeof(struct vaccino));
            
            //Inserisco nella lista
            if(cont%2==0) //Pari quindi affidabilita
            {
                p->num = token;
                pNext=p;
            }
            else //Dispari quindi il nome
            {
                p->s = token;
                pNext=p;
            }
        }
    }
    fclose(file); //Chiudo il file;
    //Faccio in modo che l'ultimo elemento punti a NULL
    pNext->next = NULL;
    
    return p;
}

/*Controllo che il file esista e se non esiste quindi == NULL restituisco un messaggio di errore e sospendo l'esecuzione**/
FILE* ApriFile(char f[], char permesso[])
{
    FILE *app = fopen(f, permesso);
    
    if(app==NULL)
    {
        printf("Errore generico file! ");
        exit(1); //Termina il programma
    }
    
    return app;
}



void ViewLista(struct vaccino *p)
{
    int cont=0;
    
    printf("\n\nContenuto della lista: ");
    while(p!=NULL)
    {
        if(p->num >90)
            cont++;
        
        p=p->next;
    }
    printf("Numero vaccini >90: %d", cont);
}

