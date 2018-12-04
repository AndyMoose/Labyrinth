using System;
using System.Collections;
using System.Collections.Generic;
public class node
{

    private int h = 0;

    private int g = 0;

    private int f = 0;

    private int x = 0;

    private int y = 0;

    private int type = 0;

    private node p;

    public node(int r, int c, int t)
    {
        this.x = r;
        this.y = c;
        this.type = t;
        this.p = null;
    }

    public node(int r, int c)
    {
        this.x = r;
        this.y = c;
        this.p = null;
    }

    // gets the type of node based on its coordinates
    public int getType(int[,] world)
    {
        // return this.type;
        return world[this.y, this.x];
    }

    public int getH()
    {
        return this.h;
    }

    // gets the manhattan distance times 10 to be the heuristic
    public void setH(node p2)
    {

        this.h = ((Math.Abs((this.x - p2.getX())) + Math.Abs((this.y - p2.getY())))
                    * 10);
    }

    public int getG()
    {
        return this.g;
    }

    public void setG(int p2)
    {
        this.g = p2;
    }

    public int getF()
    {
        return getG() + getH();
    }

    public int getX()
    {
        return this.x;
    }

    public void setX(int x)
    {
        this.x = x;
    }

    public int getY()
    {
        return this.y;
    }

    public void setY(int y)
    {
        this.y = y;
    }

    public node getP()
    {
        return this.p;
    }

    public void setP(node p1)
    {
        this.p = p1;
    }

    // checks if two nodes are equal
    public bool equals(object obj)
    {
        node n = ((node)(obj));
        return ((this.x == n.getX())
                    && (this.y == n.getY()));
    }

    // checks if the list contains a node based on their x and y coordinates
    public static bool contains(int[,] aList, node b)
    {
        
        if (aList[b.getY(), b.getX()] == 1)
        {
            return true;
        }

        return false;
    }

    public String toString()
    {
        return ("Node: "
                    + (this.x + (", "
                    + (this.y + ""))));
    }
}