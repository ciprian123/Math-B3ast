using System;
using System.Drawing;
using System.Windows.Forms;

namespace Math_Beast_Desktop
{
    public partial class MainWindow : MetroFramework.Forms.MetroForm
    {
        private long x;
        private string s;

        conversie_lungimi cl = new conversie_lungimi();
        conversie_temperaturi ct = new conversie_temperaturi();
        conversie_mase cm = new conversie_mase();
        conversie_arii ca = new conversie_arii();
        conversie_volume cv = new conversie_volume();
        conversie_timp ctmp = new conversie_timp();

        public MainWindow()
        {
            InitializeComponent();
            cl.Init();
            cm.Init();
            ca.Init();
            cv.Init();
            ctmp.Init();
            copy_button.Visible = false;

            combobox_L_din.SelectedIndex = combobox_L_in.SelectedIndex = 0;
            combobox_M_din.SelectedIndex = combobox_M_in.SelectedIndex = 0;
            combobox_T_din.SelectedIndex = combobox_T_in.SelectedIndex = 0;
            combobox_A_din.SelectedIndex = combobox_A_in.SelectedIndex = 0;
            combobox_V_din.SelectedIndex = combobox_V_in.SelectedIndex = 0;
            combobox_TIMP_din.SelectedIndex = combobox_TIMP_in.SelectedIndex = 0;
            OnEnglish();
        }

        private bool ExtraValid(string s)
        {
            if (s.Length < 2) return true;
            if (s[0] == '0' && Char.IsDigit(s[1]) || s[0] == '.') return false;
            return true;
        }

        private bool InputBin(string s)
        {
            if (s.Length > 1 && s.Length != '0' || s.Length == 1)
            {
                for (uint i = 0; i < s.Length; ++i)
                    if (i != '0' && i != '1') return false;
                return true;
            }
            return false;
        }

        private bool InputDec(string s)
        {
            if (s.Length > 1 && s[0] != '0' || s.Length == 1)
            {
                for (uint i = 0; i < s.Length; ++i)
                    if (i < '0' || i > '9') return false;
                return true;
            }
            return false;
        }

        private bool InputOct(string s)
        {
            if (s.Length > 1 && s[0] != '0' || s.Length == 1)
            {
                for (uint i = 0; i < s.Length; ++i)
                    if (i < '0' && i > '7') return false;
                return true;
            }
            return false;
        }

        private bool InputHex(string s)
        {
            if (s.Length > 1 && s[0] != '0' || s.Length == 1)
            {
                for (int i = 0; i < s.Length; ++i)
                    if (Char.IsDigit(s[i]) && (s[i] < '0' || s[i] > '9')) return false;
                    else if (Char.IsLetter(s[i]) && (s[i] < 'A' || s[i] > 'F')) return false;
                return true;
            }
            return false;
        }

        private void DisplayWarning()
        {
            value_conv.ForeColor = Color.Crimson;
            if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
            if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
            copy_button.Visible = false;
        }

        private void OnTextChanged_N()
        {
            if (String.IsNullOrEmpty(value_conv.Text))
            {
                solutionTextBox.Text = "";
                value_conv.ShowClearButton = false;
            }
            else
            {
                value_conv.ShowClearButton = true;
                s = value_conv.Text;

                // Transformam in functie de dorinta utilizatorului
                if (from_dec.Checked && to_dec.Checked || from_bin.Checked && to_bin.Checked || from_oct.Checked && to_oct.Checked || from_hex.Checked && to_hex.Checked)
                {
                    if (!from_hex.Checked && !to_hex.Checked)
                    {
                        if (from_bin.Checked && to_bin.Checked)
                        {
                            value_conv.ForeColor = Color.Black;
                            solutionTextBox.Text = s;
                            copy_button.Visible = true;
                        }
                        else if (long.TryParse(s, out x))
                        {
                            if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                            {
                                value_conv.ForeColor = Color.Black;
                                solutionTextBox.Text = s;
                                copy_button.Visible = true;
                            }
                            else
                                DisplayWarning();
                        }
                        else
                            DisplayWarning();
                    }
                    else if (InputHex(s))
                    {
                        value_conv.ForeColor = Color.Black;
                        solutionTextBox.Text = s;
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
                else if (from_dec.Checked && to_hex.Checked)
                {
                    copy_button.Visible = false;
                    if (long.TryParse(s, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            solutionTextBox.Text = Convert.ToInt64(value_conv.Text).ToString("X");
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_dec.Checked && to_oct.Checked)
                {
                    if (long.TryParse(s, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            solutionTextBox.Text = Convert.ToString(x, 8);
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_dec.Checked && to_bin.Checked)
                {
                    if (long.TryParse(value_conv.Text, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            solutionTextBox.Text = Convert.ToString(x, 2);
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_bin.Checked && to_hex.Checked)
                {
                    if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                    {
                        value_conv.ForeColor = Color.Black;
                        solutionTextBox.Text = Convert.ToInt64(s, 2).ToString("X");
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
                else if (from_bin.Checked && to_oct.Checked)
                {
                    if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                    {
                        value_conv.ForeColor = Color.Black;
                        x = Convert.ToInt64(value_conv.Text, 2);
                        solutionTextBox.Text = Convert.ToString(x, 8);
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
                else if (from_bin.Checked && to_dec.Checked)
                {
                    if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                    {
                        value_conv.ForeColor = Color.Black;
                        x = Convert.ToInt64(value_conv.Text, 2);
                        solutionTextBox.Text = Convert.ToString(x, 10);
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
                else if (from_oct.Checked && to_hex.Checked)
                {
                    if (Int64.TryParse(s, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            solutionTextBox.Text = Convert.ToInt64(s, 8).ToString("X");
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_oct.Checked && to_bin.Checked)
                {
                    if (Int64.TryParse(s, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            x = Convert.ToInt64(s, 8);
                            solutionTextBox.Text = Convert.ToString(x, 2);
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_oct.Checked && to_dec.Checked)
                {
                    if (Int64.TryParse(s, out x))
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            x = Convert.ToInt64(s, 8);
                            solutionTextBox.Text = Convert.ToString(x, 10);
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_hex.Checked && to_oct.Checked)
                {
                    if (s.Length <= 16)
                    {
                        if (s.Length > 1 && s[0] != '0' || s.Length == 1)
                        {
                            value_conv.ForeColor = Color.Black;
                            long x = Convert.ToInt64(s, 16);
                            solutionTextBox.Text = Convert.ToString(x, 8);
                            copy_button.Visible = true;
                        }
                        else DisplayWarning();
                    }
                    else DisplayWarning();
                }
                else if (from_hex.Checked && to_bin.Checked)
                {
                    if (s.Length <= 16)
                    {
                        value_conv.ForeColor = Color.Black;
                        long x = Convert.ToInt64(s, 16);
                        solutionTextBox.Text = Convert.ToString(x, 2);
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
                else if (from_hex.Checked && to_dec.Checked)
                {
                    if (s.Length <= 15)
                    {
                        value_conv.ForeColor = Color.Black;
                        long x = Convert.ToInt64(s, 16);
                        solutionTextBox.Text = Convert.ToString(x, 10);
                        copy_button.Visible = true;
                    }
                    else DisplayWarning();
                }
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_N();
        }

        private void from_hex_CheckedChanged(object sender, EventArgs e)
        {
            value_conv.Clear();
            OnTextChanged_N();
            value_conv.Focus();
        }

        private void to_dec_CheckedChanged(object sender, EventArgs e)
        {
            OnTextChanged_N();
            value_conv.Focus();
        }

        private void OnTabChanged()
        {
            int index = tabs_menu.SelectedIndex;

            solutionTextBox.Text = value_conv.Text = value_conv_l.Text = value_conv_m.Text = value_conv_t.Text = value_conv_a.Text = value_conv_v.Text = value_conv_time.Text = String.Empty;
            copy_button.Visible = false;

            if (index == 0) value_conv.Focus();
            else if (index == 1) value_conv_l.Focus();
            else if (index == 2) value_conv_t.Focus();
            else if (index == 3) value_conv_m.Focus();
            else if (index == 4) value_conv_a.Focus();
            else if (index == 5) value_conv_v.Focus();
            else if (index == 6) value_conv_time.Focus();
        }

        private void metroTabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTabChanged();
        }

        private void metroLabel2_Click(object sender, EventArgs e)
        {

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(solutionTextBox.Text);
        }

        private void metroLabel11_Click(object sender, EventArgs e)
        {
            Despre dsp = new Despre();
            dsp.ShowDialog();
        }

        private void metroLabel13_Click(object sender, EventArgs e)
        {
            Contact ct = new Contact();
            ct.ShowDialog();
        }

        private void value_conv_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            if (from_dec.Checked)
            {
                if (!Char.IsDigit(c) && c != '-' && c != 46 && c != 8 || c == '.') e.Handled = true;
            }
            else if (from_bin.Checked)
            {
                if (!Char.IsDigit(c) && c != 46 && c != 8 || c == '.' || Char.IsDigit(c) && c != '1' && c != '0') e.Handled = true;
            }
            else if (from_oct.Checked)
            {
                if (!Char.IsDigit(c) && c != 46 && c != 8 || c == '.' || Char.IsDigit(c) && (c < '0' || c > '7')) e.Handled = true;
            }
            else if (from_hex.Checked)
            {
                if (!Char.IsDigit(c) && c != 46 && c != 8 && c != 'A' && c != 'B' && c != 'C' && c != 'D' && c != 'E' && c != 'F' || c == '.') e.Handled = true;
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            tabs_menu.SelectedIndex = 0;
            value_conv.Focus();
        }

        private void OnTextChanged_L()
        {
            string s = value_conv_l.Text;
            if (String.IsNullOrEmpty(s))
            {
                solutionTextBox.Text = "";
                value_conv_l.ShowClearButton = false;
            }
            else
            {
                value_conv_l.ForeColor = Color.Black;
                value_conv_l.ShowClearButton = true;
                s = value_conv_l.Text;

                if (combobox_L_din.SelectedIndex != -1 && combobox_L_in.SelectedIndex != -1)
                {
                    double x;

                    if (Double.TryParse(s, out x) && ExtraValid(s))
                    {
                        if (Double.TryParse((cl.VAL[combobox_L_din.SelectedIndex + 1, combobox_L_in.SelectedIndex + 1] * Convert.ToDouble(value_conv_l.Text)).ToString(), out x))
                        {
                            solutionTextBox.Text = x.ToString();
                            copy_button.Visible = true;
                        }
                        else
                        {
                            copy_button.Visible = false;
                            value_conv_l.ForeColor = Color.Crimson;
                        }
                    }
                    else
                    {
                        value_conv_l.ForeColor = Color.Crimson;
                        copy_button.Visible = false;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        private void value_conv_l_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_L();
        }

        private void value_conv_l_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            bool point = false;
            s = value_conv_l.Text;

            if (String.IsNullOrEmpty(s))
                if (c == '.') e.Handled = true;

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.')
                {
                    e.Handled = true;
                    break;
                }

            if (c == '.' && point) e.Handled = true;
            if (!Char.IsDigit(c) && c != 8 && c != 46) e.Handled = true;
        }

        private void metroComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_L();
        }

        private void metroComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_L();
        }

        private void OnTextChanged_T()
        {
            string s = value_conv_t.Text;
            if (String.IsNullOrEmpty(s))
            {

                solutionTextBox.Text = "";
                value_conv.ShowClearButton = false;
            }
            else
            {
                value_conv_t.ForeColor = Color.Black;
                value_conv_t.ShowClearButton = true;

                s = value_conv_t.Text;

                int i1 = combobox_T_din.SelectedIndex;
                int i2 = combobox_T_in.SelectedIndex;
                value_conv_t.ForeColor = Color.Black;

                if (i1 != -1 && i2 != -1)
                {
                    double x;
                    if (Double.TryParse(s, out x) && ExtraValid(s))
                    {
                        copy_button.Visible = true;
                        if (i1 == i2) solutionTextBox.Text = s;
                        else if (i1 == 0 && i2 == 1) solutionTextBox.Text = ct.celsius_to_kelvin(x);
                        else if (i1 == 0 && i2 == 2) solutionTextBox.Text = ct.celsius_to_fahrenheit(x);
                        else if (i1 == 1 && i2 == 0) solutionTextBox.Text = ct.kelvin_to_celsius(x);
                        else if (i1 == 1 && i2 == 2) solutionTextBox.Text = ct.kelvin_to_fahrenheit(x);
                        else if (i1 == 2 && i2 == 0) solutionTextBox.Text = ct.fahrenheit_to_celsius(x);
                        else if (i1 == 2 && i2 == 1) solutionTextBox.Text = ct.fahrenheit_to_kelvin(x);
                    }
                    else
                    {
                        copy_button.Visible = false;
                        value_conv_t.ForeColor = Color.Crimson;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        private void value_conv_t_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_T();
        }

        private void metroComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_T();
        }

        private void metroComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_T();
        }

        private void value_conv_t_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;

            s = value_conv_t.Text;
            if (String.IsNullOrEmpty(s))
            {
                if (c == '.') e.Handled = true;
                if (c == '-') e.Handled = false;
            }

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.' || s[i] == '-' && c == '-')
                {
                    e.Handled = true;
                    break;
                }

            if (!Char.IsDigit(c) && c != 8 && c != 46 && c != '-') e.Handled = true;
        }

        private void OnTextChanged_M()
        {
            string s = value_conv_m.Text;
            if (String.IsNullOrEmpty(s))
            {

                solutionTextBox.Text = "";
                value_conv_m.ShowClearButton = false;
            }
            else
            {
                value_conv_m.ForeColor = Color.Black;
                value_conv_m.ShowClearButton = true;
                s = value_conv_m.Text;

                if (combobox_M_din.SelectedIndex != -1 && combobox_M_in.SelectedIndex != -1)
                {
                    double x;

                    if (Double.TryParse(value_conv_m.Text, out x) && ExtraValid(s))
                    {
                        if (Double.TryParse((cm.VAL[combobox_M_din.SelectedIndex + 1, combobox_M_in.SelectedIndex + 1] * Convert.ToDouble(value_conv_m.Text)).ToString(), out x))
                        {
                            solutionTextBox.Text = x.ToString();
                            copy_button.Visible = true;
                        }
                        else
                        {
                            copy_button.Visible = false;
                            value_conv_m.ForeColor = Color.Crimson;
                        }
                    }
                    else
                    {
                        value_conv_m.ForeColor = Color.Crimson;
                        copy_button.Visible = false;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        public void OnTextChanged_A()
        {
            string s = value_conv_a.Text;
            if (String.IsNullOrEmpty(s))
            {
                solutionTextBox.Text = "";
                value_conv_a.ShowClearButton = false;
            }
            else
            {
                value_conv_a.ForeColor = Color.Black;

                value_conv_a.ShowClearButton = true;
                s = value_conv_a.Text;

                value_conv_a.ForeColor = Color.Black;

                if (combobox_A_din.SelectedIndex != -1 && combobox_A_in.SelectedIndex != -1)
                {
                    double x;
                    if (Double.TryParse(value_conv_a.Text, out x) && ExtraValid(s))
                    {
                        if (Double.TryParse((ca.VAL[combobox_A_din.SelectedIndex + 1, combobox_A_in.SelectedIndex + 1] * Convert.ToDouble(value_conv_a.Text)).ToString(), out x))
                        {
                            solutionTextBox.Text = x.ToString();
                            copy_button.Visible = true;
                        }
                        else
                        {
                            copy_button.Visible = false;
                            value_conv_a.ForeColor = Color.Crimson;
                        }
                    }
                    else
                    {
                        value_conv_a.ForeColor = Color.Crimson;
                        copy_button.Visible = false;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        public void OnTextChanged_V()
        {
            string s = value_conv_v.Text;
            if (String.IsNullOrEmpty(s))
            {
                solutionTextBox.Text = "";
                value_conv_v.ShowClearButton = false;
            }
            else
            {
                value_conv_v.ForeColor = Color.Black;
                value_conv_v.ShowClearButton = true;
                s = value_conv_v.Text;


                if (combobox_V_din.SelectedIndex != -1 && combobox_V_in.SelectedIndex != -1)
                {
                    double x;
                    if (Double.TryParse(value_conv_v.Text, out x) && ExtraValid(s))
                    {
                        if (Double.TryParse((cv.VAL[combobox_V_din.SelectedIndex + 1, combobox_V_in.SelectedIndex + 1] * Convert.ToDouble(value_conv_v.Text)).ToString(), out x))
                        {
                            solutionTextBox.Text = x.ToString();
                            copy_button.Visible = true;
                        }
                        else
                        {
                            copy_button.Visible = false;
                            value_conv_v.ForeColor = Color.Crimson;
                        }
                    }
                    else
                    {
                        value_conv_v.ForeColor = Color.Crimson;
                        copy_button.Visible = false;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        private void value_conv_m_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_M();
        }

        private void combobox_M_din_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_M();
        }

        private void combobox_M_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_M();
        }

        private void value_conv_m_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            bool point = false;
            s = value_conv_m.Text;

            if (String.IsNullOrEmpty(s))
                if (c == '.') e.Handled = true;

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.')
                {
                    e.Handled = true;
                    break;
                }

            if (c == '.' && point) e.Handled = true;
            if (!Char.IsDigit(c) && c != 8 && c != 46) e.Handled = true;
        }

        private void OnEnglish()
        {
            tabs_menu.TabPages[0].Text = "  NUMBERS  ";
            tabs_menu.TabPages[1].Text = "  LENGTHS  ";
            tabs_menu.TabPages[2].Text = "  TEMPERATURES  ";
            tabs_menu.TabPages[3].Text = "  WEIGHTS  ";
            tabs_menu.TabPages[4].Text = "  AREAS  ";
            tabs_menu.TabPages[5].Text = "  VOLUMES  ";
            tabs_menu.TabPages[6].Text = "  TIME ";

            from_dec.Text = "DECIMAL";
            from_bin.Text = "BINARY";
            from_hex.Text = "HEXADECIMAL";
            to_dec.Text = "DECIMAL";
            to_bin.Text = "BINARY";
            to_hex.Text = "HEXADECIMAL";

            p_result.Text = "Transformation result:";
            p_about.Text = "ABOUT MATH-BEAST";
            p_enter_number.Text = p_enter_number_l.Text = p_enter_number_t.Text = p_enter_number_m.Text = p_enter_number_a.Text = p_enter_number_time.Text = p_enter_number_v.Text = "Enter a number...";
            p_transform_from_l.Text = p_enter_from_t.Text = p_transform_from_m.Text = p_transform_from_a.Text = p_transform_from_v.Text = p_transform_from_timp.Text = "Transform from...";
            p_transform_to.Text = p_transform_to_l.Text = p_enter_to_t.Text = p_transform_to_m.Text = p_transform_to_a.Text = p_transform_in_timp.Text = p_transform_in_v.Text = "Transform to...";

            combobox_L_din.Items[0] = combobox_L_in.Items[0] = "Nanometers".ToUpper();
            combobox_L_din.Items[1] = combobox_L_in.Items[1] = "Microns".ToUpper();
            combobox_L_din.Items[2] = combobox_L_in.Items[2] = "Millimeters".ToUpper();
            combobox_L_din.Items[3] = combobox_L_in.Items[3] = "Centimeters".ToUpper();
            combobox_L_din.Items[4] = combobox_L_in.Items[4] = "Meters".ToUpper();
            combobox_L_din.Items[5] = combobox_L_in.Items[5] = "Kilometers".ToUpper();
            combobox_L_din.Items[6] = combobox_L_in.Items[6] = "Inches".ToUpper();
            combobox_L_din.Items[7] = combobox_L_in.Items[7] = "Feet".ToUpper();
            combobox_L_din.Items[8] = combobox_L_in.Items[8] = "Yards".ToUpper();
            combobox_L_din.Items[9] = combobox_L_in.Items[9] = "Miles".ToUpper();
            combobox_L_din.Items[10] = combobox_L_in.Items[10] = "Nautical Miles".ToUpper();

            combobox_M_din.Items[0] = combobox_M_in.Items[0] = "Carats".ToUpper();
            combobox_M_din.Items[1] = combobox_M_in.Items[1] = "Milligrams".ToUpper();
            combobox_M_din.Items[2] = combobox_M_in.Items[2] = "Centigrams".ToUpper();
            combobox_M_din.Items[3] = combobox_M_in.Items[3] = "Decigrams".ToUpper();
            combobox_M_din.Items[4] = combobox_M_in.Items[4] = "Grams".ToUpper();
            combobox_M_din.Items[5] = combobox_M_in.Items[5] = "Dekagrams".ToUpper();
            combobox_M_din.Items[6] = combobox_M_in.Items[6] = "Hectograms".ToUpper();
            combobox_M_din.Items[7] = combobox_M_in.Items[7] = "Kilograms".ToUpper();
            combobox_M_din.Items[8] = combobox_M_in.Items[8] = "Metric Tonnes".ToUpper();
            combobox_M_din.Items[9] = combobox_M_in.Items[9] = "Ounces".ToUpper();
            combobox_M_din.Items[10] = combobox_M_in.Items[10] = "Pounds".ToUpper();
            combobox_M_din.Items[11] = combobox_M_in.Items[11] = "Stone".ToUpper();
            combobox_M_din.Items[12] = combobox_M_in.Items[12] = "Short Tons (UK)".ToUpper();
            combobox_M_din.Items[13] = combobox_M_in.Items[13] = "Long Tons (US)".ToUpper();

            combobox_A_din.Items[0] = combobox_A_in.Items[0] = "Square millimeters".ToUpper();
            combobox_A_din.Items[1] = combobox_A_in.Items[1] = "Square centimeters".ToUpper();
            combobox_A_din.Items[2] = combobox_A_in.Items[2] = "Square meters".ToUpper();
            combobox_A_din.Items[3] = combobox_A_in.Items[3] = "Hectares".ToUpper();
            combobox_A_din.Items[4] = combobox_A_in.Items[4] = "Square kilometers".ToUpper();
            combobox_A_din.Items[5] = combobox_A_in.Items[5] = "Square inches".ToUpper();
            combobox_A_din.Items[6] = combobox_A_in.Items[6] = "Square feet".ToUpper();
            combobox_A_din.Items[7] = combobox_A_in.Items[7] = "Square yards".ToUpper();
            combobox_A_din.Items[8] = combobox_A_in.Items[8] = "Acres".ToUpper();
            combobox_A_din.Items[9] = combobox_A_in.Items[9] = "Square miles".ToUpper();

            combobox_V_din.Items[0] = combobox_V_in.Items[0] = "Milliliters".ToUpper();
            combobox_V_din.Items[1] = combobox_V_in.Items[1] = "Cubic centimeters".ToUpper();
            combobox_V_din.Items[2] = combobox_V_in.Items[2] = "Liters".ToUpper();
            combobox_V_din.Items[3] = combobox_V_in.Items[3] = "Cubic meters".ToUpper();
            combobox_V_din.Items[4] = combobox_V_in.Items[4] = "Teaspoons (US)".ToUpper();
            combobox_V_din.Items[5] = combobox_V_in.Items[5] = "Tablespoons (US)".ToUpper();
            combobox_V_din.Items[6] = combobox_V_in.Items[6] = "Fluid ounces (US)".ToUpper();
            combobox_V_din.Items[7] = combobox_V_in.Items[7] = "Cups (US)".ToUpper();
            combobox_V_din.Items[8] = combobox_V_in.Items[8] = "Pints (US)".ToUpper();
            combobox_V_din.Items[9] = combobox_V_in.Items[9] = "Quarts (US)".ToUpper();
            combobox_V_din.Items[10] = combobox_V_in.Items[10] = "Gallons (US)".ToUpper();
            combobox_V_din.Items[11] = combobox_V_in.Items[11] = "Cubic inches".ToUpper();
            combobox_V_din.Items[12] = combobox_V_in.Items[12] = "Cubic feets".ToUpper();
            combobox_V_din.Items[13] = combobox_V_in.Items[13] = "Cubic yards".ToUpper();
            combobox_V_din.Items[14] = combobox_V_in.Items[14] = "Teaspoons (UK)".ToUpper();
            combobox_V_din.Items[15] = combobox_V_in.Items[15] = "Tablespoons (UK)".ToUpper();
            combobox_V_din.Items[16] = combobox_V_in.Items[16] = "Fluid ounces (UK)".ToUpper();
            combobox_V_din.Items[17] = combobox_V_in.Items[17] = "Pints (UK)".ToUpper();
            combobox_V_din.Items[18] = combobox_V_in.Items[18] = "Quarts (UK)".ToUpper();
            combobox_V_din.Items[19] = combobox_V_in.Items[19] = "Gallons (UK)".ToUpper();

            combobox_TIMP_din.Items[0] = combobox_TIMP_in.Items[0] = "Microseconds".ToUpper();
            combobox_TIMP_din.Items[1] = combobox_TIMP_in.Items[1] = "Milliseconds".ToUpper();
            combobox_TIMP_din.Items[2] = combobox_TIMP_in.Items[2] = "Seconds".ToUpper();
            combobox_TIMP_din.Items[3] = combobox_TIMP_in.Items[3] = "Minutes".ToUpper();
            combobox_TIMP_din.Items[4] = combobox_TIMP_in.Items[4] = "Hours".ToUpper();
            combobox_TIMP_din.Items[5] = combobox_TIMP_in.Items[5] = "Days".ToUpper();
            combobox_TIMP_din.Items[6] = combobox_TIMP_in.Items[6] = "Weeks".ToUpper();
            combobox_TIMP_din.Items[7] = combobox_TIMP_in.Items[7] = "Years".ToUpper();

            OnTextChanged_N();
            OnTextChanged_L();
            OnTextChanged_M();
            OnTextChanged_T();
            OnTextChanged_A();
            OnTextChanged_V();
        }

        private void OnRomania()
        {
            tabs_menu.TabPages[0].Text = "  NUMERE  ";
            tabs_menu.TabPages[1].Text = "  LUNGIMI  ";
            tabs_menu.TabPages[2].Text = "  TEMPERATURI  ";
            tabs_menu.TabPages[3].Text = "  MASE  ";
            tabs_menu.TabPages[4].Text = "  ARII  ";
            tabs_menu.TabPages[5].Text = "  VOLUME  ";
            tabs_menu.TabPages[6].Text = "  TIMP  ";

            from_dec.Text = to_dec.Text = "ZECIMAL";
            from_bin.Text = to_bin.Text = "BINAR";
            from_hex.Text = to_hex.Text = "HEXAZECIMAL";
            p_result.Text = "Rezultatul transformării:";
            p_about.Text = "DESPRE MATH-BEAST";

            p_enter_number.Text = p_enter_number_l.Text = p_enter_number_t.Text = p_enter_number_m.Text = p_enter_number_a.Text = p_enter_number_time.Text = p_enter_number_v.Text = "Introduceți un număr...";
            p_transform_from_l.Text = p_enter_from_t.Text = p_transform_from_m.Text = p_transform_from_a.Text = p_transform_from_timp.Text = p_transform_from_v.Text = "Transformați din...";
            p_transform_to_l.Text = p_enter_to_t.Text = p_transform_to_m.Text = p_transform_to.Text = p_transform_to_a.Text = p_transform_in_timp.Text = p_transform_in_v.Text = "Transformați în...";

            combobox_L_din.Items[0] = combobox_L_in.Items[0] = "Nanometri".ToUpper();
            combobox_L_din.Items[1] = combobox_L_in.Items[1] = "Microni".ToUpper();
            combobox_L_din.Items[2] = combobox_L_in.Items[2] = "Milimetri".ToUpper();
            combobox_L_din.Items[3] = combobox_L_in.Items[3] = "Centimetri".ToUpper();
            combobox_L_din.Items[4] = combobox_L_in.Items[4] = "Metri".ToUpper();
            combobox_L_din.Items[5] = combobox_L_in.Items[5] = "Kilometri".ToUpper();
            combobox_L_din.Items[6] = combobox_L_in.Items[6] = "Inch".ToUpper();
            combobox_L_din.Items[7] = combobox_L_in.Items[7] = "Picioare".ToUpper();
            combobox_L_din.Items[8] = combobox_L_in.Items[8] = "Yarzi".ToUpper();
            combobox_L_din.Items[9] = combobox_L_in.Items[9] = "Mile".ToUpper();
            combobox_L_din.Items[10] = combobox_L_in.Items[10] = "Mile Nautice".ToUpper();

            combobox_M_din.Items[0] = combobox_M_in.Items[0] = "Carate".ToUpper();
            combobox_M_din.Items[1] = combobox_M_in.Items[1] = "Miligrame".ToUpper();
            combobox_M_din.Items[2] = combobox_M_in.Items[2] = "Centigrame".ToUpper();
            combobox_M_din.Items[3] = combobox_M_in.Items[3] = "Decigrame".ToUpper();
            combobox_M_din.Items[4] = combobox_M_in.Items[4] = "Grame".ToUpper();
            combobox_M_din.Items[5] = combobox_M_in.Items[5] = "Decagrame".ToUpper();
            combobox_M_din.Items[6] = combobox_M_in.Items[6] = "Hectograme".ToUpper();
            combobox_M_din.Items[7] = combobox_M_in.Items[7] = "Kilograme".ToUpper();
            combobox_M_din.Items[8] = combobox_M_in.Items[8] = "Tone Metrice".ToUpper();
            combobox_M_din.Items[9] = combobox_M_in.Items[9] = "Unci".ToUpper();
            combobox_M_din.Items[10] = combobox_M_in.Items[10] = "Livre".ToUpper();
            combobox_M_din.Items[11] = combobox_M_in.Items[11] = "Pietre".ToUpper();
            combobox_M_din.Items[12] = combobox_M_in.Items[12] = "Tona Scurta (UK)".ToUpper();
            combobox_M_din.Items[13] = combobox_M_in.Items[13] = "Tona Lunga (US)".ToUpper();

            combobox_A_din.Items[0] = combobox_A_in.Items[0] = "Milimetri pătrați".ToUpper();
            combobox_A_din.Items[1] = combobox_A_in.Items[1] = "Centimetri pătrați".ToUpper();
            combobox_A_din.Items[2] = combobox_A_in.Items[2] = "Metri pătrați".ToUpper();
            combobox_A_din.Items[3] = combobox_A_in.Items[3] = "Hectare".ToUpper();
            combobox_A_din.Items[4] = combobox_A_in.Items[4] = "Kilometri pătrați".ToUpper();
            combobox_A_din.Items[5] = combobox_A_in.Items[5] = "Inch pătrați".ToUpper();
            combobox_A_din.Items[6] = combobox_A_in.Items[6] = "Picioare pătrate".ToUpper();
            combobox_A_din.Items[7] = combobox_A_in.Items[7] = "Yarzi pătrați".ToUpper();
            combobox_A_din.Items[8] = combobox_A_in.Items[8] = "Acri".ToUpper();
            combobox_A_din.Items[9] = combobox_A_in.Items[9] = "Mile pătrate".ToUpper();

            combobox_V_din.Items[0] = combobox_V_in.Items[0] = "Mililitri".ToUpper();
            combobox_V_din.Items[1] = combobox_V_in.Items[1] = "Centimetri cubi".ToUpper();
            combobox_V_din.Items[2] = combobox_V_in.Items[2] = "Litri".ToUpper();
            combobox_V_din.Items[3] = combobox_V_in.Items[3] = "Metri".ToUpper();
            combobox_V_din.Items[4] = combobox_V_in.Items[4] = "Linguriță (US)".ToUpper();
            combobox_V_din.Items[5] = combobox_V_in.Items[5] = "Lingură (US)".ToUpper();
            combobox_V_din.Items[6] = combobox_V_in.Items[6] = "Uncie de lichid (US)".ToUpper();
            combobox_V_din.Items[7] = combobox_V_in.Items[7] = "Cupă (US)".ToUpper();
            combobox_V_din.Items[8] = combobox_V_in.Items[8] = "Halbă (US)".ToUpper();
            combobox_V_din.Items[9] = combobox_V_in.Items[9] = "Cvartă (US)".ToUpper();
            combobox_V_din.Items[10] = combobox_V_in.Items[10] = "Galon (US)".ToUpper();
            combobox_V_din.Items[11] = combobox_V_in.Items[11] = "Inch cub".ToUpper();
            combobox_V_din.Items[12] = combobox_V_in.Items[12] = "Picior cub".ToUpper();
            combobox_V_din.Items[13] = combobox_V_in.Items[13] = "Yard cub".ToUpper();
            combobox_V_din.Items[14] = combobox_V_in.Items[14] = "Linguriță (UK)".ToUpper();
            combobox_V_din.Items[15] = combobox_V_in.Items[15] = "Lingură (UK)".ToUpper();
            combobox_V_din.Items[16] = combobox_V_in.Items[16] = "Uncie de lichid (UK)".ToUpper();
            combobox_V_din.Items[17] = combobox_V_in.Items[17] = "Halbă (UK)".ToUpper();
            combobox_V_din.Items[18] = combobox_V_in.Items[18] = "Cvartă (UK)".ToUpper();
            combobox_V_din.Items[19] = combobox_V_in.Items[19] = "Galon (UK)";

            combobox_TIMP_din.Items[0] = combobox_TIMP_in.Items[0] = "Microsecunde".ToUpper();
            combobox_TIMP_din.Items[1] = combobox_TIMP_in.Items[1] = "Milisecunde".ToUpper();
            combobox_TIMP_din.Items[2] = combobox_TIMP_in.Items[2] = "Secunde".ToUpper();
            combobox_TIMP_din.Items[3] = combobox_TIMP_in.Items[3] = "Minute".ToUpper();
            combobox_TIMP_din.Items[4] = combobox_TIMP_in.Items[4] = "Ore".ToUpper();
            combobox_TIMP_din.Items[5] = combobox_TIMP_in.Items[5] = "Zile".ToUpper();
            combobox_TIMP_din.Items[6] = combobox_TIMP_in.Items[6] = "Săptamâni".ToUpper();
            combobox_TIMP_din.Items[7] = combobox_TIMP_in.Items[7] = "Ani".ToUpper();

            OnTextChanged_N();
            OnTextChanged_L();
            OnTextChanged_M();
            OnTextChanged_T();
            OnTextChanged_A();
            OnTextChanged_V();
        }

        private void metroRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            string temp = String.Empty;
            if (!String.IsNullOrEmpty(solutionTextBox.Text)) temp = solutionTextBox.Text;

            OnEnglish();
            if (!String.IsNullOrEmpty(temp))
            {
                solutionTextBox.Text = temp;
                copy_button.Visible = true;

                value_conv_m.Focus();
                value_conv_m.ShowClearButton = true;

                value_conv_t.Focus();
                value_conv_t.ShowClearButton = true;

                value_conv_l.Focus();
                value_conv_l.ShowClearButton = true;

                value_conv_a.Focus();
                value_conv_a.ShowClearButton = true;

                value_conv_v.Focus();
                value_conv_v.ShowClearButton = true;

                value_conv_time.Focus();
                value_conv_time.ShowClearButton = true;

                value_conv.Focus();
                value_conv.ShowClearButton = true;
            }
        }

        private void metroRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            string temp = String.Empty;
            if (!String.IsNullOrEmpty(solutionTextBox.Text)) temp = solutionTextBox.Text;

            OnRomania();
            if (!String.IsNullOrEmpty(temp))
            {
                value_conv_m.Focus();
                value_conv_m.ShowClearButton = true;

                value_conv_t.Focus();
                value_conv_t.ShowClearButton = true;

                value_conv_l.Focus();
                value_conv_l.ShowClearButton = true;

                value_conv.Focus();
                value_conv.ShowClearButton = true;

                value_conv_a.Focus();
                value_conv_a.ShowClearButton = true;

                value_conv_v.Focus();
                value_conv_v.ShowClearButton = true;

                value_conv_time.Focus();
                value_conv_time.ShowClearButton = true;

                solutionTextBox.Text = temp;
                copy_button.Visible = true;
            }
        }

        private void value_conv_a_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_A();
        }

        private void combobox_A_din_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_A();
        }

        private void combobox_A_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_A();
        }

        private void value_conv_a_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            bool point = false;

            s = value_conv_a.Text;
            if (String.IsNullOrEmpty(s))
                if (c == '.') e.Handled = true;

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.')
                {
                    e.Handled = true;
                    break;
                }

            if (c == '.' && point) e.Handled = true;
            if (!Char.IsDigit(c) && c != 8 && c != 46) e.Handled = true;
        }

        private void value_conv_v_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_V();
        }

        private void combobox_V_din_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_V();
        }

        private void combobox_V_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_V();
        }

        private void value_conv_v_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            bool point = false;

            s = value_conv_v.Text;
            if (String.IsNullOrEmpty(s))
                if (c == '.') e.Handled = true;

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.')
                {
                    e.Handled = true;
                    break;
                }

            if (c == '.' && point) e.Handled = true;
            if (!Char.IsDigit(c) && c != 8 && c != 46) e.Handled = true;
        }

        private void value_conv_time_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = e.KeyChar;
            bool point = false;

            s = value_conv_time.Text;
            if (String.IsNullOrEmpty(s))
                if (c == '.') e.Handled = true;

            for (int i = 1; i < s.Length; ++i)
                if (s[i] == '.' && c == '.')
                {
                    e.Handled = true;
                    break;
                }

            if (c == '.' && point) e.Handled = true;
            if (!Char.IsDigit(c) && c != 8 && c != 46) e.Handled = true;
        }

        private void OnTextChanged_TIMP()
        {
            string s = value_conv_time.Text;
            if (String.IsNullOrEmpty(s))
            {
                solutionTextBox.Text = "";
                value_conv_time.ShowClearButton = false;
            }
            else
            {
                value_conv_time.ForeColor = Color.Black;
                value_conv_time.ShowClearButton = true;
                s = value_conv_v.Text;


                if (combobox_TIMP_din.SelectedIndex != -1 && combobox_TIMP_in.SelectedIndex != -1)
                {
                    double x;
                    if (Double.TryParse(value_conv_time.Text, out x) && ExtraValid(s))
                    {
                        if (Double.TryParse((ctmp.VAL[combobox_TIMP_din.SelectedIndex + 1, combobox_TIMP_in.SelectedIndex + 1] * Convert.ToDouble(value_conv_time.Text)).ToString(), out x))
                        {
                            solutionTextBox.Text = x.ToString();
                            copy_button.Visible = true;
                        }
                        else
                        {
                            copy_button.Visible = false;
                            value_conv_time.ForeColor = Color.Crimson;
                        }
                    }
                    else
                    {
                        value_conv_time.ForeColor = Color.Crimson;
                        copy_button.Visible = false;
                        if (radio_to_english.Checked) solutionTextBox.Text = "NUMBER TOO LARGE OR WRONG FORMAT!";
                        if (radio_to_romania.Checked) solutionTextBox.Text = "NUMAR PREA MARE SAU FORMAT INCORECT!";
                    }
                }
            }
        }

        private void value_conv_time_TextChanged(object sender, EventArgs e)
        {
            OnTextChanged_TIMP();
        }

        private void combobox_TIMP_din_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_TIMP();
        }

        private void combobox_TIMP_in_SelectedIndexChanged(object sender, EventArgs e)
        {
            OnTextChanged_TIMP();
        }
    }
}
