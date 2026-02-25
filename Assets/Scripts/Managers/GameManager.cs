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

    [SerializeField] GameObject loseMenu;
    [SerializeField] public int bonusEnemyDamage = 5; //valore che verrà aggiunto al danno dei nemici ogni 10 istanze

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
        enemyTotalAddedDamage = 0;
    }


    private void Update()
    {
        if (status == GameStatus.GamePaused)
        {
            Time.timeScale = 0;
        }
        else Time.timeScale = 1;

        if (currentHealth <= 0)
        {
            LoseGame();
        }
    }


    private void LoseGame()
    {
        loseMenu.gameObject.SetActive(true);
        status = GameStatus.GamePaused;
    }

    private void DamageTake(int damage)
    {
        currentHealth -= damage;
    }

    public void EnemyDamage()
    {
        enemyTotalAddedDamage += bonusEnemyDamage; //aumenta il danno dei nemici di un valore custom
    }
}
