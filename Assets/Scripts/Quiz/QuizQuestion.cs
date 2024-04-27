using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    [CreateAssetMenu(fileName = "QuizQuestion", menuName = "Quiz/QuizQuestion")]
    public class QuizQuestion : ScriptableObject
    {
        public string question;
        public List<string> answers;
        public string correctAnswer;
        
    }
}
