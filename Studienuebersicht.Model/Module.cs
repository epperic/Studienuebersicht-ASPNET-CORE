namespace Studienuebersicht.Model
{
    public class Module : Entity
    {
        public string Name { get; set; }
        public string Professor { get; set; }
        public int Ects { get; set; }
        public double Grade { get; set; }
        public int Semester { get; set; }
    }
}
