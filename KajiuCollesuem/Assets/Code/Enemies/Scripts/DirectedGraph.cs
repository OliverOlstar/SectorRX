using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectedGraph
{
    private List<Node> nodes = new List<Node>();

    public void AddNode(GameObject data)
    {
        nodes.Add(new Node(data));
    }

    public Node FindNode(GameObject data)
    {
        Node nodeFound = null;

        foreach (Node node in nodes)
        {
            if (node.GetData().Equals(data))
            {
                nodeFound = node;
                break;
            }
        }

        return nodeFound;
    }

    public void AddEdge(Node src, Node dest)
    {
        if (src == null || dest == null)
        {
            return;
        }
        src.GetOutgoing().Add(dest);
        dest.GetIncoming().Add(src);
    }

    public void AddEdge(GameObject src, GameObject dest)
    {
        AddEdge(FindNode(src), FindNode(dest));
    }

    public List<Node> GetNodes()
    {
        return nodes;
    }
}

public class Node
{
    private GameObject data;
    private List<Node> incoming = new List<Node>(), outgoing = new List<Node>();

    public Node(GameObject data)
    {
        this.data = data;
    }

    public GameObject GetData()
    {
        return data;
    }

    public List<Node> GetIncoming()
    {
        return incoming;
    }

    public List<Node> GetOutgoing()
    {
        return outgoing;
    }
}
