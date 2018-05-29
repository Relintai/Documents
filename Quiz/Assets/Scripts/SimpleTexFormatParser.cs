using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTexFormatParser {

	public static List<QuizEntry> Parse(string text)
    {
        List<QuizEntry> entries = new List<QuizEntry>();

        string[] a = new string[1];
        a[0] = "\\begin{tcolorbox}";

        string[] firstarr = text.Split(a, System.StringSplitOptions.None);

        //we can safely ignore the first entry, as it only contains the headers
        for (int i = 1; i < firstarr.Length; i++)
        {
            a[0] = "\\end{tcolorbox}";

            //the first entry should contain the text that we need
            string[] secondarr = firstarr[i].Split(a, System.StringSplitOptions.None);

            if (secondarr.Length == 0)
            {
                continue;
            }

            string body = secondarr[0];

            if (!body.Contains("\\tcblower"))
            {
                continue;
            }

            a[0] = "\\tcblower";

            string[] mainarr = body.Split(a, System.StringSplitOptions.RemoveEmptyEntries);

            if (mainarr.Length != 2)
            {
                Debug.Log("mainarr.lenght != 2! " + mainarr);
                continue;
            }

            QuizEntry qe = new QuizEntry();

            //remove the windows carriage return, so the strings are easier to split
            mainarr[0] = mainarr[0].Replace("\r", "");
            mainarr[1] = mainarr[1].Replace("\r", "");

            string[] questionarr = mainarr[0].Split('\n');

            string question = "";

            //we need to remove the tcolorbox attributes, so start with 1
            for (int j = 1; j < questionarr.Length; j++)
            {
                question += questionarr[j];
            }

            qe.question = question;

            string[] answersarr = mainarr[1].Split('\n');

            int indexj = 0;
            for (int j = 0; j < answersarr.Length; j++)
            {
                string answ = answersarr[j];
                answ = answ.Replace("\\\\", "");
                answ = answ.Trim();

                if (answ.Equals(""))
                {
                    continue;
                }

                if (answ.Contains("\\uline"))
                {
                    answ = answ.Replace("{", "");
                    answ = answ.Replace("}", "");
                    answ = answ.Replace("\\uline", "");
                    answ = answ.Trim();

                    qe.answerId = indexj;
                }

                answ = answ.Substring(3);

                qe.answers.Add(answ);

                ++indexj;
            }

            entries.Add(qe);
        }


        return entries;
    }
}
