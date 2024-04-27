using System.Collections.Generic;
using UnityEngine;

namespace Quiz
{
    [CreateAssetMenu(fileName = "Quiz", menuName = "Quiz/Quiz")]
    public class QuizScriptable : ScriptableObject
    {
        public Color color;
        public string quizName;
        public List<QuizQuestion> questions;
        
    }
    
    [CreateAssetMenu(fileName = "QuizQuestion", menuName = "Quiz/QuizQuestion")]
    public class QuizQuestion : ScriptableObject
    {
        public string question;
        public List<string> answers;
        public string correctAnswer;
    }
}
