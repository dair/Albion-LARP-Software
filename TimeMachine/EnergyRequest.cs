using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimeMachine
{
    public partial class EnergyRequest : TimeMachineControl
    {
        public EnergyRequest()
        {
            InitializeComponent();
        }

        public override void onAppear()
        {
            base.onAppear();

            errorAmount.Hide();
            periodError.Hide();
            amountText.Focus();

            DateTime now = db.now();
            from.setDate(now);
            to.setDate(now.AddMinutes(10));
        }

        private void commit_Click(object sender, EventArgs e)
        {
            periodError.Hide();
            errorAmount.Hide();
            payButton.Enabled = false;

            UInt64 amount = id0(amountText.Text);
            if (amount == 0)
            {
                errorAmount.Show();
                return;
            }

            bool ok = false;
            DateTime fromDT = from.validate(ref ok);
            if (!ok)
                return;
            DateTime toDT = to.validate(ref ok);
            if (!ok)
                return;
            if (fromDT >= toDT)
            {
                periodError.Show();
                return;
            }

            // проверить, сколько денег на счету проекта
            Dictionary<String, String> pinfo = db.getProjectInfo(id0(TimeMachineContext.getData("project_key")));
            Double projectMoney = id0(pinfo["money"]);
            Double price = db.getEnergyPrice(amount);
            priceLabel.Text = "Стоимость энергии: " + Convert.ToString(price) + ", на счету проекта " + Convert.ToString(projectMoney);
            if (projectMoney < price)
            {
                priceLabel.ForeColor = Color.Red;
                payButton.Enabled = false;
            }
            else
            {
                priceLabel.ForeColor = Color.Green;
                payButton.Enabled = true;
            }
        }

        private void payButton_Click(object sender, EventArgs e)
        {
            commit_Click(null, null);
            if (payButton.Enabled)
            {
                payButton.Enabled = false;
                UInt64 pkey = id0(TimeMachineContext.getData("project_key"));
                Dictionary<String, String> pinfo = db.getProjectInfo(pkey);
                Double projectMoney = id0(pinfo["money"]);
                UInt64 amount = id0(amountText.Text);
                UInt64 price = Convert.ToUInt64(db.getEnergyPrice(amount));
                UInt64 recvId = Convert.ToUInt64(db.getTMStatic("gorsvet_id"));
                db.moneyTransferFromProject(pkey, recvId, price);
                bool ok = false;
                db.addEnergyRequest(pkey, amount, price, TimeMachineContext.gameToReal(from.validate(ref ok)), TimeMachineContext.gameToReal(to.validate(ref ok)));
                (ParentForm as TimeMachineForm).setPage("MAIN_MENU");
            }
        }
    }
}
