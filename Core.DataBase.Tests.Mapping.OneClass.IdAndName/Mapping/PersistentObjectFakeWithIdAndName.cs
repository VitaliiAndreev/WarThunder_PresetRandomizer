using Core.DataBase.Tests.Mapping.OneClass.Id.Mapping;
using NHibernate.Mapping.Attributes;
using System;

namespace Core.DataBase.Tests.Mapping.OneClass.IdAndName.Mapping
{
    [Class(Table = nameof(PersistentObjectFakeWithIdAndName))]
    public class PersistentObjectFakeWithIdAndName : PersistentObjectFakeWithId
    {
        private string _name;

        [Id(Column = nameof(Id), TypeType = typeof(Guid), Name = nameof(Id))]
        public override Guid Id
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

        public PersistentObjectFakeWithIdAndName(string name)
        {
            _id = Guid.NewGuid();
            _name = name;
        }
    }
}