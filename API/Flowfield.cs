using Pathfinding;
using System.Collections.Generic;

namespace Flowfield
{
    public static class Flowfield
    {
        private static List<Node> _pathBuffer = new();
        public static bool CreateFlowMap(this Graph graph, in int targetX, in int targetY, in int[] result, TravelType travelType = TravelType.Cardinal)
        {
            if (result.Length != graph.Rows * graph.Columns)
                throw new System.Exception("Supplied flowfield array needs to be " + graph.Rows * graph.Columns + " in length!");


            if (!graph.TryGet(targetX, targetY, out Node targetNode) || !targetNode.IsWalkable)
                return false;

            for (int q = 0; q < graph.Columns; q++)
            {
                for (int r = 0; r < graph.Rows; r++)
                {
                    int index = graph.GetIndex(q, r);
                    if (q == targetX && r == targetY)
                    {
                        result[index] = -1;
                        continue;
                    }
                    if (graph.TryGet(q, r, out Node startNode) && startNode.IsWalkable && graph.FindPath(startNode, targetNode, _pathBuffer, travelType))
                    {
                        var next = _pathBuffer[1];
                        int nextId = graph.GetIndex(next.X, next.Y);
                        result[index] = nextId;
                    }
                    else
                        result[index] = -1;
                }
            }
            return true;
        }
    }
}