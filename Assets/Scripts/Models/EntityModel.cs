using UnityEngine;

/// <summary>
/// Stores references and settings relevant to an entity in the game (Player/Enemy)
/// </summary>
public class EntityModel : MonoBehaviour
{
    [SerializeField]
    private EntityView entityView;

    [SerializeField]
    private FillableUIView entityHealthbar;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private string entityEnemyTag;

    [SerializeField]
    private int maxHealth;

    [SerializeField]
    private int damageReceivedOnHit;

    public EntityView EntityView { get { return entityView; } }
    public FillableUIView EntityHealthbar { get { return entityHealthbar; } }
    public Rigidbody2D RigidBody { get { return rigidBody; } }
    public string EntityEnemyTag { get { return entityEnemyTag; } }
    public int MaxHealth { get { return maxHealth; } }
    public int DamageReceived { get { return damageReceivedOnHit; } }
}