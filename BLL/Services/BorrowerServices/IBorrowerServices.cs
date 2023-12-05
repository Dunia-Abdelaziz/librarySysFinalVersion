using BLL.DTOs;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BorrowerServices
{
    public interface IBorrowerServices
    {
        BorrowerDTO GetBorrowerById(string id);
        IEnumerable<BorrowerDTO> GetAllBorrowers();
        void AddBorrower(BorrowerDTO borrowerDto);
        void UpdateBorrower(string id, BorrowerDTO borrowerDto);
        void DeleteBorrower(string id);
        BorrowerDTO? LogIn(string username, string password);
        BorrowerDTO? SearchByUserName(string username);
    }
}
