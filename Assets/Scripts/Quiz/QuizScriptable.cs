using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    [CreateAssetMenu(fileName = "Quiz", menuName = "Quiz/Quiz")]
    public class QuizScriptable : ScriptableObject
    {
        public Color color;
        public Sprite background;
        public string quizName;
        public List<QuizQuestion> questions;
        
    }
    
    
}
