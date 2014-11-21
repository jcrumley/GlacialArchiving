using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using TBADev.MVC.DataAttributes;
using TBADev.MVC.Entity;

namespace GlacialArchiving.DataAccess.Base
{
    public abstract partial class BaseObject : ITBAEntity
    {
        [NoDisplay]
        [NotMapped]
        public int Id
        {
            get
            {
                PropertyInfo key = this.GetKeyProperty();
                if (key == null)
                    return 0;

                int value = 0;
                int.TryParse(key.GetValue(this).ToString(), out value);
                return value;
            }
        }

        public PropertyInfo GetKeyProperty()
        {
            IEnumerable<PropertyInfo> props = this.GetType().GetProperties().Where(p => Attribute.IsDefined(p, typeof(KeyAttribute)));
            if (props.Count() == 0)
                return null;
            return props.Single();
        }
    }
}
