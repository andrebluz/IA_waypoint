using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [System.Serializable]
    public struct Link
    {
        public enum direction { UNI, BI } //dois tipos de movimentação UNI A->B e BI  A<->B
        public GameObject node1; //gameobject WP primeiro nó
        public GameObject node2; //gameobject WP segundo nó
        public direction dir; //direção do proximo nó

    }

    public class WP_MANAGER : MonoBehaviour
    {

        public GameObject[] waypoints; //array que contém os gameobjects de todos os nós
        public Link[] links; //array que faz a ligação de node1 para node2
        public Graph graph = new Graph();

        void Start()
        {
            
            if (waypoints.Length > 0) //se length do array waypoints > 0 = verifica os nós
            {
                foreach (GameObject wp in waypoints) //loop que adiciona os nós ao algoritmo
                {
                    graph.AddNode(wp);
                }

                foreach (Link i in links) //loop que conecta os nós
                {
                    graph.AddEdge(i.node1, i.node2); //liga node 1 ao node 2
                    if (i.dir == Link.direction.BI) //caso seja bidirecional
                    {
                        graph.AddEdge(i.node1, i.node2); //adiciona ao algoritmo
                    }
                }

            }

        }

        // Update is called once per frame
        void Update()
        {
            graph.debugDraw(); //debuga a roda na janela SCENE
        }
    }

