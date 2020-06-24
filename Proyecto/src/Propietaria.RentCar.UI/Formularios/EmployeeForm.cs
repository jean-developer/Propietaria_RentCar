using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
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
    public partial class EmployeeForm : Form
    {
        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadAll();
        }
        private void LoadAll()
        {
            SetDataGridView();
            SetCbTipoDocumento();
            SetCbTandaLaboral();
        }
        private void SetCbTipoDocumento()
        {
            cbTipoDocumento.Items.Clear();
            var repository = new GetAllDocumentType();
            var list = repository.Get();
            cbTipoDocumento.Items.Add("Selecciona un tipo de documento");
           cbTipoDocumento.Items.AddRange(list);
            cbTipoDocumento.DropDownHeight = cbTipoDocumento.ItemHeight * 5;
            cbTipoDocumento.SelectedIndex = 0;
        }
        private void SetCbTandaLaboral()
        {
            cbTandaLaboral.Items.Clear();
            var repository = new GetAllTandaLaboral();
            var list = repository.Get();
            cbTandaLaboral.Items.Add("Selecciona una tanda laboral");
            cbTandaLaboral.Items.AddRange(list);
            cbTandaLaboral.DropDownHeight = cbTandaLaboral.ItemHeight * 5;
            cbTandaLaboral.SelectedIndex = 0;
        }
        private void SetDataGridView()
        {
            var repository = new Infrastructure.Dapper.Query.GetAllEmployee();
            var list = repository.Get();
            dataGridView1.DataSource = list;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string errors = FieldValidators();
                if (string.IsNullOrEmpty(errors))
                {
                    Employee model = new Employee();
                    model.Id = GetId("Id");
                    model.Name = txtNombre.Text;
                    model.LastName = txtApellido.Text;
                    model.Commission = Convert.ToDecimal(txtComision.Text);
                    model.WorkShift = cbTandaLaboral.Text;
                    model.DocumentType = cbTipoDocumento.Text;
                    model.DocumentNumber = txtNoDocumento.Text;
                    model.CreatedOn = dtpFechaIngreso.Value;
                    model.Status = "Activo";
                    model.Id = _id;
                    string message = "";
                    if (model.Id == 0)
                    {
                        Add(model);
                        message = "Registro insertado correctamente";
                    }
                    else
                    {
                        Update(model);
                        message = "Registro actualizado correctamente";
                    }

                    string title = "Información";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
                    _unitOfWork.Commit();
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
            SetCbTandaLaboral();
            SetCbTipoDocumento();
            txtApellido.Text = "";
            txtComision.Text = "";
            txtNoDocumento.Text = "";
            txtNombre.Text = "";
        }
        private void Add(Employee model)
        {
            _unitOfWork.EmployeeRepository.Add(model);
        }
        private void Update(Employee model)
        {
            _unitOfWork.EmployeeRepository.Update(model);
        }
        private string FieldValidators()
        {
            string response = "";
            if (cbTandaLaboral.SelectedIndex <= 0)
            {
                response += string.Format("- Debe seleccionar una 'Tanda laboral' válida {0}", Environment.NewLine);
            }
            if (cbTipoDocumento.SelectedIndex <= 0)
            {
                response += string.Format("- Debe seleccionar un 'Tipo de documento' válido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                response += string.Format("- Nombre es un campo requerido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                response += string.Format("- Apellido es un campo requerido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(txtComision.Text))
            {
                response += string.Format("- Comision es un campo requerido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(dtpFechaIngreso.Text))
            {
                response += string.Format("- Fecha de ingreso es un campo requerido {0}", Environment.NewLine);
            }
            return response;
        }
        private void btnEditar_Click_1(object sender, EventArgs e)
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
            txtNombre.Text = GetColumnData("Nombre");
            txtNoDocumento.Text = GetColumnData("NoDocumento");
            txtComision.Text = GetColumnData("Comision");
            cbTipoDocumento.Text = GetColumnData("TipoDocumento");
            dtpFechaIngreso.Text = GetColumnData("FechaIngreso");
            txtApellido.Text = GetColumnData("Apellido");
            cbTandaLaboral.Text = GetColumnData("TandaLaboral");
            var id = GetId("Id");
            _id = Convert.ToInt32(id);
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

        private void btnEliminar_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count < 2)
            {
                Delete();
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

       private void Delete()
        {
            var id = GetId("Id");
            _unitOfWork.EmployeeRepository.Delete(id);
            _unitOfWork.Commit();
            LoadAll();
        }

        private void btnClean_Click(object sender, EventArgs e)
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
