using UnityEngine;
using System.Collections;

//TODO: IMPLEMENTAR UN SISTEMA DE DANO...
//<INSERTE LICENCIA AQUI EN CASO DE IMPLEMENTAR EL SISTEMA>
public class RotorDamage : MonoBehaviour
{
    public float OnCollicionDamageMultiplier;
    public float CurrentDamage;
    void OnCollisionEnter(Collision col) 
    {
        Debug.Log("el objeto " + this.name + "ha colicionado con" + col.gameObject.name);
        //No tiene que ser un trigger
        if (!col.collider.isTrigger) 
        {
            CurrentDamage = CurrentDamage + OnCollicionDamageMultiplier;
        }
    
    }

}
