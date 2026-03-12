using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class S_Patrouille : MonoBehaviour
{
    [SerializeField] Transform[] points;
    [SerializeField] float distanceVision = 10f;
    NavMeshAgent agent;
    int index = 0;
    S_Joueur joueur;
  
    void Start()
    {
        joueur = GameObject.FindGameObjectWithTag("Player").GetComponent<S_Joueur>();
       
        agent = GetComponent<NavMeshAgent>();

        if (points.Length > 0)
        {
            agent.SetDestination(points[index].position);
        }
       
    }

    IEnumerator Delais()
    {
        joueur.Mourir();
        yield return new WaitForSeconds(8);
        SceneManager.LoadScene(2);
    }

     private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up, transform.forward * distanceVision);
    }

    void Update()
    {
        
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)  //Recherche sur internet 
        {
            index++;
            if (index >= points.Length)
                index = 0;

            agent.SetDestination(points[index].position);
        }

            Ray rayon = new Ray(transform.position + Vector3.up, transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(rayon, out hit, distanceVision))
            {
                if (hit.collider.CompareTag("Player") && joueur.portailOuvert)
                {

                   Debug.Log("Repéré !");
                   StartCoroutine(Delais());
                 }
            }
        
    }
}