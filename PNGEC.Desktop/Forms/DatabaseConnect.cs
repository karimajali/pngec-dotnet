using PNGEC.API.Helper;
using PNGEC.Desktop.Model;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace PNGEC.Desktop.Forms
{
    public partial class DatabaseConnect : Form
    {
        #region Variable declaration
        /// <summary>
        /// Occurs when [database changed].
        /// </summary>
        public event EventHandler DatabaseChanged = delegate { };
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnect"/> class.
        /// </summary>
        public DatabaseConnect()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        /// <summary>
        /// Handles the load event of DatabaseConnect
        /// </summary>
        private void DatabaseConnect_Load(object sender, EventArgs e)
        {
            BindDatabase();
        }

        /// <summary>
        /// Handles the click event of btnSave
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            GlobalsModel.LoggedUser.Database = (CommonEnum.Database)cmbDatabase.SelectedValue;
            DatabaseChanged(this, new EventArgs());
        }

        #endregion

        #region Methods

        #region BindDatabase
        /// <summary>
        /// Binds the database.
        /// </summary>
        private void BindDatabase()
        {
            var lst = Enum.GetValues(typeof(CommonEnum.Database))
                .Cast<Enum>()
                .Select(value => new
                {
                    (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
                })
                .OrderBy(item => item.value)
                .ToList();

            if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.DPO)
            {
                cmbDatabase.DataSource = lst.Where(x => Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Staging)).ToList();
            }
            else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.EM)
            {
                cmbDatabase.DataSource = lst.Where(x => Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Staging) || Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Preliminary)).ToList();
            }
            //else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.Enrolment)
            //{
            //    cmbDatabase.DataSource = lst.Where(x => Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Preliminary) || Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Final)).ToList();
            //}
            else if (GlobalsModel.LoggedUser.User.Role == (int)CommonEnum.Role.RO)
            {
                cmbDatabase.DataSource = lst.Where(x => Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Staging) || Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Preliminary)).ToList();
            }
            else
            {
                cmbDatabase.DataSource = lst.Where(x => Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Staging) || Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Preliminary) || Convert.ToString(x.value) == Convert.ToString(CommonEnum.Database.Final)).ToList();
            }

            cmbDatabase.DisplayMember = "Description";
            cmbDatabase.ValueMember = "value";
            cmbDatabase.SelectedValue = GlobalsModel.LoggedUser.Database;
        }
        #endregion

        #endregion
    }
}

