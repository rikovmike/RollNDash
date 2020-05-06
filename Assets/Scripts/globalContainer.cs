using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalContainer : MonoBehaviour
{

            public static int restarts=0;
            public static globalContainer instance;
            void Awake() {
                if(instance != null && instance != this) {
                    DestroyImmediate(gameObject);
                    return;
                }
                instance = this;
                DontDestroyOnLoad(gameObject);
                PlayerPrefs.SetInt("Restarts", 0);
            }    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void IncrementRestarts(){
        restarts = PlayerPrefs.GetInt("Restarts");
        restarts+=1;
        PlayerPrefs.SetInt("Restarts",restarts);
    }
}
