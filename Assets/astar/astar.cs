using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;

public class astar : MonoBehaviour
{

    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GameObject Leader;
    [SerializeField] private Transform ground;
    //private WorldDecomposer worldDecomposer;
    [SerializeField] private Transform Player;



    private bool hasTarget = false;
    private Vector3 target;

    private float maxSpeed;
    private float turnSpeed;
    private float radiusOfSat;

    private List<node> targets;
    MapData loadedData;
    private int targetIdx = 1;
    //private bool stopping = false;
    //private int stopon = 0;
    //private int stopsteps = 6000;

    int h = 20;
    int w = 20;

    void Start()
    {
        maxSpeed = 5f;
        radiusOfSat = 3f;
        turnSpeed = 2.5f;
        //worldDecomposer = Leader.GetComponent<WorldDecomposer>(); //gameObject.GetComponent<HingeJoint>();

        string filePath = Path.Combine(Application.streamingAssetsPath, "astar/maps/map1.txt");
        string dataAsJson = File.ReadAllText(filePath);
        // Pass the json to JsonUtility, and tell it to create a GameData object from it
        loadedData = JsonUtility.FromJson<MapData>(dataAsJson);

    }

    // Update is called once per frame
    void Update()
    {
        //node PlayerNode = nodeFromVec3(Player.transform.position);
        //Debug.Log(PlayerNode.getX() + ":" + PlayerNode.getY());

        if (hasTarget)
        {

            // Calculate vector from character to target
            Vector3 towards = target - trans.position;
            Quaternion targetRotation = Quaternion.LookRotation(towards);
            trans.rotation = Quaternion.Lerp(trans.rotation, targetRotation, turnSpeed * Time.deltaTime); //Quaternion.LookRotation(towards);

            // If we haven't reached the target yet
            if (towards.magnitude > radiusOfSat)
            {

                // Normalize vector to get just the direction
                towards.Normalize();
                towards *= maxSpeed;

                // Move character
                rb.velocity = towards;
            }
            else
            {
                if (targetIdx < targets.Count)
                {
                    target = targetNode(targets[targetIdx]);
                    targetIdx += 1;
                }
                else
                {

                    rb.velocity = Vector3.zero;
                    hasTarget = false;
                }
                //stopping = true;
            }
        }
    }
    List<node> astarrun(float x_, float z_)
    {

        //  TODO Auto-generated method stub
        //  create a 2d world of ints
        int[,] world = loadedData.data; //driver.generateWorld();
        //  a list for the nodes that need to be searched
        List<node> openList = new List<node>();
        //  a list for the nodes that have been searched
        List<node> closedList = new List<node>();
        int[,] closedArray = new int[h, w];
        //  a list that contains the path from start to goal
        List<node> path = new List<node>();
        //  the start and goal nodes
        //node start = new node((Mathf.FloorToInt(trans.position.z) + 50) / worldDecomposer.nodeSize, (Mathf.FloorToInt(trans.position.x) + 50) / worldDecomposer.nodeSize);
        //node goal = new node((Mathf.FloorToInt(z_) + 50) / worldDecomposer.nodeSize, (Mathf.FloorToInt(x_) + 50) / worldDecomposer.nodeSize);

        node start = new node(0, 0); //mintar
        node goal = new node(0, 0); //player

        bool found = false;

        //  begin the loop
        if (start.getX() >= world.Length || start.getY() >= world.Length || start.getX() < 0 || start.getY() < 0)
        {
            //print("You are out of bounds");
        }
        //print(goal.getX() + "," + goal.getY());
        if (world[goal.getY(), goal.getX()] == 1)
        {
            //print("Your goal is blocked");
        }
        openList.Add(start);

        //  get the goal node


        /// ///////////////////////////////////////////////////////////////////////////////////
        //  A* pathing
        node curNode = null;
        while ((!found
                    && (openList.Count != 0)))
        {
            //  step 1
            //  set a min that any node can beat
            int min = int.MaxValue;
            //  Removes the first element if there is only 1
            if ((openList.Count == 1))
            {
                curNode = openList[0];
                openList.Remove(curNode);
            }
            else
            {
                //  checks the f values of all the nodes in the open list until it finds the
                //  lowest one
                //  that then become sthe current node
                node next = null;
                for (int j = 0; (j < openList.Count); j++)
                {
                    next = openList[j];
                    if ((min > next.getF()))
                    {
                        curNode = next;
                        min = curNode.getF();
                    }

                }

                //  Removes that node for the open List
                openList.Remove(curNode);
            }

            //  step 2
            //  if the current node is the goal, break out of the search and begin making the
            //  path
            if (curNode.equals(goal))
            {
                goal = curNode;
                //System.out.//println("path has been found");
                found = true;
                break;
            }

            //  step 3
            //  generate neighbors
            for (int y = -1; (y < 2); y++)
            {
                for (int x = -1; (x < 2); x++)
                {
                    if (!((x == 0)
                                && (y == 0)))
                    {
                        // 
                        node newNode = new node((x + curNode.getX()), (y + curNode.getY()));
                        //  testing
                        //  checks if the node is in the closed list and within the bounds of the world
                        //print(newNode.getX() + "," + newNode.getY());
                        if ((
                                     ((newNode.getX() < 0)
                                    || ((newNode.getX() >= w)
                                    || ((newNode.getY() < 0)
                                    || (newNode.getY() >= h))))))
                        {
                            // TODO: Warning!!! continue 

                            continue;
                        }
                        //print(newNode.getX() + "," + newNode.getY());
                        if (node.contains(closedArray, newNode))
                        {
                            continue;
                        }

                        //  checks if the node is blocked or not
                        //print(newNode.getX() + "," + newNode.getY());
                        if ((newNode.getType(world) == 1))
                        {
                            // TODO: Warning!!! continue If
                            continue;
                        }

                        //  checks the up/down/left/right directions and if they are blocked, it ignores
                        //  the diagonal
                        if (((world[curNode.getY(), (curNode.getX() + x)] == 1)
                                    && (world[(curNode.getY() + y), curNode.getX()] == 1)))
                        {
                            // TODO: Warning!!! continue If
                            continue;
                        }

                        //  checks if the node is a diagonal or not
                        if (((Math.Abs(x) == 1)
                                    && (Math.Abs(y) == 1)))
                        {
                            //  diagonals
                            newNode.setG(14);
                        }
                        else
                        {
                            //  up/down/left/right
                            newNode.setG(10);
                        }

                        //  sets the g value, hueristic, and the parent of the node
                        newNode.setG((newNode.getG() + curNode.getG()));
                        newNode.setH(goal);
                        newNode.setP(curNode);
                        //  Adds it to the open list
                        //print("node added");
                        openList.Add(newNode);
                    }

                }

            }

            //  step 4
            //  Adds the current node to the closed list
            closedList.Add(curNode);
            closedArray[curNode.getY(), curNode.getX()] = 1;
        }

        //  if the openList is empty, then every node has been searched and a pth could
        //  not be found
        if ((openList.Count == 0))
        {
            //System.out.//println("A path could not be found");
        }

        //  generates the path for the agent to use
        path.Clear();
        node pathN = goal;
        while ((pathN.getP() != null))
        {
            //print("PATH: " + pathN.getX() + "," + pathN.getY());
            path.Add(pathN);
            pathN = pathN.getP();
        }

        /*    for (int i = (path.Count - 1); (i >= 0); i--)
           {
               if ((world[path[i].getY(), path[i].getX()] != 2))
               {
                   if ((world[path[i].getY(), path[i].getX()] != 3))
                   {
                       world[path[i].getY(), path[i].getX()] = 4;
                   }

               }

           }
   */
        path.Reverse();
        return path;
    }

    Vector3 targetNode(node obj)
    {
        return new Vector3(obj.getY(), 0, obj.getX());
        //return new Vector3((obj.getY() * worldDecomposer.nodeSize) - 50 + (worldDecomposer.nodeSize / 2f), 0, (obj.getX() * worldDecomposer.nodeSize) - 50 + (worldDecomposer.nodeSize / 2f));
    }
    node nodeFromVec3(Vector3 vec)
    {
        //(-42.3, 0.6, 98.6) : 8,19
        return new node((int)Mathf.Floor(vec.x / -5), (int)Mathf.Floor(vec.z / 5));
    }
	Vector3 vec3FromNode(node node)
	{
		return new Vector3(-5 * node.getX(), 0, 5 * node.getY());
	}
}
