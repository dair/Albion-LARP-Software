using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class VKAnswerEdit : Form
    {
        public Database.VKAnswerInfo answerInfo = null;

        public VKAnswerEdit()
        {
            InitializeComponent();
        }

        private void VKAnswerEdit_Load(object sender, EventArgs e)
        {
            Settings.UI.restoreForm(this);

            if (answerInfo == null)
                return;

            textBox.Text = answerInfo.text;
            humanValueBox.Text = Convert.ToString(answerInfo.humanValue);
            androidValueBox.Text = Convert.ToString(answerInfo.androidValue);
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (answerInfo == null)
                answerInfo = new Database.VKAnswerInfo();

            answerInfo.text = textBox.Text.Trim();
            try
            {
                if (humanValueBox.Text.Trim() == "")
                    answerInfo.humanValue = 0;
                else
                    answerInfo.humanValue = Convert.ToInt16(humanValueBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не то написано в поле " + label4.Text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                if (androidValueBox.Text.Trim() == "")
                    answerInfo.androidValue = 0;
                else
                    answerInfo.androidValue = Convert.ToInt16(androidValueBox.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Что-то не то написано в поле " + label3.Text, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
