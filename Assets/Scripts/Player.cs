using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public float alcance = 100f; // Alcance del rayo
    public LayerMask capasImpacto; // Capas que ser�n impactadas por el rayo
    public int danio = 20; // Cantidad de da�o infligido a los enemigos


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Disparar cuando se presiona el bot�n de fuego (cambiar seg�n tu configuraci�n)
        {
            Disparar();
        }
    }

    void Disparar()
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, alcance, capasImpacto);

        foreach (RaycastHit hit in hits)
        {
            Debug.Log("Objeto golpeado: " + hit.transform.name);

            EnemyController enemy = hit.transform.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.RecibirDanio(danio); // Aplica el da�o al enemigo
            }
        }
    }
}
