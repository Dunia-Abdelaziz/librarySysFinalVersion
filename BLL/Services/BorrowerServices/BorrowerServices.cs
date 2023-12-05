using BLL.DTOs;
using DAL.Entities;
using DAL.Repository.BorrowerRep;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.BorrowerServices
{
    public class BorrowerServices : IBorrowerServices
    {
        private readonly IBorrowerRepository _borrowerRepository;

        public BorrowerServices(IBorrowerRepository borrowerRepository)
        {
            _borrowerRepository = borrowerRepository;
        }

        public BorrowerDTO GetBorrowerById(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            Borrower Borrower = _borrowerRepository.GetById(objectId);
            // Map Borrower entity to BorrowerDTO
            // Example using AutoMapper or manual mapping
            return new BorrowerDTO { Name = Borrower.Name, UserName = Borrower.UserName };
        }

        public IEnumerable<BorrowerDTO> GetAllBorrowers()
        {
            IEnumerable<Borrower> Borrowers = _borrowerRepository.GetAll();
            // Map list of Borrower entities to list of BorrowerDTOs
            // Example using AutoMapper or manual mapping
            return Borrowers.Select(Borrower => new BorrowerDTO { Name = Borrower.Name, UserName = Borrower.UserName , Password = Borrower.Password });
        }

        public void AddBorrower(BorrowerDTO BorrowerDto)
        {
            // Map BorrowerDTO to Borrower entity
            // Example using AutoMapper or manual mapping
            Borrower Borrower = new Borrower { Name = BorrowerDto.Name, UserName = BorrowerDto.UserName, Password = BorrowerDto.Password };
            _borrowerRepository.Add(Borrower);
        }

        public void UpdateBorrower(string id, BorrowerDTO BorrowerDto)
        {
            ObjectId objectId = ObjectId.Parse(id);
            Borrower existingBorrower = _borrowerRepository.GetById(objectId);

            if (existingBorrower != null)
            {
                existingBorrower.Name = BorrowerDto.Name;
                existingBorrower.UserName = BorrowerDto.UserName;
                _borrowerRepository.Update(existingBorrower);
            }

        }

        public void DeleteBorrower(string id)
        {
            ObjectId objectId = ObjectId.Parse(id);
            _borrowerRepository.Delete(objectId);
        }

        public BorrowerDTO? LogIn(string username, string password)
        {
           Borrower borrower = _borrowerRepository.LogIn(username, password);
            if (borrower != null)
            {
                return new BorrowerDTO { Name = borrower.Name, UserName = borrower.UserName, Password = borrower.Password };
            }
            else
            {
                return null;
            }
        }

        public BorrowerDTO? SearchByUserName(string username)
        {
            Borrower borrower = _borrowerRepository.SearchByUserName(username);
            if (borrower != null)
            {
                return new BorrowerDTO { Name = borrower.Name, UserName = borrower.UserName, Password = borrower.Password };
            }
            else
            {
                return null;
            }
        }
    }
}
