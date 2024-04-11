using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Projekt3
{
    public partial class mkCalculator : Form
    {
        public mkCalculator()
        {
            InitializeComponent();
            mktxtOperation.Text = "0";
        }

        enum mkOperation
        {
            Add,
            Deduct,
            Multiply,
            Divide,
            None
        }

        private string mkFirstValue = string.Empty;
        private string mkSecondValue = string.Empty;
        private mkOperation _mkoperation = mkOperation.None;

        private void mkbutton_Click(object sender, EventArgs e)
        {
            var mkClickNumber = (sender as Button)!.Text;

            if (mktxtOperation.Text == "0")
            {
                mktxtOperation.Text = "";

                if (mkClickNumber == ",")
                {
                    mktxtOperation.Text = "0";
                    mkbtnPoint.Enabled = false;
                }
            }
            else if (mkClickNumber == ",")
            {
                mkbtnPoint.Enabled = false;
            }

            mktxtOperation.Text += mkClickNumber;
            mkSetResultButtonState(true);

            if (_mkoperation != mkOperation.None)
                mkSecondValue += mkClickNumber;
            else
                mkSetOperationButtonState(true);
        }

        private void mkSetResultButtonState(bool value)
        {
            mkbtnEqual.Enabled = value;
        }

        private void mkSetOperationButtonState(bool value)
        {
            mkbtnAdd.Enabled = value;
            mkbtnMultiply.Enabled = value;
            mkbtnDivide.Enabled = value;
            mkbtnDeduct.Enabled = value;
        }

        private void mkbuttonOperation_Click(object sender, EventArgs e)
        {
            mkFirstValue = mktxtOperation.Text;

            var mkClickOperation = (sender as Button)!.Text;

            _mkoperation = mkClickOperation switch
            {
                "+" => mkOperation.Add,
                "-" => mkOperation.Deduct,
                "/" => mkOperation.Divide,
                "*" => mkOperation.Multiply,
                _ => mkOperation.None,
            };

            mkbtnPoint.Enabled = true;

            mktxtOperation.Text += $" {mkClickOperation} ";

            mkSetOperationButtonState(false);
            mkSetResultButtonState(false);
        }

        private void mkbuttonEqual_Click(object sender, EventArgs e)
        {
            if (_mkoperation == mkOperation.None)
                return;

            var mkFirstNumber = double.Parse(mkFirstValue);
            var mkSecondNumber = double.Parse(mkSecondValue);

            var mkResult = mkCalculate(mkFirstNumber, mkSecondNumber);

            mktxtOperation.Text = mkResult.ToString();
            mkSecondValue = string.Empty;
            _mkoperation = mkOperation.None;
            mkSetOperationButtonState(true);
            mkSetResultButtonState(true);
        }

        private double mkCalculate(double mkFirstNumber, double mkSecondNumber)
        {
            switch (_mkoperation)
            {
                case mkOperation.None:
                    return mkFirstNumber;
                case mkOperation.Add:
                    return mkFirstNumber + mkSecondNumber;
                case mkOperation.Deduct:
                    return mkFirstNumber - mkSecondNumber;
                case mkOperation.Divide:
                    if (mkSecondNumber == 0)
                    {
                        MessageBox.Show("Nie mo¿na dzieliæ przez 0!", "B³¹d dzia³ania", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return 0;
                    }
                    return mkFirstNumber / mkSecondNumber;
                case mkOperation.Multiply:
                    return mkFirstNumber * mkSecondNumber;
            }

            return 0;
        }

        private void mkbuttonC_Click(object sender, EventArgs e)
        {
            mktxtOperation.Clear();
            mktxtOperation.Text = "0";
            mkbtnPoint.Enabled = true;
            mkSetOperationButtonState(true);
            mkSetResultButtonState(true);
        }

        private void mkbuttonCE_Click(object sender, EventArgs e)
        {
            if (mktxtOperation.Text.Length > 0)
            {
                mktxtOperation.Text = mktxtOperation.Text.Remove(mktxtOperation.Text.Length - 1, 1);
            }
            if (mktxtOperation.Text == "")
            {
                mktxtOperation.Text = "0";
            }
        }

        private void mkbuttonNWW_Click(object sender, EventArgs e)
        {
            var mkOperation = (sender as Button)!.Text;

            string mkFirstNumber = Microsoft.VisualBasic.Interaction.InputBox("Podaj pierwsz¹ liczbê do policzenia NWW:", "Kalkulator NWW");
            string mkSecondNumber = Microsoft.VisualBasic.Interaction.InputBox("Podaj drug¹ liczbê do policzenia NWW:", "Kalkulator NWW");

            double mka, mkb;

            if (double.TryParse(mkFirstNumber, out mka) && mka > 0 && double.TryParse(mkSecondNumber, out mkb) && mkb > 0)
            {
                if (mkOperation == "NWW")
                {
                    double mkNWW = mkCalc_NWW(mka, mkb);
                    mktxtOperation.Text = "NWW: " + mkNWW;
                }
            }
        }

        private void mkbuttonNWD_Click(object sender, EventArgs e)
        {
            var mkOperation = (sender as Button)!.Text;

            string mkFirstNumber = Microsoft.VisualBasic.Interaction.InputBox("Podaj pierwsz¹ liczbê do policzenia NWD:", "Kalkulator NWD");
            string mkSecondNumber = Microsoft.VisualBasic.Interaction.InputBox("Podaj drug¹ liczbê do policzenia NWD:", "Kalkulator NWD");

            double mka, mkb;

            if (double.TryParse(mkFirstNumber, out mka) && mka > 0 && double.TryParse(mkSecondNumber, out mkb) && mkb > 0)
            {
                if (mkOperation == "NWD")
                {
                    double mkNWD = mkCalc_NWD(mka, mkb);
                    mktxtOperation.Text = "NWD: " + mkNWD;
                }
            }
        }

        private double mkCalc_NWW(double mka, double mkb)
        {
            return mka * mkb / mkCalc_NWD(mka, mkb);
        }

        private double mkCalc_NWD(double mka, double mkb)
        {
            while (mka != mkb)
            {
                if (mka > mkb)
                    mka = mka - mkb;
                else
                    mkb = mkb - mka;
            }
            return mka;
        }
    }
}