/// <summary>
/// Saul Aceves Montes 29/12/2015
/// Basic Helicopter GUI interface 1.0
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
public class HelicopterGUInterface : MonoBehaviour
{
    //TODO: METODO ANTIGUO, MEJORAR O IMPLEMENAR UNA INTERFAZ CON EL NUEVO SISTEMA DE GUI(unity 5.0)
    //Data 
    public HelicopterControl HelicopterData;
    public HelicopterGun GunData;
    public Transform Altimeter;
    //Update
    RaycastHit rhit;
    void Update() 
    {
        //Calculamos la altura
        Physics.Raycast(Altimeter.position, Vector3.down, out rhit);
    }
    //GUI OUTPOUT
    void OnGUI() 
    {
        GUI.color = Color.green;
        GUI.BeginGroup(new Rect(20, 20, 500, 500));
        GUI.Box(new Rect(0, 0, 160, 90), "");
        //Indicators Labels
        GUI.Label(new Rect(20, 10, 150, 30), "Power: " + (HelicopterData.Control.Throttle * 10).ToString("F2") + "%");
        GUI.Label(new Rect(20,25,100,30),"Alture: "+rhit.distance.ToString("F2")+" Mts");
        GUI.Label(new Rect(20, 40, 110, 30), "Speed: "+(HelicopterData.Parts.Chasis.GetComponent<Rigidbody>().velocity.magnitude*3.6f).ToString("F1")+" K/H"); //ms -> kmh
        GUI.Label(new Rect(20, 55, 120, 30), "Ammo(GAU-8): " + GunData.NumberOfBullets);
        GUI.EndGroup();
    }
}
