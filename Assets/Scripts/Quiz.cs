using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* Gives us access to Text Mesh Pro element*/
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    /*create a variable to store reference to text mesh pro object on our canvas*/
    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] List <QuestionSO> questions = new List<QuestionSO>();

    /*To get the question text out of the scriptable object, we create a variable to hold the 
    scriptable object we're woking with. Just we like have variables for things like strings and integers we can have variables
    that relate to the name of another scripts*/
    QuestionSO currentQuestion;

    [Header("Answers")]
    /* We could store the button component directly but because we are not going to do much with the button component 
    we can use an arry of type GameObject and select the items in this array in the inspector*/
    [SerializeField] GameObject[] answerButtons;

    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]

    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressBar;
    public bool isComplete;

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;

    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        Debug.Log(timer.loadNextQuestion);
        Debug.Log(timer.isAnsweringQuestion);
        if(timer.loadNextQuestion)
        {
             if(progressBar.value == progressBar.maxValue)
            {
                isComplete = true;
                return;
            }
            hasAnsweredEarly = false;
            GetNextQuestion();
            Debug.Log("Loaded question");
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    /*This method is going to be used when the user click on an answer, it takes the index of the button that was selected by the user*/
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);

        /* once the user has selected an answer, we're setting the interactivity of the buttons to false by passig in bool false*/
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

    }

    void DisplayAnswer(int index)
    {
         /*variable of type image*/
        Image buttonImage;

        /*if the selected button index matches the button index of the correct answer (we can grab the correct answer from question 1, that is a 
        scriptable object, via the object method GetCorrectAnswerIndex) then we display "Correct!" (by changing the text in the questionText.text
        variable). We then access the image on the button by grabbing the image component on that button object and setting the sprite for that
        image to correct answer sprite */

        /*if the selected button index does not match the button index of the correct answer, then we display "the correct answer is" and we 
        concatenate that with the correct answer (we get the correct answer from a combination of the question (scriptable object) GetAnswer 
        method that grabs the text for the correct answer and the question (scriptable object) GetCorrectAnswerIndex method. We then use the 
        same prcocess that we used if there was a match between indeces to highlight the correct answer */
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            questionText.text = "The correct answer is\n" + currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion() 
    {
        if(questions.Count > 0)
        {
             /*When we move on to the next question, we pass the bool value of true to make all buttons interactive again and we display the 
            new question and make the buttons the default color*/ 
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
       
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    void DisplayQuestion()
    {
        /* getter method that allows the questionText variable (which is set, in the inspector,to text mesh pro UI object that holds 
        the question) and scriptable object 'question' (holds specific question) to connect so that the scriptable object question is displayed
        in the questionText variable (text mesh pro UI object on the canvas object) object variable 'question' */
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state) 
    {
        /*for loop that sets each button's interactivity to either false or true depending on the bool passed into the function*/
        for (int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprites()
    {
        Image buttonImage;
        for (int i=0; i < answerButtons.Length; i++)
        {
             buttonImage = answerButtons[i].GetComponent<Image>();
             buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
