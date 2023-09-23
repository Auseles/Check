using System.Text.RegularExpressions;
string pathRead = "чеки.txt";
string pathWrite = "чеки_по_папкам.txt";
string[] monthArray = { "январь", "февраль", "март", "апрель", "май", "июнь", "июль", "август", "сентябрь", "октябрь", "ноябрь", "декабрь" };
string[] serviceArray = { "газоснабжение", "ГВС", "домофон", "капремонт", "квартплата", "ТБО", "теплоснабжение", "ХВС", "электроснабжение" };
string text = "";
string textNotFound = "Не оплачены:\n";

foreach (string month in monthArray)
{
    string[] serviceArray2 = new string[9];
    int i = 0;
    foreach (string service in serviceArray)
    {
        using (StreamReader reader = new StreamReader(pathRead))
        {
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                Regex regex = new Regex(@service + "_" + month);
                MatchCollection matches = regex.Matches(line);
                if (matches.Count > 0)
                {
                    text += ("/" + month + "/" + line + "\n");
                    serviceArray2[i] = service;
                    i++;
                }
            }
        }
    }
    textNotFound += (month + ":" + "\n");
    foreach (string service in serviceArray)
    {
        bool flag = false;
        foreach (string service2 in serviceArray2)
        {
            if (service2 == service)
            { flag = true; break; }           
        }
        if (!flag)
            textNotFound += (service + "\n");
    }
}
using (StreamWriter writer = new StreamWriter(pathWrite, false))
{
    text += textNotFound;
    await writer.WriteLineAsync(text);
}
