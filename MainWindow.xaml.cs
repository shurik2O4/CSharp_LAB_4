using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace LAB_4 {
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : WindowEx, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #pragma warning disable CS0628 // New protected member declared in sealed type
        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #pragma warning restore CS0628

        private bool _CalcButtonEnabled = false;
        public bool CalcButtonEnabled
        {
            get { return _CalcButtonEnabled; }
            set { if (value != _CalcButtonEnabled) { _CalcButtonEnabled = value; OnPropertyChanged(nameof(CalcButtonEnabled)); } }
        }

        private string _MatrixSizeText = "";
        public string MatrixSizeText
        {
            get { return _MatrixSizeText; }
            set { if (value != _MatrixSizeText) { _MatrixSizeText = value; OnPropertyChanged(nameof(MatrixSizeText)); } }
        }

        private string _ErrorText = "Not initialized (bug 🤔)";
        public string ErrorTipText
        {
            get { return _ErrorText; }
            set { if (value != _ErrorText) { _ErrorText = value; OnPropertyChanged(nameof(ErrorTipText)); } }
        }

        public int SavedSize;
        public List<ExpandoObject> InputMatrixRendered = new();
        public List<List<int>> InputMatrix = new();

        public List<ExpandoObject> OutputMatrixRendered = new();
        public List<List<int>> OutputMatrix = new();

        public MainWindow()
        {
            InitializeComponent();
            MatrixSizeTextBox.DataContext = this;
            CalcButton.DataContext = this;
            ErrorTip.DataContext = this;

            //InputMatrixGrid.ItemsSource = InputMatrix;
            //OutputMatrixGrid.ItemsSource= OutputMatrix;
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(MatrixSizeText, out int NewMatrixSize) || NewMatrixSize <= 0)
            {
                ErrorTipText = "Invalid matrix size provided";
                ErrorTip.IsOpen = true;
                return;
            };


            //InputMatrix.Add(new Dictionary<string, int>());

            //InputMatrix.Columns.Clear();
            //InputMatrix.Columns.Add("Test", typeof(string));
            //InputMatrix.Rows.Add(new int[] {1,2,3,4,5} );
            //InputMatrix.Add(new DataGridTextColumn() { Header = "Col 1", CanUserReorder = false });
            //InputMatrix.Add(new DataGridTextColumn() { Header = "Col 2", CanUserReorder = false });
            //InputMatrix.Add(new DataGridTextColumn() { Header = "Col 3", CanUserReorder = false });
            //InputMatrix.Add(new DataGridTextColumn() { Header = "Col 4", CanUserReorder = false });


            // POG POG POG POG
            //ExpandoObject ex0 = new();
            //ex0.TryAdd("Col0", "Col 0 1");
            //ex0.TryAdd("Col1", "Col 0 2");
            //ex0.TryAdd("Col2", "Col 0 3");
            //ex0.TryAdd("Col3", "Col 0 4");
            //ex0.TryAdd("Col4", "Col 0 5");
            //InputMatrix.Add(ex0);

            //ExpandoObject ex1 = new();
            //ex1.TryAdd("Col0", "Col 1 0");
            //ex1.TryAdd("Col1", "Col 1 1");
            //ex1.TryAdd("Col2", "Col 1 2");
            //ex1.TryAdd("Col3", "Col 1 3");
            //ex1.TryAdd("Col4", "Col 1 4");
            //InputMatrix.Add(ex1);

            //ExpandoObject ex2 = new();
            //ex2.TryAdd("Col0", "Col 2 0");
            //ex2.TryAdd("Col1", "Col 2 1");
            //ex2.TryAdd("Col2", "Col 2 2");
            //ex2.TryAdd("Col3", "Col 2 3");
            //ex2.TryAdd("Col4", "Col 2 4");
            //InputMatrix.Add(ex2);

            //ExpandoObject ex3 = new();
            //ex3.TryAdd("Col0", "Col 3 0");
            //ex3.TryAdd("Col1", "Col 3 1");
            //ex3.TryAdd("Col2", "Col 3 2");
            //ex3.TryAdd("Col3", "Col 3 3");
            //ex3.TryAdd("Col4", "Col 3 4");
            //InputMatrix.Add(ex3);

            //ExpandoObject ex4 = new();
            //ex4.TryAdd("Col0", "Col 4 0");
            //ex4.TryAdd("Col1", "Col 4 1");
            //ex4.TryAdd("Col2", "Col 4 2");
            //ex4.TryAdd("Col3", "Col 4 3");
            //ex4.TryAdd("Col4", "Col 4 4");
            //InputMatrix.Add(ex4);
            // END POG POG POG

            //InputMatrix.Add(new ExpandoObject() { Col0="11" });
            //InputMatrix.Add(new ExpandoObject() { Col0="11" });
            //InputMatrix.Add(new ExpandoObject() { Col0="11" });
            //InputMatrix.Add(new ExpandoObject() { Col0="11" });


            // dynamic
            //InputMatrix.Col1 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col2 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col3 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col4 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col5 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col6 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col7 = new int[] { 1, 2, 3, 4 };
            //InputMatrix.Col8 = new int[] { 1, 2, 3, 4 };

            GenerateProgressBar.Visibility = Visibility.Visible;
            GenerateButton.IsEnabled = false;

            InputMatrix.Clear();
            OutputMatrix.Clear();
            if (OutputMatrixGrid.Columns.Count != 0) OutputMatrixGrid.Columns.Clear();

            SavedSize = NewMatrixSize;
            await Task.Run(GenerateMatrix, CancellationToken.None);


            RenderMatrix(InputMatrix, out InputMatrixRendered, InputMatrixGrid);

            GenerateProgressBar.Visibility = Visibility.Collapsed;
            GenerateButton.IsEnabled = true;

            CalcButtonEnabled = true;
        }

        private void GenerateMatrix() {
            int NewMatrixSize = SavedSize;
            for (int x = 0; x < NewMatrixSize; x++) {
                InputMatrix.Add((from v in Enumerable.Range(x + 1, NewMatrixSize) select v <= NewMatrixSize ? v : 0).ToList());
            }
            // Also remove all columns
            //for (int x = 0; x < NewMatrixSize; x++) {
            //    //Add array
            //    List<int> tmp = new();
            //    // Add numbers
            //    foreach (var v in Enumerable.Range(x + 1, NewMatrixSize - x)) {
            //        tmp.Add(v);
            //    };
            //    // Fill out with zeros
            //    foreach (var v in (from _ in Enumerable.Range(0, x) select 0)) {
            //        tmp.Add(v);
            //    }
            //    InputMatrix.Add(tmp);
            //}
        }

        public static void RenderMatrix(List<List<int>> matrix, out List<ExpandoObject> renderMatrix, DataGrid dataGrid) {
            // Disable data grid updates
            dataGrid.ItemsSource = null;
            // And clear it
            dataGrid.Columns.Clear();

            // Create new render matrix
            renderMatrix = new();

            for (int x = 0; x < matrix.Count; x++) {
                // Add rows to matrix
                renderMatrix.Add(new ExpandoObject());
                // And columns to data grid
                dataGrid.Columns.Add(new DataGridTextColumn() { Binding = new Binding() { Path = new PropertyPath($"{x}") }, IsReadOnly = true, FontSize = 15 });

                // Convert regular nested list to a list with "dynamic" object
                for (int y = 0; y < matrix[x].Count; y++) {
                    renderMatrix[x].TryAdd(y.ToString(), matrix[x][y]);
                }
            }

            // Set item source to a new render matrix
            dataGrid.ItemsSource = renderMatrix;
        }

        private void CalculateResult() {
            float halfN = SavedSize / 2;
            OutputMatrix.Clear();

            for (int x = 0; x < InputMatrix.Count; x++) {
                OutputMatrix.Add((from v in InputMatrix[x] select v < halfN ? 0 : v).ToList());
            }
        }

        private async void CalcButton_Click(object sender, RoutedEventArgs e) {
            CalculateProgressBar.Visibility = Visibility.Visible;
            CalcButtonEnabled = false;

            // Run in the backgroud thread (doesn't work properly for some reason)
            await Task.Run(CalculateResult, CancellationToken.None);

            RenderMatrix(OutputMatrix, out OutputMatrixRendered, OutputMatrixGrid);

            CalculateProgressBar.Visibility = Visibility.Collapsed;
        }

        private void Window_SizeChanged(object sender, WindowSizeChangedEventArgs args) {
            int h = (int)(args.Size.Height - 104 > 150 ? args.Size.Height - 104 : 150);

            MainGridRow.Height = new GridLength(h);

            InputMatrixGrid.MaxHeight = OutputMatrixGrid.MaxHeight = h - 30;
        }

        private void InputMatrixGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e) => OutputMatrixGrid.SelectedIndex = InputMatrixGrid.SelectedIndex;
        private void OutputMatrixGrid_SelectionChanged(object sender, Microsoft.UI.Xaml.Controls.SelectionChangedEventArgs e) => InputMatrixGrid.SelectedIndex = OutputMatrixGrid.SelectedIndex;

        private void MatrixSizeTextBox_TextChanged(object sender, Microsoft.UI.Xaml.Controls.TextChangedEventArgs e) {
            if (InputMatrixGrid.Columns.Count != 0) InputMatrixGrid.Columns.Clear();
            if (OutputMatrixGrid.Columns.Count != 0) OutputMatrixGrid.Columns.Clear();

            CalcButtonEnabled = false;
        }
    }
}