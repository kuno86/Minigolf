using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Minigolf
{
    class MySql
    {
        private MySqlCommand cmd { get; set; }
        private MySqlDataReader reader;
        private MySqlConnection con;

        public MySql(string ip="127.0.0.1", int port=3306, string database="Minigolf", string user="root", string pw="")
        {
            try{
            string connectionString = "Server="+ip+";Port="+port+";Database="+database+";Uid="+user+";password="+pw+";";
            //string connectionString = "Server=169.254.149.19;Port=3306;Database=filmdb;Uid=root;password=debian;";
            con = new MySqlConnection(connectionString);
            MySqlCommand cmd1 = con.CreateCommand();

            MySqlDataReader reader = null;
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
            con.Open();
            con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }


        public string query(string sql_cmd, string delimiter="")
        {
            string output = "";
            try
            {

                cmd = con.CreateCommand();
                cmd.CommandText = sql_cmd;
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader != null && reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        output += (reader[i].ToString())+delimiter;
                    }
                }
                if(delimiter != "")
                    output = output.Substring(0, output.Length - 1);
                con.Close();
                //Console.WriteLine(output);
            }
            catch (Exception ex)
            {
                MessageBox.Show(cmd.CommandText + "\n\n" + ex.Message);
            }
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return output;            
        }

        public void writeBlob(byte[] bytes, string targetTable, string targetField, string where="")
        {
            try
            {
                cmd = con.CreateCommand();
                if (where != "")
                    where = " WHERE " + where;
                if(query("SELECT id FROM player"+where+";")=="")    //empty -> use INSERT       
                    cmd.CommandText = "INSERT INTO '" + targetTable + "' (" + targetField + ") VALUES(" + "@bytes" + ")" + where + ";";
                else                                                //existing Values -> UPDATE SET
                    cmd.CommandText = "UPDATE '" + targetTable + "' SET " + targetField + " = " + "@bytes" + " " + where + ";";
                cmd.Parameters.Add(new MySqlParameter("@bytes", bytes));
                con.Open();
                reader = cmd.ExecuteReader();

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(cmd.CommandText+"\n\n"+ex.Message);
            }
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();
        }

        public byte[] readBlob(string table, string field, string where)
        {
            byte[] output = null;
            try
            {
                cmd = con.CreateCommand();
                if (where != "")
                    where = " WHERE " + where;
                cmd.CommandText = "SELECT "+field+" FROM "+table+where+";";
                con.Open();
                reader = cmd.ExecuteReader();

                while (reader != null && reader.Read())
                {
                    output = (byte[])(reader[field]);
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(cmd.CommandText + "\n\n" + ex.Message);
            }
            if (con.State == System.Data.ConnectionState.Open)
                con.Close();

            return output;
        }


        



    }
}
