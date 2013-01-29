using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace WorldBankDesktop
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var _db = new GLOBALEDGE_MVCEntities())
            {

            }
            //
            /*listView1.Scrollable = true;
            var mr = new MakeRequest();
            var indicators = mr.getAllIndicators();
            List<Indicators> ind = await indicators;
            foreach (var i in ind)
            {
                Match m = Regex.Match(i.name.Trim(), @"(.*)\((.*)\)+$");
                string name = "", unit = "";
                if (m.Groups.Count > 1)
                {
                    name = m.Groups[1].Value;
                    unit = m.Groups[2].Value;
                }
                else
                {
                    name = i.name;
                }

                /* Inserts a new Unit with SP 
                var result = _db.DIBS_Insert_Unit(unit);
                _db.SaveChanges();
                
                int UnitID = 0;
                if(result.Count() > 0)
                {
                    UnitID = result.First().UnitID;
                }
                else
                {
                    var next = result.GetNextResult<int>();
                    UnitID = (int)next.First();
                }

                _db.DIBS_Field_Insert(i.id, i.name.Trim(), i.sourceNote.Trim(), UnitID, 117, 38, true, false);
                _db.SaveChanges();
            }
            mr.Dispose();*/
        }


    }
}
