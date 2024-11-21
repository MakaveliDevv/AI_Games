using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    public Flock AgentFlock { get { return agentFlock; } }
    public Collider2D AgentCollider { get { return agentCollider; } }
    private Flock agentFlock;
    private Collider2D agentCollider;
    
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock) 
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity) 
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
