using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Helpers;
using Propietaria.RentCar.Infrastructure.Dapper.UnitOfWork;
using Propietaria.RentCar.Infrastructure.Model;
using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

namespace Propietaria.RentCar.UI.Formularios
{
    public partial class ModelsForm : Form
    {
        private int _idModels;
        private readonly IUnitOfWork _unitOfWork;
        public ModelsForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

        private void ModelsForm_Load(object sender, EventArgs e)
        {
            LoadAll();
        }
        private void LoadAll()
        {
            SetDataGridView();
            SetComboBox();
        }

        private void SetDataGridView()
        {
            var modelsRepository = new GetAllModels();
            var models = modelsRepository.Get();
            var list = ModelsVM.MapList(models.ToList());
            dataGridView1.DataSource = list;
        }

        private void SetComboBox()
        {
            comboBox1.Items.Clear();
            var tradeMarksRepository = new GetAllTradeMarks();
            var tradeMarks = tradeMarksRepository.Get();
            comboBox1.Items.AddRange(tradeMarks);
            comboBox1.DropDownHeight = comboBox1.ItemHeight * 5;
            comboBox1.Text = "Selecciona una marca";

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0 && dataGridView1.SelectedRows.Count < 2)
            {
                var id = GetId("Id");
                _idModels = Convert.ToInt32(id);
                _unitOfWork.ModelsRepository.Delete(Convert.ToInt32(_idModels));
                _unitOfWork.Commit();
                LoadAll();
                MessageBox.Show("Registro eliminado correctamente");
            }
            else
            {
                MessageBox.Show("Seleccione una fila por favor");
            }
        }

        private int GetId(string columnName)
        {
            var id = (dataGridView1.CurrentRow.Cells[columnName].Value.ToString() ?? "0");
            return Convert.ToInt32(id);
        }

        private void editButton_Click_1(object sender, EventArgs e)
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
            nombreText.Text = GetColumnData("Nombre");
            descripcionText.Text = GetColumnData("Descripcion");
            comboBox1.Text = GetColumnData("Marca");
            var id = GetId("Id");
            _idModels = Convert.ToInt32(id);
        }

        private string GetColumnData(string columnName)
        {
            var value = dataGridView1.CurrentRow.Cells[columnName].Value;
            return (value == null) ? "" : value.ToString();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(nombreText.Text))
                {
                    GetIdByName repository = new GetIdByName();
                    string nameTradeMark = comboBox1.Text;
                    int tradeMarkId = repository.Get(nameTradeMark, "Marcas");
                    if (tradeMarkId != 0)
                    {
                        if (_idModels == 0)
                        {
                            Add(tradeMarkId);
                            MessageBox.Show("Registro insertado correctamente");
                        }
                        else
                        {
                            Update("Activo", tradeMarkId);
                            MessageBox.Show("Registro actualizado correctamente");
                        }
                    }
                    else
                    {
                        string message = "Debe seleccionar una marca valida";
                        string title = "Validacion";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
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
                LoadAll();
                Clear();
            }
            catch (Exception ex)
            {
                string message =  "Ocurrio un error al insertar registro: " +ex.Message;
                string title = "Validacion";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
                throw ex;
            }
        }
        private void Clear()
        {
            nombreText.Text = "";
            descripcionText.Text = "";
            _idModels = 0;
            LoadAll();
        }
        private void Add(int tradeMarkId)
        {
             var model = new Models();
             model.Name = nombreText.Text;
             model.Description = descripcionText.Text;
             model.IdTrademark = tradeMarkId;
             model.Status = "Activo";
             _unitOfWork.ModelsRepository.Add(model);     
        }

        private void Update(string status, int tradeMarkId)
        {
            var model = new Models();
            model.Id = _idModels;
            model.Name = nombreText.Text;
            model.Description = descripcionText.Text;
            model.IdTrademark = tradeMarkId;
            model.Status = status;

            _unitOfWork.ModelsRepository.Update(model);
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
