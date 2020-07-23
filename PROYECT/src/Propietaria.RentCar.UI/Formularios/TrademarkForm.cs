using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query;
using Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork;
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

namespace Propietaria.RentCar.UI.Formularios
{
    public partial class TrademarkForm : Form
    {

        private readonly IUnitOfWork _unitOfWork;
        private int _idTradeMark = 0;

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

        public TrademarkForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        private void TrademarkForm_Load(object sender, EventArgs e)
        {
            LoadAllTradeMark();
        }

        private void LoadAllTradeMark()
        {
            var repository = new GetAllTrademark();
            var list = repository.Get();
            dataGridView1.DataSource = list;
        }

        private void EditButton_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                PopulateForm();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void PopulateForm()
        {
            nameText.Text = GetColumnData("Name");
            descriptionText.Text = GetColumnData("Description");
            var id = GetId("Id");
            _idTradeMark = Convert.ToInt32(id);
        }

        private string GetColumnData(string columnName)
        {
            var value = dataGridView1.CurrentRow.Cells[columnName].Value;
            return (value == null) ? "" : value.ToString();
        }

        private int GetId(string columnName)
        {
            var id = (dataGridView1.CurrentRow.Cells[columnName].Value.ToString() ?? "0");
            return Convert.ToInt32(id);
        }

        private void Clear()
        {
            nameText.Text = "";
            descriptionText.Text = "";
            _idTradeMark = 0;
            LoadAllTradeMark();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = GetId("Id");
                _idTradeMark = Convert.ToInt32(id);
                _unitOfWork.TradeMarkRepository.Delete(Convert.ToInt32(_idTradeMark));
                _unitOfWork.Commit();
                LoadAllTradeMark();
                MessageBox.Show("Registro eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(nameText.Text))
                {
                    if (_idTradeMark == 0)
                    {
                        Add();
                        MessageBox.Show("Registro insertado correctamente");

                    }
                    else
                    {
                        Update("Activo");
                        MessageBox.Show("Registro actualizado correctamente");

                    }
                }
                else
                {
                    string message = "Nombre es un campo valido";
                    string title = "Validacion";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
                }


                _unitOfWork.Commit();
                LoadAllTradeMark();
                Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un error al insertar registro: " + ex);
                throw ex;
            }
        }

        private void Add()
        {
            var model = new Trademark();
            model.Name = nameText.Text;
            model.Description = descriptionText.Text;
            model.Status = "Activo";
            _unitOfWork.TradeMarkRepository.Add(model);
        }

        private void Update(string status)
        {
            var model = new Trademark();
            model.Id = _idTradeMark;
            model.Name = nameText.Text;
            model.Description = descriptionText.Text;
            model.Status = status;
            _unitOfWork.TradeMarkRepository.Update(model);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }
    }
}
