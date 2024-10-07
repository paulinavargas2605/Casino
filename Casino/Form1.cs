using System.Data;

//Extensión conexion base de datos
using System.Configuration;
using MySql.Data.MySqlClient;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Globalization;

namespace Casino
{
    public partial class frmExportarx18 : Form
    {
        // Variables del form
        string fechaConvertida = "", nit = "", str_NitOperador = "", str_NomRazonSocial = "",
            str_TelRazonSocial = "", str_DirRazonSocial = "", str_Permiso = "", str_NumContrato = "",
            chr_Activo = "", chr_Activo_Maquina= "", fecha = "";
        double num_Id;
        int num_NumMaqContrato;
        DateTime dat_FechaCierre;

        public frmExportarx18()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnImportar_Click(object sender, EventArgs e)
        {
            //Abrir explorador de archivos
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";

            // Si el usuario selecciona un archivo
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                //Tabla inicial, en el datagrid1
                DataTable dataTable = ReadCsvFile(filePath);
                dataGridView1.DataSource = dataTable;
                
                //Tabla credito dividido
                DataTable dataTable2 = ReadCsvFile(filePath);

                dataGridView2.DataSource = dataTable2;
                
                // Agregar nuevas columnas
                dataTable2.Columns.Add("Actualizado", typeof(string));
                dataTable2.Columns.Add("Cierre_Maquina", typeof(string));

                //Dividir columnas
                DividirColumnas(dataTable2);

                //LLamar al metodo que verifica el estado de las maquinas
                MaquinaActiva(dataTable2);

                
                //Fecha obtenida del archivo
                fecha = dataGridView2.Rows[0].Cells[1].Value.ToString();
                //convertir la fecha
                fechaConvertida = $"{fecha.Substring(0, 4)}-{fecha.Substring(4, 2)}-{fecha.Substring(6, 2)}";
                //Mostrar fecha en cuadro de texto
                txtFecha.Text = Convert.ToString(fechaConvertida);

                //LLamar al metodo que verifica el cierre de las maquinas
                CierreMaquina(dataTable2, fechaConvertida);

                //Mostrar nit 
                nit = dataGridView2.Rows[0].Cells[2].Value.ToString();
                txtNit.Text = Convert.ToString(nit);

                Nit(nit);
                if (chr_Activo == "N")
                {
                    MessageBox.Show("El nit no está activo");
                }
                else
                {
                    MessageBox.Show("El nit " + nit + " está activo");
                }

                /*
                Form2 form2 = new Form2(); // Crear una instancia del segundo formulario
                form2.Show(); // Mostrar el formulario de manera no modal
                */
            }
        }

        //Método para saber si la máquina tuvo su cierre en la fecha indicada
        public DataTable CierreMaquina(DataTable dataTable2, string fechaConvertida)
        {
            int filas = dataTable2.Rows.Count;
            int columnas = dataTable2.Columns.Count;

            object[,] matriz = new object[filas, columnas];

            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre la conexión a la base de datos
                    connection.Open();

                    // Verifica si la conexión está abierta
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        //Llenar matriz
                        for (int i = 0; i < filas; i++)
                        {
                            for (int j = 0; j < columnas; j++)
                            {
                                matriz[i, j] = dataTable2.Rows[i][j];
                            }
                        }

                        //Recorre la matriz
                        for (int i = 0; i < filas; i++)
                        {
                            matriz[i, dataTable2.Columns.Count - 1] = "N/A";
                            for (int j = 0; j < columnas; j++)
                            {
                                if (Convert.ToString(matriz[i, j]) == "RD") //Entra a la fila
                                {
                                    if (matriz[i,14] == "S")
                                    {
                                        matriz[i, dataTable2.Columns.Count - 1] = "No existe";
                                        for (int z = 0; z < columnas; z++)
                                        {
                                            if (z == 14) //Si está en la columna 14
                                            {
                                                //Consulta de los datos de tbl_maquina
                                                string query = "SELECT dat_FechaCierre FROM tbl_contadorescierre " +
                                                    "WHERE dat_FechaCierre = '" + fechaConvertida + "'"; 


                                                // Crear el comando MySqlCommand
                                                MySqlCommand command = new MySqlCommand(query, connection);

                                                // Ejecutar el comando y obtener el MySqlDataReader
                                                using (MySqlDataReader reader = command.ExecuteReader())
                                                {
                                                    while (reader.Read()) // Si hay resultados
                                                    {
                                                        dat_FechaCierre = reader.GetDateTime(0);
                                                        //MessageBox.Show("Entramos al if");
                                                        // Convertir las cadenas a DateTime
                                                        DateTime fechaDG = DateTime.Parse(fechaConvertida);
                                                        DateTime fechaBD = dat_FechaCierre;

                                                        // Comparar fechas
                                                        if (fechaDG >= fechaBD)
                                                        {
                                                            matriz[i, dataTable2.Columns.Count - 1] = "S";
                                                        }
                                                        else
                                                        {
                                                            matriz[i, dataTable2.Columns.Count - 1] = "N";
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }


                        for (int i = 0; i < filas; i++)
                        {
                            for (int j = 0; j < columnas; j++)
                            {
                                dataTable2.Rows[i][j] = matriz[i, j]; // Actualizar cada celda del DataTable
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se pudo abrir la conexión.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error de MySQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error general: {ex.Message}");
                }
            }

            return dataTable2;
        }

        //Método para dividir el valor del crédito 
        public DataTable DividirColumnas(DataTable dataTable2)
        {
            int filas = dataTable2.Rows.Count;
            int columnas = dataTable2.Columns.Count;

            object[,] matriz = new object[filas, columnas];

            //Llenar matriz
            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    matriz[i, j] = dataTable2.Rows[i][j];
                }
            }

            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {

                    if (Convert.ToString(matriz[i, j]) == "RD")
                    {
                        for (int z = 4; z <= 9; z++)
                        {
                            double valor1 = Convert.ToDouble(matriz[i, z]);
                            double valor2 = Convert.ToDouble(matriz[i, 13]);

                            matriz[i, z] = valor1 / valor2;

                        }
                    }
                }
            }


            for (int i = 0; i < filas; i++)
            {
                for (int j = 0; j < columnas; j++)
                {
                    dataTable2.Rows[i][j] = matriz[i, j]; // Actualizar cada celda del DataTable
                }
            }
            return dataTable2;
        }

        //Método para saber si la maquina está activa
        private DataTable MaquinaActiva(DataTable dataTable2)
        {
            int filas = dataTable2.Rows.Count;
            int columnas = dataTable2.Columns.Count;

            object[,] matriz = new object[filas, columnas];

            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre la conexión a la base de datos
                    connection.Open();

                    // Verifica si la conexión está abierta
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        //Llenar matriz
                        for (int i = 0; i < filas; i++)
                        {
                            for (int j = 0; j < columnas; j++)
                            {
                                matriz[i, j] = dataTable2.Rows[i][j];
                            }
                        }

                        //Recorre la matriz
                        for (int i = 0; i < filas; i++)
                        {
                            matriz[i, dataTable2.Columns.Count - 2] = "N/A";
                            for (int j = 0; j < columnas; j++)
                            {
                                if (Convert.ToString(matriz[i, j]) == "RD") //Entra a la fila
                                {
                                    for(int z = 0; z < columnas; z++)
                                    {
                                        //maquina activa o no
                                        if (z == 1) //Si está en la columna 1
                                        {
                                            //Consulta de los datos de tbl_maquina
                                            string query = "SELECT chr_Activo FROM tbl_maquina " +
                                               "WHERE `str_Cedula` =" + matriz[i, 1];

                                            // Crear el comando MySqlCommand
                                            MySqlCommand command = new MySqlCommand(query, connection);

                                            // Ejecutar el comando y obtener el MySqlDataReader
                                            using (MySqlDataReader reader = command.ExecuteReader())
                                            {
                                                if (reader.Read()) // Si hay resultados
                                                {
                                                    chr_Activo_Maquina = reader.GetString(0);
                                                    if (chr_Activo_Maquina == "S")
                                                    {
                                                        // Llenar datos en la última columna de la matriz
                                                        matriz[i, dataTable2.Columns.Count - 2] = "S";
                                                    }
                                                    else
                                                    {
                                                        matriz[i, dataTable2.Columns.Count - 2] = "N";
                                                    }
                                                }
                                                else
                                                {
                                                    // Si no hay resultados, asigna "N/A"
                                                    matriz[i, dataTable2.Columns.Count - 2] = "No existe";
                                                }
                                            }
                                        }
                                    }
                                    
                                }
                            }
                        }


                        for (int i = 0; i < filas; i++)
                        {
                            for (int j = 0; j < columnas; j++)
                            {
                                dataTable2.Rows[i][j] = matriz[i, j]; // Actualizar cada celda del DataTable
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("No se pudo abrir la conexión.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error de MySQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error general: {ex.Message}");
                }
            }

            return dataTable2;
        }

        // Método para leer el archivo CSV y cargarlo en un DataTable
        private DataTable ReadCsvFile(string filePath)
        {
            DataTable dataTable = new DataTable();

            // Agregar columnas con títulos personalizados
            dataTable.Columns.Add("Columna 1");  // Columna 1
            dataTable.Columns.Add("Columna 2");    // Columna 2
            dataTable.Columns.Add("Columna 3");  // Columna 3
            dataTable.Columns.Add("Columna 4");  // Columna 4
            dataTable.Columns.Add("Columna 5");    // Columna 5
            dataTable.Columns.Add("Columna 6");  // Columna 6
            dataTable.Columns.Add("Columna 7");  // Columna 7
            dataTable.Columns.Add("Columna 8");    // Columna 8
            dataTable.Columns.Add("Columna 9");  // Columna 9
            dataTable.Columns.Add("Columna 10");  // Columna 10
            dataTable.Columns.Add("Columna 11");  // Columna 11
            dataTable.Columns.Add("Columna 12");  // Columna 12
            dataTable.Columns.Add("Columna 13");  // Columna 13
            dataTable.Columns.Add("Columna 14");  // Columna 14

            // Leer todas las líneas del archivo
            string[] lines = File.ReadAllLines(filePath);

            // Agregar filas al DataTable, omitir la primera si es encabezado en el archivo
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(';'); // Cambiado a ;
                if (values.Length == dataTable.Columns.Count)  // Asegurarse que coincidan el número de columnas
                {
                    dataTable.Rows.Add(values);
                }
            }

            return dataTable;

        }
        private void Nit(string nit)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MiConexion"].ConnectionString;

            // Crear la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abre la conexión a la base de datos
                    connection.Open();

                    // Verifica si la conexión está abierta
                    if (connection.State == System.Data.ConnectionState.Open)
                    {
                        //MessageBox.Show("Conexión exitosa a la base de datos.");

                        //Consulta de los datos razón social
                        // Definir la consulta SQL
                        string query = "SELECT num_Id, str_NitOperador, str_NomRazonSocial, " +
                            "str_TelRazonSocial, str_DirRazonSocial, str_Permiso, str_NumContrato, num_NumMaqContrato, " +
                            "chr_Activo FROM tbl_razonsocial WHERE str_NitOperador = " + nit;

                        // Crear el comando MySqlCommand
                        MySqlCommand command = new MySqlCommand(query, connection);

                        // Ejecutar el comando y obtener el MySqlDataReader
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Leer los resultados
                            while (reader.Read())
                            {
                                num_Id = reader.GetDouble(0); // Obtener el valor de la primera columna
                                str_NitOperador = reader.GetString(1);
                                str_NomRazonSocial = reader.GetString(2);
                                str_TelRazonSocial = reader.GetString(3);
                                str_DirRazonSocial = reader.GetString(4);
                                str_Permiso = reader.GetString(5);
                                str_NumContrato = reader.GetString(6);
                                num_NumMaqContrato = reader.GetInt32(7);
                                chr_Activo = reader.GetString(8);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se pudo abrir la conexión.");
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show($"Error de MySQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error general: {ex.Message}");
                }
            }
        }
    }
}