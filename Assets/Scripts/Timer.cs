using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    /* variable of type float that is initialized to value that corresponds to time it takes to complete question*/
    [SerializeField] float timeToCompleteQuestion = 30f;

    /* variable of type float that is initialized to value that corresponds to time it takes to show correct answer once user has
    selected their response*/
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    /* variable that is declared to be of type bool, this will be the flag that is updated and lets the game know that we can 
    load the next question*/
    public bool loadNextQuestion;
    
    /* variable declared to be of type float, this variable will be updated to indicate the amount of fill to display for the timer
    (circle) sprite*/
    public float fillFraction;

    /* variable of bool type that will determine if the user is has provided an answer (if yes then it'll be false, if no then it'll
    be set to true*/
   public bool isAnsweringQuestion;
    
    /* variable of type float, this will determine the timer time*/
    float timerValue;

    /* every frame update, the updateTimer method get's called*/
    void Update()
    {
        updateTimer();   
    }

    /*method setims timer to 0*/
    public void CancelTimer()
    {
        timerValue = 0;
    }

    public void updateTimer()
    {
        //Debug.Log(timerValue);
        /*timerValue = timerValue - Time.deltaTime*/
        /*this subtracts the timerValue by the amount of time it took to go to the next frame, essentially counting down from the 
        time we're alloting for providing a response, and for showing correct answer*/
        /*WHERE ARE WE SETTING THIS VALUE?*/
        timerValue -= Time.deltaTime;
        //Debug.Log(timerValue);
        //Debug.Log(isAnsweringQuestion);
        //Debug.Log(loadNextQuestion);

        /* if the user has not yet provided a response (isAnsweringQuestion is true), then this code block executes*/
        if(isAnsweringQuestion)
        {
            /*this checks to see if the alloted time has run out, if it hasn't this code block executes*/
            if(timerValue > 0)
            {
                /*sets the variable fillFraction to the fraction of time left*/
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            /*if time has run out for the user to provide an answer then this code block executes*/
            else 
            {
                /* the inAnsweringQuestion is set to false and the timer value is set to a value that corresponds to the amount
                of time allocated to show correct answer*/
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        /*if the user has already answered the question*/
        else
        {
            /* if the timer hasn't run out on the allowed show time for correct answer then this block executes */
            /*WHERE ARE WE SETTING THIS VALUE?*/
            if(timerValue > 0)
            {
                /*fill fraction variable is set to a value to corresponds to the fraction of show time that has elapsed*/
                fillFraction = timerValue/timeToShowCorrectAnswer;
            }
            /* if the timer has run out the we're ready to go to the next question and this block executes*/
            else
            {
                /* to indicate that the user is again answer a question (pending response) we set isAnsweringQuestion to true*/
                isAnsweringQuestion = true;
                /* the timer is set to a a value that corresponds with teh alloted time to provide a response */
                timerValue = timeToCompleteQuestion;
                /* the load quertion variable is also set to true, indicating that we need to move on to the next question*/
                loadNextQuestion = true;
            }
        }
    }
}
