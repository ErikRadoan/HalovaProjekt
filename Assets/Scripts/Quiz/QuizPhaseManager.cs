using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Quiz
{
    public class QuizPhaseManager : MonoBehaviour
    {
        [SerializeField] private List<QuizScriptable> quizList;

        private int currentQuiz = 0;
        private int currentQuestion = 0;

        [SerializeField] private SpriteRenderer quizPBackGround;
        [SerializeField] private List<GameObject> answerButtons;
        [SerializeField] private TMP_Text questionText;
        
        List<QuizScriptable> selectedQuizzes = new();
        List<QuizQuestion> alreadyUsedQuestions = new();
        
        System.Random rng = new System.Random();

        private QuizScriptable currentQuizScriptable;
        private QuizQuestion currentQuizQuestion;

        void Start()
        {
            // Create a list of three random quizzes
            if(quizList.Count < 3)
            {
                Debug.LogError("Not enough quizzes in the list");
                return;
            }
            
            while (selectedQuizzes.Count < 3)
            {
                int randomIndex = rng.Next(quizList.Count);
                if (!selectedQuizzes.Contains(quizList[randomIndex]))
                {
                    selectedQuizzes.Add(quizList[randomIndex]);
                }
            }
            
            // Check if the list contains the quiz "Fyzika" and replace one of the quizzes with it
            if (!selectedQuizzes.Exists(quiz => quiz.quizName == "Fyzika"))
            {
                QuizScriptable fyzikaQuiz = quizList.Find(quiz => quiz.quizName == "Fyzika");
                if (fyzikaQuiz != null)
                {
                    selectedQuizzes[rng.Next(selectedQuizzes.Count)] = fyzikaQuiz;
                }
            }

            // Initialize the quiz
            currentQuizScriptable = selectedQuizzes[currentQuiz];

            // Use the random index to access a random question
            currentQuizQuestion = GetANewQuestion();

            // Subscribe to the onClick event of each button
            foreach (var button in answerButtons)
            {
                string answer = button.GetComponentInChildren<TMP_Text>().text;
                button.GetComponent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(answer));
                button.GetComponent<Button>().colors = currentQuizScriptable.color;
            }

            // Update the UI
            UpdateUI(currentQuizScriptable, currentQuizQuestion);
        }

        void OnAnswerButtonClicked(string answer)
        {
            if (answer == currentQuizQuestion.correctAnswer)
            {
                // The answer is correct, move to the next question
                currentQuestion++;
                if (currentQuestion < currentQuizScriptable.questions.Count)
                {
                    currentQuizQuestion = GetANewQuestion();
                    UpdateUI(currentQuizScriptable, currentQuizQuestion);
                }
                else
                {
                    
                    
                    if (selectedQuizzes.Count > 0)
                    {
                        NewQuiz();
                    }
                    else
                    { //TODO: Implement day system
                        //ServiceLocator.Get<GameManager>()...
                    }
                    
                }
            }
            else
            {
                // The answer is incorrect, show an error message
                Debug.Log("Incorrect answer");
                NewQuiz();
            }
            
            
        }
        
        void UpdateUI(QuizScriptable quiz, QuizQuestion question)
        {
            questionText.text = question.question;
            
            quizPBackGround.sprite = quiz.background;

            List<string> shuffledAnswers = new List<string>(question.answers);

            System.Random random = new System.Random();
            
            foreach (var button in answerButtons)
            {
                button.GetComponent<TMP_Text>().text = shuffledAnswers[random.Next(shuffledAnswers.Count)];
            }
        }
        
        QuizQuestion GetANewQuestion()
        {
            // Use the random index to access a random question
            currentQuizQuestion = currentQuizScriptable.questions[rng.Next(currentQuizScriptable.questions.Count)];
            if (alreadyUsedQuestions.Contains(currentQuizQuestion) && alreadyUsedQuestions.Count < currentQuizScriptable.questions.Count)
            {
                GetANewQuestion();
            }
            else if (alreadyUsedQuestions.Count < currentQuizScriptable.questions.Count)
            {
                alreadyUsedQuestions.Add(currentQuizQuestion);
                return currentQuizQuestion;
            }

            return null;
        }
        
        void NewQuiz()
        {
            var newQuiz = selectedQuizzes[rng.Next(selectedQuizzes.Count)];
                        
            selectedQuizzes.Remove(newQuiz);

            currentQuizScriptable = newQuiz;
            currentQuestion = 0;
            currentQuizQuestion = GetANewQuestion();
            UpdateUI(currentQuizScriptable, currentQuizQuestion);
        }
    }
}
