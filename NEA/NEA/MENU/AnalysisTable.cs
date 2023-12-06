using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.Domain;

namespace NEA.MENU
{
    internal class AnalysisTable : Table<Medicine>
    {
        SalesAnalyser SalesAnalyser;
        List<Medicine> sample;
        public AnalysisTable(int pageLength, List<Medicine> sample) : base(pageLength) 
        {
            this.sample = sample;
        }
        public override void MakeChoice()
        {
            throw new NotImplementedException();
        }

        public override void ViewAllCommands()
        {
            throw new NotImplementedException();
        }

        protected override List<string> getAttributes()
        {
            throw new NotImplementedException();
        }

        protected override List<Medicine> getItems()
        {
            throw new NotImplementedException();
        }

        protected override void Select()
        {
            throw new NotImplementedException();
        }

        protected override List<Medicine> Sort(string command, List<Medicine> sample)
        {
            throw new NotImplementedException();
        }
    }
}
