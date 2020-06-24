using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Inspection;
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
    public partial class InspectionForm : Form
    {
        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;
        public InspectionForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

        private void InspectionForm_Load(object sender, EventArgs e)
        {
            LoadAll();
        }
        private void LoadAll()
        {
            SetDataGridView();
            SetComboBoxGomaEstado();
            SetComboBoxClientes();
            SetComboBoxVehicles();
            SetComboBoxEmpleados();
        }
        private void SetComboBoxEmpleados()
        {
            cbEmpleado.Items.Clear();
            GetAllEmployee repository = new GetAllEmployee();
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
            GetAllVehicle repository = new GetAllVehicle();
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
        private void SetComboBoxGomaEstado()
        {
            cbGomaEstado.Items.Clear();
            GetAllGomaEstados repository = new GetAllGomaEstados();
            var list = repository.Get();
            cbGomaEstado.Items.Add("Seleccione Status Goma ");
            cbGomaEstado.Items.AddRange(list);
            cbGomaEstado.SelectedIndex = 0;
            cbGomaEstado.DropDownHeight = cbGomaEstado.ItemHeight * 5;
            cbGomaEstado.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void SetDataGridView()
        {
            GetAllInspection repository = new GetAllInspection();
            dataGridView1.DataSource = repository.Get();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count < 2)
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

            dtpFechaInspeccion.Text = GetColumnData("FechaInspeccion");
            txtCantidadCombustible.Text = GetColumnData("CantidadCombustible");
            PopulateComboBox();
            PopulateRaadioButtons();
            var id = GetId("Id");
            _id = Convert.ToInt32(id);
        }
        private void PopulateComboBox()
        {
            cbEmpleado.Text = GetColumnData("Empleado");
            cbCliente.Text = GetColumnData("Cliente");
            cbGomaEstado.Text = GetColumnData("GomaEstado");
            cbVhiculo.Text = GetColumnData("Vehiculo");
        }
        private void PopulateRaadioButtons()
        {
            var gomaRepuesta = Convert.ToBoolean(GetColumnData("GomaRepuesta"));
            ckGomaRepuesta.Checked = gomaRepuesta;
            var relladura = Convert.ToBoolean(GetColumnData("Ralladuras"));
            cbRalladura.Checked = relladura;
            var roturaCrisdtal = Convert.ToBoolean(GetColumnData("RoturaCristal"));
            ckRoturaCristal.Checked = roturaCrisdtal;
            var gato = Convert.ToBoolean(GetColumnData("Gato"));
            ckGato.Checked = gato;
        }

        private int GetId(string columnName)
        {
            var id = (dataGridView1.CurrentRow.Cells[columnName].Value.ToString() ?? "0");
            return Convert.ToInt32(id);
        }

        private string GetColumnData(string columnName)
        {
            var value = dataGridView1.CurrentRow.Cells[columnName].Value;
            return (value == null) ? "" : value.ToString();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count < 2)
            {
                Delete();
                LoadAll();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }
        private void Delete()
        {
            var id = GetId("Id");
            _id = Convert.ToInt32(id);
            _unitOfWork.InspectionRepository.Delete(id);
            _unitOfWork.Commit();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                Inspection model = new Inspection();
                GetIdByName repository = new GetIdByName();
                model.Id = _id;
                model.IdClient = repository.GetClientId(cbCliente.Text);
                model.IdEmployee = repository.GetEmployeeId(cbEmpleado.Text);
                model.IdVehicle = repository.GetVehicleId(cbVhiculo.Text);
                model.FuelQuantity = txtCantidadCombustible.Text;
                model.IsScratched = cbRalladura.Checked;
                model.SubstituteRubber = ckGomaRepuesta.Checked;
                model.GlassBreak = ckRoturaCristal.Checked;
                model.Cat = ckGato.Checked;
                model.InspectionDate = dtpFechaInspeccion.Value;
                model.StatusRubber = cbGomaEstado.Text;
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
                throw ex;
            }


        }
        private void Clear() 
        {
            txtCantidadCombustible.Text = "";
            _id = 0;
            LoadAll();
            cbRalladura.Checked = false;
            ckGato.Checked = false;
            ckRoturaCristal.Checked = false;
            ckGomaRepuesta.Checked = false;
            dtpFechaInspeccion.Value = DateTime.Now;
        }

        private string FieldValidators(Inspection model)
        {
            string response = "";
            if (cbCliente.SelectedIndex == 0)
            {
                response += string.Format("- 'Goma Status' es un campo requerido {0}", Environment.NewLine);
            }
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

            if (string.IsNullOrEmpty(model.FuelQuantity))
            {
                response += string.Format("- 'Cantidad combustible' es un campo requerido {0}", Environment.NewLine);
            }

            if (model.InspectionDate == null)
            {
                response += string.Format("- 'Fecha inspeccion' es un campo requerido {0}", Environment.NewLine);
            }

            return response;
        }

        private void Add(Inspection model)
        {
            _unitOfWork.InspectionRepository.Add(model);
        }

        private void Edit(Inspection model)
        {
            _unitOfWork.InspectionRepository.Update(model);
        }

        private void button1_Click(object sender, EventArgs e)
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
