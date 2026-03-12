using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class S_Joueur : MonoBehaviour
{
    float vitesse = 0;
    float acceleration = 2.5f;
    float vitesseMax = 5f;
    Vector2 direction = Vector2.zero;
    int nbgenerateur = 0;
    int nbBatteries = 0;

    bool cibleDetectee = false;
    bool mainPleine = false;
    bool Depot = false;
   public  bool portailOuvert = false;

    [SerializeField] TextMeshProUGUI txtCountBatt;
    [SerializeField] TextMeshProUGUI txtCountGen;
    S_Generateur gen;

    [SerializeField] float distance = 2f;

    Animator animator;
    public delegate void PortailOuvert();
    public PortailOuvert OnPortailOuvert;

    RaycastHit hit;

    void Start()
    {

        animator = GetComponent<Animator>();

    }

    void OnMove(InputValue value)
    {
        direction = value.Get<Vector2>();
    }

    void OnInteract()  //Touche R du clavier
    {
      

        if (cibleDetectee && hit.collider.CompareTag("Battery") && !mainPleine)
        {
            Debug.Log("→ Batterie ramassée");
            
            hit.collider.gameObject.SetActive(false);

            nbBatteries++;
            Debug.Log("Batteries : " + nbBatteries + "/2");
        }

        if (nbBatteries == 2)
        {
            mainPleine = true;
            Debug.Log("Main pleine : tu peux activer un générateur");
        }
    }

    void OnJump() //Touche Espace du clavier 
    {
        Debug.Log("OnJump appelé");

        if (Depot && gen != null)
        {
            Debug.Log("→ Dépôt sur générateur");
            gen.ActiverEnfants();
            mainPleine = false;
            nbBatteries = 0;
            nbgenerateur++;
            Debug.Log("Générateurs activés : " + nbgenerateur + "/4");
        }

        if (nbBatteries < 2)
            Debug.Log("Il te faut 2 batteries pour activer un générateur");

        if (nbgenerateur >= 4)
        {
            
            OnPortailOuvert();
            portailOuvert = true;
        }
        else
        {
            Debug.Log("Générateurs insuffisants : " + nbgenerateur + "/4");
        }
    }


    public void Mourir()
    {
        Debug.Log("Le joueur meurt !");
        animator.SetTrigger("Reperer");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZoneVictoire"))
        {
            Debug.Log("Zone victoire atteinte");
            animator.SetTrigger("ZoneVictoire");
            StartCoroutine(Delais());
        }
    }

    IEnumerator Delais()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }

    void Update()
    {
        // Déplacement
        transform.Rotate(0f, direction.x * 50f * Time.deltaTime, 0f);
        vitesse += direction.y * acceleration * Time.deltaTime;
        vitesse = Mathf.Clamp(vitesse, 0, 1);

        transform.Translate(Vector3.forward * vitesse * vitesseMax * Time.deltaTime);

        if (direction.y == 0)
            vitesse = Mathf.Lerp(vitesse, 0, 20f * Time.deltaTime);
            animator.SetFloat("Vitesse", vitesse);

        // UI
        txtCountBatt.text = nbBatteries + "X /2";
        txtCountGen.text = nbgenerateur + " X /4 actifs";

        // Raycast
        Ray rayon = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(rayon, out hit, distance))
        {
            

            if (hit.collider.CompareTag("Battery"))
            {
                cibleDetectee = true;
                Debug.Log(" Batterie détectée");
            }
            else
                cibleDetectee = false;

            if (hit.collider.CompareTag("Generator"))
            {
                Debug.Log(" Générateur détecté");
                gen = hit.collider.GetComponent<S_Generateur>();
                Depot = mainPleine;
            }
            else
            {
                Depot = false;
                gen = null;
            }
        }
        
    }
}

