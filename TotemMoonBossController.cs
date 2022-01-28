using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemMoonBossController : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPointsHelix, spawnPointsPulse;
    [SerializeField] private GameObject spawnPointHolder;
    [SerializeField] private float speed, rotationSpeed, projectileSpawnSpeed, initProjSpawnSpeed;
    [SerializeField] public bool isRed, isYellow, isWhite;
    [SerializeField] private LineRenderer laserBeamToBoss;
    [SerializeField] private Transform moonBoss, anchorPointForLaser;
    public GameObject[] mines;
    public GameObject galaxy;
    void Start()
    {
        moonBoss = FindObjectOfType<MoonBossController>().GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moonBoss == null)
        {
            FindObjectOfType<MoonBossController>().GetComponent<Transform>();
        }
        if (projectileSpawnSpeed > 0)
        {
            projectileSpawnSpeed -= Time.deltaTime;
        }
        else if (projectileSpawnSpeed <= 0)
        {
            //helixProjectileSpawnNumber = 3;                           
            if (isRed)
            {
                projectileSpawnSpeed = initProjSpawnSpeed;
                HelixPattern();
            }
            if (isWhite)
            {
                projectileSpawnSpeed = initProjSpawnSpeed * 1.5f;
                PulsePattern();
            }
        }
    }

    void FixedUpdate()
    {
        spawnPointHolder.transform.Rotate(Vector3.up, rotationSpeed * Time.fixedDeltaTime);
    }

    private void LateUpdate()
    {
        DrawLaser();
    }

    void HelixPattern()
    {
        foreach (Transform spawnPointHelix in spawnPointsHelix)
        {
            GameObject projectileClone = ObjectPool.Instance.SpawnFromPool("TotemPool1", spawnPointHelix.position, spawnPointHelix.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.timerUntilDestroyed = 3.5f;
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            projManager.randomProjSpeed = speed;
        }
    }

    void DrawLaser()
    {
        laserBeamToBoss.SetPosition(0, transform.position);
        RaycastHit hit;
        Debug.DrawLine(transform.position, moonBoss.position, Color.green);
        laserBeamToBoss.SetPosition(1, anchorPointForLaser.position);
        laserBeamToBoss.SetPosition(2, moonBoss.position);
    }

    void PulsePattern()
    {
        foreach (Transform spawnPointPulse in spawnPointsPulse)
        {
            GameObject projectileClone = ObjectPool.Instance.SpawnFromPool("TotemPool1", spawnPointPulse.position, spawnPointPulse.rotation);
            ProjectileManager projManager = projectileClone.GetComponent<ProjectileManager>();
            projManager.timerUntilDestroyed = 6.5f;
            projManager.isSpawnedBySpawner = true;
            projManager.randomDirection = Vector3.forward;
            projManager.randomProjSpeed = speed / 2f;
        }
    }

    void OnDestroy()
    {
        foreach (GameObject mine in mines)
        {
            mine.transform.parent = null;
            mine.GetComponent<Animator>().enabled = true;
            mine.GetComponent<Animator>().Play("Resolve_Mine");
            mine.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
        }
        galaxy.transform.parent = null;
        galaxy.GetComponent<Animator>().enabled = false;
        galaxy.GetComponent<Animator>().enabled = true;
        galaxy.GetComponent<Animator>().SetTrigger("GalaxyDie");
        galaxy.GetComponent<Animator>().Play("Galaxy_die");
        galaxy.GetComponent<ProjectileLinger>().enableLingeringEffect = true;
    }
}
