using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followPatch : MonoBehaviour
{
    Transform goal; //pontos de waypoints
    float speed = 5.0f;  //velocidade do player
    float accuracy = 2.55f; // precisão de busca ponto mais próximo no *array de pontos
    float rotSpeed = 2f;// velocidade de rotação

    public GameObject wpManager; //espaço para o Objeto WP_MANAGER
    GameObject[] wp;// *array de pontos
    GameObject currentNode; //ponto atual do player
    int currentWP = 0; //ponto atual no awway
    Graph g;

    void Start()
    {

        wp = wpManager.GetComponent<WP_MANAGER>().waypoints; //busca componente da classe WP_MANAGER
        g = wpManager.GetComponent<WP_MANAGER>().graph; //busca componente da classe WP_MANAGER
        currentNode = wp[0]; //defini o currentNode como node inicial do array = 0
    }

   

    public void GotoHeliport()// método que vai até o ponto [1] HELIPORT
    {
        g.AStar(currentNode, wp[1]);
        currentWP = 0;
    }
    public void GotoRuins()// método que vai até o ponto [2] RUINS
    {
        g.AStar(currentNode, wp[2]);
        currentWP = 0;
    }
    public void GotoTanks()// método que vai até o ponto [3] TANKS
    {
        g.AStar(currentNode, wp[3]);
        currentWP = 0;
    }




    private void LateUpdate()
    {

        //verifica length do path
        if (g.getPathLength() == 0 || currentWP == g.getPathLength()) 
            return;

        //nó mais proximo
        currentNode = g.getPathPoint(currentWP);

        //se accuracy = player se move para proximo nó
        if (Vector3.Distance(g.getPathPoint(currentWP).transform.position, transform.position) < accuracy)
        { 
            currentWP++; 
        }

        if (currentWP < g.getPathLength())
        {
            goal = g.getPathPoint(currentWP).transform;

            Vector3 lookAtGoal = new Vector3(goal.position.x, this.transform.position.y, goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position; //derection = diferença entre a distancia do lookAtGoal e player
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotSpeed);//rotaciona player

            this.transform.Translate(0, 0, speed * Time.deltaTime); //velocidade do player
        }
    }
}