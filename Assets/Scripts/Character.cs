using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public int vida = 10;
    public int destActual;

    public NavMeshAgent Enemigo;
    
    public float margen = 1;

    public GameObject player;
    public float rango = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
