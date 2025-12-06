namespace feederdikti_importer
{
    using MySql.Data.MySqlClient;
    using Mysqlx.Crud;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Design;
    using System.Net.Http;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using OfficeOpenXml;

    public partial class formImporter : Form
    {
        #region Public Property
        public string DbString { get; set; }
        public string ApiToken { get; set; }
        public int dbConTimeout { get; set; }
        public int httpClientTimeOut { get; set; }
        public int TotalDownloadData { get; set; }
        public List<string[]> DataProdi { get; set; } = new List<string[]>();
        public List<string[]> DataTahun { get; set; } = new List<string[]>();
        public List<string[]> DataApiAction { get; set; } = new List<string[]>();

        #endregion

        #region Private Property

        private static readonly HttpClient client;

        #endregion

        #region Class Item

        public class ProdiItem
        {
            public string Name { get; set; }
            public string Id { get; set; }
            public string ShortName { get; set; }
            public string KodeNIM { get; set; }
            public string JenjangPendidikan { get; set; }
        }

        public class TahunItem
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }

        public class ApiActionItem
        {
            public string ApiAction { get; set; }
            public string TabelName { get; set; }
            public string Filter { get; set; }
            public string IsSeparateDatabase { get; set; }
        }

        #endregion

        #region Load

        public formImporter()
        {
            InitializeComponent();
        }

        static formImporter()
        {
            client = new HttpClient();
        }

        private async void formImporter_Load(object sender, EventArgs e)
        {
            // await SetupConfigFromFile().ConfigureAwait(false);

            LoadProdi();
            LoadApiAction();
        }

        private void LoadProdi()
        {
            DataProdi.Clear();

            DataProdi.Add(new[] { "All", "all", "xxx", "all", "all" });
            DataProdi.Add(new[] { "Akuntansi", "d4dc894c-579b-4c35-905d-6357f40d0bdd", "Akuntansi", "19", "S1" });
            DataProdi.Add(new[] { "Biologi", "842f16ff-34e0-439a-8ad2-294fd9ad9f5d", "Biologi", "15", "S1" });
            DataProdi.Add(new[] { "Ilmu Hukum", "3d83dfc5-49fc-42f7-bde7-a07d47400211", "IlmuHukum", "31", "S1" });
            DataProdi.Add(new[] { "Ilmu Pendidikan Teologi", "e0932392-2931-46ca-8ccd-e0f9911f222e", "IPT", "11", "S1" });
            DataProdi.Add(new[] { "Kepemimpinan Kristen", "5b393472-77ea-4cca-bc60-255c7a1c86eb", "KKristen", "22", "S2" });
            DataProdi.Add(new[] { "Manajemen", "745c457c-0a9a-4abb-b488-208495457419", "Manajemen", "41", "S1" });
            DataProdi.Add(new[] { "Manajemen Sumber Daya Perairan", "840673b4-f17f-4544-a9cc-a4e1ac6ed8d0", "MSDP", "38", "S1" });
            DataProdi.Add(new[] { "Mekanisasi Pertanian", "70d9a750-18b0-47a2-98a3-123497fa6aec", "MP", "52", "S1" });
            DataProdi.Add(new[] { "Pendidikan Bahasa Inggris", "742e9a9e-46a2-4fa1-b31f-2cbde7b87bd5", "PBI", "13", "S1" });
            DataProdi.Add(new[] { "Pendidikan Biologi", "6db9a85c-9b43-47ef-aff9-59217875c7e7", "PendBiologi", "15", "S1" });
            DataProdi.Add(new[] { "Pendidikan Jasmani, Kesehatan dan Rekreasi", "25c6438b-9323-4c87-8266-d0087a2d2f26", "PJKR", "32", "S1" });
            DataProdi.Add(new[] { "Sumber Daya Akuatik", "667a5410-0738-4b08-b9d6-22162606b2e4", "SDA", "53", "S2" });
            DataProdi.Add(new[] { "Teknologi Hasil Perikanan", "b6b022a1-926f-4976-8e45-7ec2f05065b6", "THPerikanan", "39", "S1" });
            DataProdi.Add(new[] { "Teknologi Hasil Pertanian", "00944eda-002b-4e1d-89e7-4005a6eed8d4", "THPertanian", "51", "S1" });
            DataProdi.Add(new[] { "Teologi", "3b429372-393e-4085-99d7-11be20508aca", "Teologi", "77", "S2" });
            DataProdi.Add(new[] { "Teologi Agama Kristen", "17046fb5-34d1-467b-9193-4bda68880239", "TAK", "21", "S1" });

            var list = DataProdi.Where(a => a?.Length >= 3)
                .Select(a => new ProdiItem { Name = a[0], Id = a[1], ShortName = a[2], KodeNIM = a[3], JenjangPendidikan = a[4] })
                .ToList();

            cmbProdi.DataSource = null;
            cmbProdi.DisplayMember = nameof(ProdiItem.Name);
            cmbProdi.ValueMember = nameof(ProdiItem.Id);
            cmbProdi.DataSource = list;
        }

        private void LoadApiAction()
        {
            DataApiAction.Clear();
            var filter = "";
            var filterSemester = ", \"filter\" : \"id_prodi = '" + "[idProdiValue]" + "' AND id_semester = '" + "[taValue]" + "[semValue]'\"";
            var filterPeriode = ", \"filter\" : \"id_prodi = '" + "[idProdiValue]" + "' AND id_periode = '" + "[taValue]" + "[semValue]'\"";
            var filterProdi = ", \"filter\" : \"id_prodi = '" + "[idProdiValue]'\"";
            var filterSmt = ", \"filter\" : \"nama_prodi = '" + "[jenjangValue] [namaProdiValue]" + "' AND id_smt = '" + "[taValue]" + "[semValue]'\"";
            var filterAngkatan = ", \"filter\" : \"angkatan = '" + "[taValue]" + "'\"";
            var filterIdRegistrasiMahasiswa = ", \"filter\" : \"id_registrasi_mahasiswa = '" + "[idRegMhsValue]'\"";

            // done
            //DataApiAction.Add(new[] { "ProfilPT", "ProfilPT", filter, "N" });
            //DataApiAction.Add(new[] { "BiodataMahasiswa", "BiodataMahasiswa", filter, "N" });
            //DataApiAction.Add(new[] { "ListMahasiswa", "ListMahasiswa", filter, "N" });
            //DataApiAction.Add(new[] { "DetailBiodataDosen", "DetailBiodataDosen", filter, "N" });

            //DataApiAction.Add(new[] { "DetailNilaiPerkuliahanKelas", "DetailNilaiPerkuliahanKelas", filterSemester, "Y" });
            //DataApiAction.Add(new[] { "KRSMahasiswa", "KRSMahasiswa", filterPeriode, "Y" });
            //DataApiAction.Add(new[] { "RiwayatNilaiMahasiswa", "RiwayatNilaiMahasiswa", filterPeriode, "Y" });
            //DataApiAction.Add(new[] { "ListPerkuliahanMahasiswa", "ListPerkuliahanMahasiswa", filterSemester, "Y" });
            //DataApiAction.Add(new[] { "NilaiTransferPendidikanMahasiswa", "NilaiTransferPendidikanMahasiswa", filterSemester, "Y" });
            //DataApiAction.Add(new[] { "PesertaKelasKuliah", "PesertaKelasKuliah", filterAngkatan, "Y" });
            //DataApiAction.Add(new[] { "RekapIPSMahasiswa", "RekapIPSMahasiswa", filterProdi, "Y" });
            //DataApiAction.Add(new[] { "RekapKHSMahasiswa", "RekapKHSMahasiswa", filterPeriode, "Y" });
            //DataApiAction.Add(new[] { "RekapKRSMahasiswa", "RekapKRSMahasiswa", filterPeriode, "Y" });

            // no need?
            //DataApiAction.Add(new[] { "ListNilaiPerkuliahanKelas", "ListNilaiPerkuliahanKelas", filterSmt, "Y" });

            // error dari api
            // DataApiAction.Add(new[] { "RekapLaporan", "RekapLaporan", filterSemester, "Y" });

            DataApiAction.Add(new[] { "TranskripMahasiswa", "TranskripMahasiswa", filterIdRegistrasiMahasiswa, "Y" });

            var list = DataApiAction.Where(a => a?.Length >= 3)
                .Select(a => new ApiActionItem { ApiAction = a[0], TabelName = a[1], Filter = a[2], IsSeparateDatabase = a[3] }).ToList();

            cmbApiAction.DataSource = null;
            cmbApiAction.DisplayMember = nameof(ApiActionItem.ApiAction);
            cmbApiAction.ValueMember = nameof(ApiActionItem.TabelName);
            cmbApiAction.DataSource = list;
        }

        #endregion

        #region Event

        private void cmbProdi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbApiAction.SelectedValue != null && cmbApiAction.SelectedItem is ApiActionItem actionItem)
            {
                cbxSeparateDatabase.Checked = actionItem.IsSeparateDatabase == "Y";
                cbxDropAndCreate.Checked = true;

                txtApiAdditionalFilter.Text = actionItem.Filter;

                if (cbxSeparateDatabase.Checked)
                {
                    if (cmbProdi.SelectedValue != null && cmbProdi.SelectedItem is ProdiItem prodiItem)
                    {
                        txtDestTableName.Text = "FeederRawData_" + actionItem.TabelName + "_" + prodiItem.ShortName + "_[tahun]";
                    }
                }
                else
                {
                    txtDestTableName.Text = "FeederRawData_" + actionItem.TabelName;
                }

                cmbProdi.Enabled = actionItem.IsSeparateDatabase == "Y";
                if (actionItem.IsSeparateDatabase != "Y")
                {
                    cmbProdi.SelectedIndex = 0;
                }
            }
        }

        private void cmbApiAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDestTableName.Enabled = false;
            txtApiAdditionalFilter.Enabled = false;
            cbxDropAndCreate.Enabled = false;
            cbxSeparateDatabase.Enabled = false;

            if (cmbApiAction.SelectedValue != null && cmbApiAction.SelectedItem is ApiActionItem actionItem)
            {
                cbxSeparateDatabase.Checked = actionItem.IsSeparateDatabase == "Y";
                cbxDropAndCreate.Checked = true;

                txtApiAdditionalFilter.Text = actionItem.Filter;

                if (cbxSeparateDatabase.Checked)
                {
                    if (cmbProdi.SelectedValue != null && cmbProdi.SelectedItem is ProdiItem prodiItem)
                    {
                        txtDestTableName.Text = "FeederRawData_" + actionItem.TabelName + "_" + prodiItem.ShortName + "_[tahun]";
                    }
                }
                else
                {
                    txtDestTableName.Text = "FeederRawData_" + actionItem.TabelName;
                }

                cmbProdi.Enabled = actionItem.IsSeparateDatabase == "Y";
                if (actionItem.IsSeparateDatabase != "Y")
                {
                    cmbProdi.SelectedIndex = 0;
                }
            }
        }

        private void cbxSeparateDatabase_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSeparateDatabase.Checked && (cmbApiAction.SelectedValue != null && cmbApiAction.SelectedItem is ApiActionItem item))
            {
                txtDBName.Text = "db_importer_feeder_" + item.TabelName;
            }
            else
            {
                txtDBName.Text = "db_importer_feeder";
            }
        }

        #endregion

        #region Button Event

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void StartRun()
        {
            btnRun.Enabled = false;
            progressBar.Visible = true;
            progressBar.Value = 1;
            txtDestTableName.Enabled = false;

            printLog();
            printLog("-----------------------------------");
            printLog("START RUN IMPORT");
            printLog();

            TotalDownloadData = 0;
        }

        private void EndRun()
        {
            btnRun.Enabled = true;
            progressBar.Value = 100;
            btnRun.Text = "Run";
            txtDestTableName.Enabled = true;

            printLog();
            printLog("END RUN IMPORT");
            printLog("-----------------------------------");
        }

        private async void btnRun_Click(object sender, EventArgs e)
        {
            MySqlConnection connection = null;
            txtLog.Clear();

            try
            {
                //await SaveConfigAsync(new AppConfig()
                //{
                //    DbAddress = txtDbAddress.Text,
                //    DbPort = txtDbPort.Text,
                //    DbUsername = txtDbUserName.Text,
                //    DbPassword = txtDbPassword.Text,
                //    DbName = txtDBName.Text,

                //    ApiAddress = txtApiAddress.Text,
                //    ApiUsername = txtApiUserName.Text,
                //    ApiPassword = txtApiPassword.Text,
                //    ApiAction = txtApiAction.Text,
                //    ApiAdditionalParam = txtApiAdditionalFilter.Text,
                //    DestTableName = txtDestTableName.Text,

                //    HttpClientTimeoutInMinutes = httpClientTimeOut,
                //    MySQLConnectionTimeoutInSecond = dbConTimeout
                //});
                //printLog($"'{ConfigFileName}' has been updated.");

                DbString = GetDbString();
                printLog("DB String : " + DbString);
                progressBar.Value = 10;

                connection = new MySqlConnection(DbString);
                CheckConnection(connection);
                progressBar.Value = 20;

                ApiToken = await GetToken(txtApiAddress.Text, txtApiUserName.Text, txtApiPassword.Text);
                printLog("API Token : " + ApiToken);
                progressBar.Value = 30;


                StartRun();
                if (cmbApiAction.SelectedItem is ApiActionItem selectedApiActionItem)
                {
                    connection.Open();
                    printLog("  Database connection open.");

                    var action = selectedApiActionItem.ApiAction.Replace(" ", "");
                    printLog("      Start Import : " + action);

                    var apiAction = action == "DetailBiodataDosen" ? action : "Get" + action;
                    string destTable = "FeederRawData_" + action;
                    txtDestTableName.Text = destTable;

                    if (!string.Equals(selectedApiActionItem.IsSeparateDatabase, "Y"))
                    {
                        await runImport(connection, apiAction, destTable, selectedApiActionItem.Filter, 1);
                    }
                    else
                    {
                        if (cmbProdi.SelectedItem is ProdiItem selectedProdi)
                        {
                            if (selectedProdi.Name == "All" && action != "PesertaKelasKuliah")
                            {
                                // import per Prodi
                                foreach (var prodi in DataProdi)
                                {
                                    var prodiName = prodi[0];
                                    var idProdi = prodi[1];
                                    var shortName = prodi[2];
                                    var jenjang = prodi[4];

                                    if (prodiName != "All")
                                    {
                                        if (action == "TranskripMahasiswa")
                                        {
                                            await ImportByFilterReadFile(connection, apiAction, destTable, prodiName, shortName, selectedApiActionItem.Filter);
                                        }
                                        else
                                        {
                                            await attemptRunImportPerYear(destTable, shortName, idProdi, prodiName, jenjang
                                            , selectedApiActionItem, connection, apiAction);
                                        }
                                    }
                                }

                            }
                            else
                            {
                                var prodiName = selectedProdi.Name;
                                var idProdi = selectedProdi.Id;
                                var shortName = selectedProdi.ShortName;
                                var jenjang = selectedProdi.JenjangPendidikan;

                                if (action == "TranskripMahasiswa")
                                {
                                    await ImportByFilterReadFile(connection, apiAction, destTable, prodiName, shortName, selectedApiActionItem.Filter);
                                }
                                else
                                {
                                    await attemptRunImportPerYear(destTable, shortName, idProdi, prodiName, jenjang
                                    , selectedApiActionItem, connection, apiAction);
                                }
                            }

                        }
                    }

                    printLog("      End Import : " + action);
                }
            }
            catch (Exception ex)
            {
                printLog("Exception : \r\n" + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    printLog("  Database connection closed.");
                }

                EndRun();
            }
        }

        #endregion

        #region Import
        private async Task<bool> attemptRunImportPerYear(
            string destTable, string shortName, string idProdi, string prodiName, string jenjang,
            ApiActionItem selectedApiActionItem, MySqlConnection connection, string apiAction)
        {
            printLog("          Program Studi : " + prodiName);

            var destTablePerProdi = destTable + "_" + shortName + "_[tahun]";
            var paramFilterProdi = selectedApiActionItem.Filter
                .Replace("[idProdiValue]", idProdi)
                .Replace("[namaProdiValue]", prodiName)
                .Replace("[jenjangValue]", jenjang);

            if (apiAction == "GetRekapIPSMahasiswa")
            {
                printLog("              Tahun : " + prodiName + " All");
                var destTablePerProdiTahun = destTablePerProdi.Replace("_[tahun]", string.Empty);
                txtDestTableName.Text = destTablePerProdiTahun;
                await runImport(connection, apiAction, destTablePerProdiTahun, paramFilterProdi, 1);
            }
            else
            {
                for (int tahun = 2025; tahun <= 2025; tahun++)
                {
                    printLog("              Tahun : " + prodiName + " " + tahun);
                    var destTablePerProdiTahun = destTablePerProdi.Replace("[tahun]", tahun.ToString());
                    var paramFilterProdiTahun = paramFilterProdi.Replace("[taValue]", tahun.ToString());
                    txtDestTableName.Text = destTablePerProdiTahun;

                    if (apiAction == "GetPesertaKelasKuliah")
                    {
                        // limit 8000
                        await runImport(connection, apiAction, destTablePerProdiTahun, paramFilterProdiTahun, 1);
                    }
                    else
                    {
                        int[] semesterList = { 1, 2, 3 };
                        for (int semester = 1; semester <= semesterList.Length; semester++)
                        {
                            printLog("                  Semester " + semester);
                            var paramFilterProdiTahunSem = paramFilterProdiTahun.Replace("[semValue]", semester.ToString());
                            await runImport(connection, apiAction, destTablePerProdiTahun, paramFilterProdiTahunSem, semester);
                        }
                    }
                }
            }

            return true;
        }

        private async Task<bool> runImport(
            MySqlConnection connection,
            string apiAction, string destTable, string paramFilter, int currentSemester)
        {
            var apiAddress = txtApiAddress.Text;
            var apiUserName = txtApiUserName.Text;
            var apiPassword = txtApiPassword.Text;

            // setup filter and limit
            var limitText = txtAdditionalLimit.Text.Replace(" ", "");
            if (String.IsNullOrEmpty(limitText)) { limitText = "4000"; }
            var paramLimitFormat = ", \"limit\": " + limitText + ", \"offset\": [offsetValue]";
            int limit = int.Parse(limitText);

            using var cmd = connection.CreateCommand();

            int batch = 1;
            bool runImport = true;
            do
            {

                printLog("                      BATCH " + batch);
                int offset = (batch - 1) * limit;
                string paramLimit = paramLimitFormat.Replace("[offsetValue]", offset.ToString());

                // 1. get json data
                var jsonData = await GetData(apiAddress, apiAction, ApiToken, paramFilter, paramLimit);
                if (jsonData.Count == 0)
                {
                    printLog("                          No Data returned");
                    runImport = false;
                    return true;
                }

                // 2. convert json into insert
                StringBuilder sqlInsert = new StringBuilder();
                sqlInsert.AppendLine("");
                sqlInsert.AppendLine(ConvertJsonToMySqlInsert(jsonData.First().GetRawText(), destTable));
                foreach (var item in jsonData.Skip(1))
                    sqlInsert.AppendLine(ConvertJsonToMySqlInsert(item.GetRawText()));
                sqlInsert.AppendLine(";");
                progressBar.Value = 40;

                // 3. create new table
                if (currentSemester == 1 && batch == 1)
                {
                    printLog("                          Create New Table = YES");
                    var sqlSchema = GenerateTableSchemaFromJson(jsonData.First().GetRawText(), destTable);
                    MySqlCommand cmd2 = new MySqlCommand(sqlSchema.ToString(), connection);
                    cmd2.CommandTimeout = dbConTimeout; cmd2.ExecuteNonQuery();
                    progressBar.Value = 50;
                }
                else
                {
                    printLog("                          Create New Table = NO");
                }
                progressBar.Value = 60;

                //4. Execute Insert Table
                printLog("                          Process to insert data ...");
                cmd.CommandText = sqlInsert.ToString();
                cmd.CommandTimeout = dbConTimeout;
                int rowsAffected = cmd.ExecuteNonQuery();

                printLog("                          Success insert " + rowsAffected + " data.");
                TotalDownloadData = TotalDownloadData + rowsAffected;
                progressBar.Value = 80;

                batch++;
            } while (runImport);

            return true;
        }

        private string GetDbString()
        {
            var defaultMySQLPort = "3306";
            return $"Server={txtDbAddress.Text};Port={(System.String.IsNullOrWhiteSpace(txtDbPort.Text) ? defaultMySQLPort : txtDbPort.Text)};Database={txtDBName.Text};Uid={txtDbUserName.Text};Pwd={txtDbPassword.Text};AllowLoadLocalInfile=true;";
        }

        private bool CheckConnection(MySqlConnection connection)
        {
            try
            {
                connection.Open();
                printLog("Connection successful!");
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception($"Connection failed! Error: {ex.Message}");
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    printLog("Done Test Connetion.");
                }
            }
        }

        private bool TableExists(MySqlConnection connection, string tableName)
        {
            try
            {
                string query = $"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = DATABASE() AND TABLE_NAME = '{tableName}'";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.CommandTimeout = dbConTimeout;

                object result = cmd.ExecuteScalar();
                return result != null;
            }
            catch (Exception ex)
            {
                printLog($"Error checking table existence: {ex.Message}");
                return false;
            }
        }

        public async Task<string> GetToken(string url, string username, string password)
        {
            string jsonPayload = $"{{\"act\":\"GetToken\",\"username\":\"{username}\",\"password\":\"{password}\"}}";
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var responseBody = "";

            try
            {
                // Send the POST request
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Ensure the response was successful (status code 200-299)
                response.EnsureSuccessStatusCode();

                // Read the response body as a string
                responseBody = await response.Content.ReadAsStringAsync();

                // Use JsonDocument to parse the response string
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    // Navigate to the "data" object and then get the "token" property
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("data", out JsonElement dataElement) &&
                        dataElement.TryGetProperty("token", out JsonElement tokenElement))
                    {
                        // Return the token as a string
                        return tokenElement.GetString();
                    }
                }
            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Request error: {e.Message}");
            }
            catch (JsonException e)
            {
                throw new Exception($"JSON parsing error: {e.Message}");
            }
            catch (Exception e)
            {
                throw new Exception($"Response : {responseBody} {Environment.NewLine}GetToken Exception : {e.Message}");
            }

            return null;

        }

        public async Task<List<JsonElement>> GetData(string url, string act, string token, string paramFilter = "", string paramLimitOffer = "")
        {
            var requestData = new { act = act, token = token };

            string jsonPayload = JsonSerializer.Serialize(requestData);

            jsonPayload = jsonPayload.TrimEnd('}') + paramFilter + paramLimitOffer + "}";

            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            try
            {
                // Send the POST request to the specified URL
                HttpResponseMessage response = await client.PostAsync(url, content);

                // Throw an exception if the response status code is not a success code (200-299)
                response.EnsureSuccessStatusCode();

                // Read the entire response body as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Use JsonDocument to find the "data" element
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    if (doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                    {
                        // Deserialize the "data" element into a list of JsonElements
                        var dataList = JsonSerializer.Deserialize<List<JsonElement>>(dataElement.GetRawText());
                        return dataList;
                    }
                }

            }
            catch (HttpRequestException e)
            {
                throw new Exception($"Request error: {e.Message}");
            }
            return new List<JsonElement>();
        }

        public string ConvertJsonToMySqlInsert(string jsonString, string tableName)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                        throw new Exception("Error: The JSON root must be an object.");

                    bool isFirstProperty = true;
                    foreach (JsonProperty property in doc.RootElement.EnumerateObject())
                    {
                        if (!isFirstProperty)
                        {
                            columns.Append(", ");
                            values.Append(", ");
                        }

                        columns.Append($"`{property.Name}`");

                        // Handle different JSON value kinds to format the SQL value correctly.
                        string value = property.Value.ValueKind switch
                        {
                            // Strings need to be single-quoted and escaped.
                            JsonValueKind.String => $"'{property.Value.GetString()?.Replace("'", "''")}'",
                            JsonValueKind.True => "'TRUE'",
                            JsonValueKind.False => "'FALSE'",
                            JsonValueKind.Null => "''",
                            _ => $"'{property.Value.GetRawText()?.Replace("'", "''")}'"
                        };

                        values.Append(value);

                        isFirstProperty = false;
                    }
                }
            }
            catch (JsonException ex)
            {
                throw new Exception($"JSON parsing error: {ex.Message}");
            }

            // Construct the final INSERT query string.
            if (columns.Length == 0)
            {
                throw new Exception("Error: The JSON object contains no properties.");
            }

            return $"INSERT INTO `{tableName}` ({columns.ToString()}) VALUES ({values.ToString()})";
        }

        public string ConvertJsonToMySqlInsert(string jsonString)
        {
            StringBuilder columns = new StringBuilder();
            StringBuilder values = new StringBuilder();

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                    {
                        txtLog.AppendText(Environment.NewLine);
                        txtLog.AppendText("Error: The JSON root must be an object.");
                        return string.Empty;
                    }

                    bool isFirstProperty = true;
                    foreach (JsonProperty property in doc.RootElement.EnumerateObject())
                    {
                        // Add a comma separator for all but the first column and value.
                        if (!isFirstProperty)
                        {
                            columns.Append(", ");
                            values.Append(", ");
                        }

                        columns.Append($"`{property.Name}`");

                        // Handle different JSON value kinds to format the SQL value correctly.
                        string value = property.Value.ValueKind switch
                        {
                            // Strings need to be single-quoted and escaped.
                            JsonValueKind.String => $"'{property.Value.GetString()?.Replace("'", "''")}'",
                            JsonValueKind.True => "'TRUE'",
                            JsonValueKind.False => "'FALSE'",
                            JsonValueKind.Null => "''",
                            _ => $"'{property.Value.GetRawText()?.Replace("'", "''")}'"
                        };

                        values.Append(value);

                        isFirstProperty = false;
                    }
                }
            }
            catch (JsonException ex)
            {
                throw new Exception($"JSON parsing error: {ex.Message}");
            }

            // Construct the final INSERT query string.
            if (columns.Length == 0)
            {
                throw new Exception("Error: The JSON object contains no properties.");
            }

            return $",({values.ToString()})";
        }

        public string GenerateTableSchemaFromJson(string jsonString, string tableName)
        {
            if (string.IsNullOrEmpty(jsonString) || string.IsNullOrEmpty(tableName))
                throw new ArgumentException("JSON string and table name cannot be null or empty.");


            StringBuilder queryBuilder = new StringBuilder();

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonString))
                {
                    // The provided JSON must be a single object.
                    if (doc.RootElement.ValueKind != JsonValueKind.Object)
                        throw new JsonException("The root element of the JSON string must be an object.");

                    // Drop the existing table if it exists.
                    queryBuilder.AppendLine($"DROP TABLE IF EXISTS `{tableName}`;");

                    // Start the CREATE TABLE statement.
                    queryBuilder.AppendLine($"CREATE TABLE {tableName} (");

                    var columns = new List<string>();

                    foreach (JsonProperty property in doc.RootElement.EnumerateObject())
                    {
                        string columnName = property.Name;
                        string columnDefinition = $"{columnName} TEXT NULL";
                        columns.Add(columnDefinition);
                    }

                    queryBuilder.AppendLine(string.Join(",\n", columns));
                    queryBuilder.AppendLine(");");
                }
            }
            catch (JsonException ex)
            {
                throw new Exception($"JSON parsing error: {ex.Message}");
            }

            return queryBuilder.ToString();
        }

        public void ExecuteMySqlQuery(string sqlQuery, string connectionString)
        {
            if (string.IsNullOrEmpty(sqlQuery)) throw new Exception("Error: The SQL query string cannot be null or empty.");

            if (string.IsNullOrEmpty(connectionString)) throw new Exception("Error: The connection string cannot be null or empty.");

            MySqlConnection connection = null;

            try
            {
                connection = new MySqlConnection(connectionString);
                connection.Open();
                MySqlCommand cmd = new MySqlCommand(sqlQuery, connection);
                cmd.CommandTimeout = dbConTimeout;

                int rowsAffected = cmd.ExecuteNonQuery();

                txtLog.AppendText($"Query executed successfully. Rows affected: {rowsAffected}");
                txtLog.AppendText(Environment.NewLine);
            }
            catch (MySqlException ex)
            {
                throw new Exception($"MySQL Error: {ex.Message} {Environment.NewLine} Stack Trace: {ex.StackTrace}");
            }
            catch (Exception ex)
            {
                throw new Exception($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                    txtLog.AppendText("Database connection closed.");
                    txtLog.AppendText(Environment.NewLine);
                }
            }
        }

        #endregion Import

        #region Helper

        private async Task SetupConfigFromFile()
        {
            var config = await GetConfigAsync();

            txtDbAddress.Text = config.DbAddress;
            txtDbPort.Text = config.DbPort;
            txtDbUserName.Text = config.DbUsername;
            txtDbPassword.Text = config.DbPassword;
            txtDBName.Text = config.DbName;

            txtApiAddress.Text = config.ApiAddress;
            txtApiUserName.Text = config.ApiUsername;
            txtApiPassword.Text = config.ApiPassword;
            //txtApiAction.Text = config.ApiAction;
            txtApiAdditionalFilter.Text = config.ApiAdditionalParam;
            txtDestTableName.Text = config.DestTableName;

            httpClientTimeOut = config.HttpClientTimeoutInMinutes;
            client.Timeout = TimeSpan.FromMinutes(httpClientTimeOut);
            dbConTimeout = config.MySQLConnectionTimeoutInSecond;
        }

        private void printLog(string message = "")
        {
            txtLog.AppendText(message);
            txtLog.AppendText(Environment.NewLine);
        }

        #endregion

        #region Read File

        private async Task ImportByFilterReadFile(MySqlConnection connection,
            string apiAction, string destTable, string prodiName, string shortName, string paramFilter)
        {
            destTable = destTable + "_" + shortName;
            string todoFile = prodiName.Replace(" ", "") + "_ltemReg_todo.txt";
            string doneFile = prodiName.Replace(" ", "") + "_ltemReg_done.txt";
            string failedFile = prodiName.Replace(" ", "") + "_ltemReg_failed.txt";
            printLog("          Read file " + todoFile);

            // Get the project root directory (parent of bin folder)
            var baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            var projectRoot = baseDir.Parent?.Parent?.Parent?.FullName ?? AppDomain.CurrentDomain.BaseDirectory;
            string todoFolder = Path.Combine(projectRoot, "DataTodo");
            string doneFolder = Path.Combine(projectRoot, "DataDone");
            string failedFolder = Path.Combine(projectRoot, "DataFailed");

            string todoPath = Path.Combine(todoFolder, todoFile);
            string donePath = Path.Combine(doneFolder, doneFile);
            string failedPath = Path.Combine(failedFolder, failedFile);

            //printLog($"Looking for file at: {todoPath}");

            List<string> allLines = new List<string>();
            int limit = int.Parse(txtAdditionalLimit.Text);
            try
            {
                if (!File.Exists(todoPath))
                {
                    printLog("Todo file not found: " + todoPath);
                    return;
                }

                allLines = File.ReadAllLines(todoPath).ToList();
                if (allLines.Count == 0)
                {
                    printLog("Todo file is empty.");
                    return;
                }

                // Move only the first limit lines from todo to done
                int movedSuccessCount = 0;
                int movedFailedCount = 0;
                List<string> remainingLines = new List<string>();
                int linesToProcessCount = Math.Min(limit, allLines.Count); // Move only limit lines or less if fewer exist

                var apiAddress = txtApiAddress.Text;
                var apiUserName = txtApiUserName.Text;
                var apiPassword = txtApiPassword.Text;
                TotalDownloadData = 0;

                using var cmd = connection.CreateCommand();
                bool isTableExist = false; // Initialize table check variable

                for (int i = 0; i < allLines.Count; i++)
                {
                    string line = allLines[i];

                    if (i < linesToProcessCount)
                    {
                        // Process first limit lines
                        printLog($"             Line {i + 1}: {line}");

                        bool lineProcessedSuccessfully = false; // Track if line processed successfully

                        try
                        {
                            string paramFilterPerMahasiswa = paramFilter.Replace("[idRegMhsValue]", line.Trim());

                            // 1. get json data
                            var jsonData = await GetData(apiAddress, apiAction, ApiToken, paramFilterPerMahasiswa, string.Empty);
                            if (jsonData.Count == 0)
                            {
                                printLog("                          No Data returned");
                                lineProcessedSuccessfully = false; // Treat as failure
                                throw new Exception("No data returned from API");
                            }

                            // 2. convert json into insert
                            StringBuilder sqlInsert = new StringBuilder();
                            sqlInsert.AppendLine("");
                            sqlInsert.AppendLine(ConvertJsonToMySqlInsert(jsonData.First().GetRawText(), destTable));
                            foreach (var item in jsonData.Skip(1))
                                sqlInsert.AppendLine(ConvertJsonToMySqlInsert(item.GetRawText()));
                            sqlInsert.AppendLine(";");
                            progressBar.Value = 40;

                            // 3.create new table
                            // Check if table exists only on first iteration
                            if (i == 0 || !isTableExist)
                            {
                                isTableExist = TableExists(connection, destTable);
                            }

                            if (!isTableExist)
                            {
                                printLog("                          Create New Table = YES");
                                var sqlSchema = GenerateTableSchemaFromJson(jsonData.First().GetRawText(), destTable);
                                MySqlCommand cmd2 = new MySqlCommand(sqlSchema.ToString(), connection);
                                cmd2.CommandTimeout = dbConTimeout; cmd2.ExecuteNonQuery();
                                isTableExist = true; // Mark table as created
                                progressBar.Value = 50;
                            }
                            else
                            {
                                printLog("                          Create New Table = NO");
                            }
                            progressBar.Value = 60;

                            //4. Execute Insert Table
                            printLog("                          Process to insert data ...");
                            cmd.CommandText = sqlInsert.ToString();
                            cmd.CommandTimeout = dbConTimeout;
                            int rowsAffected = cmd.ExecuteNonQuery();

                            printLog("                          Success insert " + rowsAffected + " data.");
                            TotalDownloadData = TotalDownloadData + rowsAffected;
                            progressBar.Value = 80;

                            // Mark as successfully processed only if no exceptions occurred
                            lineProcessedSuccessfully = true;
                        }
                        catch (Exception ex)
                        {
                            printLog($"                 ✗ Error processing line: {ex.Message}");
                            lineProcessedSuccessfully = false;
                        }

                        // Only move to done file if processing was successful
                        if (lineProcessedSuccessfully)
                        {
                            try
                            {
                                File.AppendAllText(donePath, line + Environment.NewLine);
                                printLog($"                     ✓ Moved to done file.");
                                movedSuccessCount++;
                            }
                            catch (IOException ioEx)
                            {
                                printLog($"                 ✗ Failed to move to done file: {ioEx.Message}");
                                remainingLines.Add(line); // Keep in todo file if can't write to done
                            }
                        }
                        else
                        {
                            // Line failed - add to failed file and remove from todo
                            try
                            {
                                File.AppendAllText(failedPath, line + Environment.NewLine);
                                printLog($"                 ✓ Moved to failed file.");
                                movedFailedCount++;
                            }
                            catch (IOException ioEx)
                            {
                                printLog($"                 ✗ Failed to move to failed file: {ioEx.Message}");
                                remainingLines.Add(line); // Keep in todo file if can't write to failed
                            }
                        }
                    }
                    else
                    {
                        // Keep remaining lines
                        remainingLines.Add(line);
                    }
                }

                // Update todo file with remaining lines
                if (remainingLines.Count > 0)
                {
                    File.WriteAllLines(todoPath, remainingLines);
                    printLog($"          Todo file updated with {remainingLines.Count} remaining lines.");
                }
                else
                {
                    File.WriteAllText(todoPath, string.Empty);
                    printLog("          Todo file has been cleared.");
                }

                printLog(string.Empty);
                printLog("============== " + todoFile);
                printLog($"          Moved {movedFailedCount}/{limit} items to failed file.");
                printLog($"          Moved {movedSuccessCount}/{limit} items to done file.");
            }
            catch (IOException ex)
            {
                printLog($"                 An IO error occurred while accessing the file: {ex.Message}");
            }
            catch (Exception ex)
            {
                printLog($"                 An unexpected error occurred: {ex.Message}");
            }

        }

        #endregion

        #region Config

        public class AppConfig
        {
            public string DbAddress { get; set; } = "localhost";
            public string DbPort { get; set; } = "3306";
            public string DbUsername { get; set; } = "root";
            public string DbPassword { get; set; } = "root";
            public string DbName { get; set; } = "db_importer_feeder";
            public string ApiAddress { get; set; } = "";
            public string ApiUsername { get; set; } = "";
            public string ApiPassword { get; set; } = "";
            public string ApiAction { get; set; } = "";
            public string ApiAdditionalParam { get; set; } = "";
            public string DestTableName { get; set; } = "";
            public int HttpClientTimeoutInMinutes { get; set; } = 120;
            public int MySQLConnectionTimeoutInSecond { get; set; } = 3600;

        }

        private const string ConfigFileName = "app.config";

        private async Task<AppConfig> CreateDefaultConfigAsync()
        {
            var defaultConfig = new AppConfig();
            string jsonString = JsonSerializer.Serialize(defaultConfig, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(ConfigFileName, jsonString);
            return defaultConfig;
        }

        private async Task<AppConfig> GetConfigAsync()
        {
            if (!File.Exists(ConfigFileName)) return await CreateDefaultConfigAsync();

            try
            {
                string jsonString = await File.ReadAllTextAsync(ConfigFileName);
                var config = JsonSerializer.Deserialize<AppConfig>(jsonString);
                return config ?? new AppConfig();
            }
            catch (JsonException ex)
            {
                throw new Exception($"Error reading '{ConfigFileName}': {ex.Message}");
            }
        }

        private async Task SaveConfigAsync(AppConfig config)
        {
            string jsonString = JsonSerializer.Serialize(config, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(ConfigFileName, jsonString);
        }

        #endregion

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Set filter to Excel files
                openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls|All Files (*.*)|*.*";
                openFileDialog.Title = "Select an Excel File";
                
                // Set default folder
                string defaultFolder = @"E:\backupdatabse local\Data Pembayaran 2018 - 2025";
                if (Directory.Exists(defaultFolder))
                {
                    openFileDialog.InitialDirectory = defaultFolder;
                }
                else
                {
                    openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }

                // Show the dialog
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the file name and full path
                    string fileName = Path.GetFileName(openFileDialog.FileName);
                    string filePath = openFileDialog.FileName;

                    // Display on label
                    lblFileName.Text = $"{filePath}";
                    
                    // Set database name to db_importer_feeder
                    txtDBName.Text = "db_importer_feeder";
                    
                    printLog($"Selected file: {filePath}");
                    printLog($"Database set to: db_importer_feeder");
                }
            }
        }

        private void btnExportFile_Click(object sender, EventArgs e)
        {
            try
            {
                // Get file path from label
                string filePath = lblFileName.Text;

                if (string.IsNullOrWhiteSpace(filePath))
                {
                    printLog("Error: Please select a file first using 'Open File' button.");
                    return;
                }

                if (!File.Exists(filePath))
                {
                    printLog($"Error: File not found at {filePath}");
                    return;
                }

                // Extract filename without extension
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // Set EPPlus license context
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Read Excel file
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    if (package.Workbook.Worksheets.Count == 0)
                    {
                        printLog("Error: Excel file contains no sheets.");
                        return;
                    }

                    printLog($"Processing Excel file: {fileName}");
                    printLog($"Found {package.Workbook.Worksheets.Count} sheet(s)");

                    // Get database connection
                    string dbString = GetDbString();
                    using (MySqlConnection connection = new MySqlConnection(dbString))
                    {
                        connection.Open();
                        printLog("Database connection opened.");

                        // Process each sheet
                        foreach (var worksheet in package.Workbook.Worksheets)
                        {
                            string sheetName = worksheet.Name;
                            string tableName = $"Keuangan_{fileName}_{sheetName}";

                            printLog($"\n  Processing sheet: {sheetName}");

                            // Check if worksheet has data
                            if (worksheet.Dimension == null || worksheet.Dimension.Rows == 0)
                            {
                                printLog($"    Sheet '{sheetName}' is empty. Skipping...");
                                continue;
                            }

                            // Get column headers from first row
                            List<string> headers = new List<string>();
                            for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                            {
                                string headerValue = worksheet.Cells[1, col].Value?.ToString() ?? $"Column{col}";
                                headers.Add(headerValue.Trim());
                            }

                            printLog($"    Found {headers.Count} columns");

                            // Create table
                            string createTableQuery = GenerateCreateTableQuery(tableName, headers);
                            using (MySqlCommand cmd = new MySqlCommand(createTableQuery, connection))
                            {
                                cmd.CommandTimeout = dbConTimeout;
                                cmd.ExecuteNonQuery();
                                printLog($"    Table '{tableName}' created successfully.");
                            }

                            // Create temporary CSV file
                            string tempCsvPath = Path.Combine(Path.GetTempPath(), $"temp_{tableName}_{Guid.NewGuid()}.csv");
                            
                            try
                            {
                                // Export data to CSV (rows 2 onwards, skipping header)
                                ExportExcelToCsv(worksheet, tempCsvPath, headers);
                                
                                int rowsInserted = 0;
                                int rowCount = worksheet.Dimension.Rows - 1; // Exclude header row

                                // Load data from CSV using LOAD DATA INFILE
                                if (rowCount > 0)
                                {
                                    rowsInserted = LoadDataFromCsv(connection, tableName, headers, tempCsvPath);
                                    printLog($"    ✓ Inserted {rowsInserted} rows into table '{tableName}' using LOAD DATA INFILE.");
                                }
                                else
                                {
                                    printLog($"    Sheet '{sheetName}' has no data rows (only header). Skipping data load.");
                                }
                            }
                            finally
                            {
                                // Clean up temporary CSV file
                                if (File.Exists(tempCsvPath))
                                {
                                    try
                                    {
                                        File.Delete(tempCsvPath);
                                    }
                                    catch
                                    {
                                        // Ignore cleanup errors
                                    }
                                }
                            }
                        }

                        connection.Close();
                        printLog("\nDatabase connection closed.");
                    }

                    printLog("\n✓ Excel file import completed successfully!");
                }
            }
            catch (Exception ex)
            {
                printLog($"Error during export: {ex.Message}");
                printLog($"Stack trace: {ex.StackTrace}");
            }
        }

        private void ExportExcelToCsv(ExcelWorksheet worksheet, string csvFilePath, List<string> headers)
        {
            using (StreamWriter writer = new StreamWriter(csvFilePath, false, Encoding.UTF8))
            {
                // Write data rows (skip header row)
                for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                {
                    List<string> rowValues = new List<string>();
                    for (int col = 1; col <= headers.Count; col++)
                    {
                        string cellValue = worksheet.Cells[row, col].Value?.ToString() ?? "";
                        // Escape CSV values
                        cellValue = EscapeCsvValue(cellValue.Trim());
                        rowValues.Add(cellValue);
                    }

                    // Write row to CSV (tab-delimited for better compatibility)
                    writer.WriteLine(string.Join("\t", rowValues));
                }
            }
        }

        private string EscapeCsvValue(string value)
        {
            // If value contains special characters, wrap in quotes
            if (value.Contains("\"") || value.Contains("\t") || value.Contains("\n") || value.Contains("\r"))
            {
                value = "\"" + value.Replace("\"", "\"\"") + "\"";
            }
            return value;
        }

        private int LoadDataFromCsv(MySqlConnection connection, string tableName, List<string> headers, string csvFilePath)
        {
            try
            {
                // Try LOAD DATA LOCAL INFILE first
                return LoadDataUsingLoadDataInfile(connection, tableName, headers, csvFilePath);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Loading local data is disabled") || ex.Message.Contains("local"))
                {
                    printLog($"    ⚠ LOAD DATA INFILE disabled on server, using batch INSERT method...");
                    // Fallback to batch INSERT
                    return LoadDataUsingBatchInsert(connection, tableName, headers, csvFilePath);
                }
                else
                {
                    throw;
                }
            }
        }

        private int LoadDataUsingLoadDataInfile(MySqlConnection connection, string tableName, List<string> headers, string csvFilePath)
        {
            // Escape file path for MySQL
            string escapedPath = csvFilePath.Replace("\\", "\\\\");

            // Build column list
            string columnList = string.Join(", ", headers.Select(h => $"`{SanitizeColumnName(h)}`"));

            // Build LOAD DATA INFILE query with proper escaping
            string loadDataQuery = $@"
                LOAD DATA LOCAL INFILE '{escapedPath}'
                INTO TABLE `{tableName}`
                CHARACTER SET utf8mb4
                FIELDS TERMINATED BY '\t'
                LINES TERMINATED BY '\n'
                ({columnList})
            ";

            using (MySqlCommand cmd = new MySqlCommand(loadDataQuery, connection))
            {
                cmd.CommandTimeout = dbConTimeout;
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        private int LoadDataUsingBatchInsert(MySqlConnection connection, string tableName, List<string> headers, string csvFilePath)
        {
            int totalRowsInserted = 0;
            int batchSize = 500; // Insert 500 rows per batch
            List<List<string>> batch = new List<List<string>>();

            try
            {
                using (StreamReader reader = new StreamReader(csvFilePath, Encoding.UTF8))
                {
                    string? line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Parse CSV line
                        List<string> values = ParseCsvLine(line);
                        batch.Add(values);

                        // Execute batch insert when batch size is reached
                        if (batch.Count >= batchSize)
                        {
                            int inserted = ExecuteBatchInsert(connection, tableName, headers, batch);
                            totalRowsInserted += inserted;
                            batch.Clear();
                        }
                    }

                    // Insert remaining rows
                    if (batch.Count > 0)
                    {
                        int inserted = ExecuteBatchInsert(connection, tableName, headers, batch);
                        totalRowsInserted += inserted;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Batch insert error: {ex.Message}");
            }

            return totalRowsInserted;
        }

        private int ExecuteBatchInsert(MySqlConnection connection, string tableName, List<string> headers, List<List<string>> batch)
        {
            if (batch.Count == 0) return 0;

            StringBuilder queryBuilder = new StringBuilder();
            string columnList = string.Join(", ", headers.Select(h => $"`{SanitizeColumnName(h)}`"));

            queryBuilder.Append($"INSERT INTO `{tableName}` ({columnList}) VALUES ");

            for (int i = 0; i < batch.Count; i++)
            {
                List<string> values = batch[i];
                queryBuilder.Append("(");

                for (int j = 0; j < values.Count; j++)
                {
                    string escapedValue = values[j].Replace("'", "''");
                    queryBuilder.Append($"'{escapedValue}'");
                    if (j < values.Count - 1)
                        queryBuilder.Append(", ");
                }

                queryBuilder.Append(")");
                if (i < batch.Count - 1)
                    queryBuilder.Append(", ");
            }

            queryBuilder.Append(";");

            using (MySqlCommand cmd = new MySqlCommand(queryBuilder.ToString(), connection))
            {
                cmd.CommandTimeout = dbConTimeout;
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        private List<string> ParseCsvLine(string line)
        {
            List<string> values = new List<string>();
            StringBuilder current = new StringBuilder();
            bool inQuotes = false;

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];

                if (c == '"')
                {
                    if (inQuotes && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"');
                        i++;
                    }
                    else
                    {
                        inQuotes = !inQuotes;
                    }
                }
                else if (c == '\t' && !inQuotes)
                {
                    values.Add(current.ToString().Trim('"'));
                    current.Clear();
                }
                else
                {
                    current.Append(c);
                }
            }

            values.Add(current.ToString().Trim('"'));
            return values;
        }

        private string GenerateCreateTableQuery(string tableName, List<string> headers)
        {
            StringBuilder query = new StringBuilder();
            query.AppendLine($"DROP TABLE IF EXISTS `{tableName}`;");
            query.AppendLine($"CREATE TABLE `{tableName}` (");

            for (int i = 0; i < headers.Count; i++)
            {
                string columnName = SanitizeColumnName(headers[i]);
                query.Append($"  `{columnName}` TEXT NULL");

                if (i < headers.Count - 1)
                    query.AppendLine(",");
                else
                    query.AppendLine();
            }

            query.AppendLine(");");
            return query.ToString();
        }

        private string SanitizeColumnName(string columnName)
        {
            // Replace spaces with underscores and remove special characters
            string sanitized = System.Text.RegularExpressions.Regex.Replace(columnName, @"[^\w]", "_");
            // Remove leading digits if any
            sanitized = System.Text.RegularExpressions.Regex.Replace(sanitized, @"^[\d_]+", "");
            // Ensure it's not empty
            return string.IsNullOrEmpty(sanitized) ? "Column" : sanitized;
        }
    }
}
