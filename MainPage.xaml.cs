//using Android.Icu.Text;

//using Android.Net.Wifi.Aware;

namespace Calculator;

public partial class MainPage : ContentPage
{
    double? Num1, Num2;
    string Operation;
    bool doneCalc = true;

    public MainPage()
    {
        InitializeComponent();
        CreateNumbers();
    }

    public void DelClicked(object sender, EventArgs e)
    {
        if (Num2 != null)
        {
            string st = Num2.ToString();
            if (st.Length <= 1)
                Num2 = null;
            else
                Num2 = double.Parse(st.Remove(st.Length - 1));
        }
        else if (Operation != null && Operation != "")
        {
            Operation = "";
        }
        else if (Num1 != null && !doneCalc)
        {
            string st = Num1.ToString();
            if (st.Length <= 1)
                Num1 = null;
            else
                Num1 = double.Parse(st.Remove(st.Length - 1));
        }

        UpdateText();
    }

    public void C_Clicked(object sender, EventArgs e)
    {
        if (Num2 != null)
            Num2 = null;

        else if (Operation != "" && Operation != null)
            Operation = "";

        else if (Num1 != null)
            Num1 = null;

        UpdateText();
    }

    public void CE_Clicked(object sender, EventArgs e)
    {
        Num1 = null;
        Num2 = null;
        Operation = "";
        UpdateText();
        doneCalc = true;
    }

    public void EqualClicked(object sender, EventArgs e)
    {
        double? CalcResult = Calc(Num1, Num2, Operation);
        if (CalcResult == null) return;

        Num1 = CalcResult;
        Operation = "";
        Num2 = null;
        UpdateText();

        doneCalc = true;
    }

    public void OperationClicked(object sender, EventArgs e)
    {
        if (Num1 == null || Num2 != null) return;

        Operation = (sender as Button).Text;
        UpdateText();

        doneCalc = false;
    }

    public void NumberClicked(object sender, EventArgs e)
    {
        double n = int.Parse((sender as Button).Text);
        if (Num1 == null || doneCalc) //אין כלום
        {
            Num1 = n;
        }
        else if(Operation == "" || Operation == null) //יש מספר אין פעולה
        {
            Num1 = Num1 * 10 + n;
        }
        else if(Num2 == null) //יש מספר יש פעולה אין מספר שני
        {
            Num2 = n;
        }
        else //יש הכל
        {
            Num2 = Num2 * 10 + n;
        }

        UpdateText();

        doneCalc = false;
    }

    public void CreateNumbers()
    {
        for (int i = 0; i < 9; i++)
        {
            Button b = new();
            b.Text = (i + 1).ToString();
            b.FontSize = 70;
            b.BackgroundColor = Color.Parse("Blue");
            b.Clicked += NumberClicked;

            pageGrid.Add(b, i % 3, i / 3 + 2);
        }
        Button b2 = new();
        b2.Text = "0";
        b2.FontSize = 70;
        b2.BackgroundColor = Color.Parse("Blue");
        b2.Clicked += NumberClicked;

        pageGrid.Add(b2, 1, 5);
    }
    public void UpdateText()
    {
        lbl.Text = $"{Num1} {Operation} {Num2}";
    }

    public static double? Calc(double? num1, double? num2, string operation)
    {
        if (num1 == null || num2 == null || operation == "" || operation == null) return num1;

        if (!IsOperation(operation)) return null;
        switch (operation)
        {
            case "+":
                return num1 + num2;
            case "-":
                return num1 - num2;
            case "*":
                return num1 * num2;
            case "/":
                if (num2 == 0) return null;
                return num1 / num2;
        }
        return null;
    }


    public static bool IsOperation(string st)
    {
        if (st == null || st == "") return false;
        return st == "+" || st == "-" || st == "*" || st == "/";
    }
}

