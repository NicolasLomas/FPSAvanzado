using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : Character
{
    public EnemySpawn enemySpawn; // Referencia al script EnemySpawn
    public enum Estado
    {
        Descanso,
        Patrulla
    }
    public Estado estadoActual = Estado.Patrulla;
    NavMeshAgent Enemigo;
    public float tiempoDescanso = 3f; // Duración de descanso
    public float tiempoPatrulla = 10f; // Duración de la patrulla 
    public float tiempoActual = 0f; // Tiempo transcurrido en el estado actual

    // Start is called before the first frame update
    void Start()
    {
        Enemigo = this.GetComponent<NavMeshAgent>();
        enemySpawn = FindObjectOfType<EnemySpawn>(); // Encontrar el script EnemySpawn en la escena


    }

    // Update is called once per frame
    void Update()
    {
        p1 = GameObject.Find("punto1");
        p2 = GameObject.Find("punto2");
        
        player = GameObject.Find("RigidBodyFPSController");
        Enemigo = this.GetComponent<NavMeshAgent>();
        tiempoActual += Time.deltaTime;


        if (estadoActual == Estado.Patrulla && tiempoActual >= tiempoPatrulla)
        {
            CambiarEstado(Estado.Descanso);
        }
        else if (estadoActual == Estado.Descanso && tiempoActual >= tiempoDescanso)
        {
            CambiarEstado(Estado.Patrulla);
        }

        if (estadoActual == Estado.Patrulla)
        {
            Patrulla(); // Realizar patrulla solo si estamos en ese estado
        }
        else if (estadoActual == Estado.Descanso)
        {
            Enemigo.isStopped = true; // Detener al enemigo durante el descanso
            //Debug.Log("Descansando...");
        }
        DetectarAlPlayer(); // Siempre detectar al jugador



    }

    void DetectarAlPlayer()
    {
        Vector3 distPlayer = player.transform.position - this.transform.position;
        if (distPlayer.magnitude < rango)
        {
            //mirar linea de vision
            RaycastHit hitinfo;
            if (Physics.Raycast(this.transform.position, distPlayer, out hitinfo, 5))
            {

                //rayo colisiona
                if (hitinfo.transform.tag == "Player") //tiene linea de vision
                {
                    //ir al jugador
                    Enemigo.SetDestination(player.transform.position);
                   

                }
            }

        }
    }

    void Patrulla()
    {
        //detectar la distancia que falta al destino
        //distancia destino
        Vector3 dist = this.transform.position - Enemigo.destination;
        if (dist.magnitude < margen)
        {
            //llegamos al destino
            if (destActual == 1)
            {
                //actualiza destino
                destActual = 2;
                //manda al punto 2
                Enemigo.SetDestination(p2.transform.position);
            }
            else
            {
                destActual = 1;
                //manda al punto 1
                Enemigo.SetDestination(p1.transform.position);
            }
        }
    }

    void CambiarEstado(Estado nuevoEstado)
    {
        estadoActual = nuevoEstado;
        tiempoActual = 0f; // Reiniciar el temporizador
        Enemigo.isStopped = (estadoActual == Estado.Descanso); // Detener o reanudar al enemigo según el estado
    }
    // Método para recibir daño
    public void RecibirDanio(int cantidadDanio)
    {
        vida -= cantidadDanio;
        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Destroy(gameObject);
        
    }
    void OnDestroy()
    {
        if (enemySpawn != null)
        {
            enemySpawn.EnemigosVivos--; // Reducir el contador de enemigos vivos cuando este enemigo es destruido
        }
    }
}

