using System;
using UnityEngine;

public class S_Generateur : MonoBehaviour
{

    MeshRenderer mr;

    void Start()
    {
        // trouver l'enfant "Batterie"
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enfant = transform.GetChild(i);
            if (enfant.name == "Batterie")
            {
                enfant.gameObject.SetActive(false);
            }
            if (enfant.name == "voyant")
            {
                mr = enfant.gameObject.GetComponent<MeshRenderer>();
            }
        }
     
    }

    public void ActiverEnfants()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enfant = transform.GetChild(i);

            if (enfant.name == "Batterie")
            {
                enfant.gameObject.SetActive(true);
            }

            Debug.Log("activer");
        }
        mr.material.color = Color.green;
    }
    
  
}
