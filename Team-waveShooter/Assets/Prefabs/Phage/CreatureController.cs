using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatureController : MonoBehaviour {
    public float moveInputFactor = 5f;
    public Vector3 velocity;
    public float walkSpeed = 2f;
    public float sprintSpeed = 10f;
    public float rotateInputFactor = 10f;
    public float rotationSpeed = 10f;
    public float averageRotationRadius = 3f;
    private float rSpeed = 0;
    private GameObject cam;
    private Vector3 forward;
    private Vector3 sideways;
    private Vector3 target;
    public int health = 100;
    private bool invinsible= false;
    private float temp;
    public ProceduralLegPlacement[] legs;
    private int index;
    public bool dynamicGait = false;
    public float timeBetweenSteps = 0.25f;
    [Tooltip ("Used if dynamicGait is true to calculate timeBetweenSteps")] public float maxTargetDistance = 1f;
    public float lastStep = 0;

    public RectTransform healthRect;
    public Text healthText;

    void Start () {
        cam = GameObject.Find("Main Camera");
        temp = walkSpeed;

        healthRect = GameObject.Find("HUD/Health/healthVal").GetComponent<RectTransform>();
        healthText = GameObject.Find("HUD/Health/healthText").GetComponent<Text>();
    }

    void Update () {

        healthRect.sizeDelta = new Vector2(health, 25);
        healthText.text = "Health: " + health;

        float mSpeed = (Input.GetButton ("Fire3") ? sprintSpeed : walkSpeed);
        //rSpeed = Mathf.MoveTowards (rSpeed, Input.GetAxis ("Turn") * rotationSpeed, rotateInputFactor * Time.deltaTime);

        //Forward from camera to player, sideways to... the side.
        forward = transform.position - cam.transform.position;
        forward = new Vector3(forward.x,0f,forward.z).normalized;
        sideways = -Vector3.Cross(forward, Vector3.up);

        //Velcotiy vector "moves towards" the target.
        target = (sideways* Input.GetAxis("Horizontal")) + (forward * Input.GetAxis("Vertical"));
        velocity = Vector3.MoveTowards(velocity, target.normalized, Time.deltaTime * moveInputFactor);
        //transform.Rotate(0f, rSpeed * Time.deltaTime, 0f);
        transform.position += velocity * mSpeed * Time.deltaTime;

        if (dynamicGait) {
            timeBetweenSteps = maxTargetDistance / Mathf.Max (mSpeed * velocity.magnitude, Mathf.Abs (rSpeed * Mathf.Deg2Rad * averageRotationRadius));
        }

        if (Time.time > lastStep + (timeBetweenSteps / legs.Length) && legs != null) {
            if (legs[index] == null) return;

            Vector3 legPoint = (legs[index].restingPosition + velocity);
            Vector3 legDirection = legPoint - transform.position;
            //Vector3 rotationalPoint = (legs[index].transform.TransformDirection (Vector3.right)) * (rSpeed * Mathf.Deg2Rad * (legPoint - transform.position).magnitude);
            Vector3 rotationalPoint = ((Quaternion.Euler (0, rSpeed / 2f, 0) * legDirection) + transform.position) - legPoint;
            Debug.DrawRay (legPoint, rotationalPoint, Color.black, 1f);
            Vector3 rVelocity = rotationalPoint + velocity;

            legs[index].stepDuration = Mathf.Min (0.5f, timeBetweenSteps / 2f);
            legs[index].worldVelocity = rVelocity;
            legs[index].Step ();
            lastStep = Time.time;
            index = (index + 1) % legs.Length;
        }
    }

    void OnTriggerEnter( Collider other)
    {
        if (other.gameObject.tag == "hole" && GameObject.Find("playField").GetComponent<fieldGenerator>().score == GameObject.Find("playField").GetComponent<fieldGenerator>().vCount)
        {
            GameObject.Find("playField").GetComponent<fieldGenerator>().level += 1;
            GameObject.Find("playField").GetComponent<fieldGenerator>().vCount = GameObject.Find("playField").GetComponent<fieldGenerator>().level * 10;
            GameObject.Find("playField").GetComponent<fieldGenerator>().totScore += GameObject.Find("playField").GetComponent<fieldGenerator>().score;
            GameObject.Find("playField").GetComponent<fieldGenerator>().score = 0;
        }
        if (other.gameObject.tag == "virus")
        {
            if (!invinsible)
            {
                health -= 5;
            }

        }

        if (other.gameObject.tag == "speedPowerUp") {
            
            walkSpeed = 14;
            StartCoroutine(PowerUp(5f));
            Debug.Log("collected");
            other.gameObject.SetActive(false);

        }


        if (other.gameObject.tag == "healthPowerUp") {

            health += 20;
            other.gameObject.SetActive(false);

        }

        if (other.gameObject.tag == "invinsiblePowerUp") {

            invinsible = true;
            StartCoroutine(PowerUp2(5f));
            other.gameObject.SetActive(false);

        }
    }

    IEnumerator PowerUp(float duration) {
        Debug.Log("Power up started");

       
        yield return new WaitForSeconds(duration);
        walkSpeed = temp;

        Debug.Log("Power up ended");
    }


    IEnumerator PowerUp2(float duration) {
        Debug.Log("Power up started");


        yield return new WaitForSeconds(duration);
        invinsible = false;

        Debug.Log("Power up ended");
    }

    public void OnDrawGizmosSelected () {
        Gizmos.DrawWireSphere (transform.position, averageRotationRadius);
    }
}
