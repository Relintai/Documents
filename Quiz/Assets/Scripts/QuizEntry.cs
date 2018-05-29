using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizEntry {
    public string question;
    public List<string> answers;
    public int answerId;

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
}
