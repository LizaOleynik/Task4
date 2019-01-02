using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Race_v2
{
    public partial class Form1 : Form
    {
        public int state;   //  1 - гонка не началась, 2 - в процессе
        List<Car> cars;
        List<Accident> loaders;
        //Accident accident;
        List<Thread> threads;
        Graphics g;
        public delegate void MyDelegate();
        int count_cars = 3, count_cycle = 1;

        public Form1()
        {
            InitializeComponent();   
            cars = new List<Car>();
            loaders = new List<Accident>();
            threads = new List<Thread>();
            g = CreateGraphics();
            state = 1;
        }


        void Movement(object obj)
        {
            Car car = (Car)obj;

            while (car.count_cycle < count_cycle)
            {
                if (!car.crash)
                {
                    Cars(car);
                    Thread.Sleep(20);
                    Invalidate();
                }
            }
        }

        void SpawnCrash(Car car)
        {
            Accident loader = new Accident();
            loader.X = car.X;
            loader.Y = Height;
            loader.startPointY = 300;
            loader.car = car;
            loaders.Add(loader);
            Thread t = new Thread(new ParameterizedThreadStart(Loader));
            t.Start(loader);
        }

        void Loader(object ld)
        {
            Accident l = (Accident)ld;
            l.Crash = true;
            while (l.Y >= l.car.Y)
            {
                l.Move();
                Thread.Sleep(10);
                Invalidate();
            }
            lock (l.car)
            {
                Thread.Sleep(1500);
                l.car.crash = false;
            }

            while (l.Y <= l.startPointY)
            {
                l.MoveBack();
                Thread.Sleep(20);
                Invalidate();
            }

            lock (loaders)
            {
                loaders.Remove(l);
            }
        }

        private void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.LightGray, new Rectangle(50, 50, 550, 300));
            //g.FillRectangle(Brushes.LightGray, new Rectangle(30, 30, 590, 340));
            g.FillRectangle(Brushes.WhiteSmoke, new Rectangle(count_cars * 20 + 50, count_cars * 20 + 50, 550 - count_cars * 40, 300 - count_cars * 40));

            for (int i = 1; i < count_cars; i++)
                g.DrawRectangle(Pens.White, new Rectangle(50 + i * 20, 50 + i * 20, 550 - i * 40, 300 - i * 40));

            g.DrawRectangle(Pens.Red, new Rectangle(550 - count_cars * 20, 350 - count_cars * 20, 20, count_cars * 20));
            for (int i = 0; i < cars.Count; i++)
            {
                g.FillEllipse(Brushes.Crimson, new Rectangle(cars[i].X, cars[i].Y, 10, 10));
                g.DrawString((i + 1).ToString(), new Font("Times New Roman", 12, FontStyle.Regular), new SolidBrush(Color.Black), cars[i].X + 10, cars[i].Y - 7);
                if (cars[i].X == 495 && cars[i].Y == 295 + i * 20)
                    cars[i].IncCount();

                if (cars[i].count_cycle == count_cycle)
                {
                    label1.Text = "Победитель: " + (i + 1).ToString();
                    state = 1;
                    foreach (Thread t in threads)
                    {
                        t.Abort();
                    }
                    threads.Clear();
                    cars.Clear();
                    loaders.Clear();
                    SF.Text = "Старт";
                    break;
                }

            }
            if (state == 2)
                foreach (Accident l in loaders)
                    if (l.Crash)
                        g.FillEllipse(Brushes.Red, l.X, l.Y, 10, 10);
        }


        private void SF_Click(object sender, EventArgs e)
        {
            foreach (Thread t in threads)
                t.Abort();
            threads.Clear();
            cars.Clear();
            loaders.Clear();
            if (SF.Text == "Старт")
            {
                for (int i = 0; i < count_cars; i++)
                    cars.Add(new Car(495, 295 + i * 20, i));
                int[] num = new int[count_cars];
                for (int i = 0; i < cars.Count; i++)
                {
                    Thread t = new Thread(new ParameterizedThreadStart(Movement));
                    cars[i].GetCrash += SpawnCrash;
                    threads.Add(t);
                    threads[i].Start(cars[i]);
                }
                state = 2;
                SF.Text = "Стоп";
                label1.Text = "Вперёд!";
            }
            else
            {
                state = 1;
                SF.Text = "Старт";
                label1.Text = "";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw(e.Graphics);
        }

        void Cars(Car c)
        {
            int i = c.number;
            if (c.X == 545 + i * 20 && c.Y == 295 + i * 20)
                c.Left();
            else if (c.X == 95 - i * 20 && c.Y == 295 + i * 20)
                c.Up();
            else if (c.X == 95 - i * 20 && c.Y == 95 - i * 20)
                c.Right();
            else if (c.X == 545 + i * 20 && c.Y == 95 - i * 20)
                c.Down();
            c.Move();
        }
    }
}
