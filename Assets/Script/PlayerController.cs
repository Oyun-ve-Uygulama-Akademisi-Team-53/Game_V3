using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    public float speed;
    public float rotationSpeed;
    public float jumpSpeed;
    public float yMagnitude;
    public float jumpButtonGracePeriod;
    float zamanDurdurmaSuresi;
    public float zamanDurdurmaSuresiMax;
    public float saniyedeZamanYetenegiDolmaMiktari = 0.5f;
    int can = 100;

    public float jumpBoost ;   //ZIPLAMA BOOSTU

    private CharacterController characterController;
    private float ySpeed;
    private float originalStepOffset;
    private float? lastGroundedTime;
    private float? jumpButtonPressedTime;
    public float clocktoplayincaartanzaman = 2f;

    public TextMeshProUGUI  canTxt;
    public TextMeshProUGUI  zamandurdurmaTxt;

    public SpiderControl sc;

    public bool oyunDurduMu = false;

    [SerializeField] private Animator animator;
    public GameObject panel;
    public GameObject oyunBittimiScreen;
    public GameObject bolumbittiScreen;
    public Button nextlevel;
    public GameObject govde;

    public GameObject yilann;

    GameObject normal;
    GameObject yirtici;
    GameObject yirtici_boynuz;
    GameObject yirtici_yilan;
    GameObject yayli_ayak;
    GameObject yayli_boynuz;
    GameObject yayli_yilan;


    bool donme = false;
    public bool donebilme = false;

    public  bool yilanbilme = false;
     bool yilansaldiri = false;
    public float yilanmax;
   float yilananlik;

    public float donmehizi;
    public float donmesuresimax;
    float donmesuresi;
    public bool zamanDurduMu = false;

    void Start()
    {
        
        string evrim = PlayerPrefs.GetString("evrim");

        Debug.Log(evrim);
        if (evrim.Equals(""))
        {
            evrim = "normal";
            jumpBoost = 1f;
        }
        //PlayerPrefs.DeleteAll();//UNUTMA

        normal = transform.GetChild(0).gameObject;
        yirtici = transform.GetChild(1).gameObject;
        yirtici_boynuz = transform.GetChild(2).gameObject;
        yirtici_yilan = transform.GetChild(3).gameObject;
        yayli_ayak = transform.GetChild(4).gameObject;
        yayli_boynuz = transform.GetChild(5).gameObject;
        yayli_yilan = transform.GetChild(6).gameObject;



        normal.SetActive(false);
        yirtici.SetActive(false);
        yirtici_boynuz.SetActive(false);
        yirtici_yilan.SetActive(false);
        yayli_ayak.SetActive(false);
        yayli_boynuz.SetActive(false);
        yayli_yilan.SetActive(false);

        switch (evrim)
        {
            case "normal":
                normal.SetActive(true);
                govde = normal;
                break;
            case "yirtici":
                yirtici.SetActive(true);
                govde = yirtici;
                speed *= 1.5f;
                break;
            case "yirtici_boynuz":
                yirtici_boynuz.SetActive(true);
                govde = yirtici_boynuz;
                donebilme = true;
                speed *= 1.5f;
                break;
            case "yirtici_yilan":
                yirtici_yilan.SetActive(true);
                govde = yirtici_yilan;
                yilanbilme = true;
                speed *= 1.5f;
                break;
            case "yayli_ayak":
                yayli_ayak.SetActive(true);
                govde = yayli_ayak;
                jumpBoost = 1.5f;
                break;
            case "yayli_boynuz":
                yayli_boynuz.SetActive(true);
                govde = yayli_boynuz;
                jumpBoost = 1.5f;
                donebilme = true;
                break;
            case "yayli_yilan":
                yayli_yilan.SetActive(true);
                govde = yayli_yilan;
                jumpBoost = 1.5f;
                yilanbilme = true;
                break;
            default:
                // code block
                break;
        }

         


        

        zamanDurdurmaSuresi = zamanDurdurmaSuresiMax;
        characterController = GetComponent<CharacterController>();
        originalStepOffset = characterController.stepOffset;
        donmesuresi = donmesuresimax;
        oyunBittimiScreen.SetActive(false);
        //sc = GameObject.Find("SpiderParent").GetComponent<SpiderControl>();

        
    }

    void Update()
    {
        

        if (can <=0)
        {
            canTxt.text = "Can: " + 0;
            oyunDurduMu = true;
            oyunBittimiScreen.SetActive(true);
            panel.SetActive(false);
        }
        canTxt.text = "Can: " + can;
        zamandurdurmaTxt.text = "Zaman Durdurma: " + zamanDurdurmaSuresi.ToString("F1");
        //Debug.Log(oyunDurduMu);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!bolumbittiScreen.activeInHierarchy)
            {
                oyunDurduMu = !oyunDurduMu;
            }
            

        }
        if (oyunDurduMu && !bolumbittiScreen.activeInHierarchy && !oyunBittimiScreen.activeInHierarchy)
        {
            panel.SetActive(true);
        }
        else if (!oyunDurduMu)
        {
            panel.SetActive(false);


            if (Input.GetKeyDown(KeyCode.E) )
            {
                if (donmesuresi > 0 && donebilme)
                {
                    donme = true;
                }

                else if (yilanbilme && !yilansaldiri)
                {
                    yilansaldiri = true;
                }
            }


            if (yilansaldiri)
            {
                if (yilanbilme)
                {
                    Instantiate(yilann, transform.position, transform.rotation).AddComponent<Rigidbody>().AddForce(new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z + 1000));
                    Instantiate(yilann, transform.position, transform.rotation).AddComponent<Rigidbody>().AddForce(new Vector3(transform.position.x - 1000, transform.position.y, transform.position.z + 1000));
                    Instantiate(yilann, transform.position, transform.rotation).AddComponent<Rigidbody>().AddForce(new Vector3(transform.position.x + 1000, transform.position.y, transform.position.z - 1000));
                    Instantiate(yilann, transform.position, transform.rotation).AddComponent<Rigidbody>().AddForce(new Vector3(transform.position.x - 1000, transform.position.y, transform.position.z - 1000));
                }//yilansaldiri = false;
                yilanbilme = false;

                yilananlik += Time.deltaTime;
                sc.yilan = true;

                if (yilananlik >= yilanmax)
                {
                    yilansaldiri = false;
                    sc.yilan = false;
                }

            }

            if (donme && donmesuresi >0 && donebilme)
            {
                transform.GetChild(7).gameObject.SetActive(true);
                govde.SetActive(false);
                transform.GetChild(7).gameObject.transform.Rotate(0,  1 * Time.deltaTime * donmehizi ,  0);
                donmesuresi -= Time.deltaTime;
            }

            if (donmesuresi<=0)
            {
                donme = false;
                transform.GetChild(7).gameObject.SetActive(false);
                govde.SetActive(true);
            }


            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!zamanDurduMu && zamanDurdurmaSuresi > 0)
                {
                    zamanDurduMu = true;
                }
                else if (zamanDurduMu)
                {
                    zamanDurduMu = false;
                }

            }
            if (zamanDurduMu)
            {
                zamanDurdurmaSuresi -= Time.deltaTime;
            }
            else
            {
                if (zamanDurdurmaSuresi < zamanDurdurmaSuresiMax)
                {
                    zamanDurdurmaSuresi += (Time.deltaTime * saniyedeZamanYetenegiDolmaMiktari);
                }
            }

            if (zamanDurdurmaSuresi <= 0)
            {
                zamanDurduMu = false;
            }
            //Debug.Log(zamanDurdurmaSuresi);


            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
            float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;
            movementDirection.Normalize();

            ySpeed += Physics.gravity.y * Time.deltaTime * yMagnitude * (1 / jumpBoost);

            if (characterController.isGrounded)
            {
                lastGroundedTime = Time.time;
            }

            if (Input.GetButtonDown("Jump"))
            {
                jumpButtonPressedTime = Time.time;
            }

            if (Time.time - lastGroundedTime <= jumpButtonGracePeriod)
            {
                characterController.stepOffset = originalStepOffset;
                ySpeed = -0.5f;

                if (Time.time - jumpButtonPressedTime <= jumpButtonGracePeriod)
                {
                    ySpeed = jumpSpeed;
                    jumpButtonPressedTime = null;
                    lastGroundedTime = null;
                }
            }
            else
            {
                characterController.stepOffset = 0;
            }

            Vector3 velocity = movementDirection * magnitude;
            velocity.y = ySpeed;
            /*if (ySpeed >0)
            {
                animator.Play("turtleUp", 0, 0.0f);
            }
            else if (ySpeed <0 && !characterController.isGrounded)
            {
                animator.Play("turtleDown", 0, 0.0f);
            }
            else if (characterController.isGrounded && movementDirection != Vector3.zero)
            {
                animator.Play("turtleWalk", 0, 0.0f);
            }
            else if (characterController.isGrounded)
            {
                animator.Play("turtleIdle", 0, 0.0f);
            }*/

            characterController.Move(velocity * Time.deltaTime);

            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

                transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }


    public void OyunuDevamEttir()
    {
        //Debug.Log("AAAAAAAAAAAAAA");
        oyunDurduMu = false;

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("pervane"))
        {
            can -= 100;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("clock"))
        {
            if(zamanDurdurmaSuresi < (zamanDurdurmaSuresiMax- clocktoplayincaartanzaman))
            {
                zamanDurdurmaSuresi += clocktoplayincaartanzaman;
            }
            else
            {
                zamanDurdurmaSuresi = zamanDurdurmaSuresiMax;
            }
        
            Destroy(other.gameObject);

        }

        //Debug.Log("CARPMA");

        if (other.CompareTag("spider") && !oyunDurduMu && !donme)
        {
            can-= 10;
        }
        else if (other.CompareTag("spider") && !oyunDurduMu && donme && donebilme)
        {
            SpiderControl sc = other.gameObject.GetComponent<SpiderControl>();
            sc.can -= 50;
        }

       


        if (other.CompareTag("finish"))
        {
            panel.SetActive(false);
           // PlayerPrefs.SetString("evrim",govde.name);

            if (SceneManager.GetActiveScene().name.Equals("Level1"))
            {
                PlayerPrefs.SetString("bolum", "Level2");
            }
            else if (SceneManager.GetActiveScene().name.Equals("Level2"))
            {
                PlayerPrefs.SetString("bolum", "Level3");
            }




            bolumbittiScreen.SetActive(true);
            /*if (!SceneManager.GetActiveScene().name.Equals("Level3"))
            {
                nextlevel.enabled = true;
            }*/
            oyunDurduMu = true;



        }




    }
}