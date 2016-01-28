/// <summary>
/// Saul Aceves Montes 29/12/2015
/// MouseLookFPS 1.0
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
public class MouseLookFPS : MonoBehaviour  {
    //Data requests
    public CameraManager Mg;    
    public Transform FirstPersonCameraTarget;
    //MouseLook
    public float LookSpeed;
    public float LerpSpeed;
    //Camera Movement Effect
    public float Amount;
    public float ReturnSpeed;
    public Vector3 Direction;
    public float MaxZoom= 70;
    float         Zoom=60;//Current camera zoom
    //Inputs
    public float InputX;
    public float InputY;
    //for lerp
    float InputXlerp;
    float InputYlerp;
    void Update() 
    {
        if (Input.GetKey(Mg.KeyToActive)) 
        {
            //Ocultamos el cursor 
            Cursor.lockState = CursorLockMode.Locked;
            //Obtenemos el input
            if (Mg.ControlType ==  CameraManager.CONTROL_TYPE.Mouse)
            {
                InputX += Input.GetAxis("Mouse X") * LookSpeed*(Zoom/MaxZoom);
                InputY -= Input.GetAxis("Mouse Y") * LookSpeed * (Zoom / MaxZoom);
                Zoom -= Input.GetAxis("Mouse ScrollWheel") * 8.0f;
                Zoom = Mathf.Clamp(Zoom, 10, MaxZoom);
                 Mg.Camara.GetComponent<Camera>().fieldOfView =Zoom;
            }
            else if (Mg.ControlType == CameraManager.CONTROL_TYPE.JoyStick)
            {
                InputX += Input.GetAxis("Horizontal") * LookSpeed;
                InputY -= Input.GetAxis("Vertical") * LookSpeed;
            }
        }
        else 
        {
        //Mostramos el cursor de nuevo
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        //Asignamos los valores obtenidos
        //hacemos calculos para la interpolacion
        InputXlerp = Mathf.Lerp(InputXlerp, InputX, LerpSpeed * Time.smoothDeltaTime);
        InputYlerp = Mathf.Lerp(InputYlerp, InputY, LerpSpeed * Time.smoothDeltaTime);
        //Quitar la suma de x + ylerp en caso de que no haya problemas con eso de la horientacion del modelo c: 
        Vector3 CameraRotation = new Vector3(InputYlerp, InputXlerp);
        Mg.Camara.GetComponent<Transform>().localRotation = Quaternion.Euler(CameraRotation);
        //Movement Amount 
        // Amount Input!:v
        Direction = (Mg.MHC.Parts.Chasis.GetComponent<Rigidbody>().velocity.normalized);//Position
        Mg.Camara.transform.position = Vector3.Lerp(Mg.Camara.transform.position,FirstPersonCameraTarget.position-Direction*Amount,ReturnSpeed*Time.deltaTime);
        
    }

}
