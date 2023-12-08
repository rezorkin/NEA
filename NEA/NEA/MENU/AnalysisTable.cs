using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NEA.DOMAIN;

namespace NEA.MENU
{
    internal class AnalysisTable : Table
    {
        List<Medicine> sample;
        L
        public AnalysisTable(int pageLength, List<Medicine> sample) : base(pageLength)
        {
            this.sample = sample;
        }
        protected override string[] attributes => throw new NotImplementedException();
        public override void FilterRows()
        {
            throw new NotImplementedException();
        }

        public override void MakeChoice()
        {
            throw new NotImplementedException();
        }

        public override void SortRows()
        {
            throw new NotImplementedException();
        }
        public override void Select()
        {
            throw new NotImplementedException();
        }


    }
}
