using UnityEngine;

public class EveBasicController : MonoBehaviour
{

    //Un parametre qui contient la vitesse de marche de notre personnage
    public float vitesseMarche = 1f;
    public float vitesseRotation = 75.0f;
    public Camera gameCamera;
    public Camera playerCamera;
    private float vitesseCourse = 3f;

    //La reference vers notre composant Animator IMPORTANT!!!
    private Animator animator;


    //la constant qui contient le nom du parametre pour communiquer avec l'Animator
    private const string SPEED = "speed";

    //HashCode vers les variables de l'Animator (permet de communiquer avec lui)
    private int animatorVitesseHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animatorVitesseHash = Animator.StringToHash(SPEED);
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle between two cameras when the player active state of the cameras
        if (Input.GetKeyDown(KeyCode.C))
        {
            gameCamera.enabled = !gameCamera.enabled;
            playerCamera.enabled = !playerCamera.enabled;
        }

        //Cette partie gere l'inp=ut de l'utilisateur
        float vitesse = 0f;
        float avance = 0f;
        avance += Input.GetAxis("Vertical");

        //Cette partie gere le déplacement du player
        if (avance != 0)
        {
            float rotation = Input.GetAxis("Horizontal") * vitesseRotation * Time.deltaTime;
            transform.Rotate(0, rotation, 0);

            vitesse = vitesseMarche;
            float translation = vitesse * Time.deltaTime;
            transform.Translate(Vector3.forward * translation);
        }

        // Toggle between two cameras when the player active state of the cameras
        if (Input.GetKey(KeyCode.LeftShift))
        {
            vitesse = vitesseCourse;
        }

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            vitesse = vitesseMarche;
        }

        //Cette partie transmet l'état du personnage à l'Animator pour piloter l'Animation
        animator.SetFloat(animatorVitesseHash, vitesse);

    }
}

