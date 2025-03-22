using System.Drawing.Text;
using System.Windows.Forms;

namespace SimpleListTarea
{
    public partial class Form1 : Form
    {
        ClassSinglyLinkedList<CoffeeShop> listSinglyCoffeeShops = new ClassSinglyLinkedList<CoffeeShop>(true, false);
        ClassDoublyLinkedList<CoffeeShop> listDoublyCoffeeShops = new ClassDoublyLinkedList<CoffeeShop>(true, false);
        ClassStack<CoffeeShop> stackCoffeeShops = new ClassStack<CoffeeShop>();
        ClassQueue<CoffeeShop> queueCoffeeShops = new ClassQueue<CoffeeShop>();
        ClassBinaryTree<CoffeeShop> binaryTree = new ClassBinaryTree<CoffeeShop>();
        ClassGraphviz<CoffeeShop> myGraphViz = new ClassGraphviz<CoffeeShop>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            dtgCoffeeShops.Columns["Column10"].Visible = false;
            grpSorted.Enabled = false;
            dtgCoffeeShops.ForeColor = Color.DarkGreen;
            btnRemoveCoffeeShop.Enabled = false;
            dtgCoffeeShops.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgCoffeeShops.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dtgCoffeeShops.MultiSelect = false;
            dtgCoffeeShops.ReadOnly = true;
            label1.Text = "Tarea";
            this.StartPosition = FormStartPosition.CenterScreen;
            dtmOpening.Format = DateTimePickerFormat.Time;
            dtmOpening.ShowUpDown = true;
            dtmOpening.Value = new DateTime(2024, 1, 1, 8, 0, 0);
            dtmClosing.Format = DateTimePickerFormat.Time;
            dtmClosing.ShowUpDown = true;
            dtmClosing.Value = new DateTime(2024, 1, 1, 22, 0, 0);
            dtgCoffeeShops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            myGraphViz.ExistFiles();
            //if (myGraphViz.ExistDrawingJPG())
            //{
            //    myGraphViz.ShowGraphviz();
            //    picBSTCoffeeShops.Image = myGraphViz.ImageGraphJPG();
            //    picBSTCoffeeShops.Refresh();
            //}
            txtID.PlaceholderText = "ID CoffeeShop";
        }
        
        private void btnAddCoffeeShop_Click(object sender, EventArgs e)
        {
            CoffeeShop? newCoffeeShop;

            try
            {
                LoadDataCoffeeShop(out newCoffeeShop);
                AddObjectInStructure(newCoffeeShop!);
                ClearControlForm();
                UpdateDataGridView();

                if (radBinarySearchTree.Checked)
                {
                    UpdatePictureBTS();
                }

            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Error Message", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning); }

        }

        private void AddObjectInStructure(CoffeeShop newCoffeeShop)
        {
            if (radSinglyLinkedList.Checked) { listSinglyCoffeeShops.InsertNode(newCoffeeShop); }
            if (radDoublyLinkedList.Checked) { listDoublyCoffeeShops.InsertNode(newCoffeeShop); }
            if (radStack.Checked) { stackCoffeeShops.Push(newCoffeeShop); }
            if (radQueue.Checked)
            {
                newCoffeeShop.ArrivalTime = DateTime.Now;
                queueCoffeeShops.Enqueue(newCoffeeShop);
            }
            if (radBinarySearchTree.Checked)
            {
                binaryTree.InsertNode(newCoffeeShop);
            }
        }
        private void LoadDataCoffeeShop(out CoffeeShop? newCoffeeShop)
        {

            if (string.IsNullOrWhiteSpace(txtID.Text)) { throw new InvalidOperationException("Input Invalid In ID Field."); }
            if (string.IsNullOrWhiteSpace(txtName.Text)) { throw new InvalidOperationException("Input Invalid In Name Field."); }
            if (!double.TryParse(txtCapital.Text, out double dblCapital))
            {
                throw new InvalidOperationException("Input Invalid In Capital Field. Need Numerical Values");

            }
            if (!char.TryParse(cboClassification.Text, out char chrClassification))
            {
                throw new InvalidOperationException("Input Invalid In Classification Field.");
            }
            if (!int.TryParse(txtID.Text, out int intID)) { throw new InvalidOperationException("Input Invalid In ID Field. Only Numbers Values"); }
            newCoffeeShop = new CoffeeShop();
            newCoffeeShop.ID = int.Parse(txtID.Text);
            newCoffeeShop.Name = txtName.Text;
            newCoffeeShop.Capital = double.Parse(txtCapital.Text);
            newCoffeeShop.ServersAlcohol = chkServersAlcohol.Checked;
            newCoffeeShop.Classification = char.Parse(cboClassification.Text);
            newCoffeeShop.OpeningTime = TimeOnly.FromDateTime(dtmOpening.Value);
            newCoffeeShop.ClosingTime = TimeOnly.FromDateTime(dtmClosing.Value);

            if (pictureBox1.Image == null) { throw new InvalidOperationException("Upload Coffeshop Image"); }
            newCoffeeShop.PathImageOfCoffeeShop = txtPathPhoto.Text;
            newCoffeeShop.PhotoOfCoffeeShop = pictureBox1.Image;
            foreach (RadioButton c in grpCoffeeType.Controls)
            {
                if (c.Checked)
                {
                    newCoffeeShop.TypeCoffee = c.Text;
                }
            }


        }

        private Func<IEnumerable<CoffeeShop>>? GetStructure()
        {
            if (radStack.Checked) { return () => stackCoffeeShops.GetEnumerator(); }
            if (radQueue.Checked) { return () => queueCoffeeShops.GetEnumerator(); }
            if (radSinglyLinkedList.Checked) { return () => listSinglyCoffeeShops.GetEnumerator(); }
            if (radDoublyLinkedList.Checked)
            {
                if (radSortedOption1.Checked) { return () => listDoublyCoffeeShops.Forward; }
                else { return () => listDoublyCoffeeShops.Backwards; }
            }
            if (radBinarySearchTree.Checked)
            {
                if (radSortedOption1.Checked) { return () => binaryTree.PreOrderTraversal(); }
                if (radSortedOption2.Checked) { return () => binaryTree.InOrderTraversal(); }
                if (radSortedOption3.Checked) { return () => binaryTree.PostOrderTraversal(); }

            }
            throw new InvalidOperationException("No Structure Selected.");
        }
        private void UpdateDataGridView()
        {
            dtgCoffeeShops.Rows.Clear();

            try
            {
                Func<IEnumerable<CoffeeShop>>? DatasStructure = GetStructure();


                if (DatasStructure != null)
                {
                    foreach (CoffeeShop i in DatasStructure.Invoke())
                    {

                        int rowIndex = dtgCoffeeShops.Rows.Add(
                            i.ID, i.Name, i.Capital.ToString("C"),
                            (i.ServersAlcohol) ? "Yes" : "No", i.Classification,
                            i.OpeningTime.ToString(), i.ClosingTime.ToString(),
                            (i.PathImageOfCoffeeShop == "") ? "Not image" : i.PathImageOfCoffeeShop,
                            i.TypeCoffee, (radQueue.Checked) ? i.ArrivalTime.ToString("HH:mm:ss") : ""
                        );


                        if (binaryTree.RootNode != null && binaryTree.RootNode.ObjectType.Equals(i))
                        {
                            dtgCoffeeShops.Rows[rowIndex].DefaultCellStyle.BackColor = Color.Olive;
                            dtgCoffeeShops.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.White;

                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnRemoveCoffeeShop_Click(object sender, EventArgs e)
        {
            dtgCoffeeShops.SelectionMode = DataGridViewSelectionMode.CellSelect;
            btnRemoveCoffeeShop.Enabled = false;
            DialogResult result = MessageBox.Show("Remove Coffee Shop?\n", "Remove Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                MessageBox.Show("Operation Cancel");
                return;
            }
            LoadCoffeeShopInControls();
            CoffeeShop? newCoffeeShop;
            LoadDataCoffeeShop(out newCoffeeShop);
            if (newCoffeeShop != null)
            {

                if (radQueue.Checked)
                {

                    CoffeeShop coffeeShop = new CoffeeShop();
                    coffeeShop = GetObjectRemoved(newCoffeeShop);
                    MessageBox.Show(coffeeShop.DataCoffeeShop() + "\nArrival Time: " + coffeeShop.ArrivalTime.ToString("HH:mm:ss") + "\n" + "Waiting Time: " + coffeeShop.WaitingTime().ToString(@"mm\:ss"), "Coffee Shop Removed", MessageBoxButtons.OK);

                }
                else
                {
                    MessageBox.Show(GetObjectRemoved(newCoffeeShop).DataCoffeeShop(), "Coffee Shop Removed", MessageBoxButtons.OK);

                }

                if (radBinarySearchTree.Checked) { UpdatePictureBTS(); }
                ClearControlForm();
                UpdateDataGridView();
            }

        }

        private CoffeeShop GetObjectRemoved(CoffeeShop newCoffeeShop)
        {
            if (radSinglyLinkedList.Checked) { return listSinglyCoffeeShops.RemoveNode(newCoffeeShop); }
            if (radDoublyLinkedList.Checked) { return listDoublyCoffeeShops.RemoveNode(newCoffeeShop); }
            if (radStack.Checked) { return stackCoffeeShops.Pop(newCoffeeShop); }
            if (radQueue.Checked) { return queueCoffeeShops.Dequeue(newCoffeeShop); }
            if (radBinarySearchTree.Checked) { return binaryTree.RemoveNode(newCoffeeShop); }
            throw new Exception("");
        }
        private void LoadCoffeeShopInControls()
        {
            int intRowSelected = dtgCoffeeShops.CurrentRow.Index;
            string? strCapitalValueWithOutFormat = null;

            txtID.Text = dtgCoffeeShops.Rows[intRowSelected].Cells[0].Value.ToString();
            txtName.Text = dtgCoffeeShops.Rows[intRowSelected].Cells[1].Value.ToString();
            strCapitalValueWithOutFormat = dtgCoffeeShops.Rows[intRowSelected].Cells[2].Value.ToString();
            if (strCapitalValueWithOutFormat != null)
            {
                strCapitalValueWithOutFormat = strCapitalValueWithOutFormat.Replace("$", "").Replace(",", "");
                txtCapital.Text = strCapitalValueWithOutFormat;
            }


            if (dtgCoffeeShops.Rows[intRowSelected].Cells[3].Value.ToString() == "Yes")
            {
                chkServersAlcohol.Checked = true;
            }
            else
            {
                chkServersAlcohol.Checked = false;
            }



            cboClassification.Text = dtgCoffeeShops.Rows[intRowSelected].Cells[4].Value.ToString();
            dtmOpening.Text = dtgCoffeeShops.Rows[intRowSelected].Cells[5].Value.ToString();
            dtmClosing.Text = dtgCoffeeShops.Rows[intRowSelected].Cells[6].Value.ToString();
            string? strValidedPathImage = dtgCoffeeShops.Rows[intRowSelected].Cells[7].Value.ToString();
            if (strValidedPathImage == "Not image") { strValidedPathImage = string.Empty; }
            txtPathPhoto.Text = strValidedPathImage;
            if (!string.IsNullOrEmpty(txtPathPhoto.Text)) { pictureBox1.Image = Image.FromFile(txtPathPhoto.Text); }


            string? strCoffeeType = dtgCoffeeShops.Rows[intRowSelected].Cells[8].Value.ToString();
            switch (strCoffeeType)
            {
                case "Arabica":
                    radArabica.Checked = true;
                    break;
                case "Robusta":
                    radRobusta.Checked = true;
                    break;
                case "Mixed":
                    radMixed.Checked = true;
                    break;
                default: break;
            }
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are sure of removed all datas?\n", "Clear All Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                try
                {
                    ClearStructure();
                    if (radBinarySearchTree.Checked) { UpdatePictureBTS(); }
                    UpdateDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Operation Deleted Data Cancel", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void ClearStructure()
        {
            if (radSinglyLinkedList.Checked) { listSinglyCoffeeShops.ClearList(); }
            if (radDoublyLinkedList.Checked) { listDoublyCoffeeShops.ClearList(); }
            if (radStack.Checked) { stackCoffeeShops.Clear(); }
            if (radQueue.Checked) { queueCoffeeShops.Clear(); }
            if (radBinarySearchTree.Checked) { binaryTree.Clear(); }
        }

        private void btnGenerateRandomData_Click(object sender, EventArgs e)
        {
            Random aleatorio = new Random();
            int intChecked = 0;
            int intNumeroColor = 0;
            int intIndexCoffeeType = 0;

            for (int i = 0; i < nudCountRandomData.Value; i++)
            {
                intNumeroColor = aleatorio.Next(1, 6);
                intChecked = aleatorio.Next(0, 2);
                intIndexCoffeeType = aleatorio.Next(0, 3);

                txtID.Text = aleatorio.Next(1, 100).ToString();
                txtName.Text = Guid.NewGuid().ToString().Substring(0, 5);
                txtCapital.Text = aleatorio.Next(0, 999999).ToString();

                chkServersAlcohol.Checked = (intChecked == 1) ? true : false;
                ComboBoxClassificationInsertText();
                dtmOpening.Value = new DateTime(2024, 1, 1, aleatorio.Next(0, 24), 0, 0);
                dtmClosing.Value = new DateTime(2024, 1, 1, aleatorio.Next(0, 24), 0, 0);

                string pathPhoto = $"C:\\Datos\\ImagesTest\\Color{intNumeroColor.ToString()}.jpg";
                pictureBox1.Image = Image.FromFile(pathPhoto);
                pictureBox1.Refresh();
                txtPathPhoto.ReadOnly = false;
                txtPathPhoto.Text = pathPhoto;
                txtPathPhoto.ReadOnly = true;
                switch (intIndexCoffeeType)
                {
                    case 0:
                        radArabica.Checked = true;
                        break;
                    case 1:
                        radRobusta.Checked = true;
                        break;
                    case 2:
                        radMixed.Checked = true;
                        break;
                    default:
                        radArabica.Checked = true;
                        break;
                }

                CoffeeShop? newCoffeeShop;
                LoadDataCoffeeShop(out newCoffeeShop);
                if (newCoffeeShop != null)
                {
                    AddObjectInStructure(newCoffeeShop);
                }
            }

            if (radBinarySearchTree.Checked) { UpdatePictureBTS(); }
            UpdateDataGridView();
            ClearControlForm();
        }

        private void UpdatePictureBTS()
        {
            try
            {
                if (binaryTree.RootNode == null)
                {
                    return;
                }
                myGraphViz.CreatedDotFile(binaryTree.RootNode);
                myGraphViz.ShowGraphviz();
                Image newImage = myGraphViz.ImageGraphJPG();


                picBSTCoffeeShops.Image = newImage;
                picBSTCoffeeShops.Size = new Size(newImage.Width, newImage.Height);
                //tabDisplayInfoCoffeeShop.Size = new Size(picBSTCoffeeShops.Width, picBSTCoffeeShops.Height);
                picBSTCoffeeShops.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

            if (radBinarySearchTree.Checked)
            {

                if (tabDisplayInfoCoffeeShop.SelectedIndex == 0)
                {
                    tabDisplayInfoCoffeeShop.Size = new Size(1106, 558);
                }
                else
                {
                    tabDisplayInfoCoffeeShop.Size = new Size(picBSTCoffeeShops.Image.Width, picBSTCoffeeShops.Image.Height + 150);

                }

            }

        }
        private void ComboBoxClassificationInsertText()
        {
            Random aleatorio = new Random();
            int intComboBoxItem = 0;
            intComboBoxItem = aleatorio.Next(0, 3);
            cboClassification.SelectedIndex = intComboBoxItem;
            if (cboClassification.Text == "")
            {
                ComboBoxClassificationInsertText();
            }
        }
        private void ClearControlForm()
        {
            txtID.Text = "";
            txtPathPhoto.ReadOnly = false;
            txtPathPhoto.Text = "";
            txtPathPhoto.ReadOnly = true;
            txtName.Clear();
            txtCapital.Clear();
            chkServersAlcohol.Checked = false;
            cboClassification.Text = "";
            dtmOpening.Value = new DateTime(2024, 1, 1, 8, 0, 0);
            dtmClosing.Value = new DateTime(2024, 1, 1, 22, 0, 0);
            pictureBox1.Image = null;
            radArabica.Checked = true;
            dtgCoffeeShops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;



        }

        private void btnSearchCoffeeShop_Click(object sender, EventArgs e)
        {
            if (dtgCoffeeShops.CurrentRow != null && dtgCoffeeShops.CurrentRow.Selected)
            {
                LoadCoffeeShopInControls();
            }

            CoffeeShop? newCoffeeShop = null;

            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text) && string.IsNullOrWhiteSpace(txtName.Text))
                {
                    throw new InvalidOperationException("Please select an item from the DataGridView or enter data to search.");
                }

                LoadDataCoffeeShop(out newCoffeeShop);
                CoffeeShop coffeeShop = new CoffeeShop();
                coffeeShop = FindObject(newCoffeeShop!);
                MessageBox.Show(coffeeShop.DataCoffeeShop() + (radQueue.Checked ? "\nArrival Time: " + coffeeShop.ArrivalTime.ToString("HH:mm:ss") + "\n" + "Waiting Time: " + coffeeShop.WaitingTime().ToString(@"mm\:ss") : ""), "Coffee Shop Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException invEx)
            {
                MessageBox.Show(invEx.Message, "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }


        private CoffeeShop FindObject(CoffeeShop newCoffeeShop)
        {
            if (radSinglyLinkedList.Checked) { return listSinglyCoffeeShops.ToFindObject(newCoffeeShop); }
            if (radDoublyLinkedList.Checked) { return listDoublyCoffeeShops.ToFindObject(newCoffeeShop); }
            if (radStack.Checked) { return stackCoffeeShops.ToFindObject(newCoffeeShop); }
            if (radQueue.Checked) { return queueCoffeeShops.ToFindObject(newCoffeeShop); }
            if (radBinarySearchTree.Checked) { return binaryTree.SearchNode(newCoffeeShop); }
            throw new Exception("Not Selected Structure");
        }

        private void btnLoadPhotoOfCoffeeShop_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select an image file";
            openFileDialog.InitialDirectory = @"C:\Datos\ImagesTest";
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName == "")
            {
                MessageBox.Show("File not selected");
                return;
            }

            pictureBox1.Image = Image.FromFile(openFileDialog.FileName);
            InsertPathInTextBoxPathPhoto(openFileDialog.FileName);

        }
        private void InsertPathInTextBoxPathPhoto(string strPath)
        {
            txtPathPhoto.ReadOnly = false;
            txtPathPhoto.Text = strPath;
            txtPathPhoto.ReadOnly = true;
        }
        private void btnOpenImageLocationOfCoffeeShop_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No image to search for.");
                return;
            }
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = $"{pictureBox1.ImageLocation}";
            openFileDialog.ShowDialog();


        }

        private void btnClearControlsForm_Click(object sender, EventArgs e)
        {
            ClearControlForm();
        }

        private void dtgCoffeeShops_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dtgCoffeeShops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dtgCoffeeShops.CurrentRow != null)
            {
                //btnAddCoffeeShop.Visible = false;
                LoadCoffeeShopInControls();
                pictureBox1.Refresh();
                btnRemoveCoffeeShop.Enabled = true;
                btnSearchCoffeeShop.Enabled = true;
            }
            else
            {
                btnRemoveCoffeeShop.Enabled = false;
                btnSearchCoffeeShop.Enabled = false;

            }

        }

        private void btnSaveCoffeeShop_Click(object sender, EventArgs e)
        {
            //CoffeeShop? newCoffeeShop;
            //LoadDataCoffeeShop(out newCoffeeShop);

            //CoffeeShop ModifiqueCoffeeShop = list.ToFindNode(newCoffeeShop);
            //LoadDataCoffeeShop(out newCoffeeShop);

            //btnAddCoffeeShop.Visible = true;

        }

        private void radForwardSorted_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortedOption1.Checked)
            {
                UpdateDataGridView();
            }

        }

        private void radBackwardSorted_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortedOption2.Checked)
            {
                UpdateDataGridView();
            }

        }



        private void radSinglyLinkedList_CheckedChanged(object sender, EventArgs e)
        {
            if (radSinglyLinkedList.Checked)
            {
                dtgCoffeeShops.ForeColor = radSinglyLinkedList.ForeColor;
                UpdateDataGridView();
                grpSorted.Enabled = false;
                grpSpecialsFunctionsStack.Visible = false;
            }

        }

        private void radDoublyLinkedList_CheckedChanged(object sender, EventArgs e)
        {
            if (radDoublyLinkedList.Checked)
            {
                dtgCoffeeShops.ForeColor = radDoublyLinkedList.ForeColor;
                radSortedOption1.Text = "Forward";
                radSortedOption2.Text = "Backward";
                radSortedOption3.Visible = false;
                grpSorted.Size = new Size(300, 66);
                UpdateDataGridView();
                grpSorted.Enabled = true;
                grpSpecialsFunctionsStack.Visible = false;
            }
        }

        private void radStack_CheckedChanged(object sender, EventArgs e)
        {
            if (radStack.Checked)
            {
                dtgCoffeeShops.ForeColor = radStack.ForeColor;
                btnPeekStack.ForeColor = radStack.ForeColor;
                btnDequeue.Visible = false;
                btnRear.Visible = false;
                grpSorted.Enabled = false;
                grpSpecialsFunctionsStack.Text = "Specials Functions Stack";
                btnPopStack.Visible = true;
                grpSpecialsFunctionsStack.Visible = true;
                UpdateDataGridView();
            }
        }

        private void btnPopStack_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are sure of remove Top Data?\n", "Remove Top Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {

                try
                {

                    MessageBox.Show(stackCoffeeShops.Pop().DataCoffeeShop());
                    UpdateDataGridView();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Operation Deleted Data Cancel", "Operation Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnPeekStack_Click(object sender, EventArgs e)
        {
            try
            {
                CoffeeShop coffeeShop = new CoffeeShop();

                if (radStack.Checked)
                {

                    MessageBox.Show(stackCoffeeShops.Peek().DataCoffeeShop() + "\nArrival Time: " + coffeeShop.ArrivalTime.ToString("HH:mm:ss") + "\n" + "Waiting Time: " + coffeeShop.WaitingTime().ToString(@"mm\:ss"));
                }
                if (radQueue.Checked)
                {
                    coffeeShop = queueCoffeeShops.Peek();
                    MessageBox.Show(queueCoffeeShops.Peek().DataCoffeeShop());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radQueue_CheckedChanged(object sender, EventArgs e)
        {
            if (radQueue.Checked)
            {
                dtgCoffeeShops.Columns["Column10"].Visible = true;
                dtgCoffeeShops.ForeColor = radQueue.ForeColor;
                grpSorted.Enabled = false;
                btnDequeue.Visible = true;
                btnRear.Visible = true;
                btnPeekStack.ForeColor = radQueue.ForeColor;
                grpSpecialsFunctionsStack.Visible = true;
                btnPopStack.Visible = false;
                grpSpecialsFunctionsStack.Text = "Specials Functions Queue";
                UpdateDataGridView();

            }
            else { dtgCoffeeShops.Columns["Column10"].Visible = false; }
        }
        private void radBinarySearchTree_CheckedChanged(object sender, EventArgs e)
        {
            if (radBinarySearchTree.Checked)
            {
                dtgCoffeeShops.Rows.Clear();
                grpSorted.Enabled = true;
                grpSorted.Size = new Size(300, 96);
                radSortedOption3.Visible = true;
                radSortedOption1.Text = "Pre-Order";
                radSortedOption2.Text = "In-Order";
                radSortedOption3.Text = "Post-Order";
                btnTraverseTree.Visible = true;
                grpSpecialsFunctionsStack.Visible = false;
                dtgCoffeeShops.ForeColor = Color.Olive;
            }
            else
            {
                btnTraverseTree.Visible = false;
                tabDisplayInfoCoffeeShop.SelectedTab = tabDisplayInfoCoffeeShop.TabPages[0];
                ;
            }

        }
        private void btnRear_Click(object sender, EventArgs e)
        {
            try
            {
                CoffeeShop coffeeShop = new CoffeeShop();
                coffeeShop = queueCoffeeShops.Rear();
                MessageBox.Show(coffeeShop.DataCoffeeShop() + "\nArrival Time: " + coffeeShop.ArrivalTime.ToString("HH:mm:ss") + "\n" + "Waiting Time: " + coffeeShop.WaitingTime().ToString(@"mm\:ss"));
                UpdateDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDequeue_Click(object sender, EventArgs e)
        {
            try
            {
                CoffeeShop coffeeShop = new CoffeeShop();
                coffeeShop = queueCoffeeShops.Dequeue();

                MessageBox.Show(coffeeShop.DataCoffeeShop() + "\nArrival Time: " + coffeeShop.ArrivalTime.ToString("HH:mm:ss") + "\n" + "Waiting Time: " + coffeeShop.WaitingTime().ToString(@"mm\:ss"), "Coffee Shop Removed", MessageBoxButtons.OK);


                UpdateDataGridView();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void radSortedOption3_CheckedChanged(object sender, EventArgs e)
        {
            if (radSortedOption3.Checked)
            {
                UpdateDataGridView();
            }
        }

        private void btnTraverseTree_Click(object sender, EventArgs e)
        {
            if (radSortedOption1.Checked)
            {
                foreach (CoffeeShop i in binaryTree.PreOrderTraversal())
                {
                    MessageBox.Show(i.DataCoffeeShop());
                }

            }
            if (radSortedOption2.Checked)
            {
                foreach (CoffeeShop i in binaryTree.InOrderTraversal())
                {
                    MessageBox.Show(i.DataCoffeeShop());
                }

            }
            if (radSortedOption3.Checked)
            {
                foreach (CoffeeShop i in binaryTree.PostOrderTraversal())
                {
                    MessageBox.Show(i.DataCoffeeShop());
                }

            }
        }

        private void tabDisplayInfoCoffeeShop_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radBinarySearchTree.Checked)
            {

                if (tabDisplayInfoCoffeeShop.SelectedIndex == 0)
                {
                    tabDisplayInfoCoffeeShop.Size = new Size(1106, 558);
                }
                else
                {
                    try
                    {
                        tabDisplayInfoCoffeeShop.Size = new Size(picBSTCoffeeShops.Image.Width, picBSTCoffeeShops.Image.Height + 150);

                    }
                    catch(Exception ex) { MessageBox.Show(ex.Message); }
                }

            }
            else
            {
                tabDisplayInfoCoffeeShop.SelectedIndex = 0;
                tabDisplayInfoCoffeeShop.Size = new Size(1106, 558);

            }


        }

        private void btnMinSubDerecho_Click(object sender, EventArgs e)
        {
            if (dtgCoffeeShops.CurrentRow != null && dtgCoffeeShops.CurrentRow.Selected)
            {
                LoadCoffeeShopInControls();
            }

            CoffeeShop? newCoffeeShop = null;

            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text) && string.IsNullOrWhiteSpace(txtName.Text))
                {
                    throw new InvalidOperationException("Please select an item from the DataGridView or enter data to search.");
                }
                
                LoadDataCoffeeShop(out newCoffeeShop);
                 CoffeeShop NodeFound = binaryTree.EncontrarNodoMinSubArbolDerecho(newCoffeeShop).ObjectType;
                
                MessageBox.Show($"El objecto: {newCoffeeShop.Name} tiene como el hijo menor de sub-derecho a: \n" + NodeFound.DataCoffeeShop(),"About", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (InvalidOperationException invEx)
            {
                MessageBox.Show(invEx.Message, "Invalid Operation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }
    }
}