using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
//using static System.Net.Mime.MediaTypeNames;

namespace WHILE_Berechenbarkeit
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Variable> Variablen = new();
        bool MakeStep = false;
        bool InProgress = false; // Berechnung läuft
        bool StepByStep = false;
        // Abbrechvariable
        bool stop = false;
        // Derzeitige Zeile
        int zeiger = 0;
        int logcount = 0;
        bool debug = false;
        int Structurecount = 0;
        string errorReason;
        int lastLineNumber = -1;

        private void Pruefen_btn_Click(object sender, EventArgs e)
        {
            if (InProgress)
            {
                TerminalTB.Text = "Die derzeitige Berechnung muss erst beendet werden!\r\n" + TerminalTB.Text;
                return;
            }
            InProgress = true;

            // Fehlerrot entfernen
            StartEnd entf = new();
            var (Estart, Eend) = StartEnd.GetLineStartEnd(EingabeTb.Text, Structurecount - 1);
            int Elength = Eend - Estart;
            if (Elength < 0) Elength = 0;
            EingabeTb.SelectionStart = Estart;
            EingabeTb.SelectionLength = Elength;
            EingabeTb.SelectionColor = Color.Black;

            // Herkunft des Klicks bestimmen            
            if (sender.ToString() == "System.Windows.Forms.Button, Text: Schrittweise Ausführen" || sender.ToString() == "Schrittweise")
            {// Schrittweise Ausführung aktivieren
                StepByStep = true;
            }
            else
            {
                if (StepByStep)
                {
                    stop = true;
                    Thread.Sleep(50);
                    InProgress = false;
                }
                StepByStep = false;
            }
            //Wichtig für den Zeiger, dass das auf den END ladet
            //EingabeTb.Text += "\n";   ist jetzt im Aufruf

            // Stellt sicher das alle nötigen gobalen Variablen richtig initialisiert sind .Ist zur Sicherheit nochmal am Ende
            stop = false;
            zeiger = 0;
            logcount = 0;
            Structurecount = 0;
            errorReason = "";
            lastLineNumber = -1;
            Variablen.Clear();
            TerminalTB.Text = "";

            string code = EingabeTb.Text;
            string[] lines = code.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            for (int i = 0; i < lines.Length; i++)
            {                
                if (lines[i].Contains('#'))
                {
                    lines[i] = lines[i].Substring(0, lines[i].IndexOf("#"));
                }
                else if (lines[i].Contains("//"))
                {
                    lines[i] = lines[i].Substring(0, lines[i].IndexOf("//"));                    
                }                
            }
            string cleancode = string.Join(Environment.NewLine, lines);            

            //Start der Syntaxprüfung
            bool result = CheckWhileStructure(cleancode + "\n");
            if (!result)
            {
                TerminalTB.Text = "Fehler in der rot geschriebenen Zeile gefunden." + "\r\n" + "Möglicher Grund: " + errorReason + "\r\n" + TerminalTB.Text;

                // Fehler Rot markieren
                StartEnd instance = new();
                var (start, end) = StartEnd.GetLineStartEnd(EingabeTb.Text, Structurecount - 1);
                int length = end - start;
                if (length < 0) length = 0;
                EingabeTb.SelectionStart = start;
                EingabeTb.SelectionLength = length;
                EingabeTb.SelectionColor = Color.Red;
                InProgress = false;
            }
            else // Wenn es kein Fehler gibt, Berechnung starten
            {
                //TerminalTB.Text = "Prüfung abgeschlossen: Akzeptiert" + TerminalTB.Text;
                Variablen = ExtractVariables(cleancode);
                string ogtext = EingabeTb.Text;
                Thread t = new(() => ThreadStart(cleancode, ogtext)) { IsBackground = true };
                t.Start();
            }
        }

        bool schrittweiseAusgabe = false;
        private void StepByStep_Click(object sender, EventArgs e)
        {
            if (InProgress == false)
            {
                schrittweiseAusgabe = true;
                MakeStep = false;
                //InProgress = true;
                Pruefen_btn_Click(sender, e);
            }
            else
            {
                MakeStep = true;
            }
        }

        void ThreadStart(string cleancode, string originalText)
        {
            zeiger = 0;
            ExecuteInput(cleancode, originalText);

            try
            {
                IAsyncResult asyncResult = TerminalTB.BeginInvoke((MethodInvoker)delegate
                {
                    TerminalTB.Text = "------------------------------------\n" + TerminalTB.Text;
                    foreach (Variable v in Variablen)
                    {
                        TerminalTB.Text = v.Name + " := " + v.Value.ToString() + "\r\n" + TerminalTB.Text;
                    }
                    TerminalTB.Text = "Ergebnis: --------------------------\n" + TerminalTB.Text;
                });
                TerminalTB.EndInvoke(asyncResult);
            }
            catch (InvalidOperationException) { }
            InProgress = false;
            schrittweiseAusgabe = false;

            //Aufräumen
            stop = false;
            zeiger = 0;
            logcount = 0;
            Structurecount = 0;
            errorReason = "";
            lastLineNumber = -1;
            Variablen.Clear();
        }

        void ExecuteInput(string input, string originalText)
        {
            // Mögliche Erkennung einer Endlosschleife
            logcount++;
            if (logcount == 100000)
            {
                DialogResult dialogResult = MessageBox.Show("Wahrscheinliche Endlosschleife endeckt (<100.000 Operationen), soll das Programm die Berechnung stoppen?", "Abbrechen", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    stop = true;
                }
            }
            //Debug.WriteLine(logcount);

            // Regulären Ausdruck an den Syntax anpassen 
            Regex assignmentRegex = new(@"^\s*[a-zA-Z_][a-zA-Z_0-9]*\s*:=\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)\s*([-|+|*])?\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)?(;+)?\s*$");
            Regex whileStartRegex = new(@"^\s*WHILE\s+(([a-zA-Z_][a-zA-Z_0-9]*))\s+!=\s+0\s+DO\s*$");//\s(.+)\sEND$");    eine WHILE-Anweisung, die eine Variable auf ungleich Null abfragt und ein WHILE-Programm zwischen DO und END enthält
            Regex END = new(@"^\s*END(;)?\s*$");
            Regex space = new(@"^\s*$");

            // In seperate Anweisungen unterteilen
            string[] instructions = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // ExecuteInput zähler
            int count = 0;
            // Anweisungen überspringen => Wichtig für WHILE
            int skips = 0;


            foreach (string instruction in instructions)
            {
                // Schon in While ausgeführtes skippen
                if (skips != 0)
                {
                    skips--;
                    count++;
                    continue;
                }

                if (whileStartRegex.IsMatch(instruction))
                {
                    //Prüfen ob msPerExecution an ist
                    CheckMarkOnOff(originalText);

                    //Whileabschnitt erhalten
                    string whileblock = "";
                    int whilecount = 1;
                    for (int i = count + 1; i <= instructions.Length - 1; i++)
                    {
                        if (whileStartRegex.IsMatch(instructions[i]))
                        {
                            whilecount++;
                        }
                        else if (END.IsMatch(instructions[i]))
                        {
                            whilecount--;
                            if (whilecount == 0)
                            {
                                break;
                            }
                        }
                        whileblock += instructions[i] + "\r\n";
                    }
                    //MessageBox.Show("wb: " + whileblock);

                    string[] whileblock2 = whileblock.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    skips = whileblock2.Length; //!War Count

                    // WHILE Zähler erhalten
                    Match match = whileStartRegex.Match(instruction);
                    string whilecountername = match.Groups[1].Value;
                    //Stelle des Zählers im Objekt Variablen finden
                    int iVarCounter = 0;
                    foreach (Variable v in Variablen)
                    {
                        if (v.Name == whilecountername)
                        {
                            whilecountername = v.Name;
                            break;
                        }
                        iVarCounter++;
                    }

                    zeiger++;
                    while (Variablen[iVarCounter].Value > 0)    //Schleife berechnen
                    {
                        ExecuteInput(whileblock, originalText);
                        zeiger -= whileblock2.Length; //Count
                    }
                    // -1 Damit der Zeiger nochmal auf END geht
                    zeiger += whileblock2.Length - 1; //Count
                    CheckMarkOnOff(originalText);
                    zeiger++;

                }
                else if (assignmentRegex.IsMatch(instruction))
                {
                    CheckMarkOnOff(originalText);
                    PerformCalculations(instruction);
                    zeiger++;
                }
                else if (END.IsMatch(instruction))
                {
                    zeiger++;
                }
                else if (space.IsMatch(instruction))
                {
                    zeiger++;
                }
                count++;
            }
        }


        private void CheckMarkOnOff(string originalText)
        {
            try
            {
                MarkLineGreen(originalText);
                if (StepByStep)
                {
                    // Zeile markieren

                    while (MakeStep == false)
                    {
                        Thread.Sleep(20);

                        if (stop == true) //Thread anhalten
                        {
                            //Beim Abbrechen Grün wieder weg machen
                            StartEnd instance = new();
                            var (start, end) = StartEnd.GetLineStartEnd(originalText, lastLineNumber);
                            int length = end - start;
                            if (length < 0) length = 0;
                            IAsyncResult asyncResult = EingabeTb.BeginInvoke((MethodInvoker)delegate
                            {
                                EingabeTb.SelectionStart = start;
                                EingabeTb.SelectionLength = length;
                                EingabeTb.SelectionColor = Color.Black;
                            });
                            EingabeTb.EndInvoke(asyncResult);

                            InProgress = false;
                            Thread t = Thread.CurrentThread;
                            try
                            {
                                t.Join();
                                t.Interrupt();
                                Thread.Sleep(1);
                            }
                            catch (ThreadInterruptedException ex)
                            {
                                Debug.WriteLine(ex);
                            }
                        }
                    }
                    MakeStep = false;
                }
                else
                {
                    // Zeile markieren
                    //MarkLineGreen(originalText);

                    int ms = 1;
                    //Zugriff auf den GUI-Thread für die Millisekunden
                    IAsyncResult asyncResult = MsPerExeCb.BeginInvoke((MethodInvoker)delegate
                    {
                        if (MsPerExeCb.Text == "") { MsPerExeCb.Text = "1"; }
                        ms = Convert.ToInt32(MsPerExeCb.Text);
                    });
                    MsPerExeCb.EndInvoke(asyncResult);

                    if (stop == true) //Thread anhalten
                    {
                        InProgress = false;
                        Thread t = Thread.CurrentThread;
                        try
                        {
                            t.Join();
                            t.Interrupt();
                            Thread.Sleep(1);
                        }
                        catch (ThreadInterruptedException ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    }
                    Thread.Sleep(ms);
                }
            }
            catch (InvalidOperationException) { }


        }

        void PerformCalculations(string input)
        {
            //Struktur: x1 := x2 +|-|* x3
            // Input gruppieren
            Regex assignmentRegex = new(@"^\s*([a-zA-Z_][a-zA-Z_0-9]*)\s*:=\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)\s*([+|*|-])?\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)?(;+)?\s*$");
            // Groups[1] == Zuweisungsvariable (x1)
            // Groups[2] == Erste Zahl (x2)
            // Groups[3] == Operator
            // Groups[4] == Zweite Zahl (x3)

            Match match = assignmentRegex.Match(input);
            if (match.Length == 0)
            {
                MessageBox.Show("Error in PerformCalculations"); //Falls dich mal was falsches hier landet
                return;
            }

            // "x1"
            string variableName = match.Groups[1].Value;

            // x1 in Liste finden
            Variable x1 = new("NULL", 0);
            int x1count = 0;
            foreach (Variable v in Variablen)
            {
                if (v.Name == variableName)
                {
                    x1 = v;
                    break;
                }
                x1count++;
            }
            //MessageBox.Show("START:" + x1.Name + ": " + x1.Value);

            //Erste Variable oder Zahl bekommen
            Variable x2 = new("NULL", 0);
            string ersteZahl = match.Groups[2].Value;
            //int x2 = 0;
            foreach (Variable v in Variablen)
            {
                if (v.Name == ersteZahl)
                {
                    x2 = v;
                }
            }

            //Zweite Variable oder Zahl bekommen
            Variable x3 = new("NULL", 0);
            string zweiteZahl = match.Groups[4].Value;
            //int x2 = 0;
            foreach (Variable v in Variablen)
            {
                if (v.Name == zweiteZahl)
                {
                    x3 = v;
                }
            }
            // Wenn es kein +|-|* und x3 gibt und x2 nicht ersetzt wurde => Direktzuweisung
            if (match.Groups[3].Value == "" && match.Groups[4].Value == "" && x2.Name == "NULL")
            {
                Variablen[x1count].Value = Convert.ToInt32(ersteZahl);
            }
            else if (match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name != "NULL" && x3.Name != "NULL") //Doppelzuweisung
            {
                // Operator ermitteln
                string operatorType = match.Groups[3].Value;

                switch (operatorType)
                {
                    case "+":
                        x1.Value = x2.Value + x3.Value;
                        break;
                    case "-":
                        x1.Value = x2.Value - x3.Value;
                        break;
                    case "*":
                        x1.Value = x2.Value * x3.Value;
                        break;
                }
                Variablen[x1count] = x1;
            }
            else if (match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name != "NULL" && x3.Name == "NULL") // Neuzuweisung => x2 +|*|- int
            {
                string operatorType = match.Groups[3].Value;
                int operand = Convert.ToInt32(match.Groups[4].Value);
                switch (operatorType)
                {
                    case "+":
                        x1.Value = x2.Value + operand;
                        break;
                    case "-":
                        x1.Value = x2.Value - operand;
                        break;
                    case "*":
                        x1.Value = x2.Value * operand;
                        break;
                }
                Variablen[x1count] = x1;
            }
            else if (match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name == "NULL" && x3.Name != "NULL") // Neuzuweisung => int +|*|- x3
            {
                string operatorType = match.Groups[3].Value;
                int operand = Convert.ToInt32(match.Groups[2].Value);
                switch (operatorType)
                {
                    case "+":
                        x1.Value = x3.Value + operand;
                        break;
                    case "-":
                        x1.Value = x3.Value - operand;
                        break;
                    case "*":
                        x1.Value = x3.Value * operand;
                        break;
                }
            }
            else if (match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name == "NULL" && x3.Name == "NULL") // Zwei-Konstanten => c +|*|- c
            {
                string operatorType = match.Groups[3].Value;
                int operand1 = Convert.ToInt32(match.Groups[2].Value);
                int operand2 = Convert.ToInt32(match.Groups[4].Value);
                switch (operatorType)
                {
                    case "+":
                        x1.Value = operand1 + operand2;
                        break;
                    case "-":
                        x1.Value = operand1 - operand2;
                        break;
                    case "*":
                        x1.Value = operand1 * operand2;
                        break;
                }
            }

            // Zahlen kleiner Null werden auf null gesetzt
            if (x1.Value < 0) x1.Value = 0;

            if (debug || schrittweiseAusgabe) //Ausgabe der hier berechneten Variable
            {
                try
                {
                    IAsyncResult asyncResult = TerminalTB.BeginInvoke((MethodInvoker)delegate
                    {
                        TerminalTB.Text = Variablen[x1count].Name + " => " + Variablen[x1count].Value + "\n" + TerminalTB.Text;
                    });
                    TerminalTB.EndInvoke(asyncResult);
                }
                catch (ObjectDisposedException) { }
            }
        }

        bool CheckWhileStructure(string input)
        {
            string mulOperator = "";
            if (MultipErlaubenTS.Checked == true)
                mulOperator = "|*";

            // Regulären Ausdruck an den Syntax anpassen
            Regex assignmentRegex = new(@"^\s*[a-zA-Z_][a-zA-Z_0-9]*\s+:=\s+([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)\s*([+" + mulOperator + @"|-])?\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)?(;)?\s*$");
            //Gr[1] = x, Gr[2] = :=, Gr[3] = y, Gr[4] = +-*, Gr[5] = z, Gr[6] = ;   nicht richtig?
            Regex whileStartRegex = new(@"^\s*WHILE\s+[a-zA-Z_][a-zA-Z_0-9]*\s+!=\s+0\s+DO\s*$");//\s(.+)\sEND$");
            Regex END = new(@"^\s*END(;)?\s*$");
            Regex space = new(@"^\s*$");
            // Split the input into separate instructions
            string[] instructions = input.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            int Starts = 0;
            Structurecount = 0;
            bool nextMustBeEND = false;

            //Durchgehen der Zeilen und prüfen des Syntax
            foreach (string instruction in instructions)
            {
                Structurecount++;
                if (whileStartRegex.IsMatch(instruction))
                {
                    if (nextMustBeEND == true)
                    {
                        Structurecount--;
                        errorReason = "Semikolon vergessen.";
                        return false;
                    }
                    Starts++;
                    //MessageBox.Show(count.ToString() + " WHILE");
                }
                else if (assignmentRegex.IsMatch(instruction))
                {
                    if (nextMustBeEND == true)
                    {
                        Structurecount--;
                        errorReason = "Semikolon vergessen.";
                        return false;
                    }

                    Match semikolon = assignmentRegex.Match(instruction);
                    if (semikolon.Groups[4].Value.ToString() != ";")
                    {
                        nextMustBeEND = true;
                    }

                    Match match = assignmentRegex.Match(instruction);
                    // => Direktzuweisung
                    if (match.Groups[1].Value != "" && match.Groups[2].Value == "" && match.Groups[3].Value == "")
                    {
                        if (DirektAssignmentTS.Checked == false)
                        {
                            errorReason = "Direktzuweisung muss erlaubt sein";
                            return false;
                        }
                    }
                    else if (match.Groups[1].Value != "" && match.Groups[2].Value != "" && match.Groups[3].Value != "" && int.TryParse(match.Groups[1].Value, out int output) == false && int.TryParse(match.Groups[3].Value, out int output2) == false) //Doppelzuweisung
                    {
                        if (DoubleAssignmentTS.Checked == false)
                        {
                            errorReason = "Doppelzuweisung muss erlaubt sein";
                            return false;
                        }
                    }
                    else if (1 == 0 /*match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name != "NULL" && x3.Name == "NULL"*/) // Neuzuweisung => x2 +|*|- int
                    {

                    }
                    else if (1 == 0 /* match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name == "NULL" && x3.Name != "NULL"*/) // Neuzuweisung => int +|*|- x3
                    {

                    }
                    else if (1 == 0 /*match.Groups[3].Value != "" && match.Groups[4].Value != "" && x2.Name == "NULL" && x3.Name == "NULL"*/) // Zwei-Konstanten => c +|*|- c
                    {

                    }
                }
                else if (END.IsMatch(instruction))
                {
                    nextMustBeEND = false;
                    Match match = END.Match(instruction);
                    if (match.Groups[1].Value != ";")
                    {
                        nextMustBeEND = true;
                    }
                    Starts--;

                    //MessageBox.Show(count.ToString() + " END");
                }
                else if (space.IsMatch(instruction))
                {
                    //MessageBox.Show(count.ToString() + " space");
                }
                else // Versuch eine Fehleranalyse
                {
                    Regex containsInsensitiveWHILE = new(@"^\s*(WHILE)(\s*)([a-zA-Z_0-9]+)(\s*)(!?=?)(\s*)0(\s*)(DO).*", RegexOptions.IgnoreCase);
                    Regex containsInsensitiveEND = new(@"(\s*)?END(\s*).?", RegexOptions.IgnoreCase);
                    Regex InsensitiveAssignment = new(@"^\s*[a-zA-Z_][a-zA-Z_0-9]*(\s*)(:?=?)(\s*)([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)\s*([-|+|*])\s*([a-zA-Z_][a-zA-Z_0-9]*|[0-9]+)?(;)?\s*$");
                    Regex containsSensitiveWHILE = new(@"\bWHILE\b");
                    Regex containsSensitiveDO = new(@"\bDO\b");

                    if (containsInsensitiveWHILE.IsMatch(instruction))
                    {
                        Match match = containsInsensitiveWHILE.Match(instruction);
                        if (containsSensitiveWHILE.IsMatch(instruction) == false)
                        {
                            errorReason = "Bitte schreiben Sie WHILE groß!";
                        }
                        else if (containsSensitiveDO.IsMatch(instruction) == false && containsSensitiveDO.IsMatch(instruction).ToString() != "0")
                        {
                            errorReason = "Bitte schreiben Sie DO groß!";
                        }
                        else if (match.Groups[2].Length + match.Groups[4].Length + match.Groups[6].Length + match.Groups[7].Length < 4)
                        {
                            errorReason = "Bitte setzten Sie mindestens ein Leerzeichen zwischen \"WHILE\" -> der Variable -> \"!=\" -> \"0\" -> \"DO\"!";
                        }
                        else if (match.Groups[5].ToString() != "!=")
                        {
                            errorReason = "Bitte schreiben Sie != anstatt \"" + match.Groups[5].ToString() + "\".";
                        }
                    }
                    else if (containsInsensitiveEND.IsMatch(instruction))
                    {
                        errorReason = "Bitte schreiben Sie END groß!";
                    }
                    else
                    {
                        Match match = InsensitiveAssignment.Match(instruction);
                        if (match.Groups[1].Length + match.Groups[3].Length < 2)
                        {
                            errorReason = "Bitte machen Sie jeweils vor und nach dem \":=\" ein Leerzeichen";
                        }
                        else if (match.Groups[5].ToString() == "*")
                        {
                            errorReason = "Bitte überprüfen Sie ob die Multiplikation erlaubt ist.";
                        }
                        else if (match.Groups[7].ToString() != ";")
                        {
                            errorReason = "Bitte schreiben ein Semikolon (;) hinter eine Zuweisung.";
                        }
                    }
                    return false;
                }
            }
            //Wenn WHILE - END = 0 => true //if  WHILE - END = 0 => true
            if (Starts != 0) return false;
            else return true;
        }

        static List<Variable> ExtractVariables(string input)
        {
            // Regulärer Ausdruck der zu Variablen passt
            Regex variableRegex = new(@"\b[a-zA-Z_][a-zA-Z_0-9]*\b");

            // Variablen finden
            MatchCollection matches = variableRegex.Matches(input);

            List<string> variablen = new();

            foreach (Match match in matches.Cast<Match>())
            {
                variablen.Add(match.Value);
            }

            //Keywörter zusortieren
            List<string> keywords = new() { "WHILE", "END", "DO", "while", "end", "do", "While", "End", "Do" };

            variablen.RemoveAll(x => keywords.Contains(x, StringComparer.OrdinalIgnoreCase));
            variablen = variablen.Distinct().ToList();

            List<Variable> variablenobj = new();
            foreach (string s in variablen)
            {
                variablenobj.Add(new Variable(s, 0));
            }

            return variablenobj;
        }

        class Variable
        {
            public string Name { get; set; }
            public int Value { get; set; }
            public Variable(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            //TerminalTB.Text = "--help";
            EingabeTb.Focus();
        }

        public class StartEnd
        {
            public static (int start, int end) GetLineStartEnd(string text, int lineNumber)
            {
                int start = 0;
                int end = 0;
                int currentLine = 0;
                if (lineNumber == 0)
                {
                    string[] s = text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
                    end = s[0].Length;
                    return (0, end);
                }
                for (int i = 0; i < text.Length; i++) //Text pro Buchstable durchgehen
                {
                    if (text[i] == '\n')// \n ist neu Zeile
                    {
                        if (currentLine == lineNumber)
                        {
                            end = i;
                            break;
                        }
                        else if (currentLine == lineNumber - 1)
                        {
                            start = i + 1;
                        }
                        currentLine++;
                    }
                }
                if (currentLine < lineNumber)
                    end = text.Length;

                return (start, end);
            }
        }

        private void TerminalTB_KeyUp_1(object sender, KeyEventArgs e)
        {
            //Prefix überprüfen
            Regex prefix = new(@"^--([a-zA-Z_][a-zA-Z_0-9?]*)\s*$");

            if (e.KeyCode == Keys.Enter && prefix.IsMatch(TerminalTB.Lines[0]))
            {
                Match match = prefix.Match(TerminalTB.Lines[0]);
                string command = match.Groups[1].Value;

                if (command == "help")
                {
                    TerminalTB.Text = "--help \t\t=> Zeigt diese Übersicht an\r\n--syntax \t=> Zeige erlaubte Eingaben an\r\n--bsp? \t=> Zeigt Beispielmöglichleiten an\n--debug \t=> Schaltet den Debug-Modus an/aus\n--version \t=> Über/Version\n*Klick auf \"Terminal:\" erzeugt eine leere Zeile\n*STRG + Mausrad vergrößert/verkleinert den Text";
                }
                else if (command == "syntax")
                {
                    TerminalTB.Text = "Ein WHILE-Programm P besteht aus den Symbolen WHILE, DO, END, :=, +, -, ;, !=, einer Anzahl Variablen x, y,... sowie beliebigen Konstanten c." + "\r\n" + "Erlaubt ist: \"x := y +/- c;\", \"x := c;\", \"x := y + z;\"" + "\r\n" + "WHILE x != 0 DO" + "\r\n" + "   P;" + "\r\n" + "   ..." + "\r\n" + "END";
                }
                else if (command == "dark")
                {
                    this.BackColor = Color.FromArgb(47, 49, 54);

                    TerminalTB.BackColor = Color.FromArgb(64, 68, 75);
                    TerminalTB.BorderStyle = BorderStyle.FixedSingle;
                    TerminalTB.ForeColor = Color.White;
                    TerminalLbl.ForeColor = Color.White;

                    EingabeTb.BackColor = Color.FromArgb(64, 68, 75);
                    EingabeTb.BorderStyle = BorderStyle.FixedSingle;
                    EingabeTb.ForeColor = Color.White;
                    EingabeLbl.ForeColor = Color.White;

                    Pruefen_btn.BackColor = Color.FromArgb(220, 221, 222);

                    goto clear;
                }
                else if (command == "bsp?")
                {
                    TerminalTB.Text = "bsp1: Addition\r\nbsp2: Multiplikation";
                }
                else if (command == "bsp1")
                {
                    EingabeTb.Text = "x1 := x1 + 5;\r\nx2 := x2 + 10;\r\nWHILE x1 != 0 DO\r\n\tx2 := x2 + 1;\r\n\tx1 := x1 - 1;\r\nEND";
                    goto clear;
                }
                else if (command == "bsp2")
                {
                    EingabeTb.Text = "x1 := x1 + 5;\r\nx2 := x2 + 3;\r\nx3 := x1 + 0;\r\nWHILE x2 != 0 DO\r\n\tWHILE x1 != 0 DO\r\n\t\tx0 := x0 + 1;\r\n\t\tx1 := x1 - 1;\r\n\tEND\r\n\tx1 := x3 + 0;\r\n\tx2 := x2 - 1;\r\nEND";
                    goto clear;
                }
                else if (command == "debug")
                {
                    if (debug)
                    {
                        debug = false;
                        TerminalTB.Text = "\nDebug off";
                    }
                    else
                    {
                        debug = true;
                        MsPerExeCb.Text = "1500";
                        TerminalTB.Text = "\nDebug on";
                    }
                }
                else if (command == "v" || command == "version")
                {
                    string v = Assembly.GetEntryAssembly().GetName().Version.ToString();
                    TerminalTB.Text = "v" + v + " ,27.01.2023, Jonas Hülse\r\n" + TerminalTB.Text;
                }
                else
                {
                    TerminalTB.Text = "\"" + command + "\"" + "ist ein unbekannter Befehl!" + TerminalTB.Text;
                    TerminalTB.Text = Environment.NewLine + TerminalTB.Text;
                }
                return;

            clear:
                {
                    TerminalTB.Text = "";
                }
            }
        }


        void MarkLineGreen(string originalText)
        {
            try
            {
                int lineNumber = zeiger;
                if (lastLineNumber != -1)
                {
                    StartEnd instance = new();
                    var (start, end) = StartEnd.GetLineStartEnd(originalText, lastLineNumber);
                    int length = end - start;
                    if (length < 0) length = 0;
                    IAsyncResult asyncResult = EingabeTb.BeginInvoke((MethodInvoker)delegate
                    {
                        EingabeTb.SelectionStart = start;
                        EingabeTb.SelectionLength = length;
                        EingabeTb.SelectionColor = Color.Black;
                    });
                    EingabeTb.EndInvoke(asyncResult);

                    (start, end) = StartEnd.GetLineStartEnd(originalText, lineNumber);
                    length = end - start;
                    if (length < 0) length = 0;
                    IAsyncResult asyncResult2 = EingabeTb.BeginInvoke((MethodInvoker)delegate
                    {
                        EingabeTb.SelectionStart = start;
                        EingabeTb.SelectionLength = length;
                        EingabeTb.SelectionColor = Color.Green;
                    });
                    EingabeTb.EndInvoke(asyncResult);
                }
                else
                {
                    StartEnd instance = new();
                    var (start, end) = StartEnd.GetLineStartEnd(originalText, lineNumber);
                    int length = end - start;
                    if (length < 0) length = 0;
                    IAsyncResult asyncResult = EingabeTb.BeginInvoke((MethodInvoker)delegate
                    {
                        EingabeTb.SelectionStart = start;
                        EingabeTb.SelectionLength = length;
                        EingabeTb.SelectionColor = Color.Green;
                    });
                    EingabeTb.EndInvoke(asyncResult);
                }
                lastLineNumber = lineNumber;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Line: 631: " + ex);
            }
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            stop = true;
            Thread.Sleep(30); //Zeit zum Abbrechen des Threads
            //stop = false;
            zeiger = 0;
            logcount = 0;
            Structurecount = 0;
            errorReason = "";
            lastLineNumber = -1;
            Variablen.Clear();
            InProgress = false;
            //InProgress = false;
            //stop = false; führt zu absturtz
        }

        private void CheckCancleThread()
        {
            if (StepByStep)
            {
                stop = true;
                Thread.Sleep(50);
                InProgress = false;
            }
            StepByStep = false;
        }

        private void TerminalLbl_MouseUp(object sender, MouseEventArgs e)
        {
            TerminalTB.Text = "\n" + TerminalTB.Text;
        }

        private void BspMultipTS_Click(object sender, EventArgs e)
        {
            CheckCancleThread();
            EingabeTb.Text = "x1 := x1 + 5;\r\nx2 := x2 + 3;\r\nx3 := x1 + 0;\r\nWHILE x2 != 0 DO\r\n\tWHILE x1 != 0 DO\r\n\t\tx0 := x0 + 1;\r\n\t\tx1 := x1 - 1;\r\n\tEND;\r\n\tx1 := x3 + 0;\r\n\tx2 := x2 - 1;\r\nEND";
            TerminalTB.Text = "";
        }

        private void BspAdditionTS_Click(object sender, EventArgs e)
        {
            CheckCancleThread();
            EingabeTb.Text = "x1 := x1 + 5;\r\nx2 := x2 + 10;\r\nWHILE x1 != 0 DO\r\n\tx2 := x2 + 1;\r\n\tx1 := x1 - 1;\r\nEND";
            TerminalTB.Text = "";
        }

        private void FakultätTS_Click(object sender, EventArgs e)
        {
            CheckCancleThread();
            EingabeTb.Text = "x1 := x1 + 5;\r\nx0 := x0 + 1;\r\nWHILE x1 != 0 DO\r\n\tx0 := x0 * x1;\r\n\tx1 := x1 - 1;\r\nEND";
            MultipErlaubenTS.Checked = true;
            DoubleAssignmentTS.Checked = true;
            MultipErlaubenTS.Text = "Multiplikation verbieten";
            if (MultipErlaubenTS.Checked == true)
            {
                TerminalTB.Text = "Multiplikation und Doppelzuweisungen sind nun erlaubt! Kann in den Einstellungen wieder ausgeschaltet werden.";
            }
        }

        private void ExpoTS_Click(object sender, EventArgs e)
        {
            CheckCancleThread();
            EingabeTb.Text = "x1 := x1 + 4;\r\nx2 := x2 + 2;\r\nx0 := x0 + 1;\r\nWHILE x2 != 0 DO\r\n\tx0 := x0 * x1;\r\n\tx2 := x2 - 1;\r\nEND";
            MultipErlaubenTS.Checked = true;
            DoubleAssignmentTS.Checked = true;
            if (MultipErlaubenTS.Checked == true)
            {
                TerminalTB.Text = "Multiplikation und Doppelzuweisungen sind nun erlaubt!\nZum Ausschalten erneut auf \"Multiplikation erlauben\" klicken.";
            }
            else { TerminalTB.Text = "Multiplikation ist nun nicht mehr erlaubt!"; }
            TerminalTB.Text = "Hinweis: \"x1\" steht für die Basis, \"x2\" steht für den Exponent.\r\n" + TerminalTB.Text;
        }

        private void DivTS_Click(object sender, EventArgs e)
        {
            CheckCancleThread();
            EingabeTb.Text = "x1 := x1 + 13;\r\nx2 := x1 + 0;\r\nWHILE x2 != 0 DO\r\n\tx3 := x3 + 1;\r\n\tx2 := x2 - 2;\r\nEND";
        }

        private void SyntaxTS_Click(object sender, EventArgs e)
        {
            SyntaxForm Sf = new();
            Sf.ShowDialog();
        }

        private void MultipErlaubenTS_Click(object sender, EventArgs e)
        {
            if (MultipErlaubenTS.Checked == true)
            {
                MultipErlaubenTS.Text = "Multiplikation verbieten";
                //TerminalTB.Text = "Multiplikation ist nun erlaubt!\nZum Ausschalten erneut auf \"Multiplikation erlauben\" klicken.";
            }
            else { MultipErlaubenTS.Text = "Multiplikation erlauben"; } //TerminalTB.Text = "Multiplikation ist nun nicht mehr erlaubt!";
        }

        private void InfoTS_Click(object sender, EventArgs e)
        {
            InfoForm Info = new();
            Info.ShowDialog();
        }

        private void BeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ErweiterteAusgabeTS_Click(object sender, EventArgs e)
        {
            if (debug)
            {
                debug = false;
                ErweiterteAusgabeTS.Checked = false;
            }
            else
            {
                debug = true;
                //MsPerExeCb.Text = "1500";
                ErweiterteAusgabeTS.Checked = true;
            }
        }

        string pathToFile = @"C:\";
        private void OpenFileTS_Click(object sender, EventArgs e)
        {
            OpenFileDialog theDialog = new OpenFileDialog();
            theDialog.Title = "Open Text File";
            theDialog.Filter = "TXT files|*.txt";
            theDialog.RestoreDirectory = true;
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pathToFile = theDialog.FileName;
            }
            else return;

            if (File.Exists(pathToFile))
            {
                using StreamReader sr = new(pathToFile);
                EingabeTb.Text = sr.ReadToEnd();
                TerminalTB.Text = "";
            }
        }

        private void FileSaveTS_Click(object sender, EventArgs e)
        {
            SaveFileDialog theDialog = new()
            {
                Title = "Open Text File",
                Filter = "TXT files|*.txt",
                RestoreDirectory = true
            };
            if (theDialog.ShowDialog() == DialogResult.OK)
            {
                pathToFile = theDialog.FileName;
            }
            else return;

            using StreamWriter sw = File.CreateText(pathToFile);
            sw.WriteLine(EingabeTb.Text);
            sw.Close();
            TerminalTB.Text = "Datei gespeichert.";
        }

        private void ExeTS_Click(object sender, EventArgs e)
        {
            Pruefen_btn_Click(sender, e);
        }

        private void MultipErlaubenTS_MouseEnter(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = false;
        }

        private void MultipErlaubenTS_MouseLeave(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = true;
        }

        private void ErweiterteAusgabeTS_MouseEnter(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = false;
        }

        private void ErweiterteAusgabeTS_MouseLeave(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = true;
        }

        private void ZoomInTS_Click(object sender, EventArgs e)
        {
            int fontSize = GetFontSize();
            if (fontSize <= 10000)
            {
                EingabeTb.Font = new Font("Consolas", fontSize + 1, FontStyle.Regular);
            }
        }

        private void ZoomOutTS_Click(object sender, EventArgs e)
        {
            int fontSize = GetFontSize();
            if (fontSize >= 2)
            {
                EingabeTb.Font = new Font("Consolas", fontSize - 1, FontStyle.Regular);
            }
        }

        private void ClearTS_Click(object sender, EventArgs e)
        {
            EingabeTb.Clear();
        }

        int GetFontSize()
        {
            //MessageBox.Show(EingabeTb.Font.ToString());
            Regex FontSizeInfo = new(@".*Font:\s*(Name=[a-zA-Z_][a-zA-Z_0-9\s]*,*\s*)?Size=([0-9]+,?.?[0-9]*)(,*\s*Units=[0-9]+,* GdiCharSet=[0-9]+,* GdiVerticalFont=[a-zA-Z]+)?.*");
            Match m = FontSizeInfo.Match(EingabeTb.Font.ToString());
            int fontSize = Convert.ToInt32(Math.Round(Convert.ToDouble(m.Groups[2].Value), 0));

            return fontSize;
        }
        int GetFontSize2()
        {
            //MessageBox.Show(EingabeTb.Font.ToString());
            Regex FontSizeInfo = new(@".*Font:\s*(Name=[a-zA-Z_][a-zA-Z_0-9\s]*,*\s*)?Size=([0-9]+,?.?[0-9]*)(,*\s*Units=[0-9]+,* GdiCharSet=[0-9]+,* GdiVerticalFont=[a-zA-Z]+)?.*");
            Match m = FontSizeInfo.Match(TerminalTB.Font.ToString());
            int fontSize = Convert.ToInt32(Math.Round(Convert.ToDouble(m.Groups[2].Value), 0));

            return fontSize;
        }

        private void ZoomOutTS2_Click(object sender, EventArgs e)
        {
            int fontSize = GetFontSize2();
            if (fontSize >= 2)
            {
                TerminalTB.Font = new Font("Consolas", fontSize - 1, FontStyle.Regular);
            }
        }

        private void ZoomInTS2_Click(object sender, EventArgs e)
        {
            int fontSize = GetFontSize2();
            if (fontSize <= 10000)
            {
                TerminalTB.Font = new Font("Consolas", fontSize + 1, FontStyle.Regular);
            }
        }

        private void ClearTS2_Click(object sender, EventArgs e)
        {
            TerminalTB.Clear();
        }

        private void MsPerExeCb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up)
            {
                int value = int.Parse(MsPerExeCb.Text);
                MsPerExeCb.Text = (value + 100).ToString();
            }
            else if (e.KeyCode == Keys.Down)
            {
                int value = int.Parse(MsPerExeCb.Text);
                MsPerExeCb.Text = (value - 100).ToString();
            }
        }

        private void MsPerExeCb_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(MsPerExeCb.Text, "[^0-9]"))
            {
                MessageBox.Show("Bitte nur ganze Zahlen eingeben.");
                MsPerExeCb.Text = "";
            }
        }

        private void MsPerExeCb_MouseHover(object sender, EventArgs e)
        {
            ToolTipMain.Show("Verwenden Sie die Pfeiltasten nach oben und unten,\r\num die Zahl zu erhöhen oder zu verringern.", MsPerExeCb);
        }

        private void DoubleAssignmentTS_Click(object sender, EventArgs e)
        {
            if (DoubleAssignmentTS.Checked == true)
            {
                DoubleAssignmentTS.Text = "Doppelzuweisung verbieten";
                //TerminalTB.Text = "Multiplikation ist nun erlaubt!\nZum Ausschalten erneut auf \"Multiplikation erlauben\" klicken.";
            }
            else { DoubleAssignmentTS.Text = "Doppelzuweisung erlauben"; }
        }

        private void DirektAssignmentTS_Click(object sender, EventArgs e)
        {
            if (DirektAssignmentTS.Checked == true)
            {
                DirektAssignmentTS.Text = "Direktzuweisung verbieten";
                //TerminalTB.Text = "Multiplikation ist nun erlaubt!\nZum Ausschalten erneut auf \"Multiplikation erlauben\" klicken.";
            }
            else { DirektAssignmentTS.Text = "Direktzuweisung erlauben"; }
        }

        private void DirektAssignmentTS_MouseEnter(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = false;
        }

        private void DirektAssignmentTS_MouseLeave(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = true;
        }

        private void DoubleAssignmentTS_MouseEnter(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = false;
        }

        private void DoubleAssignmentTS_MouseLeave(object sender, EventArgs e)
        {
            EinstellungenTS.DropDown.AutoClose = true;
        }
    }
}