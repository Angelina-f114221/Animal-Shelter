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
        ArrayList animalList; // Създавам си масив, в който да съхранявам записите
        int Id = 1; // създавам си променлива брояч, която да отговаря за id-то, за да е уникален всеки запис
        public Form1()
        {
            InitializeComponent();
            animalList = new ArrayList();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); // при кликване на бутона exit, затваряме цялото приложение. 
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            foreach (Control ctr in panel2.Controls) // обхождаме всички контроли в панел 2, като изчистваме информацията само от контролите, които не са тип label. това се прави за улеснение на потребителя, за да не трие ръчно всичко. също така следва да внимаваме кои контроли трием, за да не изтрием самите labels, които просто указват какво следва да въведем като информация.
            {
                if (!(ctr is Label))
                    ctr.Text = "";
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && comboSpecies.Text != "" && numericAge.Value != 0 && comboState.Text != "") // проверявам дали всички полета са запълнени, преди да създадем запис, тъй като искаме информацията да е пълна
            {
                try // поставяме switch в конструкция try, за да следим дали ще се хвърли изключение
                {
                    switch (comboSpecies.Text) // минаваме през конструкцията, съобразявайки се с типа на животното.
                    {
                        case "Dog":
                            animalList.Add(new Dog(Id, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        case "Cat":
                            animalList.Add(new Cat(Id, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        case "Hamster":
                            animalList.Add(new Hamster(Id, txtName.Text, Convert.ToInt32(numericAge.Value), comboSpecies.Text, comboState.Text));
                            break;
                        default: // ако нито един от по-горните случаи не бъде достъпен, то тогава типа на животното не е от позволените
                            MessageBox.Show("Choose a species!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
                    Id++; // инкрементираме брояча на id-то за следващия запис
                    Animal lastAnimal = (Animal)animalList[animalList.Count - 1];
                    dataGridView1.Rows.Add(lastAnimal.Name, lastAnimal.ID, lastAnimal.Age, lastAnimal.Species, lastAnimal.State);
                    btnDelete.Enabled = true; // едва когато успешно е създаден запис се активират бутоните delete и update, тъй като едва тогава ще има какво да се изменя
                    btnUpdate.Enabled = true;
                }
                catch (Exception ex) // хващане на изключението и поява на съобщение
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else // ако някое от полетата не е попълнено, то се изписва съобщение, за да е наясно потребителя
            {
                MessageBox.Show("Missing data!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // следва поне един ред да е маркиран, за да можем да трием
            {
                int row = dataGridView1.SelectedRows[0].Index; // съхранява се първия ммаркиран от потребителя запис/ред, който след това се премахва от масива и от таблицата с данни, която се визуализира
                animalList.RemoveAt(row);
                dataGridView1.Rows.RemoveAt(row);
            }
            else // ако не сме маркирали редица, то се изписва съобщение
            {
                MessageBox.Show("Select a row to delete!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // отново следва да имаме маркиран поне един ред
            {
                int row = dataGridView1.SelectedRows[0].Index; // тук се съхранява маркирания от потребителя ред, създава се обект от класа Animal и в него се съхраняват новите данни от полетата. след това старият запис се изменя спрямо обекта и данните са актуални
                Animal updated = (Animal)animalList[row];

                updated.Name = txtName.Text; // изменяме данните в масива
                updated.Age = Convert.ToInt32(numericAge.Value);
                updated.Species = comboSpecies.Text;
                updated.State = comboState.Text;

                dataGridView1.Rows[row].SetValues(updated.Name, updated.ID, updated.Age, updated.Species, updated.State); // данните в създадения запис се изменят спрямо новата информация в полетата
            }
            else // ако няма избран ред, то се изписва съобщение
            {
                MessageBox.Show("Select a row to update!", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // при зареждане на приложението, задавам неговите граници и деактивирам бутоните delete и update, тъй като няма данни, върху които да се извършват операции
            this.Size = new Size(1025, 600); 
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;
        }

    }
    public class Animal // създавам базов клас Animal, който да бъде наследен от позволените типове животни. създавам и съответните член данни и конструктор без параметри, както и такъв, който да инициализира всички член данни.
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
        public virtual string ToString() // тук имаме полиморфизъм и съответна виртуална функция, която да бъде презаписана. всяко презаписване ще добавя нова конкретна информация, която да надгражда вече общата от базовия клас
        {
            return $"ID: {ID}, Name: {Name}, Age: {Age}, Species: {Species}, State: {State}";
        }
    }

    public class Dog:Animal // тук има населдяване на клас и съответно създаване на конструктор. при конструктора се извиква и базовия конструктор, който да инициализира член данните от базовия клас
    {
        public Dog() {}
        public Dog(int id, string name, int age, string species, string state)
            : base(id, name, age, species, state) { }
        public override string ToString() // осъществява се презаписването на функцията и извикването на оригиналното състояние от базовия клас
        {
            return $"Dog - {base.ToString()}";
        }
    }
    public class Cat:Animal // в следващите два класа се прилага същото както и при класа Dog
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
