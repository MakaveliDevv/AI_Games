using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehavior flockBehavior;
    [SerializeField] private List<FlockAgent> agents = new();

    [Range(10, 500)] 
    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)] public float driveFactor = 10f;
    [Range(1f, 100f)] public float maxSpeed = 5f;
    [Range(1f, 10f)] public float neighborRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = .5f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate
            (
                agentPrefab,
                startingCount * AgentDensity * Random.insideUnitCircle,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
            );

            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    void Update() 
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            
            Vector2 move = flockBehavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            if(move.sqrMagnitude > squareMaxSpeed) 
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }
    
    private List<Transform> GetNearbyObjects(FlockAgent agent) 
    {
        List<Transform> context = new();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        
        foreach (Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider) 
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
