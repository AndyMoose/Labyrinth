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

    [SerializeField] private Transform Player;



    private bool hasTarget = false;
    private Vector3 target;

    private float maxSpeed;
    private float turnSpeed;
    private float radiusOfSat;

    private List<node> targets;
    private int targetIdx = 1;
    //private bool stopping = false;
    //private int stopon = 0;
    //private int stopsteps = 6000;
    //StreamWriter writer;

    void Start()
    {
        maxSpeed = 5f;
        radiusOfSat = 3f;
        turnSpeed = 2.5f;
        mp = new int[w2, h2];
        //worldDecomposer = Leader.GetComponent<WorldDecomposer>(); //gameObject.GetComponent<HingeJoint>();

        //writer = new StreamWriter(Path.Combine(Application.streamingAssetsPath, "astar/maps/output.txt"), true);

        string filePath = Path.Combine(Application.streamingAssetsPath, "astar/maps/map1.txt");

        if (File.Exists(filePath))
        {
            // Read the json from the file into a string
            string dataAsJson = File.ReadAllText(filePath);
            string[] lines = dataAsJson.Split('\n');
            for (int y = 0; y < lines.Length; y++)
            {
                string[] linedata = lines[y].Split(',');
                for (int x = 0; x < linedata.Length; x++)
                {
                    mp[y, x] = int.Parse(linedata[x]);
                    if (mp[y, x] == 1)
                    {
                        Debug.DrawRay(vec3FromNode(new node(y, x)), new Vector3(0, 7, 0), Color.red, 600);
                    }
                }
            }
        }

        run();
    }

    // Update is called once per frame
    void Update()
    {





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
    int h2 = 100;
    int w2 = 100;

    int[,] mp;
    int time = 0;
    List<node> astarrun()
    {

        //  TODO Auto-generated method stub
        //  create a 2d world of ints
        int[,] world = new int[w2, h2]; //driver.generateWorld();
        world = mp;
        //  a list for the nodes that need to be searched
        List<node> openList = new List<node>();
        //  a list for the nodes that have been searched
        List<node> closedList = new List<node>();
        int[,] closedArray = new int[w2, h2];
        int[,] openArray = new int[w2,h2];
        //  a list that contains the path from start to goal
        List<node> path = new List<node>();
        //  the start and goal nodes
        node start = nodeFromVec3(trans.position);  //new node((Mathf.FloorToInt(trans.position.z) + 50) / worldDecomposer.nodeSize, (Mathf.FloorToInt(trans.position.x) + 50) / worldDecomposer.nodeSize);
        node goal = nodeFromVec3(Player.position); //new node((Mathf.FloorToInt(z_) + 50) / worldDecomposer.nodeSize, (Mathf.FloorToInt(x_) + 50) / worldDecomposer.nodeSize);

        bool found = false;

        //  begin the loop
        if (start.getX() >= world.Length || start.getY() >= world.Length || start.getX() < 0 || start.getY() < 0)
        {
            //print("You are out of bounds");
        }
        //print(goal.getX() + "," + goal.getY());
        if (world[goal.getX(), goal.getY()] == 1)
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
            //log("count: " + openList.Count + "");
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
                // /Debug.Log("getf: " + curNode.getF());
                //  Removes that node for the open List
                //log("remove time: " + time + " node: " + curNode.getX() + ":" + curNode.getY());
                openList.Remove(curNode);
            }
            //log("pos: " + curNode.getX() + ":" + curNode.getY());

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
                                    || ((newNode.getX() >= w2)
                                    || ((newNode.getY() < 0)
                                    || (newNode.getY() >= h2))))))
                        {
                            // TODO: Warning!!! continue 

                            continue;
                        }
                        //print(newNode.getX() + "," + newNode.getY());
                        //log("open: " + test2);
                        //log("closed: " + closedList.Count);
                        //time++;
                        if (node.contains(closedArray, newNode))
                        {
                            //log("hit closed: " + time);
                            continue;
                        }
                        else
                        {
                            //log("hit none: " + time);
                        }
                        if(node.contains(openArray, newNode))
                        {
                            //log("abc");
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
                        //log("add to open: " + time + " node: " + newNode.getX() + ":" + newNode.getY());
                        openList.Add(newNode);
                        openArray[newNode.getY(), newNode.getX()] = 1;

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
            Debug.Log("no path found");
        }

        //  generates the path for the agent to use
        path.Clear();
        node pathN = goal;
        while ((pathN.getP() != null))
        {
            //print("PATH: " + pathN.getX() + "," + pathN.getY());
            path.Add(pathN);
            pathN = pathN.getP();
            //Debug.Log(pathN.getX()+":"+pathN.getY());
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
        //Debug.Log(path.Count);
        return path;
    }

    Vector3 targetNode(node obj)
    {
        return vec3FromNode(obj);
        //return new Vector3((obj.getY() * worldDecomposer.nodeSize) - 50 + (worldDecomposer.nodeSize / 2f), 0, (obj.getX() * worldDecomposer.nodeSize) - 50 + (worldDecomposer.nodeSize / 2f));
    }
    Vector3 vec3FromNode(node node)
    {
        return new Vector3(-1 * node.getX(), 0, 1 * node.getY());
    }
    node nodeFromVec3(Vector3 vec)
    {
        return new node((int)Mathf.Floor(-1 * vec.x), (int)Mathf.Floor(1 * vec.z));
    }
    
    void run()
    {
        targetIdx = 1;
        targets = astarrun();
        Vector3 dist = trans.position - Player.position;
        float ds = Mathf.Abs(dist.x) + Mathf.Abs(dist.y) + Mathf.Abs(dist.z);
        float maxtimeout = 1f;
        float mintimeout = 0.1f;

        float timeout = maxtimeout * (ds / 10);
        if(timeout > maxtimeout) { timeout = maxtimeout; }
        if(timeout < mintimeout) { timeout = mintimeout; }


        try
        {


            target = targetNode(targets[0]);

            hasTarget = true;
        } catch
        {

        }
        //Debug.Log(timeout);
        Invoke("run", timeout);
    }

    void log(string txt)
    {
        //writer.WriteLine(txt);
    }
}
