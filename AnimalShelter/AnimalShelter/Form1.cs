using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace AnimalShelter
{
    public partial class Form1:Form
    {
        ArrayList animalList;
        int nextId = 1;
        public Form1()
        {
            InitializeComponent();
            animalList = new ArrayList();
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
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && comboSpecies.Text != "" && numericAge.Value != 0 && comboState.Text != "")
            {
                try
                {
                    switch (comboSpecies.Text)
                    {
                        case "Dog":
                            animalList.Add(new Dog(nextId, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        case "Cat":
                            animalList.Add(new Cat(nextId, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        case "Hamster":
                            animalList.Add(new Hamster(nextId, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        default:
                            MessageBox.Show("Choose a species!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                    nextId++;
                    Animal lastAnimal = (Animal)animalList[animalList.Count - 1];
                    dataGridView1.Rows.Add(lastAnimal.Name, lastAnimal.ID, lastAnimal.Age, lastAnimal.Species, lastAnimal.State);
                    btnDelete.Enabled = true;
                    btnUpdate.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Missing data!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                animalList.RemoveAt(row);
                dataGridView1.Rows.RemoveAt(row);
            }
            else
            {
                MessageBox.Show("Select a row to delete!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int row = dataGridView1.SelectedRows[0].Index;
                Animal updated = (Animal)animalList[row];

                updated.Name = txtName.Text;
                updated.Age = Convert.ToInt32(numericAge.Value);
                updated.Species = comboSpecies.Text;
                updated.State = comboState.Text;

                dataGridView1.Rows[row].SetValues(updated.Name, updated.ID, updated.Age, updated.Species, updated.State);
            }
            else
            {
                MessageBox.Show("Select a row to update!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(1025, 600); 
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
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
