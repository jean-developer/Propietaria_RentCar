using Propietaria.RentCar.Core.Entities;
using Propietaria.RentCar.Infrastructure.Dapper.Query.Auth;
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

namespace Propietaria.RentCar.UI.Auth
{
    public partial class RegisterForm : Form
    {
        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;

        public RegisterForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

        private bool Register(RentCar.Core.Entities.User model)
        {
            var isRegister = _unitOfWork.AuthRepository.Register(model);

            return isRegister;

        }

        private void Register_Load(object sender, EventArgs e)
        {
          
          
        }
        private string Validators(RentCar.Core.Entities.User model) 
        {
             
            string response = "";
            GetUserByUsername repository = new GetUserByUsername();
            var userWasCreated = repository.Get(model.UserName);
            if (string.IsNullOrEmpty(model.UserName))
            {
                response += string.Format("- 'Nombre de usuario' es un campo requerido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(model.Password))
            {
                response += string.Format("- 'Contraseña' es un campo requerido {0}", Environment.NewLine);
            }
            if (userWasCreated != null)
            {
                response += string.Format("- 'Nombre de usuario' ya esta en uso {0}", Environment.NewLine);
            }

            return response;
        }

        private void Clear()
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                RentCar.Core.Entities.User model = new RentCar.Core.Entities.User();
                model.Estado = "Activo";
                model.UserName = txtUserName.Text;
                model.Password = txtPassword.Text;
                string errors = Validators(model);

                if (string.IsNullOrEmpty(errors))
                {
                    var isRegistered = Register(model);
                    if (isRegistered)
                    {
                        _unitOfWork.Commit();
                        Clear();
                        string title = "Información";
                        string message = "Registrado correctamente";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);

                    }
                    else
                    {
                        string title = "Validacion";
                        MessageBoxButtons buttons = MessageBoxButtons.OK;
                        MessageBox.Show("Ocurrio un error al registrar registro: es posible que el Nombre de usuario este en uso", title, buttons, MessageBoxIcon.Warning);
                    }

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
                string errorMessage = "Ocurrio un error al insertar registro: " + ex.Message;
                string title = "Informacion";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, title, buttons, MessageBoxIcon.Error);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
    }
}
