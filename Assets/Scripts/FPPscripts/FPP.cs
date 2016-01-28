/// <summary>
/// Saul Aceves Montes 29/12/2015
/// FirstPersonPlayer Script 1.0
/// 
/// -LICENCIA (ZLIB)
/// 
/// Derechos de autor (c) <2015> <Saul Aceves>
/// Este software se proporciona "tal cual", sin ninguna garantía expresa o implícita. En ningún caso los autores ser declarados responsables de los daños y perjuicios derivados de la utilización de este software.
/// Se concede permiso para que cualquiera pueda utilizar este software para cualquier propósito, incluyendo aplicaciones comerciales, y para alterarlo y redistribuirlo libremente, sujeto a las siguientes restricciones:
/// 1. El origen de este software no debe ser tergiversado; no hay que decir que usted escribió el software original. Si utiliza este software en un producto, un reconocimiento en la documentación del producto sería apreciada pero no se requiere.
//  2. Las versiones alteradas de la fuente deben estar claramente identificados como tales, y no deben ser tergiversados ​​como el software original.
//  3. Este aviso no puede ser suprimida o alterada de cualquier distribución de código fuente.
/// 
/// -Contacto
/// 54ulxd@gmail.com 
/// </summary>


using UnityEngine;
using System.Collections;
public class FPP : MonoBehaviour
{
    //Clases
    //Mouse look class
    [System.Serializable]
    public class ML
    {
        public GameObject MainBody;
        public float LookSpeed = 1;
        public float HorizontalLerpSpeed = 1;
        public float VerticalLerpSpeed = 1;
        public float VerticalAxisLimit = 90;   // Vertical axis clamp
        //Variables Internas
        public float MouseX; // Current Mouse input in X
        public float MouseY;//  Current Mouse input in Y
        public float MouseXlerp;
        public float MouseYlerp;
    };
    //Movement control Class
    [System.Serializable]
    public class MVC
    {
        public float VerticalSpeed = 1;   //foward and backward  speed
        public float HorizontalSpeed = 1; //Left and roght speed;
        //TODO: IMPLEMENTAR CAPACIDAD PARA SALTAR >:V
        // public float JumpForce = 4; 
        public float GravityForce = -9.81f;
        //Sound
        public AudioSource AudioPoint;
        public AudioClip FootStep;
        public float StepRatio = 0.3f;
        public float MinSpeedToPlay = 0.4f;
        //Varaibles internas
        public float InputX;
        public float InpuY;
    };
    //Variables del script
    public bool IsOutOfAnyVehicle = true;
    public ML MouseLook;
    public MVC MovementControl;
    //Variables internas >:V
    float Ntime;// for foot steps ratio effect
    //Metodos
    void FPPinput()//Gets all input of the keyboard and mouse 
    {
        //MOUSE 
        MouseLook.MouseX += Input.GetAxis("Mouse X") * MouseLook.LookSpeed;
        MouseLook.MouseY -= Input.GetAxis("Mouse Y") * MouseLook.LookSpeed;
        //Keyboard
        MovementControl.InputX = Input.GetAxis("Horizontal2") * MovementControl.HorizontalSpeed;
        MovementControl.InpuY = Input.GetAxis("Vertical2") * MovementControl.HorizontalSpeed;
    }
    void OnGround()
    {
        //Mouse look
        //el transform actual deberia ser la camara
        //Creaamos las interpolaciones necesarias pero primero hacemos el clamp
        MouseLook.MouseY = Mathf.Clamp(MouseLook.MouseY, -MouseLook.VerticalAxisLimit, MouseLook.VerticalAxisLimit);
        MouseLook.MouseXlerp = Mathf.Lerp(MouseLook.MouseXlerp, MouseLook.MouseX, MouseLook.HorizontalLerpSpeed * Time.deltaTime);
        MouseLook.MouseYlerp = Mathf.Lerp(MouseLook.MouseYlerp, MouseLook.MouseY, MouseLook.VerticalLerpSpeed * Time.deltaTime);
        //asginamos valores
        transform.localRotation = Quaternion.Euler(new Vector3(MouseLook.MouseYlerp, 0, 0));//Camra
        MouseLook.MainBody.transform.rotation = Quaternion.Euler(new Vector3(0, MouseLook.MouseXlerp, 0));//body
        //Movement Control
        Vector3 MovementDirection = new Vector3(MovementControl.InputX * MovementControl.HorizontalSpeed,0, MovementControl.InpuY * MovementControl.VerticalSpeed);
        Vector3 Dir = MouseLook.MainBody.transform.TransformDirection(MovementDirection);
        MouseLook.MainBody.GetComponent<CharacterController>().SimpleMove(Dir);

        if ( MouseLook.MainBody.GetComponent<CharacterController>().isGrounded)
        {
            //Reproducimos los sonidos
            if (Time.time > Ntime && MouseLook.MainBody.GetComponent<CharacterController>().velocity.magnitude > MovementControl.MinSpeedToPlay)
            {
                //Configuramos el interavalo
                Ntime = Time.time + MovementControl.StepRatio;
                MovementControl.AudioPoint.clip = MovementControl.FootStep;
                MovementControl.AudioPoint.Play();
            }
        }


    }
    //Output methods
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        if (IsOutOfAnyVehicle)
        {
            FPPinput();
            OnGround();
            transform.GetComponent<Camera>().enabled = true;
        }
        else
            transform.GetComponent<Camera>().enabled = false;
    }






  


}


