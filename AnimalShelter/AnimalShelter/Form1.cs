using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnimalShelter
{
    public partial class Form1:Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in panel2.Controls)
            {
                if (!(ctr is Label))
                    ctr.Text = "";
            }
        }
    }
    public class Animal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Species { get; set; }
        public string State { get; set; }
        public Animal() {}
        public Animal(int id, string name, int age, string species, string state)
        {
            ID = id;
            Name = name;
            Age = age;
            Species = species;
            State = state;
        }
        public virtual string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Age: {Age}, Species: {Species}, State: {State}";
        }
    }

    public class Dog:Animal
    {
        public Dog() {}
        public Dog(int id, string name, int age, string species, string state)
            : base(id, name, age, species, state) { }
        public override string ToString()
        {
            return $"Dog - {base.ToString()}";
        }
    }
    public class Cat:Animal
    {
        public Cat() {}
        public Cat(int id, string name, int age, string species, string state)
            : base(id, name, age, species, state) { }
        public override string ToString()
        {
            return $"Cat - {base.ToString()}";
        }
    }

    public class Hamster:Animal
    {
        public Hamster() {}
        public Hamster(int id, string name, int age, string species, string state)
            : base(id, name, age, species, state) { }
        public override string ToString()
        {
            return $"Hamster - {base.ToString()}";
        }
    }

    




}
