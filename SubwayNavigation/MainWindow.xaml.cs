using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SubwayNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SubwayGraph subwayGraph = new SubwayGraph();
        private readonly List<System.Windows.Shapes.Path> stationsPathMark = new List<System.Windows.Shapes.Path>();
        private List<Station> resultPath = new List<Station>();
        private EllipseGeometry movingPoint = new EllipseGeometry();
        private double radius = 6;

        public MainWindow()
        {
            InitializeComponent();
            NameScope.SetNameScope(this, new NameScope());

            subwayGraph.GetGraphDataFromXML();
            DrawStationOnCanvas();
            AddStationLabels();
            FillComboBoxes();

            RegisterName("movingPoint", movingPoint);
            movingPoint.RadiusX = radius;
            movingPoint.RadiusY = radius;
        }

        /// <summary>
        /// Add stations to ComboBoxes 
        /// </summary>
        public void FillComboBoxes()
        {
            cbFromStation.ItemsSource = subwayGraph.stations;
            cbToStation.ItemsSource = subwayGraph.stations;
        }

        /// <summary>
        /// List all stations on the way in ListView
        /// </summary>
        /// <param name="way"></param>
        public void ShowPathInListView(List<Station> way)
        {
            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = System.IO.Path.Combine(projectPath, "Resources");
            for (int i = 0; i < way.Count; i++)
            {
                if (i == 0)
                {
                    if (way[i].stationLine != way[i + 1].stationLine)
                    {
                        way[i].stationStatus = filePath + "\\" + way[i].stationLine + "-start-passage.png";
                    }
                    else
                    {
                        way[i].stationStatus = filePath + "\\" + way[i].stationLine + "-start.png";
                    }
                }
                else if (i == way.Count - 1)
                {
                    way[i].stationStatus = filePath + "\\" + way[i].stationLine + "-end.png";
                }
                else
                {
                    if (way[i].stationLine != way[i + 1].stationLine)
                    {
                        way[i].stationStatus = filePath + "\\" + way[i].stationLine + "-passage.png";
                    }
                    else
                    {
                        way[i].stationStatus = filePath + "\\" + way[i].stationLine + "-normal.png";
                    }
                }
            }
            lvWay.ItemsSource = way;
        }

        bool firstTime = true;
        /// <summary>
        /// Draw way from start station to end station
        /// </summary>
        /// <param name="resultPath"></param>
        public void ShowWay(List<Station> resultPath)
        {
            if (!firstTime)
            {
                RemoveWay("resultWay");
            }
            Polyline way = new Polyline();
            way.Stroke = (SolidColorBrush)(new BrushConverter().ConvertFrom("#840032"));
            way.Name = "resultWay";
            way.StrokeThickness = 7.5;

            List<Point> pathPoints = DetalizePathDisplaying(resultPath);
            for (int i = 0; i < pathPoints.Count; i++)
            {
                way.Points.Add(pathPoints[i]);
            }
            way.StrokeDashArray = new DoubleCollection() { 0.2 };
            Panel.SetZIndex(way, 2);
            canvasSubway.Children.Add(way);
            firstTime = false;
        }

        /// <summary>
        /// Detalize the path from the start station to the end station for better animation and shown way
        /// </summary>
        /// <param name="resultPath"></param>
        /// <returns></returns>
        public static List<Point> DetalizePathDisplaying(List<Station> resultPath)
        {
            List<Point> pathPoints = new List<Point>();
            for (int i = 0; i < resultPath.Count; i++)
            {
                pathPoints.Add(resultPath[i].stationLocation);
                if (i + 1 < resultPath.Count)
                {
                    if ((resultPath[i].stationId == 9 && resultPath[i + 1].stationId == 10) ||
                        (resultPath[i].stationId == 10 && resultPath[i + 1].stationId == 9))
                    {
                        pathPoints.Add(new Point(293, 215.8));
                    }
                    else if ((resultPath[i].stationId == 11 && resultPath[i + 1].stationId == 12) ||
                        (resultPath[i].stationId == 12 && resultPath[i + 1].stationId == 11))
                    {
                        pathPoints.Add(new Point(235.5, 216));
                    }
                    else if ((resultPath[i].stationId == 16 && resultPath[i + 1].stationId == 18) ||
                        (resultPath[i].stationId == 18 && resultPath[i + 1].stationId == 16))
                    {
                        pathPoints.Add(new Point(211.3, 217));
                    }
                    else if ((resultPath[i].stationId == 16 && resultPath[i + 1].stationId == 27) ||
                        (resultPath[i].stationId == 27 && resultPath[i + 1].stationId == 16))
                    {
                        pathPoints.Add(new Point(240.3, 194));
                    }
                    else if ((resultPath[i].stationId == 38 && resultPath[i + 1].stationId == 39) ||
                        (resultPath[i].stationId == 39 && resultPath[i + 1].stationId == 38))
                    {
                        pathPoints.Add(new Point(264, 320));
                    }
                }
            }
            return pathPoints;
        }

        /// <summary>
        /// Remove way from start station to end station
        /// </summary>
        /// <param name="name"></param>
        public void RemoveWay(string name)
        {
            var way = (UIElement)LogicalTreeHelper.FindLogicalNode(canvasSubway, name);
            canvasSubway.Children.Remove(way);
        }

        /// <summary>
        /// Draw stations on canvas
        /// </summary>
        public void DrawStationOnCanvas()
        {
            NameScope.GetNameScope(this);
            foreach (Station st in subwayGraph.stations)
            {
                System.Windows.Shapes.Path stationPath = new System.Windows.Shapes.Path();
                string name = "s" + Convert.ToString(st.stationId);
                RegisterName(name, stationPath);
                stationPath.Name = name;
                stationPath.Data = new EllipseGeometry(st.stationLocation, radius, radius);
                if (st.stationLine == "green")
                {
                    stationPath.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#71BA51"));
                }
                else if (st.stationLine == "blue")
                {
                    stationPath.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#2C82C9"));
                }
                else
                {
                    stationPath.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#D1213E"));
                }

                stationsPathMark.Add(stationPath);

            }
            foreach (System.Windows.Shapes.Path path in stationsPathMark)
            {
                Panel.SetZIndex(path, 3);
                canvasSubway.Children.Add(path);
            }
        }

        /// <summary>
        /// Add label with station names
        /// </summary>
        public void AddStationLabels()
        {
            foreach (Station st in subwayGraph.stations)
            {
                Label stationLabel = new Label();
                stationLabel.FontSize = 7.5;
                stationLabel.Foreground = (SolidColorBrush)(new BrushConverter().ConvertFrom("#363732"));
                stationLabel.Visibility = Visibility.Visible;
                stationLabel.Content = st.stationName;

                switch (st.stationLine)
                {
                    case "blue":
                        stationLabel = HandleBlueLineStation(st, stationLabel);
                        break;
                    case "red":
                        stationLabel = HandleRedLineStation(st, stationLabel);
                        break;
                    case "green":
                        stationLabel = HandleGreenLineStation(st, stationLabel);
                        break;
                }
                canvasSubway.Children.Add(stationLabel);
            }
        }

        private Label HandleBlueLineStation(Station st, Label stationLabel)
        {
            if (st.stationId >= 39 && st.stationId <= 43)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 3);
                stationLabel.RenderTransform = new RotateTransform(30);
            }
            else if (st.stationId <= 39)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X + 8);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 10);
                stationLabel.RenderTransform = new RotateTransform(30);
            }
            else if (st.stationId > 43)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 3);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 10);
                stationLabel.RenderTransform = new RotateTransform(-30);
            };
            return stationLabel;
        }

        private Label HandleRedLineStation(Station st, Label stationLabel)
        {
            if (st.stationId >= 18 && st.stationId < 27)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 3);
                stationLabel.RenderTransform = new RotateTransform(30);
            }
            else if (st.stationId == 16)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 20);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 40);
                stationLabel.RenderTransform = new RotateTransform(45);
            }
            else if (st.stationId == 27)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 30);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 50);
                stationLabel.RenderTransform = new RotateTransform(45);
            }
            else
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 10);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 15);
                stationLabel.RenderTransform = new RotateTransform(-30);
            }
            return stationLabel;
        }

        private Label HandleGreenLineStation(Station st, Label stationLabel)
        {
            Canvas.SetLeft(stationLabel, st.stationLocation.X + 2);
            Canvas.SetTop(stationLabel, st.stationLocation.Y - 13);
            if (st.stationId >= 12)
            {
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 8);
                if (st.stationId == 12)
                {
                    Canvas.SetLeft(stationLabel, st.stationLocation.X - 56);
                }
                else if (st.stationId == 13)
                {
                    Canvas.SetLeft(stationLabel, st.stationLocation.X - 50);
                }
                else if (st.stationId == 14)
                {
                    Canvas.SetLeft(stationLabel, st.stationLocation.X - 58);
                }
                else if (st.stationId == 15)
                { Canvas.SetLeft(stationLabel, st.stationLocation.X - 32); }
            }
            else if (st.stationId == 10)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 2);
                Canvas.SetTop(stationLabel, st.stationLocation.Y - 17);
            }
            else if (st.stationId == 11)
            {
                Canvas.SetLeft(stationLabel, st.stationLocation.X - 48);
                Canvas.SetTop(stationLabel, st.stationLocation.Y + 1);
                stationLabel.RenderTransform = new RotateTransform(-5);
            }
            return stationLabel;
        }

        /// <summary>
        /// Reset path displaying
        /// </summary>
        public void ResetPathDisplay()
        {
            startStationMark.Stroke = Brushes.Transparent;
            endStationMark.Stroke = Brushes.Transparent; 
            RemoveWay("resultWay");
            lvWay.ItemsSource = null;
            movingPoint.RadiusX = 0;
            movingPoint.RadiusY = 0;
            reset = false;
        }

        /// <summary>
        /// Create animation for moving from start station to end station
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="way"></param>
        public void AnimatePath(List<Station> way)
        {
            int pathStartId = stationsPathMark.FindIndex(x => x.Name == startStationMark.Name);

            System.Windows.Shapes.Path movingObjectPath = new System.Windows.Shapes.Path();
            movingPoint.RadiusX = radius;
            movingPoint.RadiusY = radius;
            Panel.SetZIndex(movingObjectPath, 6);
            movingObjectPath.Data = movingPoint;
            movingObjectPath.Fill = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F5B700"));

            canvasSubway.Children.Add(movingObjectPath);

            PathGeometry animationPath = new PathGeometry();
            PathFigure pathWay = new PathFigure();
            pathWay.StartPoint = subwayGraph.stations[pathStartId].stationLocation;
            PolyLineSegment pathLine = new PolyLineSegment();
            List<Point> pathPoints = DetalizePathDisplaying(way);
            for (int i = 0; i < pathPoints.Count; i++)
            {
                pathLine.Points.Add(pathPoints[i]);
            }
            pathWay.Segments.Add(pathLine);
            animationPath.Figures.Add(pathWay);

            PointAnimationUsingPath animation = new PointAnimationUsingPath();
            animation.PathGeometry = animationPath;
            if (way.Count <= 6)
            {
                animation.Duration = TimeSpan.FromSeconds(1);
            }
            else
            {
                animation.Duration = TimeSpan.FromSeconds(3.5);
            }

            Storyboard.SetTargetName(animation, "movingPoint");
            Storyboard.SetTargetProperty(animation, new PropertyPath(EllipseGeometry.CenterProperty));
            Storyboard pathAnimationStoryboard = new Storyboard();
            pathAnimationStoryboard.Children.Add(animation);

            movingObjectPath.Loaded += delegate (object sender, RoutedEventArgs e)
            {
                pathAnimationStoryboard.Begin(this);
            };
        }

        System.Windows.Shapes.Path startStationMark, endStationMark;
        bool startClicked, endClicked, reset;
        public void canvasSubway_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (reset)
            {
                ResetPathDisplay();
                cbFromStation.SelectedIndex = cbToStation.SelectedIndex = -1;
            }

            var mouseWasDownOn = e.Source as FrameworkElement;
            string elementName = mouseWasDownOn.Name;
            if (!startClicked)
            {
                startStationMark = (System.Windows.Shapes.Path)FindName(elementName);
                if (startStationMark != null)
                {
                    startStationMark.Stroke = Brushes.Green;
                    startStationMark.StrokeThickness = 2;
                    startClicked = true;
                }
            }
            else if (!endClicked && startStationMark != null)
            {
                endStationMark = (System.Windows.Shapes.Path)FindName(elementName);
                if (endStationMark != null)
                {
                    endStationMark.Stroke = Brushes.DarkRed;
                    endStationMark.StrokeThickness = 2;
                    endClicked = true;
                }
            }

            if ((startClicked && endClicked) && (startStationMark != null && endStationMark != null))
            {
                int pathStartId = stationsPathMark.FindIndex(x => x.Name == startStationMark.Name);
                int pathEndId = stationsPathMark.FindIndex(x => x.Name == endStationMark.Name);
                if (pathStartId != pathEndId)
                {
                    resultPath = subwayGraph.GetShortestPath(subwayGraph.stations[pathStartId], subwayGraph.stations[pathEndId]);
                    ShowPathInListView(resultPath);
                    ShowWay(resultPath);
                    AnimatePath(resultPath);
                    reset = true;
                }
                else
                {
                    startStationMark.Stroke = Brushes.Transparent;
                    endStationMark.Stroke = Brushes.Transparent;
                }
                startClicked = endClicked = false;
            }
        }

        public void btShowPath_Click(object sender, RoutedEventArgs e)
        {
            if (cbFromStation.SelectedIndex != -1 && cbToStation.SelectedIndex != -1)
            {
                if (startClicked)
                {
                    startStationMark.Stroke = Brushes.Transparent;
                    startClicked = false;
                }
                else if (reset)
                {
                    ResetPathDisplay();
                }


                Station fromStation = (Station)cbFromStation.SelectedItem;
                Station toStation = (Station)cbToStation.SelectedItem;
                startStationMark = (System.Windows.Shapes.Path)FindName("s" + fromStation.stationId);
                endStationMark = (System.Windows.Shapes.Path)FindName("s" + toStation.stationId);

                if (fromStation != toStation)
                {
                    List<Station> result = subwayGraph.GetShortestPath(subwayGraph.stations[fromStation.stationId],
                        subwayGraph.stations[toStation.stationId]);

                    startStationMark.Stroke = Brushes.Green;
                    startStationMark.StrokeThickness = 2;
                    endStationMark.Stroke = Brushes.DarkRed;
                    endStationMark.StrokeThickness = 2;

                    ShowPathInListView(result);
                    ShowWay(result);
                    AnimatePath(result);
                    reset = true;
                }
                else if (fromStation == toStation)
                {
                    MessageBox.Show("You're already at you destination point - " + toStation.stationName);
                }
            }
        }
    }
}
