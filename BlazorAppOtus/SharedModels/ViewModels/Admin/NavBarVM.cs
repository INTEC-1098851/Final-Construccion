using SharedModels.Entities.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.ViewModels.Admin
{
    public class NavBarVM
    {
        public int Id { get; set; }
        public string NameOption { get; set; }
        public string DisplayOptionName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public bool Status { get; set; }
        public bool IsParent { get; set; }
        public int ParentId { get; set; }
        // Implicit Operator to Map from Entity to entity
        public static implicit operator NavBar(NavBarVM vm)
            => new()
            {
                Id = vm.Id,
                NameOption = vm.NameOption,
                DisplayOptionName = vm.DisplayOptionName,
                Controller = vm.Controller,
                Action = vm.Action,
                Status = vm.Status,
                IsParent = vm.IsParent,
                ParentId = vm.ParentId
            };

        public static implicit operator NavBarVM(NavBar entity)
            => new()
            {
                Id = (int)entity.Id,
                NameOption = entity.NameOption,
                DisplayOptionName = entity.DisplayOptionName,
                Controller = entity.Controller,
                Action = entity.Action,
                Status = entity.Status,
                IsParent = entity.IsParent,
                ParentId = entity.ParentId
            };


    }
}
