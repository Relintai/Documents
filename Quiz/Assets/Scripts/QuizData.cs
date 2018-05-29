using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuizData {
    public List<QuizEntry> entries = new List<QuizEntry>();

    public QuizEntry GetNextQuastion()
    {
        return null;
    }
}
