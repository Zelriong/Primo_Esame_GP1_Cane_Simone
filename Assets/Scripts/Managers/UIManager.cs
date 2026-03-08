using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Image hpFillBar;
    [SerializeField] TMP_Text money;

    [Header("Turrets Buttons")]
    [SerializeField] Button baseButton;
    [SerializeField] TMP_Text baseTurretTXT;
    [SerializeField] Button mgButton;
    [SerializeField] TMP_Text mgTurretTXT;
    [SerializeField] Button aoeButton;
    [SerializeField] TMP_Text aoeTurretTXT;
    
    [SerializeField] TMP_Text timerTXT;
    [SerializeField] TMP_Text loseTimerTXT; // test del tempo di fine schermata
    [SerializeField] TMP_Text enemiesKilledCount;
    [SerializeField] TMP_Text enemiesPassedCount;

    float timer;
    float minutes;
    float seconds;
    private void OnEnable()
    {
        Enemy.SetScore += UpdateUI; // qua avrei potuto fare la stessa cosa creando un evento per quando piazzo le torrette e le potenzio,
                                    // ma l'ho messo direttamente nelle loro funzioni perché prendono i money nell'instance del GM
    }

    private void OnDisable()
    {
        Enemy.SetScore -= UpdateUI;
    }

    private void Start()
    {
         
        money.text = GameManager.instance.money.ToString();
        baseTurretTXT.text = GameManager.instance.baseTurretPrice.ToString();
        mgTurretTXT.text = GameManager.instance.mgTurretPrice.ToString();
        aoeTurretTXT.text = GameManager.instance.aoeTurretPrice.ToString();
    }
    private void Update()
    {
        money.text = GameManager.instance.money.ToString(); //qua aggiorna sempre la ui con i soldi effettivi (so che avrei potuto metterlo fuori dall'update per non appesantire il codice
                                                            //aggiornandolo ogni volta che succedeva qualcosa nelle varie funzioni relative ai soldi,
                                                            //ma mi sarei complicato la vita inutilmente in questo caso
        hpFillBar.fillAmount = GameManager.instance.currentHealth / GameManager.instance.maxHealth;

        if (Input.GetKeyDown(KeyCode.P) && GameManager.instance.status == GameStatus.GameRunning)
        {
            OpenPauseMenu();
            return;
        }

        if (Input.GetKeyDown(KeyCode.P) && GameManager.instance.status == GameStatus.GamePaused)
        {
           ClosePauseMenu();
            return;
        }
        #region turretButtons 
        //tutta la gestione dei bottoni delle torrette in ui, cosìche siano attive solo quando
        //i soldi sono abbastanza e ci sono ancora dei posti dove spawnarle
        if (GameManager.instance.money >= GameManager.instance.baseTurretPrice && GameManager.instance.canBuild == true)
        {
            baseButton.interactable = true;
        }
        else baseButton.interactable = false;


        if (GameManager.instance.money >= GameManager.instance.mgTurretPrice && GameManager.instance.canBuild == true)
        {
            mgButton.interactable = true;
        }
        else mgButton.interactable = false;


        if (GameManager.instance.money >= GameManager.instance.aoeTurretPrice && GameManager.instance.canBuild == true)
        {
            aoeButton.interactable = true;
        }
        else aoeButton.interactable = false;
        #endregion

        #region timer
        timer += Time.deltaTime;
        
        //converto i secondi in minuti
        minutes = Mathf.Floor(timer / 60);

        //restituisco il resto clampandolo
        seconds = Mathf.FloorToInt(timer % 60);

        timerTXT.text = string.Format("{0:00}:{1:00}", minutes, seconds); // do il formato in cui deve comparire
        #endregion
        if (GameManager.instance.currentHealth <= 0)
        {
            loseTimerTXT.text = ("You didn't lose your focus for: " + timerTXT.text + "!"); //prendo la reference per il messaggio di fine partita
            enemiesKilledCount.text = ("You defeated " + GameManager.instance.defeatedEnemies + " enemies");
            enemiesPassedCount.text = (GameManager.instance.passedEnemies + " enemies managed to pass");
        }
    }

    public void OpenPauseMenu()
    {
        PauseMenu.SetActive(true);
        GameManager.instance.status = GameStatus.GamePaused;
        
    }

    public void ClosePauseMenu()
    {
        PauseMenu.SetActive(false);
        GameManager.instance.status = GameStatus.GameRunning;
        
    }

    private void UpdateUI(int score) //qui aumenta il denaro quando muoiono i nemici
    {
       
        GameManager.instance.money += score;
       
    }
}