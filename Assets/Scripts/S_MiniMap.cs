using UnityEngine;

public class S_MiniMap : MonoBehaviour
{
    [SerializeField] Transform joueur;
    [SerializeField] float hauteur = 30f;

    void Update()
    { 
        Vector3 pos = joueur.position;

        transform.position = new Vector3(
            pos.x,
            pos.y + hauteur,
            pos.z
        );

        
    }
}