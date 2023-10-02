using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




public class SingletonScript<T> : MonoBehaviour where T : Component
{
    //Variabile:
    private static T scriptInstance;

    //Functii noi:

    //Getter pentru obiect:
    public static T Instance
    {
        get
        {
            if (scriptInstance == null)
            {
                scriptInstance = FindObjectOfType<T>();
                
                if (scriptInstance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    scriptInstance = obj.AddComponent<T>();
                }
            }

            return scriptInstance;
        }
    }

    //Pentru cand se creaza un obiect:
    public virtual void Awake()
    {
        if (scriptInstance == null)
        {
            scriptInstance = this as T;
            //Nu trebuie impartita intre scene:
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(scriptInstance);
        }
    }

    //Functii predefinite:
    //void Start()
    //{
    //}
    //void Update()
    //{
    //}
}
