using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Clients;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
using Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Propietaria.RentCar.UI.Formularios
{
    public partial class CustomerForm : Form
    {
        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;

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

        public CustomerForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }


        private void ClienteForm_Load(object sender, EventArgs e)
        {
            LoadAll();
        }

        private void LoadAll()
        {
            SetDataGrid();
            SetComboBoxDocumentType();
            SetComboBoxTipoPersona();
        }

        private void SetDataGrid()
        {
            var repository = new Infrastructure.Dapper.Query.Clients.GetAllClients();
            var list = repository.Get();
            dataGridView1.DataSource = list;
        }

        private void SetComboBoxDocumentType()
        {
            cbDocumentType.Items.Clear();
            var tradeMarksRepository = new GetAllDocumentType();
            var tradeMarks = tradeMarksRepository.Get();
            cbDocumentType.Items.Add("Selecciona un tipo de documento");
            cbDocumentType.Items.AddRange(tradeMarks);
            cbDocumentType.SelectedIndex = 0;
            cbDocumentType.DropDownHeight = cbDocumentType.ItemHeight * 5;
            cbDocumentType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetComboBoxTipoPersona()
        {
            cbTipoPersonaje.Items.Clear();
            var tradeMarksRepository = new GetAllTypePersons();
            var tradeMarks = tradeMarksRepository.Get();
            cbTipoPersonaje.Items.Add("Selecciona un tipo de persona");
            cbTipoPersonaje.Items.AddRange(tradeMarks);
            cbTipoPersonaje.SelectedIndex = 0;
            cbTipoPersonaje.DropDownHeight = cbDocumentType.ItemHeight * 5;
            cbTipoPersonaje.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string errors = FieldValidators();
                string message = "";
                if (string.IsNullOrEmpty(errors))
                {

                    Customers model = new Customers();
                    model.LastName = txtApellido.Text;
                    model.PersonType = cbTipoPersonaje.Text;
                    model.Status = "Activo";
                    model.DocumentType = cbDocumentType.Text;
                    model.DocumentNumber = txtNoDocumento.Text;
                    model.Name = txtNombre.Text;
                    model.AccountNumber = txtNoTarjeta.Text;

                    model.CreditLimit = Convert.ToDecimal(txtLimiteCredito.Text);
                    model.Id = _id;
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
                //throw ex;
            }
        }

        private void Clear()
        {
            _id = 0;
            txtNoTarjeta.Text = "";
            txtNombre.Text = "";
            txtNoDocumento.Text = "";
            txtLimiteCredito.Text = "";
            txtApellido.Text = "";
        }

        private string FieldValidators()
        {
            string response = "";
            if (cbTipoPersonaje.SelectedIndex == 0)
            {
                response += string.Format("- Debe seleccionar una 'Tipo de personaje' válida {0}", Environment.NewLine);
            }
            if (cbDocumentType.SelectedIndex == 0)
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
            return response;
        }
        private void Add(Customers customer)
        {
            _unitOfWork.CurstomerRepository.Add(customer);
        }
        private void Update(Customers customer)
        {
            _unitOfWork.CurstomerRepository.Update(customer);
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
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
            _id = GetId("Id");
            txtApellido.Text = GetColumnData("Apellido");
            txtNombre.Text = GetColumnData("Nombre");
            cbDocumentType.Text = GetColumnData("TipoDocumento");
            cbTipoPersonaje.Text = GetColumnData("TipoPersona");
            txtNoTarjeta.Text = GetColumnData("NoDocumento");
            txtLimiteCredito.Text = GetColumnData("LimiteCredito");
            txtNoDocumento.Text = GetColumnData("NoDocumento");
            txtNoTarjeta.Text = GetColumnData("NoTarjeta");
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                _id = GetId("Id");
                Delete(_id);
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private void Delete(int id)
        {
            _unitOfWork.CurstomerRepository.Delete(id);
            _unitOfWork.Commit();
            LoadAll();
        }

        private void btnClean_Click_1(object sender, EventArgs e)
        {
            SetComboBoxDocumentType();
            SetComboBoxTipoPersona();
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
