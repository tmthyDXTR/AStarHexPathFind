using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
///This is a customzied A* search from 
///https://gist.github.com/keithcollins/307c3335308fea62db2731265ab44c06#file-astarsearch-cs-L101
/// </summary>
// From Red Blob: I'm using an unsorted array for this example, but ideally this
// would be a binary heap. Find a binary heap class:
// * https://bitbucket.org/BlueRaja/high-speed-priority-queue-for-c/wiki/Home
// * http://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
// * http://xfleury.github.io/graphsearch.html
// * http://stackoverflow.com/questions/102398/priority-queue-in-net

public class AStarSearch : MonoBehaviour
{
    // Now that all of our classes are in place, we get get
    // down to the business of actually finding a path.

    // Someone suggested making this a 2d field.
    // That will be worth looking at if you run into performance issues.
    public Dictionary<CubeIndex, CubeIndex> cameFrom = new Dictionary<CubeIndex, CubeIndex>();
    public Dictionary<CubeIndex, float> costSoFar = new Dictionary<CubeIndex, float>();

    private CubeIndex start;
    private CubeIndex goal;

    static public float Heuristic(CubeIndex a, CubeIndex b)
    {
        return Mathf.Max(Mathf.Abs(a.x - b.x), Mathf.Abs(a.y - b.y), Mathf.Abs(a.z - b.z));
    }

    // Conduct the A* search
    public AStarSearch(Grid grid, CubeIndex start, CubeIndex goal)
    {
        // start is current pos Location
        this.start = start;
        // goal is destination eg tile user clicked on
        this.goal = goal;

        // add the cross product of the start to goal and tile to goal vectors
        // Vector3 startToGoalV = Vector3.Cross(start.vector3,goal.vector3);
        // Location startToGoal = new Location(startToGoalV);
        // Vector3 neighborToGoalV = Vector3.Cross(neighbor.vector3,goal.vector3);

        // frontier is a List of key-value pairs:
        // Location, (float) priority
        var frontier = new PriorityQueue<CubeIndex>();
        // Add the starting location to the frontier with a priority of 0
        frontier.Enqueue(start, 0f);

        cameFrom.Add(start, start); // is set to start, None in example
        costSoFar.Add(start, 0f);

        while (frontier.Count > 0f)
        {
            // Get the Location from the frontier that has the lowest
            // priority, then remove that Location from the frontier
            CubeIndex current = frontier.Dequeue();

            // If we're at the goal Location, stop looking.
            if (current.Equals(goal)) break;

            // Neighbors will return a List of valid tile Locations
            // that are next to, diagonal to, above or below current
            foreach (var neighborIndex in grid.TilesInMoveRange(current, 1))
            {

                // If neighbor is diagonal to current, graph.Cost(current,neighbor)
                // will return Sqrt(2). Otherwise it will return only the cost of
                // the neighbor, which depends on its type, as set in the TileType enum.
                // So if this is a normal floor tile (1) and it's neighbor is an
                // adjacent (not diagonal) floor tile (1), newCost will be 2,
                // or if the neighbor is diagonal, 1+Sqrt(2). And that will be the
                // value assigned to costSoFar[neighbor] below.
                float newCost = costSoFar[current] + grid.Distance(current, neighborIndex);

                // If there's no cost assigned to the neighbor yet, or if the new
                // cost is lower than the assigned one, add newCost for this neighbor
                if (!costSoFar.ContainsKey(neighborIndex) || newCost < costSoFar[neighborIndex])
                {

                    // If we're replacing the previous cost, remove it
                    if (costSoFar.ContainsKey(neighborIndex))
                    {
                        costSoFar.Remove(neighborIndex);
                        cameFrom.Remove(neighborIndex);
                    }

                    costSoFar.Add(neighborIndex, newCost);
                    cameFrom.Add(neighborIndex, current);
                    float priority = newCost + Heuristic(neighborIndex, goal);
                    frontier.Enqueue(neighborIndex, priority);
                }
            }
        }

    }

    // Return a List of Locations representing the found path
    public List<CubeIndex> FindPath()
    {

        List<CubeIndex> path = new List<CubeIndex>();
        CubeIndex current = goal;
        // path.Add(current);

        while (!current.Equals(start))
        {
            if (!cameFrom.ContainsKey(current))
            {
                MonoBehaviour.print("cameFrom does not contain current.");
                return new List<CubeIndex>();
            }
            path.Add(current);
            current = cameFrom[current];
        }
        // path.Add(start);
        path.Reverse();
        return path;
    }

}

public class PriorityQueue<CubeIndex>
{
    private List<KeyValuePair<CubeIndex, float>> indices = new List<KeyValuePair<CubeIndex, float>>();

    public int Count
    {
        get { return indices.Count; }
    }

    public void Enqueue(CubeIndex index, float priority)
    {
        indices.Add(new KeyValuePair<CubeIndex, float>(index, priority));
    }

    // Returns the Location that has the lowest priority
    public CubeIndex Dequeue()
    {
        int bestIndex = 0;

        for (int i = 0; i < indices.Count; i++)
        {
            if (indices[i].Value < indices[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        CubeIndex bestItem = indices[bestIndex].Key;
        indices.RemoveAt(bestIndex);
        return bestItem;
    }
}