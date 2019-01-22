using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace kasir_laundry_fix
{
    public partial class Form1 : Form
    {
        MySqlConnection koneksi = new MySqlConnection("SERVER=localhost;DATABASE=laundry;UID=root;PASSWORD=;");

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            LoadData();
            koneksi.Close();
        }

        public void LoadData()
        {
            MySqlCommand command;
            command = koneksi.CreateCommand();

            command.CommandText = "select * from pelanggan,transaksi where pelanggan.id_pelanggan=transaksi.id_pelanggan";

            MySqlDataAdapter adapater = new MySqlDataAdapter(command);
            DataSet dataset = new DataSet();
            adapater.Fill(dataset);
            dataGridView1.DataSource = dataset.Tables[0].DefaultView;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        public void tabPage1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();

                MySqlCommand command;
                command = koneksi.CreateCommand();

                command.CommandText = "insert into pelanggan(nama,no_hp,alamat) values(@nama,@no_hp,@alamat);";

                command.Parameters.AddWithValue("@nama", textBox2.Text);
                command.Parameters.AddWithValue("@no_hp", textBox3.Text);
                command.Parameters.AddWithValue("@alamat", textBox4.Text);

                MessageBox.Show("data berhasil disimpan!");

                command.ExecuteNonQuery();

                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";

                LoadData();

                koneksi.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "message error!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            koneksi.Open();

            MySqlCommand command;
            command = koneksi.CreateCommand();

            command.CommandText = "insert into transaksi(tanggal_masuk,tanggal_keluar,berat_pakaian,jumlah_pakaian,biaya,id_pelanggan) values(@tanggal_masuk,@tanggal_keluar,@berat_pakaian,@jumlah_pakaian,@biaya,@id_pelanggan);";

            int harga_satuan = 5000;
            int berat_pakaian = int.Parse(textBox8.Text);
            int biaya;

            biaya = harga_satuan * berat_pakaian;

            command.Parameters.AddWithValue("@tanggal_masuk", dateTimePicker1.Value.Date.ToString("yyyy/MM/dd"));
            command.Parameters.AddWithValue("@tanggal_keluar", dateTimePicker2.Value.Date.ToString("yyyy/MM/dd"));
            command.Parameters.AddWithValue("@berat_pakaian", textBox8.Text);
            command.Parameters.AddWithValue("@jumlah_pakaian", textBox7.Text);
            command.Parameters.AddWithValue("@biaya", textBox9.Text=biaya.ToString());
            command.Parameters.AddWithValue("@id_pelanggan", textBox1.Text);            
            
            MessageBox.Show("data berhasil disimpan\n"+"Biaya laundry = "+biaya);

            command.ExecuteNonQuery();

            textBox8.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";

            LoadData();

            koneksi.Close();
        }

        private void buttonrefresh_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonhapus_Click(object sender, EventArgs e)
        {
            try
            {
                koneksi.Open();
                MySqlCommand command;
                command = koneksi.CreateCommand();

                command.CommandText = "delete from table pelanggan where name'" + "@nama" + "'";
                command.ExecuteNonQuery();
                LoadData();
                koneksi.Close();
                MessageBox.Show("Data " + "@nama" + " berhasil dihapus");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "message error!");
            }

        }
        

        
    }
}
