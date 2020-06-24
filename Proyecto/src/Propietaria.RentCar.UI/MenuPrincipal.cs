﻿using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query;
using Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork;
using Propietaria.RentCar.UI.Formularios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propietaria.RentCar.UI
{
    public partial class MenuPrincipal : Form
    {

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );

        public MenuPrincipal()
        {
         
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            var vehicleTypeForm = new VehicleTypeForm();
            vehicleTypeForm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            this.Hide();
            FuelTypeForm fuelTypeForm = new FuelTypeForm();
            fuelTypeForm.Show();
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrademarkForm trademarkForm = new TrademarkForm();
            trademarkForm.Show();
        }
    }
}
