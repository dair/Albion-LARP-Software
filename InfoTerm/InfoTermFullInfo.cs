using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace InfoTerm
{
    public partial class InfoTermFullInfo : InfoTermObject
    {
        String searchString;
        UInt64 infoAboutId;

        public InfoTermFullInfo()
        {
            InitializeComponent();
        }

        public InfoTermFullInfo(Database.Connection db)
            : base(db)
        {
            InitializeComponent();
        }

        public override void Init(ClientUI.UserObjectEventArgs args)
        {
            base.Init(args);
            searchString = Convert.ToString(args.data["SEARCH_STRING"]);
            infoAboutId = Convert.ToUInt64(args.data["INFO_ABOUT"]);

            String codeString = Convert.ToString(infoAboutId);
            int len = codeString.Length;
            for (int i = 0; i < 13 - len; ++i)
            {
                codeString = "0" + codeString;
            }

            String countryCode = codeString.Substring(0, 2);
            String manCode = codeString.Substring(2, 5);
            String prodCode = codeString.Substring(7, 5);
            String checkSum = codeString.Substring(12);
            Ean13Barcode2005.Ean13 ean13 =
                new Ean13Barcode2005.Ean13(countryCode, manCode, prodCode, checkSum);
            ean13.Scale = 2.0F;
            Bitmap bmp = ean13.CreateBitmap();
            Rectangle rect = new Rectangle(0, 95, 270, 100);
            Bitmap cropped = bmp.Clone(rect, bmp.PixelFormat);
            pictureBox.Image = cropped;

            Database.FullPersonInfo fpInfo = getDatabase().getPersonInfo(infoAboutId);
            DataTable table = new DataTable();
            getDatabase().fillWithPoliceProperties(infoAboutId, table);
            baseTableView.DataSource = table;
            baseTableView.Columns["ID"].Visible = false;
            baseTableView.Columns["NAME"].Width = 200;

            codeBox.Text = Convert.ToString(infoAboutId);
            nameBox.Text = fpInfo.name;
            switch (fpInfo.gender)
            {
                case Database.FullPersonInfo.Gender.Male:
                    genderBox.Text = "Мужской";
                    break;
                case Database.FullPersonInfo.Gender.Female:
                    genderBox.Text = "Женский";
                    break;
                default:
                    genderBox.Text = "Неизвестно";
                    break;
            }
        }

        public override void BarCodeScanned(ulong code)
        {
            codeBox.Text = infoAboutId.ToString() + " (" + code.ToString() + ")";
        }

        private void baseTableView_KeyDown(object sender, KeyEventArgs e)
        {
            OnKeyDown(sender, e);
        }
    }
}
