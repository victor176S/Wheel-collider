using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using VicGenLib.Logic;

public class Movement : MonoBehaviour
{

    [Header("Solo es necesario asignar aqui los controles")]

    public ControlsDetector controls;

    private Animator animator;

    public GameObject ruedas;

    public Rigidbody rb;

    [SerializeField] private float velocidad;

    public float velocidadBase, anguloInclinacionSuelo;

    public bool enSuelo, alreadyActivated;

    void Awake()
    {
       velocidadBase = velocidad; 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void OnLevelWasLoaded(int level)
    {
        this.gameObject.transform.GetChild(0).gameObject.transform.localRotation = quaternion.Euler(0,0,0);
    }
    void Start()
    {
        ruedas = this.gameObject.transform.GetChild(1).GetChild(1).gameObject;

        rb = this.gameObject.GetComponent<Rigidbody>();

        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        UnityEngine.Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        CameraMov();
    }

    void FixedUpdate()
    {
        Movimiento();
    }

    void OnCollisionEnter(Collision collision)
    {
        
    }

    /*void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {

        if (collision.gameObject.CompareTag("Suelo"))
        {
            enSuelo = false;
        }
    }*/

    private void Movimiento()
    {

        if (controls.SpaceM)
        {
            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().brakeTorque = rb.mass * 10000 * velocidad;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().brakeTorque = rb.mass * 10000 * velocidad;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().brakeTorque = rb.mass * 10000 * velocidad;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().brakeTorque = rb.mass * 10000 * velocidad;

            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
        }
        else
        {
            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().brakeTorque = 0;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().brakeTorque = 0;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().brakeTorque = 0;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().brakeTorque = 0;
        }

        if (controls.ShiftM)
        {
            velocidad = velocidadBase * 1.5f;
        }
        else
        {
            velocidad = velocidadBase; 
        }

        if (controls.AM)
        {
           
        }

        if (controls.WM)
        {
            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().motorTorque = rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().motorTorque = rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().motorTorque = rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().motorTorque = rb.mass * velocidad * 1000;
        }

        if (controls.SM)
        {
            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().motorTorque = -rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().motorTorque = -rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().motorTorque = -rb.mass * velocidad * 1000;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().motorTorque = -rb.mass * velocidad * 1000;
        }

        if (controls.DM)
        {
            
        }

        if (rb.linearVelocity.magnitude > velocidad)
        {
            Vector3 vel = rb.linearVelocity.normalized * velocidad;
            vel.y = rb.linearVelocity.y;
            rb.linearVelocity = vel;
        }

        if (!Input.anyKey)
        {
            ruedas.transform.GetChild(0).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(1).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(2).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
            ruedas.transform.GetChild(3).gameObject.GetComponent<WheelCollider>().motorTorque = 0;
        }
    }

    private void CameraMov()
    {

        //Movimiento ratón
        this.gameObject.transform.GetChild(2).gameObject.transform.localRotation = Quaternion.Euler(controls.xRotation, controls.yRotation, 0);

    }
}
