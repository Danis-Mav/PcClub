//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PcClub.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        public int Id { get; set; }
        public Nullable<int> IdPlace { get; set; }
        public Nullable<int> IdUser { get; set; }
        public Nullable<double> Hour { get; set; }
        public Nullable<System.DateTime> DateTimeStart { get; set; }
        public Nullable<System.DateTime> DateTimeEnd { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Place Place { get; set; }
        public virtual User User { get; set; }
    }
}