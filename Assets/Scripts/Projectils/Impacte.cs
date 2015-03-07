using UnityEngine;

public class Impacte 
{
    Vector2 dir_impacte;
    int mals;
    float for_impacte;
    
    
    public Impacte(Vector2 dir,int mal, float fors )
    {
         dir_impacte = dir;
         mals = mal;
         for_impacte = fors;
    }

    public Vector2 Direccio
    {
        get { return dir_impacte; }
        set { dir_impacte = value; }
    }

    public int Mal
    {
        get { return mals; }
        set { mals = value; }
    }

    public float Forsa
    {
        get { return for_impacte; }
        set { for_impacte = value; }
    }
}
