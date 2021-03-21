using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hein.Framework.Serialization.Tests
{
    public static class ExpectedOutcome
    {
        public const string FarmA_Json1 = "{\"Tractors\":[{\"Type\":\"New Holland\",\"Year\":2012,\"Id\":42}],\"Trees\":[{\"Type\":\"Candy Crisp Apple\",\"Id\":4},{\"Type\":\"Granny Smith Apple\",\"Id\":7}]}";
        public const string FarmA_Json2 = "{\"tractors\":[{\"type\":\"New Holland\",\"year\":2012,\"id\":42}],\"trees\":[{\"type\":\"Candy Crisp Apple\",\"id\":4},{\"type\":\"Granny Smith Apple\",\"id\":7}]}";
        public const string FarmA_Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Farm><Tractors><Tractor><Id>42</Id><Type>New Holland</Type><Year>2012</Year></Tractor></Tractors><Trees><Tree><Id>4</Id><Type>Candy Crisp Apple</Type></Tree><Tree><Id>7</Id><Type>Granny Smith Apple</Type></Tree></Trees></Farm>";
        public const string FarmA_Soap = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><Farm><Tractors><Tractor><Id>42</Id><Type>New Holland</Type><Year>2012</Year></Tractor></Tractors><Trees><Tree><Id>4</Id><Type>Candy Crisp Apple</Type></Tree><Tree><Id>7</Id><Type>Granny Smith Apple</Type></Tree></Trees></Farm></soap:Body></soap:Envelope>";
    }

    public static class TestObject
    {
        public static Farm FarmA
        {
            get
            {
                var farm = new Farm();
                farm.Add(new Tractor() { Type = "New Holland", Year = 2012, Id = 42 });
                farm.Add(new Tree() { Type = "Candy Crisp Apple", Id = 4 });
                farm.Add(new Tree() { Type = "Granny Smith Apple", Id = 7 });
                return farm;
            }
        }
    }

    public class Tractor : FarmItemBase
    {
        public string Type { get; set; }
        public int Year { get; set; }
    }

    public class Tree : FarmItemBase
    {
        public string Type { get; set; }
    }

    public class FarmItemBase
    {
        public int Id { get; set; }
    }

    public class Farm
    {
        public Farm()
        {
            this.Tractors = new List<Tractor>();
            this.Trees = new List<Tree>();
        }

        [XmlArray]
        [XmlArrayItem("Tractor")]
        public List<Tractor> Tractors { get; set; }
        [XmlArray]
        [XmlArrayItem("Tree")]
        public List<Tree> Trees { get; set; }

        public void Add(Tractor tractor)
        {
            var list = this.Tractors.ToList();
            list.Add(tractor);
            this.Tractors = list;
        }

        public void Add(Tree tree)
        {
            var list = this.Trees.ToList();
            list.Add(tree);
            this.Trees = list;
        }
    }
}
