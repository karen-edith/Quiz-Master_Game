using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    /* This allows us to control the text area by selecting min and max number of lines
    respectivley. This will display the full question in the editor and allow us to check for
    typos to make corrections */
    [TextArea(2,6)]

    /*we've serialized this field so that we can only access and change this variable from the 
    inspector but but becuase the variable is still private we cannot edit it directly from another
    script*/
   [SerializeField]string question = "Enter new question text here";
   
   //initialize array that will contain the 4 answer options
   [SerializeField]string[] answers = new string[4];

   [SerializeField] int correctAnswerIndex;

   /* to provide us read access we've created the GetQuestion method to get the question stored 
   within our variable which we can then use to display the question text on the canvas itself*/
   public string GetQuestion() 
   {
       return question;
   }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }
}
