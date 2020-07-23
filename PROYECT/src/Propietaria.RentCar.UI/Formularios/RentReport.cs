using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propietaria.RentCar.UI.Formularios
{
    public partial class RentReport : Form
    {
        public RentReport()
        {
            InitializeComponent();
        }

        private void RentReport_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
