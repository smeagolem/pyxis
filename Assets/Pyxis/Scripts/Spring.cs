using UnityEngine;

// https://natureofcode.com/book/chapter-3-oscillation/#chapter03_section10

[System.Serializable]
public class Spring
{

    [Range(0f, 1f)]
    public float stiffness = 0.1f;

    [Range(0f, 1f)]
    public float damping = 0.25f;

    public float length = 0f;

    [HideInInspector]
    public Vector2 position;

    [HideInInspector]
    public Vector2 velocity;

    [HideInInspector]
    public Vector2 acceleration;

    Vector2 force;
    float forceMultiplier = 60f;

    public void FixedUpdate()
    {
        var displacement = position.magnitude - length;
        var springForce = position.normalized * -stiffness * displacement;
        force += springForce;

        acceleration = force * forceMultiplier;
        velocity += acceleration;
        position += velocity * Time.deltaTime;
        velocity *= 1 - damping;

        force = Vector2.zero;
    }

    public void addForce(Vector2 force)
    {
        this.force += force;
    }

    public void DrawGizmos(Vector3 origin)
    {
        var worldPosition = origin + (Vector3)position;
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(origin, 0.2f);
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(worldPosition, 0.2f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(origin, worldPosition);
    }
}
