using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.ResultView
{
    public class ResultView<TEntity>
    {
        public bool IsSuccess { get; set; }
        public string msg { get; set; }
        public TEntity Entity { get; set; }
    }
}
