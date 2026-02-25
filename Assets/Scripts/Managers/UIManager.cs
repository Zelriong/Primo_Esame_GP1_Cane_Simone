using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject PauseMenu;
    [SerializeField] Image hpFillBar;
    [SerializeField] TMP_Text money;
    int totalMoney = 100;
    private void OnEnable()
    {
        Enemy.SetScore += UpdateUI;

    }

    private void OnDisable()
    {
        Enemy.SetScore -= UpdateUI;
    }

    private void Start()
    {
        
        money.text = totalMoney.ToString();
    }
    private void Update()
    {
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

    private void UpdateUI(int score)
    {
        score = 
        totalMoney += score;
        money.text = totalMoney.ToString();
    }
}