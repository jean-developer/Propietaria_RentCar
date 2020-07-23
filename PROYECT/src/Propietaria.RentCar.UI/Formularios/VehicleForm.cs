using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Vehicle;
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
    public partial class VehicleForm : Form
    {

        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;
        public VehicleForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }
      

        private void VehicleForm_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void LoadAll()
        {
            SetDataGridView();
            SetComboBoxMarcas();
            SetComboBoxTipoCombustible();
            SetComboBoxTipoVehiculo();
            cbModelos.Enabled = false;

        }

        private void SetDataGridView()
        {
            var vehicleRepository = new Infrastructure.Dapper.Query.Vehicle.GetAllVehicle();
            var vehicles = vehicleRepository.Get();
            dataGridView1.DataSource = vehicles;
        }

        private void SetComboBoxMarcas()
        {
            cbMarcas.Items.Clear();
            var tradeMarksRepository = new GetAllTradeMarks();
            var tradeMarks = tradeMarksRepository.Get();
            cbMarcas.Items.Add("Selecciona una marca");
            cbMarcas.Items.AddRange(tradeMarks);
            cbMarcas.SelectedIndex = 0;
            cbMarcas.DropDownHeight = cbMarcas.ItemHeight * 5;
            cbMarcas.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void SetComboBoxTipoCombustible()
        {
            cbTipoCombustible.Items.Clear();
            var repository = new GetAllTypeCombustible();
            var list = repository.Get();
            cbTipoCombustible.Items.Add("Selecciona un tipo de combustible");
            cbTipoCombustible.Items.AddRange(list);
            cbTipoCombustible.SelectedIndex = 0;
            cbTipoCombustible.DropDownHeight = cbMarcas.ItemHeight * 5;
            cbTipoCombustible.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void SetComboBoxTipoVehiculo()
        {
            cbTipoVehiculo.Items.Clear();
            var repository = new GetAllVehicleType();
            var list = repository.Get();
            cbTipoVehiculo.Items.Add("Selecciona un tipo de vehiculo");
            cbTipoVehiculo.Items.AddRange(list);
            cbTipoVehiculo.SelectedIndex = 0;
            cbTipoVehiculo.DropDownHeight = cbMarcas.ItemHeight * 5;
            cbTipoVehiculo.DropDownStyle = ComboBoxStyle.DropDownList;

        }



        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBoxsValues cb = new ComboBoxsValues();
                cb.Marca = cbMarcas.Text;
                cb.Modelo = cbModelos.Text;
                cb.TipoCombustible = cbTipoCombustible.Text;
                cb.TipoVehiculo = cbTipoVehiculo.Text;

                cb = FieldValidators(cb, txtNombre.Text);
                string message = "";
                if (string.IsNullOrEmpty(cb.Errors))
                {
                   
                    Vehicle model = new Vehicle();
                    model.Description = txtDescripcion.Text;
                    model.NoChasis = txtNoChasis.Text;
                    model.NoMotor = txtNoMotor.Text;
                    model.NoPlaca = txtNoPlaca.Text;
                    model.Name = txtNombre.Text;
                    model.ModelsId = cb.IdModelo;
                    model.TrademarkId = cb.IdMarca;
                    model.VehicleTypeId = cb.IdTipoVehiculo;
                    model.FuelType = cb.IdTipoCombustible;
                    model.Status = "Activo";
                    model.Id = _id;
                    if (model.Id == 0)
                    {

                        Add(model);
                        message = "Registro insertado correctamente";
                        
                    }
                    else
                    {
                        Update( model);
                        message = "Registro actualizado correctamente";
                    }
                    string title = "Información";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                    _unitOfWork.Commit();
                    LoadAll();
                }
                else
                {
                    string title = "Validacion";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(cb.Errors, title, buttons, MessageBoxIcon.Warning);
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

        private void Update( Vehicle model)
        {
            _unitOfWork.VehicleRepository.Update(model);
        }

        private void Add(Vehicle model)
        {
            _unitOfWork.VehicleRepository.Add(model);
        }

        private ComboBoxsValues FieldValidators(ComboBoxsValues comboBox, string nombre)
        {
            GetIdByName repository = new GetIdByName();
            comboBox.IdMarca= repository.Get(comboBox.Marca, "Marcas");
            comboBox.IdModelo = repository.Get(comboBox.Modelo, "Modelos");
            comboBox.IdTipoCombustible = repository.Get(comboBox.TipoCombustible, "TipoCombustible");
            comboBox.IdTipoVehiculo = repository.Get(comboBox.TipoVehiculo, "TipoVehiculo");
            string response = "";
            if(comboBox.IdMarca == 0)
            {
                response += string.Format("- Debe seleccionar una 'Marca' válida {0}", Environment.NewLine);
            }
            if (comboBox.IdModelo == 0)
            {
                response += string.Format("- Debe seleccionar un 'Modelo' válido {0}", Environment.NewLine);
            }
            if (comboBox.IdTipoCombustible == 0)
            {
                response += string.Format("- Debe seleccionar una 'Tipo de combustible' válido {0}", Environment.NewLine);
            }
            if (comboBox.IdTipoVehiculo == 0)
            {
                response += string.Format("- Debe seleccionar una 'Tipo de vehículo' válida {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(nombre))
            {
                response += string.Format("- Nombre es un campo requerido {0}", Environment.NewLine);
            }
            comboBox.Errors = response;
            return comboBox;
        }

        private void cbMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbMarcas.SelectedIndex != 0)
            {
                cbModelos.Enabled = true;
                string nombreMarca = cbMarcas.SelectedItem.ToString();
                LoadComboBoxModelos(nombreMarca);
            }
            else
            {
                cbModelos.Enabled = false;
            }
        }
        private void LoadComboBoxModelos(string nombreMarca)
        {
            cbModelos.Items.Clear();
            var repository = new GetModelsByTradeMarkName();
            var list = repository.Get(nombreMarca);
            if (list.Count()> 0)
            {
                cbModelos.Items.Add("Selecciona un modelo");
                cbModelos.Items.AddRange(list);
            }
            else
            {
                cbModelos.Items.Add("Selecciona un modelo");
                cbModelos.Enabled = false;
                string title = "Advertencia";
                string message = "No existen modelos para la marca seleccionada.";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            }
            
            cbModelos.SelectedIndex = 0;
            cbModelos.DropDownHeight = cbMarcas.ItemHeight * 5;
            cbModelos.DropDownStyle = ComboBoxStyle.DropDownList;
            
        }

        private void btnEdit_Click(object sender, EventArgs e)
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
            txtNombre.Text = GetColumnData("Nombre");
            txtDescripcion.Text = GetColumnData("Descripcion");
            cbMarcas.Text = GetColumnData("Marca");
            cbModelos.Text = GetColumnData("Modelo");
            cbTipoVehiculo.Text = GetColumnData("TipoVehiculo");
            cbTipoCombustible.Text = GetColumnData("TipoCombustible");
            txtNoChasis.Text = GetColumnData("NoChasis");
            txtNoMotor.Text = GetColumnData("NoMotor");
            txtNoPlaca.Text = GetColumnData("NoPlaca");
            var id = GetId("Id");
            _id = Convert.ToInt32(id);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var id = GetId("Id");
                _id = Convert.ToInt32(id);
                Delete(_id);

            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void Delete(int id)
         {
            _unitOfWork.VehicleRepository.Delete(id);
            _unitOfWork.Commit();
            LoadAll();
            string message = "Registro eliminado correctamente";
            string title = "Informacion";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        private void btnClean_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MenuPrincipal menuPrincipal = new MenuPrincipal();
            menuPrincipal.Show();
            this.Close();
        }
    }

    public class ComboBoxsValues
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string TipoVehiculo { get; set; }
        public string TipoCombustible { get; set; }

        public int IdMarca { get; set; }
        public int IdModelo { get; set; }
        public int IdTipoVehiculo { get; set; }
        public int IdTipoCombustible { get; set; }

        public string Errors { get; set; }
    }
}
