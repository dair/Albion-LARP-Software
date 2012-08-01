using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace TimeMachine
{
    public partial class RecoveryConsole : TimeMachineControl
    {
        Dictionary<UInt64, ConsoleLine> lines = new Dictionary<ulong, ConsoleLine>();
        bool updating = false;

        public RecoveryConsole()
        {
            InitializeComponent();
            isBlueScreen = true;
        }

        public override void onAppear()
        {
            base.onAppear();

            update();
        }

        public override void update()
        {
            base.update();

            DataTable table = new DataTable();
            db.fillWithConsoleLines(table);
            ConsoleLine line = null;

            shellControl1.CommandEntered -= shellControl1_CommandEntered;
            bool first = true;

            foreach (DataRow row in table.Rows)
            {
                UInt64 id = id0(row["ID"]);

                if (!lines.ContainsKey(id))
                {

                    line = new ConsoleLine();
                    line.line = Convert.ToString(row["LINE"]);
                    line.response = Convert.ToString(row["RESPONSE"]);
                    lines.Add(id, line);

                    if (line.line != null)
                    {
                        String l = line.line;
                        if (first)
                        {
                            first = false;
                        }
                        else
                        {
                            l = shellControl1.Prompt + line.line;
                        }
                        shellControl1.WriteText(l + "\r\n");
                    }

                    if (line.response != null)
                    {
                        shellControl1.WriteText(line.response + "\r\n");
                    }
                }
            }

            shellControl1.CommandEntered -= shellControl1_CommandEntered;
            shellControl1.Enabled = (line == null || line.response != null || line.response.Length == 0);
        }

        private void shellControl1_CommandEntered(object sender, UILibrary.CommandEnteredEventArgs e)
        {
            while (updating)
            {
                Thread.Yield();
            }

            updating = true;

            ConsoleLine line = new ConsoleLine();
            line.line = e.Command;
            UInt64 id = db.addConsoleLine(line.line);
            lines.Add(id, line);
        }
    }

    public class ConsoleLine
    {
        public String line = null;
        public String response = null;
    }
}
