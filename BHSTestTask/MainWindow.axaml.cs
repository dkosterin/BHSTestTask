using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using System;
using System.Numerics;

namespace BHSTestTask
{
    public partial class MainWindow : Window
    {
        Scene CreateScene()
        {
            var scene = new Scene();

            scene.AddObject(new Wall(new Vector2(0, 0), new Vector2(10, 0)));
            scene.AddObject(new Wall(new Vector2(10, 0), new Vector2(10, 10)));
            scene.AddObject(new Wall(new Vector2(10, 10), new Vector2(5, 15)));
            scene.AddObject(new Wall(new Vector2(5, 15), new Vector2(0, 10)));
            scene.AddObject(new Wall(new Vector2(0, 10), new Vector2(0, 0)));
            scene.AddObject(new Ball(new Vector2(5, 5), new Vector2(1, 0.5f), 0.5f));

            return scene;
        }
        public MainWindow()
        {
            InitializeComponent();

            var scene = CreateScene();
            var manager = new EcsManager(scene);
            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(16)
            };
            timer.Tick += (s, e) =>
            {
                manager.RunSystem();
                RenderScene(scene, 20);
            };
            timer.Start();
        }
        void RenderScene(Scene scene, float scale)
        {
            float originX = 100;
            float originY = 100;
            var canvas = this.FindControl<Canvas>("SceneCanvas");
            canvas.Children.Clear();

            foreach (var obj in scene.GetObjects())
            {
                if (obj is Wall wall)
                {
                    var line = new Line
                    {
                        StartPoint = new Avalonia.Point(originX + wall.Begin.X * scale, canvas.Height - originY - wall.Begin.Y * scale),
                        EndPoint = new Avalonia.Point(originX + wall.End.X * scale, canvas.Height - originY - wall.End.Y * scale),
                        Stroke = Brushes.Black,
                        StrokeThickness = 2
                    };

                    canvas.Children.Add(line);
                }
                if (obj is Ball ball)
                {
                    var ellipse = new Ellipse
                    {
                        Width = 2 * ball.Radius * scale,
                        Height = 2 * ball.Radius * scale,
                        Fill = Brushes.Red
                    };

                    canvas.Children.Add(ellipse);
                    Canvas.SetBottom(ellipse, originY + (ball.Center.Y - ball.Radius) * scale);
                    Canvas.SetLeft(ellipse, originX + (ball.Center.X - ball.Radius) * scale);
                }
            }
        }
    }
}