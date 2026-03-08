using Unity.VisualScripting;
using UnityEngine;


public enum GameStatus
{
    GameRunning,
    GamePaused
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float currentHealth;
    public int maxHealth = 200;
    public int money = 300;
    public int baseTurretPrice = 150;
    public int mgTurretPrice = 200;
    public int aoeTurretPrice = 300;

    public bool canBuild; //bool che permette l'attivazione dei button
    public int turretNum; //numero del tipo di torretta da spawnare
    public bool building; //bool che permette di costruire su una piattaforma
    public int maxTurretCount = 9; //numero di piattaforme disponibili (so che era meglio con un array magari, ma non volevo complicarmi la vita)

    [SerializeField] GameObject loseMenu;
    public int defeatedEnemies = 0;
    public int passedEnemies = 0;
    public int bonusEnemyDamage = 5; //valore che verrà aggiunto al danno dei nemici ogni 10 istanze
    public int maxHealthMultiplier = 1; //multiplier della vita che aumenterà di 1 ogni volta
    public GameStatus status;
    public int enemyTotalAddedDamage; //il danno in più che verrà aggiunto ai nemici ogni 10 spawn
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;

    }



    private void OnEnable()
    {
        EnemySpawner.EnemyBuff += EnemyDamage;
        Enemy.OnDamageDeal += DamageTake;
    }

    private void OnDisable()
    {
        EnemySpawner.EnemyBuff -= EnemyDamage;
        Enemy.OnDamageDeal -= DamageTake;
    }

    private void Start()
    {
        currentHealth = maxHealth;
        enemyTotalAddedDamage = 0; //parte col danno base
        canBuild = true;
    }


    private void Update()
    {
        if (status == GameStatus.GamePaused) //per rendere il tempo nullo in pausa
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;

        if (currentHealth <= 0)
        {
            LoseGame();
        }

        if (maxTurretCount <= 0) //non fa più costruire se le pedane sono piene
        {
            canBuild = false;
        }
    }


    private void LoseGame()
    {
        loseMenu.gameObject.SetActive(true);
        status = GameStatus.GamePaused;
    }

    private void DamageTake(int damage)
    {
        currentHealth -= damage; //danno preso da Enemy
    }

    public void EnemyDamage()
    {
        enemyTotalAddedDamage += bonusEnemyDamage; //aumenta il danno dei nemici di un valore custom
        maxHealthMultiplier++; //aumenta la vita dei nemici
    }
}
