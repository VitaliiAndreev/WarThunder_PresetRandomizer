using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using NHibernate.Mapping.Attributes;

namespace Core.DataBase.Tests.Mapping.OneClass.IdAndName.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithIdAndName))]
    public class PersistentObjectFakeWithIdAndName : PersistentObjectFakeWithId
    {
        private string _name;

        [Id(Column = nameof(Id), TypeType = typeof(string), Name = nameof(Id))]
        public override string Id
        {
            get { return _id; }
            protected set { _id = value; }
        }

        [Property()]
        public virtual string Name
        {
            get { return _name; }
            protected set { _name = value; }
        }

        protected PersistentObjectFakeWithIdAndName()
        {
        }

        public PersistentObjectFakeWithIdAndName(string id, string name)
            : base(id)
        {
            _name = name;
        }
    }
}