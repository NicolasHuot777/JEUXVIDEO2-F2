using System.Collections;


using UnityEngine;

public class PlayerControllerID : MonoBehaviour
{
    public float acceleration = 1f;
    public float deceleration = 3f;

    public float vitesseMax = 3f;
    public float vitesseRotation = 75.0f;

    //Un parametre qui contient la vitesse de course de notre personnage
    private float vitesse = 0f;

    //La reference vers notre composant Animator
    private Animator animator;


    //la constant qui contient le nom deu parametre pour communiquer avec l'Animator
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

        //Cette partie gere l'input de l'utilisateur
        float avance= 0f;
        avance = Input.GetAxis("Vertical");


        //Cette partie gere le déplacement du player
        //Tant qu'on appuie sur avance, la vitesse augmente (lavitesse est fonction de la vitesse à la frame précédente
        if (avance != 0 & vitesse < vitesseMax)
        {
            vitesse += Time.deltaTime * acceleration;

        }
        if (avance == 0 && vitesse > 0)
        {
            vitesse -= Time.deltaTime * deceleration;
            if (vitesse < 0.1f)
            {
                vitesse = 0;
            }
        }

        if (vitesse != 0)
        {
            float rotation = Input.GetAxis("Horizontal") * vitesseRotation * Time.deltaTime;
            transform.Rotate(0, rotation, 0);

            float translation = vitesse * Time.deltaTime;
            transform.Translate(Vector3.forward * translation);
        }


        //Cette partie transmet l'état du personnage à l'Animator pour piloter l'Animation
        animator.SetFloat(animatorVitesseHash, vitesse);

    }
}

