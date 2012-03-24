/* ***********************************************************************
 * (C) 2008-2012 Vladimir Lebedev-Schmidthof <vladimir@schmidthof.com>
 * Made for Albion Games (http://albiongames.org)
 * 
 * 
 *            DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *                    Version 2, December 2004

 * Copyright (C) 2004 Sam Hocevar <sam@hocevar.net>
 * 
 * Everyone is permitted to copy and distribute verbatim or modified
 * copies of this license document, and changing it is allowed as long
 * as the name is changed.

 *           DO WHAT THE FUCK YOU WANT TO PUBLIC LICENSE
 *   TERMS AND CONDITIONS FOR COPYING, DISTRIBUTION AND MODIFICATION

 *  0. You just DO WHAT THE FUCK YOU WANT TO.
 * *********************************************************************** */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StockMaster
{
    public partial class RequestEditor : UI.DBObjectUserControl
    {
        public RequestEditor()
        {
            InitializeComponent();
        }

        public RequestEditor(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public void Retrieve(UInt64 cycleId)
        {
            requestList.Retrieve(cycleId);
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            DataTable table = (DataTable)requestList.DataSource;

            // 1. список компаний, чьи акции вообще есть
            List<String> tickers = new List<string>();
            // 2.для каждой компании считаем суммарный спрос и предложение
            Dictionary<String, UInt64> totalDemand = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> totalOffer = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> limits = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> lotsDemand = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> lotsOffer = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> npcsBuy = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> stockInAction = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> lotsInAction = new Dictionary<string, ulong>();

            Dictionary<UInt64, UInt64> satisfaction = new Dictionary<ulong, ulong>();
            Dictionary<String, UInt64> freeStockOffer = new Dictionary<string, ulong>();
            Dictionary<String, UInt64> freeStockDemand = new Dictionary<string, ulong>();

            Dictionary<String, List<DataRow>> recalcListOffer = new Dictionary<string, List<DataRow>>();
            Dictionary<String, List<DataRow>> recalcListDemand = new Dictionary<string, List<DataRow>>();

            // Цикл 1: считаем общий спрос, общее предложение;
            // общие доли спроса, общие доли предложения
            // Инициализируем словари
            foreach (DataRow row in table.Rows)
            {
                String ticker = Convert.ToString(row["TICKER"]);
                if (!tickers.Contains(ticker))
                {
                    tickers.Add(ticker);

                    totalDemand[ticker] = 0;
                    totalOffer[ticker] = 0;
                    lotsDemand[ticker] = 0;
                    lotsOffer[ticker] = 0;
                    limits[ticker] = Convert.ToUInt64(row["TRADE_LIMIT"]);
                    npcsBuy[ticker] = Convert.ToUInt64(row["NPCS_BUY"]);

                    stockInAction[ticker] = 0;
                    freeStockOffer[ticker] = 0;
                    freeStockDemand[ticker] = 0;

                    recalcListOffer[ticker] = new List<DataRow>();
                    recalcListDemand[ticker] = new List<DataRow>();
                }

                UInt64 qty = Convert.ToUInt64(row["QTY"]);
                bool isBroker = Convert.ToBoolean(row["BROKER"]);
                ushort multiplier = 1;
                if (isBroker)
                    multiplier = 2;

                if (Convert.ToString(row["OPERATION"]).ToUpper() == "B")
                {
                    totalDemand[ticker] += qty;
                    lotsDemand[ticker] += (qty * multiplier);
                }
                else
                {
                    totalOffer[ticker] += qty;
                    lotsOffer[ticker] += (qty * multiplier);
                }
            }

            // Цикл 2: определяем, сколько акций участвует в торговых операциях
            // Задействуем NPC, если необходимо
            foreach (String ticker in tickers)
            {
                if (totalOffer[ticker] > limits[ticker])
                    totalOffer[ticker] = limits[ticker];

                if (totalDemand[ticker] > limits[ticker])
                    totalDemand[ticker] = limits[ticker];

                if (totalOffer[ticker] > totalDemand[ticker])
                {
                    // предложение больше спроса
                    // узнАем, готовы ли купить NPC
                    if (totalOffer[ticker] > totalDemand[ticker] + npcsBuy[ticker])
                    {
                        // Готовы, но не всё
                        stockInAction[ticker] = totalDemand[ticker] + npcsBuy[ticker];
                        lotsDemand[ticker] += npcsBuy[ticker];
                    }
                    else
                    {
                        // Готовы купить весь остаток предложения
                        // этот остаток предложения, который достанется NPC, составляет
                        UInt64 npcPart = totalOffer[ticker] - totalDemand[ticker];
                        stockInAction[ticker] = totalOffer[ticker];
                        lotsDemand[ticker] += npcPart;
                    }
                }
                else
                {
                    // спрос больше предложения
                    stockInAction[ticker] = totalOffer[ticker];
                    // продадут всё, купят не всё
                }
            }


            // Цикл 3: Подсчитываем, сколько кому акций достанется
            foreach (DataRow row in table.Rows)
            {
                String ticker = Convert.ToString(row["TICKER"]);
                UInt64 id = Convert.ToUInt64(row["ID"]);
                UInt64 qty = Convert.ToUInt64(row["QTY"]);
                bool isBroker = Convert.ToBoolean(row["BROKER"]);
                ushort multiplier = 1;
                if (isBroker)
                    multiplier = 2;
                UInt64 share = qty * multiplier;
                
                UInt64 totalShare;
                if (Convert.ToString(row["OPERATION"]).ToUpper() == "B")
                {
                    totalShare = lotsDemand[ticker];
                }
                else
                {
                    totalShare = lotsOffer[ticker];
                }

                satisfaction[id] = share * stockInAction[ticker] / totalShare;
            }

            // Цикл 4:
            // Смотрим, не досталось ли кому больше, чем ему бы хотелось
            foreach (DataRow row in table.Rows)
            {
                String ticker = Convert.ToString(row["TICKER"]);
                UInt64 qty = Convert.ToUInt64(row["QTY"]);
                UInt64 id = Convert.ToUInt64(row["ID"]);

                if (qty < satisfaction[id])
                {
                    // Получили больше чем хотели
                    UInt64 extra = satisfaction[id] - qty;
                    // Добавляем "лишние" акции в буфер свободных
                    if (Convert.ToString(row["OPERATION"]).ToUpper() == "B")
                    {
                        freeStockDemand[ticker] += extra;
                    }
                    else
                    {
                        freeStockOffer[ticker] += extra;
                    }
                    satisfaction[id] = qty;
                }
                else
                {
                    // добавляем строчку в список для рекалькуляции
                    if (Convert.ToString(row["OPERATION"]).ToUpper() == "B")
                    {
                        recalcListDemand[ticker].Add(row);
                    }
                    else
                    {
                        recalcListOffer[ticker].Add(row);
                    }
                }
            }

            // Цикл 5
            // Пересчитываем оставшиеся от брокерского стола крохи
            foreach (String ticker in tickers)
            {
                // Если что-то вдруг осталось от брокеров и есть кому это отдавать
                while (freeStockDemand[ticker] > 0 && recalcListDemand[ticker].Count > 0)
                {
                    // Подсчитываем общие доли оставшихся
                    UInt64 newTotalShare = 0;
                    foreach (DataRow row in recalcListDemand[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        bool isBroker = Convert.ToBoolean(row["BROKER"]);
                        ushort multiplier = 1;
                        if (isBroker)
                            multiplier = 2;
                        UInt64 share = qty * multiplier;
                        newTotalShare += share;
                    }

                    UInt64 freeStock = freeStockDemand[ticker];
                    bool isMeaning = false;
                    foreach (DataRow row in recalcListDemand[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        bool isBroker = Convert.ToBoolean(row["BROKER"]);
                        UInt64 id = Convert.ToUInt64(row["ID"]);
                        ushort multiplier = 1;
                        if (isBroker)
                            multiplier = 2;
                        UInt64 share = qty * multiplier;

                        UInt64 part = freeStock * share / newTotalShare;
                        isMeaning |= part > 0;
                        satisfaction[id] += part;
                        freeStockDemand[ticker] -= part;
                    }
                    //recalcListDemand.Clear();

                    List<DataRow> newRecalcList = new List<DataRow>();

                    foreach (DataRow row in recalcListDemand[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        UInt64 id = Convert.ToUInt64(row["ID"]);
                        if (satisfaction[id] > qty)
                        {
                            UInt64 share = satisfaction[id] - qty;
                            freeStockDemand[ticker] += share;
                            isMeaning = true;
                            satisfaction[id] = qty;
                        }
                        else
                        {
                            newRecalcList.Add(row);
                        }
                    }

                    recalcListDemand[ticker] = newRecalcList;

                    if (!isMeaning)
                    {
                        // делим одну акцию на пятерых
                        freeStockDemand[ticker] = 0;
                    }
                }

                while (freeStockOffer[ticker] > 0 && recalcListOffer[ticker].Count > 0)
                {
                    UInt64 newTotalShare = 0;
                    foreach (DataRow row in recalcListOffer[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        bool isBroker = Convert.ToBoolean(row["BROKER"]);
                        ushort multiplier = 1;
                        if (isBroker)
                            multiplier = 2;
                        UInt64 share = qty * multiplier;
                        newTotalShare += share;
                    }

                    UInt64 freeStock = freeStockOffer[ticker];
                    bool isMeaning = false;
                    foreach (DataRow row in recalcListOffer[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        bool isBroker = Convert.ToBoolean(row["BROKER"]);
                        UInt64 id = Convert.ToUInt64(row["ID"]);
                        ushort multiplier = 1;
                        if (isBroker)
                            multiplier = 2;
                        UInt64 share = qty * multiplier;

                        UInt64 part = freeStock * share / newTotalShare;
                        isMeaning |= part > 0;
                        satisfaction[id] += part;
                        freeStockOffer[ticker] -= part;
                    }

                    List<DataRow> newRecalcList = new List<DataRow>();

                    foreach (DataRow row in recalcListOffer[ticker])
                    {
                        UInt64 qty = Convert.ToUInt64(row["QTY"]);
                        UInt64 id = Convert.ToUInt64(row["ID"]);
                        if (satisfaction[id] > qty)
                        {
                            UInt64 share = satisfaction[id] - qty;
                            freeStockOffer[ticker] += share;
                            isMeaning = true;
                            satisfaction[id] = qty;
                        }
                        else
                        {
                            newRecalcList.Add(row);
                        }
                    }

                    recalcListOffer[ticker] = newRecalcList;

                    if (!isMeaning)
                    {
                        // делим одну акцию на пятерых
                        freeStockOffer[ticker] = 0;
                    }
                }
            }

            // Всё закончилось, показываем:

            DataTable newTable = new DataTable();
            newTable.Columns.Add("ID", Type.GetType("System.UInt64"));
            newTable.Columns.Add("PERSON_ID", Type.GetType("System.UInt64"));
            newTable.Columns.Add("NAME");
            newTable.Columns.Add("TICKER");
            newTable.Columns.Add("OPERATION");
            newTable.Columns.Add("QTY", Type.GetType("System.UInt64"));
            newTable.Columns.Add("BROKER", Type.GetType("System.Boolean"));
            newTable.Columns.Add("RESULT", Type.GetType("System.UInt64"));

            foreach (DataRow row in table.Rows)
            {
                UInt64 id = Convert.ToUInt64(row["ID"]);
                newTable.Rows.Add(id, row["PERSON_ID"], row["NAME"], row["TICKER"], row["OPERATION"], row["QTY"], row["BROKER"], satisfaction[id]);
            }

            ProcessCycleForm form = new ProcessCycleForm();
            form.table = newTable;
            if (form.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow row in form.table.Rows)
                {
                    UInt64 reqId = Convert.ToUInt64(row["ID"]);
                    String ticker = Convert.ToString(row["TICKER"]);
                    String op = Convert.ToString(row["OPERATION"]).ToUpper();
                    UInt64 qty = Convert.ToUInt64(row["QTY"]);
                    UInt64 result = Convert.ToUInt64(row["RESULT"]);
                    UInt64 personId = Convert.ToUInt64(row["PERSON_ID"]);

                    if (op == "B")
                    {
                        getDatabase().buyRequestFulfill2(reqId, personId, ticker, qty, result);
                    }
                    else
                    {
                        getDatabase().sellRequestFulfill2(reqId, personId, ticker, qty, result);
                    }
                }
            }
        }
    }
}
