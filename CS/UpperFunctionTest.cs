using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Data.Filtering;

namespace UpperFunction {
    public class UpperFunction {
        public void Run() {
            IDataLayer dl = XpoDefault.GetDataLayer(AutoCreateOption.DatabaseAndSchema);
            UnitOfWork uof = new UnitOfWork(dl);
            uof.ClearDatabase();
            uof.UpdateSchema();
            MyObject obj1 = new MyObject(uof);
            obj1.MyCaseSensitiveProperty = "ab";
            MyObject obj2 = new MyObject(uof);
            obj2.MyCaseSensitiveProperty = "AB";
            MyObject obj3 = new MyObject(uof);
            obj3.MyCaseSensitiveProperty = "aB";
            MyObject obj4 = new MyObject(uof);
            obj4.MyCaseSensitiveProperty = "BC";
            MyObject obj5 = new MyObject(uof);
            obj5.MyCaseSensitiveProperty = "Cb";
            uof.CommitChanges();

            // Test UPPER
            CriteriaOperator filter = CriteriaOperator.Parse("Upper([MyCaseSensitiveProperty]) = ?", "AB");
            XPCollection<MyObject> collection = new XPCollection<MyObject>(uof, filter);

            // Test LOWER
            filter = CriteriaOperator.Parse("Lower([MyCaseSensitiveProperty]) = ?", "ab");
            collection = new XPCollection<MyObject>(uof, filter);

            // Test SUBSTRING
            filter = CriteriaOperator.Parse("Substring([MyCaseSensitiveProperty], 0, 1) = ?", "c");
            collection = new XPCollection<MyObject>(uof, filter);
        }
    }

    public class UpperFunctionTest : UpperFunction {
        public new void Run() {
            base.Run();
        }
    }

    class MyObject : XPObject {
        public MyObject(Session session) : base(session) { }

        string myCaseSensitiveValue;
        public string MyCaseSensitiveProperty {
            get { return myCaseSensitiveValue; }
            set { SetPropertyValue("MyCaseSensitiveProperty", ref myCaseSensitiveValue, value); }
        }
    }
}
