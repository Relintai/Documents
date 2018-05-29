using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizEntry {
    //public bool remap = true;
    public string question;
    public List<string> answers;
    public int answerId;
    public int remappedAnswerId;
    public List<int> order = new List<int>();

    public int AnswerID
    {
        get
        {
            return remappedAnswerId;
        }
    }

    public bool IsValid { get { return answerId != -1; } }

    public QuizEntry()
    {
        answers = new List<string>();
        answerId = -1;
    }

    public QuizEntry(string question, List<string> answers, int answerId)
    {
        this.question = question;
        this.answers = answers;
        this.answerId = answerId;
    }

    public void RandomizeOrder(bool remap = true)
    {
        order.Clear();

        for (int i = 0; i < answers.Count; i++)
        {
            order.Add(i);
        }

        if (remap)
        {
            order.Shuffle();
        }

        for (int i = 0; i < order.Count; i++)
        {
            if (order[i] == answerId)
            {
                remappedAnswerId = i;
                break;
            }
        }
    }

    public string GetAnswer(int index)
    {
        return answers[order[index]];
    }

    public int GetAnswerID(int index)
    {
        return order[index];
    }

    public bool IsAnswerCorrect(int answerId)
    {
        int ra = order[answerId];

        return this.answerId == ra;
    }

    public string GetSaveString()
    {
        if (order.Count == 0)
        {
            return "";
        }

        string ss = order[0].ToString();

        for (int i = 1; i < order.Count; i++)
        {
            ss += ":" + order[i];
        }

        return ss;
    }

    public void loadOrderFromString(string str)
    {
        string[] os = str.Split(':');

        for (int i = 0; i < os.Length; i++)
        {
            order.Add(int.Parse(os[i]));
        }
    }
}
