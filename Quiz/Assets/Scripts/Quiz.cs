using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum QuizState
{
    None, Running, Finished
}

public enum QuestionState
{
    Question, Answers
}

public class Quiz : MonoBehaviour {
    [SerializeField]
    bool randomOrder = true;

    [SerializeField]
    TextMeshProUGUI questionTextField;

    [SerializeField]
    TextMeshProUGUI messagesTextField;

    [SerializeField]
    Button showAnswerButton;

    [SerializeField]
    Button nextQuestionButton;

    [SerializeField]
    List<QuizInput> inputs;

    [SerializeField]
    TextAsset data;

    [SerializeField]
    List<QuizEntry> entries;

    [SerializeField]
    List<int> order;

    [SerializeField]
    int currentQuestionIndex;

    [SerializeField]
    QuizEntry currentQuestion;

    [SerializeField]
    QuizState state;

    [SerializeField]
    QuestionState questionState;

    [SerializeField]
    int selectedAnswer;

    [SerializeField]
    int goodAnswer = 0;

    [SerializeField]
    int badAnswer = 0;

    // Use this for initialization
    void Awake() {
        entries = SimpleTexFormatParser.Parse(data.text);

        if (!Load())
        {
            NewGame();
        }
    }

    public void NewGame()
    {
        state = QuizState.Running;
        questionState = QuestionState.Question;
        goodAnswer = 0;
        badAnswer = 0;
        selectedAnswer = -1;

        order.Clear();

        for (int i = 0; i < entries.Count; i++)
        {
            order.Add(i);
        }

        if (randomOrder)
        {
            order.Shuffle();
        }
       // else
       // {
       //     order.Sort();
       // }

        currentQuestionIndex = -1;

        NextQuestion();
    }

    public void NextQuestion()
    {
        if (state != QuizState.Running)
        {
            return;
        }

        selectedAnswer = -1;
        ResetGUI();

        ++currentQuestionIndex;

        if (currentQuestionIndex >= entries.Count)
        {
            GameFinished();
        }

        SetQuestion(entries[order[currentQuestionIndex]]);
    }

    public void ShowAnswers()
    {
        if (state != QuizState.Running)
        {
            return;
        }

        if (questionState == QuestionState.Answers)
        {
            if (currentQuestion.IsValid && currentQuestion.answerId != selectedAnswer)
            {
                inputs[selectedAnswer].incorrectImage.gameObject.SetActive(true);
            }

            inputs[currentQuestion.answerId].correctImage.gameObject.SetActive(true);

            return;
        }

        questionState = QuestionState.Answers;

        if (selectedAnswer != -1)
        {
            if (currentQuestion.IsValid && currentQuestion.answerId != selectedAnswer)
            {
                inputs[selectedAnswer].incorrectImage.gameObject.SetActive(true);

                ++badAnswer;
            }
            else
            {
                if (currentQuestion.IsValid)
                {
                    ++goodAnswer;
                }
            }
        }
        else
        {
            ++badAnswer;
        }

        if (!currentQuestion.IsValid)
        {
            messagesTextField.text = "A kérdésre még nincs meg a válasz!";
            return; ;
        }

        inputs[currentQuestion.answerId].correctImage.gameObject.SetActive(true);
    }

    public void SetQuestion(QuizEntry e)
    {
        currentQuestion = e;

        questionState = QuestionState.Question;

        questionTextField.text = currentQuestion.question;

        for (int i = 0; i < currentQuestion.answers.Count; i++)
        {
            if (i >= inputs.Count)
            {
                break;
            }

            inputs[i].text.text = currentQuestion.answers[i];
        }
    }

    public void ResetGUI()
    {
        messagesTextField.text = (currentQuestionIndex + 1) + "/" + entries.Count + " - jó: " + goodAnswer + " rossz: " + badAnswer;

        for (int i = 0; i < inputs.Count; i++)
        {
            inputs[i].Reset();
        }
    }

    public void GameFinished()
    {
        state = QuizState.Finished;
    }

    public void Save()
    {
        string save = "";

        save += currentQuestionIndex + ";" + selectedAnswer + ";" + goodAnswer + ";" + badAnswer + ";" + (int)state + ";" + (int)questionState;

        for (int i = 0; i < order.Count; i++)
        {
            save += ";";

            save += order[i];
        }

        PlayerPrefs.SetString("qsave", save);
    }

    public bool Load()
    {
        string save = PlayerPrefs.GetString("qsave", "");

        if (save.Equals(""))
        {
            return false;
        }

        string[] indices = save.Split(';');

        if (indices.Length <= 5)
        {
            return false;
        }

        try
        {
            currentQuestionIndex = int.Parse(indices[0]);
            selectedAnswer = int.Parse(indices[1]);
            goodAnswer = int.Parse(indices[2]);
            badAnswer = int.Parse(indices[3]);
            questionState = (QuestionState)int.Parse(indices[5]);
            state = (QuizState)int.Parse(indices[4]);
            

            for (int i = 6; i < indices.Length; i++)
            {
                order.Add(int.Parse(indices[i]));
            }

            ResetGUI();
            SetQuestion(entries[order[currentQuestionIndex]]);

            questionState = (QuestionState)int.Parse(indices[5]);

            if (questionState == QuestionState.Answers)
            {
                ShowAnswers();
            }
        }
        catch (System.Exception)
        {
            return false;
        }

        return true;
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void SelectAnswer(int id)
    {
        selectedAnswer = id;

        ShowAnswers();
    }

    public void SetRandomOrder(bool on)
    {
        randomOrder = on;
    }
}
