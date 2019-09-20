using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Hein.Framework.Serialization.Tests
{
    public static class ExpectedOutcome
    {
        public const string FarmA_Json1 = "{\"Tractors\":[{\"Type\":\"New Holland\",\"Year\":2012}],\"Trees\":[{\"TreeId\":4,\"Type\":\"Candy Crisp Apple\"},{\"TreeId\":7,\"Type\":\"Granny Smith Apple\"}]}";
        public const string FarmA_Json2 = "{\"tractors\":[{\"type\":\"New Holland\",\"year\":2012}],\"trees\":[{\"treeId\":4,\"type\":\"Candy Crisp Apple\"},{\"treeId\":7,\"type\":\"Granny Smith Apple\"}]}";
        public const string FarmA_Xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><Farm><Tractors><Tractor><Type>New Holland</Type><Year>2012</Year></Tractor></Tractors><Trees><Tree><TreeId>4</TreeId><Type>Candy Crisp Apple</Type></Tree><Tree><TreeId>7</TreeId><Type>Granny Smith Apple</Type></Tree></Trees></Farm>";
        public const string FarmA_Soap = "<?xml version=\"1.0\" encoding=\"utf-8\"?><soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\"><soap:Body><Farm><Tractors><Tractor><Type>New Holland</Type><Year>2012</Year></Tractor></Tractors><Trees><Tree><TreeId>4</TreeId><Type>Candy Crisp Apple</Type></Tree><Tree><TreeId>7</TreeId><Type>Granny Smith Apple</Type></Tree></Trees></Farm></soap:Body></soap:Envelope>";
    }

    public static class TestObject
    {
        public static Farm FarmA
        {
            get
            {
                var farm = new Farm();
                farm.Add(new Tractor() { Type = "New Holland", Year = 2012 });
                farm.Add(new Tree() { Type = "Candy Crisp Apple", TreeId = 4 });
                farm.Add(new Tree() { Type = "Granny Smith Apple", TreeId = 7 });
                return farm;
            }
        }
    }

    public class Tractor
    {
        public string Type { get; set; }
        public int Year { get; set; }
    }

    public class Tree
    {
        public int TreeId { get; set; }
        public string Type { get; set; }
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
