using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PaylAllJakubTest
{
    public partial class MainWindow : Window
    {
        private int totalRectCount;
        private SolidColorBrush onBrush = new SolidColorBrush(Colors.HotPink);
        private SolidColorBrush offBrush = new SolidColorBrush(Colors.DarkViolet);

        int[] gridLayout = new int[]
        {
            0,0,0,0,0,
            1,1,0,1,1,
            0,0,0,0,0,
            1,0,0,0,1,
            1,1,0,1,1
        };        

        public MainWindow()
        {
            InitializeComponent();
            int rows = MainGrid.RowDefinitions.Count;
            int columns = MainGrid.ColumnDefinitions.Count;
            int gridLayoutCount = 0;

            //create grid annd insert rectangles
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    Rectangle rect = new Rectangle();
                    if (gridLayout[gridLayoutCount] == 1)
                    {
                        rect.Fill = onBrush;
                    }
                    else
                    {
                        rect.Fill = offBrush;
                    }

                    gridLayoutCount++;                    
                    rect.Margin = new Thickness(5);
                    rect.MouseUp += new MouseButtonEventHandler(Button_MouseUp);    //subscribe to event onMouseUp
                    Grid.SetColumn(rect, column);   //set correct column
                    Grid.SetRow(rect, row); //set correct row
                    MainGrid.Children.Add(rect);    //add to children of grid
                    totalRectCount++;   //add the rect count
                }
            }
        }
        /// <summary>
        /// When pressed it changes recatbngle to on or off. Includes surrounding recantgles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle rect = sender as Rectangle;
            var col = Grid.GetColumn(rect);
            var row = Grid.GetRow(rect);

            if (rect.Fill == offBrush)
            {
                rect.Fill = onBrush;
            }
            else
            {
                rect.Fill = offBrush;
            }
            
            SetNeighbourRects(col,row); 
        }

        /// <summary>
        /// Sets surrounding rectangles to change colour
        /// </summary>
        /// <param name="col"></param>
        /// <param name="row"></param>
        private void SetNeighbourRects(int col, int row)
        { 
            int newCol = 0;
            int newRow = 0;
            int rows = MainGrid.RowDefinitions.Count;
            int columns = MainGrid.ColumnDefinitions.Count;

            for (int i = 0; i < 4; i++) //this for loop iterates 4 times to change each rectangle approprietly
            {
                if (i == 0) 
                {
                    newCol = col - 1;
                    newRow = row;
                }
                if (i == 1)
                {
                    newCol = col + 1;
                    newRow = row;
                }
                if (i == 2)
                {
                    newCol = col;
                    newRow = row + 1;
                }
                if (i == 3)
                {
                    newCol = col;
                    newRow = row - 1;
                }
               
                foreach (var item in MainGrid.Children)
                {
                    Rectangle rect = item as Rectangle;

                    if (Grid.GetColumn(rect) == newCol)
                    {
                        if (Grid.GetRow(rect) == newRow)
                        {
                            if (rect.Fill == offBrush)
                            {
                                rect.Fill = onBrush;
                            }
                            else
                            {
                                rect.Fill = offBrush;                                
                            }                        
                        }
                    }            
                }
            }
            
            CheckIfAllOff();
        }
        /// <summary>
        /// Checks if all rectangles are off
        /// </summary>
        private void CheckIfAllOff()
        {
            int offRectCount = 0;

            foreach (Rectangle rect in MainGrid.Children)
            {               
                if (rect.Fill == offBrush)
                {
                    offRectCount++;
                }
            }

            if (totalRectCount == offRectCount)
            {               
                MessageBox.Show("Winner!");
                foreach (Rectangle rect in MainGrid.Children)
                {                    
                    rect.MouseUp -= Button_MouseUp; //unsubscribe from event
                }

                Environment.Exit(1);
            }
        }
                
    }

}

