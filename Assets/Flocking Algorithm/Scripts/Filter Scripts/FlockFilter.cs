using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/SameFlock")]
public class FlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbours)
    {
        List<Transform> filtered = new();

        foreach (Transform item in originalNeighbours)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if(itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock) 
            {
                filtered.Add(item);
            }
        }
        
        return filtered;
    }
}
