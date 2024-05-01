using System;
using System.Collections.Generic;
using Core;
using SavingSystem;
using Sound;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Image = UnityEngine.UIElements.Image;

namespace Quiz
{
    public class QuizPhaseManager : MonoBehaviour
    {
        [SerializeField] private List<QuizScriptable> quizList;
        
        private int currentQuestion = 0;
        
        [SerializeField] private List<GameObject> answerButtons;
        [SerializeField] private TMP_Text questionText;
        
        [SerializeField] private Sprite correctSprite;
        [SerializeField] private Sprite nullSprite;
        [SerializeField] private List<RawImage> feedbackImages;
        [SerializeField] private TMP_Text classNameText;
        
        [SerializeField] private GameObject winScreen;
        
        List<QuizScriptable> selectedQuizzes = new();
        List<QuizQuestion> alreadyUsedQuestions = new();
        
        System.Random rng = new System.Random();

        private QuizScriptable currentQuizScriptable;
        private QuizQuestion currentQuizQuestion;
        
        private AudioSource audioSource;

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
            
            foreach (var quiz in selectedQuizzes)
            {
                Debug.Log(quiz.quizName);
            }
            
            NewQuiz();
            // Use the random index to access a random question
            currentQuizQuestion = GetANewQuestion();

            // Subscribe to the onClick event of each button
            foreach (var button in answerButtons)
            {
                button.GetComponent<Button>().onClick.AddListener(() => OnAnswerButtonClicked(button.GetComponentInChildren<TMP_Text>().text));
                var block = button.GetComponent<Button>().colors;
                block.normalColor = currentQuizScriptable.color;
                button.GetComponent<Button>().colors = block;
            }

            // Update the UI
            audioSource = ServiceLocator.Get<SoundPlayer>().PlayUntilStopped("QuizBackground", true);
            UpdateUI(currentQuizScriptable, currentQuizQuestion);
        }

        void OnAnswerButtonClicked(string answer)
        {
            Debug.Log(answer + " clicked" + currentQuizQuestion.correctAnswer);
            if (string.Equals(answer, currentQuizQuestion.correctAnswer, StringComparison.Ordinal))
            {
                // The answer is correct, move to the next question
                ServiceLocator.Get<SoundPlayer>().PlaySound("Correct");
                currentQuestion++;
                if (currentQuestion < 3)
                {
                    currentQuizQuestion = GetANewQuestion();
                    UpdateUI(currentQuizScriptable, currentQuizQuestion);
                }
                else
                {
                    NewQuiz();
                }
            }
            else
            {
                ServiceLocator.Get<SoundPlayer>().PlaySound("Wrong");
                ServiceLocator.Get<GameManager>().DeductTime();
                NewQuiz();
            }
            
            
            
            
        }
        
        void UpdateUI(QuizScriptable quiz, QuizQuestion question)
        {
            Debug.Log(question);
            questionText.text = question.question;
            classNameText.text = quiz.quizName;

            foreach (var feedBackImage in feedbackImages)
            {
                feedBackImage.texture = feedbackImages.IndexOf(feedBackImage) < currentQuestion ? correctSprite.texture : nullSprite.texture;
            }
            
            GetComponent<RawImage>().texture = quiz.background.texture;

            List<string> shuffledAnswers = new List<string>(question.answers);

            System.Random random = new System.Random();
            
            foreach (var button in answerButtons)
            {
                string answer = shuffledAnswers[random.Next(shuffledAnswers.Count)];
                button.GetComponentInChildren<TMP_Text>().text = answer;
                shuffledAnswers.Remove(answer);
                var block = button.GetComponent<Button>().colors;
                block.normalColor = currentQuizScriptable.color;
                button.GetComponent<Button>().colors = block;
            }
        }
        
        QuizQuestion GetANewQuestion()
        {
            // If all questions have been used, return null
            if (alreadyUsedQuestions.Count >= currentQuizScriptable.questions.Count)
            {
                return null;
            }

            // Use the random index to access a random question
            QuizQuestion newQuestion = currentQuizScriptable.questions[rng.Next(currentQuizScriptable.questions.Count)];

            // If the question has already been used, recursively call the function until a new question is found
            while (alreadyUsedQuestions.Contains(newQuestion))
            {
                newQuestion = currentQuizScriptable.questions[rng.Next(currentQuizScriptable.questions.Count)];
            }

            // Add the new question to the list of used questions
            alreadyUsedQuestions.Add(newQuestion);

            return newQuestion;
        }
        
        void NewQuiz()
        {
            if (selectedQuizzes.Count <= 0)
            {
                Destroy(audioSource);
                if(ServiceLocator.Get<GameManager>().currentDay >= 5)
                {
                    ServiceLocator.Get<SoundPlayer>().PlaySound("Victory");
                    winScreen.SetActive(true);
                }
                else
                {
                    ServiceLocator.Get<GameManager>().currentDay++;
                    SceneManager.LoadScene("School");
                }
                
            }
            else
            {
                var newQuiz = selectedQuizzes[rng.Next(selectedQuizzes.Count)];
                        
                selectedQuizzes.Remove(newQuiz);

                currentQuizScriptable = newQuiz;
                currentQuestion = 0;
                alreadyUsedQuestions = new List<QuizQuestion>();
                currentQuizQuestion = GetANewQuestion();
                UpdateUI(currentQuizScriptable, currentQuizQuestion);
            }
            
        }

        public void ReturnToMenu()
        {
            ServiceLocator.Get<SavingManager>().ResetGame();
            SceneManager.LoadScene("Menu");
        }
    }
}
