using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamaManagerSc : MonoBehaviour
{
    [SerializeField]
    private GameObject squarePrefab;

    [SerializeField]
    private Transform questionPanel;

    [SerializeField]
    private Transform squaresPanel;

    [SerializeField]
    private Text questionText;

    [SerializeField]
    private Sprite[] squareSprites;

    [SerializeField]
    private GameObject resultPanel;

    [SerializeField]
    private AudioSource audioSource;

    public AudioClip buttonSound;

    private GameObject[] squaresSeries = new GameObject[25];

    List<int> levelValuesList = new List<int>();

    int dividingNumber, dividedNumber;
    int questionNumberIndex;
    int buttonValue;
    int correctAnswer;
    bool makeButtonClickable;
    public int remaningLife;
    string difficuly;

    RemaningLifeManager remaningLifeManager;
    PointManager pointManager;
    BackgroundMusic backgroundMusic;

    GameObject currentSquare;

    private void Awake()
    {
        remaningLife = 3;
        audioSource = GetComponent<AudioSource>();

        resultPanel.GetComponent<RectTransform>().localScale = Vector3.zero;

        remaningLifeManager = Object.FindObjectOfType<RemaningLifeManager>();
        pointManager = Object.FindObjectOfType<PointManager>();
        backgroundMusic = Object.FindObjectOfType<BackgroundMusic>();
        remaningLifeManager.CheckRemainingLives(remaningLife);
    }
    // Start is called before the first frame update
    void Start()
    {
        makeButtonClickable = false;
        questionPanel.GetComponent<RectTransform>().localScale = Vector3.zero;
        CreateSquares();
    }

    public void CreateSquares()
    {
        for (int i = 0; i < squaresSeries.Length; i++)
        {
            GameObject square = Instantiate(squarePrefab, squaresPanel);
            square.transform.GetChild(1).GetComponent<Image>().sprite = squareSprites[Random.Range(0, squareSprites.Length)];
            square.transform.GetComponent<Button>().onClick.AddListener(() => ButtonIsClicked()); // Butona onClickListener eklendi
            squaresSeries[i] = square;
        }
        WriteLevelValuesToText();
        StartCoroutine(DoFadeRoutine());
        Invoke("OpenQuestionPanel", 2f);
    }

    void ButtonIsClicked()
    {
        if (makeButtonClickable)
        {
            audioSource.PlayOneShot(buttonSound);

            // EventSystem'den butonlarin icindeki Text sayilari alindi
            buttonValue = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);
            currentSquare = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            CheckTheAnswer();
        }
    }

    void CheckTheAnswer()
    {
        if(buttonValue == correctAnswer)
        {
            currentSquare.transform.GetChild(1).GetComponent<Image>().enabled = true;
            currentSquare.transform.GetChild(0).GetComponent<Text>().text = "";
            currentSquare.transform.GetComponent<Button>().interactable = false;

            pointManager.IncreasePoints(difficuly);
            levelValuesList.RemoveAt(questionNumberIndex);

            if (levelValuesList.Count > 0)
            {
                OpenQuestionPanel();
            }
            else
            {
                GameOver();
            }
        }
        else
        {
            pointManager.DecreasePoints();
            remaningLife--;
            remaningLifeManager.CheckRemainingLives(remaningLife);
        }
        if(remaningLife <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        makeButtonClickable = false;
        backgroundMusic.acikMi = true;
        resultPanel.GetComponent<RectTransform>().DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach (var square in squaresSeries)
        {
            square.GetComponent<CanvasGroup>().DOFade(1f, 0.2f);

            yield return new WaitForSeconds(0.07f);
        }
    }

    void WriteLevelValuesToText()
    {
        foreach (var square in squaresSeries)
        {
            int randomValue = Random.Range(1, 13);
            levelValuesList.Add(randomValue);
            square.transform.GetChild(0).GetComponent<Text>().text = randomValue.ToString();
        }
    }

    void OpenQuestionPanel()
    {
        AskTheQuestion();
        makeButtonClickable = true;
        questionPanel.GetComponent<RectTransform>().DOScale(1f, 0.3f).SetEase(Ease.OutBack);
    }

    void AskTheQuestion()
    {
        dividingNumber = Random.Range(2, 11);
        questionNumberIndex = Random.Range(0, levelValuesList.Count);
        correctAnswer = levelValuesList[questionNumberIndex];
        dividedNumber = dividingNumber * correctAnswer;

        if(dividedNumber <= 40)
        {
            difficuly = "Easy";
        }else if(dividedNumber > 40 && dividedNumber <= 80)
        {
            difficuly = "Medium";
        }
        else if (dividedNumber > 80)
        {
            difficuly = "Hard";
        }
        questionText.text = dividedNumber.ToString()+ " : "+ dividingNumber.ToString();
    }
}
