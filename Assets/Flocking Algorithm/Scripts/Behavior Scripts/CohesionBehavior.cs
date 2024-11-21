using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbours, return no adjusment
        if(context.Count == 0) 
            return Vector2.zero;


        // Add all points together 
        Vector2 cohesionMove = Vector2.zero;

        // List<Transform> filteredContext;

        // if(filter == null) 
        // {
        //     filteredContext = context;
        // }
        // else 
        // {
        //     filteredContext = filter.Filter(agent, context);
        // }
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }

        // Avarage
        cohesionMove /= context.Count;

        // Create offset from agent position
        cohesionMove -= (Vector2)agent.transform.position;

        return cohesionMove;
    }
}
