using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Rent;
using Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Propietaria.RentCar.UI.Formularios
{
    public partial class RentForm : Form
    {

        private readonly IUnitOfWork _unitOfWork;
        private int _id = 0;
        private int lastIdVehicle = 0;
        public RentForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

        private void RentForm_Load(object sender, EventArgs e)
        {
            LoadAll();
          
        }

        private void SetComboBoxEmpleados()
        {
            cbEmpleado.Items.Clear();
            Infrastructure.Dapper.Query.Helpers.GetAllEmployee repository = new Infrastructure.Dapper.Query.Helpers.GetAllEmployee();
            var list = repository.Get();
            cbEmpleado.Items.Add("Seleccione un empleado");
            cbEmpleado.Items.AddRange(list);
            cbEmpleado.SelectedIndex = 0;
            cbEmpleado.DropDownHeight = cbEmpleado.ItemHeight * 5;
            cbEmpleado.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SetComboBoxVehicles()
        {
            cbVhiculo.Items.Clear();
            Infrastructure.Dapper.Query.Helpers.GetAllVehicle repository = new Infrastructure.Dapper.Query.Helpers.GetAllVehicle();
            var list = repository.Get();
            cbVhiculo.Items.Add("Seleccione un vehiculo");
            cbVhiculo.Items.AddRange(list);
            cbVhiculo.SelectedIndex = 0;
            cbVhiculo.DropDownHeight = cbVhiculo.ItemHeight * 5;
            cbVhiculo.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void SetComboBoxClientes()
        {
            cbCliente.Items.Clear();
            GetAllClients repository = new GetAllClients();
            var list = repository.Get();
            cbCliente.Items.Add("Seleccione un cliente");
            cbCliente.Items.AddRange(list);
            cbCliente.SelectedIndex = 0;
            cbCliente.DropDownHeight = cbCliente.ItemHeight * 5;
            cbCliente.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadAll()
        {
            var repository = new GetAllRents();
            var list = repository.Get();
            dataGridView1.DataSource = list;
            // SetComboBoxGomaEstado();
            SetComboBoxClientes();
            SetComboBoxVehicles();
            SetComboBoxEmpleados();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                var id = GetId("Id");
                _id = Convert.ToInt32(id);
                _unitOfWork.RentRepository.Delete(Convert.ToInt32(_id));
                _unitOfWork.Commit();
                LoadAll();
                MessageBox.Show("Registro eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                GetIdByName repository = new GetIdByName();
                PopulateForm();
                lastIdVehicle = repository.GetVehicleId(cbVhiculo.Text);
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void PopulateForm()
        {
            PopulateComboBox();
            var id = GetId("Id");
            _id = Convert.ToInt32(id);
            txtDias.Text = GetColumnData("Dias");
            txtMontoDiario.Text = GetColumnData("MontoDiario");
            dtpFechaDevolucion.Value = Convert.ToDateTime(GetColumnData("FechaDevolucion"));
            dtpFechaRenta.Value = Convert.ToDateTime(GetColumnData("FechaRenta"));
        }

        private void PopulateComboBox()
        {

            cbEmpleado.Text = GetColumnData("Empleado");
            cbCliente.Text = GetColumnData("Cliente");
            cbVhiculo.Text = GetColumnData("Vehiculo");
        }

        private void txtMontoDiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void txtDias_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Rent model = new Rent();
                GetIdByName repository = new GetIdByName();
                model.Id = _id;
                model.IdClient = repository.GetClientId(cbCliente.Text);
                model.IdEmployee = repository.GetEmployeeId(cbEmpleado.Text);
                model.IdVehicle = repository.GetVehicleId(cbVhiculo.Text);
                model.DateStart = dtpFechaRenta.Value;
                model.DateEnd = dtpFechaDevolucion.Value;
                model.AmountForDay = Convert.ToDecimal(txtMontoDiario.Text);
                model.Days = Convert.ToInt32(txtDias.Text);
                model.Status = "Activo";
                string errors = FieldValidators(model);
                string message = "";
                if (string.IsNullOrEmpty(errors))
                {
                    if (model.Id == 0)
                    {

                        Add(model);
                        message = "Registro Agregado correctamente";
                    }
                    else
                    {
                        Edit(model);
                        message = "Registro Editado correctamente";
                    }
                    _unitOfWork.Commit();
                    string title = "Información";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                    LoadAll();
                    Clear();
                }
                else
                {
                    string title = "Validacion";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(errors, title, buttons, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                string message = "Ocurrio un error al insertar registro: " + ex.Message;
                string title = "Informacion";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                //throw ex;
            }

        }

        private string FieldValidators(Rent model)
        {
            string response = "";

            if (model.IdClient == 0)
            {
                response += string.Format("- 'Cliente' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.IdEmployee == 0)
            {
                response += string.Format("- 'Empleado' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.IdVehicle == 0)
            {
                response += string.Format("- 'Vehiculo' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.AmountForDay == 0)
            {
                response += string.Format("- 'Monto por dias' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.Days == 0)
            {
                response += string.Format("- 'Dias' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.DateEnd == null)
            {
                response += string.Format("- 'Fecha Renta' es un campo requerido {0}", Environment.NewLine);
            }
            if (model.DateStart == null)
            {
                response += string.Format("- 'Fecha Devolucion' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.DateStart > model.DateEnd)
            {
                response += string.Format("- 'Fecha Devolucion' no puede ser menor que 'Fecha Renta' {0}", Environment.NewLine);
            }

            return response;
        }

        private void Clear()
        {
            LoadAll();
            _id = 0;
            txtDias.Text ="";
            txtMontoDiario.Text = "";
            dtpFechaDevolucion.Value =DateTime.Now;
            dtpFechaRenta.Value =DateTime.Now.AddDays(-8);
            lastIdVehicle = 0;

        }
        private void Add(Rent model)
        {
            GetByIdVehicle repository = new GetByIdVehicle();
            var vehiculo = repository.Get(model.IdVehicle);
            vehiculo.Estado = "Rentado";
            _unitOfWork.VehicleRepository.UpdateStatus(vehiculo.Id, "Rentado");

            _unitOfWork.RentRepository.Add(model);
        }
        private void Edit(Rent model)
        {
            UpdateLastVehicle(model);
            _unitOfWork.VehicleRepository.UpdateStatus(model.Id, "Rentado");
            _unitOfWork.RentRepository.Update(model);

        }

        private void UpdateLastVehicle(Rent model)
        {
            GetByIdVehicle repository = new GetByIdVehicle();
            var lastVehiculo = repository.Get(lastIdVehicle);
            if (model.IdVehicle != lastVehiculo.Id)
            {
                lastVehiculo.Estado = "Rentado";
                _unitOfWork.VehicleRepository.UpdateStatus(lastVehiculo.Id, "Activo");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
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
