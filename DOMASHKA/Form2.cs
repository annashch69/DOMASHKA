using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DOMASHKA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Load += Form2_Load;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            try
            {
                var cs = "Host=localhost;Username=postgres;Password=1111;Database=Tovar";

                using (NpgsqlConnection con = new NpgsqlConnection(cs))
                {
                    con.Open();

                    var sql = "SELECT price, price FROM products";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(sql, con))
                    {
                        using (NpgsqlDataReader dataReader = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dataReader);

                            // Создаем новую DataTable для сгруппированных данных
                            DataTable groupedData = GroupDataByPrice(dt);

                            // Предполагая, что у вас есть элемент управления Chart с именем chart1
                            chart1.Series.Clear();
                            chart1.DataSource = groupedData;

                            // Предполагая, что вы хотите создать столбчатую диаграмму
                            Series series = new Series("Ценовые категории");
                            series.ChartType = SeriesChartType.Column;
                            series.XValueMember = "КатегорияЦен";
                            series.YValueMembers = "КоличествоПродуктов";

                            chart1.Series.Add(series);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GroupDataByPrice(DataTable originalData)
        {
            DataTable groupedData = new DataTable();
            groupedData.Columns.Add("КатегорияЦен", typeof(string));
            groupedData.Columns.Add("КоличествоПродуктов", typeof(int));

            // Определите свои ценовые категории
            int[] priceCategories = { 500, 1000, 1500, 2000 }; // Ваши интервалы

            // Группируем данные по ценовым категориям
            for (int i = 0; i < priceCategories.Length; i++)
            {
                int lowerBound = i * 500 + 100;
                int upperBound = (i + 1) * 500;

                string categoryName = $"{lowerBound} р. - {upperBound} р.";

                int productCount = originalData.AsEnumerable()
                    .Count(row => row.Field<decimal>("price") >= lowerBound && row.Field<decimal>("price") <= upperBound);

                groupedData.Rows.Add(categoryName, productCount);
            }

            return groupedData;
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Обработка события щелчка по графику (если необходимо)
        }
    }
}