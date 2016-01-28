/// <summary>
/// Saul Aceves Montes 29/12/2015
/// Helicopter Script 1.0 
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
public class HelicopterControl : MonoBehaviour {
   
    //Definiciones de clases 
    [System.Serializable]
    public class HelicopterParts 
    {
        //Helices
        //Main
        public Vector3 RotationAxisMainRotor;
        public GameObject MainRotor;
        //Tail
        public Vector3 RotationAxisTailRotor;
        public GameObject TailRotor;
        //Chasis and others
        //public float ChasisDamage;
        public GameObject Chasis;
    }
    [System.Serializable]
    public class HelicopterSettings
    {
        //Motor settings
        public float MotorForce;
        public Vector3 MainForceDir;//Direccion configurable, esto se debe a que no en todos los modelos tienen la misma horientacion   
       //Control force settings
        public float ThrotleSpeed;//Speed multiplier (throttle)
        public float PitchForce;
        public float RollForce;
        public float YawForce;
        //RigidBody Settings
        public Transform CenterOfMass;
    }
    //Enum :v
    public enum CT { Keyboard,JoyStick,KeyBoardAndMouse};
    //Helicopter Control
    [System.Serializable]
    public class HC  
    {
     public CT ControlType=CT.Keyboard;
     //------------Inputs------------//
     //Control options
     public float Throttle;// Force Multiplier
     //Movement
     public float Roll;
     public float Pitch;
     public float Yaw;
    //Por el momento  me interesa que esten ocultas c: 
    [HideInInspector]
     public float ReturnSpeed=1;
    [HideInInspector]
    public bool ClampValues = true;
    }

    //Audio
    [System.Serializable]
    public class AudioH
    {
        public AudioSource AudioPoint;
        public AudioClip IdleMotorSound;
        public float PitchMultiplier;
        public float VolumeMultiplier;
        
    };

    //Variables y instancias de clases
    public bool UseHelicopter;// Control Enabled For the helicopter ??
    public HelicopterParts Parts;
    public AudioH HelicopterSound;
    public HelicopterSettings Settings;
    public HC Control;




    //obtenemos todo el input necesairo
    void InputControl(HC c) 
    {
        switch (c.ControlType) 
        {
            //KeyBoard Control
            case CT.Keyboard:
                // W AND S FOR THROTTLE, A & D FOR YAW , ARROWS FOR PITCH(up, down) and ROLL(right,left) 
                //throttle 
                if (Input.GetKey(KeyCode.W))
                    c.Throttle += Settings.ThrotleSpeed * Time.deltaTime;
                else if(Input.GetKey(KeyCode.S))
                    c.Throttle -= Settings.ThrotleSpeed * Time.deltaTime;
                //pitch roll yaw control
                //YAW
                if (Input.GetKey(KeyCode.D) )
                    c.Yaw += 1 * Time.deltaTime;
                else if (Input.GetKey(KeyCode.A))
                    c.Yaw -= 1 * Time.deltaTime;
                else
                    c.Yaw = Mathf.Lerp(c.Yaw, 0, c.ReturnSpeed*Time.deltaTime);
                //Pitch
                if (Input.GetKey(KeyCode.UpArrow))
                    c.Pitch += 1 * Time.deltaTime;
                    else if (Input.GetKey(KeyCode.DownArrow))
                    c.Pitch -= 1 * Time.deltaTime;
                else 
                    c.Pitch = Mathf.Lerp(c.Pitch, 0, c.ReturnSpeed*Time.deltaTime);
                //Roll
                if (Input.GetKey(KeyCode.RightArrow))
                    c.Roll += 1 * Time.deltaTime; 
                else if (Input.GetKey(KeyCode.LeftArrow))
                    c.Roll -= 1 * Time.deltaTime;
                else 
                    c.Roll = Mathf.Lerp(c.Roll, 0, c.ReturnSpeed * Time.deltaTime);

                if (c.ClampValues) 
                {
                    c.Roll = Mathf.Clamp(c.Roll, -1, 1);
                    c.Pitch = Mathf.Clamp(c.Pitch, -1, 1);
                    c.Yaw = Mathf.Clamp(c.Yaw, -1, 1);
                }
            break;
            
            //JoyStickControl pitch (vertical joystick axe), roll (horizontal axe),  buttons 4 and 5 for yaw, LOGITECH ATTACK 3
            case CT.JoyStick:

                //Axes
            c.Pitch = Input.GetAxis("Vertical");
            c.Roll = Input.GetAxis("Horizontal");
            
                

                //Buttons
                //trhottle
            if (Input.GetKey(KeyCode.Joystick1Button2))
                c.Throttle += Settings.ThrotleSpeed * Time.deltaTime;
            else if (Input.GetKey(KeyCode.Joystick1Button1))
                c.Throttle -= Settings.ThrotleSpeed * Time.deltaTime;
            

            //YAW
            if (Input.GetKey(KeyCode.Joystick1Button4))
                c.Yaw += 1 * Time.deltaTime;
            else if (Input.GetKey(KeyCode.Joystick1Button3))
                c.Yaw -= 1 * Time.deltaTime;
            else
                c.Yaw = Mathf.Lerp(c.Yaw, 0, c.ReturnSpeed * Time.deltaTime);

            if (c.ClampValues)
                c.Yaw = Mathf.Clamp(c.Yaw, -1, 1);
            break;


            //Mouse and key board (no default case)
            case CT.KeyBoardAndMouse:
                //coming son...



            break;
               
            default:
            Debug.LogError("Wtf nigga??? (no control selected)");
            break;

        }
        //Generals
        c.Throttle = Mathf.Clamp(c.Throttle, 0, 10);
    
    }
    //Control del motor y de mecanica extra c: 
    void Hmotor() 
    {
        // MEJORAR LA MECANICA DEL MOTOR Y ANADIR COSAS NUEVAS C:

        //"Set the horse power", realmente esto no son caballos de fuerza... pero no encontre otro nombre cual ponerle
        float HP = (Settings.MotorForce *Control.Throttle);    // es la potencia actual HP, no son horse power exactos si no es mi propia unidad de medida llamada horse power :v
        float RotationPower = HP / (Settings.MotorForce * 10); // este es el porcentaje normalizado de la potencia del motor
         // Debug.Log(HP);


        //-----------------------------------------------------------Main rotor
        //aplicamos la fuerza sobre el chasis y verificamos los danos del rotor 
        if (Parts.MainRotor.GetComponent<RotorDamage>().CurrentDamage < 100)
        {
            //la parte del pitch, roll y el trhottle lo llevara el rotor principal
            //Simulacion de la rotacion
            Parts.MainRotor.transform.Rotate(Parts.RotationAxisMainRotor,Mathf.Lerp(0,HP,0.36f*Time.deltaTime));//Rotation
            //damos la fuerza principal del helicoptero 
            Parts.Chasis.GetComponent<Rigidbody>().AddRelativeForce(Settings.MainForceDir* HP);


            //Main Sound
            if (!HelicopterSound.AudioPoint.isPlaying) 
              HelicopterSound.AudioPoint.PlayOneShot(HelicopterSound.IdleMotorSound);

                HelicopterSound.AudioPoint.pitch = RotationPower*  HelicopterSound.PitchMultiplier + (Parts.Chasis.GetComponent<Rigidbody>().velocity.magnitude*0.014f);
                HelicopterSound.AudioPoint.volume = RotationPower * HelicopterSound.VolumeMultiplier;
            
            
            //Asignamos la funcionalidad del rotor primario solamente si la potencia es del 20%
             if(RotationPower > 0.2f)
              Parts.Chasis.transform.Rotate(new Vector3( (Control.Pitch*RotationPower)*Settings.PitchForce, (Control.Roll*RotationPower)*Settings.RollForce, 0));
       
        }
        else //decuple part 
        {
            Parts.MainRotor.transform.parent = null;
            Parts.MainRotor.GetComponent<Rigidbody>().isKinematic = false;
            Parts.Chasis.GetComponent<Rigidbody>().AddTorque(0, HP / .10f * Time.deltaTime, 0);
        }

         //-------------------------------------------------------Tail Rotor
        if (Parts.TailRotor.GetComponent<RotorDamage>().CurrentDamage < 100)
        {
            // el rotor secundario es el YAW,
            //Simulacion de la rotacion

            Parts.TailRotor.transform.Rotate(Parts.RotationAxisTailRotor, Mathf.Lerp(0, (HP / 0.5f),0.36f*Time.deltaTime));//Rotation
            //Asignamos la funcionalidad del rotor secundario solamente si la potencia es del 10 % (puede variar este porcentaje pero no debe ser mayor al del principal)
            if(RotationPower > 0.1f)
            Parts.Chasis.transform.Rotate(0, 0, (Control.Yaw*RotationPower)*Settings.YawForce);
        }
        else //decuple part 
        {
            Parts.TailRotor.transform.parent = null;
            Parts.TailRotor.GetComponent<Rigidbody>().isKinematic = false;
            Parts.Chasis.GetComponent<Rigidbody>().AddTorque(0, HP / .30f * Time.deltaTime, 0);
        }
		//Debug.Log (Parts.Chasis.GetComponent<Rigidbody> ().velocity.magnitude);
    }
    //Outpouts methods 
    void Start() 
    {
        //Set the center of mass
        Parts.Chasis.GetComponent<Rigidbody>().centerOfMass = Settings.CenterOfMass.localPosition;
;
        HelicopterSound.AudioPoint.clip = HelicopterSound.IdleMotorSound;
    }
    void FixedUpdate() 
    {
        if (UseHelicopter)
            InputControl(Control);

        Hmotor();
    }
    
}
