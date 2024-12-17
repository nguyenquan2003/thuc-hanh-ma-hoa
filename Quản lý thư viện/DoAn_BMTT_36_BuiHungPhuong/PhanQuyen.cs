using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_BMTT_36_BuiHungPhuong
{
    public partial class PhanQuyen : Form
    {
        public PhanQuyen()
        {
            InitializeComponent();
            LoadUSER();
            listView1.View = View.Details;
            listView1.Columns.Add("USENAME",120);
            listView1.Columns.Add("LOCK_DATE",100);
            listView1.Columns.Add("USER_ID",100);
            listView1.Columns.Add("PROFILE", 100);

            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            LoadQUYEN();
            listview2.View = View.Details;
            listview2.Columns.Add("GRANTEE", 120);
            listview2.Columns.Add("PRIVILEGE", 100);
            listview2.Columns.Add("ADMIN_OPTION", 100);
            listview2.Columns.Add("COMMON", 100);
            listview2.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            LoadTablespace();
            list_tablespace.View = View.Details;
            list_tablespace.Columns.Add("file_name", 120);
            list_tablespace.Columns.Add("file_id", 100);
            list_tablespace.Columns.Add("tablespace_name", 100);
            list_tablespace.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            LoadTablespaceCombox();

            listView_profile.View = View.Details;
            listView_profile.Columns.Add("PROFILE", 120);
            listView_profile.Columns.Add("RESOURCE_NAME", 100);
            listView_profile.Columns.Add("LIMIT", 100);
            listView_profile.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            listView_quyentable.View = View.Details;
            listView_quyentable.Columns.Add("GRANTEE", 120);
            listView_quyentable.Columns.Add("TABLE_NAME", 100);
            listView_quyentable.Columns.Add("PRIVILEGE", 100);
            listView_quyentable.Columns.Add("TYPE", 100);
            listView_quyentable.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);


            LoadRole();
            listView_role.View = View.Details;
            listView_role.Columns.Add("ROLE", 120);
            listView_role.Columns.Add("PASSWORD_REQUIRED", 100);
            listView_role.Columns.Add("COMMON", 100);
            listView_role.Columns.Add("ORACLE_MAINTAINED", 100);
            listView_role.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            // Các tham số tài nguyên
            string[] resourceParams = new string[]
            {
                "cpu_per_session",
                "cpu_per_call",
                "connect_time",
                "idle_time",
                "sessions_per_user",
                "logical_reads_per_session",
                "logical_reads_per_call",
                "private_sga",
                "composite_limit",
                "password_life_time",
                "password_grace_time",
                "password_reuse_max",
                "password_reuse_time",
                "password_verify_function",
                "failed_login_attempts",
                "password_lock_time"
            };

            // Thêm các tham số vào ComboBox
            cbm_thamso.Items.AddRange(resourceParams);
            LoadProfilecombox();
        }

        public void LoadTablespace()
        {
            try
            {
                
                using (OracleCommand cmd = new OracleCommand("BEGIN LoadALLTablespace(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    
                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    
                    cmd.ExecuteNonQuery();

                    
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        list_tablespace.Items.Clear(); 

                        
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["file_name"].ToString());
                            item.SubItems.Add(reader["file_id"].ToString());
                            item.SubItems.Add(reader["tablespace_name"].ToString());

                            list_tablespace.Items.Add(item);  // Add item to ListView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }

        public void LoadUSER()
        {
            try
            {
                
                using (OracleCommand cmd = new OracleCommand("BEGIN XemUser(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    
                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    
                    cmd.ExecuteNonQuery();

                    
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView1.Items.Clear();  
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["USERNAME"].ToString());
                            item.SubItems.Add(reader["LOCK_DATE"].ToString());
                            item.SubItems.Add(reader["USER_ID"].ToString());
                            item.SubItems.Add(reader["PROFILE"].ToString());

                            listView1.Items.Add(item);  // Add item to ListView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }


        public void LoadTimUSER(string v_username)
        {      
            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN TimUSER(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Thêm tham số đầu vào
                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);

                    // Thêm tham số output cho RefCursor
                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    // Thực thi thủ tục
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView1.Items.Clear();

                        // Kiểm tra nếu có dữ liệu trả về
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm các cột vào ListView
                                ListViewItem item = new ListViewItem(reader["USERNAME"].ToString());
                                item.SubItems.Add(reader["LOCK_DATE"].ToString());
                                item.SubItems.Add(reader["USER_ID"].ToString());
                                item.SubItems.Add(reader["PROFILE"].ToString());

                                listView1.Items.Add(item);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy người dùng.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }


        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_loadlistview1_Click(object sender, EventArgs e)
        {
            LoadUSER();
            LoadQUYEN();
        }

        public void LoadQUYEN()
        {
            try
            {
                
                using (OracleCommand cmd = new OracleCommand("BEGIN XEMQUYEN(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                  
                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                   
                    cmd.ExecuteNonQuery();

                    
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listview2.Items.Clear();  
                  
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["GRANTEE"].ToString());
                            item.SubItems.Add(reader["PRIVILEGE"].ToString());
                            item.SubItems.Add(reader["ADMIN_OPTION"].ToString());
                            item.SubItems.Add(reader["COMMON"].ToString());

                            listview2.Items.Add(item);  // Add item to ListView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
      
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }



        public void LoadTimQUYENUSER(string v_username)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN TimQUYENUSER(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;


                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);


                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);


                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listview2.Items.Clear();


                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["GRANTEE"].ToString());
                            item.SubItems.Add(reader["PRIVILEGE"].ToString());
                            item.SubItems.Add(reader["ADMIN_OPTION"].ToString());
                            item.SubItems.Add(reader["COMMON"].ToString());

                            listview2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }

        public void LoadTimquyenTableUSER(string v_username)
        {

            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN TimquyentableUSER(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;


                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);


                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);


                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView_quyentable.Items.Clear();


                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["GRANTEE"].ToString());
                            item.SubItems.Add(reader["TABLE_NAME"].ToString());
                            item.SubItems.Add(reader["PRIVILEGE"].ToString());
                            item.SubItems.Add(reader["TYPE"].ToString());

                            listView_quyentable.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }



        private void btn_timkiem_Click_1(object sender, EventArgs e)
        {
            string username = txt_username.Text.ToUpper();

            if (!string.IsNullOrEmpty(username))
            {
                LoadTimUSER(username);

                LoadTimQUYENUSER(username);

                LoadTimquyenTableUSER(username);
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên người dùng để tìm kiếm.");
            }
        }

        private void btn_taotablespace_Click(object sender, EventArgs e)
        {
          
            if (string.IsNullOrWhiteSpace(txt_tentablespace.Text) ||
                string.IsNullOrWhiteSpace(txt_datafilepath.Text) ||
                string.IsNullOrWhiteSpace(txt_kichthuoc.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("CreateTablespace", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                  
                    cmd.Parameters.Add("p_tablespace_name", OracleDbType.Varchar2).Value = txt_tentablespace.Text;
                    cmd.Parameters.Add("p_datafile_path", OracleDbType.Varchar2).Value = txt_datafilepath.Text;
                    cmd.Parameters.Add("p_size", OracleDbType.Varchar2).Value = txt_kichthuoc.Text;

                   
                    cmd.ExecuteNonQuery();
                    LoadTablespace();
                    MessageBox.Show("Tablespace đã được tạo thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo tablespace: " + ex.Message);
            }
        }

        private void btn_choduongdan_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Oracle Datafiles (*.dbf)|*.dbf|All Files (*.*)|*.*";
                openFileDialog.Title = "Select Oracle Datafile";

               
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;

                   
                    if (!filePath.EndsWith(".dbf", StringComparison.OrdinalIgnoreCase))
                    {
                        filePath += ".dbf";
                    }

                  
                    txt_datafilepath.Text = filePath;
                }
            }
        }

        public void LoadTablespaceCombox()
        {
            try
            {
                
                using (OracleCommand cmd = new OracleCommand("BEGIN loadtablespace(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    
                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                   
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        cbm_loadtablespace.Items.Clear();  

                       
                        while (reader.Read())
                        {
                            string tablespaceName = reader["tablespace_name"].ToString();
                            cbm_loadtablespace.Items.Add(tablespaceName);  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }

        private void cbm_loadtablespace_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
        private void CreateUserProfile(string profileName, string parameterName, string parameterValue)
        {
  
            if (string.IsNullOrWhiteSpace(txt_tenprofile.Text) ||
                string.IsNullOrWhiteSpace(cbm_thamso.Text) ||
                string.IsNullOrWhiteSpace(numericUpDown1.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            try
            {
                using (OracleCommand cmd = new OracleCommand("create_user_profile", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

            
                    cmd.Parameters.Add("profileName", OracleDbType.Varchar2).Value = profileName;
                    cmd.Parameters.Add("parameterName", OracleDbType.Varchar2).Value = parameterName;
                    cmd.Parameters.Add("parameterValue", OracleDbType.Varchar2).Value = parameterValue;

               
                    cmd.ExecuteNonQuery();


                    MessageBox.Show("Profile đã được tạo thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo Profile: " + ex.Message);
            }
        }


        private void btn_taoprofile_Click(object sender, EventArgs e)
        {
            string profileName = txt_tenprofile.Text;
            string parameterName = cbm_thamso.SelectedItem.ToString();
            string parameterValue = numericUpDown1.Value.ToString();

           
            try
            {
                CreateUserProfile(profileName, parameterName, parameterValue);
                MessageBox.Show($"Profile {profileName} created successfully!");
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }


        public void LoadProfilecombox()
        {
            try
            {
                
                using (OracleCommand cmd = new OracleCommand("BEGIN get_unique_profiles(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                   
                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    cmd.ExecuteNonQuery();

                   
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        cnm_tencacprofile.Items.Clear();  
                        cbm_tenprofile1.Items.Clear();
                       
                        while (reader.Read())
                        {
                            string profilename = reader["PROFILE"].ToString();
                            cnm_tencacprofile.Items.Add(profilename);  
                            cbm_tenprofile1.Items.Add(profilename);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
             
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }

        private void AlterProfile(string profileName, string parameterName, string parameterValue)
        {

            if (string.IsNullOrWhiteSpace(cnm_tencacprofile.Text) ||
                string.IsNullOrWhiteSpace(cbm_thamso.Text) ||
                string.IsNullOrWhiteSpace(numericUpDown1.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            try
            {
                using (OracleCommand cmd = new OracleCommand("alter_user_profile", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.Add("profileName", OracleDbType.Varchar2).Value = profileName;
                    cmd.Parameters.Add("parameterName", OracleDbType.Varchar2).Value = parameterName;
                    cmd.Parameters.Add("parameterValue", OracleDbType.Varchar2).Value = parameterValue;


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Profile đã được tạo thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo Profile: " + ex.Message);
            }
        }

        private void btn_themthamso_Click(object sender, EventArgs e)
        {
            string profileName = cnm_tencacprofile.SelectedItem.ToString();
            string parameterName = cbm_thamso.SelectedItem.ToString();
            string parameterValue = numericUpDown1.Value.ToString();

            
            try
            {
                AlterProfile(profileName, parameterName, parameterValue);
                MessageBox.Show($"Profile {profileName} created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        public void LoadTimProfile(string v_username)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN Timprofile(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Thêm tham số đầu vào
                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);

                    // Thêm tham số output cho RefCursor
                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    // Thực thi thủ tục
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView_profile.Items.Clear();

                        // Kiểm tra nếu có dữ liệu trả về
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm các cột vào ListView
                                ListViewItem item = new ListViewItem(reader["PROFILE"].ToString());
                                item.SubItems.Add(reader["RESOURCE_NAME"].ToString());
                                item.SubItems.Add(reader["LIMIT"].ToString());
                                listView_profile.Items.Add(item);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy người dùng.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }

        private void cnm_tencacprofile_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu ComboBox không rỗng và có item đã được chọn
            if (cnm_tencacprofile.SelectedIndex >= 0)
            {
                // Lấy giá trị từ ComboBox (tên người dùng)
                string selectedUsername = cnm_tencacprofile.SelectedItem.ToString();

                // Gọi phương thức LoadTimProfile để tải dữ liệu tương ứng
                LoadTimProfile(selectedUsername);
            }
        }

        private void TaoUSER(string username, string userPassword, string tablespace, string profile, int quota)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("TaoNguoiDung", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số vào thủ tục
                    cmd.Parameters.Add("v_username", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("v_password", OracleDbType.Varchar2).Value = userPassword;
                    cmd.Parameters.Add("v_tablespace", OracleDbType.Varchar2).Value = tablespace;
                    cmd.Parameters.Add("v_profile", OracleDbType.Varchar2).Value = profile;
                    cmd.Parameters.Add("v_quota", OracleDbType.Int32).Value = quota;

                    // Thực thi
                    cmd.ExecuteNonQuery();
                    MessageBox.Show($"USER {username} đã được tạo thành công.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo USER: " + ex.Message);
            }
        }

        //tao user tablespace, profile, quota
        private void button2_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string userPassword = password.Text.Trim();
            string tablespace = cbm_loadtablespace.SelectedItem?.ToString() ?? string.Empty;
            string profile = cbm_tenprofile1.SelectedItem?.ToString() ?? string.Empty;
            string quotaText = txt_quota.Text.Trim();

            // Kiểm tra giá trị quota
            if (!int.TryParse(quotaText, out int quota) || quota <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị quota hợp lệ (số nguyên dương).");
                return;
            }

            // Gọi phương thức tạo user, truyền giá trị quota kiểu int
            try
            {
                TaoUSER(username, userPassword, tablespace, profile, quota);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }

        private void btn_chonquyen_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ giao diện
            string username = txt_username.Text.Trim();
            string privilege = cbm_quyenhethong.SelectedItem?.ToString();

            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(privilege))
            {
                MessageBox.Show("Vui lòng chọn quyền hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (OracleCommand cmd = new OracleCommand("Ganquyenhethong", Database_sys.SysConn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                // Truyền tham số
                cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                cmd.Parameters.Add("p_privilege", OracleDbType.Varchar2).Value = privilege;

                // Thực thi thủ tục
                cmd.ExecuteNonQuery();
                MessageBox.Show("Đã cấp quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadQUYEN();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string privilege = cbm_quyentable.SelectedItem?.ToString();
            string table = cbm_table.SelectedItem?.ToString();

            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(privilege))
            {
                MessageBox.Show("Vui lòng chọn quyền đối tượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(table))
            {
                MessageBox.Show("Vui lòng chọn table!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("Ganquyentable", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_table_name", OracleDbType.Varchar2).Value = table;
                    cmd.Parameters.Add("p_permission", OracleDbType.Varchar2).Value = privilege;

                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cấp quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQUYEN(); // Tải lại danh sách quyền
                    LoadTimquyenTableUSER(username);

                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi khi cấp quyền: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PhanQuyen_Load(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void btn_chonthuquyen_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string privilege = cbm_quyenthuhoi.SelectedItem?.ToString();
            string table = cbm_tablethuquyen.SelectedItem?.ToString();

            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(privilege))
            {
                MessageBox.Show("Vui lòng chọn quyền đối tượng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(table))
            {
                MessageBox.Show("Vui lòng chọn table!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand("thuhoi_quyen", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số
                    cmd.Parameters.Add("p_quyen", OracleDbType.Varchar2).Value = privilege;
                    cmd.Parameters.Add("p_bang", OracleDbType.Varchar2).Value = table;
                    cmd.Parameters.Add("p_user", OracleDbType.Varchar2).Value = username;

                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thu hồi quyền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQUYEN(); // Tải lại danh sách quyền
                    LoadTimquyenTableUSER(username);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi khi thu hồi quyền: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadRole()
        {
            try
            {

                using (OracleCommand cmd = new OracleCommand("BEGIN Xemrole(:data); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;


                    OracleParameter outputParam = new OracleParameter("data", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);


                    cmd.ExecuteNonQuery();


                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView_role.Items.Clear();


                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(reader["ROLE"].ToString());
                            item.SubItems.Add(reader["PASSWORD_REQUIRED"].ToString());
                            item.SubItems.Add(reader["COMMON"].ToString());
                            item.SubItems.Add(reader["ORACLE_MAINTAINED"].ToString());


                            listView_role.Items.Add(item);  // Add item to ListView
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                MessageBox.Show("Lỗi khi lấy dữ liệu từ bảng sách: " + ex.Message);
            }
        }

        private void btn_taonhomquyen_Click(object sender, EventArgs e)
        {
            string username = txt_tenrole.Text.Trim();
            string password = txt_passwordrole.Text.Trim();


            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập password!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }         
            try
            {
                using (OracleCommand cmd = new OracleCommand("CreateRole", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số
                    cmd.Parameters.Add("p_role_name", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("p_password", OracleDbType.Varchar2).Value = password;
                    

                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã role "+ username +" thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQUYEN(); // Tải lại danh sách quyền
                    LoadTimquyenTableUSER(username);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi khi tạo role: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadTimRole(string v_username)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN TimROLE(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Thêm tham số đầu vào
                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);

                    // Thêm tham số output cho RefCursor
                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    // Thực thi thủ tục
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView_role.Items.Clear();

                        // Kiểm tra nếu có dữ liệu trả về
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm các cột vào ListView
                                ListViewItem item = new ListViewItem(reader["ROLE"].ToString());
                                item.SubItems.Add(reader["PASSWORD_REQUIRED"].ToString());
                                item.SubItems.Add(reader["COMMON"].ToString());
                                item.SubItems.Add(reader["ORACLE_MAINTAINED"].ToString());

                                listView_role.Items.Add(item);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy role.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }

        private void btn_timrole_Click(object sender, EventArgs e)
        {
            string role = txt_tenrole.Text.ToUpper();

            if (!string.IsNullOrEmpty(role))
            {
                LoadTimRole(role);
                LoadTimQUYENUSER(role);
                LoadTimquyenTableUSER(role);

            }
            else
            {
                MessageBox.Show("Vui lòng nhập tên role để tìm kiếm.");
            }
        }

        private void btn_capuser_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string role = txt_tenrole.Text.Trim();


            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập role!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (OracleCommand cmd = new OracleCommand("GrantRoleToUser", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số
                    cmd.Parameters.Add("p_role_name", OracleDbType.Varchar2).Value = role;
                    cmd.Parameters.Add("p_user_name", OracleDbType.Varchar2).Value = username;


                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã cấp user " + username + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listView_role.Columns.Clear();
                    listView_role.View = View.Details;
                    listView_role.Columns.Add("GRANTEE", 120);
                    listView_role.Columns.Add("GRANTED_ROLE", 100);
                    listView_role.Columns.Add("ADMIN_OPTION", 100);
                    listView_role.Columns.Add("DELEGATE_OPTION", 100);
                    listView_role.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    LoadCapUserROLE(username);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi khi cấp: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void LoadCapUserROLE(string v_username)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand("BEGIN TimuserROLE(:data, :result); END;", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.Text;

                    // Thêm tham số đầu vào
                    OracleParameter inputParam = new OracleParameter("data", OracleDbType.Varchar2);
                    inputParam.Value = v_username;
                    cmd.Parameters.Add(inputParam);

                    // Thêm tham số output cho RefCursor
                    OracleParameter outputParam = new OracleParameter("result", OracleDbType.RefCursor);
                    outputParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParam);

                    // Thực thi thủ tục
                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        listView_role.Items.Clear();

                        // Kiểm tra nếu có dữ liệu trả về
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Thêm các cột vào ListView
                                ListViewItem item = new ListViewItem(reader["GRANTEE"].ToString());
                                item.SubItems.Add(reader["GRANTED_ROLE"].ToString());
                                item.SubItems.Add(reader["ADMIN_OPTION"].ToString());
                                item.SubItems.Add(reader["DELEGATE_OPTION"].ToString());



                                listView_role.Items.Add(item);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy role.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ
                MessageBox.Show("Lỗi khi lấy dữ liệu từ thủ tục: " + ex.Message);
            }
        }

        private void btn_thuhoiuserrole_Click(object sender, EventArgs e)
        {
            string username = txt_username.Text.Trim();
            string role = txt_tenrole.Text.Trim();


            // Kiểm tra giá trị đầu vào
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Vui lòng nhập tên user!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Vui lòng nhập role!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (OracleCommand cmd = new OracleCommand("revoke_role_from_user", Database_sys.SysConn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số
                    cmd.Parameters.Add("p_role_name", OracleDbType.Varchar2).Value = role;
                    cmd.Parameters.Add("p_user_name", OracleDbType.Varchar2).Value = username;


                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa user " + username + " thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listView_role.Columns.Clear();
                    listView_role.View = View.Details;
                    listView_role.Columns.Add("GRANTEE", 120);
                    listView_role.Columns.Add("GRANTED_ROLE", 100);
                    listView_role.Columns.Add("ADMIN_OPTION", 100);
                    listView_role.Columns.Add("DELEGATE_OPTION", 100);
                    listView_role.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                    LoadCapUserROLE(username);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
