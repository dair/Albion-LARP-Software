using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Master
{
    public partial class VKEditor : UI.DBObjectUserControl
    {
        public VKEditor()
        {
            InitializeComponent();
        }

        public VKEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();

            this.vkQuestionList.SelectionChanged += new EventHandler(vkQuestionList_SelectionChanged);
        }

        private void refreshButton_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void vkQuestionList_SelectionChanged(object sender, EventArgs e)
        {
            UInt64 qid = vkQuestionList.getCurrentQuestionId();
            if (qid > 0)
            {
                Database.VKQuestionInfo questionInfo = getDatabase().getQuestionInfo(qid);
                if (questionInfo != null)
                {
                    idBox.Text = Convert.ToString(questionInfo.id);
                    textBox.Text = questionInfo.text;
                    switch (questionInfo.gender)
                    {
                        case Database.VKQuestionInfo.Gender.All:
                            genderBox.SelectedIndex = 0;
                            break;
                        case Database.VKQuestionInfo.Gender.Female:
                            genderBox.SelectedIndex = 2;
                            break;
                        case Database.VKQuestionInfo.Gender.Male:
                            genderBox.SelectedIndex = 1;
                            break;
                    }
                }

                vkAnswerList.Retrieve(qid);
            }
            else
            {
                idBox.Text = "";
                textBox.Text = "";
                genderBox.SelectedIndex = 0;
                vkAnswerList.Retrieve(0);
            }
        }

        private void VKEditor_Load(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            UInt64 qid = vkQuestionList.getCurrentQuestionId();
            vkQuestionList.SelectionChanged -= vkQuestionList_SelectionChanged;
            vkQuestionList.Retrieve();
            vkQuestionList.SelectionChanged += vkQuestionList_SelectionChanged;
            vkQuestionList.setCurrentQuestionId(qid);
        }

        private void addAnswerButton_Click(object sender, EventArgs e)
        {
            VKAnswerEdit edit = new VKAnswerEdit();
            DialogResult res = edit.ShowDialog();
            if (res == DialogResult.OK)
            {
                vkAnswerList.dataTable.Rows.Add(edit.answerInfo.text, edit.answerInfo.humanValue, edit.answerInfo.androidValue);
                vkAnswerList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                vkAnswerList.Refresh();
            }
        }

        private void editAnswerButton_Click(object sender, EventArgs e)
        {
            Database.VKAnswerInfo info = vkAnswerList.getCurrentAnswerInfo();
            if (info == null)
            {
                MessageBox.Show("Нечего редактировать", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            VKAnswerEdit edit = new VKAnswerEdit();
            edit.answerInfo = info;
            DialogResult res = edit.ShowDialog();
            if (res == DialogResult.OK)
            {
                vkAnswerList.dataTable.Rows[vkAnswerList.selectedIndex()]["TEXT"] = edit.answerInfo.text;
                vkAnswerList.dataTable.Rows[vkAnswerList.selectedIndex()]["HUMAN"] = edit.answerInfo.humanValue;
                vkAnswerList.dataTable.Rows[vkAnswerList.selectedIndex()]["ANDROID"] = edit.answerInfo.androidValue;
                vkAnswerList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                vkAnswerList.Refresh();
            }

        }

        private void deleteAnswerButton_Click(object sender, EventArgs e)
        {
            if (vkAnswerList.SelectedRows.Count != 1)
            {
                MessageBox.Show("Нечего удалять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Удалить ответ?", "В самом деле?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                vkAnswerList.dataTable.Rows.RemoveAt(vkAnswerList.selectedIndex());
                vkAnswerList.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            DataRow row = vkQuestionList.dataTable.Rows.Add();
            vkQuestionList.Rows[vkQuestionList.Rows.Count - 1].Selected = true;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            UInt64 qid = vkQuestionList.getCurrentQuestionId();
            if (qid == 0)
            {
                MessageBox.Show("Нечего удалять", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("В самом деле удалить вопрос?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                getDatabase().deleteQuestion(qid);
                RefreshData();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Database.VKQuestionInfo qInfo = new Database.VKQuestionInfo();

            UInt64 qid = 0;
            try
            {
                qid = Convert.ToUInt64((idBox.Text));
            }
            catch (Exception)
            {
            }

            qInfo.id = qid;
            qInfo.text = textBox.Text;
            switch (genderBox.SelectedIndex)
            {
                case 1:
                    qInfo.gender = Database.VKQuestionInfo.Gender.Male;
                    break;
                case 2:
                    qInfo.gender = Database.VKQuestionInfo.Gender.Female;
                    break;
                default:
                    qInfo.gender = Database.VKQuestionInfo.Gender.All;
                    break;
            }

            qInfo.id = getDatabase().editQuestion(qInfo);
            idBox.Text = Convert.ToString(qInfo.id);
            getDatabase().setAnswers(qInfo.id, vkAnswerList.dataTable);
            RefreshData();
        }
    }
}
