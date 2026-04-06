using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.Helpers
{
    // Trùng lặp dữ liệu, đã tồn tại  409
    public class DuplicateData : Exception
    {
        public DuplicateData() : base() { }

        public DuplicateData(string message) : base(message) { }

        public DuplicateData(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}

