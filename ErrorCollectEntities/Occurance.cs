//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ErrorCollectEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Occurance
    {
        public int Id { get; set; }
        public int ExceptionLogId { get; set; }
        public System.DateTime Time { get; set; }
        public string StackTrace { get; set; }
        public int SessionId { get; set; }
    
        public virtual ExceptionLog ExceptionLog { get; set; }
        public virtual Session Session { get; set; }
    }
}