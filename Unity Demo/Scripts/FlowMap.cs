using Pathfinding;
using UnityEngine;

namespace Flowfield.Demo
{
    [RequireComponent(typeof(Grid))]
    public class FlowMap : Pathfinding.Demo.Map
    {
        public bool CreateFlowField(int x, int y, in int[] flowfield, TravelType travelType = TravelType.Cardinal)
        {
            return Graph != null && Graph.TryGet(x, y, out _) && Graph.CreateFlowMap(x, y, flowfield, travelType);
        }
    }
}