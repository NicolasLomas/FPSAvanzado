using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int EnemigosVivos = 0;
    public int EnemigosMax = 3;
    public GameObject Spawn;
    public float cont; //contador de tiempo
    public float lim = 4; // tiempo entre spawn de eemigos
    public GameObject Enemigo;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //actualizar el contador
        cont = cont + Time.deltaTime;

        if (cont > lim & EnemigosVivos < EnemigosMax)
        {
            //crear enemigo
            GameObject nuevoEnemigo = (GameObject)Instantiate(Enemigo, this.transform.position, this.transform.rotation);
            cont = 0;
            EnemigosVivos++;

        }

    }

}