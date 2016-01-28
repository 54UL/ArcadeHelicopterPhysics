/// <summary>
/// Saul Aceves Montes 29/12/2015
/// Camera Manager 1.0
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
public class CameraManager : MonoBehaviour 
{
    //Enumeraciones
    public enum CONTROL_TYPE { Mouse, JoyStick};
    //-----------------------------------------Variables
    public HelicopterControl MHC;
    public GameObject Camara;
    public CONTROL_TYPE ControlType = CONTROL_TYPE.Mouse;
    public KeyCode KeyToActive;
    //change camera controlsa
    public bool Mode; // 1 Mouse look ,0 Mouse Orbit
    public KeyCode KeyToChangeCamera;

    void Update() 
    {
    //Pillamos el input
        if (Input.GetKeyDown(KeyToChangeCamera))
            Mode = !Mode;

        //Asignamos todos los valores
        if (MHC.UseHelicopter)
        {
            gameObject.GetComponent<MouseLookFPS>().enabled = Mode;
            //low pass onlye enbaled with mouse look fps 
            Camara.GetComponent<AudioLowPassFilter>().enabled = gameObject.GetComponent<MouseLookFPS>().enabled;
            gameObject.GetComponent<MouseOrbit>().enabled = !Mode;
        }
        else 
        {
            gameObject.GetComponent<MouseLookFPS>().enabled = false;
            gameObject.GetComponent<MouseOrbit>().enabled = false;
            Camara.GetComponent<AudioLowPassFilter>().enabled = false;
      
        }


            Camara.SetActive(MHC.UseHelicopter);
        }
}
