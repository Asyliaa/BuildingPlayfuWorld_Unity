using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//de onderdelen over de first person controller komen van het script uit de les. Ik heb in dit script zelf er nog veel bijgeschreven,
//aan de hand van tutorials, maar ook mijn eigen code geschreven.

public class Controller : MonoBehaviour
{
    //movement and such
    public GameObject cam;
    public float moveSpeed = 3f;
    public float mouseSensitivityX, mouseSensitivityY;
    private float verticalAxis;
    private float horizontalAxis;
    private float angleX, angleY;
    private float mouseX, mouseY;
    private Rigidbody rigidBody;

    //jumping 
    private bool jump;
    public float jumpForce = 200;
    public float rayCastRange = 0.25f;

    //texts 
    public Text countText;
    public int count;
    public Text winText;
    public Text infoText;
    public Text controlText;
    public Text scoreText; 


    //andere script van de timer importeren zodat ik die kan gebruiken om de eindscore weer te geven
    public TimerScript timerScript;
  


    void Start()
    {
        //texten leeg zetten
        rigidBody = GetComponent<Rigidbody>();
        count = 0;
        countText.text = "Count: " + count.ToString() + " / 10";
        winText.text = "";
        infoText.text = "";
        controlText.text = ""; 
        scoreText.text = "";


    }

    // Update is called once per frame
    void Update()
    {
        //Get input from WASD
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");

        //Get Input from the mouse movement
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        //Input for Jump
        jump = Input.GetKeyDown(KeyCode.Space);
        Debug.DrawLine(transform.position, transform.position + -transform.up * rayCastRange);
        if (jump)
        {
            DoJump();
            
        }


        //Look Up and Down
        angleY += mouseY * mouseSensitivityY;
        angleY = Mathf.Clamp(angleY, -89f, 89f);
        cam.transform.localRotation = Quaternion.Euler(-angleY, 0, 0);

        //Look Right and Left
        angleX += mouseX * mouseSensitivityX;
        transform.rotation = Quaternion.Euler(0, angleX, 0);

      
    }

    private void FixedUpdate()
    {
        //Move the Player with Physics
        Vector3 forwardMovement = transform.forward * verticalAxis;
        Vector3 sideMovement = transform.right * horizontalAxis;
        rigidBody.MovePosition(rigidBody.position + (forwardMovement + sideMovement).normalized * moveSpeed * Time.deltaTime);
    }

    //jumping 
    private void DoJump()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.up * 0.1f, -transform.up, out hit, rayCastRange))
        {
            rigidBody.AddForce(transform.up * jumpForce);
            //Debug omdat het jumpen niet wilde werken.
            Debug.Log("HALLO");
        }
    }

    //trigger voor pick ups en voor de informatie. 
    void OnTriggerEnter(Collider other)
    {
        //als je een pick up object oppakt dan gaat het object weg en telt je score op.
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }

        //als je in de trigger voor informatie loopt, roept hij die functie aan en krijg je informatie te zien. 
        if (other.gameObject.CompareTag("Info"))
        {
            SetInfoText();
        }

        
    }


    //win text , hij kijkt of je alle objecten hebt verzameld (10) en als dat zo is krijg je text te zien dat je hebt gewonnen.
    //Ook zie je dan hoe lang je erover hebt gedaan, de eindtijd wordt weergegeven. 
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / 10";
        if (count >= 10)
        {
            winText.text = "Good job! You collected all the lost fairy dust and can now return back to your homeland. ";
            scoreText.text = "Your score is: " + timerScript.counterText.text;

        }
    }



    private void OnTriggerExit(Collider other)
    {
        //als je uit de info trigger loopt, gaat de text met informatie weer uit je scherm. 
        if (other.gameObject.CompareTag("Info"))
        {
            SetInfoDelText();
        }
    }


    void SetInfoText()
    {
        //de informatie text die je krijgt als je de info trigger inloopt.
        infoText.text = "Oh no! When you hit your head a moment ago, you woke up in a place you don't know! You lost almost all of your magic powers and to return home you have to find fairy dust, which will restore it. ";
        controlText.text = "Use wasd to move and your mouse to look around. You can jump by pressing the spacebar and double jump by pressing it twice. ";
    }

    void SetInfoDelText()
    {
        //deze functie zet de informatie text weer op leeg, zodat hij niet meer in je beeld staat als je uit de info trigger loopt.
        infoText.text = "";
        controlText.text = "";
    }




}