using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using PNGEC.API.Domain.Mobile.Voter;
using DataTable = System.Data.DataTable;
using System.Globalization;
using PNGEC.API.Helper;

namespace PNGEC.Desktop.Forms
{
    public partial class ImportFromExcel : Form
    {
        public ImportFromExcel()
        {
            InitializeComponent();
        }

        private async void btnOpenExcelFile_Click(object sender, EventArgs e)
        {
            string fname = "";
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Excel File Dialog";
            fdlg.InitialDirectory = @"c:\";
            fdlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                fname = fdlg.FileName;
            }


            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fname);
            Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[2];
            Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

            int rowCount = xlRange.Rows.Count;
            int colCount = xlRange.Columns.Count;

            // dt.Column = colCount;  
            dataGridView1.ColumnCount = colCount;
            dataGridView1.RowCount = rowCount;
            List<VoterModel> voterModels = new List<VoterModel>();
            
            for (int i = 2; i <= rowCount; i++)
            {
                VoterModel voterModel = new VoterModel();
                for (int j = 1; j <= colCount; j++)
                {
                    if (j == 1)
                    {
                        voterModel.FName = xlRange.Cells[i, j].Value2.ToString();
                    }
                    else if (j == 2)
                    {
                        voterModel.LName = xlRange.Cells[i, j].Value2.ToString();
                    }
                    else if (j == 3)
                    {
                        if (xlRange.Cells[i, j].Value2.ToString() == "Male")
                        {
                            voterModel.Gender = "M";
                        }
                        if (xlRange.Cells[i, j].Value2.ToString() == "Female")
                        {
                            voterModel.Gender = "F";
                        }                        
                    }
                    else if (j == 4)
                    {
                        //voterModel.DOB = DateTime.ParseExact(xlRange.Cells[i, j].Value2.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        //voterModel.DateOfBirth = xlRange.Cells[i, j].Value2.ToString();
                        voterModel.DateOfBirth = Convert.ToString(xlRange.Cells[i, j].Value);
                        DateTime cellDate;
                        if (DateTime.TryParse(xlRange.Cells[i, j].Value.ToString(), out cellDate))
                        {
                            voterModel.DOB = cellDate;
                        }
                    }
                    else if (j == 5)
                    {

                    }
                    else if (j == 6)
                    {
                        voterModel.LocationId = Convert.ToInt32(xlRange.Cells[i, j].Value2.ToString());
                        var location = Globals.GeoList.LocationList.FirstOrDefault(l => l.LocationId == voterModel.LocationId);
                        voterModel.RegionId = location.RegionId;
                        voterModel.ProvinceId = location.ProvinceId;
                        voterModel.DistrictId = location.DistrictId;
                        voterModel.LLGId = location.LLGId;
                        voterModel.WardId = location.WardId;
                    }
                    else if (j == 7)
                    {
                        voterModel.OccupationFK = Convert.ToInt32(xlRange.Cells[i, j].Value2.ToString());
                    }
                    else if (j == 8)
                    {
                        voterModel.IsManualFormNumber = true;
                        var ward = voterModel.WardId.ToString().PadLeft(4, '0');
                        var numberWithRange = xlRange.Cells[i, j].Value2.ToString().PadLeft(7, '0');
                        voterModel.EnrolmentFormNumber =  string.Format("{0}{1}{2}", voterModel.ProvinceId, ward, numberWithRange);
                    }

                    //write the value to the Grid  
                    if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                    {
                        dataGridView1.Rows[i - 1].Cells[j - 1].Value = xlRange.Cells[i, j].Value2.ToString();
                    }


                    // Console.Write(xlRange.Cells[i, j].Value2.ToString() + "\t");  

                    //add useful things here!     
                }
                voterModel.Status = "NEW";
                //voterModel.Lat = Convert.ToDecimal("-10.307078");
                //voterModel.Lat = Convert.ToDecimal("150.448345");
                //voterModel.CreatedBy = 376;

                voterModel.Lat = Convert.ToDecimal(txtLat.Text.Trim());
                voterModel.Lat = Convert.ToDecimal(txtLong.Text.Trim());
                voterModel.CreatedBy = Convert.ToInt32(txtUserId.Text.Trim());

                voterModels.Add(voterModel);
            }

            //cleanup  
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //rule of thumb for releasing com objects:  
            //  never use two dots, all COM objects must be referenced and released individually  
            //  ex: [somthing].[something].[something] is bad  

            //release com objects to fully kill excel process from running in the background  
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release  
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release  
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);

            Response responseSyncElectors = await GenericHttpClient.PostRequest("mobile/Voter/SyncElectors", voterModels);
        }

        private DataTable GetDataTableFromDGV(DataGridView dgv)
        {
            var dt = new DataTable();
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                dt.Columns.Add(column.HeaderText);
            }

            object[] cellValues = new object[dgv.Columns.Count];
            foreach (DataGridViewRow row in dgv.Rows)
            {
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    cellValues[i] = row.Cells[i].Value;
                }
                dt.Rows.Add(cellValues);
            }


            return dt;
        }

        private void ImportFromExcel_Load(object sender, EventArgs e)
        {
            ////Ward 1
            //txtLat.Text = "-10.307018";
            //txtLong.Text = "150.447816";
            //txtUserId.Text = "379";

            ////Ward 2
            //txtLat.Text = "-10.307017";
            //txtLong.Text = "150.447816";
            //txtUserId.Text = "375";

            ////Ward 3
            //txtLat.Text = "-10.307091";
            //txtLong.Text = "150.448385";
            //txtUserId.Text = "375";

            ////Ward 4
            //txtLat.Text = "-10.307071";
            //txtLong.Text = "150.448359";
            //txtUserId.Text = "375";

            ////Ward 5
            //txtLat.Text = "-10.307078";
            //txtLong.Text = "150.448345";
            //txtUserId.Text = "376";

            ////Ward 6
            //txtLat.Text = "-10.307078";
            //txtLong.Text = "150.448345";
            //txtUserId.Text = "376";

            ////Ward 7
            //txtLat.Text = "-10.309547";
            //txtLong.Text = "150.451366";
            //txtUserId.Text = "376";

            ////Ward 14
            //txtLat.Text = "-9.445970";
            //txtLong.Text = "147.188293";
            //txtUserId.Text = "379";

            //Ward 20
            txtLat.Text = "-10.307019";
            txtLong.Text = "150.447816";
            txtUserId.Text = "379";
        }
    }
}
