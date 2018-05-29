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
    bool randomQuestionOrder = true;

    [SerializeField]
    TextMeshProUGUI questionTextField;

    [SerializeField]
    TextMeshProUGUI messagesTextField;

    [SerializeField]
    Button showAnswerButton;

    [SerializeField]
    Button nextQuestionButton;

    [SerializeField]
    Toggle orderOffToggle;

    [SerializeField]
    Toggle randomQuestionOrderToggle;

    [SerializeField]
    List<QuizInput> inputs;

    [SerializeField]
    List<string> answerPrefixes;

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

    bool badAnswerMarked = false;

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

        badAnswerMarked = false;
        selectedAnswer = -1;

        ++currentQuestionIndex;

        ResetGUI();

        if (currentQuestionIndex >= entries.Count)
        {
            GameFinished();
            return;
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
            if (!badAnswerMarked)
            {
                if (currentQuestion.IsValid && !currentQuestion.IsAnswerCorrect(selectedAnswer))
                {
                    inputs[selectedAnswer].incorrectImage.gameObject.SetActive(true);
                }

                inputs[currentQuestion.AnswerID].correctImage.gameObject.SetActive(true);

                badAnswerMarked = true;
            }

            return;
        }

        questionState = QuestionState.Answers;

        if (selectedAnswer != -1)
        {
            if (currentQuestion.IsValid && !currentQuestion.IsAnswerCorrect(selectedAnswer))
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

        inputs[currentQuestion.AnswerID].correctImage.gameObject.SetActive(true);

        badAnswerMarked = true;
    }

    public void SetQuestion(QuizEntry e, bool randomize = true)
    {
        if (randomize)
        {
            e.RandomizeOrder(randomQuestionOrder);
        }

        currentQuestion = e;

        questionState = QuestionState.Question;

        questionTextField.text = currentQuestion.question;

        for (int i = 0; i < currentQuestion.answers.Count; i++)
        {
            if (i >= inputs.Count)
            {
                break;
            }

            string ca = "";

            if (answerPrefixes.Count > i)
            {
                ca += answerPrefixes[i];
            }

            ca += currentQuestion.GetAnswer(i);

            inputs[i].text.text = ca;
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
        string save = "V2;";

        save += randomOrder + ";" + randomQuestionOrder + ";" + currentQuestionIndex + ";" + currentQuestion.GetSaveString() + ";" + selectedAnswer + ";" + goodAnswer + ";" + badAnswer + ";" + (int)state + ";" + (int)questionState;

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
            int index = -1;

            if (!indices[++index].Equals("V2"))
            {
                return false;
            }

            SetupRandomOrderGUI(bool.Parse(indices[++index]));
            randomQuestionOrder = bool.Parse(indices[++index]);
            SetupRandomQuestionOrderGUI(randomQuestionOrder);
            currentQuestionIndex = int.Parse(indices[++index]);
            string currentQuestionSaveString = indices[++index];
            selectedAnswer = int.Parse(indices[++index]);
            goodAnswer = int.Parse(indices[++index]);
            badAnswer = int.Parse(indices[++index]);
            questionState = (QuestionState)int.Parse(indices[++index]);
            state = (QuizState)int.Parse(indices[++index]);
            

            for (int i = ++index; i < indices.Length; i++)
            {
                order.Add(int.Parse(indices[i]));
            }

            entries[order[currentQuestionIndex]].loadOrderFromString(currentQuestionSaveString);

            ResetGUI();
            SetQuestion(entries[order[currentQuestionIndex]], false);

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

    public void SetupRandomOrderGUI(bool order)
    {
        if (!order)
        {
            orderOffToggle.isOn = true;
        }
    }

    public void SetRandomQuestionOrder(bool on)
    {
        randomQuestionOrder = on;
    }

    public void SetupRandomQuestionOrderGUI(bool order)
    {
        randomQuestionOrderToggle.isOn = order;
    }
}
