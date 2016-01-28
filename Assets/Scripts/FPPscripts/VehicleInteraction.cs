/// <summary>
/// Saul Aceves Montes 29/12/2015
/// VehicleInteraction 1.0 
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
public class VehicleInteraction : MonoBehaviour 
{
    //Variables
    public Transform Player;
    public float CurrentDistance;
    public float MinDistanceToEnter;
    public bool ToogleKey;
    public bool ShowHint;


    //metodos propios
    void OnEnter() 
    {
    //desactivamos la opcion "is out of any vehicle" del script FPP y tambien la opcion de "show hint"
    ShowHint = false;
    Player.GetComponentInChildren<FPP>().IsOutOfAnyVehicle = false;
    //Activamos el uso del helicoptero, desactivamos el character controller del player y lo transportamos siempre hacia donde este este punto
    transform.parent.GetComponent<HelicopterControl>().UseHelicopter = true;
    Player.GetComponent<CharacterController>().enabled = false;
    Player.transform.position = transform.position;

    }

    void OnExit() 
    {
        //Hacemos lo contrario que la funcion on Enter
        //Activamos  la opcion "is out of any vehicle" del script FPP
        Player.GetComponentInChildren<FPP>().IsOutOfAnyVehicle = true;
        //desactivamos el uso del helicoptero, activamos el character controller del player 
        transform.parent.GetComponent<HelicopterControl>().UseHelicopter = false;
        Player.GetComponent<CharacterController>().enabled = true;
        Player.transform.position = transform.position;
    }
    //Metodos predefinidos
    void Update() 
    {
        if (Player)
        {

            //GET ALL VALUES
            CurrentDistance = Vector3.Distance(transform.position, Player.position);
            if (Input.GetKeyDown(KeyCode.E))
                ToogleKey = !ToogleKey;

            //APPLY THE DATA
            if (CurrentDistance < MinDistanceToEnter)
            {
                //Si el jugador preciona la tecla e entramos en el vehiculo
                if (ToogleKey)
                    OnEnter();
                else if (Input.GetKeyUp(KeyCode.E)&& !ToogleKey)
                    OnExit();
                else
                    ShowHint = true;
            }
            else
                ShowHint = false;
        }
       
    }

    //GUI OUTPOUT
    void OnGUI() 
    {
        GUI.color = Color.green;
        if (ShowHint)
            GUI.Label(new Rect((Screen.width / 2)-50 , Screen.height / 2, 200, 30), "Press E key to enter");
    }


}
