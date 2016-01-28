/// <summary>
/// Saul Aceves Montes 29/12/2015
/// Helicopter Gun 1.0
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
public class HelicopterGun : MonoBehaviour 
   {
    //Hardcore programing >:v
    //Only for galting gun, TODO: implements all gun types for more complexity c: 
    //External Data
    public HelicopterControl HC;
    //Controls and counters 
    public KeyCode KeyToActive;
    public int NumberOfBullets;
    //Speed
    public float Ratio;
    public float MinValueOfRotation;
    public float BulletForce;
    //Ohter
    public GameObject BulletPrefab;
    public Transform SpawnPoint;
    public Transform Barrel;
    //Audio
    public AudioSource GunAudioPoint;
    public AudioClip Brrrrrrt; // "gun sound >:v"
    //Particles
    public ParticleSystem Fire;
    public ParticleSystem Smoke;
    //Internal vars
    float NextTime;
    float BarrelSpeed;
    void Start() 
    {
        GunAudioPoint.clip = Brrrrrrt;
    }
    //Gun system
    void FixedUpdate () 
    {
        if (Input.GetKey(KeyToActive) && HC.UseHelicopter)
        {
            Debug.LogWarning("smoke disabled, edit script to enable");
            BarrelSpeed = Mathf.Lerp(BarrelSpeed, 1, 1 * Time.deltaTime); // to 1
            //Disparamos el arma
                if (Time.time > NextTime&&BarrelSpeed >= MinValueOfRotation && NumberOfBullets > 0)
                {
                    //Establecemos intervalos
                    NextTime = Time.time + Ratio;
                    NumberOfBullets--;
                    GunAudioPoint.PlayOneShot(Brrrrrrt);
                    Fire.Play();
            //        Smoke.Play();
                    //Instanciamos el proyectil 
                    GameObject Clone = Instantiate(BulletPrefab, SpawnPoint.position, SpawnPoint.rotation) as GameObject;
                    Clone.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * BulletForce);
                }
            }
        else 
        {
            BarrelSpeed = Mathf.Lerp(BarrelSpeed, 0, 1 * Time.deltaTime); // to 0
            Fire.Stop();
         //   Smoke.Stop();
        }
        //Asignamos la informacion
        Barrel.Rotate(Vector3.forward * BarrelSpeed*360);
    }
}
