//using Android.Icu.Text;

namespace Calculator;

public partial class MainPage : ContentPage
{
    double? Num1, Num2;
    string Operation;

    public MainPage()
    {
        InitializeComponent();
        CreateNumbers();
    }

    public void CEClicked(object sender, EventArgs e)
    {
        Num1 = null;
        Num2 = null;
        Operation = "";
        lbl.Text = "";
    }

    public void EqualClicked(object sender, EventArgs e)
    {
        double CalcResult = Calc(Num1, Num2, Operation);
        Num1 = CalcResult;
        Operation = "";
        Num2 = null;
        lbl.Text = CalcResult.ToString();
    }

    public void OperationClicked(object sender, EventArgs e)
    {
        Operation = (sender as Button).Text;
        lbl.Text = "";
    }

    public void NumberClicked(object sender, EventArgs e)
    {
        double n = int.Parse((sender as Button).Text);
        if (Num1 == null) //אין כלום
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
        else // יש הכל
        {
            Num2 = Num2 * 10 + n;
        }
        lbl.Text += n.ToString();
    }

    public void CreateNumbers()
    {
        for (int i = 0; i < 9; i++)
        {
            Button b = new();
            b.Text = (i + 1).ToString();
            b.FontSize = 100;
            b.BackgroundColor = Color.Parse("Blue");
            b.Clicked += NumberClicked;

            pageGrid.Add(b, i % 3, i / 3 + 2);
        }
        Button b2 = new();
        b2.Text = "0";
        b2.FontSize = 80;
        b2.BackgroundColor = Color.Parse("Blue");
        b2.Clicked += NumberClicked;

        pageGrid.Add(b2, 1, 5);
    }

    public static double Calc(double? a, double? b, string operation)
    {
        if (a == null || b == null) return 0;
        double a1 = (double)a;
        double b1 = (double)b;
        if (!IsOperation(operation)) return 0;
        switch (operation)
        {
            case "+":
                return a1 + b1;
            case "-":
                return a1 - b1;
            case "*":
                return a1 * b1;
            case "/":
                if (b == 0) return 0;
                return a1 / b1;
                
            default:
                return 0;
        }
    }

    public static bool IsOperation(string st)
    {
        if (st == null || st == "") return false;
        return st == "+" || st == "-" || st == "*" || st == "/";
    }
}

