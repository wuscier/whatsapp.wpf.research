using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace whatsapp.winform.layout
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtTotal.Text = "100";
            txtGroupCapacity.Text = "13";
            txtRowCapacity.Text = "5";



            //int groupCapacity = 13;
            //int rowCapacity = 1;

            //int rowNum = groupCapacity / rowCapacity + (groupCapacity % rowCapacity == 0 ? 0 : 1);

            //tableGroup.RowCount = rowNum;
            //tableGroup.ColumnCount = rowCapacity;

            //for (int i = 0; i < groupCapacity; i++)
            //{

            //    int x = i / rowCapacity;

            //    int y = i % rowCapacity;

            //    Button btn = new Button()
            //    {
            //        Text = $"{x},{y}",
            //        Width = 80,
            //        Height = 80,
            //    };

            //    tableGroup.Controls.Add(btn, y, x);
            //}
        }


        private int _total = 0;
        private int _groupCapacity = 0;
        private int _rowCapacity = 0;

        private int _rowNum = 0;
        private int _groupNum = 0;

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                _total = int.Parse(txtTotal.Text.Trim());
                _groupCapacity = int.Parse(txtGroupCapacity.Text.Trim());
                _rowCapacity = int.Parse(txtRowCapacity.Text.Trim());

                _rowNum = _groupCapacity / _rowCapacity + (_groupCapacity % _rowCapacity == 0 ? 0 : 1);
                _groupNum = _total / _groupCapacity + (_total % _groupCapacity == 0 ? 0 : 1);

                gbGroup.Text = $"共{_groupNum}组";
                gbContainer.Text = $"共{_rowNum}行";

                UninitGroup();
                InitGroup();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UninitGroup()
        {
            tableGroup.RowCount = 1;
            tableGroup.ColumnCount = 1;
            tableGroup.Controls.Clear();
        }

        private void InitGroup()
        {
            tableGroup.RowCount = _groupNum;
            tableGroup.ColumnCount = 1;

            for (int i = 0; i < _groupNum - 1; i++)
            {
                int groupBegin = i * _groupCapacity + 1;
                int groupEnd = (i + 1) * _groupCapacity;


                tableGroup.Controls.Add(GetFlowLayoutPanel(i + 1, groupBegin, groupEnd), 0, i);
            }

            int lastGroupNum = _total - _groupCapacity * (_groupNum - 1);

            int lastGroupBegin = (_groupNum - 1) * _groupCapacity;
            int lastGroupEnd = lastGroupBegin + lastGroupNum;



            tableGroup.Controls.Add(GetFlowLayoutPanel(_groupNum, lastGroupBegin, lastGroupEnd), 0, _groupNum - 1);
        }

        private FlowLayoutPanel GetFlowLayoutPanel(int groupNum, int groupBegin, int groupEnd)
        {
            Label lbl = new Label()
            {
                Width = 150,
                Height = 30,
                Text = $"第{groupNum}组 [ {groupBegin} - {groupEnd} ]",
                TextAlign = ContentAlignment.MiddleCenter,
            };

            Button btn = new Button()
            {
                Text = "启动",
                Height = 30,
                Width = 60,
                TextAlign = ContentAlignment.MiddleCenter,

            };

            FlowLayoutPanel flp = new FlowLayoutPanel();
            flp.Width = 250;
            flp.FlowDirection = FlowDirection.LeftToRight;

            flp.Controls.Add(lbl);
            flp.Controls.Add(btn);

            return flp;
        }
    }
}
