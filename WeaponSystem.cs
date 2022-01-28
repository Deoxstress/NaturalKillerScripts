using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading;

public class WeaponSystem : MonoBehaviour
{

    public Transform[] closestEnemies;
    public GameObject homingMissilePrefab;
    public GameObject vfxInitiationTirPrefab;
    public float timeBetweenShots;
    private float initAttackSpeed;
    [Range(0, 15)]
    public int maxTargetsToShoot;
    public List<Enemy> enemyList = new List<Enemy>();
    private PlayerMovement player;
    public bool canShoot;
    [HideInInspector]
    public List<Enemy> scannedEnemies = new List<Enemy>();
    public List<Enemy> loopedEnemyScanned = new List<Enemy>();
    public GameObject[] MissileOriginArray;
    [SerializeField] private float initTimer = 0.1f, shakeIntensity, shakeTime;
    float timerToShootAnother;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip lockSound;





    static WeaponSystem instance;
    public static WeaponSystem Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<WeaponSystem>();
            }
            return instance;
        }
    }

    void OnEnable()
    {
        canShoot = false;
        enemyList.Clear();
        scannedEnemies.Clear();
    }

    void OnDisable()
    {
        canShoot = false;

        EnemyNotInListAnymore();
        enemyList.Clear();
        scannedEnemies.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        timerToShootAnother = initTimer;
        player = GameObject.FindObjectOfType<PlayerMovement>();
        initAttackSpeed = timeBetweenShots;
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        //Sur la pression d'un bouton : RT par exemple, la distance augmenterait et enregistrerait la position de chaque ennemi.
        //audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();
        //audioSource.volume = 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        //closestEnemies = FindClosestEnemies();
        //.OrderBy(x => x.gameObject.GetComponent<Enemy>().roundedDistanceToPlayer).ToArray();
        timerToShootAnother -= Time.deltaTime;

        if (canShoot)
        {
            if (scannedEnemies != null)
            {
                FireMissilesSalvo(scannedEnemies);
            }


            /*
            timeBetweenShots -= Time.deltaTime;
            maxTargetsToShoot = scannedEnemies.Count;
            if (timeBetweenShots <= 0 && scannedEnemies.Count != 0)
            {
                //  FireMissiles(closestEnemies, maxTargetsToShoot);
                FireMissilesToClosestEnemies(scannedEnemies);
                timeBetweenShots = initAttackSpeed;
            }
            */

            /* foreach(Enemy e in scannedEnemies)
             {
                 int enemycount = 0;
                 if(e.enemyHP <= 0)
                 {
                     enemycount++;
                     if(enemycount == )
                 }
             }*/

            if (scannedEnemies == null)
            {
                this.gameObject.SetActive(false);
            }

            if (enemyList.Count == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }


    Transform[] FindClosestEnemies()
    {
        int enemyCounter = 0;
        Enemy[] allEnemies = GameObject.FindObjectsOfType<Enemy>();

        foreach (Enemy currentEnemy in allEnemies)
        {
            if (Vector3.Distance(currentEnemy.transform.position, transform.position) < 15.0f)
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;

                closestEnemies[enemyCounter] = currentEnemy.transform;
                enemyCounter++;
            }
        }
        return closestEnemies;
    }

    void FireMissiles(Transform[] enemies, int maxTargets)
    {
        int targetsAcquired = 0;

        while (targetsAcquired <= maxTargets)
        {
            GameObject homingMissileClone = Instantiate(homingMissilePrefab, transform.position, Quaternion.identity);
            homingMissileClone.GetComponent<HomingProjectileController>().target = enemies[targetsAcquired];
            targetsAcquired++;
        }
    }

    void FireMissilesToClosestEnemies(List<Enemy> enemiesScanned)
    {
        int targetsAcquired = 0;
        while (targetsAcquired <= enemiesScanned.Count - 1)
        {
            if (enemiesScanned[targetsAcquired] == null)
            {
                targetsAcquired++;
            }
            else
            {
                GameObject homingMissileClone = Instantiate(homingMissilePrefab, player.transform.position, Quaternion.identity);
                homingMissileClone.GetComponent<HomingProjectileController>().target = enemiesScanned[targetsAcquired].transform;
                targetsAcquired++;
            }
        }
    }

    void FireMissilesSalvo(List<Enemy> enemiesScannedDupe)
    {
        
        if(timerToShootAnother <= 0)
        {
            foreach (Enemy enemyToShoot in enemiesScannedDupe)
            {
                if(enemyToShoot.closeCounter >= 1)
                {
                    //lPos = MissileOriginArray[enemyToShoot.closeCounter - 1].transform.localPosition;
                    //wPos = MissileOriginArray[enemyToShoot.closeCounter - 1].transform.TransformPoint(lPos);
                    //GameObject homingMissileClone = Instantiate(homingMissilePrefab, wPos, Quaternion.identity);
                    //GameObject vfxInitiationTirClone = Instantiate(vfxInitiationTirPrefab, wPos, Quaternion.identity);
                    //Debug.Log("World Position" + wPos);
                    //Debug.Log("Local Position" + lPos);

                    GameObject homingMissileClone = Instantiate(homingMissilePrefab, MissileOriginArray[enemyToShoot.closeCounter - 1].transform, false);
                    homingMissileClone.transform.parent = null;
                    homingMissileClone.transform.localScale = homingMissilePrefab.transform.localScale;
                    GameObject vfxInitiationTirClone = Instantiate(vfxInitiationTirPrefab, MissileOriginArray[enemyToShoot.closeCounter - 1].transform, false);
                    vfxInitiationTirClone.transform.parent = null;
                    vfxInitiationTirClone.transform.localScale = Vector3.one;

                    homingMissileClone.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                    enemyToShoot.closeCounter--;
                    CinemachineCameraShake.Instance.CameraShake(shakeIntensity, shakeTime);
                    audioSource.PlayOneShot(shootSound);
                }                
            }
            
            timerToShootAnother = initTimer;
        }
        
    }


    void ShootSalvoToEnemy(Enemy enemyToShoot)
    {

        //Version to fire one missile at a time to each enemy.

        if (timerToShootAnother <= 0 && enemyToShoot.closeCounter > 0)
        {

        }

        /* Version to fire all missiles at once to all enemies.
         * while(enemyToShoot.closeCounter > 0)
         * {
         * 
         * switch (enemyToShoot.closeCounter)
        {

            case 10:
                GameObject homingMissileClone10 = Instantiate(homingMissilePrefab, MissileOriginArray[9].transform.position, Quaternion.identity);
                homingMissileClone10.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 9:
                GameObject homingMissileClone9 = Instantiate(homingMissilePrefab, MissileOriginArray[8].transform.position, Quaternion.identity);
                homingMissileClone9.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 8:
                GameObject homingMissileClone8 = Instantiate(homingMissilePrefab, MissileOriginArray[7].transform.position, Quaternion.identity);
                homingMissileClone8.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 7:
                GameObject homingMissileClone7 = Instantiate(homingMissilePrefab, MissileOriginArray[6].transform.position, Quaternion.identity);
                homingMissileClone7.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 6:
                GameObject homingMissileClone6 = Instantiate(homingMissilePrefab, MissileOriginArray[5].transform.position, Quaternion.identity);
                homingMissileClone6.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 5:
                GameObject homingMissileClone5 = Instantiate(homingMissilePrefab, MissileOriginArray[4].transform.position, Quaternion.identity);
                homingMissileClone5.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 4:
                GameObject homingMissileClone4 = Instantiate(homingMissilePrefab, MissileOriginArray[3].transform.position, Quaternion.identity);
                homingMissileClone4.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 3:
                GameObject homingMissileClone3 = Instantiate(homingMissilePrefab, MissileOriginArray[2].transform.position, Quaternion.identity);
                homingMissileClone3.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 2:
                GameObject homingMissileClone2 = Instantiate(homingMissilePrefab, MissileOriginArray[1].transform.position, Quaternion.identity);
                homingMissileClone2.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
            case 1:
                GameObject homingMissileClone1 = Instantiate(homingMissilePrefab, MissileOriginArray[0].transform.position, Quaternion.identity);
                homingMissileClone1.GetComponent<HomingProjectileController>().target = enemyToShoot.transform;
                enemyToShoot.closeCounter--;
                break;
        } 
        }
         */
    }


    public void NewScanForEnemies()
    {
        if (enemyList.Count == 0)
            return;
        Enemy closestEnemy = null;
        foreach (Enemy enemy in enemyList)
        {
            if (!scannedEnemies.Contains(enemy))
            {
                closestEnemy = enemy;
                closestEnemy.target.SetActive(true);
                break;
            }
        }
        if (closestEnemy != null)
        {
            foreach (Enemy enemyToScan in enemyList)
            {
                if (enemyToScan.distanceToPlayer < closestEnemy.distanceToPlayer && !scannedEnemies.Contains(enemyToScan))
                {
                    closestEnemy = enemyToScan;
                    closestEnemy.target.SetActive(true);
                }
            }
            scannedEnemies.Add(closestEnemy);
        }
    }

    //c'étais en OnTriggerEnter avant, on test avec le Stay pour voir si ca résout le bug des ennemies que sont pas lockable
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.GetComponent<Enemy>().isInList == false)
            {
                enemyList.Add(other.GetComponent<Enemy>());
                other.GetComponent<Enemy>().isInList = true;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (!scannedEnemies.Contains(other.gameObject.GetComponent<Enemy>()))
            {
                OnEnemyDestroyed(other.gameObject.GetComponent<Enemy>());
                other.gameObject.GetComponent<Enemy>().target.SetActive(false);
            }
        }
    }

    public void OnEnemyDestroyed(Enemy enemy)
    {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
            enemy.isInList = false;
        }
        if (scannedEnemies.Contains(enemy))
        {
            scannedEnemies.Remove(enemy);
            if (scannedEnemies.Count == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
        if (enemyList.Count == 0 && scannedEnemies.Count == 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    void EnemyNotInListAnymore()
    {
        foreach (Enemy e in enemyList)
        {
            if (e.isInList)
            {
                e.isInList = false;
                e.target.SetActive(false);
            }
        }
    }

    public void SalvoScan()
    {
        if (enemyList.Count == 0)
            return;
        foreach (Enemy enemyToScan in enemyList)
        {
            if (!scannedEnemies.Contains(enemyToScan))
            {
                scannedEnemies.Add(enemyToScan);
                enemyToScan.target.SetActive(true);
            }
            enemyToScan.closeCounter++;
            if (!enemyToScan.isMaxHit)
            {
                enemyToScan.animator.Play("Lock_counter");
                audioSource.PlayOneShot(lockSound);
            }
            if (scannedEnemies.Count >= 15)
            {
                break;
            }
        }
    }

}

