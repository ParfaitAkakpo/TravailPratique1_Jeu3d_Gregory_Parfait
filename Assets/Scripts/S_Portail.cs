using System;
using UnityEngine;

public class S_Portail : MonoBehaviour
{
     S_Joueur Joueur;
    Animator animator;

    void Start()
    { 
        Joueur = GameObject.FindGameObjectWithTag("Player").GetComponent<S_Joueur>();

        animator = GetComponent<Animator>();
       
        
        Joueur.OnPortailOuvert += Ouvrir;
     
    }

    void Ouvrir()
    {
        Debug.Log(" Le portail s'ouvre !");
        animator.SetTrigger("Ouvrir");
    }
}

