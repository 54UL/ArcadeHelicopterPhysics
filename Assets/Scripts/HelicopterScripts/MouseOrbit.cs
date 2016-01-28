/// <summary>
/// Saul Aceves Montes 29/12/2015
/// MouseOrbit Script 1.0
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

//Traditional Mouse Orbit 
public class MouseOrbit : MonoBehaviour
{

    //External Scripts
    public CameraManager Manager;    
    //Inputs
    //Objects
    public Transform Target;
    //Mouse look
    public float LookSpeed;
    public float LookLerpSpeed;
    public float Distance;
    //Camera Follow
    public   float FollowSpeed;

    //Internal Script vars
    //Input
    float MouseX;
    float MouseY;
    //Lerp
    float MouseXlerp;
    float MouseYlerp;
    Vector3 FollowLerp;
    //Vectors
    Quaternion Rotation;
    Vector3 Position;

    //Methods
    void Update() 
    {
        if (Input.GetKey(Manager.KeyToActive)) 
        {

            if (Manager.ControlType == CameraManager.CONTROL_TYPE.Mouse) 
            {
                //Get Input
                MouseX -= Input.GetAxis("Mouse Y") * LookSpeed;
                MouseY += Input.GetAxis("Mouse X") * LookSpeed;
                Distance -= Input.GetAxis("Mouse ScrollWheel") * 8.0f;
                //Metemos esto aqui nada mas para que no joda afuera...
                //Ocultamos el cursor 
                Cursor.lockState = CursorLockMode.Locked;
            }

            else if (Manager.ControlType == CameraManager.CONTROL_TYPE.JoyStick) 
            {
                //TODO: IMPLEMENTAR CONTROL CON JOYSTICK >:V
                Debug.Log("NO CONTROL... in process"); 
            }
            }
        else 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //follow rotation
            //TODO: IMPLEMENTAR  SEGUIMIENTO ANGULAR >:V
        }
        //Mouse Look
        MouseXlerp = Mathf.Lerp(MouseXlerp, MouseX, LookLerpSpeed * Time.deltaTime);
        MouseYlerp = Mathf.Lerp(MouseYlerp, MouseY, LookLerpSpeed * Time.deltaTime);
        Rotation = Quaternion.Euler(new Vector3(MouseXlerp, MouseYlerp, 0));// this
        Position = Rotation * (new Vector3(0, 0, -Distance)) + FollowLerp;//   this
        //Hacemos el lerp para el seguimiento 
        FollowLerp = Vector3.Lerp(FollowLerp, Target.position, FollowSpeed * Time.deltaTime);// asignacion 
        //Asignamos valores
        Manager.Camara.transform.position = Position;
        Manager.Camara.transform.rotation = Rotation;
    }

}
