using GenericRepository.Attributes;
using GenericRepository.Entities;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace SharedModels.Entities.Admin
{
    [Database(null, "ADM.sp_Get_Menu")]
    public class NavBar:IEntity
    {
        [GetByParameter(SqlDbType.VarChar)]
        [NotMapped]
        public string IDUSUARIO { get; set; }
        [GetByParameter(SqlDbType.Int)]
        [NotMapped]
        public int Sistemas { get; set; }
        public int? Id { get; set; }
        public string NameOption { get; set; }
        public string DisplayOptionName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Status { get; set; }
        public bool IsParent { get; set; }
        public int ParentId { get; set; }
        [NotMapped]
        public string CreatedBy { get; set; }
        [NotMapped]
        public DateTime CreatedDate { get; set; }
        [NotMapped]
        public int? StatusId { get; set; }
        [NotMapped]
        public string UpdatedBy{ get; set; }
        [NotMapped]
        public DateTime? UpdatedDate { get; set; }
    }
}
