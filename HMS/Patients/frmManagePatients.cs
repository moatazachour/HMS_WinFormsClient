using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HMS.Patients
{
    public partial class frmManagePatients : Form
    {
        static readonly HttpClient httpClient = new HttpClient();

        public frmManagePatients()
        {
            InitializeComponent();
        }

        private async Task GetAllStudents()
        {
            HttpResponseMessage response = await httpClient.GetAsync("All");

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var patients = JsonConvert.DeserializeObject<List<PatientDTO>>(jsonResponse);

                if (patients != null)
                {
                    dgvListPatients.DataSource = patients;
                }
            }
        }

        private async void frmManagePatients_Load(object sender, EventArgs e)
        {
            httpClient.BaseAddress = new Uri("http://localhost:5282/api/Patients/");

            await GetAllStudents();
        }
    }

    public class PatientDTO
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImagePath { get; set; }

        public PatientDTO(int iD, string fullName, DateTime dateOfBirth, string gender, string phoneNumber,
            string email, string address, string imagePath)
        {
            ID = iD;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
            ImagePath = imagePath;
        }
    }

}
