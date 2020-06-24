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
    public partial class LoginForm : Form
    {
        private int _id = 0;
        private readonly IUnitOfWork _unitOfWork;

        public LoginForm()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["RentCarDb"].ConnectionString;
            _unitOfWork = new UnitOfWork(connectionString);
            InitializeComponent();
        }

       

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtUserName.Text;
            var password = txtUserName.Text;
            var errors = Validattions(username, password);
            string message = "";
            var icon = MessageBoxIcon.Information;
            try
            {
                if (string.IsNullOrEmpty(errors))
                {

                    bool loginSuccesful = Login(username, password);
                    if (loginSuccesful)
                    {
                        message = "Login Correcto";
                    }
                    else
                    {
                        message = "Login Incorrecto";
                        icon = MessageBoxIcon.Warning;
                    }

                    string title = "Información";
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    MessageBox.Show(message, title, buttons, icon);
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
                string errorMessage = "Ocurrio un error al intentar loguearse: " + ex.Message;
                string title = "Informacion";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBox.Show(errorMessage, title, buttons, MessageBoxIcon.Error);
            }
          
           
        }

        private bool Login(string username, string password)
        {
            return _unitOfWork.AuthRepository.Login(username, password);
        }

        private void Clear()
        {
            txtPassword.Text = "";
            txtUserName.Text = "";
        }
        private string Validattions(string username, string password)
        {
            string response = "";
            if (string.IsNullOrEmpty(username))
            {
                response += string.Format("- Username es un campo requerido {0}", Environment.NewLine);
            }
            if (string.IsNullOrEmpty(password))
            {
                response += string.Format("- Password es un campo requerido {0}", Environment.NewLine);
            }

            return response;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm register = new RegisterForm();
            register.Show();
            this.Hide();
        }
    }
}
