using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum SideConflict
{
    Player,
    Enemy
}


public class Character : MonoBehaviour, IDestroyed
{

    public float hp, speed, armor, radiusLook;
    public NavMeshAgent agent;
    public SideConflict sideConflict;
    public IActivity target;
    public Attack attack;
    public CollectionUP collectionUP;
    public Transform collectionUpParent;
    public Vector3 nextPosition;
    public Transform bulletTransform;
    public Animator animator;
    public string curatorAudio;
    protected float timeOldSelect;
    protected int countClick;
    float timeIdle, timeIdleSound;
    public string snoryd;
    public Sprite sprite;
    
    //public Bullet bullet;
    //public Attack radiusUpCharacter;


    public virtual void Start()
    {
        string[] folders = Resources.Load<TextAsset>("Sound/Folders").text.Split('\n');
        curatorAudio = folders[Random.Range(0, folders.Length)];


        AudioManager.AddAudio(this, "Create");
    }

    public virtual void Update()
    {
        if (this as Unit != null && (this as Unit).typeTarget == TypeTarget.Set)
            if (agent.velocity.magnitude < agent.speed / 5)
                timeIdle += Time.deltaTime;
            else timeIdle = 0;
        timeOldSelect += Time.deltaTime;
        if (timeIdle >= 0.5 && this as Unit != null)
        {
            (this as Unit).typeTarget = TypeTarget.Auto;
            SetTargetPosition(transform.position);
            timeIdle = 0;
        }
        if (agent.velocity.magnitude < agent.speed / 5)
        {
            timeIdleSound += Time.deltaTime;
            if (timeIdleSound > 15)
            {
                AudioManager.AddAudio(this, "Idle", true);
                timeIdleSound -= 30;
            }
        }
        else timeIdleSound = 0;
        agent.acceleration = speed * 2;
        agent.speed = speed;
        if (target == null || (target as MonoBehaviour) == null)
            target = null;
        if (target != null)
        {
            CollectionUP collection = target as CollectionUP;
            IDestroyed destroyed = target as IDestroyed;
            LegoBox legoBox = target as LegoBox;
            Smelter smelter = target as Smelter;

            Vector3 distance = transform.position - (target as MonoBehaviour).transform.position;
            if (destroyed != null && distance.magnitude > attack.radiusAttack && (target as Build != null && (target as Build).sideConflict != sideConflict || target as Character != null && (target as Character).sideConflict != sideConflict || target as Interactive != null))
            {
                SetTargetPosition((target as MonoBehaviour).transform.position + distance.normalized * (attack.radiusAttack - (attack.radiusAttack * 0.1f)));
            }
            else
            {
                if (destroyed != null && (target as Build != null && (target as Build).sideConflict != sideConflict || target as Character != null && (target as Character).sideConflict != sideConflict || target as Interactive != null))
                {
                    if (attack.cdProgress <= 0)
                    {
                        Attack(attack, destroyed);
                    }
                    if (this as Unit != null && (this as Unit).typeTarget == TypeTarget.Set)
                        SetTargetPosition(transform.position);
                }
                else
                {
                    if (collection != null)
                    {
                        SetTargetPosition(collection.transform.position);
                        if (distance.magnitude < 2) //Длина 
                        {
                            collectionUP = collection;
                            collection.transform.parent = collectionUpParent;
                            collection.transform.localPosition = new Vector3(0, 0, 0);
                            SetTarget(null);
                            (this as Unit).typeTarget = TypeTarget.Auto;
                        }
                    }
                    else
                    {
                        if (legoBox != null)
                        {
                            SetTargetPosition(legoBox.pointSbor.position);
                            if ((transform.position - legoBox.pointSbor.position).magnitude < 50)
                            {
                                GameObject gameObject;
                                gameObject = legoBox.GetBox(this as Unit);
                                collectionUP = gameObject.GetComponent<CollectionUP>();
                                gameObject.transform.parent = collectionUpParent;
                                gameObject.transform.localPosition = new Vector3(0, 0, 0);
                                SetTarget(Main.instance.smelter);
                            }
                        }
                        else
                        {
                            SetTargetPosition(smelter.pointSdachi.position);
                            if ((transform.position - smelter.pointSdachi.position).magnitude < 30)
                            {
                                if (collectionUP != null)
                                {
                                    Main.instance.storage.AddBabin(collectionUP.babin);
                                    Main.instance.storage.AddPlastic(collectionUP.plastic);
                                    Destroy(collectionUP.gameObject);
                                    collectionUP = null;
                                    SetTarget(Main.instance.legoBox);
                                }
                            }
                        }
                    }

                }

            }
        }
        attack.cdProgress -= Time.deltaTime;
        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetTarget(IActivity target)
    {
        this.target = target;
    }

    public void SetTargetPosition(Vector3 position)
    {
        NavMeshPath path = new NavMeshPath();
        if (agent.CalculatePath(position, path))
        {
            agent.SetPath(path);
        }

    }

    public void Attack(Attack attack, IDestroyed destroyed)
    {
        attack.cdProgress = attack.cd;
        if (bulletTransform == null)
            destroyed.GetDamage(attack.damage);
        else
        {
            if (animator != null)
                animator.SetTrigger("Attack");
            GameObject bullet = Instantiate(Resources.Load<GameObject>("Prefabs/" + snoryd));
            bullet.transform.position = bulletTransform.position;
            bullet.transform.LookAt((target as MonoBehaviour).transform);
            bullet.GetComponent<Bullet>().parent = this;
            AudioManager.AddAudio(bullet.transform, "Shoot", "", false, transform, SoundMusicVoice.Sound);
        }
    }

    public void GetDamage(float damage)
    {

        hp -= damage;
        if (hp <= 0)
        {
            Destroyed();
            AudioManager.AddAudio(this, "Hit");
        }

    }

    public virtual void Destroyed()
    {
        GameObject residue = Instantiate(Resources.Load<GameObject>("Prefabs/Residue/ResidueResource"));
        Destroy(gameObject);
        residue.transform.position = transform.position;
        AudioManager.AddAudio(this, "Destroy");

    }

}
